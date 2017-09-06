using System;
using System.Collections.Generic;

namespace DomainClasses.MailManagement
{
    internal class MailingList_Hardcoded : IMailingListRepository
    {
        private Dictionary<string, MailingList> ml = new Dictionary<string, MailingList>()
        {
            { "L1", new MailingList("L1")
                {
                    Id = "L1",
                    Emails = new string[] { "pippo", "pluto"}
                }
            },
            { "L2", new MailingList("L2")
                {
                    Id = "L2",
                    Emails = new string[] { "pippo", "topolino"}
                }
            }
        };

        public void Delete(string id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}