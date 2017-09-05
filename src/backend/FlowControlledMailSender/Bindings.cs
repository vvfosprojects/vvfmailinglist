using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowControlledMailSender.MailEnqueuer;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace FlowControlledMailSender
{
    public class Bindings : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterConditional<MailSender.ISendMail, MailSender.SendMail_Fake_Delay>(
                Lifestyle.Scoped,
                pc => pc.Consumer.ImplementationType == typeof(MailEnqueuerWithFlowControl)
            );

            container.RegisterConditional<MailSender.ISendMail, MailEnqueuerWithFlowControl>(
                Lifestyle.Singleton,
                pc => pc.Consumer.ImplementationType != typeof(MailEnqueuerWithFlowControl)
            );
        }
    }
}