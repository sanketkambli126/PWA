using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	/// <summary>
	/// 
	/// </summary>
	public class InspectionOverviewPlanningRequest
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
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets the inspection type ids.
		/// </summary>
		/// <value>
		/// The inspection type ids.
		/// </value>
		public List<string> InspectionTypeIds { get; set; }

		/// <summary>
		/// Gets or sets the in days.
		/// </summary>
		/// <value>
		/// The in days.
		/// </value>
		public int InDays { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is due.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is due; otherwise, <c>false</c>.
		/// </value>
		public bool IsDue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is overdue.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is overdue; otherwise, <c>false</c>.
		/// </value>
		public bool IsOverdue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is never done.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is never done; otherwise, <c>false</c>.
		/// </value>
		public bool IsNeverDone { get; set; }

		/// <summary>
		/// Gets or sets the type of the menu.
		/// </summary>
		/// <value>
		/// The type of the menu.
		/// </value>
		public string MenuType { get; set; }

		/// <summary>
		/// Gets or sets the fleet identifier.
		/// </summary>
		/// <value>
		/// The fleet identifier.
		/// </value>
		public string FleetId { get; set; }

		/// <summary>
		/// Gets or sets the type of the inspection.
		/// </summary>
		/// <value>
		/// The type of the inspection.
		/// </value>
		public InspectionDashboardType? InspectionType { get; set; }

		/// <summary>
		/// Gets or sets the type ids.
		/// </summary>
		/// <value>
		/// The type ids.
		/// </value>
		public string TypeIds { get; set; }
	}
}
