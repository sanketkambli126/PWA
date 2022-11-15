namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// The  maintenance history summary view model
    /// </summary>
    public class ClosedWorkOrderHistorySummaryResponceViewModel
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

        /// <summary>
        /// Gets or sets the overhaul URL.
        /// </summary>
        /// <value>
        /// The overhaul URL.
        /// </value>
        public string OverhaulURL { get; set; }

        /// <summary>
        /// Gets or sets the rescheduled URL.
        /// </summary>
        /// <value>
        /// The rescheduled URL.
        /// </value>
        public string RescheduledURL { get; set; }

    }
}
