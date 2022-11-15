namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// 
	/// </summary>
	public class AccountLookupWrapperViewModel
    {
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public string Identifier { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets the chart account display.
		/// </summary>
		/// <value>
		/// The chart account display.
		/// </value>
		public string Text
		{
			get
			{
				return Description;
			}
		}

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public string Id
		{
			get
			{
				return Identifier;
			}
		}
	}
}
