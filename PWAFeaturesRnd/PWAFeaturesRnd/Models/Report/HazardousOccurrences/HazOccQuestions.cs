namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// DataContract for Hazocc questions.
    /// </summary>
    public class HazOccQuestions
    {
        /// <summary>
        /// Gets or sets the iad identifier.
        /// </summary>
        /// <value>
        /// The iad identifier.
        /// </value>        
        public string IadId { get; set; }

        /// <summary>
        /// Gets or sets the idc identifier.
        /// </summary>
        /// <value>
        /// The idc identifier.
        /// </value>        
        public string IdcId { get; set; }

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

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>        
        public int? Value { get; set; }

        /// <summary>
        /// Gets or sets the parent iad identifier.
        /// </summary>
        /// <value>
        /// The parent iad identifier.
        /// </value>        
        public string ParentIadId { get; set; }
    }
}
