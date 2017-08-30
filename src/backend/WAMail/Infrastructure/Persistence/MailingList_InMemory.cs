using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WAMail.Infrastructure.Persistence
{
    public class MailingList_InMemory : IMailingListRepository
    {
        private Dictionary<string, MailingList> ml = new Dictionary<string, MailingList>();

        public void Delete(string id)
        {
            if (!ml.Remove(id))
                throw new InvalidOperationException("Nessun elemento con l'id dato");
        }

        public IEnumerable<MailingList> Get()
        {
            return ml.Values;
        }

        public IEnumerable<MailingList> Get(IEnumerable<string> ids)
        {
            foreach (var id in ids)
                yield return ml[id];
        }

        public MailingList Get(string id)
        {
            return ml[id];
        }

        public void Save(MailingList mailingList)
        {
            if (string.IsNullOrWhiteSpace(mailingList.Id))
            {
                mailingList.InitializeId();
            }

            ml[mailingList.Id] = mailingList;
        }
    }
}