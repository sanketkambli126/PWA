using System;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Psc Deficiencies Request
    /// </summary>
    public class PscDeficienciesRequest
    {
        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>
        public string FleetId { get; set; }

        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public string MenuType { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselIds { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

    }
}
