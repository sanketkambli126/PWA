using System;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Inspection Manager Summary Request
    /// </summary>
    public class InspectionManagerSummaryRequest
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the PSC detention from date.
        /// </summary>
        /// <value>
        /// The PSC detention from date.
        /// </value>
        public DateTime? PSCDetentionFromDate { get; set; }

        /// <summary>
        /// Gets or sets the PSC detention to date.
        /// </summary>
        /// <value>
        /// The PSC detention to date.
        /// </value>
        public DateTime? PSCDetentionToDate { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per PSC from date.
        /// </summary>
        /// <value>
        /// The deficiencies per PSC from date.
        /// </value>
        public DateTime? DeficienciesPerPSCFromDate { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per PSC to date.
        /// </summary>
        /// <value>
        /// The deficiencies per PSC to date.
        /// </value>
        public DateTime? DeficienciesPerPSCToDate { get; set; }

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
        /// Gets or sets the deficiencies per omv from date.
        /// </summary>
        /// <value>
        /// The deficiencies per omv from date.
        /// </value>
        public DateTime? DeficienciesPerOMVFromDate { get; set; }

        /// <summary>
        /// Gets or sets the deficiencies per omv to date.
        /// </summary>
        /// <value>
        /// The deficiencies per omv to date.
        /// </value>
        public DateTime? DeficienciesPerOMVToDate { get; set; }

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
        public DateTime? PscDeficiencyFromDate { get; set; }

        /// <summary>
        /// Gets or sets the PSC deficiency to date.
        /// </summary>
        /// <value>
        /// The PSC deficiency to date.
        /// </value>
        public DateTime? PscDeficiencyToDate { get; set; }

        /// <summary>
        /// Gets or sets the omv rejection from date.
        /// </summary>
        /// <value>
        /// The omv rejection from date.
        /// </value>
        public DateTime? OmvRejectionFromDate { get; set; }

        /// <summary>
        /// Gets or sets the omv rejection to date.
        /// </summary>
        /// <value>
        /// The omv rejection to date.
        /// </value>
        public DateTime? OmvRejectionToDate { get; set; }

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
    }
}
