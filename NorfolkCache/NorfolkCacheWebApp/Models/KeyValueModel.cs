using System.Collections.Generic;

namespace NorfolkCacheWebApp.Models
{
    /// <summary>
    /// Represents a model for a key-value pair in a namespace.
    /// </summary>
    public class KeyValueModel
    {
        /// <summary>
        /// Gets or sets a namespace.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets a key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets a collection of values.
        /// </summary>
        public IEnumerable<string> Values { get; set; }
    }
}
