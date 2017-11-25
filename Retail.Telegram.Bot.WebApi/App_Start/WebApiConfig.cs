using System.Web.Http;

namespace Retail.Telegram.Bot.WebApi
{
    public static class WebApiConfig
    {
        /// <summary>
        /// Register the only routing for web api
        /// </summary>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();

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
