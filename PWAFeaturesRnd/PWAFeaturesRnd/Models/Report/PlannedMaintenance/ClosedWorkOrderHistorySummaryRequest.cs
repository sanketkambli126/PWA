using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
    /// <summary>
    /// The ClosedWorkOrderHistorySummaryRequest
    /// </summary>
    public class ClosedWorkOrderHistorySummaryRequest
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
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }
    }
}
