namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationSearchRequest
    {
        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is search clicked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is search clicked; otherwise, <c>false</c>.
        /// </value>
        public bool IsSearchClicked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [open create new channel].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [open create new channel]; otherwise, <c>false</c>.
        /// </value>
		public bool OpenCreateNewChannel { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }

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
        /// Gets or sets the navigation URL.
        /// </summary>
        /// <value>
        /// The navigation URL.
        /// </value>
        public string NavigationURL { get; set; }
    }
}
