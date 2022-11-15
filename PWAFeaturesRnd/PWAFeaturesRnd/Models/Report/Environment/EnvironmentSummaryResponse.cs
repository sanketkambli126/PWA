namespace PWAFeaturesRnd.Models.Report.Environment
{
    /// <summary>
    /// Environment Summary Response
    /// </summary>
    public class EnvironmentSummaryResponse
    {
        /// <summary>
        /// Gets or sets the eeoi.
        /// </summary>
        /// <value>
        /// The eeoi.
        /// </value>
        public decimal EEOI { get; set; }

        /// <summary>
        /// Gets or sets the accidental oil spills count.
        /// </summary>
        /// <value>
        /// The accidental oil spills count.
        /// </value>
        public int AccidentalOilSpillsCount { get; set; }
       
        /// <summary>
        /// Gets or sets the oil bilge retention.
        /// </summary>
        /// <value>
        /// The oil bilge retention.
        /// </value>
        public decimal OilBilgeRetention { get; set; }

        /// <summary>
        /// Gets or sets the ae utilisation.
        /// </summary>
        /// <value>
        /// The ae utilisation.
        /// </value>
        public decimal AEUtilisation { get; set; }

    }
}
