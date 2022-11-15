namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// 
    /// </summary>
    public class VoyageDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the remaining value.
        /// </summary>
        /// <value>
        /// The remaining value.
        /// </value>
        public decimal RemainingValue { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        /// <value>
        /// The total distance.
        /// </value>
        public decimal TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the distance travelled.
        /// </summary>
        /// <value>
        /// The distance travelled.
        /// </value>
        public decimal DistanceTravelled { get; set; }

        /// <summary>
        /// Gets or sets the last event position.
        /// </summary>
        /// <value>
        /// The last event position.
        /// </value>
        public string LastEventPosition { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sea passage event.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sea passage event; otherwise, <c>false</c>.
        /// </value>
        public bool IsSeaPassageEvent { get; set; }
    }
}
