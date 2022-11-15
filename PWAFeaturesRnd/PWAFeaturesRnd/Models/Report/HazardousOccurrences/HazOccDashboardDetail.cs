using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// 
    /// </summary>
    public class HazOccDashboardDetail
    {
        /// <summary>
        /// Gets or sets the haz occ dashboard list.
        /// </summary>
        /// <value>
        /// The haz occ dashboard list.
        /// </value>
        public List<HazoccDashboardResponse> HazOccDashboardList { get; set; }

        /// <summary>
        /// Gets or sets the header detail.
        /// </summary>
        /// <value>
        /// The header detail.
        /// </value>
        public HazOccDashboardHeaderDetail HeaderDetail { get; set; }

        /// <summary>
        /// Gets or sets the accident classifications.
        /// </summary>
        /// <value>
        /// The accident classifications.
        /// </value>
        public List<Lookup.Lookup> AccidentClassifications { get; set; }
    }
}
