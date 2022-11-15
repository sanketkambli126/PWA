using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// JsaJobDetailResponse
    /// </summary>
    public class JsaJobDetailResponse
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the type of the job.
        /// </summary>
        /// <value>
        /// The type of the job.
        /// </value>
        public string JobType { get; set; }

        /// <summary>
        /// Gets or sets the job type identifier.
        /// </summary>
        /// <value>
        /// The job type identifier.
        /// </value>
        public string JobTypeId { get; set; }

        /// <summary>
        /// Gets or sets the estimated start date.
        /// </summary>
        /// <value>
        /// The estimated start date.
        /// </value>
        public DateTime EstimatedStartDate { get; set; }

        /// <summary>
        /// Gets or sets the estimated end date.
        /// </summary>
        /// <value>
        /// The estimated end date.
        /// </value>
        public DateTime EstimatedEndDate { get; set; }

        /// <summary>
        /// Gets or sets the risk factor.
        /// </summary>
        /// <value>
        /// The risk factor.
        /// </value>
        public int RiskFactor { get; set; }

        /// <summary>
        /// Gets or sets the risk factor description.
        /// </summary>
        /// <value>
        /// The risk factor description.
        /// </value>
        public string RiskFactorDescription { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the updated site.
        /// </summary>
        /// <value>
        /// The updated site.
        /// </value>
        public string UpdatedSite { get; set; }

        /// <summary>
        /// Gets or sets the reference number.
        /// </summary>
        /// <value>
        /// The reference number.
        /// </value>
        public int RefNumber { get; set; }

        /// <summary>
        /// Gets or sets the PGR identifier.
        /// </summary>
        /// <value>
        /// The PGR identifier.
        /// </value>
        public string PgrId { get; set; }

        /// <summary>
        /// Gets or sets the creator role identifier.
        /// </summary>
        /// <value>
        /// The creator role identifier.
        /// </value>
        public string CreatorRoleIdentifier { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is critical operation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is critical operation; otherwise, <c>false</c>.
        /// </value>
        public bool? IsCriticalOperation { get; set; }

        /// <summary>
        /// Gets or sets the completed date.
        /// </summary>
        /// <value>
        /// The completed date.
        /// </value>
        public DateTime? CompletedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the fore.
        /// </summary>
        /// <value>
        /// The name of the fore.
        /// </value>
        public string ForeName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the job detail.
        /// </summary>
        /// <value>
        /// The job detail.
        /// </value>
        public string JobDetail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is simultaneous job visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is simultaneous job visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsSimultaneousJobVisible { get; set; }

        /// <summary>
        /// Gets or sets the simultaneous job count.
        /// </summary>
        /// <value>
        /// The simultaneous job count.
        /// </value>
        public int SimultaneousJobCount { get; set; }

        /// <summary>
        /// Gets or sets the jsa permit list.
        /// </summary>
        /// <value>
        /// The jsa permit list.
        /// </value>
        public List<JSAAttributeDetail> JsaPermitList { get; set; }

        /// <summary>
        /// Gets or sets the crew meeting date.
        /// </summary>
        /// <value>
        /// The crew meeting date.
        /// </value>
        public DateTime? CrewMeetingDate { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public string StatusId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }
    }
}
