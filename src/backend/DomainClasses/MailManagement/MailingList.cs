using System;
using System.Collections.Generic;

namespace DomainClasses.MailManagement
{
    public class MailingList
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public IList<string> Emails { get; set; }

        protected MailingList()
        {
        }

        public MailingList(string Nome)
        {
            this.Nome = Nome;
            Emails = new List<string>();
        }

        public void InitializeId()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}