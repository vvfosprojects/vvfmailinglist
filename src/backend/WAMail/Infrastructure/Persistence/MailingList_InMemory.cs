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
            var listaMail = new List<MailingList>();

            foreach(var item in ml.Keys)
            {
                listaMail.Add(ml[item]);
            }
            return listaMail;
        }

        public IEnumerable<MailingList> Get(IEnumerable<string> ids)
        {
            var listaMail = new List<MailingList>();

            foreach (var item in ml.Keys)
            {
                if (ids.Contains(item))
                    listaMail.Add(ml[item]);
            }
            return listaMail;
        }

        public void Save(MailingList mailingList)
        {
            ml.Add(Guid.NewGuid().ToString(), mailingList);
        }
    }
}