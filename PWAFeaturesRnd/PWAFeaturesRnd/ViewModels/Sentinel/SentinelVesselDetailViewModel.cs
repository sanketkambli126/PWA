using System;

namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Sentinel Vessel Detail ViewModel
    /// </summary>
    public class SentinelVesselDetailViewModel
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
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the vessel model total value.
        /// </summary>
        /// <value>
        /// The vessel model total value.
        /// </value>
        public decimal? VesselModelTotalValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the vessel model total value.
        /// </summary>
        /// <value>
        /// The color of the vessel model total value.
        /// </value>
        public string VesselModelTotalValueColor { get; set; }

        /// <summary>
        /// Gets or sets the drydock period end date.
        /// </summary>
        /// <value>
        /// The drydock period end date.
        /// </value>
        public string DrydockPeriodEndDate { get; set; }

        /// <summary>
        /// Gets or sets the vessel flag.
        /// </summary>
        /// <value>
        /// The vessel flag.
        /// </value>
        public string VesselFlag { get; set; }
    }
}
