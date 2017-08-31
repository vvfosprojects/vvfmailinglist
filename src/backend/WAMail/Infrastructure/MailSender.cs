using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using log4net;
using WAMail.Infrastructure.Persistence;
using WAMail.Models;

namespace WAMail.Infrastructure
{
    public class MailSender
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MailSender));

        private readonly IMailingListRepository mailingListRepository;

        public MailSender(IMailingListRepository mailingListRepository)
        {
            this.mailingListRepository = mailingListRepository;
        }

        public void Send(SendMailDTO dto, Int16 DelaySend)
        {
            var tutteLeMail = this.mailingListRepository
                .Get(dto.IdListeDestinatarie)
                .SelectMany(l => l.Emails);

            if (tutteLeMail != null && tutteLeMail.Count() > 0  )
            {
                var email = new EMail(dto.Corpo, dto.Oggetto);
                foreach (var item in tutteLeMail)
                {
                    if (item.Trim().Length > 0)
                    {
                        log.InfoFormat("Mail inviata a {0}", item);
                        email.To = item;
                        email.Send();
                        Thread.Sleep(DelaySend);
                    }
                    else
                        log.InfoFormat("Il valore dell'indirizzo non è presente.");
                }
            }
            else
                log.InfoFormat("Nessun indirizzo mail presente.");
        }
    }
}