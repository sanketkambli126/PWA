using System;

namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectAdditionalJob
    {
        /// <summary>
        /// Gets or sets the DWR identifier.
        /// </summary>
        /// <value>
        /// The DWR identifier.
        /// </value>
        public string DwrId { get; set; }

        /// <summary>
        /// Gets or sets the dwo identifier.
        /// </summary>
        /// <value>
        /// The dwo identifier.
        /// </value>
        public string DwoId { get; set; }

        /// <summary>
        /// Gets or sets the pwo identifier.
        /// </summary>
        /// <value>
        /// The pwo identifier.
        /// </value>
        public string PwoId { get; set; }

        /// <summary>
        /// Gets or sets the is deleted.
        /// </summary>
        /// <value>
        /// The is deleted.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the status short code.
        /// </summary>
        /// <value>
        /// The status short code.
        /// </value>
        public string StatusShortCode { get; set; }

        /// <summary>
        /// Gets or sets the job type short code.
        /// </summary>
        /// <value>
        /// The job type short code.
        /// </value>
        public string JobTypeShortCode { get; set; }

        /// <summary>
        /// Gets or sets the job type description.
        /// </summary>
        /// <value>
        /// The job type description.
        /// </value>
        public string JobTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the interval value.
        /// </summary>
        /// <value>
        /// The interval value.
        /// </value>
        public int? IntervalValue { get; set; }

        /// <summary>
        /// Gets or sets the type of the interval.
        /// </summary>
        /// <value>
        /// The type of the interval.
        /// </value>
        public string IntervalType { get; set; }

        /// <summary>
        /// Gets or sets the interval type short code.
        /// </summary>
        /// <value>
        /// The interval type short code.
        /// </value>
        public string IntervalTypeShortCode { get; set; }

        /// <summary>
        /// Gets or sets the responsibility.
        /// </summary>
        /// <value>
        /// The responsibility.
        /// </value>
        public string Responsibility { get; set; }

        /// <summary>
        /// Gets or sets the responsibility short code.
        /// </summary>
        /// <value>
        /// The responsibility short code.
        /// </value>
        public string ResponsibilityShortCode { get; set; }

        /// <summary>
        /// Gets or sets the last done date.
        /// </summary>
        /// <value>
        /// The last done date.
        /// </value>
        public DateTime? LastDoneDate { get; set; }

        /// <summary>
        /// Gets or sets the is critical.
        /// </summary>
        /// <value>
        /// The is critical.
        /// </value>
        public bool? IsCritical { get; set; }
    }
}
