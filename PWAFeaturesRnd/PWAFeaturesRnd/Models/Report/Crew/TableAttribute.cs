namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// Used for fetching the CovidVaccineTypeDocList
    /// </summary>
    public class TableAttribute
    {

        /// <summary>
        /// Gets or sets the name of the attribute.
        /// </summary>
        /// <value>
        /// The name of the attribute.
        /// </value>
        public string AttributeName { get; set; }

        /// <summary>
        /// Gets or sets the attribute description.
        /// </summary>
        /// <value>
        /// The attribute description.
        /// </value>
        public string AttributeDescription { get; set; }

        /// <summary>
        /// Gets or sets the bit value.
        /// </summary>
        /// <value>
        /// The bit value.
        /// </value>
        public long BitValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; }
    }
}
