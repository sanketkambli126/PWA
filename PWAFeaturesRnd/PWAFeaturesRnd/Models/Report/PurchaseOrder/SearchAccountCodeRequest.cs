namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Search Account Code Request
	/// </summary>
	public class SearchAccountCodeRequest
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the account code.
		/// </summary>
		/// <value>
		/// The account code.
		/// </value>
		public string AccountCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the account.
		/// </summary>
		/// <value>
		/// The name of the account.
		/// </value>
		public string AccountName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is quick search.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is quick search; otherwise, <c>false</c>.
		/// </value>
		public bool IsQuickSearch { get; set; }

		/// <summary>
		/// Gets or sets the accounting company identifier.
		/// </summary>
		/// <value>
		/// The accounting company identifier.
		/// </value>
		public string AccountingCompanyId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is vessel in purchasing management.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is vessel in purchasing management; otherwise, <c>false</c>.
		/// </value>
		public bool IsVesselInPurchasingManagement { get; set; }
	}
}
