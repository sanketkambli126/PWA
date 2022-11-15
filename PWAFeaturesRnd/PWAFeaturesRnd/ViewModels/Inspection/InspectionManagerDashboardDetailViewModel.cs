namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// Inspection Manager Dashboard Detail ViewModel
    /// </summary>
    public class InspectionManagerDashboardDetailViewModel
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
        public string OmvDefectRate { get; set; }

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
        public string PscDefectRate { get; set; }

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
        public int PscDetaintionCount { get; set; }

        /// <summary>
        /// Gets or sets the omv inspection average risk.
        /// </summary>
        /// <value>
        /// The omv inspection average risk.
        /// </value>
        public decimal OmvInspectionAverageRisk { get; set; }

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

        #region URL

        /// <summary>
		/// Gets or sets the detention type URL.
		/// </summary>
		/// <value>
		/// The detention type URL.
		/// </value>
		public string DetentionTypeURL { get; set; }

        /// <summary>
        /// Gets or sets the inspection omv type URL.
        /// </summary>
        /// <value>
        /// The inspection omv type URL.
        /// </value>
        public string InspectionOMVTypeURL { get; set; }

        /// <summary>
        /// Gets or sets the inspection due type URL.
        /// </summary>
        /// <value>
        /// The inspection due type URL.
        /// </value>
        public string InspectionDueTypeURL { get; set; }

        /// <summary>
        /// Gets or sets the inspection overdue type URL.
        /// </summary>
        /// <value>
        /// The inspection overdue type URL.
        /// </value>
        public string InspectionOverdueTypeURL { get; set; }

        /// <summary>
        /// Gets or sets the inspection finding outstanding type URL.
        /// </summary>
        /// <value>
        /// The inspection finding outstanding type URL.
        /// </value>
        public string InspectionFindingOutstandingTypeURL { get; set; }

        /// <summary>
        /// Gets or sets the inspection finding overdue type URL.
        /// </summary>
        /// <value>
        /// The inspection finding overdue type URL.
        /// </value>
        public string InspectionFindingOverdueTypeURL { get; set; }

        /// <summary>
        /// Gets or sets the pending closure by office type URL.
        /// </summary>
        /// <value>
        /// The pending closure by office type URL.
        /// </value>
        public string PendingClosureByOfficeTypeURL { get; set; }

        /// <summary>
        /// Gets or sets the audit inspection finding outstanding type URL.
        /// </summary>
        /// <value>
        /// The audit inspection finding outstanding type URL.
        /// </value>
        public string AuditInspectionFindingOutstandingTypeURL { get; set; }

        /// <summary>
        /// Gets or sets the audit inspection finding overdue type URL.
        /// </summary>
        /// <value>
        /// The audit inspection finding overdue type URL.
        /// </value>
        public string AuditInspectionFindingOverdueTypeURL { get; set; }

        /// <summary>
        /// Gets or sets the audit pending closure by office type URL.
        /// </summary>
        /// <value>
        /// The audit pending closure by office type URL.
        /// </value>
        public string AuditPendingClosureByOfficeTypeURL { get; set; }

        /// <summary>
        /// Gets or sets all inspection URL.
        /// </summary>
        /// <value>
        /// All inspection URL.
        /// </value>
        public string AllInspectionURL { get; set; }

        /// <summary>
        /// Gets or sets the findings open URL.
        /// </summary>
        /// <value>
        /// The findings open URL.
        /// </value>
        public string FindingsOutstandingUrl { get; set; }

        /// <summary>
        /// Gets or sets the findings overdue URL.
        /// </summary>
        /// <value>
        /// The findings overdue URL.
        /// </value>
        public string FindingsOverdueUrl { get; set; }

        /// <summary>
        /// Gets or sets the inspection type omv URL.
        /// </summary>
        /// <value>
        /// The inspection type omv URL.
        /// </value>
        public string InspectionTypeOmvURL { get; set; }

        /// <summary>
        /// Gets or sets the inspection type PSC URL.
        /// </summary>
        /// <value>
        /// The inspection type PSC URL.
        /// </value>
        public string InspectionTypePscURL { get; set; }

        #endregion

        #region Dashboard
        /// <summary>
        /// Gets or sets the PSC Detention priority.
        /// </summary>
        /// <value>
        /// The PSC Detention priority.
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

        /// <summary>Gets or sets the vessel identifier.</summary>
        /// <value>The vessel identifier.</value>
        public string VesselId { get; set; }

        /// <summary>Gets or sets the name of the vessel.</summary>
        /// <value>The name of the vessel.</value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the omv rej count.
        /// </summary>
        /// <value>
        /// The omv rej count.
        /// </value>
        public string OMVRejCount { get; set; }

        /// <summary>
        /// Gets or sets the omv rej priority.
        /// </summary>
        /// <value>
        /// The omv rej priority.
        /// </value>
        public int OMVRejPriority { get; set; }

        /// <summary>
        /// Gets or sets the PSC deficen priority.
        /// </summary>
        /// <value>
        /// The PSC deficen priority.
        /// </value>
        public int PSCDeficenPriority { get; set; }

        /// <summary>
        /// Gets or sets the PSC deficiency URL.
        /// </summary>
        /// <value>
        /// The PSC deficiency URL.
        /// </value>
        public string PSCDeficiencyUrl { get; set; }

        /// <summary>
        /// Gets or sets the omv rejection URL.
        /// </summary>
        /// <value>
        /// The omv rejection URL.
        /// </value>
        public string OMVRejectionUrl { get; set; }

        /// <summary>
        /// Gets or sets the overview inspection URL.
        /// </summary>
        /// <value>
        /// The overview inspection URL.
        /// </value>
        public string OverviewInspectionUrl { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per omv URL.
        /// </summary>
        /// <value>
        /// The deficiencies per omv URL.
        /// </value>
        public string DeficienciesPerOmvURL { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per PSC URL.
        /// </summary>
        /// <value>
        /// The deficiencies per PSC URL.
        /// </value>
        public string DeficienciesPerPscURL { get; set; }

        /// <summary>
        /// Gets or sets the overdue findings url.
        /// </summary>
        /// <value>
        /// The overdue findings url.
        /// </value>
        public string OverdueFindingsUrl { get; set; }

        /// <summary>
        /// Gets or sets the overdue inspection url.
        /// </summary>
        /// <value>
        /// The overdue inspection url.
        /// </value>
        public string OverdueInspectionUrl { get; set; }

        /// <summary>
        /// Gets or sets the psc detaintion url.
        /// </summary>
        /// <value>
        /// The overdue inspection url.
        /// </value>
        public string PSCDetentionUrl { get; set; }

        /// <summary>
        /// Gets or sets the open PSC inspection count.
        /// </summary>
        /// <value>
        /// The open PSC inspection count.
        /// </value>
        public int OpenPSCInspectionCount { get; set; }

        /// <summary>
        /// Gets or sets the open omv inspection count.
        /// </summary>
        /// <value>
        /// The open omv inspection count.
        /// </value>
        public string OpenOMVInspectionCount { get; set; }

        #endregion
    }
}
