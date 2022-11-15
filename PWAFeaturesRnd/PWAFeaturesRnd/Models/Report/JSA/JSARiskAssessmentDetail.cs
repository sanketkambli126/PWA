using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// 
    /// </summary>
    public class JSARiskAssessmentDetail
    {
        /// <summary>
        /// Gets or sets the job risk assessment identifier.
        /// </summary>
        /// <value>
        /// The job risk assessment identifier.
        /// </value>
        public String JraId { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public String JobId { get; set; }

        /// <summary>
        /// Gets or sets the risk assessment vessel identifier.
        /// </summary>
        /// <value>
        /// The risk assessment vessel identifier.
        /// </value>
        public String RavId { get; set; }

        /// <summary>
        /// Gets or sets the rag identifier.
        /// </summary>
        /// <value>
        /// The rag identifier.
        /// </value>
        public String RagId { get; set; }

        /// <summary>
        /// Gets or sets the work activity identifier.
        /// </summary>
        /// <value>
        /// The work activity identifier.
        /// </value>
        public String WorkActivityId { get; set; }

        /// <summary>
        /// Gets or sets the work activity description.
        /// </summary>
        /// <value>
        /// The work activity description.
        /// </value>
        public String WorkActivityDescription { get; set; }

        /// <summary>
        /// Gets or sets the system area identifier.
        /// </summary>
        /// <value>
        /// The system area identifier.
        /// </value>
        public String SystemAreaId { get; set; }

        /// <summary>
        /// Gets or sets the system area description.
        /// </summary>
        /// <value>
        /// The system area description.
        /// </value>
        public String SystemAreaDescription { get; set; }

        /// <summary>
        /// Gets or sets the originator.
        /// </summary>
        /// <value>
        /// The originator.
        /// </value>
        public String Originator { get; set; }

        /// <summary>
        /// Gets or sets the reference number.
        /// </summary>
        /// <value>
        /// The reference number.
        /// </value>
        public int RefNumber { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public int Interval { get; set; }

        /// <summary>
        /// Gets or sets the last assessment.
        /// </summary>
        /// <value>
        /// The last assessment.
        /// </value>
        public DateTime? LastAssessment { get; set; }

        /// <summary>
        /// Gets or sets the next assessment.
        /// </summary>
        /// <value>
        /// The next assessment.
        /// </value>
        public DateTime? NextAssessment { get; set; }

        /// <summary>
        /// Gets or sets the hazard list.
        /// </summary>
        /// <value>
        /// The hazard list.
        /// </value>
        public List<JSAHazardDetail> HazardList { get; set; }

        /// <summary>
        /// Gets or sets the jsara bookmark detail.
        /// </summary>
        /// <value>
        /// The jsara bookmark detail.
        /// </value>
        public List<JSABookmarkDetail> JSARABookmarkDetail { get; set; }

        /// <summary>
        /// Gets or sets the maximum risk factor description.
        /// </summary>
        /// <value>
        /// The maximum risk factor description.
        /// </value>
        public string MaxRiskFactorDescription { get; set; }

        /// <summary>
        /// Gets or sets the maximum risk count.
        /// </summary>
        /// <value>
        /// The maximum risk count.
        /// </value>
        public int MaxRiskCount { get; set; }

        /// <summary>
        /// Gets or sets the average risk factor description.
        /// </summary>
        /// <value>
        /// The average risk factor description.
        /// </value>
        public string AverageRiskFactorDescription { get; set; }

        /// <summary>
        /// Gets or sets the average risk count.
        /// </summary>
        /// <value>
        /// The average risk count.
        /// </value>
        public double AverageRiskCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }
    }
}
