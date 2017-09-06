[assembly: WebActivator.PostApplicationStartMethod(typeof(WAMail.App_Start.SimpleInjectorWebApiInitializer), "Initialize")]

namespace WAMail.App_Start
{
    using System.Linq;
    using System.Reflection;
    using System.Web.Compilation;
    using System.Web.Http;
    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>
        /// Initialize the container and register it as Web API Dependency Resolver.
        /// </summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new SimpleInjector.Lifestyles.AsyncScopedLifestyle();

            // Scan all the referenced assemblies for packages containing DI wiring rules
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            container.RegisterPackages(assemblies);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

#if DEBUG
            container.Verify();
#endif

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void InitializeContainer(Container container)
        {
            // For instance: container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }
    }
}