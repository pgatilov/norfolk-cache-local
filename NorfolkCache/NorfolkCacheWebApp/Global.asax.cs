using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NorfolkCacheWebApp
{
    /// <summary>
    /// Represents a Web API apllication.
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Starts an application.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DependencyConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}
