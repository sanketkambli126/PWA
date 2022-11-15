namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// User Search Request
    /// </summary>
    public class UserSearchRequest
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { set; get; }

        /// <summary>
        /// Gets or sets the user name search.
        /// </summary>
        /// <value>
        /// The user name search.
        /// </value>
        public string UserNameSearch { set; get; }
    }
}
