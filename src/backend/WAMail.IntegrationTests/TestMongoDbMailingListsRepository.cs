using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.MailManagement;
using NUnit.Framework;
using Persistence.MongoDB;

namespace WAMail.IntegrationTests
{
    [TestFixture]
    public class TestMongoDbMailingListsRepository
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            new DbContext().DropMailingListCollection();
        }

        [TearDown]
        public void TearDown()
        {
            new DbContext().DropMailingListCollection();
        }

        [Test]
        [Repeat(10)]
        public void UnaNuovaListaVieneSalvataConIdNonNullo()
        {
            var repository = new Persistence.MongoDB.MailingListRepository();
            var mailingList = new MailingList("TestNome")
            {
                Emails = new List<string>()
                {
                    "pippo@prova.net",
                    "pluto@prova.net",
                    "topolino@prova.net"
                }
            };

            repository.Save(mailingList);
            var theOnlyMailingList = repository.Get().Single();

            Assert.That(theOnlyMailingList.Id, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        [Repeat(10)]
        public void UnaListaPuoEssereSalvata()
        {
            var repository = new MailingListRepository();
            var mailingList = new MailingList("TestNome")
            {
                Emails = new List<string>()
                {
                    "pippo@prova.net",
                    "pluto@prova.net",
                    "topolino@prova.net"
                }
            };

            repository.Save(mailingList);
            var theOnlyMailingList = repository.Get().Single();

            Assert.That(theOnlyMailingList.Nome, Is.EqualTo("TestNome"));
        }

        [Test]
        [Repeat(10)]
        public void UnaListaPuoEssereAggiornata()
        {
            var repository = new MailingListRepository();
            var mailingList = new MailingList("TestNome")
            {
                Emails = new List<string>()
                {
                    "pippo@prova.net",
                    "pluto@prova.net",
                    "topolino@prova.net"
                }
            };

            repository.Save(mailingList);
            mailingList.Nome = "NuovoTestNome";
            repository.Save(mailingList);
            var theOnlyMailingList = repository.Get().Single();

            Assert.That(theOnlyMailingList.Nome, Is.EqualTo("NuovoTestNome"));
        }

        [Test]
        [Repeat(10)]
        public void DueListePossonoEssereSalvate()
        {
            var repository = new MailingListRepository();
            var mailingList1 = new MailingList("TestNome1")
            {
                Emails = new List<string>()
                {
                    "pippo@prova.net",
                    "pluto@prova.net",
                    "topolino@prova.net"
                }
            };
            var mailingList2 = new MailingList("TestNome2")
            {
                Emails = new List<string>()
                {
                    "pippo2@prova.net",
                    "pluto2@prova.net",
                    "topolino2@prova.net"
                }
            };

            repository.Save(mailingList1);
            repository.Save(mailingList2);
            var mailingLists = repository.Get();

            Assert.Multiple(() =>
            {
                Assert.That(mailingLists.Count(), Is.EqualTo(2));
                CollectionAssert.AreEquivalent(new[] { "TestNome1", "TestNome2" }, mailingLists.Select(ml => ml.Nome));
            }
            );
        }

        [Test]
        [Repeat(10)]
        public void UnaListaPuoEssereRecuperataPerId()
        {
            var repository = new MailingListRepository();
            var mailingList = new MailingList("TestNome")
            {
                Emails = new List<string>()
                {
                    "pippo@prova.net",
                    "pluto@prova.net",
                    "topolino@prova.net"
                }
            };

            repository.Save(mailingList);
            var idAssegnato = mailingList.Id;
            var theOnlyMailingList = repository.Get(idAssegnato);

            Assert.That(theOnlyMailingList.Id, Is.EqualTo(idAssegnato));
        }

        [Test]
        [Repeat(10)]
        public void DueListePossonoEssereRecuperatePerId()
        {
            var repository = new MailingListRepository();
            var mailingList1 = new MailingList("TestNome1")
            {
                Emails = new List<string>()
                {
                    "pippo@prova.net",
                    "pluto@prova.net",
                    "topolino@prova.net"
                }
            };
            var mailingList2 = new MailingList("TestNome2")
            {
                Emails = new List<string>()
                {
                    "pippo2@prova.net",
                    "pluto2@prova.net",
                    "topolino2@prova.net"
                }
            };

            repository.Save(mailingList1);
            repository.Save(mailingList2);
            var ids = new[] { mailingList1.Id, mailingList2.Id };
            var mailingLists = repository.Get(ids);

            Assert.Multiple(() =>
            {
                Assert.That(mailingLists.Count(), Is.EqualTo(2));
                CollectionAssert.AreEquivalent(ids, mailingLists.Select(ml => ml.Id));
            }
            );
        }

        [Test]
        [Repeat(10)]
        public void UnaListaPuoEssereCancellataPerId()
        {
            var repository = new MailingListRepository();
            var mailingList1 = new MailingList("TestNomeDelete")
            {
                Emails = new List<string>()
                {
                    "pippo@prova.net",
                    "pluto@prova.net",
                    "topolino@prova.net"
                }
            };
            var mailingList2 = new MailingList("TestNomeDelete1")
            {
                Emails = new List<string>()
                {
                    "pippo2@prova.net",
                    "pluto2@prova.net",
                    "topolino2@prova.net"
                }
            };

            repository.Save(mailingList1);
            var idDaEliminare = mailingList1.Id;
            repository.Save(mailingList2);
            repository.Delete(idDaEliminare);
            var mailingLists = repository.Get();

            Assert.AreEqual(new string[] { mailingList2.Id }, mailingLists.Select(ml => ml.Id));
        }
    }
}