using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Reschedule Mapped WO Detail
	/// </summary>
	public class RescheduleMappedWODetail
	{
		/// <summary>
		/// Gets or sets the work order status short code.
		/// </summary>
		/// <value>
		/// The work order status short code.
		/// </value>
		public string WorkOrderStatusShortCode { get; set; }

		/// <summary>
		/// Gets or sets the work order status description.
		/// </summary>
		/// <value>
		/// The work order status description.
		/// </value>
		public string WorkOrderStatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the left hours.
		/// </summary>
		/// <value>
		/// The left hours.
		/// </value>
		public int? LeftHours { get; set; }

		/// <summary>
		/// Gets or sets the responsibility short code.
		/// </summary>
		/// <value>
		/// The responsibility short code.
		/// </value>
		public string ResponsibilityShortCode { get; set; }

		/// <summary>
		/// Gets or sets the responsibility description.
		/// </summary>
		/// <value>
		/// The responsibility description.
		/// </value>
		public string ResponsibilityDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="RescheduleMappedWODetail"/> is iscritical.
		/// </summary>
		/// <value>
		///   <c>true</c> if iscritical; otherwise, <c>false</c>.
		/// </value>
		public bool Iscritical { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status identifier.
		/// </summary>
		/// <value>
		/// The reschedule status identifier.
		/// </value>
		public string RescheduleStatusId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status short code.
		/// </summary>
		/// <value>
		/// The reschedule status short code.
		/// </value>
		public string RescheduleStatusShortCode { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status description.
		/// </summary>
		/// <value>
		/// The reschedule status description.
		/// </value>
		public string RescheduleStatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the reschedule count.
		/// </summary>
		/// <value>
		/// The reschedule count.
		/// </value>
		public int? RescheduleCount { get; set; }

		/// <summary>
		/// Gets or sets the work order status identifier.
		/// </summary>
		/// <value>
		/// The work order status identifier.
		/// </value>
		public string WorkOrderStatusId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the requested interval.
		/// </summary>
		/// <value>
		/// The requested interval.
		/// </value>
		public decimal? RequestedInterval { get; set; }

		/// <summary>
		/// Gets or sets the rescheduled interval.
		/// </summary>
		/// <value>
		/// The rescheduled interval.
		/// </value>
		public decimal? RescheduledInterval { get; set; }

		/// <summary>
		/// Gets or sets the original interval.
		/// </summary>
		/// <value>
		/// The original interval.
		/// </value>
		public int? OriginalInterval { get; set; }

		/// <summary>
		/// Gets or sets the PST identifier.
		/// </summary>
		/// <value>
		/// The PST identifier.
		/// </value>
		public string PstId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule request number.
		/// </summary>
		/// <value>
		/// The reschedule request number.
		/// </value>
		public string RescheduleRequestNumber { get; set; }

		/// <summary>
		/// Gets or sets the job type description.
		/// </summary>
		/// <value>
		/// The job type description.
		/// </value>
		public string JobTypeDescription { get; set; }

		/// <summary>
		/// Gets or sets the job type short code.
		/// </summary>
		/// <value>
		/// The job type short code.
		/// </value>
		public string JobTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the extended by.
		/// </summary>
		/// <value>
		/// The extended by.
		/// </value>
		public decimal? ExtendedBy { get; set; }

		/// <summary>
		/// Gets or sets the por identifier.
		/// </summary>
		/// <value>
		/// The por identifier.
		/// </value>
		public string PorId { get; set; }

		/// <summary>
		/// Gets or sets the por request identifier.
		/// </summary>
		/// <value>
		/// The por request identifier.
		/// </value>
		public string PorRequestId { get; set; }

		/// <summary>
		/// Gets or sets the pwo identifier.
		/// </summary>
		/// <value>
		/// The pwo identifier.
		/// </value>
		public string PwoId { get; set; }

		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		/// <value>
		/// The interval value.
		/// </value>
		public int? IntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the interval type identifier.
		/// </summary>
		/// <value>
		/// The interval type identifier.
		/// </value>
		public string IntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the type of the interval.
		/// </summary>
		/// <value>
		/// The type of the interval.
		/// </value>
		public string IntervalType { get; set; }

		/// <summary>
		/// Gets or sets the original due date.
		/// </summary>
		/// <value>
		/// The original due date.
		/// </value>
		public DateTime? OriginalDueDate { get; set; }

		/// <summary>
		/// Gets or sets the requested due date.
		/// </summary>
		/// <value>
		/// The requested due date.
		/// </value>
		public DateTime? RequestedDueDate { get; set; }

		/// <summary>
		/// Gets or sets the index of the reschedule request sort.
		/// </summary>
		/// <value>
		/// The index of the reschedule request sort.
		/// </value>
		public decimal? RescheduleRequestSortIndex { get; set; }

		/// <summary>
		/// Creates new duedate.
		/// </summary>
		/// <value>
		/// The new due date.
		/// </value>
		public DateTime? NewDueDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the average.
		/// </summary>
		/// <value>
		/// The average.
		/// </value>
		public int? Average { get; set; }

		/// <summary>
		/// Gets or sets the last completed date.
		/// </summary>
		/// <value>
		/// The last completed date.
		/// </value>
		public DateTime? LastCompletedDate { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the job identifier.
		/// </summary>
		/// <value>
		/// The job identifier.
		/// </value>
		public string JobId { get; set; }

		/// <summary>
		/// Gets or sets the system area identifier.
		/// </summary>
		/// <value>
		/// The system area identifier.
		/// </value>
		public string SystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the last counter reading date.
		/// </summary>
		/// <value>
		/// The last counter reading date.
		/// </value>
		public DateTime? LastCounterReadingDate { get; set; }

		/// <summary>
		/// Gets or sets the last work done reading.
		/// </summary>
		/// <value>
		/// The last work done reading.
		/// </value>
		public int? LastWorkDoneReading { get; set; }

		/// <summary>
		/// Gets or sets the counter reading.
		/// </summary>
		/// <value>
		/// The counter reading.
		/// </value>
		public int? CounterReading { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can reschedule.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can reschedule; otherwise, <c>false</c>.
		/// </value>
		public bool CanReschedule { get; set; }
	}
}
