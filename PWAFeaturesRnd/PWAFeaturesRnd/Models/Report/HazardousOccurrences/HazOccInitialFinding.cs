namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Data Contract for hazocc initial findings.
    /// </summary>
    public class HazOccInitialFinding
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
