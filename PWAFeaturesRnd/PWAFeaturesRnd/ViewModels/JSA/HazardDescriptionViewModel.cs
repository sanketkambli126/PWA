using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    public class HazardDescriptionViewModel
    {
        /// <summary>
        /// To Get or set Hazard Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// To Get or Set the Existing Control Measures
        /// </summary>
        public List<string> ExistingControlMeasures { get; set; }

        /// <summary>
        /// To Get or Set Further control measures
        /// </summary>
        public List<string> FurtherControlMeasures { get; set; }
    }
}
