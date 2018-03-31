using System;
using System.Web.Mvc;
using NorfolkCache.Services;

namespace NorfolkCacheWebApp.Controllers
{
    /// <summary>
    /// Represents a home controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ICacheService _cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="cacheService"></param>
        public HomeController(ICacheService cacheService)
        {
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var info = _cacheService.GetInfo();

            ViewBag.GetRequestsCount = info.TotalGetRequests;
            ViewBag.SetRequestsCount = info.TotalSetRequests;
            ViewBag.NamespaceCount = info.TotalNamespaces;
            ViewBag.KeyCount = info.TotalKeys;
            ViewBag.Title = "Norfolk Cache";

            return View();
        }
    }
}
