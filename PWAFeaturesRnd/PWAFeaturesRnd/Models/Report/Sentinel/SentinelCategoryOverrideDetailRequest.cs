namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Category Override Detail Request
    /// </summary>
    public class SentinelCategoryOverrideDetailRequest
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
    }
}
