using System;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	/// <summary>
	/// Omv Findings Response
	/// </summary>
	public class OmvFindingsResponse
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

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

		/// <summary>
		/// Gets or sets the inspection identifier.
		/// </summary>
		/// <value>
		/// The inspection identifier.
		/// </value>
		public string InspectionId { get; set; }

		/// <summary>
		/// Gets or sets the type of the inspection.
		/// </summary>
		/// <value>
		/// The type of the inspection.
		/// </value>
		public string InspectionType { get; set; }

		/// <summary>
		/// Gets or sets the inspection date.
		/// </summary>
		/// <value>
		/// The inspection date.
		/// </value>
		public DateTime? InspectionDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the company.
		/// </summary>
		/// <value>
		/// The name of the company.
		/// </value>
		public string CompanyName { get; set; }

		/// <summary>
		/// Gets or sets the where.
		/// </summary>
		/// <value>
		/// The where.
		/// </value>
		public string Where { get; set; }

		/// <summary>
		/// Gets or sets the name of the inspector.
		/// </summary>
		/// <value>
		/// The name of the inspector.
		/// </value>
		public string InspectorName { get; set; }

		/// <summary>
		/// Gets or sets the finding count.
		/// </summary>
		/// <value>
		/// The finding count.
		/// </value>
		public int FindingCount { get; set; }

		/// <summary>
		/// Gets or sets the next due date.
		/// </summary>
		/// <value>
		/// The next due date.
		/// </value>
		public DateTime? NextDueDate { get; set; }
	}
}
