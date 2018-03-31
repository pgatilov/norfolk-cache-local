using System;

namespace NorfolkCache.Services.CacheHistory
{
    /// <summary>
    /// Represents a cache service decorator that writes history log.
    /// </summary>
    public class CacheServiceHistory : CacheServiceDecorator
    {
        private readonly ICacheHistoryWriterService _cacheHistoryService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheServiceHistory"/> class.
        /// </summary>
        /// <param name="cacheService">A cache service.</param>
        public CacheServiceHistory(ICacheService cacheService, ICacheHistoryWriterService cacheHistoryService)
            : base(cacheService)
        {
            _cacheHistoryService = cacheHistoryService ?? throw new ArgumentNullException(nameof(cacheHistoryService));
        }

        public override void Set(string @namespace, string key, string value)
        {
            try
            {
                base.Set(@namespace, key, value);
            }
            catch (Exception)
            {
                throw;
            }

            _cacheHistoryService.WriteRecords(new[]
            {
                new CacheHistoryRecord
                {
                    Action = CacheHistoryAction.Set,
                    Namespace = @namespace,
                    Key = key,
                    Value = value
                }
            });
        }
    }
}
