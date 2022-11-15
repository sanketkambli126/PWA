using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// Jsa Job Detail Request Custom Contract
    /// </summary>
    public class JsaJobDetailRequest
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the job type identifier list.
        /// </summary>
        /// <value>
        /// The job type identifier list.
        /// </value>
        public List<string> JobTypeIdList { get; set; }

        /// <summary>
        /// Gets or sets the status identifier list.
        /// </summary>
        /// <value>
        /// The status identifier list.
        /// </value>
        public List<string> StatusIdList { get; set; }

        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the type of the risk.
        /// </summary>
        /// <value>
        /// The type of the risk.
        /// </value>
        public RiskAssessment? riskType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cancelled jsa include.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is cancelled jsa include; otherwise, <c>false</c>.
        /// </value>
        public bool IsNotIncludeCancelledJsa { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is overdue for closure.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is overdue for closure; otherwise, <c>false</c>.
        /// <value>
        public bool IsOverdueForClosure { get; set; }

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
        public List<string> VesselIds { get; set; }
    }
}
