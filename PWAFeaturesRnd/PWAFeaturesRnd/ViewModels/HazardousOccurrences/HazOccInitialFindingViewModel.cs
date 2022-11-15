namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// Data Contract for hazocc initial findings.
    /// </summary>
    public class HazOccInitialFindingViewModel
    {
        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the analysis.
        /// </summary>
        /// <value>
        /// The analysis.
        /// </value>
        public string Analysis { get; set; }

        /// <summary>
        /// Gets or sets the risk.
        /// </summary>
        /// <value>
        /// The risk.
        /// </value>
        public string Risk { get; set; }
    }
}
