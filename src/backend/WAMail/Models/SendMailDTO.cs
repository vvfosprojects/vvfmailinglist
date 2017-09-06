namespace WAMail.Models
{
    public class SendMailDTO
    {
        public string[] IdListeDestinatarie { get; set; }
        public string Oggetto { get; set; }
        public string Corpo { get; set; }
    }
}