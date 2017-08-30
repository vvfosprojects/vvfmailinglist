using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;
using WAMail.Infrastructure;
using WAMail.Infrastructure.Persistence;
using WAMail.Models;

namespace WAMail.Controllers
{
    public class EmailManagerController : ApiController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(EmailManagerController));

        private readonly IMailingListRepository mailingListRepository;

        public EmailManagerController(IMailingListRepository mailingListRepository)
        {
            this.mailingListRepository = mailingListRepository;
        }

        // GET: api/EmailManager
        public IEnumerable<MailingListsInfoDTO> Get()
        {
            return mailingListRepository.Get()
                .Select(ml => new MailingListsInfoDTO(ml.Id, ml.Nome));
        }

        //// POST: api/EmailManager
        public void Post([FromBody]SendMailDTO value)
        {
            Task.Run(() =>
            {
                log.Debug("Inizio...");
                Thread.Sleep(5000);
                log.Debug("Fine");
            });

            //var DelaySend = Convert.ToInt16(ConfigurationManager.AppSettings["DelayMail"]);

            //var result = new Result(false);
            //var status = HttpStatusCode.InternalServerError;

            //var mail = new EMail();
            //mail.Body = value.Corpo;
            //mail.Subject = value.Oggetto;
            //foreach (var item in value.ListeDestinatarie)
            //{
            //    mail.To = item;
            //    await Task.Delay(DelaySend);
            //    result = await mail.Send();
            //}
        }

        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/EmailManager/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/EmailManager/5
        //public void Delete(int id)
        //{
        //}
    }
}