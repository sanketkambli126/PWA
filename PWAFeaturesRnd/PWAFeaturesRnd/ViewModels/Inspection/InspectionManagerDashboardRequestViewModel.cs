using System;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>Inspection Manager Dashboard Request ViewModel</summary>
    public class InspectionManagerDashboardRequestViewModel
    {
        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the inspection type ids.
        /// </summary>
        /// <value>
        /// The inspection type ids.
        /// </value>
        public string InspectionTypeIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is navigated from dashboard.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is navigated from dashboard; otherwise, <c>false</c>.
        /// </value>
        public bool IsNavigatedFromDashboard { get; set; }

        /// <summary>Gets or sets the deficiencies per PSC from date.</summary>
        /// <value>The deficiencies per PSC from date.</value>
        public DateTime DeficienciesPerPSCFromDate { get; set; }

        /// <summary>Gets or sets the deficiencies per PSC to date.</summary>
        /// <value>The deficiencies per PSC to date.</value>
        public DateTime DeficienciesPerPSCToDate { get; set; }

        /// <summary>Gets or sets the psc detention from date.</summary>
        /// <value>The psc detention from date.</value>
        public DateTime PSCDetentionFromDate { get; set; }

        /// <summary>Gets or sets the psc detention to date.</summary>
        /// <value>The psc detention to date.</value>
        public DateTime PSCDetentionToDate { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per PSC priority high limit.
        /// </summary>
        /// <value>
        /// The deficiencies per PSC priority high limit.
        /// </value>
        public decimal DeficienciesPerPscPriorityHighLimit { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per PSC priority mid limit.
        /// </summary>
        /// <value>
        /// The deficiencies per PSC priority mid limit.
        /// </value>
        public decimal DeficienciesPerPscPriorityMidLimit { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per PSC priority low limit.
        /// </summary>
        /// <value>
        /// The deficiencies per PSC priority low limit.
        /// </value>
        public decimal DeficienciesPerPscPriorityLowLimit { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per omv priority high limit.
        /// </summary>
        /// <value>
        /// The deficiencies per omv priority high limit.
        /// </value>
        public decimal DeficienciesPerOmvPriorityHighLimit { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per omv priority mid limit.
        /// </summary>
        /// <value>
        /// The deficiencies per omv priority mid limit.
        /// </value>
        public decimal DeficienciesPerOmvPriorityMidLimit { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per omv priority low limit.
        /// </summary>
        /// <value>
        /// The deficiencies per omv priority low limit.
        /// </value>
        public decimal DeficienciesPerOmvPriorityLowLimit { get; set; }

        /// <summary>
        /// Gets or sets the PSC deficiency from date.
        /// </summary>
        /// <value>
        /// The PSC deficiency from date.
        /// </value>
        public DateTime PscDeficiencyFromDate { get; set; }

        /// <summary>
        /// Gets or sets the PSC deficiency to date.
        /// </summary>
        /// <value>
        /// The PSC deficiency to date.
        /// </value>
        public DateTime PscDeficiencyToDate { get; set; }

        /// <summary>
        /// Gets or sets the omv rejection from date.
        /// </summary>
        /// <value>
        /// The omv rejection from date.
        /// </value>
        public DateTime OmvRejectionFromDate { get; set; }

        /// <summary>
        /// Gets or sets the omv rejection to date.
        /// </summary>
        /// <value>
        /// The omv rejection to date.
        /// </value>
        public DateTime OmvRejectionToDate { get; set; }

        /// <summary>
        /// Gets or sets the overdue findings priority limit.
        /// </summary>
        /// <value>
        /// The overdue findings priority limit.
        /// </value>
        public int OverdueFindingsPriorityLimit { get; set; }

        /// <summary>
        /// Gets or sets the overdue inspections priority limit.
        /// </summary>
        /// <value>
        /// The overdue inspections priority limit.
        /// </value>
        public int OverdueInspectionsPriorityLimit { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is from dashboard.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is from dashboard; otherwise, <c>false</c>.
        /// </value>
        public bool IsFromDashboard { get; set; }
    }
}
