using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlowControlledMailSender.DomainClasses;
using log4net;
using log4net.Core;

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