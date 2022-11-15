namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Inspection Dashboard Detail
    /// </summary>
    public class InspectionDashboardDetail
    {
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
        /// Gets or sets the total in period count.
        /// </summary>
        /// <value>
        /// The total in period count.
        /// </value>
        public int TotalInPeriodCount { get; set; }

        /// <summary>
        /// Gets or sets the inspection due count.
        /// </summary>
        /// <value>
        /// The inspection due count.
        /// </value>
        public int InspectionDueCount { get; set; }

        /// <summary>
        /// Gets or sets the inspection overdue count.
        /// </summary>
        /// <value>
        /// The inspection overdue count.
        /// </value>
        public int InspectionOverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the inspection never done count.
        /// </summary>
        /// <value>
        /// The inspection never done count.
        /// </value>
        public int InspectionNeverDoneCount { get; set; }
    }
}
