using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// NoonReport24Hweather
    /// </summary>
    public class NoonReport24HweatherViewModel
    {
        /// <summary>
        /// Gets or sets the PHW recorded from.
        /// </summary>
        /// <value>
        /// The PHW recorded from.
        /// </value>
        public DateTime? PhwRecordedFrom { get; set; }

        /// <summary>
        /// Gets or sets the PDR identifier swell direction.
        /// </summary>
        /// <value>
        /// The PDR identifier swell direction.
        /// </value>
        public string PdrIdSwellDirection { get; set; }

        /// <summary>
        /// Gets or sets the PHW identifier.
        /// </summary>
        /// <value>
        /// The PHW identifier.
        /// </value>
        public string PhwId { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [PHW is deleted].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [PHW is deleted]; otherwise, <c>false</c>.
        /// </value>
        public bool PhwIsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the PHW recorded at.
        /// </summary>
        /// <value>
        /// The PHW recorded at.
        /// </value>
        public DateTime? PhwRecordedAt { get; set; }


        /// <summary>
        /// Gets or sets the PHW wind dir.
        /// </summary>
        /// <value>
        /// The PHW wind dir.
        /// </value>
        public string PhwWindDir { get; set; }


        /// <summary>
        /// Gets or sets the PHW wind force.
        /// </summary>
        /// <value>
        /// The PHW wind force.
        /// </value>
        public string PhwWindForce { get; set; }

        /// <summary>
        /// Gets or sets the PHW wind speed.
        /// </summary>
        /// <value>
        /// The PHW wind speed.
        /// </value>
        public decimal? PhwWindSpeed { get; set; }



        /// <summary>
        /// Gets or sets the spa identifier.
        /// </summary>
        /// <value>
        /// The spa identifier.
        /// </value>
        public string SpaId { get; set; }


        /// <summary>
        /// Gets or sets the wav identifier.
        /// </summary>
        /// <value>
        /// The wav identifier.
        /// </value>
        public string WavId { get; set; }
    }
}
