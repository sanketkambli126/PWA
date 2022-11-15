namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// ChannelRequest
    /// </summary>
    public class ChannelRequest
    {
        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the context payload.
        /// </summary>
        /// <value>
        /// The context payload.
        /// </value>
        public string ContextPayload { get; set; }

        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public string ReferenceIdentifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is search clicked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is search clicked; otherwise, <c>false</c>.
        /// </value>
        public bool IsSearchClicked { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is page scroled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is page scroled; otherwise, <c>false</c>.
        /// </value>
        public bool IsPageScroled { get; set; }

        /// <summary>
        /// Gets or sets the selected channel identifier.
        /// </summary>
        /// <value>
        /// The selected channel identifier.
        /// </value>
        public int SelectedChannelId { get; set; }
    }
}
