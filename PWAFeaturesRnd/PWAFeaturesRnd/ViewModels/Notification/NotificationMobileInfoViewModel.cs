namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// NotificationMobileInfoViewModel
    /// </summary>
    public class NotificationMobileInfoViewModel
    {
        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the ves imo number.
        /// </summary>
        /// <value>
        /// The ves imo number.
        /// </value>
        public string VesIMONumber { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is general cat.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is general cat; otherwise, <c>false</c>.
        /// </value>
        public bool IsGeneralCat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is information page loaded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is information page loaded; otherwise, <c>false</c>.
        /// </value>
        public bool IsInfoPageLoaded { get; set; }

        public string SessionStorageDetails { get; set; }
    }
}
