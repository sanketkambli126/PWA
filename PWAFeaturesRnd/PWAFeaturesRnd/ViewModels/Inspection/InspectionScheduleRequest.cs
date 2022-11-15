namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// Model for Inspection Schedule Request
    /// </summary>
    public class InspectionScheduleRequest
    {
        /// <summary>
        /// Gets or sets the inspection identifier.
        /// </summary>
        /// <value>
        /// The inspection identifier.
        /// </value>
        public string InspectionId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }
    }
}
