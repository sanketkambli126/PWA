namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Work Order Rank ViewModel
    /// </summary>
    public class WorkOrderRankViewModel
    {
        /// <summary>
        /// Gets or sets the rank short code.
        /// </summary>
        /// <value>
        /// The rank short code.
        /// </value>
        public string RankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the rank description.
        /// </summary>
        /// <value>
        /// The rank description.
        /// </value>
        public string RankDescription { get; set; }

        /// <summary>
        /// Gets or sets the man hours.
        /// </summary>
        /// <value>
        /// The man hours.
        /// </value>
        public float? ManHours { get; set; }

        /// <summary>
        /// Gets or sets the total man hours.
        /// </summary>
        /// <value>
        /// The total man hours.
        /// </value>
        public float? TotalManHours { get; set; }
    }
}
