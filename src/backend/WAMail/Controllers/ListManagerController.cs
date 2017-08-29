using System.Collections.Generic;
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
            var listModel = mlInMemory.Get(new List<string>() { id });

            foreach (var item in listModel)
                return item;

            return null;
        }

        // POST: api/ListManager
        public void Post([FromBody]MailingList value)
        {
            mlInMemory.Save(value);
        }

        // PUT: api/ListManager/5
        public void Put(string id, [FromBody]MailingList value)
        {
            var model = mlInMemory.Get(new List<string>() { id });

            foreach (var item in model) {
                item.Emails = value.Emails;
                item.Nome = value.Nome;
            }
        }

        // DELETE: api/ListManager/5
        public void Delete(string id)
        {
            mlInMemory.Delete(id);
        }
    }
}
