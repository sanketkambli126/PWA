namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Vessel Model Factor Detail Response View Model
    /// </summary>
    public class VesselModelFactorDetailResponseViewModel
    {
        /// <summary>
        /// Gets or sets the name of the factor.
        /// </summary>
        /// <value>
        /// The name of the factor.
        /// </value>
        public string FactorName { get; set; }

        /// <summary>
        /// Gets or sets the factor value numeric.
        /// </summary>
        /// <value>
        /// The factor value numeric.
        /// </value>
        public decimal? FactorValueNumeric { get; set; }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the name of the navigation link.
        /// </summary>
        /// <value>
        /// The name of the navigation link.
        /// </value>
        public string NavigationLinkName { get; set; }
    }
}