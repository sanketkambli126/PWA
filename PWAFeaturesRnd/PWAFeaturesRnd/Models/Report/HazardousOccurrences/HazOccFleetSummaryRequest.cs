using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
	/// <summary>
	/// HazOcc Fleet Summary Request
	/// </summary>
	public class HazOccFleetSummaryRequest
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
		/// Gets or sets the incident start date.
		/// </summary>
		/// <value>
		/// The incident start date.
		/// </value>
		public DateTime? IncidentStartDate { get; set; }

		/// <summary>
		/// Gets or sets the incident end date.
		/// </summary>
		/// <value>
		/// The incident end date.
		/// </value>
		public DateTime? IncidentEndDate { get; set; }

		/// <summary>
		/// Gets or sets the serious incidents priority.
		/// </summary>
		/// <value>
		/// The serious incidents priority.
		/// </value>
		public int SeriousIncidentsPriority { get; set; }

		/// <summary>
		/// Gets or sets the ltifrom date.
		/// </summary>
		/// <value>
		/// The ltifrom date.
		/// </value>
		public DateTime? LtiFromDate { get; set; }

		/// <summary>
		/// Gets or sets the ltito date.
		/// </summary>
		/// <value>
		/// The ltito date.
		/// </value>
		public DateTime? LtiToDate { get; set; }

		/// <summary>
		/// Gets or sets the ltifpriority.
		/// </summary>
		/// <value>
		/// The ltifpriority.
		/// </value>
		public int LtifPriority { get; set; }

		/// <summary>
		/// Gets or sets the oil spill from date.
		/// </summary>
		/// <value>
		/// The oil spill from date.
		/// </value>
		public DateTime? OilSpillFromDate { get; set; }

		/// <summary>
		/// Gets or sets the oil spill to date.
		/// </summary>
		/// <value>
		/// The oil spill to date.
		/// </value>
		public DateTime? OilSpillToDate { get; set; }

		/// <summary>
		/// Gets or sets the oil spill priority limit.
		/// </summary>
		/// <value>
		/// The oil spill priority limit.
		/// </value>
		public int OilSpillPriorityLimit { get; set; }
	}
}
