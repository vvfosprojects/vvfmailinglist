using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMail.Infrastructure.Persistence
{
    public class MailingList_InMemory : IGetMailingList, IGetMailingListById, ISaveMailingList, IDeleteMailingList
    {
        private static Dictionary<string, MailingList> ml = new Dictionary<string, MailingList>();
        
        public void Delete(string id)
        {
            if (ml.ContainsKey(id)) ml.Remove(id);
        }

        public IEnumerable<MailingList> Get()
        {
            return from item in ml
                  select new MailingList(item.Value.Nome)
                  {
                    Id = item.Value.Id,
                    Emails = item.Value.Emails
                  };
        }

        public IEnumerable<MailingList> Get(IEnumerable<string> ids)
        {
            return from item in ml
                   where ids.Contains(item.Value.Id)
                   select new MailingList(item.Value.Nome)
                   {
                       Id = item.Value.Id,
                       Emails = item.Value.Emails
                   };
        }

        public void Save(MailingList mailingList)
        {
            if (string.IsNullOrWhiteSpace(mailingList.Id))
            {
                mailingList.Id = Guid.NewGuid().ToString();
                ml.Add(mailingList.Id, mailingList);
            }
            else
            {
                ml[mailingList.Id].Nome = mailingList.Nome;
                ml[mailingList.Id].Emails = mailingList.Emails;
            }
        }
    }
}