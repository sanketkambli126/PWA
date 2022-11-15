namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	/// <summary>
	/// Inspection Overview Filter Response
	/// </summary>
	public class InspectionOverviewFilterResponse
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public int Value { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is default.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is default; otherwise, <c>false</c>.
		/// </value>
		public bool IsDefault { get; set; }
	}
}
