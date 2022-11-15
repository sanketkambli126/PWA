using System;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// Oil Spill Water Request View Model
    /// </summary>
    public class OilSpillWaterRequestViewModel
    {
        /// <summary>
        /// Gets or sets the oil spill from date.
        /// </summary>
        /// <value>
        /// The oil spill from date.
        /// </value>
        public DateTime OilSpillFromDate { get; set; }

        /// <summary>
        /// Gets or sets the oil spill to date.
        /// </summary>
        /// <value>
        /// The oil spill to date.
        /// </value>
        public DateTime OilSpillToDate { get; set; }

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
        public string VesselId { get; set; }
    }
}
