﻿using System;
using System.Collections.Generic;

namespace DomainClasses.MailManagement
{
    internal class MailingList_InMemory : IMailingListRepository
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
            else
            {
                if (!ml.ContainsKey(mailingList.Id))
                    throw new InvalidOperationException("Id da aggiornare inesistente");
            }

            ml[mailingList.Id] = mailingList;
        }
    }
}