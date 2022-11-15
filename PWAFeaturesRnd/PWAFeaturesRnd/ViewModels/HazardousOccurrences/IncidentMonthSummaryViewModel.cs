using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.HazardousOccurrences;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// IncidentMonthSummaryViewModel
    /// </summary>
    public class IncidentMonthSummaryViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the report type identifier.
        /// </summary>
        /// <value>
        /// The report type identifier.
        /// </value>
        public string ReportTypeId { get; set; }

        /// <summary>
        /// Gets or sets the month total.
        /// </summary>
        /// <value>
        /// The month total.
        /// </value>
        public List<IncidentMonthTotal> MonthTotal { get; set; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public int MaxValue { get; set; }
    }
}
