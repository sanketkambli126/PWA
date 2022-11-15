namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// VoyageRunningHour
    /// </summary>
    public class VoyageRunningHour
    {
        /// <summary>
        /// Gets or sets the VRH identifier.
        /// </summary>
        /// <value>
        /// The VRH identifier.
        /// </value>

        public string VrhId { get; set; }

        /// <summary>
        /// Gets or sets the name of the part.
        /// </summary>
        /// <value>
        /// The name of the part.
        /// </value>

        public string PartName { get; set; }

        /// <summary>
        /// Gets or sets the previous.
        /// </summary>
        /// <value>
        /// The previous.
        /// </value>

        public decimal? Previous { get; set; }

        /// <summary>
        /// Gets or sets the daily.
        /// </summary>
        /// <value>
        /// The daily.
        /// </value>

        public decimal? Daily { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>

        public decimal? Total { get; set; }

        /// <summary>
        /// Gets or sets the daily KWH.
        /// </summary>
        /// <value>
        /// The daily KWH.
        /// </value>

        public decimal? PowerOutput { get; set; }

        /// <summary>
        /// Gets or sets the name of the field.
        /// </summary>
        /// <value>
        /// The name of the field.
        /// </value>

        public string FieldName { get; set; }

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

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>

        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the evp identifier.
        /// </summary>
        /// <value>
        /// The evp identifier.
        /// </value>

        public string EvpId { get; set; }

        /// <summary>
        /// Gets or sets the part code.
        /// </summary>
        /// <value>
        /// The part code.
        /// </value>

        public string PartCode { get; set; }


        /// <summary>
        /// Gets or sets the index of the sort.
        /// </summary>
        /// <value>
        /// The index of the sort.
        /// </value>
        public int SortIndex { get; set; }
    }
}
