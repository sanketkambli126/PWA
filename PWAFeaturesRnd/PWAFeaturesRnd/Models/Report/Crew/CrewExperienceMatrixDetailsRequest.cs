using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// Crew Experience Matrix Details Request
    /// </summary>
    public class CrewExperienceMatrixDetailsRequest
    {
        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>
        public string FleetId { get; set; }

        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public string MenuType { get; set; }

        /// <summary>
        /// Gets or sets the vessel ids.
        /// </summary>
        /// <value>
        /// The vessel ids.
        /// </value>
        public List<string> VesselIds { get; set; }
    }
}
