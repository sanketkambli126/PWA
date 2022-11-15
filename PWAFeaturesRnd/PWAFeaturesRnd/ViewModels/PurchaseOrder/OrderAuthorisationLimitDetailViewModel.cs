namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// ViewModel for OrderAuthorisationLimit
	/// </summary>
	public class OrderAuthorisationLimitDetailViewModel
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
		/// Gets or sets the authentication level1 limit.
		/// </summary>
		/// <value>
		/// The authentication level1 limit.
		/// </value>
		public decimal? AuthLevel1Limit { get; set; }

		/// <summary>
		/// Gets or sets the client limit.
		/// </summary>
		/// <value>
		/// The client limit.
		/// </value>
		public decimal? ClientLimit { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is above vessel limit.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is above vessel limit; otherwise, <c>false</c>.
		/// </value>
		public bool IsAboveVesselLimit { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is above office limit.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is above office limit; otherwise, <c>false</c>.
		/// </value>
		public bool IsAboveOfficeLimit { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is above authentication level1 limit.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is above authentication level1 limit; otherwise, <c>false</c>.
		/// </value>
		public bool IsAboveAuthLevel1Limit { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is above client limit.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is above client limit; otherwise, <c>false</c>.
		/// </value>
		public bool IsAboveClientLimit { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is contracted account.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is contracted account; otherwise, <c>false</c>.
		/// </value>
		public bool IsContractedAccount { get; set; }

		/// <summary>
		/// Gets or sets the limit currency.
		/// </summary>
		/// <value>
		/// The limit currency.
		/// </value>
		public string LimitCurrency { get; set; }
	}
}
