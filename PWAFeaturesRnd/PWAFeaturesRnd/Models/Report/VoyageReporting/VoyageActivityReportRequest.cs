using System;
using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Voyage Activity Report Request
	/// </summary>
	public class VoyageActivityReportRequest
	{
		/// <summary>
		/// Gets or sets the item.
		/// </summary>
		/// <value>
		/// The item.
		/// </value>
		public UserMenuItem Item { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the date from.
		/// </summary>
		/// <value>
		/// The date from.
		/// </value>
		public DateTime DateFrom { get; set; }

		/// <summary>
		/// Gets or sets the date to.
		/// </summary>
		/// <value>
		/// The date to.
		/// </value>
		public DateTime DateTo { get; set; }
	}
}
