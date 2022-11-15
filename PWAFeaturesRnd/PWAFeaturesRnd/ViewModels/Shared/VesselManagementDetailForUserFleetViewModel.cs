namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// The VesselManagementDetailForUserFleetViewModel
    /// </summary>
    public class VesselManagementDetailForUserFleetViewModel
    {
        /// <summary>
        /// Gets or sets the fleet vessel identifier.
        /// </summary>
        /// <value>
        /// The fleet vessel identifier.
        /// </value>
        public string FleetVesselId { get; set; }

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
        /// Gets or sets a value indicating whether this instance is vessel mapped.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vessel mapped; otherwise, <c>false</c>.
        /// </value>
        public bool isVesselMapped { get; set; }

        /// <summary>
        /// Gets or sets the vessel management identifier.
        /// </summary>
        /// <value>
        /// The vessel management identifier.
        /// </value>
        public string VesselManagementId { get; set; }

        /// <summary>
        /// Gets or sets the row identifier.
        /// </summary>
        /// <value>
        /// The row identifier.
        /// </value>
        public string RowId { get; set; }
    }
}
