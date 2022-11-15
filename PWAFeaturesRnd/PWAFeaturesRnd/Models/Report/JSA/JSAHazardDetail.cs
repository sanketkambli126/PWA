using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// 
    /// </summary>
    public class JSAHazardDetail
    {
        /// <summary>
        /// Gets or sets the job additional hazard identifier.
        /// </summary>
        /// <value>
        /// The job additional hazard identifier.
        /// </value>
        public string JahId { get; set; }

        /// <summary>
        /// Gets or sets the risk assessment hazard identifier.
        /// </summary>
        /// <value>
        /// The risk assessment hazard identifier.
        /// </value>
        public string RahId { get; set; }

        /// <summary>
        /// Gets or sets the RGH identifier.
        /// </summary>
        /// <value>
        /// The RGH identifier.
        /// </value>
        public string RghId { get; set; }

        /// <summary>
        /// Gets or sets the risk assessment vessel identifier.
        /// </summary>
        /// <value>
        /// The risk assessment vessel identifier.
        /// </value>
        public string RavId { get; set; }

        /// <summary>
        /// Gets or sets the rag identifier.
        /// </summary>
        /// <value>
        /// The rag identifier.
        /// </value>
        public string RagId { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the hazard number.
        /// </summary>
        /// <value>
        /// The hazard number.
        /// </value>
        public int HazardNumber { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the control measures.
        /// </summary>
        /// <value>
        /// The control measures.
        /// </value>
        public string ControlMeasures { get; set; }

        /// <summary>
        /// Gets or sets the consequences.
        /// </summary>
        /// <value>
        /// The consequences.
        /// </value>
        public string Consequences { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hazard active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hazard active]; otherwise, <c>false</c>.
        /// </value>
        public bool HazardActive { get; set; }

        /// <summary>
        /// Gets or sets the likelihood identifier.
        /// </summary>
        /// <value>
        /// The likelihood identifier.
        /// </value>
        public string LikelihoodId { get; set; }

        /// <summary>
        /// Gets or sets the initial likelihood identifier.
        /// </summary>
        /// <value>
        /// The initial likelihood identifier.
        /// </value>
        public string InitialLikelihoodId { get; set; }

        /// <summary>
        /// Gets or sets the severity identifier.
        /// </summary>
        /// <value>
        /// The severity identifier.
        /// </value>
        public string SeverityId { get; set; }

        /// <summary>
        /// Gets or sets the initial severity identifier.
        /// </summary>
        /// <value>
        /// The initial severity identifier.
        /// </value>
        public string InitialSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the likelihood description.
        /// </summary>
        /// <value>
        /// The likelihood description.
        /// </value>
        public string LikelihoodDescription { get; set; }

        /// <summary>
        /// Gets or sets the initial likelihood description.
        /// </summary>
        /// <value>
        /// The initial likelihood description.
        /// </value>
        public string InitialLikelihoodDescription { get; set; }

        /// <summary>
        /// Gets or sets the severity description.
        /// </summary>
        /// <value>
        /// The severity description.
        /// </value>
        public string SeverityDescription { get; set; }

        /// <summary>
        /// Gets or sets the initial severity description.
        /// </summary>
        /// <value>
        /// The initial severity description.
        /// </value>
        public string InitialSeverityDescription { get; set; }

        /// <summary>
        /// Gets or sets the risk factor description.
        /// </summary>
        /// <value>
        /// The risk factor description.
        /// </value>
        public string RiskFactorDescription { get; set; }

        /// <summary>
        /// Gets or sets the initial risk factor description.
        /// </summary>
        /// <value>
        /// The initial risk factor description.
        /// </value>
        public string InitialRiskFactorDescription { get; set; }

        /// <summary>
        /// Gets or sets the risk count.
        /// </summary>
        /// <value>
        /// The risk count.
        /// </value>
        public int RiskCount { get; set; }

        /// <summary>
        /// Gets or sets the initial risk count.
        /// </summary>
        /// <value>
        /// The initial risk count.
        /// </value>
        public int InitialRiskCount { get; set; }

        /// <summary>
        /// Gets or sets the jsa hazard bookmark detail.
        /// </summary>
        /// <value>
        /// The jsa hazard bookmark detail.
        /// </value>
        public List<JSABookmarkDetail> JSAHazardBookmarkDetail { get; set; }

        /// <summary>
        /// Gets or sets the further control measure.
        /// </summary>
        /// <value>
        /// The further control measure.
        /// </value>
        public List<JSAFurtherControlMeasure> FurtherControlMeasure { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is additional hazard.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is additional hazard; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdditionalHazard { get; set; }

        /// <summary>
        /// Gets or sets the reported date.
        /// </summary>
        /// <value>
        /// The reported date.
        /// </value>
        public DateTime? ReportedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the severity definition.
        /// </summary>
        /// <value>
        /// The severity definition.
        /// </value>
        public string SeverityDefinition { get; set; }

        /// <summary>
        /// Gets or sets the likelihood definition.
        /// </summary>
        /// <value>
        /// The likelihood definition.
        /// </value>
        public string LikelihoodDefinition { get; set; }

        /// <summary>
        /// Gets or sets the risk factor definition.
        /// </summary>
        /// <value>
        /// The risk factor definition.
        /// </value>
        public string RiskFactorDefinition { get; set; }
    }
}
