namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// User Search Response
    /// </summary>
    public class UserSearchResponse
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { set; get; }

        /// <summary>
        /// Gets or sets the name of the fore.
        /// </summary>
        /// <value>
        /// The name of the fore.
        /// </value>
        public string ForeName { set; get; }

        /// <summary>
        /// Gets or sets the name of the sur.
        /// </summary>
        /// <value>
        /// The name of the sur.
        /// </value>
        public string SurName { set; get; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        /// <value>
        /// The name of the role.
        /// </value>
        public string RoleName { set; get; }
    }
}
