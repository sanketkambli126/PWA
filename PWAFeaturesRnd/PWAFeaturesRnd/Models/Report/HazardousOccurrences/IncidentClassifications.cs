namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// The IncidentClassifications
    /// </summary>
    public class IncidentClassifications
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the severity identifier.
        /// </summary>
        /// <value>
        /// The severity identifier.
        /// </value>
        public string SeverityId { get; set; }

        /// <summary>
        /// Gets or sets the short code.
        /// </summary>
        /// <value>
        /// The short code.
        /// </value>
        public string ShortCode { get; set; }

        /// <summary>
        /// Gets or sets the long description.
        /// </summary>
        /// <value>
        /// The long description.
        /// </value>
        public string LongDescription { get; set; }
    }
}
