using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    public class GetLastTwelveMonthSummaryRequest
    {
        /// <summary>
        /// Gets or sets the vessel ids.
        /// </summary>
        /// <value>
        /// The vessel ids.
        /// </value>
        public List<string> VesselIds { get; set; }
        /// <summary>
        /// Gets or sets the report type identifier.
        /// </summary>
        /// <value>
        /// The report type identifier.
        /// </value>
        public string ReportTypeId { get; set; }
        /// <summary>
        /// Gets or sets the months.
        /// </summary>
        /// <value>
        /// The months.
        /// </value>
        public int Months { get; set; }
    }
}
