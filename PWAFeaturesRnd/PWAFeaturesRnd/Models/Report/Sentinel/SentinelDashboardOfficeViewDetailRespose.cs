using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Sentinel
{
	/// <summary>
	/// Sentinel Dashboard Office View Detail Respose
	/// </summary>
	public class SentinelDashboardOfficeViewDetailRespose
	{
		/// <summary>
		/// Gets or sets the office detail.
		/// </summary>
		/// <value>
		/// The office detail.
		/// </value>
		public List<SentinelDashboardOfficeViewDetail> OfficeDetail { get; set; }

		/// <summary>
		/// Gets or sets the color vessel count detail.
		/// </summary>
		/// <value>
		/// The color vessel count detail.
		/// </value>
		public List<SentinelDashboardOfficeViewColorVesselCount> ColorVesselCountDetail { get; set; }

		/// <summary>
		/// Gets or sets the range vessel count detail.
		/// </summary>
		/// <value>
		/// The range vessel count detail.
		/// </value>
		public List<SentinelDashboardRangeVesselCountDetail> RangeVesselCountDetail { get; set; }
	}
}
