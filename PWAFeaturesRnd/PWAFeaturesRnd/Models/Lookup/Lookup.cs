namespace PWAFeaturesRnd.Models.Lookup
{
	/// <summary>
	/// Lookup
	/// </summary>
	public class Lookup
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
		/// Gets or sets the long description.
		/// </summary>
		/// <value>
		/// The long description.
		/// </value>
		public string LongDescription { get; set; }

		/// <summary>
		/// Gets or sets the lookup code.
		/// </summary>
		/// <value>
		/// The lookup code.
		/// </value>
		public string LookupCode { get; set; }

		/// <summary>
		/// Gets or sets the sort order.
		/// </summary>
		/// <value>
		/// The sort order.
		/// </value>
		public decimal SortOrder { get; set; }

		/// <summary>
		/// Converts to string.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "Idenitifier:" + this.Identifier + "-Description:" + this.Description;
		}
	}
}
