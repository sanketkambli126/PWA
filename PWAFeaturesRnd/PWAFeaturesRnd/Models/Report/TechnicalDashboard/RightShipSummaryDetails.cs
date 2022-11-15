using System;

namespace PWAFeaturesRnd.Models.Report.TechnicalDashboard
{
    public class RightShipSummaryDetails
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the vessel imo.
        /// </summary>
        /// <value>
        /// The vessel imo.
        /// </value>
        public string Imonumber { get; set; }

        /// <summary>
        /// Gets or sets the right ship date.
        /// </summary>
        /// <value>
        /// The right ship date.
        /// </value>
        public DateTime? RighShipMonth { get; set; }

        /// <summary>
        /// Gets or sets the right ship value.
        /// </summary>
        /// <value>
        /// The right ship value.
        /// </value>
        public double? RighShipValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the office.
        /// </summary>
        /// <value>
        /// The name of the office.
        /// </value>
        public string OfficeName { get; set; }

        /// <summary>
        /// Gets or sets the GHG rating.
        /// </summary>
        /// <value>
        /// The GHG rating.
        /// </value>
        public string Ghgrating { get; set; }
    }
}
