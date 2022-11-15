namespace PWAFeaturesRnd.Models.Report.Sentinel
{
	/// <summary>
	/// Sentinel Dashboard Office View Color Vessel Count
	/// </summary>
	public class SentinelDashboardOfficeViewColorVesselCount
	{
		/// <summary>
		/// Gets or sets the office identifier.
		/// </summary>
		/// <value>
		/// The office identifier.
		/// </value>
		public string OfficeId { get; set; }

		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>
		/// The color.
		/// </value>
		public string Color { get; set; }

		/// <summary>
		/// Gets or sets the vessel count.
		/// </summary>
		/// <value>
		/// The vessel count.
		/// </value>
		public int? VesselCount { get; set; }
	}
}
