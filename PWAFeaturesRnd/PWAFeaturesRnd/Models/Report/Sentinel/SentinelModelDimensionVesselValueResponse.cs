using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Model Dimension Vessel Value Response
    /// </summary>
    public class SentinelModelDimensionVesselValueResponse
    {
        /// <summary>
        /// Gets or sets the office identifier.
        /// </summary>
        /// <value>
        /// The office identifier.
        /// </value>
        public string OfficeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the office.
        /// </summary>
        /// <value>
        /// The name of the office.
        /// </value>
        public string OfficeName { get; set; }

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
        /// Gets or sets the model dimension value.
        /// </summary>
        /// <value>
        /// The model dimension value.
        /// </value>
        public decimal? ModelDimensionValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the model dimension value.
        /// </summary>
        /// <value>
        /// The color of the model dimension value.
        /// </value>
        public string ModelDimensionValueColor { get; set; }

        /// <summary>
        /// Gets or sets the model dimension value change status.
        /// </summary>
        /// <value>
        /// The model dimension value change status.
        /// </value>
        public int? ModelDimensionValueChangeStatus { get; set; }

        /// <summary>
        /// Gets or sets the biggest movers value.
        /// </summary>
        /// <value>
        /// The biggest movers value.
        /// </value>
        public string BiggestMoversValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has active override.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has active override; otherwise, <c>false</c>.
        /// </value>
        public bool HasActiveOverride { get; set; }

        /// <summary>
        /// Gets or sets the vessel model combined value.
        /// </summary>
        /// <value>
        /// The vessel model combined value.
        /// </value>
        public decimal? VesselModelCombinedValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the vessel model combined.
        /// </summary>
        /// <value>
        /// The color of the vessel model combined.
        /// </value>
        public string VesselModelCombinedColor { get; set; }

        /// <summary>
        /// Gets or sets the vessel current voyage detail.
        /// </summary>
        /// <value>
        /// The vessel current voyage detail.
        /// </value>
        public List<SentinelDashboardVesselCurrentVoyageDetail> VesselCurrentVoyageDetail { get; set; }
    }
}
