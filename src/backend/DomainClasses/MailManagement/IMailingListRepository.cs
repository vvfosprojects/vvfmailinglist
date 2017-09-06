using System.Collections.Generic;

namespace DomainClasses.MailManagement
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