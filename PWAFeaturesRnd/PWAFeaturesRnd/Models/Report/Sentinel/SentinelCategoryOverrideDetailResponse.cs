namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Category Override Detail Response
    /// </summary>
    public class SentinelCategoryOverrideDetailResponse
    {
        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>

        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the override dimension identifier.
        /// </summary>
        /// <value>
        /// The override dimension identifier.
        /// </value>
        public string OverrideDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the override dimension.
        /// </summary>
        /// <value>
        /// The name of the override dimension.
        /// </value>
        public string OverrideDimensionName { get; set; }

        /// <summary>
        /// Gets or sets the override dimension current value.
        /// </summary>
        /// <value>
        /// The override dimension current value.
        /// </value>
        public decimal? OverrideDimensionCurrentValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the override dimension current value.
        /// </summary>
        /// <value>
        /// The color of the override dimension current value.
        /// </value>
        public string OverrideDimensionCurrentValueColor { get; set; }

        /// <summary>
        /// Gets or sets the row number.
        /// </summary>
        /// <value>
        /// The row number.
        /// </value>
        public int? RowNumber { get; set; }
    }
}
