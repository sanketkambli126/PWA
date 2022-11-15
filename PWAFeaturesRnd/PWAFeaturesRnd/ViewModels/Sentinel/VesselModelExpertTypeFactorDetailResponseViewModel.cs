namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Vessel Model Expert Type Factor Detail Response View Model 
    /// </summary>
    public class VesselModelExpertTypeFactorDetailResponseViewModel
    {
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
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public decimal? Weight { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }
    }
}