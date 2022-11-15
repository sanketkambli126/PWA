using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class PMSRequestViewModel
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime ToDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is summary clicked.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is summary clicked; otherwise, <c>false</c>.
		/// </value>
		public bool IsSummaryClicked { get; set; }

		/// <summary>
		/// Gets or sets the type of the PMS dashboard.
		/// </summary>
		/// <value>
		/// The type of the PMS dashboard.
		/// </value>
		public PMSDashboardType PMSDashboardType { get; set; }
	}
}
