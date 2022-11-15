using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.Sentinel;

namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Sentinel Dashboard Detail View Model
    /// </summary>
    public class SentinelDashboardDetailViewModel
    {
        /// <summary>
        /// Gets or sets the vessel detail.
        /// </summary>
        /// <value>
        /// The vessel detail.
        /// </value>
        public SentinelVesselDetailViewModel VesselDetail { get; set; }

        /// <summary>
        /// Gets or sets the vessel current voyage detail.
        /// </summary>
        /// <value>
        /// The vessel current voyage detail.
        /// </value>
        public VesselCurrentVoyageDetailViewModel VesselCurrentVoyageDetail { get; set; }

        /// <summary>
        /// Gets or sets the model value detail.
        /// </summary>
        /// <value>
        /// The model value detail.
        /// </value>
        public List<VesselModelValueDetailViewModel> ModelValueDetail { get; set; }

        /// <summary>
        /// Gets or sets the vessel static factor details.
        /// </summary>
        /// <value>
        /// The vessel static factor details.
        /// </value>
        public List<VesselModelFactorDetailViewModel> VesselStaticFactorDetails { get; set; }
    }
}
