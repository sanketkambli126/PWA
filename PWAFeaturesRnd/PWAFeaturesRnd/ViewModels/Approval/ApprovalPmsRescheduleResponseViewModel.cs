using System;

namespace PWAFeaturesRnd.ViewModels.Approval
{
    /// <summary>
	/// PMS Pending Reschedule Response View Model
	/// </summary>
    public class ApprovalPmsRescheduleResponseViewModel
    {        /// <summary>
             /// Gets or sets the pwo identifier.
             /// </summary>
             /// <value>
             /// The pwo identifier.
             /// </value>
        public string PwoId { get; set; }

        /// <summary>
        /// Gets or sets the PJS identifier.
        /// </summary>
        /// <value>
        /// The PJS identifier.
        /// </value>
        public string PjsId { get; set; }

        /// <summary>
        /// Gets or sets the PST identifier.
        /// </summary>
        /// <value>
        /// The PST identifier.
        /// </value>
        public string PstId { get; set; }

        /// <summary>
        /// Gets or sets the PTR identifier.
        /// </summary>
        /// <value>
        /// The PTR identifier.
        /// </value>
        public string PtrId { get; set; }

        /// <summary>
        /// Gets or sets the PJB identifier.
        /// </summary>
        /// <value>
        /// The PJB identifier.
        /// </value>
        public string PjbId { get; set; }

        /// <summary>
        /// Gets or sets the previous due date.
        /// </summary>
        /// <value>
        /// The previous due date.
        /// </value>
        public DateTime? PreviousDueDate { get; set; }

        /// <summary>
        /// Gets or sets the current due date.
        /// </summary>
        /// <value>
        /// The current due date.
        /// </value>
        public DateTime? CurrentDueDate { get; set; }

        /// <summary>
        /// Gets or sets the reason for rescedule.
        /// </summary>
        /// <value>
        /// The reason for rescedule.
        /// </value>
        public string ReasonForRescedule { get; set; }

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
        /// Gets or sets the type of the job.
        /// </summary>
        /// <value>
        /// The type of the job.
        /// </value>
        public string JobType { get; set; }

        /// <summary>
        /// Gets or sets the job type short code.
        /// </summary>
        /// <value>
        /// The job type short code.
        /// </value>
        public string JobTypeShortCode { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the status short code.
        /// </summary>
        /// <value>
        /// The status short code.
        /// </value>
        public string StatusShortCode { get; set; }

        /// <summary>
        /// Gets or sets the interval value.
        /// </summary>
        /// <value>
        /// The interval value.
        /// </value>
        public int? IntervalValue { get; set; }

        /// <summary>
        /// Gets or sets the intervaltype.
        /// </summary>
        /// <value>
        /// The intervaltype.
        /// </value>
        public string Intervaltype { get; set; }

        /// <summary>
        /// Gets or sets the interval type short code.
        /// </summary>
        /// <value>
        /// The interval type short code.
        /// </value>
        public string IntervalTypeShortCode { get; set; }

        /// <summary>
        /// Gets or sets the responsible rank.
        /// </summary>
        /// <value>
        /// The responsible rank.
        /// </value>
        public string ResponsibleRank { get; set; }

        /// <summary>
        /// Gets or sets the responsible rank short code.
        /// </summary>
        /// <value>
        /// The responsible rank short code.
        /// </value>
        public string ResponsibleRankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the original interval.
        /// </summary>
        /// <value>
        /// The original interval.
        /// </value>
        public string OriginalInterval { get; set; }

        /// <summary>
        /// Gets or sets the requested interval.
        /// </summary>
        /// <value>
        /// The requested interval.
        /// </value>
        public string RequestedInterval { get; set; }

        /// <summary>
        /// Gets or sets the rescheduled interval.
        /// </summary>
        /// <value>
        /// The rescheduled interval.
        /// </value>
        public int? RescheduledInterval { get; set; }

        /// <summary>
        /// Gets or sets the por identifier.
        /// </summary>
        /// <value>
        /// The por identifier.
        /// </value>
        public string PorId { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the approver comment.
        /// </summary>
        /// <value>
        /// The approver comment.
        /// </value>
        public string ApproverComment { get; set; }

        /// <summary>
        /// Gets or sets the reschedule status identifier.
        /// </summary>
        /// <value>
        /// The reschedule status identifier.
        /// </value>
        public string RescheduleStatusId { get; set; }

        /// <summary>
        /// Gets or sets the name of the reschedule status.
        /// </summary>
        /// <value>
        /// The name of the reschedule status.
        /// </value>
        public string RescheduleStatusName { get; set; }

        /// <summary>
        /// Gets or sets the reschedule status short code.
        /// </summary>
        /// <value>
        /// The reschedule status short code.
        /// </value>
        public string RescheduleStatusShortCode { get; set; }

        /// <summary>
        /// Gets or sets the pji identifier.
        /// </summary>
        /// <value>
        /// The pji identifier.
        /// </value>
        public string PjiId { get; set; }

        /// <summary>
        /// Gets or sets the PGR identifier.
        /// </summary>
        /// <value>
        /// The PGR identifier.
        /// </value>
        public string PgrId { get; set; }

        /// <summary>
        /// Gets or sets the reschedule request number.
        /// </summary>
        /// <value>
        /// The reschedule request number.
        /// </value>
        public string RequestNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is critical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
        /// </value>
        public bool? IsCritical { get; set; }

        /// <summary>
        /// Gets or sets the reschedule type identifier.
        /// </summary>
        /// <value>
        /// The reschedule type identifier.
        /// </value>
        public string RescheduleTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the reschedule.
        /// </summary>
        /// <value>
        /// The type of the reschedule.
        /// </value>
        public string RescheduleType { get; set; }

        /// <summary>
        /// Gets or sets the interval to value.
        /// </summary>
        /// <value>
        /// The interval to value.
        /// </value>
        public int? IntervalToValue { get; set; }

        /// <summary>
        /// Gets or sets the previous from due date.
        /// </summary>
        /// <value>
        /// The previous from due date.
        /// </value>
        public DateTime? PreviousFromDueDate { get; set; }

        /// <summary>
        /// Gets or sets the current from due date.
        /// </summary>
        /// <value>
        /// The current from due date.
        /// </value>
        public DateTime? CurrentFromDueDate { get; set; }

        /// <summary>
        /// Gets or sets the original from interval.
        /// </summary>
        /// <value>
        /// The original from interval.
        /// </value>
        public int? OriginalFromInterval { get; set; }

        /// <summary>
        /// Gets or sets the requested from interval.
        /// </summary>
        /// <value>
        /// The requested from interval.
        /// </value>
        public int? RequestedFromInterval { get; set; }

        /// <summary>
        /// Gets or sets the rescheduled from interval.
        /// </summary>
        /// <value>
        /// The rescheduled from interval.
        /// </value>
        public int? RescheduledFromInterval { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the Encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The Encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }
        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the total records.
        /// </summary>
        /// <value>
        /// The total records.
        /// </value>
        public int? TotalRecords { get; set; }

        /// <summary>
		/// Gets or sets the PMS details URL.
		/// </summary>
		/// <value>
		/// The PMS details URL.
		/// </value>
		public string PlannedMaintenanceDetailsRequestURL { get; set; }
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
    }
}
