namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Sentinel Dashboard Detail Request ViewModel
    /// </summary>
    public class SentinelDashboardDetailRequestViewModel
    {
        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public string MenuType { get; set; }

        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>
        public string FleetId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the model dimension identifier.
        /// </summary>
        /// <value>
        /// The model dimension identifier.
        /// </value>
        public string ModelDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the color of the value.
        /// </summary>
        /// <value>
        /// The color of the value.
        /// </value>
        public string ValueColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [get category graph details].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [get category graph details]; otherwise, <c>false</c>.
        /// </value>
        public bool GetCategoryGraphDetails { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

    }
}
