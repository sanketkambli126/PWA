namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// Crew Document Type Detail
    /// </summary>
    public class CrewDocumentTypeDetail
    {
        /// <summary>
        /// Gets or sets the document desc.
        /// </summary>
        /// <value>
        /// The document desc.
        /// </value>
        public string DocDesc { get; set; }

        /// <summary>
        /// Gets or sets the document reference.
        /// </summary>
        /// <value>
        /// The document reference.
        /// </value>
        public string DocRef { get; set; }

        /// <summary>
        /// Gets or sets the document seq.
        /// </summary>
        /// <value>
        /// The document seq.
        /// </value>
        public int? DocSeq { get; set; }

        /// <summary>
        /// Gets or sets the crew document type group.
        /// </summary>
        /// <value>
        /// The crew document type group.
        /// </value>
        public CrewDocumentTypeGroupDetail CrewDocumentTypeGroup { get; set; }

    }
}
