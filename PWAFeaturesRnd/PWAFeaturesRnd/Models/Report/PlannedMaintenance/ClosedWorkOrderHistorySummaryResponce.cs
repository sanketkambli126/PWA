namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
    /// <summary>
    /// The ClosedWorkOrderHistorySummaryResponce
    /// </summary>
    public class ClosedWorkOrderHistorySummaryResponce
    {
        /// <summary>
        /// Gets or sets the overhaul count.
        /// </summary>
        /// <value>
        /// The overhaul count.
        /// </value>
        public int OverhaulCount { get; set; }

        /// <summary>
        /// Gets or sets the rescheduled count.
        /// </summary>
        /// <value>
        /// The rescheduled count.
        /// </value>
        public int RescheduledCount { get; set; }
    }
}
