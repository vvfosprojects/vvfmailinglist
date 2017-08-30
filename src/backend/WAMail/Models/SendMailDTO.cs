using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WAMail.Models
{
    public class SendMailDTO
    {
        public string[] ListeDestinatarie { get; set; }
        public string Oggetto { get; set; }
        public string Corpo { get; set; }
    }
}