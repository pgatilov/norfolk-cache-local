using System.Collections.Generic;

namespace NorfolkCacheWebApp.Models
{
    /// <summary>
    /// Represents a model that contains information for all namespace keys.
    /// </summary>
    public class NamespaceKeysModel : BriefNamespaceModel
    {
        /// <summary>
        /// Gets or sets a list of keys in the namespace.
        /// </summary>
        public IEnumerable<string> Keys { get; set; }
    }
}