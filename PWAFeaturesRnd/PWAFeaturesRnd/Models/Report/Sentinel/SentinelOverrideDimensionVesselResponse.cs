namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Override Dimension Vessel Response
    /// </summary>
    public class SentinelOverrideDimensionVesselResponse
    {
        /// <summary>
        /// Gets or sets the parent model dimension identifier.
        /// </summary>
        /// <value>
        /// The parent model dimension identifier.
        /// </value>
        public string ParentModelDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the parent model dimension description.
        /// </summary>
        /// <value>
        /// The parent model dimension description.
        /// </value>
        public string ParentModelDimensionDescription { get; set; }

        /// <summary>
        /// Gets or sets the model dimension identifier.
        /// </summary>
        /// <value>
        /// The model dimension identifier.
        /// </value>
        public string ModelDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the model dimension description.
        /// </summary>
        /// <value>
        /// The model dimension description.
        /// </value>
        public string ModelDimensionDescription { get; set; }

        /// <summary>
        /// Gets or sets the override dimension identifier.
        /// </summary>
        /// <value>
        /// The override dimension identifier.
        /// </value>
        public string OverrideDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the override dimension.
        /// </summary>
        /// <value>
        /// The override dimension.
        /// </value>
        public string OverrideDimension { get; set; }

        /// <summary>
        /// Gets or sets the vessel count.
        /// </summary>
        /// <value>
        /// The vessel count.
        /// </value>
        public int VesselCount { get; set; }
    }
}
