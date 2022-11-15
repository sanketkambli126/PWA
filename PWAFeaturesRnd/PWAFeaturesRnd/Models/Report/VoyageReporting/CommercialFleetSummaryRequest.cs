using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Commercial Fleet Summary Request
	/// </summary>
	public class CommercialFleetSummaryRequest
	{
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
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the off hire start date.
		/// </summary>
		/// <value>
		/// The off hire start date.
		/// </value>
		public DateTime? OffHireStartDate { get; set; }

		/// <summary>
		/// Gets or sets the off hire end date.
		/// </summary>
		/// <value>
		/// The off hire end date.
		/// </value>
		public DateTime? OffHireEndDate { get; set; }

		/// <summary>
		/// Gets or sets the off hire priority.
		/// </summary>
		/// <value>
		/// The off hire priority.
		/// </value>
		public string OffHirePriority { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency from date.
		/// </summary>
		/// <value>
		/// The fuel efficiency from date.
		/// </value>
		public DateTime? FuelEfficiencyFromDate { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency to date.
		/// </summary>
		/// <value>
		/// The fuel efficiency to date.
		/// </value>
		public DateTime? FuelEfficiencyToDate { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency priority high limit.
		/// </summary>
		/// <value>
		/// The fuel efficiency priority high limit.
		/// </value>
		public decimal FuelEfficiencyPriorityHighLimit { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency priority low limit.
		/// </summary>
		/// <value>
		/// The fuel efficiency priority low limit.
		/// </value>
		public decimal FuelEfficiencyPriorityLowLimit { get; set; }
	}
}
