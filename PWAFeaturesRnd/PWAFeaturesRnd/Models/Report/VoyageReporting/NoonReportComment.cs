namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// NoonReportComment
    /// </summary>
    public class NoonReportComment
    {
        /// <summary>
        /// Gets or sets the PNC identifier.
        /// </summary>
        /// <value>
        /// The PNC identifier.
        /// </value>

        public string PncId { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>

        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>

        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets the spa identifier.
        /// </summary>
        /// <value>
        /// The spa identifier.
        /// </value>

        public string SpaId { get; set; }

        /// <summary>
        /// Gets or sets the PLK identifier comment category.
        /// </summary>
        /// <value>
        /// The PLK identifier comment category.
        /// </value>

        public string PlkIdCommentCategory { get; set; }

        /// <summary>
        /// Gets or sets the NCR identifier.
        /// </summary>
        /// <value>
        /// The NCR identifier.
        /// </value>

        public string NcrId { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>

        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>

        public bool IsDeleted { get; set; }
    }
}
