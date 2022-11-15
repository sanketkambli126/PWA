using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// 
    /// </summary>
    public class SeaPassageReportRequestViewModel
    {
        /// <summary>
        /// Gets or sets the spa identifier.
        /// </summary>
        /// <value>
        /// The spa identifier.
        /// </value>
        public string SpaId { get; set; }
        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PosId { get; set; }
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string vesselId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is IDL.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is IDL; otherwise, <c>false</c>.
        /// </value>
        public bool IsIdl { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is noon incomplete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is noon incomplete; otherwise, <c>false</c>.
        /// </value>
        public bool? IsNoonIncomplete { get; set; }
    }
}
