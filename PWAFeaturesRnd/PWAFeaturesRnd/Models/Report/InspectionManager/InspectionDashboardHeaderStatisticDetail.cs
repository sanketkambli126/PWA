namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// InspectionDashboardHeaderStatisticDetail
    /// </summary>
    public class InspectionDashboardHeaderStatisticDetail
    {
        #region Statistics

        /// <summary>
        /// Gets or sets the total omv inspection count.
        /// </summary>
        /// <value>
        /// The total omv inspection count.
        /// </value>
        public int TotalOmvInspectionCount { get; set; }

        /// <summary>
        /// Gets or sets the total PSC inspection count.
        /// </summary>
        /// <value>
        /// The total PSC inspection count.
        /// </value>
        public int TotalPscInspectionCount { get; set; }

        /// <summary>
        /// Gets or sets the omv defect rate.
        /// </summary>
        /// <value>
        /// The omv defect rate.
        /// </value>
        public decimal? OMVDefectRate { get; set; }

        /// <summary>
        /// Gets or sets the omv flawless rate.
        /// </summary>
        /// <value>
        /// The omv flawless rate.
        /// </value>
        public decimal OMVFlawlessRate { get; set; }

        /// <summary>
        /// Gets or sets the PSC defect rate.
        /// </summary>
        /// <value>
        /// The PSC defect rate.
        /// </value>
        public decimal PSCDefectRate { get; set; }

        /// <summary>
        /// Gets or sets the PSC flawless rate.
        /// </summary>
        /// <value>
        /// The PSC flawless rate.
        /// </value>
        public decimal PSCFlawlessRate { get; set; }

        /// <summary>
        /// Gets or sets the PSC detaintion count.
        /// </summary>
        /// <value>
        /// The PSC detaintion count.
        /// </value>
        public int PSCDetaintionCount { get; set; }

        /// <summary>
        /// Gets or sets the omv inspection average risk.
        /// </summary>
        /// <value>
        /// The omv inspection average risk.
        /// </value>
        public decimal OMVInspectionAverageRisk { get; set; }

        /// <summary>
        /// Gets or sets the total omv finding count.
        /// </summary>
        /// <value>
        /// The total omv finding count.
        /// </value>
        public int TotalOMVFindingCount { get; set; }

        /// <summary>
        /// Gets or sets the total PSC finding count.
        /// </summary>
        /// <value>
        /// The total PSC finding count.
        /// </value>
        public int TotalPSCFindingCount { get; set; }

        /// <summary>
        /// Gets or sets the total omv risk rating.
        /// </summary>
        /// <value>
        /// The total omv risk rating.
        /// </value>
        public int TotalOMVRiskRating { get; set; }

        #endregion

        #region Planning Details

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

        #endregion

        #region Pending Closure Detail

        /// <summary>
        /// Gets or sets the total inspection count.
        /// </summary>
        /// <value>
        /// The total inspection count.
        /// </value>
        public int TotalInspectionCount { get; set; }

        /// <summary>
        /// Gets or sets the total finding count.
        /// </summary>
        /// <value>
        /// The total finding count.
        /// </value>
        public int TotalFindingCount { get; set; }

        /// <summary>
        /// Gets or sets the inspection finding outstanding count.
        /// </summary>
        /// <value>
        /// The inspection finding outstanding count.
        /// </value>
        public int InspectionFindingOutstandingCount { get; set; }

        /// <summary>
        /// Gets or sets the inspection finding overdue count.
        /// </summary>
        /// <value>
        /// The inspection finding overdue count.
        /// </value>
        public int InspectionFindingOverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the pending closure count.
        /// </summary>
        /// <value>
        /// The pending closure count.
        /// </value>
        public int PendingClosureCount { get; set; }

        /// <summary>
        /// Gets or sets the inspection audit finding outstanding count.
        /// </summary>
        /// <value>
        /// The inspection audit finding outstanding count.
        /// </value>
        public int InspectionAuditFindingOutstandingCount { get; set; }

        /// <summary>
        /// Gets or sets the inspection audit finding overdue count.
        /// </summary>
        /// <value>
        /// The inspection audit finding overdue count.
        /// </value>
        public int InspectionAuditFindingOverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the audit pending closure count.
        /// </summary>
        /// <value>
        /// The audit pending closure count.
        /// </value>
        public int AuditPendingClosureCount { get; set; }

        /// <summary>
        /// Gets or sets the total inspection count without audit.
        /// </summary>
        /// <value>
        /// The total inspection count without audit.
        /// </value>
        public int TotalInspectionCountWithoutAudit { get; set; }

        /// <summary>
        /// Gets or sets the total inspection audit count.
        /// </summary>
        /// <value>
        /// The total inspection audit count.
        /// </value>
        public int TotalInspectionAuditCount { get; set; }

        /// <summary>
        /// Gets or sets the total outstanding finding count.
        /// </summary>
        /// <value>
        /// The total outstanding finding count.
        /// </value>
        public int TotalOutstandingFindingCount { get; set; }

        /// <summary>
        /// Gets or sets the total overdue finding count.
        /// </summary>
        /// <value>
        /// The total overdue finding count.
        /// </value>
        public int TotalOverdueFindingCount { get; set; }

        #endregion

        #region Dashboard Priority
        /// <summary>
        /// Gets or sets the PSC detention priority.
        /// </summary>
        /// <value>
        /// The PSC detention priority.
        /// </value>
        public int PSCDetentionPriority { get; set; }

        /// <summary>
		/// Gets or sets the deficiencies per omv priority.
		/// </summary>
		/// <value>
		/// The deficiencies per omv priority.
		/// </value>
		public int DeficienciesPerOMVPriority { get; set; }

        /// <summary>
		/// Gets or sets the deficiencies per PSC priority.
		/// </summary>
		/// <value>
		/// The deficiencies per PSC priority.
		/// </value>
		public int DeficienciesPerPSCPriority { get; set; }

        /// <summary>
		/// Gets or sets the overdue findings priority.
		/// </summary>
		/// <value>
		/// The overdue findings priority.
		/// </value>
		public int OverdueFindingsPriority { get; set; }

        /// <summary>
		/// Gets or sets the overdue inspection priority.
		/// </summary>
		/// <value>
		/// The overdue inspection priority.
		/// </value>
		public int OverdueInspectionPriority { get; set; }

        /// <summary>
		/// Gets or sets the PSC deficiency priority.
		/// </summary>
		/// <value>
		/// The PSC deficiency priority.
		/// </value>
		public int PscDeficiencyPriority { get; set; }

        /// <summary>
        /// Gets or sets the omv rejection priority.
        /// </summary>
        /// <value>
        /// The omv rejection priority.
        /// </value>
        public int OmvRejectionPriority { get; set; }

        /// <summary>
        /// Gets or sets the omv rejection count.
        /// </summary>
        /// <value>
        /// The omv rejection count.
        /// </value>
        public int? OmvRejectionCount { get; set; }
        #endregion


        /// <summary>
        /// Gets or sets the open PSC inspection count.
        /// </summary>
        /// <value>
        /// The open PSC inspection count.
        /// </value>
        public int? OpenPSCInspectionCount { get; set; }

        /// <summary>
        /// Gets or sets the open omv inspection count.
        /// </summary>
        /// <value>
        /// The open omv inspection count.
        /// </value>
        public int? OpenOMVInspectionCount { get; set; }
    }
}
