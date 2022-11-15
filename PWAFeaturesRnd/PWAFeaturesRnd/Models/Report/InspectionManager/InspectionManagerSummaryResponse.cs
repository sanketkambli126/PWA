namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Inspection Manager Summary Response
    /// </summary>
    public class InspectionManagerSummaryResponse
    {
        /// <summary>
        /// Gets or sets the PSC detention count.
        /// </summary>
        /// <value>
        /// The PSC detention count.
        /// </value>
        public int PSCDetentionCount { get; set; }

        /// <summary>
        /// Gets or sets the PSC detention priority.
        /// </summary>
        /// <value>
        /// The PSC detention priority.
        /// </value>
        public int PSCDetentionPriority { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per omv rate.
        /// </summary>
        /// <value>
        /// The deficiencies per omv rate.
        /// </value>
        public decimal? DeficienciesPerOMVRate { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per omv priority.
        /// </summary>
        /// <value>
        /// The deficiencies per omv priority.
        /// </value>
        public int DeficienciesPerOMVPriority { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per PSC rate.
        /// </summary>
        /// <value>
        /// The deficiencies per PSC rate.
        /// </value>
        public decimal DeficienciesPerPSCRate { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per PSC priority.
        /// </summary>
        /// <value>
        /// The deficiencies per PSC priority.
        /// </value>
        public int DeficienciesPerPSCPriority { get; set; }

        /// <summary>
        /// Gets or sets the overdue findings count.
        /// </summary>
        /// <value>
        /// The overdue findings count.
        /// </value>
        public int OverdueFindingsCount { get; set; }

        /// <summary>
        /// Gets or sets the overdue findings priority.
        /// </summary>
        /// <value>
        /// The overdue findings priority.
        /// </value>
        public int OverdueFindingsPriority { get; set; }

        /// <summary>
        /// Gets or sets the overdue inspection count.
        /// </summary>
        /// <value>
        /// The overdue inspection count.
        /// </value>
        public int OverdueInspectionCount { get; set; }

        /// <summary>
        /// Gets or sets the overdue inspection priority.
        /// </summary>
        /// <value>
        /// The overdue inspection priority.
        /// </value>
        public int OverdueInspectionPriority { get; set; }

        /// <summary>
        /// Gets or sets the PSC deficiency count.
        /// </summary>
        /// <value>
        /// The PSC deficiency count.
        /// </value>
        public int PscDeficiencyCount { get; set; }

        /// <summary>
        /// Gets or sets the PSC deficiency priority.
        /// </summary>
        /// <value>
        /// The PSC deficiency priority.
        /// </value>
        public int PscDeficiencyPriority { get; set; }

        /// <summary>
        /// Gets or sets the omv rejection count.
        /// </summary>
        /// <value>
        /// The omv rejection count.
        /// </value>
        public int? OmvRejectionCount { get; set; }

        /// <summary>
        /// Gets or sets the omv rejection priority.
        /// </summary>
        /// <value>
        /// The omv rejection priority.
        /// </value>
        public int OmvRejectionPriority { get; set; }
    }
}
