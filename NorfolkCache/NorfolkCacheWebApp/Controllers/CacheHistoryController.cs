using System;
using System.Web.Mvc;
using NorfolkCache.Services.CacheHistory;

namespace NorfolkCacheWebApp.Controllers
{
    /// <summary>
    /// Represents a cache history controller.
    /// </summary>
    public class CacheHistoryController : Controller
    {
        private readonly ICacheHistoryService _cacheHistoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheHistoryController"/> class.
        /// </summary>
        public CacheHistoryController(ICacheHistoryService cacheHistoryService)
        {
            _cacheHistoryService = cacheHistoryService ?? throw new ArgumentNullException(nameof(cacheHistoryService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int total;
            var info = _cacheHistoryService.GetRecords(out total);

            ViewBag.Records = info;

            return View();
        }
    }
}
