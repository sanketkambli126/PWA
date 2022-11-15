using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// The HazOccInvestigationFindingViewModel
    /// </summary>
    public class HazOccInvestigationFindingViewModel
    {
        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the analysis.
        /// </summary>
        /// <value>
        /// The analysis.
        /// </value>
        public string Analysis { get; set; }

        /// <summary>
        /// Gets or sets the risk.
        /// </summary>
        /// <value>
        /// The risk.
        /// </value>
        public string Risk { get; set; }
    }
}
