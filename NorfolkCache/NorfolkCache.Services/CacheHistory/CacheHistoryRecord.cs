namespace NorfolkCache.Services.CacheHistory
{
    /// <summary>
    /// Represents a cache history record.
    /// </summary>
    public class CacheHistoryRecord
    {
        public CacheHistoryAction Action { get; set; }

        public string Namespace { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
