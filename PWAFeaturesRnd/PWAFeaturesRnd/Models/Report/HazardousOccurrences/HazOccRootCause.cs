using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// DataContract for haocc rootcauses.
    /// </summary>
    public class HazOccRootCause
    {
        /// <summary>
        /// Gets or sets the root causes.
        /// </summary>
        /// <value>
        /// The root causes.
        /// </value>
        public List<HazOccQuestions> RootCauses { get; set; }
    }
}
