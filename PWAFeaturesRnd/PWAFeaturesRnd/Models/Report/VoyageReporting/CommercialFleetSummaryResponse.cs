namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Commercial Fleet Summary Response
	/// </summary>
	public class CommercialFleetSummaryResponse
	{
		/// <summary>
		/// Gets or sets the off hire data.
		/// </summary>
		/// <value>
		/// The off hire data.
		/// </value>
		public string OffHireData { get; set; }

		/// <summary>
		/// Gets or sets the off hire priority.
		/// </summary>
		/// <value>
		/// The off hire priority.
		/// </value>
		public int OffHirePriority { get; set; }

		/// <summary>
		/// Gets or sets the off hire information.
		/// </summary>
		/// <value>
		/// The off hire information.
		/// </value>
		public string OffHireInfo { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency percentage.
		/// </summary>
		/// <value>
		/// The fuel efficiency percentage.
		/// </value>
		public decimal? FuelEfficiencyPercentage { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency priority.
		/// </summary>
		/// <value>
		/// The fuel efficiency priority.
		/// </value>
		public int FuelEfficiencyPriority { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency information.
		/// </summary>
		/// <value>
		/// The fuel efficiency information.
		/// </value>
		public string FuelEfficiencyInfo { get; set; }
	}
}
