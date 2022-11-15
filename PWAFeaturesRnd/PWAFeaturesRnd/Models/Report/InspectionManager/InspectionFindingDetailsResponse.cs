using System;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Object for finding details
    /// </summary>
    public class InspectionFindingDetailsResponse
    {
        /// <summary>
        /// Gets or sets the inspection identifier.
        /// </summary>
        /// <value>
        /// The inspection identifier.
        /// </value>
        public string InspectionId { get; set; }

        /// <summary>
		/// Gets or sets the name of the inspection.
		/// </summary>
		/// <value>
		/// The name of the inspection.
		/// </value>
		public string InspectionName { get; set; }

        /// <summary>
        /// Gets or sets the finding identifier.
        /// </summary>
        /// <value>
        /// The finding identifier.
        /// </value>
        public string FindingId { get; set; }

        /// <summary>
        /// Gets or sets the vesselreference number.
        /// </summary>
        /// <value>
        /// The vesselreference number.
        /// </value>
        public string VesselreferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the chapter inspection reference number.
        /// </summary>
        /// <value>
        /// The chapter inspection reference number.
        /// </value>
        public string ChapterInspectionReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the category.
        /// </summary>
        /// <value>
        /// The type of the category.
        /// </value>
        public string CategoryType { get; set; }

        /// <summary>
        /// Gets or sets the correction actions assigned to.
        /// </summary>
        /// <value>
        /// The correction actions assigned to.
        /// </value>
        public string CorrectionActionsAssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the systeam area.
        /// </summary>
        /// <value>
        /// The systeam area.
        /// </value>
        public string SysteamArea { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the date cleared.
        /// </summary>
        /// <value>
        /// The date cleared.
        /// </value>
        public DateTime? DateCleared { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the substandard acts.
        /// </summary>
        /// <value>
        /// The substandard acts.
        /// </value>
        public string SubstandardActs { get; set; }

        /// <summary>
        /// Gets or sets the substandard conditions.
        /// </summary>
        /// <value>
        /// The substandard conditions.
        /// </value>
        public string SubstandardConditions { get; set; }

        /// <summary>
        /// Gets or sets the human factors.
        /// </summary>
        /// <value>
        /// The human factors.
        /// </value>
        public string HumanFactors { get; set; }

        /// <summary>
        /// Gets or sets the job factors.
        /// </summary>
        /// <value>
        /// The job factors.
        /// </value>
        public string JobFactors { get; set; }

        /// <summary>
        /// Gets or sets the control management failure.
        /// </summary>
        /// <value>
        /// The control management failure.
        /// </value>
        public string ControlManagementFailure { get; set; }
    }
}
