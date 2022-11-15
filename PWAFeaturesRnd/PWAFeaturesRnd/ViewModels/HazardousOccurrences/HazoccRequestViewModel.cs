using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// 
    /// </summary>
    public class HazoccRequestViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the hazocc dashboard.
        /// </summary>
        /// <value>
        /// The type of the hazocc dashboard.
        /// </value>
        public HazoccDashboardType HazoccDashboardType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is summary clicked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is summary clicked; otherwise, <c>false</c>.
        /// </value>
        public bool IsSummaryClicked { get; set; }
    }
}