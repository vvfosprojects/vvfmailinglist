using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMail.Infrastructure.Persistence
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
            this.Id = Guid.NewGuid().ToString();
            this.Nome = Nome;
            Emails = new List<string>();
        }
    }
}