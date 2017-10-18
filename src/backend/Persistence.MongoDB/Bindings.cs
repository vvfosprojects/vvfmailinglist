using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainClasses.Infrastructure;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace Persistence.MongoDB
{
    public class Bindings : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<DomainClasses.MailManagement.IMailingListRepository>(() =>
            {
                return new MailingListRepository(new DbContext(container.GetInstance<IAppSettings>().ConnectionString));
            }, Lifestyle.Singleton);
        }
    }
}