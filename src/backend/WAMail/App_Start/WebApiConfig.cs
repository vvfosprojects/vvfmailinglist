using System.Web.Http;
using System.Web.Http.Cors;

namespace WAMail
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var corsAttribute = new EnableCorsAttribute("*", 
                                                        "Origin, Content-Type, Accept",
                                                        "GET, PUT, POST, DELETE, OPTIONS");
            config.EnableCors(corsAttribute);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}