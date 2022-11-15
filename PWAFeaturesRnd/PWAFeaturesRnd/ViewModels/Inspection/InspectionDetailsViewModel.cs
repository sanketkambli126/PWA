using System;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
    public class InspectionDetailsViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
        /// </value>
        public bool IsClosed { get; set; }

        /// <summary>
        /// Gets or sets the TPL identifier.
        /// </summary>
        /// <value>
        /// The TPL identifier.
        /// </value>
        public string TplId { get; set; }

        /// <summary>
        /// Gets or sets the ial identifier report status.
        /// </summary>
        /// <value>
        /// The ial identifier report status.
        /// </value>
        public string IalIdReportStatus { get; set; }

        /// <summary>
        /// Gets or sets the is deleted.
        /// </summary>
        /// <value>
        /// The is deleted.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is supporting document visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is supporting document visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsSupportingDocumentVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [date closed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [date closed]; otherwise, <c>false</c>.
        /// </value>
        public DateTime? DateClosed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is inspection clomplete.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is inspection clomplete; otherwise, <c>false</c>.
        /// </value>
        public bool IsInspectionClomplete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [all findings cleared].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [all findings cleared]; otherwise, <c>false</c>.
        /// </value>
        public bool AllFindingsCleared { get; set; }

        /// <summary>
        /// Gets or sets the inspection type identifier.
        /// </summary>
        /// <value>
        /// The inspection type identifier.
        /// </value>
        public string InspectionTypeId { get; set; }

        public bool IsAllQuestionAndAnsValid { get; set; }
    }
}
