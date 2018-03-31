using System.Collections.Generic;

namespace NorfolkCacheWebApp.Models
{
    /// <summary>
    /// Represents a namespace model with full information.
    /// </summary>
    public class FullNamespaceModel : BriefNamespaceModel
    {
        /// <summary>
        /// Gets or sets a list of keys in the namespace.
        /// </summary>
        public IList<string> Keys { get; set; }
    }
}
