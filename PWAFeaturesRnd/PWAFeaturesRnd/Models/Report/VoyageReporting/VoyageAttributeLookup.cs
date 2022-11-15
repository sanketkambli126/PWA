namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    public class VoyageAttributeLookup
    {
        /// <summary>
        /// Gets or sets the PLK identifier.
        /// </summary>
        /// <value>
        /// The PLK identifier.
        /// </value>
        public string PlkId { get; set; }
        /// <summary>
        /// Gets or sets the lookup code.
        /// </summary>
        /// <value>
        /// The lookup code.
        /// </value>
        public string LookupCode { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the index of the sort.
        /// </summary>
        /// <value>
        /// The index of the sort.
        /// </value>
        public int? SortIndex { get; set; }
    }
}
