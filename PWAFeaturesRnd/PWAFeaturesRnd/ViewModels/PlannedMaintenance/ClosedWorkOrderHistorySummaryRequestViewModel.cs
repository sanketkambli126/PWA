using System;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// The ClosedWorkOrderHistorySummaryRequestViewModel
    /// </summary>
    public class ClosedWorkOrderHistorySummaryRequestViewModel
    {

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the grid sub title.
        /// </summary>
        /// <value>
        /// The grid sub title.
        /// </value>
        public string GridSubTitle { get; set; }
    }
}
