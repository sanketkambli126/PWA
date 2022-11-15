namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectSpecification
    {
        /// <summary>
        /// Gets or sets the DWJ identifier.
        /// </summary>
        /// <value>
        /// The DWJ identifier.
        /// </value>
        public string DwjId { get; set; }

        /// <summary>
        /// Gets or sets the dwo identifier.
        /// </summary>
        /// <value>
        /// The dwo identifier.
        /// </value>
        public string DwoId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the is deleted.
        /// </summary>
        /// <value>
        /// The is deleted.
        /// </value>
        public bool IsDeleted { get; set; }
    }
}
