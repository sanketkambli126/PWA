using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Vessel
{
    /// <summary>
    /// The right ship request
    /// </summary>
    public class RightShipRequest
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
        public List<string> VesselIds { get; set; }
    }
}
