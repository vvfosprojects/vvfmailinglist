using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace DomainClasses
{
    public class Bindings : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<DomainClasses.MailManagement.IMailingListRepository, MailManagement.MailingList_InMemory>(Lifestyle.Singleton);
        }
    }
}