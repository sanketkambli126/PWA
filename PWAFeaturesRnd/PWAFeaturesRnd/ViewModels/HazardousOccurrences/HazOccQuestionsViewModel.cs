namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// 
    /// </summary>
    public class HazOccQuestionsViewModel
    {
        /// <summary>
        /// Gets or sets the iad identifier.
        /// </summary>
        /// <value>
        /// The iad identifier.
        /// </value>        
        public string IadId { get; set; }
    
        /// <summary>
        /// Gets or sets the long description.
        /// </summary>
        /// <value>
        /// The long description.
        /// </value>        
        public string LongDescription { get; set; }
    
        /// <summary>
        /// Gets or sets the parent iad identifier.
        /// </summary>
        /// <value>
        /// The parent iad identifier.
        /// </value>        
        public string ParentIadId { get; set; }

        /// <summary>
        /// Gets or sets the name of the sub standard.
        /// </summary>
        /// <value>
        /// The name of the sub standard.
        /// </value>
        public string SubStandardName { get; set; }
    }
}
