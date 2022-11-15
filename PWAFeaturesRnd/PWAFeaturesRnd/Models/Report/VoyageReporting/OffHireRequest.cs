using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// OffHireRequest
    /// </summary>
    public class OffHireRequest
    {
        /// <summary>
        /// Gets or sets the off hire start date.
        /// </summary>
        /// <value>
        /// The off hire start date.
        /// </value>
        public DateTime OffHireStartDate { get; set; }

        /// <summary>
        /// Gets or sets the off hire end date.
        /// </summary>
        /// <value>
        /// The off hire end date.
        /// </value>
        public DateTime OffHireEndDate { get; set; }

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
    }
}
