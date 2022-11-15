namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Vessel Model Factor Detail Response
    /// </summary>
    public class SentinelVesselModelFactorDetailResponse
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the model dimension identifier.
        /// </summary>
        /// <value>
        /// The model dimension identifier.
        /// </value>
        public string ModelDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the factor.
        /// </summary>
        /// <value>
        /// The name of the factor.
        /// </value>
        public string FactorName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [actionable factor].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [actionable factor]; otherwise, <c>false</c>.
        /// </value>
        public bool ActionableFactor { get; set; }

        /// <summary>
        /// Gets or sets the factor value numeric.
        /// </summary>
        /// <value>
        /// The factor value numeric.
        /// </value>
        public decimal? FactorValueNumeric { get; set; }

        /// <summary>
        /// Gets or sets the factor value text.
        /// </summary>
        /// <value>
        /// The factor value text.
        /// </value>
        public string FactorValueText { get; set; }

        /// <summary>
        /// Gets or sets the factor rank.
        /// </summary>
        /// <value>
        /// The factor rank.
        /// </value>
        public int? FactorRank { get; set; }

        /// <summary>
        /// Gets or sets the name of the navigation link.
        /// </summary>
        /// <value>
        /// The name of the navigation link.
        /// </value>
        public string NavigationLinkName { get; set; }

        /// <summary>
        /// Gets or sets the name of the module.
        /// </summary>
        /// <value>
        /// The name of the module.
        /// </value>
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        /// <value>
        /// The name of the view.
        /// </value>
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the type of the navigation.
        /// </summary>
        /// <value>
        /// The type of the navigation.
        /// </value>
        public string NavigationType { get; set; }

        /// <summary>
        /// Gets or sets the navigation filter.
        /// </summary>
        /// <value>
        /// The navigation filter.
        /// </value>
        public string NavigationFilter { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }
    }
}
