namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// NoonReportVesselDraft
    /// </summary>
    public class NoonReportVesselDraft
    {


        /// <summary>
        /// Gets or sets the pla identifier.
        /// </summary>
        /// <value>
        /// The pla identifier.
        /// </value>
        public string PlaId { get; set; }



        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the spa draft mean d.
        /// </summary>
        /// <value>
        /// The spa draft mean d.
        /// </value>
        public decimal? SpaDraftMeanD { get; set; }

        /// <summary>
        /// Gets or sets the spa draft mid d.
        /// </summary>
        /// <value>
        /// The spa draft mid d.
        /// </value>
        public decimal? SpaDraftMidD { get; set; }

        /// <summary>
        /// Gets or sets the spa DRFT aft d.
        /// </summary>
        /// <value>
        /// The spa DRFT aft d.
        /// </value>
        public float? SpaDrftAftD { get; set; }

        /// <summary>
        /// Gets or sets the spa DRFT forward d.
        /// </summary>
        /// <value>
        /// The spa DRFT forward d.
        /// </value>
        public float? SpaDrftFwdD { get; set; }



        /// <summary>
        /// Gets or sets the spa identifier.
        /// </summary>
        /// <value>
        /// The spa identifier.
        /// </value>
        public string SpaId { get; set; }



        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }
    }
}
