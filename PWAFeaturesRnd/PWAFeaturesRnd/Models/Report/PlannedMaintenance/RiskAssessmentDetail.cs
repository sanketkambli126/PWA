using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Risk Assessment Detail
	/// </summary>
	public class RiskAssessmentDetail
	{
		/// <summary>
		/// Gets or sets the average risk count.
		/// </summary>
		/// <value>
		/// The average risk count.
		/// </value>
		public double AverageRiskCount { get; set; }

		/// <summary>
		/// Gets or sets the average risk factor description.
		/// </summary>
		/// <value>
		/// The average risk factor description.
		/// </value>
		public string AverageRiskFactorDescription { get; set; }

		/// <summary>
		/// Gets or sets the maximum risk count.
		/// </summary>
		/// <value>
		/// The maximum risk count.
		/// </value>
		public int MaxRiskCount { get; set; }

		/// <summary>
		/// Gets or sets the maximum risk factor description.
		/// </summary>
		/// <value>
		/// The maximum risk factor description.
		/// </value>
		public string MaxRiskFactorDescription { get; set; }

		/// <summary>
		/// Gets or sets the hazard list.
		/// </summary>
		/// <value>
		/// The hazard list.
		/// </value>
		public List<HazardDetail> HazardList { get; set; }

		/// <summary>
		/// Gets or sets the updated site.
		/// </summary>
		/// <value>
		/// The updated site.
		/// </value>
		public string UpdatedSite { get; set; }

		/// <summary>
		/// Gets or sets the interval.
		/// </summary>
		/// <value>
		/// The interval.
		/// </value>
		public int Interval { get; set; }

		/// <summary>
		/// Gets or sets the reference number.
		/// </summary>
		/// <value>
		/// The reference number.
		/// </value>
		public int RefNumber { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the originator.
		/// </summary>
		/// <value>
		/// The originator.
		/// </value>
		public string Originator { get; set; }

		/// <summary>
		/// Gets or sets the system area identifier.
		/// </summary>
		/// <value>
		/// The system area identifier.
		/// </value>
		public string SystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the work activity description.
		/// </summary>
		/// <value>
		/// The work activity description.
		/// </value>
		public string WorkActivityDescription { get; set; }

		/// <summary>
		/// Gets or sets the work activity identifier.
		/// </summary>
		/// <value>
		/// The work activity identifier.
		/// </value>
		public string WorkActivityId { get; set; }

		/// <summary>
		/// Gets or sets the rag identifier.
		/// </summary>
		/// <value>
		/// The rag identifier.
		/// </value>
		public string RagId { get; set; }

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
		/// Gets or sets the rra identifier.
		/// </summary>
		/// <value>
		/// The rra identifier.
		/// </value>
		public string RraId { get; set; }

		/// <summary>
		/// Gets or sets the system area description.
		/// </summary>
		/// <value>
		/// The system area description.
		/// </value>
		public string SystemAreaDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is default ra.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is default ra; otherwise, <c>false</c>.
		/// </value>
		public bool IsDefaultRA { get; set; }
	}
}
