namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// BiggestMoverRangeResponseViewModel
    /// </summary>
    public class BiggestMoverRangeResponseViewModel
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
		/// Gets or sets the index of the range.
		/// </summary>
		/// <value>
		/// The index of the range.
		/// </value>
		public int? RangeIndex { get; set; }
	}
}
