namespace PWAFeaturesRnd.ViewModels.Dashboard
{
    /// <summary>
    /// Available vessel view model
    /// </summary>
    public class AvailableVesselViewModel
    {
        /// <summary>
     /// Gets or sets the vessel identifier.
     /// </summary>
     /// <value>
     /// The vessel identifier.
     /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is vessel mapped.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vessel mapped; otherwise, <c>false</c>.
        /// </value>
        public bool isVesselMapped { get; set; }
    }
}
