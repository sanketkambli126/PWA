namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// The near miss event detail
    /// </summary>
    public class NearMissEventDetail
    {
        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the operation identifier.
        /// </summary>
        /// <value>
        /// The operation identifier.
        /// </value>
        public string OperationId { get; set; }

        /// <summary>
        /// Gets or sets the possible consequence identifier.
        /// </summary>
        /// <value>
        /// The possible consequence identifier.
        /// </value>
        public string PossibleConsequenceId { get; set; }

        /// <summary>
        /// Gets or sets the immediate action taken.
        /// </summary>
        /// <value>
        /// The immediate action taken.
        /// </value>
        public string ImmediateActionTaken { get; set; }
    }
}
