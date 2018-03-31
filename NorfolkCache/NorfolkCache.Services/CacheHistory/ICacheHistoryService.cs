using System.Collections.Generic;

namespace NorfolkCache.Services.CacheHistory
{
    /// <summary>
    /// Represents a cache history service.
    /// </summary>
    public interface ICacheHistoryService
    {
        IList<CacheHistoryRecord> GetRecords(out int totalNumber, int start = 0, int end = 0);
    }
}
