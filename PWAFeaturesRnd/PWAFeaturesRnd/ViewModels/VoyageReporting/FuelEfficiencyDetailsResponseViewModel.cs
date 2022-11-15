namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// Fuel Efficiency Details Response View Model
	/// </summary>
	public class FuelEfficiencyDetailsResponseViewModel
	{
		/// <summary>
		/// Gets or sets the encrypted vessel identifier.
		/// </summary>
		/// <value>
		/// The encrypted vessel identifier.
		/// </value>
		public string EncryptedVesselId { get; set; }

		/// <summary>
		/// Gets or sets the encrypted fuel efficiency URL.
		/// </summary>
		/// <value>
		/// The encrypted fuel efficiency URL.
		/// </value>
		public string EncryptedFuelEfficiencyURL { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency ratio.
		/// </summary>
		/// <value>
		/// The fuel efficiency ratio.
		/// </value>
		public decimal? FuelEfficiencyRatio { get; set; }
	}
}
