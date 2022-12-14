using System;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Work Basket All Detail View Model
    /// </summary>
    public class WorkBasketAllDetailViewModel
    {
        /// <summary>
        /// Gets or sets the type of the job.
        /// </summary>
        /// <value>
        /// The type of the job.
        /// </value>
        public string JobType { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the done date.
        /// </summary>
        /// <value>
        /// The done date.
        /// </value>
        public DateTime? DoneDate { get; set; }

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
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public string Interval { get; set; }

        /// <summary>
        /// Gets or sets the resp.
        /// </summary>
        /// <value>
        /// The resp.
        /// </value>
        public string Resp { get; set; }

        /// <summary>
        /// Gets or sets the left hours.
        /// </summary>
        /// <value>
        /// The left hours.
        /// </value>
        public int? LeftHours { get; set; }

        /// <summary>
        /// Gets or sets the closed date.
        /// </summary>
        /// <value>
        /// The closed date.
        /// </value>
        public DateTime? ClosedDate { get; set; }

        /// <summary>
        /// Gets or sets the class code.
        /// </summary>
        /// <value>
        /// The class code.
        /// </value>
        public string ClassCode { get; set; }

        /// <summary>
        /// Gets or sets the dept.
        /// </summary>
        /// <value>
        /// The dept.
        /// </value>
        public string Dept { get; set; }

        /// <summary>
        /// Gets or sets the type of the order.
        /// </summary>
        /// <value>
        /// The type of the order.
        /// </value>
        public string OrderType { get; set; }

        /// <summary>
        /// Gets or sets the report form.
        /// </summary>
        /// <value>
        /// The report form.
        /// </value>
        public string ReportForm { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        /// <value>
        /// The days.
        /// </value>
        public int Days { get; set; }

        /// <summary>
        /// Gets or sets the st deleted.
        /// </summary>
        /// <value>
        /// The st deleted.
        /// </value>
        public string StDeleted { get; set; }

        /// <summary>
        /// Gets or sets the running hours.
        /// </summary>
        /// <value>
        /// The running hours.
        /// </value>
        public int RunningHours { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>
        /// The attachments.
        /// </value>
        public string Attachments { get; set; }

        /// <summary>
        /// Gets or sets the planned maintenance details request URL.
        /// </summary>
        /// <value>
        /// The planned maintenance details request URL.
        /// </value>
        public string PlannedMaintenanceDetailsRequestURL { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is critical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
        /// </value>
        public bool IsCritical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is over due visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is over due visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverDueVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is overdue period visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is overdue period visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverduePeriodVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is due.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is due; otherwise, <c>false</c>.
        /// </value>
        public bool IsDue { get; set; }

        /// <summary>
        /// Gets or sets the last completed date.
        /// </summary>
        /// <value>
        /// The last completed date.
        /// </value>
        public DateTime? LastCompletedDate { get; set; }
    }
}
