namespace NorfolkCacheWebApp.Models
{
    /// <summary>
    /// Represents a namespace model with brief information.
    /// </summary>
    public class BriefNamespaceModel
    {
        /// <summary>
        /// Gets or sets a namespace name.
        /// </summary>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets a number of keys in the namespace.
        /// </summary>
        public int? KeyCount { get; set; }
    }
}
