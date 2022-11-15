namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Category Override Detail Response View Model
    /// </summary>
    public class CategoryOverrideDetailResponseViewModel
    {
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
    }
}