namespace PWAFeaturesRnd.ViewModels.Sentinel
{
	/// <summary>
	/// Vessel Sentinel Score Response View Model
	/// </summary>
	public class VesselSentinelScoreResponseViewModel
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the sentinel total value.
		/// </summary>
		/// <value>
		/// The sentinel total value.
		/// </value>
		public decimal? SentinelTotalValue { get; set; }

		/// <summary>
		/// Gets or sets the color of the sentinel total value.
		/// </summary>
		/// <value>
		/// The color of the sentinel total value.
		/// </value>
		public string SentinelTotalValueColor { get; set; }
	}
}
