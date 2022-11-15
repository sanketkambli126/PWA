namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Fuel Efficiency Details Response
	/// </summary>
	public class FuelEfficiencyDetailsResponse
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
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

		/// <summary>
		/// Gets or sets the sea passage days.
		/// </summary>
		/// <value>
		/// The sea passage days.
		/// </value>
		public int? SeaPassageDays { get; set; }

		/// <summary>
		/// Gets or sets the actual fuel.
		/// </summary>
		/// <value>
		/// The actual fuel.
		/// </value>
		public decimal? ActualFuel { get; set; }

		/// <summary>
		/// Gets or sets the charter party order fuel.
		/// </summary>
		/// <value>
		/// The charter party order fuel.
		/// </value>
		public decimal? CharterPartyOrderFuel { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency ratio.
		/// </summary>
		/// <value>
		/// The fuel efficiency ratio.
		/// </value>
		public decimal? FuelEfficiencyRatio { get; set; }
	}
}
