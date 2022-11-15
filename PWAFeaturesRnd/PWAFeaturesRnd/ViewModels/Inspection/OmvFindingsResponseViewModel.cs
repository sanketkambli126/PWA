using System;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
	/// <summary>
	/// Omv Findings Response View Model
	/// </summary>
	public class OmvFindingsResponseViewModel
	{
		/// <summary>
		/// Gets or sets the encrypted inspection URL.
		/// </summary>
		/// <value>
		/// The encrypted inspection URL.
		/// </value>
		public string EncryptedInspectionURL { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the encrypted vessel identifier.
		/// </summary>
		/// <value>
		/// The encrypted vessel identifier.
		/// </value>
		public string EncryptedVesselId { get; set; }

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
