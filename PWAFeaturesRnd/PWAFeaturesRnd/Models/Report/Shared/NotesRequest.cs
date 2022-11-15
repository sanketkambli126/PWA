namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class NotesRequest
    {
        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string StatusIds { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the context parameters.
        /// </summary>
        /// <value>
        /// The context parameters.
        /// </value>
        public string ContextParams { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public string ReferenceIdentifier { get; set; }
    }
}
