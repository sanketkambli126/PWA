using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Finance
{
	/// <summary>
	/// Over Budget Details Request
	/// </summary>
	public class OverBudgetDetailsRequest
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
		/// Gets or sets the vessel ids.
		/// </summary>
		/// <value>
		/// The vessel ids.
		/// </value>
		public List<string> VesselIds { get; set; }

		/// <summary>
		/// Gets or sets the type of the chart.
		/// </summary>
		/// <value>
		/// The type of the chart.
		/// </value>
		public string ChartType { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime? ToDate { get; set; }
	}
}
