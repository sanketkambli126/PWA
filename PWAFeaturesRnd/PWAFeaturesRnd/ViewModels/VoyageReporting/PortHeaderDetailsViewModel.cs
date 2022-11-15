namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// Port Header Details ViewModel
	/// </summary>
	public class PortHeaderDetailsViewModel
	{
		/// <summary>
		/// Gets or sets the full name of the port.
		/// </summary>
		/// <value>
		/// The full name of the port.
		/// </value>
		public string PortFullName { get; set; }

		/// <summary>
		/// Gets or sets the name of the country.
		/// </summary>
		/// <value>
		/// The name of the country.
		/// </value>
		public string CountryName { get; set; }

		/// <summary>
		/// Gets or sets the country code.
		/// </summary>
		/// <value>
		/// The country code.
		/// </value>
		public string CountryCode { get; set; }

		/// <summary>
		/// Gets or sets the unlocode.
		/// </summary>
		/// <value>
		/// The unlocode.
		/// </value>
		public string Unlocode { get; set; }

		/// <summary>
		/// Gets or sets the full latitude.
		/// </summary>
		/// <value>
		/// The full latitude.
		/// </value>
		public string FullLatitude { get; set; }

		/// <summary>
		/// Gets or sets the full longitude.
		/// </summary>
		/// <value>
		/// The full longitude.
		/// </value>
		public string FullLongitude { get; set; }

		/// <summary>
		/// Gets or sets the is key hub port.
		/// </summary>
		/// <value>
		/// The is key hub port.
		/// </value>
		public string IsKeyHubPort { get; set; }
	}
}
