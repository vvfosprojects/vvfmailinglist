using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using WAMail.Infrastructure.Persistence;
using WAMail.Models;

namespace WAMail.Controllers
{
    public class ListManagerController : ApiController
    {
        private MailingList_InMemory mlInMemory = new MailingList_InMemory();

        // GET: api/ListManager
        public IEnumerable<MailingList> Get()
        {
            return mlInMemory.Get(); 
        }

        // GET: api/ListManager/5
        public MailingList Get(string id)
        {
            var model = mlInMemory.Get(new List<string>() { id });
            foreach (var item in model)
                return item;
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // POST: api/ListManager
        public void Post([FromBody]MailingList value)
        {
            var model = value;

            model.Id = string.Empty;
            mlInMemory.Save(model);
        }

        // PUT: api/ListManager/5
        public void Put(string id, [FromBody]MailingList value)
        {
            var model = mlInMemory.Get(new List<string>() { id });
            foreach (var item in model)
            {
                if (value == null)
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                value.Id = id;
                mlInMemory.Save(value);

                return;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE: api/ListManager/5
        public void Delete(string id)
        {
            var model = mlInMemory.Get(new List<string>() { id });
            foreach (var item in model)
            {
                mlInMemory.Delete(id);

                return;
            }
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
