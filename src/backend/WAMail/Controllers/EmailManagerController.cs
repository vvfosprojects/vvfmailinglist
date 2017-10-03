using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Http;
using DomainClasses.MailManagement;
using log4net;
using WAMail.Models;

namespace WAMail.Controllers
{
    public class EmailManagerController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EmailManagerController));

        private readonly IMailingListRepository mailingListRepository;
        private readonly ISendMail mailSender;

        public EmailManagerController(IMailingListRepository mailingListRepository,
            ISendMail mailSender)
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
        public IHttpActionResult Post([FromBody]SendMailDTO dto)
        {
            Thread.Sleep(2000);
            log.Debug("Invio mail...");
            try
            {
                var mailingLists = mailingListRepository.Get(dto.IdListeDestinatarie);
                var destinatari = mailingLists.SelectMany(ml => ml.Emails).Distinct().ToArray();
                var email = new Email(new string[0], new string[0], destinatari, dto.Oggetto, dto.Corpo);

                mailSender.Send(email);
            }
            catch (Exception ex)
            {
                log.ErrorFormat("L'invio della mail non è stato completato per i seguente errore :\n Error: {0}\n Description: {1}", ex.HResult, ex.Message);
                throw;
            }

            log.Debug("Mail inviate");

            return Ok(new { id = dto.IdListeDestinatarie });
        }
    }
}