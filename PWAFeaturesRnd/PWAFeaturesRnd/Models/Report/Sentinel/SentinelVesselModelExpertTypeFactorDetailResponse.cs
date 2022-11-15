namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Vessel Model Expert Type Factor Detail Response
    /// </summary>
    public class SentinelVesselModelExpertTypeFactorDetailResponse
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
        /// Gets or sets the indicator.
        /// </summary>
        /// <value>
        /// The indicator.
        /// </value>
        public string Indicator { get; set; }

        /// <summary>
        /// Gets or sets the indicator value.
        /// </summary>
        /// <value>
        /// The indicator value.
        /// </value>
        public decimal? IndicatorValue { get; set; }

        /// <summary>
        /// Gets or sets the indicator value text.
        /// </summary>
        /// <value>
        /// The indicator value text.
        /// </value>
        public string IndicatorValueText { get; set; }

        /// <summary>
        /// Gets or sets the color of the indicator.
        /// </summary>
        /// <value>
        /// The color of the indicator.
        /// </value>
        public string IndicatorColor { get; set; }

        /// <summary>
        /// Gets or sets the threshold value.
        /// </summary>
        /// <value>
        /// The threshold value.
        /// </value>
        public decimal? ThresholdValue { get; set; }

        /// <summary>
        /// Gets or sets the threshold value text.
        /// </summary>
        /// <value>
        /// The threshold value text.
        /// </value>
        public string ThresholdValueText { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public decimal? Weight { get; set; }

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
