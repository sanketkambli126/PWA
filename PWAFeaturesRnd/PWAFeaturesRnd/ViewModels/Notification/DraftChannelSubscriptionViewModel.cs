namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// The draft channel subscription view model
    /// </summary>
    public class DraftChannelSubscriptionViewModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the ss user identifier.
        /// </summary>
        /// <value>
        /// The ss user identifier.
        /// </value>
        public string SSUserId { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the short name of the user.
        /// </summary>
        /// <value>
        /// The short name of the user.
        /// </value>
        public string UserShortName { get; set; }

        /// <summary>
        /// Gets or sets the user role description.
        /// </summary>
        /// <value>
        /// The user role description.
        /// </value>
        public string UserRoleDescription { get; set; }
    }
}
