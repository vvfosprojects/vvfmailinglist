using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace WAMail.Infrastructure
{
    public class Bindings : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<DomainClasses.Infrastructure.IAppSettings, AppSettings_WebConfig>(Lifestyle.Singleton);
        }
    }
}