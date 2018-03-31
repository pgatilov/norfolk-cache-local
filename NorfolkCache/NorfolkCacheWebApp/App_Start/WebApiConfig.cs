using System.Web.Http;

namespace NorfolkCacheWebApp
{
    /// <summary>
    /// Represents a configuraion for Web API.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers a Web API
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.Routes.MapHttpRoute(
            //    name: "CacheGetNamespaces",
            //    routeTemplate: "api/cache/namespaces",
            //    defaults: new { controller = "Cache", action = "GetNamespaces" });

            //config.Routes.MapHttpRoute(
            //    name: "CacheGetNamespace",
            //    routeTemplate: "api/cache/namespaces/{namespace}",
            //    defaults: new { controller = "Cache", action = "GetNamespace" });

            //config.Routes.MapHttpRoute(
            //    name: "CacheGetNamespaceKeys",
            //    routeTemplate: "api/cache/namespaces/{namespace}",
            //    defaults: new { controller = "Cache", action = "GetNamespace" });

            //config.RegisterKeyValueManagement("api/cache/keys/{namespace}/{key}");

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }

        private static void RegisterKeyValueManagement(this HttpConfiguration config, string routeTemplate)
        {
            const string controllerName = "Cache";

            config.Routes.MapHttpRoute("RemoveKey", routeTemplate, new { controller = controllerName, action = "RemoveKey" });
            config.Routes.MapHttpRoute("GetKeyValue", routeTemplate, new { controller = controllerName, action = "GetKeyValue" });
            config.Routes.MapHttpRoute("SetKeyValue", routeTemplate, new { controller = controllerName, action = "SetKeyValue" });
        }
    }
}
