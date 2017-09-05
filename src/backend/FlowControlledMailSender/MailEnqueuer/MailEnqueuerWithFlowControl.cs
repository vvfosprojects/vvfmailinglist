using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FlowControlledMailSender.DomainClasses;
using FlowControlledMailSender.MailSender;
using log4net;

namespace FlowControlledMailSender.MailEnqueuer
{
    internal class MailEnqueuerWithFlowControl : ISendMail
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly int maxMailPerMinute;
        private readonly ISendMail sendMail;

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
            int maxMailPerMinute)
        {
            this.sendMail = sendMail;
            this.maxMailPerMinute = maxMailPerMinute;
        }

        public void Send(Email email)
        {
            if (!this.EmailCanBeSent(email))
            {
                log.WarnFormat("Impossibile inviare l'email. {0}", email.Digest);
                throw new InvalidOperationException("La mail non può essere inviata.");
            }

            this.EnqueueSafe(email);
        }

        private bool EmailCanBeSent(Email email)
        {
            return email.RecipientCount <= this.maxMailPerMinute;
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
                        log.DebugFormat("Attendo {0} secondi prima di inviare la prossima mail.", this.nextSendDate.Subtract(DateTime.Now).TotalSeconds);
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
                    this.nextSendDate = DateTime.Now.AddSeconds(email.RecipientCount * SingleMailDelay_secs);
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

                return secondsPerMinute / (this.maxMailPerMinute);
            }
        }
    }
}