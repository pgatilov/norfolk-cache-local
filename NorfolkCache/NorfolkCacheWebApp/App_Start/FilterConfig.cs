using System.Web.Mvc;

namespace NorfolkCacheWebApp
{
    /// <summary>
    /// Represents a configuration for filters.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Registers global filters.
        /// </summary>
        /// <param name="filters">A <see cref="GlobalFilterCollection"/>.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
