using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Work History Response
	/// </summary>
	public class WorkHistoryResponse
	{
		/// <summary>
		/// Gets or sets the work order indication identifier.
		/// </summary>
		/// <value>
		/// The work order indication identifier.
		/// </value>
		public string WorkOrderIndicationId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has attachments.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has attachments; otherwise, <c>false</c>.
		/// </value>
		public bool HasAttachments { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [schedule task exists].
		/// </summary>
		/// <value>
		///   <c>true</c> if [schedule task exists]; otherwise, <c>false</c>.
		/// </value>
		public bool ScheduleTaskExists { get; set; }

		/// <summary>
		/// Gets or sets the reported running hours.
		/// </summary>
		/// <value>
		/// The reported running hours.
		/// </value>
		public int? ReportedRunningHours { get; set; }

		/// <summary>
		/// Gets or sets the interval identifier.
		/// </summary>
		/// <value>
		/// The interval identifier.
		/// </value>
		public string IntervalId { get; set; }

		/// <summary>
		/// Gets or sets the wo class.
		/// </summary>
		/// <value>
		/// The wo class.
		/// </value>
		public string WOClass { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has report form.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has report form; otherwise, <c>false</c>.
		/// </value>
		public bool HasReportForm { get; set; }

		/// <summary>
		/// Gets or sets the interval.
		/// </summary>
		/// <value>
		/// The interval.
		/// </value>
		public string Interval { get; set; }

		/// <summary>
		/// Gets or sets the no of days.
		/// </summary>
		/// <value>
		/// The no of days.
		/// </value>
		public int? NoOfDays { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsCritical { get; set; }

		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// Gets or sets the wo closed date.
		/// </summary>
		/// <value>
		/// The wo closed date.
		/// </value>
		public DateTime? WOClosedDate { get; set; }

		/// <summary>
		/// Gets or sets the wo completed date.
		/// </summary>
		/// <value>
		/// The wo completed date.
		/// </value>
		public DateTime? WOCompletedDate { get; set; }

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
		/// Gets or sets the responsible rank description.
		/// </summary>
		/// <value>
		/// The responsible rank description.
		/// </value>
		public string ResponsibleRankDescription { get; set; }

		/// <summary>
		/// Gets or sets the responsible rank short code.
		/// </summary>
		/// <value>
		/// The responsible rank short code.
		/// </value>
		public string ResponsibleRankShortCode { get; set; }

		/// <summary>
		/// Gets or sets the department description.
		/// </summary>
		/// <value>
		/// The department description.
		/// </value>
		public string DepartmentDescription { get; set; }

		/// <summary>
		/// Gets or sets the department short code.
		/// </summary>
		/// <value>
		/// The department short code.
		/// </value>
		public string DepartmentShortCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the job identifier.
		/// </summary>
		/// <value>
		/// The job identifier.
		/// </value>
		public string JobId { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the parent component identifier.
		/// </summary>
		/// <value>
		/// The parent component identifier.
		/// </value>
		public string ParentComponentId { get; set; }

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
		/// Gets or sets the defect work order identifier.
		/// </summary>
		/// <value>
		/// The defect work order identifier.
		/// </value>
		public string DefectWorkOrderId { get; set; }

		/// <summary>
		/// Gets or sets the class code.
		/// </summary>
		/// <value>
		/// The class code.
		/// </value>
		public string ClassCode { get; set; }
	}
}
