namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectionClosureActionCheckViewModel
    {
        /// <summary>
        /// Gets or sets the encrypted inspection identifier.
        /// </summary>
        /// <value>
        /// The encrypted inspection identifier.
        /// </value>
        public string EncryptedInspectionId { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the inspection type identifier.
        /// </summary>
        /// <value>
        /// The inspection type identifier.
        /// </value>
        public string InspectionTypeId { get; set; }
    }
}
