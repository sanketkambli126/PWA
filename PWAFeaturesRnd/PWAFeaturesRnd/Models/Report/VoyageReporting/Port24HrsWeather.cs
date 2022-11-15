using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// The port24hrs weather
    /// </summary>
    public class Port24HrsWeather
    {
        /// <summary>
        /// Gets or sets the recorder at.
        /// </summary>
        /// <value>
        /// The recorder at.
        /// </value>
        public DateTime RecorderAt { get; set; }

        /// <summary>
        /// Gets or sets the wind force identifier.
        /// </summary>
        /// <value>
        /// The wind force identifier.
        /// </value>
        public string WindForceId { get; set; }

        /// <summary>
        /// Gets or sets the wind direction identifier.
        /// </summary>
        /// <value>
        /// The wind direction identifier.
        /// </value>
        public string WindDirectionId { get; set; }

        /// <summary>
        /// Gets or sets the swell direction identifier.
        /// </summary>
        /// <value>
        /// The swell direction identifier.
        /// </value>
        public string SwellDirectionId { get; set; }

        /// <summary>
        /// Gets or sets the current wave direction.
        /// </summary>
        /// <value>
        /// The current wave direction.
        /// </value>
        public string CurrentWaveDirection { get; set; }

        /// <summary>
        /// Gets or sets the wind speed.
        /// </summary>
        /// <value>
        /// The wind speed.
        /// </value>
        public decimal? WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the PPH identifier.
        /// </summary>
        /// <value>
        /// The PPH identifier.
        /// </value>
        public string PphId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the PSF identifier.
        /// </summary>
        /// <value>
        /// The PSF identifier.
        /// </value>
        public string PsfId { get; set; }
    }
}
