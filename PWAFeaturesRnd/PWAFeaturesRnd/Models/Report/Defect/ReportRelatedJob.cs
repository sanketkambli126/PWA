using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class ReportRelatedJob
    {
        /// <summary>
        /// Gets or sets the PST identifier.
        /// </summary>
        /// <value>
        /// The PST identifier.
        /// </value>
        public string PstId { get; set; }

        /// <summary>
        /// Gets or sets the risks.
        /// </summary>
        /// <value>
        /// The risks.
        /// </value>
        public List<ScheduleTaskHSERisk> Risks { get; set; }

        /// <summary>
        /// Gets or sets the DWR identifier.
        /// </summary>
        /// <value>
        /// The DWR identifier.
        /// </value>
        public string DwrId { get; set; }

        /// <summary>
        /// Gets or sets the reason identifier.
        /// </summary>
        /// <value>
        /// The reason identifier.
        /// </value>
        public string ReasonId { get; set; }

        /// <summary>
        /// Gets or sets the comment for reason.
        /// </summary>
        /// <value>
        /// The comment for reason.
        /// </value>
        public string CommentForReason { get; set; }

        /// <summary>
        /// Gets or sets the pwo identifier.
        /// </summary>
        /// <value>
        /// The pwo identifier.
        /// </value>
        public string PwoId { get; set; }
    }
}
