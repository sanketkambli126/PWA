namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// Notification Mobile ChatDetailViewModel
    /// </summary>
    public class NotificationMobileChatDetailViewModel
    {
        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the vessel imo number.
        /// </summary>
        /// <value>
        /// The vessel imo number.
        /// </value>
        public string VesselIMONumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is one to one chat.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is one to one chat; otherwise, <c>false</c>.
        /// </value>
        public bool IsOneToOneChat { get; set; }

        /// <summary>
        /// Gets or sets the notification mobile information URL.
        /// </summary>
        /// <value>
        /// The notification mobile information URL.
        /// </value>
        public string NotificationMobileInfoURL { get; set; }

        /// <summary>
        /// Gets or sets the notification mobile participant URL.
        /// </summary>
        /// <value>
        /// The notification mobile participant URL.
        /// </value>
        public string NotificationMobileParticipantURL { get; set; }

        /// <summary>
        /// Gets or sets the participant count.
        /// </summary>
        /// <value>
        /// The participant count.
        /// </value>
        public int ParticipantCount { get; set; }

        /// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the active participants count.
        /// </summary>
        /// <value>
        /// The active participants count.
        /// </value>
        public int ActiveParticipantsCount { get; set; }

        /// <summary>
        /// Gets or sets the session storage detail.
        /// </summary>
        /// <value>
        /// The session storage detail.
        /// </value>
        public string SessionStorageDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is general cat.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is general cat; otherwise, <c>false</c>.
        /// </value>
        public bool IsGeneralCat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is from other source.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is from other source; otherwise, <c>false</c>.
        /// </value>
        public bool IsFromOtherSource { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filter change.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filter change; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilterChange { get; set; }

    }
}
