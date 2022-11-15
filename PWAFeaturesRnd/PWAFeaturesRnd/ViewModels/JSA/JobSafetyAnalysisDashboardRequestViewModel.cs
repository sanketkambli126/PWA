using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    /// <summary>
    /// JobSafetyAnalysisDashboardRequestViewModel
    /// </summary>
    public class JobSafetyAnalysisDashboardRequestViewModel
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public UserMenuItem Item { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

    }
}
