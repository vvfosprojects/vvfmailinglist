using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WAMail.Infrastructure;
using WAMail.Models;

namespace WAMail.Controllers
{
    public class ManageMailController : ApiController
    {
        [HttpGet]
        public string[] NomiLista()
        {
            var RetCode = new string[] { "Mario Rossi", "Giuseppe Verdi", "Alessandro Manzoni" };
            return RetCode;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> InviaMail(ListaMail listaMail)
        {
            var DelaySend = Convert.ToInt16(ConfigurationManager.AppSettings["DelayMail"]);

            var result = new Result(false);
            var status = HttpStatusCode.InternalServerError;

            var mail = new EMail();
            mail.Body = listaMail.TestoMail;
            mail.Subject = listaMail.Oggetto;
            foreach(var item in listaMail.Emails)
            {
                mail.To = item;
                await Task.Delay(DelaySend);
                var mResult = await mail.Send();           
            }

            return Request.CreateResponse<Result>(status,result);
        }
    }
}
