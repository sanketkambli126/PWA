using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// Rolled Up Fore cast Alert View Model 
    /// </summary>
    public class RolledUpForecastAlertViewModel
    {
        /// <summary>
        /// Gets or sets the weather date.
        /// </summary>
        /// <value>
        /// The weather date.
        /// </value>
        public DateTime WeatherDate { get; set; }

        /// <summary>
        /// Gets or sets the speed beaufort.
        /// </summary>
        /// <value>
        /// The speed beaufort.
        /// </value>
        public int SpeedBeaufort { get; set; }

        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public int Direction { get; set; }

        /// <summary>
        /// Gets or sets the speed ms.
        /// </summary>
        /// <value>
        /// The speed ms.
        /// </value>
        public decimal SpeedMS { get; set; }

        /// <summary>
        /// Gets or sets the location string.
        /// </summary>
        /// <value>
        /// The location string.
        /// </value>
        public string LocationStr { get; set; }

        /// <summary>
        /// Gets or sets the beaufort colour.
        /// </summary>
        /// <value>
        /// The beaufort colour.
        /// </value>
        public string BeaufortColour { get; set; }
    }
}
