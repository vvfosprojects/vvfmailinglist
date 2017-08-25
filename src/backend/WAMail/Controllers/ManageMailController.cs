using System;
using System.Collections.Generic;
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
            var RetCode = new string[] { "pippo", "pluto" };
            return RetCode;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> InviaMail(List<ListaMail> listaMail)
        {
            var mail = new EMail();
            var result = new Result(false);
            var status = HttpStatusCode.InternalServerError;
            foreach(var item in listaMail)
            {
                await Task.Delay(300);
               
                var mResult = await mail.Send();           
            }

            return Request.CreateResponse<Result>(status,result);
        }
    }
}
