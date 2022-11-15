using System;

namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Sentinel Vessel Value View Model
    /// </summary>
    public class VesselSentinelValueViewModel
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

        /// <summary>
        /// Gets or sets the sentinel total value.
        /// </summary>
        /// <value>
        /// The sentinel total value.
        /// </value>
        public decimal? SentinelTotalValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the sentinel total value.
        /// </summary>
        /// <value>
        /// The color of the sentinel total value.
        /// </value>
        public string SentinelTotalValueColor { get; set; }

        /// <summary>
        /// Gets or sets the stat date.
        /// </summary>
        /// <value>
        /// The stat date.
        /// </value>
        public DateTime? StatDate { get; set; }

    }
}
