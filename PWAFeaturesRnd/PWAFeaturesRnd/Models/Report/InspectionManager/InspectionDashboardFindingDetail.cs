namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Inspection Dashboard Finding Detail
    /// </summary>
    public class InspectionDashboardFindingDetail
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

        /// <summary>
        /// Gets or sets the total finding count.
        /// </summary>
        /// <value>
        /// The total finding count.
        /// </value>
        public int TotalFindingCount { get; set; }
    }
}
