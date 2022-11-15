namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Vessel mangament detail for user fleet
    /// </summary>
    public class VesselManagementDetailForUserFleet
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
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }
        /// <summary>
        /// Gets or sets the vessel management identifier.
        /// </summary>
        /// <value>
        /// The vessel management identifier.
        /// </value>
        public string VesselManagementId { get; set; }
        /// <summary>
        /// Gets or sets the purchasing office identifier.
        /// </summary>
        /// <value>
        /// The purchasing office identifier.
        /// </value>
        public string PurchasingOfficeId { get; set; }
        /// <summary>
        /// Gets or sets the name of the purchasing office.
        /// </summary>
        /// <value>
        /// The name of the purchasing office.
        /// </value>
        public string PurchasingOfficeName { get; set; }
        /// <summary>
        /// Gets or sets the technical office identifier.
        /// </summary>
        /// <value>
        /// The technical office identifier.
        /// </value>
        public string TechnicalOfficeId { get; set; }
        /// <summary>
        /// Gets or sets the name of the technical office.
        /// </summary>
        /// <value>
        /// The name of the technical office.
        /// </value>
        public string TechnicalOfficeName { get; set; }
    }
}
