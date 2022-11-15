namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// Notification Mobile Discussion ViewModel
    /// </summary>
    public class NotificationMobileDiscussionViewModel
    {
        /// <summary>
        /// Gets or sets the reply private participant.
        /// </summary>
        /// <value>
        /// The reply private participant.
        /// </value>
        public ParticipantsDetailsViewModel replyPrivateParticipant { get; set; }

        /// <summary>
        /// Creates new messagedetails.
        /// </summary>
        /// <value>
        /// The new message details.
        /// </value>
        public NewMessageParametersViewModel NewMessageDetails { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string SelectedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string SelectedVesselName { get; set; }

        /// <summary>
        /// Session Storage Details
        /// </summary>
        public string SessionStorageDetails { get; set; }

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
