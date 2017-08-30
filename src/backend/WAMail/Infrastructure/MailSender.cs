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

        public void Send(SendMailDTO dto)
        {
            var tutteLeMail = this.mailingListRepository
                .Get(dto.IdListeDestinatarie)
                .SelectMany(l => l.Emails);
            log.DebugFormat("Indirizzi: {0}", string.Join(", ", tutteLeMail));

            Thread.Sleep(5000);
        }
    }
}