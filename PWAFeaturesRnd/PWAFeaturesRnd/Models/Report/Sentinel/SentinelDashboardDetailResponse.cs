using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Dashboard Detail Response
    /// </summary>
    public class SentinelDashboardDetailResponse
    {
        /// <summary>
        /// Gets or sets the vessel detail.
        /// </summary>
        /// <value>
        /// The vessel detail.
        /// </value>
        public List<SentinelDashboardVesselDetail> VesselDetail { get; set; }

        /// <summary>
        /// Gets or sets the vessel current voyage detail.
        /// </summary>
        /// <value>
        /// The vessel current voyage detail.
        /// </value>
        public List<SentinelDashboardVesselCurrentVoyageDetail> VesselCurrentVoyageDetail { get; set; }

        /// <summary>
        /// Gets or sets the model value detail.
        /// </summary>
        /// <value>
        /// The model value detail.
        /// </value>
        public List<SentinelDashboardVesselModelValueDetail> ModelValueDetail { get; set; }

        /// <summary>
        /// Gets or sets the vessel static factor details.
        /// </summary>
        /// <value>
        /// The vessel static factor details.
        /// </value>
        public List<SentinelVesselModelFactorDetailResponse> VesselStaticFactorDetails { get; set; }
    }
}
