using System.Data.Entity;

namespace NorfolkCache.History
{
    /// <summary>
    /// Represents a database context for history database.
    /// </summary>
    public class HistoryDatabaseContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryDatabaseContext"/> class.
        /// </summary>
        public HistoryDatabaseContext()
            : base("HistoryDbConnection")
        {
        }

        /// <summary>
        /// Gets or sets a cache history <see cref="DbSet"/>.
        /// </summary>
        public DbSet<DatabaseCacheHistoryRecord> CacheHistory { get; set; }
    }
}
