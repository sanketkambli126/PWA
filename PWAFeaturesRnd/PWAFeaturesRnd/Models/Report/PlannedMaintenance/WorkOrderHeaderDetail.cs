using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Work Order Header Detail
	/// </summary>
	public class WorkOrderHeaderDetail
	{
		/// <summary>
		/// Gets or sets a value indicating whether this instance is critical component.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical component; otherwise, <c>false</c>.
		/// </value>
		public bool IsCriticalComponent { get; set; }

		/// <summary>
		/// Gets or sets the alternate number.
		/// </summary>
		/// <value>
		/// The alternate number.
		/// </value>
		public string AlternateNumber { get; set; }

		/// <summary>
		/// Gets or sets the type of the alternate.
		/// </summary>
		/// <value>
		/// The type of the alternate.
		/// </value>
		public string AlternateType { get; set; }

		/// <summary>
		/// Gets or sets the report form required.
		/// </summary>
		/// <value>
		/// The report form required.
		/// </value>
		public bool? ReportFormRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [report form data migration required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [report form data migration required]; otherwise, <c>false</c>.
		/// </value>
		public bool ReportFormDataMigrationRequired { get; set; }

		/// <summary>
		/// Gets or sets the CBT identifier.
		/// </summary>
		/// <value>
		/// The CBT identifier.
		/// </value>
		public string CbtId { get; set; }

		/// <summary>
		/// Gets or sets the schedule job interval type identifier.
		/// </summary>
		/// <value>
		/// The schedule job interval type identifier.
		/// </value>
		public string ScheduleJobIntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the work order status identifier.
		/// </summary>
		/// <value>
		/// The work order status identifier.
		/// </value>
		public string WorkOrderStatusId { get; set; }

		/// <summary>
		/// Gets or sets the work order indication type identifier.
		/// </summary>
		/// <value>
		/// The work order indication type identifier.
		/// </value>
		public string WorkOrderIndicationTypeId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [jsa required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [jsa required]; otherwise, <c>false</c>.
		/// </value>
		public bool JsaRequired { get; set; }

		/// <summary>
		/// Gets or sets the jsa identifier.
		/// </summary>
		/// <value>
		/// The jsa identifier.
		/// </value>
		public string JsaId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule count.
		/// </summary>
		/// <value>
		/// The reschedule count.
		/// </value>
		public int? RescheduleCount { get; set; }

		/// <summary>
		/// Gets or sets the job identifier.
		/// </summary>
		/// <value>
		/// The job identifier.
		/// </value>
		public string JobId { get; set; }

		/// <summary>
		/// Gets or sets the job type identifier.
		/// </summary>
		/// <value>
		/// The job type identifier.
		/// </value>
		public string JobTypeId { get; set; }

		/// <summary>
		/// Gets or sets the por identifier.
		/// </summary>
		/// <value>
		/// The por identifier.
		/// </value>
		public string PorId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status identifier.
		/// </summary>
		/// <value>
		/// The reschedule status identifier.
		/// </value>
		public string RescheduleStatusId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsCritical { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is history available.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is history available; otherwise, <c>false</c>.
		/// </value>
		public bool IsHistoryAvailable { get; set; }

		/// <summary>
		/// Gets or sets the component position.
		/// </summary>
		/// <value>
		/// The component position.
		/// </value>
		public string ComponentPosition { get; set; }

		/// <summary>
		/// Gets or sets the model.
		/// </summary>
		/// <value>
		/// The model.
		/// </value>
		public string Model { get; set; }

		/// <summary>
		/// Gets or sets the designer.
		/// </summary>
		/// <value>
		/// The designer.
		/// </value>
		public string Designer { get; set; }

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
		/// Gets or sets the system area identifier.
		/// </summary>
		/// <value>
		/// The system area identifier.
		/// </value>
		public string SystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime DueDate { get; set; }

		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		/// <value>
		/// The interval value.
		/// </value>
		public int? IntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the due date interval type identifier.
		/// </summary>
		/// <value>
		/// The due date interval type identifier.
		/// </value>
		public string DueDateIntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets from interval value.
		/// </summary>
		/// <value>
		/// From interval value.
		/// </value>
		public int? FromIntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the type of the due date interval.
		/// </summary>
		/// <value>
		/// The type of the due date interval.
		/// </value>
		public string DueDateIntervalType { get; set; }

		/// <summary>
		/// Gets or sets the work order status code.
		/// </summary>
		/// <value>
		/// The work order status code.
		/// </value>
		public string WorkOrderStatusCode { get; set; }

		/// <summary>
		/// Gets or sets the responsibility rank short code.
		/// </summary>
		/// <value>
		/// The responsibility rank short code.
		/// </value>
		public string ResponsibilityRankShortCode { get; set; }

		/// <summary>
		/// Gets or sets the responsibility rank description.
		/// </summary>
		/// <value>
		/// The responsibility rank description.
		/// </value>
		public string ResponsibilityRankDescription { get; set; }

		/// <summary>
		/// Gets or sets the approver rank short code.
		/// </summary>
		/// <value>
		/// The approver rank short code.
		/// </value>
		public string ApproverRankShortCode { get; set; }

		/// <summary>
		/// Gets or sets the approver rank description.
		/// </summary>
		/// <value>
		/// The approver rank description.
		/// </value>
		public string ApproverRankDescription { get; set; }

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
		/// Gets or sets the name of the maker.
		/// </summary>
		/// <value>
		/// The name of the maker.
		/// </value>
		public string MakerName { get; set; }

		/// <summary>
		/// Gets or sets the work order status.
		/// </summary>
		/// <value>
		/// The work order status.
		/// </value>
		public string WorkOrderStatus { get; set; }

		/// <summary>
		/// Gets or sets from due date.
		/// </summary>
		/// <value>
		/// From due date.
		/// </value>
		public DateTime? FromDueDate { get; set; }

		/// <summary>
		/// Gets or sets the type of the CBM task.
		/// </summary>
		/// <value>
		/// The type of the CBM task.
		/// </value>
		public string CbmTaskType { get; set; }

		/// <summary>
		/// Gets or sets the PMS reschedule rules request.
		/// </summary>
		/// <value>
		/// The PMS reschedule rules request.
		/// </value>
		public PMSRescheduleRulesRequest PMSRescheduleRulesRequest { get; set; }
	}
}
