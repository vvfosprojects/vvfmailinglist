using DomainClasses.MailManagement;
using log4net;
using System.Net.Mail;

namespace FlowControlledMailSender.MailSender
{
    public class SendMail : ISendMail
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Send(Email email)
        {
            log.DebugFormat("Invio mail con oggetto {0}...", email.Subject);

            var message = new MailMessage();
            foreach (var emailBcc in email.Bcc)
            {
                if (!string.IsNullOrEmpty(emailBcc))
                {
                    message.Bcc.Add(new MailAddress(emailBcc));
                }
                log.DebugFormat("Aggiunto il seguente destinatario: {0}", emailBcc);
            }
            message.Subject = email.Subject;
            message.Body = email.Body;
            message.IsBodyHtml = email.IsBodyHtml;
            using (var smtp = new SmtpClient())
            {
                smtp.Send(message);
                log.DebugFormat("Mail inviata con oggetto {0}...", email.Subject);
            }
        }
    }
}
