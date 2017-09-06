using System;
using System.Configuration;
using DomainClasses.MailManagement;
using FlowControlledMailSender.MailEnqueuer;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace FlowControlledMailSender
{
    public class Bindings : IPackage
    {
        public void RegisterServices(Container container)
        {
            var mailSenderRegistration = Lifestyle.Singleton.CreateRegistration<MailEnqueuerWithFlowControl>(() =>
            {
                return new MailEnqueuerWithFlowControl(container.GetInstance<ISendMail>(), 50, 150);
            },
            container);

            container.AddRegistration<MailEnqueuerWithFlowControl>(mailSenderRegistration);

            container.Register(typeof(ISendMail), () =>
            {
                return new MailEnqueuerWithFlowControl(
                    new MailSender.SendMail_Fake_Delay(),
                    Convert.ToInt32(ConfigurationManager.AppSettings["maxMailPerMinute"]),
                    Convert.ToInt32(ConfigurationManager.AppSettings["maxRecipientCount"]));
            }, Lifestyle.Singleton);
        }
    }
}