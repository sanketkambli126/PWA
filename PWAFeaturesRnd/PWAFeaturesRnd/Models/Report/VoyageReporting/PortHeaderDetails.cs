namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// PortHeaderDetails
	/// </summary>
	public class PortHeaderDetails
	{
		/// <summary>
		/// Gets or sets the port identifier.
		/// </summary>
		/// <value>
		/// The port identifier.
		/// </value>
		public string PortIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the name of the port.
		/// </summary>
		/// <value>
		/// The name of the port.
		/// </value>
		public string PortName { get; set; }

		/// <summary>
		/// Gets or sets the country code.
		/// </summary>
		/// <value>
		/// The country code.
		/// </value>
		public string CountryCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the country.
		/// </summary>
		/// <value>
		/// The name of the country.
		/// </value>
		public string CountryName { get; set; }

		/// <summary>
		/// Gets or sets the un locode.
		/// </summary>
		/// <value>
		/// The un locode.
		/// </value>
		public string UNLocode { get; set; }

		/// <summary>
		/// Gets or sets the lat degree.
		/// </summary>
		/// <value>
		/// The lat degree.
		/// </value>
		public decimal? LatDegree { get; set; }

		/// <summary>
		/// Gets or sets the lat indicator.
		/// </summary>
		/// <value>
		/// The lat indicator.
		/// </value>
		public string LatIndicator { get; set; }

		/// <summary>
		/// Gets or sets the lat minimum.
		/// </summary>
		/// <value>
		/// The lat minimum.
		/// </value>
		public decimal? LatMin { get; set; }

		/// <summary>
		/// Gets or sets the long degree.
		/// </summary>
		/// <value>
		/// The long degree.
		/// </value>
		public decimal? LongDegree { get; set; }

		/// <summary>
		/// Gets or sets the long indicator.
		/// </summary>
		/// <value>
		/// The long indicator.
		/// </value>
		public string LongIndicator { get; set; }

		/// <summary>
		/// Gets or sets the long minimum.
		/// </summary>
		/// <value>
		/// The long minimum.
		/// </value>
		public decimal? LongMin { get; set; }

		/// <summary>
		/// Gets or sets the active.
		/// </summary>
		/// <value>
		/// The active.
		/// </value>
		public bool? Active { get; set; }

		/// <summary>
		/// Gets or sets the is key hub port.
		/// </summary>
		/// <value>
		/// The is key hub port.
		/// </value>
		public bool? IsKeyHubPort { get; set; }
	}
}
