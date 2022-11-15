namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// Defect Report Work Order Rank
    /// </summary>
    public class DefectReportWorkOrderRank
    {
        /// <summary>
        /// Gets or sets the DRR identifier.
        /// </summary>
        /// <value>
        /// The DRR identifier.
        /// </value>
        public string DrrId { get; set; }

        /// <summary>
        /// Gets or sets the DRW identifier.
        /// </summary>
        /// <value>
        /// The DRW identifier.
        /// </value>
        public string DrwId { get; set; }

        /// <summary>
        /// Gets or sets the RNK identifier.
        /// </summary>
        /// <value>
        /// The RNK identifier.
        /// </value>
        public string RnkId { get; set; }

        /// <summary>
        /// Gets or sets the ship staff rank.
        /// </summary>
        /// <value>
        /// The ship staff rank.
        /// </value>
        public string ShipStaffRank { get; set; }

        /// <summary>
        /// Gets or sets the shore staff rank.
        /// </summary>
        /// <value>
        /// The shore staff rank.
        /// </value>
        public string ShoreStaffRank { get; set; }

        /// <summary>
        /// Gets or sets the hours.
        /// </summary>
        /// <value>
        /// The hours.
        /// </value>
        public float? Hours { get; set; }
    }
}
