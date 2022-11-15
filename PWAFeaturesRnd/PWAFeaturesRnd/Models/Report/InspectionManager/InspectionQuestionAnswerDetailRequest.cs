namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Inspection Question Answer Detail Request
    /// </summary>
    public class InspectionQuestionAnswerDetailRequest
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the VRP identifier.
        /// </summary>
        /// <value>
        /// The VRP identifier.
        /// </value>
        public string VrpId { get; set; }

        /// <summary>
        /// Gets or sets the VST identifier.
        /// </summary>
        /// <value>
        /// The VST identifier.
        /// </value>
        public string VstId { get; set; }
    }
}
