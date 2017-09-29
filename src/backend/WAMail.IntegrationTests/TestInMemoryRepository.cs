using System;
using System.Linq;
using DomainClasses.MailManagement;
using NUnit.Framework;

namespace WAMail.IntegrationTests
{
    [TestFixture]
    public class TestInMemoryRepository
    {
        [Test]
        public void SeSiInserisceUnaMailingListGetRestituisceUnSoloRisultato()
        {
            var repository = new MailingList_InMemory();
            repository.Save(new MailingList("Belli")
            {
                Emails = new string[] { "A", "B", "C" }
            });

            var mailingLists = repository.Get();

            Assert.That(mailingLists, Has.Count.EqualTo(1));
        }

        [Test]
        public void UnaMailingListInseritaECorrettamenteRestituita()
        {
            var repository = new MailingList_InMemory();
            repository.Save(new MailingList("Belli")
            {
                Emails = new string[] { "A", "B", "C" }
            });

            var mailingList = repository.Get().Single();

            Assert.That(mailingList.Nome, Is.EqualTo("Belli"));
            CollectionAssert.AreEqual(new string[] { "A", "B", "C" }, mailingList.Emails);
        }

        [Test]
        public void UnaMailingListInseritaECorrettamenteAggiornata()
        {
            var repository = new MailingList_InMemory();
            repository.Save(new MailingList("Belli")
            {
                Emails = new string[] { "A", "B", "C" }
            });

            var mailingList = repository.Get().Single();
            var id = mailingList.Id;
            var nuovaMailingList = new MailingList("Brutti")
            {
                Id = id,
                Emails = new string[] { "X", "Y", "Z" }
            };
            repository.Save(nuovaMailingList);
            var mailingListAggiornata = repository.Get(id);

            Assert.That(mailingListAggiornata.Nome, Is.EqualTo("Brutti"));
            CollectionAssert.AreEqual(new string[] { "X", "Y", "Z" }, mailingListAggiornata.Emails);
        }

        [Test]
        public void UnaMailingListInseritaECorrettamenteEliminata()
        {
            var repository = new MailingList_InMemory();
            repository.Save(new MailingList("Belli")
            {
                Emails = new string[] { "A", "B", "C" }
            });

            var mailingList = repository.Get().Single();
            var id = mailingList.Id;
            repository.Delete(id);
            var mailingLists = repository.Get();

            Assert.That(mailingLists, Has.Count.EqualTo(0));
        }

        [Test]
        public void UnaMailingListInesistenteNonPuoEssereAggiornata()
        {
            var repository = new MailingList_InMemory();
            var mailingListInesistente = new MailingList("Belli")
            {
                Id = "Inesistente",
                Emails = new string[] { "A", "B", "C" }
            };

            Assert.Throws<InvalidOperationException>(() =>
            {
                repository.Save(mailingListInesistente);
            });
        }
    }
}