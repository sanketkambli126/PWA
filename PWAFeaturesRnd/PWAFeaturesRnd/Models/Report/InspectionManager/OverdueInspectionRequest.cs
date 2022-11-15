namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Overdue Inspection Request
    /// </summary>
    public class OverdueInspectionRequest
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
        /// Gets or sets the vessel ids.
        /// </summary>
        /// <value>
        /// The vessel ids.
        /// </value>
        public string VesselIds { get; set; }
    }
}
