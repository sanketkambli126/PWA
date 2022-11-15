namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// The near miss event detail view model
    /// </summary>
    public class NearMissEventDetailViewModel
    {
        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        /// <value>
        /// The operation.
        /// </value>
        public string Operation { get; set; }

        /// <summary>
        /// Gets or sets the possible consequence.
        /// </summary>
        /// <value>
        /// The possible consequence.
        /// </value>
        public string PossibleConsequence { get; set; }

        /// <summary>
        /// Gets or sets the immediate action taken.
        /// </summary>
        /// <value>
        /// The immediate action taken.
        /// </value>
        public string ImmediateActionTaken { get; set; }
    }
}
