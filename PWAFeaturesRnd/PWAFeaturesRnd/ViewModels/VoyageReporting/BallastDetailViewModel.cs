namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    public class BallastDetailViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is eosp faop mode enable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is eosp faop mode enable; otherwise, <c>false</c>.
        /// </value>
        public bool IsEospFaopModeEnable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is consumption mode enable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is consumption mode enable; otherwise, <c>false</c>.
        /// </value>
        public bool IsConsumptionModeEnable { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the rob details.
        /// </summary>
        /// <value>
        /// The rob details.
        /// </value>
        public PortEventAttributeDetailViewModel RobDetails { get; set; }
    }
}
