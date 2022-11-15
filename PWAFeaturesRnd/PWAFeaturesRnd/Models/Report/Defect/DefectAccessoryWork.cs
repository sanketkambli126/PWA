namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectAccessoryWork
    {
        /// <summary>
        /// Gets or sets the daw identifier.
        /// </summary>
        /// <value>
        /// The daw identifier.
        /// </value>

        public string DawId { get; set; }

        /// <summary>
        /// Gets or sets the dwo identifier.
        /// </summary>
        /// <value>
        /// The dwo identifier.
        /// </value>
        public string DwoId { get; set; }

        /// <summary>
        /// Gets or sets the dal identifier.
        /// </summary>
        /// <value>
        /// The dal identifier.
        /// </value>
        public string DalId { get; set; }

        /// <summary>
        /// Gets or sets the is deleted.
        /// </summary>
        /// <value>
        /// The is deleted.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the specific value.
        /// </summary>
        /// <value>
        /// The specific value.
        /// </value>
        public string SpecificValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the dal.
        /// </summary>
        /// <value>
        /// The name of the dal.
        /// </value>
        public string DalName { get; set; }
    }
}
