namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// Data Contract for doctor visit.
    /// </summary>
    public class DoctorVisitReportViewModel
    {
        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the MMR identifier.
        /// </summary>
        /// <value>
        /// The MMR identifier.
        /// </value>
        public string MmrId { get; set; }

        /// <summary>
        /// Gets or sets the MMV identifier.
        /// </summary>
        /// <value>
        /// The MMV identifier.
        /// </value>
        public string MmvId { get; set; }

        /// <summary>
        /// Gets or sets the limitation identifier.
        /// </summary>
        /// <value>
        /// The limitation identifier.
        /// </value>
        public string LimitationId { get; set; }

        /// <summary>
        /// Gets or sets the limitation.
        /// </summary>
        /// <value>
        /// The limitation.
        /// </value>
        public string Limitation { get; set; }

        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>
        /// The type identifier.
        /// </value>
        public string TypeId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
    }
}
