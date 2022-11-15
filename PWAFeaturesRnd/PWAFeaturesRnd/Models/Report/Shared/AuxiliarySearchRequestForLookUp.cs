namespace PWAFeaturesRnd.Models.Report.Shared
{
	/// <summary>
	/// Auxiliary Search Request For LookUp
	/// </summary>
	public class AuxiliarySearchRequestForLookUp
	{
		/// <summary>
		/// Gets or sets the search text.
		/// </summary>
		/// <value>
		/// The search text.
		/// </value>
		public string SearchText { get; set; }

		/// <summary>
		/// Gets or sets the short code.
		/// </summary>
		/// <value>
		/// The short code.
		/// </value>
		public string ShortCode { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is quick search.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is quick search; otherwise, <c>false</c>.
		/// </value>
		public bool IsQuickSearch { get; set; }
	}
}
