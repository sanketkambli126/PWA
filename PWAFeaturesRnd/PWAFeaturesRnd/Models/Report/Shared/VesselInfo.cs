namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Vessel Info
    /// </summary>
    public class VesselInfo
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
        /// Gets or sets the imo number.
        /// </summary>
        /// <value>
        /// The imo number.
        /// </value>
        public string IMOnumber { get; set; }

        /// <summary>
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }
    }
}
