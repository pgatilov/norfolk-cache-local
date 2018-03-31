using System.Collections.Generic;

namespace NorfolkCache.Services.CacheHistory
{
    /// <summary>
    /// Represents a service that stores cache history.
    /// </summary>
    public interface ICacheHistoryWriterService
    {
        /// <summary>
        /// Writes a history records.
        /// </summary>
        /// <param name="records">A list of history records.</param>
        void WriteRecords(IEnumerable<CacheHistoryRecord> records);
    }
}
