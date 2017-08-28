using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMail.Models
{
    public class ListaMail
    {
        public string Id { get; set; }
        public string Oggetto { get; set; }
        public string TestoMail { get; set; }
        public string Nome { get; set; }
        public string[] Emails { get; set; }    

    }
}