namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Category Override Detail Request View Model
    /// </summary>
    public class CategoryOverrideDetailRequestViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }
    }
}
