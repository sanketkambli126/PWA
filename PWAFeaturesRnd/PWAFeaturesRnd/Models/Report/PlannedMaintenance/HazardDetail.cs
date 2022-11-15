using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Hazard Detail
	/// </summary>
	public class HazardDetail
	{
		/// <summary>
		/// Gets or sets the updated site.
		/// </summary>
		/// <value>
		/// The updated site.
		/// </value>
		public string UpdatedSite { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is additional hazard.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is additional hazard; otherwise, <c>false</c>.
		/// </value>
		public bool IsAdditionalHazard { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets the reported date.
		/// </summary>
		/// <value>
		/// The reported date.
		/// </value>
		public DateTime? ReportedDate { get; set; }

		/// <summary>
		/// Gets or sets the risk factor definition.
		/// </summary>
		/// <value>
		/// The risk factor definition.
		/// </value>
		public string RiskFactorDefinition { get; set; }

		/// <summary>
		/// Gets or sets the likelihood definition.
		/// </summary>
		/// <value>
		/// The likelihood definition.
		/// </value>
		public string LikelihoodDefinition { get; set; }

		/// <summary>
		/// Gets or sets the severity definition.
		/// </summary>
		/// <value>
		/// The severity definition.
		/// </value>
		public string SeverityDefinition { get; set; }

		/// <summary>
		/// Gets or sets the initial risk count.
		/// </summary>
		/// <value>
		/// The initial risk count.
		/// </value>
		public int InitialRiskCount { get; set; }

		/// <summary>
		/// Gets or sets the risk count.
		/// </summary>
		/// <value>
		/// The risk count.
		/// </value>
		public int RiskCount { get; set; }

		/// <summary>
		/// Gets or sets the initial risk factor description.
		/// </summary>
		/// <value>
		/// The initial risk factor description.
		/// </value>
		public string InitialRiskFactorDescription { get; set; }

		/// <summary>
		/// Gets or sets the risk factor description.
		/// </summary>
		/// <value>
		/// The risk factor description.
		/// </value>
		public string RiskFactorDescription { get; set; }

		/// <summary>
		/// Gets or sets the initial severity description.
		/// </summary>
		/// <value>
		/// The initial severity description.
		/// </value>
		public string InitialSeverityDescription { get; set; }

		/// <summary>
		/// Gets or sets the initial severity identifier.
		/// </summary>
		/// <value>
		/// The initial severity identifier.
		/// </value>
		public string InitialSeverityId { get; set; }

		/// <summary>
		/// Gets or sets the initial likelihood description.
		/// </summary>
		/// <value>
		/// The initial likelihood description.
		/// </value>
		public string InitialLikelihoodDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

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
		/// Gets or sets the likelihood description.
		/// </summary>
		/// <value>
		/// The likelihood description.
		/// </value>
		public string LikelihoodDescription { get; set; }

		/// <summary>
		/// Gets or sets the likelihood identifier.
		/// </summary>
		/// <value>
		/// The likelihood identifier.
		/// </value>
		public string LikelihoodId { get; set; }

		/// <summary>
		/// Gets or sets the consequences.
		/// </summary>
		/// <value>
		/// The consequences.
		/// </value>
		public string Consequences { get; set; }

		/// <summary>
		/// Gets or sets the controls.
		/// </summary>
		/// <value>
		/// The controls.
		/// </value>
		public string Controls { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the hazard number.
		/// </summary>
		/// <value>
		/// The hazard number.
		/// </value>
		public int HazardNumber { get; set; }

		/// <summary>
		/// Gets or sets the rag identifier.
		/// </summary>
		/// <value>
		/// The rag identifier.
		/// </value>
		public string RagId { get; set; }

		/// <summary>
		/// Gets or sets the RGH identifier.
		/// </summary>
		/// <value>
		/// The RGH identifier.
		/// </value>
		public string RghId { get; set; }

		/// <summary>
		/// Gets or sets the rra identifier.
		/// </summary>
		/// <value>
		/// The rra identifier.
		/// </value>
		public string RraId { get; set; }

		/// <summary>
		/// Gets or sets the source identifier.
		/// </summary>
		/// <value>
		/// The source identifier.
		/// </value>
		public string SourceId { get; set; }

		/// <summary>
		/// Gets or sets the mla identifier source.
		/// </summary>
		/// <value>
		/// The mla identifier source.
		/// </value>
		public string MlaIdSource { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the PRZ identifier.
		/// </summary>
		/// <value>
		/// The PRZ identifier.
		/// </value>
		public string PrzId { get; set; }

		/// <summary>
		/// Gets or sets the severity description.
		/// </summary>
		/// <value>
		/// The severity description.
		/// </value>
		public string SeverityDescription { get; set; }

		/// <summary>
		/// Gets or sets the further control measure.
		/// </summary>
		/// <value>
		/// The further control measure.
		/// </value>
		public List<FurtherControlMeasures> FurtherControlMeasure { get; set; }
	}
}
