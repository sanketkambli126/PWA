using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Crew
{
    /// <summary>
    ///   <br />ViewModel used for holding the crewId on the page
    /// </summary>
    public class CrewDetailsViewModel : BaseViewModel
    {
        /// <summary>Gets or sets the crew identifier.</summary>
        /// <value>The crew identifier.</value>
        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }

        /// <summary>
		/// Gets or sets the active mobile tab class.
		/// </summary>
		/// <value>
		/// The active mobile tab class.
		/// </value>
		public string ActiveMobileTabClass { get; set; }

    }
}
