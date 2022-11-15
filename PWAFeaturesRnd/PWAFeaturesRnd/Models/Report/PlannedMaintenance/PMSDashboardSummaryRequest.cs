using System;
using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// PMS Dashboard Summary Request
	/// </summary>
	public class PMSDashboardSummaryRequest
	{
		/// <summary>
		/// Gets or sets the item.
		/// </summary>
		/// <value>
		/// The item.
		/// </value>
		public UserMenuItem Item { get; set; }

		/// <summary>
		/// Gets or sets the type of the menu.
		/// </summary>
		/// <value>
		/// The type of the menu.
		/// </value>
		public string MenuType { get; set; }

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public string Identifier { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the user identifier.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		public string UserId { get; set; }

		/// <summary>
		/// Gets or sets the reported in last n days.
		/// </summary>
		/// <value>
		/// The reported in last n days.
		/// </value>
		public int ReportedInLastNDays { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>
		/// The start date.
		/// </value>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>
		/// The end date.
		/// </value>
		public DateTime EndDate { get; set; }

		/// <summary>
		/// Gets or sets the overdue priority limit.
		/// </summary>
		/// <value>
		/// The overdue priority limit.
		/// </value>
		public int OverduePriorityLimit { get; set; }

		/// <summary>
		/// Gets or sets the critical overdue priority limit.
		/// </summary>
		/// <value>
		/// The critical overdue priority limit.
		/// </value>
		public int CriticalOverduePriorityLimit { get; set; }

		/// <summary>
		/// Gets or sets the grid sub title.
		/// </summary>
		/// <value>
		/// The grid sub title.
		/// </value>
		public string GridSubTitle { get; set; }
	}
}
