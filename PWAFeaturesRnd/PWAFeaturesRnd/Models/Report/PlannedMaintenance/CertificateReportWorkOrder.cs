using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Certificate Report Work Order
	/// </summary>
	public class CertificateReportWorkOrder
	{
		/// <summary>
		/// Gets or sets the reschedule por identifier.
		/// </summary>
		/// <value>
		/// The reschedule por identifier.
		/// </value>
		public string ReschedulePorId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule por request identifier.
		/// </summary>
		/// <value>
		/// The reschedule por request identifier.
		/// </value>
		public string ReschedulePorRequestId { get; set; }

		/// <summary>
		/// Gets or sets the por identifier.
		/// </summary>
		/// <value>
		/// The por identifier.
		/// </value>
		public string PorId { get; set; }

		/// <summary>
		/// Gets or sets the supporting work order ids.
		/// </summary>
		/// <value>
		/// The supporting work order ids.
		/// </value>
		public List<string> SupportingWorkOrderIds { get; set; }

		/// <summary>
		/// Gets or sets the work order status description.
		/// </summary>
		/// <value>
		/// The work order status description.
		/// </value>
		public string WorkOrderStatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the work order status short code.
		/// </summary>
		/// <value>
		/// The work order status short code.
		/// </value>
		public string WorkOrderStatusShortCode { get; set; }

		/// <summary>
		/// Gets or sets the work order status identifier.
		/// </summary>
		/// <value>
		/// The work order status identifier.
		/// </value>
		public string WorkOrderStatusId { get; set; }

		/// <summary>
		/// Gets or sets the job interval type identifier.
		/// </summary>
		/// <value>
		/// The job interval type identifier.
		/// </value>
		public string JobIntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has rescheduled.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has rescheduled; otherwise, <c>false</c>.
		/// </value>
		public bool HasRescheduled { get; set; }

		/// <summary>
		/// Gets or sets the original due date.
		/// </summary>
		/// <value>
		/// The original due date.
		/// </value>
		public DateTime? OriginalDueDate { get; set; }

		/// <summary>
		/// Gets or sets the responsible rank.
		/// </summary>
		/// <value>
		/// The responsible rank.
		/// </value>
		public string ResponsibleRank { get; set; }

		/// <summary>
		/// Gets or sets the job description.
		/// </summary>
		/// <value>
		/// The job description.
		/// </value>
		public string JobDescription { get; set; }

		/// <summary>
		/// Gets or sets from range interval value.
		/// </summary>
		/// <value>
		/// From range interval value.
		/// </value>
		public int? FromRangeIntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the guideline template.
		/// </summary>
		/// <value>
		/// The guideline template.
		/// </value>
		public string GuidelineTemplate { get; set; }

		/// <summary>
		/// Gets or sets the work done date.
		/// </summary>
		/// <value>
		/// The work done date.
		/// </value>
		public DateTime? WorkDoneDate { get; set; }

		/// <summary>
		/// Gets or sets the last completed date.
		/// </summary>
		/// <value>
		/// The last completed date.
		/// </value>
		public DateTime? LastCompletedDate { get; set; }

		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// Gets or sets the interval.
		/// </summary>
		/// <value>
		/// The interval.
		/// </value>
		public int Interval { get; set; }

		/// <summary>
		/// Gets or sets the type of the interval.
		/// </summary>
		/// <value>
		/// The type of the interval.
		/// </value>
		public string IntervalType { get; set; }

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
		/// Gets or sets the name of the work order.
		/// </summary>
		/// <value>
		/// The name of the work order.
		/// </value>
		public string WorkOrderName { get; set; }

		/// <summary>
		/// Gets or sets the work order history identifier.
		/// </summary>
		/// <value>
		/// The work order history identifier.
		/// </value>
		public string WorkOrderHistoryId { get; set; }

		/// <summary>
		/// Gets or sets the work order identifier.
		/// </summary>
		/// <value>
		/// The work order identifier.
		/// </value>
		public string WorkOrderId { get; set; }

		/// <summary>
		/// Gets or sets the schedule task identifier.
		/// </summary>
		/// <value>
		/// The schedule task identifier.
		/// </value>
		public string ScheduleTaskId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the remarks.
		/// </summary>
		/// <value>
		/// The remarks.
		/// </value>
		public string Remarks { get; set; }

		/// <summary>
		/// Gets or sets from due date.
		/// </summary>
		/// <value>
		/// From due date.
		/// </value>
		public DateTime? FromDueDate { get; set; }
	}
}
