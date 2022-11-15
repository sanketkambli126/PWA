using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Inspection Manager Dashboard Detail
    /// </summary>
    public class InspectionManagerDashboardDetail
    {
        /// <summary>
        /// Gets or sets the inspection dashboard list.
        /// </summary>
        /// <value>
        /// The inspection dashboard list.
        /// </value>
        public List<InspectionManagerDashboardResponse> InspectionDashboardList { get; set; }

        /// <summary>
        /// Gets or sets the header statistic detail.
        /// </summary>
        /// <value>
        /// The header statistic detail.
        /// </value>
        public InspectionDashboardHeaderStatisticDetail HeaderStatisticDetail { get; set; }
    }
}
