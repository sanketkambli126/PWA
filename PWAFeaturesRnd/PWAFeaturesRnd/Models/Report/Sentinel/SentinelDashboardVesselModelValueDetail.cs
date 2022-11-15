using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Sentinel Dashboard Vessel Model Value Detail
    /// </summary>
    public class SentinelDashboardVesselModelValueDetail
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the model dimension identifier.
        /// </summary>
        /// <value>
        /// The model dimension identifier.
        /// </value>
        public string ModelDimensionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the model dimension.
        /// </summary>
        /// <value>
        /// The name of the model dimension.
        /// </value>
        public string ModelDimensionName { get; set; }

        /// <summary>
        /// Gets or sets the model value.
        /// </summary>
        /// <value>
        /// The model value.
        /// </value>
        public decimal? ModelValue { get; set; }

        /// <summary>
        /// Gets or sets the color of the model value.
        /// </summary>
        /// <value>
        /// The color of the model value.
        /// </value>
        public string ModelValueColor { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int? SortOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has active override.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has active override; otherwise, <c>false</c>.
        /// </value>
        public bool HasActiveOverride { get; set; }

        /// <summary>
        /// Gets or sets the model dimension value change status.
        /// </summary>
        /// <value>
        /// The model dimension value change status.
        /// </value>
        public int? ModelDimensionValueChangeStatus { get; set; }

        /// <summary>
        /// Gets or sets the model dimension value difference.
        /// </summary>
        /// <value>
        /// The model dimension value difference.
        /// </value>
        public decimal? ModelDimensionValueDifference { get; set; }

        /// <summary>
        /// Gets or sets the latest history date.
        /// </summary>
        /// <value>
        /// The latest history date.
        /// </value>
        public DateTime? LatestHistoryDate { get; set; }

        /// <summary>
        /// Gets or sets the category graph detail.
        /// </summary>
        /// <value>
        /// The category graph detail.
        /// </value>
        public List<VesselSentinelValue> CategoryGraphDetail { get; set; }
    }
}
