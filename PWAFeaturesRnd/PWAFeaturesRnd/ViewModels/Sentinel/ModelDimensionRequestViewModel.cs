using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    public class ModelDimensionRequestViewModel
    {
        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public string MenuType { get; set; }

        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>
        public string FleetId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the dimension level.
        /// </summary>
        /// <value>
        /// The dimension level.
        /// </value>
        public List<int> DimensionLevel { get; set; }

        /// <summary>
        /// Gets or sets the dimension level string.
        /// </summary>
        /// <value>
        /// The dimension level string.
        /// </value>
        public string DimensionLevelString { get; set; }

        /// <summary>
        /// Gets or sets the model dimension identifier.
        /// </summary>
        /// <value>
        /// The model dimension identifier.
        /// </value>
        public string ModelDimensionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [consider override score category].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [consider override score category]; otherwise, <c>false</c>.
        /// </value>
        public bool ConsiderOverrideScoreCategory { get; set; }
    }
}
