namespace PWAFeaturesRnd.Models.Report.Sentinel
{
	/// <summary>
	/// Sentinel Dashboard Range Vessel Count Detail
	/// </summary>
	public class SentinelDashboardRangeVesselCountDetail
	{
		/// <summary>
		/// Gets or sets the range title.
		/// </summary>
		/// <value>
		/// The range title.
		/// </value>
		public string RangeTitle { get; set; }

		/// <summary>
		/// Gets or sets from range.
		/// </summary>
		/// <value>
		/// From range.
		/// </value>
		public decimal FromRange { get; set; }

		/// <summary>
		/// Gets or sets to range.
		/// </summary>
		/// <value>
		/// To range.
		/// </value>
		public decimal ToRange { get; set; }

		/// <summary>
		/// Gets or sets the range color code.
		/// </summary>
		/// <value>
		/// The range color code.
		/// </value>
		public string RangeColorCode { get; set; }

		/// <summary>
		/// Gets or sets the opacity.
		/// </summary>
		/// <value>
		/// The opacity.
		/// </value>
		public decimal? Opacity { get; set; }

		/// <summary>
		/// Gets or sets the range vessel count.
		/// </summary>
		/// <value>
		/// The range vessel count.
		/// </value>
		public int? RangeVesselCount { get; set; }

		/// <summary>
		/// Gets or sets the range selection section title.
		/// </summary>
		/// <value>
		/// The range selection section title.
		/// </value>
		public string RangeSelectionSectionTitle { get; set; }
	}
}
