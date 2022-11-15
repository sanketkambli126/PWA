namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    public class HazOccDetailRequestViewModel
    {
        /// <summary>
        /// Gets or sets the encrypted incident identifier.
        /// </summary>
        /// <value>
        /// The encrypted incident identifier.
        /// </value>
        public string EncryptedIncidentId { get; set; }

        /// <summary>
        /// Gets or sets the type of the vessel.
        /// </summary>
        /// <value>
        /// The type of the vessel.
        /// </value>
        public string VesselType { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public string CategoryId { get; set; }
    }
}
