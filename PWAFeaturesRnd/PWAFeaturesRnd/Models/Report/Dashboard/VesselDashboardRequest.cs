using System;

namespace PWAFeaturesRnd.Models.Report.Dashboard
{
    /// <summary>
    /// Vessel dashboard request
    /// </summary>
    public class VesselDashboardRequest
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>
        public string FleetId { get; set; }

        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public string MenuType { get; set; }

        /// <summary>
        /// Gets or sets the vessel ids.
        /// </summary>
        /// <value>
        /// The vessel ids.
        /// </value>
        public string VesselIds { get; set; }

        /// <summary>
        /// Gets or sets the opex over spend high.
        /// </summary>
        /// <value>
        /// The opex over spend high.
        /// </value>
        public decimal OpexOverSpendHigh { get; set; }

        /// <summary>
        /// Gets or sets the opex over spend mid.
        /// </summary>
        /// <value>
        /// The opex over spend mid.
        /// </value>
        public decimal OpexOverSpendMid { get; set; }

        /// <summary>
        /// Gets or sets the haz occ serious incident start date.
        /// </summary>
        /// <value>
        /// The haz occ serious incident start date.
        /// </value>
        public DateTime? HazOccSeriousIncidentStartDate { set; get; }

        /// <summary>
        /// Gets or sets the haz occ serious incident end date.
        /// </summary>
        /// <value>
        /// The haz occ serious incident end date.
        /// </value>
        public DateTime? HazOccSeriousIncidentEndDate { set; get; }

        /// <summary>
        /// Gets or sets the haz occ serious accident start date.
        /// </summary>
        /// <value>
        /// The haz occ serious accident start date.
        /// </value>
        public DateTime? HazOccSeriousAccidentStartDate { set; get; }

        /// <summary>
        /// Gets or sets the haz occ serious accident end date.
        /// </summary>
        /// <value>
        /// The haz occ serious accident end date.
        /// </value>
        public DateTime? HazOccSeriousAccidentEndDate { set; get; }

        /// <summary>
        /// Gets or sets the haz occ unsafe act check count.
        /// </summary>
        /// <value>
        /// The haz occ unsafe act check count.
        /// </value>
        public int HazOccUnsafeActCheckCount { set; get; }

        /// <summary>
        /// Gets or sets the cert stop sailing trading start date.
        /// </summary>
        /// <value>
        /// The cert stop sailing trading start date.
        /// </value>
        public DateTime? CertStopSailingTradingStartDate { set; get; }

        /// <summary>
        /// Gets or sets the cert stop sailing trading end date.
        /// </summary>
        /// <value>
        /// The cert stop sailing trading end date.
        /// </value>
        public DateTime? CertStopSailingTradingEndDate { set; get; }

        /// <summary>
        /// Gets or sets the insp PSC detention start date.
        /// </summary>
        /// <value>
        /// The insp PSC detention start date.
        /// </value>
        public DateTime? InspPSCDetentionStartDate { set; get; }

        /// <summary>
        /// Gets or sets the insp PSC detention end date.
        /// </summary>
        /// <value>
        /// The insp PSC detention end date.
        /// </value>
        public DateTime? InspPSCDetentionEndDate { set; get; }

        /// <summary>
        /// Gets or sets the insp omv rejection start date.
        /// </summary>
        /// <value>
        /// The insp omv rejection start date.
        /// </value>
        public DateTime? InspOMVRejectionStartDate { set; get; }

        /// <summary>
        /// Gets or sets the insp omv rejection end date.
        /// </summary>
        /// <value>
        /// The insp omv rejection end date.
        /// </value>
        public DateTime? InspOMVRejectionEndDate { set; get; }

        /// <summary>
        /// Gets or sets the insp overduefindings check count.
        /// </summary>
        /// <value>
        /// The insp overduefindings check count.
        /// </value>
        public int InspOverduefindingsCheckCount { set; get; }

        /// <summary>
        /// Gets or sets the defects off hire check count.
        /// </summary>
        /// <value>
        /// The defects off hire check count.
        /// </value>
        public int DefectsOffHireCheckCount { set; get; }

        /// <summary>
        /// Gets or sets the defects awaiting spares check count.
        /// </summary>
        /// <value>
        /// The defects awaiting spares check count.
        /// </value>
        public int DefectsAwaitingSparesCheckCount { set; get; }

        /// <summary>
        /// Gets or sets the PMS critical overdue check count.
        /// </summary>
        /// <value>
        /// The PMS critical overdue check count.
        /// </value>
        public int PMSCriticalOverdueCheckCount { set; get; }

        /// <summary>
        /// Gets or sets the PMS overdue check count.
        /// </summary>
        /// <value>
        /// The PMS overdue check count.
        /// </value>
        public int PMSOverdueCheckCount { set; get; }

        /// <summary>
        /// Gets or sets the purch awaiting SNR authentication check days count.
        /// </summary>
        /// <value>
        /// The purch awaiting SNR authentication check days count.
        /// </value>
        public int PurchAwaitingSnrAuthCheckDaysCount { set; get; }

        /// <summary>
        /// Gets or sets the med and high risk.
        /// </summary>
        /// <value>
        /// The med and high risk.
        /// </value>
        public int MedAndHighRisk { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { set; get; }
    }
}
