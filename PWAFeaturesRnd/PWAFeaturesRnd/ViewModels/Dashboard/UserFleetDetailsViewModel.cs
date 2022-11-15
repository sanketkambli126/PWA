namespace PWAFeaturesRnd.ViewModels.Dashboard
{

    /// <summary>
    /// User fleet view model
    /// </summary>
    public class UserFleetDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>
        public int FleetId { get; set; }

        /// <summary>
        /// Gets or sets the name of the fleet.
        /// </summary>
        /// <value>
        /// The name of the fleet.
        /// </value>
        public string FleetName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is fleet active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fleet active; otherwise, <c>false</c>.
        /// </value>
        public bool IsFleetActive { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// The is active.
        /// </value>
        public string IsActive { get; set; }
    }
}
