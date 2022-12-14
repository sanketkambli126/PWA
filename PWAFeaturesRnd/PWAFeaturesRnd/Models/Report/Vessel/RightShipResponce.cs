namespace PWAFeaturesRnd.Models.Report.Vessel
{
    /// <summary>
    /// The rightship responce
    /// </summary>
    public class RightShipResponce
    {
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the right ship score.
        /// </summary>
        /// <value>
        /// The right ship score.
        /// </value>
        public double RightShipScore { get; set; }

        /// <summary>
        /// Gets or sets the GHG rating.
        /// </summary>
        /// <value>
        /// The GHG rating.
        /// </value>
        public string GHGRating { get; set; }

    }
}
