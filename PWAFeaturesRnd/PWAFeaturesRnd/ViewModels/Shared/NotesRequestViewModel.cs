namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// NotesRequestViewModel
    /// </summary>
    public class NotesRequestViewModel
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
        /// Gets or sets a value indicating whether this instance is page scroled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is page scroled; otherwise, <c>false</c>.
        /// </value>
        public bool IsPageScroled { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }
    }
}
