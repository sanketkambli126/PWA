using System;

namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// request object for defect summary
    /// </summary>
    public class DefectSummaryRequest
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the closed date.
        /// </summary>
        /// <value>
        /// The closed date.
        /// </value>
        public DateTime ClosedDate { get; set; }

        /// <summary>
        /// Gets or sets the off hire priority limit.
        /// </summary>
        /// <value>
        /// The off hire priority limit.
        /// </value>
        public int OffHirePriorityLimit { get; set; }

        /// <summary>
        /// Gets or sets the overdue priority limit.
        /// </summary>
        /// <value>
        /// The overdue priority limit.
        /// </value>
        public int OverduePriorityLimit { get; set; }

        /// <summary>
        /// Gets or sets the awaiting spares priority limit.
        /// </summary>
        /// <value>
        /// The awaiting spares priority limit.
        /// </value>
        public int AwaitingSparesPriorityLimit { get; set; }
    }
}
