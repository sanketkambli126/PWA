namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// The UserFleetsRequestViewModel
    /// </summary>
    public class UserFleetsRequestViewModel
    {
        /// <summary>
        /// Gets or sets the name of the fleet.
        /// </summary>
        /// <value>
        /// The name of the fleet.
        /// </value>
        public string FleetName { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// The is active.
        /// </value>
        public bool? IsActive { get; set; }
    }
}
