using System;
using System.Threading;
using DomainClasses.MailManagement;
using FlowControlledMailSender.MailEnqueuer;
using FlowControlledMailSender.MailSender;
using log4net.Appender;
using log4net.Core;
using NUnit.Framework;

namespace WAMail.IntegrationTests
{
    //[TestFixture]
    //[Ignore("Non è un test, ma un frammento di programma che esegue al fine di stampare i logs.")]
    public class TestMailSender
    {
        [OneTimeSetUp]
        public void SetUp()
        {
            log4net.Config.BasicConfigurator.Configure(new MyAppender());
        }

        [Test]
        public void UnaMailConUnDestinatarioPuoEssereInviata()
        {
            var email = new Email(
                new string[] { "dest1" },
                "Subject",
                "Body");
            var sender = new MailEnqueuerWithFlowControl(
                new SendMail_Fake_Delay(),
                50,
                150);

            Assert.DoesNotThrow(() =>
            {
                sender.Send(email);
                Thread.Sleep(10000);
            });
        }
    }

    internal class MyAppender : IAppender
    {
        public string Name
        {
            get; set;
        }

        public void Close()
        {
        }

        public void DoAppend(LoggingEvent loggingEvent)
        {
            Console.WriteLine(loggingEvent.ToString());
        }
    }
}