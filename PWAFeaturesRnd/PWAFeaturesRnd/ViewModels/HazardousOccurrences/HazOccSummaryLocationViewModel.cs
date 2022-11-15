using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// 
    /// </summary>
    public class HazOccSummaryLocationViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether [show port].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show port]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show maneuvering].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show maneuvering]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowManeuvering { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show location name].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show location name]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowLocationName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show long lat].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show long lat]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowLongLat { get; set; }

        /// <summary>
        /// Gets or sets the location lookup label.
        /// </summary>
        /// <value>
        /// The location lookup label.
        /// </value>
        public string LocationLookupLabel { get; set; }
    }
}
