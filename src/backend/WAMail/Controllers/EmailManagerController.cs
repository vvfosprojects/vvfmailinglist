using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WAMail.Infrastructure;
using WAMail.Infrastructure.Persistence;
using WAMail.Models;

namespace WAMail.Controllers
{
    public class EmailManagerController : ApiController
    {
        private MailingList_InMemory mlInMemory = new MailingList_InMemory();

        // GET: api/EmailManager
        public IEnumerable<MailingListModel> Get()
        {
            var listMailingModel = new List<MailingListModel>();
            var model = mlInMemory.Get();

            foreach(var item in model)
            {
                listMailingModel.Add(new MailingListModel()
                {
                    Id = item.Id,
                    Nome = item.Nome
                });
            }

            return listMailingModel;
        }
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/EmailManager/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/EmailManager
        public async Task<HttpResponseMessage> Post([FromBody]SendingMailingListModel value)
        {
            var DelaySend = Convert.ToInt16(ConfigurationManager.AppSettings["DelayMail"]);

            var result = new Result(false);
            var status = HttpStatusCode.InternalServerError;

            var mail = new EMail();
            mail.Body = value.Corpo;
            mail.Subject = value.Oggetto;
            foreach (var item in value.ListeDestinatarie)
            {
                mail.To = item;
                await Task.Delay(DelaySend);
                var mResult = await mail.Send();
            }

            return Request.CreateResponse<Result>(status, result);
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
