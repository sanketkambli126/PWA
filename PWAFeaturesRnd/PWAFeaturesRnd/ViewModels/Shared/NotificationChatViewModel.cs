namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationChatViewModel
    {
        /// <summary>
        /// Gets or sets the URL parameter.
        /// </summary>
        /// <value>
        /// The URL parameter.
        /// </value>
        public string UrlParameter { get; set; }

        /// <summary>
        /// Gets or sets the sesseion storage details.
        /// </summary>
        /// <value>
        /// The sesseion storage details.
        /// </value>
        public string SessionStorageDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filter change.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filter change; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilterChange { get; set; }
    }
}
