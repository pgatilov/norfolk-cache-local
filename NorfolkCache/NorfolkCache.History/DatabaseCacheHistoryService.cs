using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NorfolkCache.Services.CacheHistory;

namespace NorfolkCache.History
{
    /// <summary>
    /// Represents a service that stores cache shistory.
    /// </summary>
    public class DatabaseCacheHistoryService : ICacheHistoryWriterService, ICacheHistoryService
    {
        private readonly HistoryDatabaseContext _historyDatabaseContex = new HistoryDatabaseContext();

        public IList<CacheHistoryRecord> GetRecords(out int totalNumber, int start = 0, int end = 0)
        {
            totalNumber = _historyDatabaseContex.CacheHistory.Count();

            return _historyDatabaseContex.CacheHistory.AsEnumerable().Select(r => new CacheHistoryRecord
            {
                Action = r.Action,
                Namespace = r.Namespace,
                Key = r.Key,
                Value = r.Value
            }).ToArray();
        }

        /// <summary>
        /// Writes a history records.
        /// </summary>
        /// <param name="records">A list of history records.</param>
        public void WriteRecords(IEnumerable<CacheHistoryRecord> records)
        {
            if (records == null)
            {
                throw new ArgumentNullException(nameof(records));
            }

            foreach (var record in records)
            {
                Trace.TraceInformation("Writing a history record: namespace \"{0}\", key \"{1}\", value \"{2}\".", record.Namespace, record.Key, record.Value);

                _historyDatabaseContex.CacheHistory.Add(new DatabaseCacheHistoryRecord
                {
                    Action = record.Action,
                    ActionDate = DateTime.Now,
                    Namespace = record.Namespace,
                    Key = record.Key,
                    Value = record.Value
                });
            }

            _historyDatabaseContex.SaveChanges();
        }
    }
}
