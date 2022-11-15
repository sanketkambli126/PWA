using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.VesselManagement
{
    /// <summary>
    /// The rightshiprequest view model
    /// </summary>
    public class RightShipRequestViewModel
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
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the month date.
        /// </summary>
        /// <value>
        /// The month date.
        /// </value>
        public DateTime? MonthDate { get; set; }
    }
}
