using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DomainClasses.MailManagement;
using log4net;

namespace FlowControlledMailSender.MailEnqueuer
{
    internal enum recipientEnum
    {
        To,
        Cc,
        Bcc
    }

    internal class MailEnqueuerWithFlowControl : ISendMail
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly int maxMailPerMinute;
        private readonly ISendMail sendMail;
        private readonly int maxRecipientCount;

        private object lockObj = new object();
        private Task senderTask;
        private Queue<Email> emailQueue = new Queue<Email>();
        private DateTime nextSendDate;

        /// <summary>
        /// Costruttore della classe
        /// </summary>
        /// <param name="maxMailPerMinute">
        /// Il massimo numero di mail al minuto che il server tollera, oltre il quale agiscono i
        /// sistemi di antispam e le mail in più vengono droppate.
        /// </param>
        public MailEnqueuerWithFlowControl(
            ISendMail sendMail,
            int maxMailPerMinute,
            int maxRecipientCount)
        {
            this.sendMail = sendMail;
            this.maxMailPerMinute = maxMailPerMinute;
            this.maxRecipientCount = maxRecipientCount;
        }

        public void Send(Email email)
        {
            if (!this.EmailCanBeSent(email))
            {
                log.InfoFormat("La mail presenta un numero elevato di destinari. {0}", email.Digest);
                var emailSplitter = splitterEmail(email);
                foreach (var item in emailSplitter)
                {
                    this.EnqueueSafe(item);
                }
            }
            else
            {
                this.EnqueueSafe(email);
            }
        }

        private IEnumerable<Email> splitterEmail(Email email)
        {
            var emailSplit = new List<Email>();
            var itemToCount = splitRecipient(email.To, emailSplit, email.Subject, email.Body, email.IsBodyHtml, recipientEnum.To);
            var itemCcCount = splitRecipient(email.Cc, emailSplit, email.Subject, email.Body, email.IsBodyHtml, recipientEnum.Cc);
            var itemBccCount = splitRecipient(email.Bcc, emailSplit, email.Subject, email.Body, email.IsBodyHtml, recipientEnum.Bcc);

            var appoTo = new string[itemToCount];
            var appoCc = new string[itemCcCount];
            var appoBcc = new string[itemBccCount];

            appoTo.CopyTo(email.To, itemToCount);
            emailSplit.Add(new Email(appoTo, null, null, email.Subject, email.Body, email.IsBodyHtml));

            appoCc.CopyTo(email.Cc, itemToCount);
            emailSplit.Add(new Email(null, appoCc, null, email.Subject, email.Body, email.IsBodyHtml));

            appoBcc.CopyTo(email.Bcc, itemBccCount);
            emailSplit.Add(new Email(null, null, appoBcc, email.Subject, email.Body, email.IsBodyHtml));

            return emailSplit;
        }

        private int splitRecipient(string[] recipient, List<Email> email, string Subject, string Body, bool IsBodyHtml, recipientEnum recipientEnum)
        {
            var RetCode = recipient.Length;

            if (recipient.Length > this.maxRecipientCount)
            {
                var splitRecipient = new string[this.maxRecipientCount];
                var Index = 0;
                for (var i = 0; i < (recipient.Length / this.maxRecipientCount); i++)
                {
                    recipient.CopyTo(splitRecipient, Index);
                    switch (recipientEnum)
                    {
                        case recipientEnum.Bcc:
                            email.Add(new Email(null, null, splitRecipient, Subject, Body, IsBodyHtml));
                            break;

                        case recipientEnum.Cc:
                            email.Add(new Email(null, splitRecipient, null, Subject, Body, IsBodyHtml));
                            break;

                        case recipientEnum.To:
                            email.Add(new Email(splitRecipient, null, null, Subject, Body, IsBodyHtml));
                            break;
                    }
                    Index += this.maxRecipientCount;
                }
                RetCode = recipient.Length - Index;
            }
            return RetCode;
        }

        private bool EmailCanBeSent(Email email)
        {
            return email.RecipientCount < this.maxRecipientCount;
        }

        private void StartSenderTask()
        {
            if (senderTask == null)
            {
                senderTask = Task.Factory.StartNew(SendEnqueuedMails);
            }
        }

        private void SendEnqueuedMails()
        {
            log.Debug("Task avviato.");
            var logPrinted = false;

            while (emailQueue.Any())
            {
                if (DateTime.Now < this.nextSendDate)
                {
                    if (!logPrinted)
                    {
                        log.DebugFormat("Attendo {0} msec prima di inviare la prossima mail.", this.nextSendDate.Subtract(DateTime.Now).TotalMilliseconds);
                        logPrinted = true;
                    }
                    Thread.Sleep(100);
                }
                else
                {
                    var email = DequeueSafe();
                    try
                    {
                        this.sendMail.Send(email);
                    }
                    catch (Exception ex)
                    {
                        log.WarnFormat("Errore nell'invio sincrono della mail. Errore: {0} - Mail: {1}", ex.Message, email.Digest);
                    }

                    log.InfoFormat("Mail inviata. - {0}", email.Digest);
                    this.nextSendDate = DateTime.Now.AddSeconds(SingleMailDelay_secs);
                    logPrinted = false;
                }
            }

            log.Debug("Task concluso.");
            this.senderTask = null;
        }

        private Email DequeueSafe()
        {
            Email email;

            lock (lockObj)
            {
                email = this.emailQueue.Dequeue();
            }

            log.DebugFormat("DequeueSafe invocato per l'oggetto {0}", email.Subject);
            return email;
        }

        private void EnqueueSafe(Email email)
        {
            lock (lockObj)
            {
                this.emailQueue.Enqueue(email);
            }

            log.DebugFormat("EnqueueSafe invocato per l'oggetto {0}", email.Subject);
            this.StartSenderTask();
        }

        private Double SingleMailDelay_secs
        {
            get
            {
                const int secondsPerMinute = 60;

                return (double)secondsPerMinute / (this.maxMailPerMinute);
            }
        }
    }
}