using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// VesselHeaderPartialViewModel
    /// </summary>
    public class VesselHeaderPartialViewModel
    {
        /// <summary>
        /// Gets or sets the vessel URL.
        /// </summary>
        /// <value>
        /// The vessel URL.
        /// </value>
        public string VesselUrl { get; set; }
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }
        /// <summary>
        /// Gets or sets the drop down identifier.
        /// </summary>
        /// <value>
        /// The drop down identifier.
        /// </value>
        public string DropDownId { get; set; }

    }
}
