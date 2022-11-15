namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Model Dimension Response
    /// </summary>
    public class SentinelModelDimensionResponse
    {
        /// <summary>
        /// Gets or sets the model dimension identifier.
        /// </summary>
        /// <value>
        /// The model dimension identifier.
        /// </value>
        public string ModelDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the model dimension.
        /// </summary>
        /// <value>
        /// The name of the model dimension.
        /// </value>
        public string ModelDimensionName { get; set; }

        /// <summary>
        /// Gets or sets the model dimension level.
        /// </summary>
        /// <value>
        /// The model dimension level.
        /// </value>
        public int? ModelDimensionLevel { get; set; }

        /// <summary>
        /// Gets or sets the model dimension parent.
        /// </summary>
        /// <value>
        /// The model dimension parent.
        /// </value>
        public string ModelDimensionParent { get; set; }

        /// <summary>
        /// Gets or sets the type of the model.
        /// </summary>
        /// <value>
        /// The type of the model.
        /// </value>
        public string ModelType { get; set; }

        /// <summary>
        /// Gets or sets the model type value.
        /// </summary>
        /// <value>
        /// The model type value.
        /// </value>
        public int? ModelTypeValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is override.
        /// </summary>
        /// <value>
        ///  <c>true</c> if this instance is override; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverride { get; set; }
    }
}
