namespace PWAFeaturesRnd.Models.Report.Sentinel
{
	/// <summary>
	/// BiggestMoverRangeResponse
	/// </summary>
	public class BiggestMoverRangeResponse
    {
		/// <summary>
		/// Gets or sets the type of the comparison.
		/// </summary>
		/// <value>
		/// The type of the comparison.
		/// </value>
		public string ComparisonType { get; set; }

		/// <summary>
		/// Gets or sets the display range.
		/// </summary>
		/// <value>
		/// The display range.
		/// </value>
		public string DisplayRange { get; set; }

		/// <summary>
		/// Gets or sets from range.
		/// </summary>
		/// <value>
		/// From range.
		/// </value>
		public decimal? FromRange { get; set; }

		/// <summary>
		/// Gets or sets the index of the range.
		/// </summary>
		/// <value>
		/// The index of the range.
		/// </value>
		public int? RangeIndex { get; set; }

		/// <summary>
		/// Gets or sets the sort order.
		/// </summary>
		/// <value>
		/// The sort order.
		/// </value>
		public int? SortOrder { get; set; }

		/// <summary>
		/// Converts to range.
		/// </summary>
		/// <value>
		/// To range.
		/// </value>
		public decimal? ToRange { get; set; }
	}
}
