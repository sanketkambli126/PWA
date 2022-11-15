namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Value Difference Range Response View Model
    /// </summary>
    public class ValueDifferenceRangeResponseViewModel
    { 
        /// <summary>
        /// Gets or sets the display range.
        /// </summary>
        /// <value>
        /// The display range.
        /// </value>
        public string DisplayRange { get; set; }

        /// <summary>
        /// Gets or sets the type of the comparison.
        /// </summary>
        /// <value>
        /// The type of the comparison.
        /// </value>
        public string ComparisonType { get; set; }

        /// <summary>
        /// Gets or sets the vessel count.
        /// </summary>
        /// <value>
        /// The vessel count.
        /// </value>
        public int VesselCount { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel list request.
        /// </summary>
        /// <value>
        /// The encrypted vessel list request.
        /// </value>
        public string EncryptedVesselListRequest { get; set; }

        /// <summary>
        /// Gets or sets the fleet request.
        /// </summary>
        /// <value>
        /// The fleet request.
        /// </value>
        public string FleetRequest { get; set; }
    }
}
