using System.Linq;

namespace DomainClasses.MailManagement
{
    public class Email
    {
        public Email(string[] to, string[] cc, string[] bcc, string subject, string body, bool isBodyHtml = false)
        {
            this.To = to;
            this.Cc = cc;
            this.Bcc = bcc;
            this.Subject = subject;
            this.Body = body;
            this.IsBodyHtml = isBodyHtml;
        }

        public Email(string[] to, string subject, string body, bool isBodyHtml = false) : this(to, new string[0], new string[0], subject, body, isBodyHtml)
        {
        }

        public string[] To { get; private set; }
        public string[] Cc { get; private set; }
        public string[] Bcc { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public bool IsBodyHtml { get; set; }

        public int RecipientCount
        {
            get
            {
                return this.To.Length + this.Cc.Length + this.Bcc.Length;
            }
        }

        public string Digest
        {
            get
            {
                const int maxRecipientCountForDigest = 4;
                string recipientDigest;
                var allRecipients = this.To.Concat(this.Cc.Concat(this.Bcc));
                if (this.RecipientCount > maxRecipientCountForDigest)
                {
                    recipientDigest = $"Destinatari: {string.Join(", ", allRecipients.Take(maxRecipientCountForDigest))} (+ altri {this.RecipientCount - maxRecipientCountForDigest})";
                }
                else
                {
                    recipientDigest = $"Destinatari: {string.Join(", ", allRecipients)}";
                }

                return $"Oggetto: {this.Subject} - {recipientDigest}";
            }
        }
    }
}