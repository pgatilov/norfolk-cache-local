using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using NorfolkCache.History;
using NorfolkCache.Services;
using NorfolkCache.Services.CacheHistory;
using NorfolkCache.Services.CacheLog;
using NorfolkCacheWebApp.Controllers;

namespace NorfolkCacheWebApp
{
    /// <summary>
    /// Represents a configuration for dependencies.
    /// </summary>
    public static class DependencyConfig
    {
        /// <summary>
        /// Registers dependencies configuration.
        /// </summary>
        /// <param name="config">A <see cref="HttpConfiguration "/>.</param>
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);

            // OPTIONAL: Register the Autofac model binder provider.
            //builder.RegisterWebApiModelBinderProvider();

            builder.RegisterModelBinders(typeof(HomeController).Assembly);

            // Register instances.
            var cacheHistoryService = new DatabaseCacheHistoryService();
            builder.RegisterInstance(cacheHistoryService).As<ICacheHistoryWriterService>().SingleInstance();
            builder.RegisterInstance(cacheHistoryService).As<ICacheHistoryService>().SingleInstance();

            var cacheService = new CacheService();
            var cacheServiceHistory = new CacheServiceHistory(cacheService, cacheHistoryService);

            var cacheServiceLog = new CacheServiceTraceLog(cacheServiceHistory);
            builder.RegisterInstance(cacheServiceLog).As<ICacheService>().SingleInstance();

            // TODO: Add NorfolkCache.SpecialServices to the solution, reference the project and uncomment the line below.
            // builder.RegisterInstance(new MyService("ServiceName")).As<IMyService>().SingleInstance();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
