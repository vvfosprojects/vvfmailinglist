using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using WAMail.Infrastructure;
using WAMail.Infrastructure.Persistence;
using WAMail.Models;
using System.Configuration;
using System;

namespace WAMail.Controllers
{
    public class EmailManagerController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EmailManagerController));

        private readonly IMailingListRepository mailingListRepository;
        private readonly MailSender mailSender;

        public EmailManagerController(IMailingListRepository mailingListRepository,
            MailSender mailSender)
        {
            this.mailingListRepository = mailingListRepository;
            this.mailSender = mailSender;
        }

        // GET: api/EmailManager
        public IEnumerable<MailingListsInfoDTO> Get()
        {
            return mailingListRepository.Get()
                .Select(ml => new MailingListsInfoDTO(ml.Id, ml.Nome));
        }

        // POST: api/EmailManager
        public void Post([FromBody]SendMailDTO dto)
        {
            Task.Run(() =>
            {
                log.Debug("Inizio...");
                try
                {
                    var delaySend = Convert.ToInt16(ConfigurationManager.AppSettings["DelayMail"]);
                    mailSender.Send(dto, delaySend);
                }
                catch(Exception ex)
                {
                    log.ErrorFormat("L'invio della mail non è stato completato per i seguente errore :\n Error: {0}\n Description: {1}", ex.HResult, ex.Message);
                }

                log.Debug("Fine");
            });
        }
    }
}