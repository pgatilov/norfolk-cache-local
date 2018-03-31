using System.Web.Mvc;
using System.Web.Routing;

namespace NorfolkCacheWebApp
{
    /// <summary>
    /// Represents a configuration for routes.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers routes.
        /// </summary>
        /// <param name="routes">A <see cref="RouteCollection"/>.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
