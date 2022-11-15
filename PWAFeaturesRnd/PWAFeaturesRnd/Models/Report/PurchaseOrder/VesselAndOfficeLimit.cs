namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Vessel And Office Limit
	/// </summary>
	public class VesselAndOfficeLimit
	{
		/// <summary>
		/// Gets or sets the vessel limit.
		/// </summary>
		/// <value>
		/// The vessel limit.
		/// </value>
		public decimal? VesselLimit { get; set; }

		/// <summary>
		/// Gets or sets the office limit.
		/// </summary>
		/// <value>
		/// The office limit.
		/// </value>
		public decimal? OfficeLimit { get; set; }

		/// <summary>
		/// Gets or sets the level1 limit.
		/// </summary>
		/// <value>
		/// The level1 limit.
		/// </value>
		public decimal? Level1Limit { get; set; }

		/// <summary>
		/// Gets or sets the level2 limit.
		/// </summary>
		/// <value>
		/// The level2 limit.
		/// </value>
		public decimal? Level2Limit { get; set; }

		/// <summary>
		/// Gets or sets the level3 limit.
		/// </summary>
		/// <value>
		/// The level3 limit.
		/// </value>
		public decimal? Level3Limit { get; set; }

		/// <summary>
		/// Gets or sets the client limit.
		/// </summary>
		/// <value>
		/// The client limit.
		/// </value>
		public decimal? ClientLimit { get; set; }
	}
}
