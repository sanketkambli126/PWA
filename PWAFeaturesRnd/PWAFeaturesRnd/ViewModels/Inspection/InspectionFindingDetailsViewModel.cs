using System;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// ViewModel for FindingDetails and Causation tab
    /// </summary>
    public class InspectionFindingDetailsViewModel
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
        /// Gets or sets the ves reference.
        /// </summary>
        /// <value>
        /// The ves reference.
        /// </value>
        public string VesRef { get; set; }

        /// <summary>
        /// Gets or sets the reference no.
        /// </summary>
        /// <value>
        /// The reference no.
        /// </value>
        public string RefNo { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the correction action assigned to.
        /// </summary>
        /// <value>
        /// The correction action assigned to.
        /// </value>
        public string CorrectionActionAssignedTo { get; set; }

        /// <summary>
        /// Gets or sets the system area.
        /// </summary>
        /// <value>
        /// The system area.
        /// </value>
        public string SystemArea { get; set; }

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

        //Causation

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
