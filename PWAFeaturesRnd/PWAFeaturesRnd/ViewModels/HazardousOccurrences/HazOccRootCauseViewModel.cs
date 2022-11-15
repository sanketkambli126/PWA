using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// DataContract for haocc rootcauses view model.
    /// </summary>
    public class HazOccRootCauseViewModel
    {
        /// <summary>
        /// Gets or sets the root causes.
        /// </summary>
        /// <value>
        /// The root causes.
        /// </value>
        public List<HazOccQuestionsViewModel> RootCauses { get; set; }
    }
}
