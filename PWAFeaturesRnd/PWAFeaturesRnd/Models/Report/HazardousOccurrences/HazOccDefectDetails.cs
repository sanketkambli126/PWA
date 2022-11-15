using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// HazOcc Defect Details
    /// </summary>
    public class HazOccDefectDetails
    {
        /// <summary>
        /// Gets or sets the defect no.
        /// </summary>
        /// <value>
        /// The defect no.
        /// </value>
        public string DefectNo { get; set; }
        /// <summary>
        /// Gets or sets the defect title.
        /// </summary>
        /// <value>
        /// The defect title.
        /// </value>
        public string DefectTitle { get; set; }
        /// <summary>
        /// Gets or sets the current due date.
        /// </summary>
        /// <value>
        /// The current due date.
        /// </value>
        public DateTime? CurrentDueDate { get; set; }
        /// <summary>
        /// Gets or sets the datecompleted.
        /// </summary>
        /// <value>
        /// The datecompleted.
        /// </value>
        public DateTime? Datecompleted { get; set; }
        /// <summary>
        /// Gets or sets the planned for.
        /// </summary>
        /// <value>
        /// The planned for.
        /// </value>
        public string PlannedFor { get; set; }

        /// <summary>
        /// Gets or sets the defect status.
        /// </summary>
        /// <value>
        /// The defect status.
        /// </value>
        public string DefectStatus { get; set; }

        /// <summary>
        /// Gets or sets the defect identifier.
        /// </summary>
        /// <value>
        /// The defect identifier.
        /// </value>
        public string DefectId { get; set; }

        /// <summary>
        /// Gets or sets the requisition identifier.
        /// </summary>
        /// <value>
        /// The requisition identifier.
        /// </value>
        public string RequisitionId { get; set; }

        /// <summary>
        /// Gets or sets the requisition count.
        /// </summary>
        /// <value>
        /// The requisition count.
        /// </value>
        public int RequisitionCount { get; set; }
        /// <summary>
        /// Gets or sets the acc code.
        /// </summary>
        /// <value>
        /// The acc code.
        /// </value>
        public string AccCode { get; set; }
        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public string Priority { get; set; }
    }
}
