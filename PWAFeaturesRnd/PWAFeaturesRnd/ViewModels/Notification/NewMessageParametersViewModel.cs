namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// New Message Parameters View Model
    /// </summary>
    public class NewMessageParametersViewModel
	{
        /// <summary>
        /// Gets or sets the context payload.
        /// </summary>
        /// <value>
        /// The context payload.
        /// </value>
        public string ContextPayload { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the message template.
        /// </summary>
        /// <value>
        /// The message template.
        /// </value>
        public string DefaultMessage { get; set; }

        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int ChannelId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is general.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is general; otherwise, <c>false</c>.
        /// </value>
        public bool IsGeneralCat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is save as draft.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is save as draft; otherwise, <c>false</c>.
        /// </value>
        public bool IsSaveAsDraft { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public string ReferenceIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the notification JWT token.
        /// </summary>
        /// <value>
        /// The notification JWT token.
        /// </value>
        public string NotificationJwtToken { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user email id.
        /// </summary>
        /// <value>
        /// The user email id.
        /// </value>
        public string UserEmailId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is standalone create channel.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is standalone create channel; otherwise, <c>false</c>.
        /// </value>
        public bool IsStandaloneCreateChannel { get; set; }

        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public string EncryptedNoteId { get; set; }
    }
}
