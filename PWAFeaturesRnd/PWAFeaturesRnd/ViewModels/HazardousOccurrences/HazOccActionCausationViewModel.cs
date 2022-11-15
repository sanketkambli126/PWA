using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// The hazocc action causation view model
    /// </summary>
    public class HazOccActionCausationViewModel
    {
        /// <summary>
        /// Gets or sets the direct causes.
        /// </summary>
        /// <value>
        /// The direct causes.
        /// </value>
        public List<HazOccCausationViewModel> DirectCauses { get; set; }

        /// <summary>
        /// Gets or sets the root causes.
        /// </summary>
        /// <value>
        /// The root causes.
        /// </value>
        public List<HazOccCausationViewModel> RootCauses { get; set; }
    }
}
