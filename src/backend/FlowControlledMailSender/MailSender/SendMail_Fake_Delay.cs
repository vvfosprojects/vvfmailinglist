using System.Threading;
using DomainClasses.MailManagement;
using log4net;

namespace FlowControlledMailSender.MailSender
{
    public class SendMail_Fake_Delay : ISendMail
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Send(Email email)
        {
            log.DebugFormat("Invio mail fake con oggetto {0}...", email.Subject);
            Thread.Sleep(1000);
            log.DebugFormat("Mail fake inviata con oggetto {0}...", email.Subject);
        }
    }
}