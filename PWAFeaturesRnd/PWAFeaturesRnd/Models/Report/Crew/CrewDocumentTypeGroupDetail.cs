using System;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// Crew document type group detail
    /// </summary>
    public class CrewDocumentTypeGroupDetail
    {
        /// <summary>
        /// Gets or sets a value indicating whether [CDT critical qualification].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [CDT critical qualification]; otherwise, <c>false</c>.
        /// </value>
        public bool CdtCriticalQualification { get; set; }
        /// <summary>
        /// Gets or sets the CDT identifier.
        /// </summary>
        /// <value>
        /// The CDT identifier.
        /// </value>
        public string CdtId { get; set; }
        /// <summary>
        /// Gets or sets the CDT seq.
        /// </summary>
        /// <value>
        /// The CDT seq.
        /// </value>
        public int CdtSeq { get; set; }
        /// <summary>
        /// Gets or sets the CDT description.
        /// </summary>
        /// <value>
        /// The CDT description.
        /// </value>
        public string CdtDescription { get; set; }
        /// <summary>
        /// Gets or sets the CDT updated on.
        /// </summary>
        /// <value>
        /// The CDT updated on.
        /// </value>
        public DateTime CdtUpdatedOn { get; set; }
    }
}
