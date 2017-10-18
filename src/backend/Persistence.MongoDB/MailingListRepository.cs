using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.MailManagement;
using MongoDB.Driver;

namespace Persistence.MongoDB
{
    internal class MailingListRepository : IMailingListRepository
    {
        private readonly DbContext dbContext;

        public MailingListRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(string id)
        {
            this.dbContext.MailingLists
                .DeleteOne(ml => ml.Id == id);
        }

        public IEnumerable<MailingList> Get()
        {
            return this.dbContext.MailingLists
                .Find(_ => true)
                .ToEnumerable();
        }

        public MailingList Get(string id)
        {
            return this.dbContext.MailingLists
                .Find(ml => ml.Id == id)
                .Single();
        }

        public IEnumerable<MailingList> Get(IEnumerable<string> ids)
        {
            var filter = Builders<MailingList>
                .Filter
                .In(ml => ml.Id, ids);

            return this.dbContext.MailingLists
                .Find(filter)
                .ToEnumerable();
        }

        public void Save(MailingList mailingList)
        {
            if (string.IsNullOrEmpty(mailingList.Id))
                dbContext.MailingLists.InsertOne(mailingList);
            else
                dbContext.MailingLists
                    .ReplaceOne(ml => ml.Id == mailingList.Id, mailingList, new UpdateOptions() { IsUpsert = true });
        }

        public void DropCollection()
        {
            dbContext.DropMailingListCollection();
        }
    }
}