using System;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
	/// <summary>
	/// Inspection View Model
	/// </summary>
	public class InspectionViewModel
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
		/// Gets or sets the inspection identifier.
		/// </summary>
		/// <value>
		/// The inspection identifier.
		/// </value>
		public string InspectionId { get; set; }

		/// <summary>
		/// Gets or sets the inspection type identifier.
		/// </summary>
		/// <value>
		/// The inspection type identifier.
		/// </value>
		public string InspectionTypeId { get; set; }

		/// <summary>
		/// Gets or sets the type of the inspection.
		/// </summary>
		/// <value>
		/// The type of the inspection.
		/// </value>
		public string InspectionType { get; set; }

		/// <summary>
		/// Gets or sets the default interval.
		/// </summary>
		/// <value>
		/// The default interval.
		/// </value>
		public int DefaultInterval { get; set; }

		/// <summary>
		/// Gets or sets the occured date.
		/// </summary>
		/// <value>
		/// The occured date.
		/// </value>
		public string OccuredDate { get; set; }

		/// <summary>
		/// Gets or sets the next due date.
		/// </summary>
		/// <value>
		/// The next due date.
		/// </value>
		public string NextDueDate { get; set; }

		/// <summary>
		/// Gets or sets from location.
		/// </summary>
		/// <value>
		/// From location.
		/// </value>
		public string FromLocation { get; set; }

		/// <summary>
		/// Converts to location.
		/// </summary>
		/// <value>
		/// To location.
		/// </value>
		public string ToLocation { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the name of the company.
		/// </summary>
		/// <value>
		/// The name of the company.
		/// </value>
		public string CompanyName { get; set; }

		/// <summary>
		/// Gets or sets the inspection status.
		/// </summary>
		/// <value>
		/// The inspection status.
		/// </value>
		public string InspectionStatus { get; set; }

		/// <summary>
		/// Gets or sets the management start date.
		/// </summary>
		/// <value>
		/// The management start date.
		/// </value>
		public string ManagementStartDate { get; set; }

		/// <summary>
		/// Gets or sets the total finding count.
		/// </summary>
		/// <value>
		/// The total finding count.
		/// </value>
		public int TotalFindingCount { get; set; }

		/// <summary>
		/// Gets or sets the out standing finding count.
		/// </summary>
		/// <value>
		/// The out standing finding count.
		/// </value>
		public int OutStandingFindingCount { get; set; }

		/// <summary>
		/// Gets or sets the overdue finding count.
		/// </summary>
		/// <value>
		/// The overdue finding count.
		/// </value>
		public int OverdueFindingCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is detention.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is detention; otherwise, <c>false</c>.
		/// </value>
		public bool IsDetention { get; set; }

		/// <summary>
		/// Gets or sets the finding URL.
		/// </summary>
		/// <value>
		/// The finding URL.
		/// </value>
		public string FindingURL { get; set; }

		/// <summary>
		/// Gets or sets the type of the occured date.
		/// </summary>
		/// <value>
		/// The type of the occured date.
		/// </value>
		public DateTime? OccuredDateType { get; set; }

		/// <summary>
		/// Gets or sets the next due date.
		/// </summary>
		/// <value>
		/// The next due date.
		/// </value>
		public DateTime? NextDueDateType { get; set; }

		/// <summary>
		/// Gets or sets the type of the management start date.
		/// </summary>
		/// <value>
		/// The type of the management start date.
		/// </value>
		public DateTime? ManagementStartDateType { get; set; }

		/// <summary>
		/// Gets or sets the interval.
		/// </summary>
		/// <value>
		/// The interval.
		/// </value>
		public string Interval { get; set; }

	}
}
