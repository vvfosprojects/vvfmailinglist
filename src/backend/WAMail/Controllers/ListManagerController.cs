using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using DomainClasses.MailManagement;

namespace WAMail.Controllers
{
    public class ListManagerController : ApiController
    {
        private readonly IMailingListRepository mailingListRepository;

        public ListManagerController(IMailingListRepository mailingListRepository)
        {
            this.mailingListRepository = mailingListRepository;
        }

        // GET: api/ListManager
        public IEnumerable<MailingList> Get()
        {
            return this.mailingListRepository.Get();
        }

        // GET: api/ListManager/5
        public MailingList Get(string id)
        {
            return mailingListRepository.Get(id);
        }

        // POST: api/ListManager
        public IHttpActionResult Post([FromBody]MailingList mailingList)
        {
            if (!string.IsNullOrWhiteSpace(mailingList.Id))
            {
                throw new InvalidOperationException("L'id deve essere null per una nuova lista");
            }

            mailingListRepository.Save(mailingList);

            return Ok(new { id = mailingList.Id });
        }

        // PUT: api/ListManager/5
        public IHttpActionResult Put([FromBody]MailingList mailingList)
        {
            if (string.IsNullOrWhiteSpace(mailingList.Id))
            {
                throw new InvalidOperationException("L'id non può essere null in caso di aggiornamento");
            }

            mailingListRepository.Save(mailingList);

            return Ok(new { id = mailingList.Id });
        }

        // DELETE: api/ListManager/5
        public IHttpActionResult Delete(string id)
        {
            mailingListRepository.Delete(id);

            return Ok(new { id = id });
        }
    }
}