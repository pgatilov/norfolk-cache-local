using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NorfolkCache.Services.CacheHistory;

namespace NorfolkCache.History
{
    /// <summary>
    /// Represents a cache history record.
    /// </summary>
    public class DatabaseCacheHistoryRecord
    {
        [Key]
        [Column("record_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("action_type", TypeName = "int")]
        public CacheHistoryAction Action { get; set; }

        [Column("action_date")]
        public DateTime ActionDate { get; set; }

        [Column("namespace")]
        public string Namespace { get; set; }

        [Column("key")]
        public string Key { get; set; }

        [Column("value")]
        public string Value { get; set; }
    }
}
