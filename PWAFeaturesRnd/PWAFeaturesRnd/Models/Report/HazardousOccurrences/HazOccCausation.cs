namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// DataContract for hazocc causation.
    /// </summary>
    public class HazOccCausation
    {
        /// <summary>
        /// Gets or sets the iad identifier.
        /// </summary>
        /// <value>
        /// The iad identifier.
        /// </value>
        public string IadId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the long description.
        /// </summary>
        /// <value>
        /// The long description.
        /// </value>
        public string LongDescription { get; set; }

        /// <summary>
        /// Gets or sets the lookup code.
        /// </summary>
        /// <value>
        /// The lookup code.
        /// </value>
        public string LookupCode { get; set; }
    }
}
