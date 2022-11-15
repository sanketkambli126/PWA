using System;

namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// DefectReportWorkOrderHSE
	/// </summary>
	public class DefectReportWorkOrderHSE
	{
		/// <summary>
		/// Gets or sets the DHR identifier.
		/// </summary>
		/// <value>
		/// The DHR identifier.
		/// </value>
		public string DhrId { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the DRW identifier.
		/// </summary>
		/// <value>
		/// The DRW identifier.
		/// </value>
		public string DrwId { get; set; }

		/// <summary>
		/// Gets or sets the RNK identifier.
		/// </summary>
		/// <value>
		/// The RNK identifier.
		/// </value>
		public string RnkId { get; set; }

		/// <summary>
		/// Gets or sets the hse impact identifier.
		/// </summary>
		/// <value>
		/// The hse impact identifier.
		/// </value>
		public string HSEImpactId { get; set; }

		/// <summary>
		/// Gets or sets the hse likelihood identifier.
		/// </summary>
		/// <value>
		/// The hse likelihood identifier.
		/// </value>
		public string HSELikelihoodId { get; set; }

		/// <summary>
		/// Gets or sets the hse number.
		/// </summary>
		/// <value>
		/// The hse number.
		/// </value>
		public int? HSENumber { get; set; }

		/// <summary>
		/// Gets or sets the assessment date.
		/// </summary>
		/// <value>
		/// The assessment date.
		/// </value>
		public DateTime? AssessmentDate { get; set; }

		/// <summary>
		/// Gets or sets the hse hazard.
		/// </summary>
		/// <value>
		/// The hse hazard.
		/// </value>
		public string HSEHazard { get; set; }

		/// <summary>
		/// Gets or sets the control required.
		/// </summary>
		/// <value>
		/// The control required.
		/// </value>
		public string ControlRequired { get; set; }

		/// <summary>
		/// Gets or sets the hse impact.
		/// </summary>
		/// <value>
		/// The hse impact.
		/// </value>
		public string HSEImpact { get; set; }

		/// <summary>
		/// Gets or sets the hse likelihood.
		/// </summary>
		/// <value>
		/// The hse likelihood.
		/// </value>
		public string HSELikelihood { get; set; }

		/// <summary>
		/// Gets or sets the responsibility.
		/// </summary>
		/// <value>
		/// The responsibility.
		/// </value>
		public string Responsibility { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }
	}
}
