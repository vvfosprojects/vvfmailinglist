using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.MailManagement;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Persistence.MongoDB
{
    internal class DbContext
    {
        private static IMongoDatabase database;
        private readonly string mailingListsCollectionName = "mailingLists";

        public DbContext(string connectionString)
        {
            if (database == null)
            {
                var pack = new ConventionPack();
                pack.Add(new CamelCaseElementNameConvention());
                ConventionRegistry.Register("camel case", pack, _ => true);

                BsonClassMap.RegisterClassMap<MailingList>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(ml => ml.Id)
                        .SetIgnoreIfDefault(true)
                        .SetIdGenerator(StringObjectIdGenerator.Instance)
                        .SetSerializer(new StringSerializer(BsonType.ObjectId));
                });

                var url = new MongoUrl(connectionString);
                var client = new MongoClient(url);
                database = client.GetDatabase(url.DatabaseName);
            }
        }

        public IMongoCollection<MailingList> MailingLists { get { return database.GetCollection<MailingList>(mailingListsCollectionName); } }

        public void DropMailingListCollection()
        {
            database.DropCollection(mailingListsCollectionName);
        }
    }
}