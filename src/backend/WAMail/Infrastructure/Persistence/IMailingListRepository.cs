using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAMail.Infrastructure.Persistence
{
    public interface IMailingListRepository
    {
        IEnumerable<MailingList> Get();

        IEnumerable<MailingList> Get(IEnumerable<string> ids);

        MailingList Get(string id);

        void Save(MailingList mailingList);

        void Delete(string id);
    }
}