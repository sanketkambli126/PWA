using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Work Basket Detail Response
	/// </summary>
	public class WorkBasketDetailResponse
	{
		/// <summary>
		/// Gets or sets the actual run time average.
		/// </summary>
		/// <value>
		/// The actual run time average.
		/// </value>
		public int? ActualRunTimeAverage { get; set; }

		/// <summary>
		/// Gets or sets the vessel department identifier.
		/// </summary>
		/// <value>
		/// The vessel department identifier.
		/// </value>
		public string VesselDepartmentId { get; set; }

		/// <summary>
		/// Gets or sets the department.
		/// </summary>
		/// <value>
		/// The department.
		/// </value>
		public string Department { get; set; }

		/// <summary>
		/// Gets or sets the job interval type identifier.
		/// </summary>
		/// <value>
		/// The job interval type identifier.
		/// </value>
		public string JobIntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the work order reschedule count.
		/// </summary>
		/// <value>
		/// The work order reschedule count.
		/// </value>
		public int? WorkOrderRescheduleCount { get; set; }

		/// <summary>
		/// Gets or sets the left hours.
		/// </summary>
		/// <value>
		/// The left hours.
		/// </value>
		public int? LeftHours { get; set; }

		/// <summary>
		/// Gets or sets the last counter reading date.
		/// </summary>
		/// <value>
		/// The last counter reading date.
		/// </value>
		public DateTime? LastCounterReadingDate { get; set; }

		/// <summary>
		/// Gets or sets the is rob less than req.
		/// </summary>
		/// <value>
		/// The is rob less than req.
		/// </value>
		public bool? IsRobLessThanReq { get; set; }

		/// <summary>
		/// Gets or sets the work order rank hour details.
		/// </summary>
		/// <value>
		/// The work order rank hour details.
		/// </value>
		public List<WorkOrderRank> WorkOrderRankHourDetails { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is component critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is component critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsComponentCritical { get; set; }

		/// <summary>
		/// Gets or sets the reschedule count.
		/// </summary>
		/// <value>
		/// The reschedule count.
		/// </value>
		public int? RescheduleCount { get; set; }

		/// <summary>
		/// Gets or sets the original due date.
		/// </summary>
		/// <value>
		/// The original due date.
		/// </value>
		public DateTime? OriginalDueDate { get; set; }

		/// <summary>
		/// Gets or sets the next due date.
		/// </summary>
		/// <value>
		/// The next due date.
		/// </value>
		public List<DateTime> NextDueDate { get; set; }

		/// <summary>
		/// Gets or sets the system area identifier.
		/// </summary>
		/// <value>
		/// The system area identifier.
		/// </value>
		public string SystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the dwo identifier.
		/// </summary>
		/// <value>
		/// The dwo identifier.
		/// </value>
		public string DwoId { get; set; }

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
		/// Gets or sets a value indicating whether [jsa permit required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [jsa permit required]; otherwise, <c>false</c>.
		/// </value>
		public bool JsaPermitRequired { get; set; }

		/// <summary>
		/// Gets or sets the mapped jsa identifier.
		/// </summary>
		/// <value>
		/// The mapped jsa identifier.
		/// </value>
		public string MappedJsaId { get; set; }

		/// <summary>
		/// Gets or sets the other mapped components.
		/// </summary>
		/// <value>
		/// The other mapped components.
		/// </value>
		public List<string> OtherMappedComponents { get; set; }

		/// <summary>
		/// Gets or sets the por request identifier.
		/// </summary>
		/// <value>
		/// The por request identifier.
		/// </value>
		public string PorRequestId { get; set; }

		/// <summary>
		/// Gets or sets the planned for count.
		/// </summary>
		/// <value>
		/// The planned for count.
		/// </value>
		public int? PlannedForCount { get; set; }

		/// <summary>
		/// Gets or sets the type of the reschedule.
		/// </summary>
		/// <value>
		/// The type of the reschedule.
		/// </value>
		public string RescheduleType { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status.
		/// </summary>
		/// <value>
		/// The reschedule status.
		/// </value>
		public string RescheduleStatus { get; set; }

		/// <summary>
		/// Gets or sets the por identifier.
		/// </summary>
		/// <value>
		/// The por identifier.
		/// </value>
		public string PorId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule request number.
		/// </summary>
		/// <value>
		/// The reschedule request number.
		/// </value>
		public string RescheduleRequestNumber { get; set; }

		/// <summary>
		/// Gets or sets the parent PTR identifier.
		/// </summary>
		/// <value>
		/// The parent PTR identifier.
		/// </value>
		public string ParentPtrId { get; set; }

		/// <summary>
		/// Gets or sets the index of the reschedule request sort.
		/// </summary>
		/// <value>
		/// The index of the reschedule request sort.
		/// </value>
		public decimal? RescheduleRequestSortIndex { get; set; }

		/// <summary>
		/// Gets or sets the estimated man hours.
		/// </summary>
		/// <value>
		/// The estimated man hours.
		/// </value>
		public int? EstimatedManHours { get; set; }

		/// <summary>
		/// Gets or sets the wo complete date.
		/// </summary>
		/// <value>
		/// The wo complete date.
		/// </value>
		public DateTime? WOCompleteDate { get; set; }

		/// <summary>
		/// Gets or sets the schedule task identifier.
		/// </summary>
		/// <value>
		/// The schedule task identifier.
		/// </value>
		public string ScheduleTaskId { get; set; }

		/// <summary>
		/// Gets or sets the work order identifier.
		/// </summary>
		/// <value>
		/// The work order identifier.
		/// </value>
		public string WorkOrderId { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the job identifier.
		/// </summary>
		/// <value>
		/// The job identifier.
		/// </value>
		public string JobId { get; set; }

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
		/// Gets or sets the work order status identifier.
		/// </summary>
		/// <value>
		/// The work order status identifier.
		/// </value>
		public string WorkOrderStatusId { get; set; }

		/// <summary>
		/// Gets or sets the work order status description.
		/// </summary>
		/// <value>
		/// The work order status description.
		/// </value>
		public string WorkOrderStatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the job type identifier.
		/// </summary>
		/// <value>
		/// The job type identifier.
		/// </value>
		public string JobTypeId { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the job type description.
		/// </summary>
		/// <value>
		/// The job type description.
		/// </value>
		public string JobTypeDescription { get; set; }

		/// <summary>
		/// Gets or sets the wo history complete date.
		/// </summary>
		/// <value>
		/// The wo history complete date.
		/// </value>
		public DateTime? WOHistoryCompleteDate { get; set; }

		/// <summary>
		/// Gets or sets the frequency from range value.
		/// </summary>
		/// <value>
		/// The frequency from range value.
		/// </value>
		public int? FrequencyFromRangeValue { get; set; }

		/// <summary>
		/// Gets or sets the frequency type identifier.
		/// </summary>
		/// <value>
		/// The frequency type identifier.
		/// </value>
		public string FrequencyTypeId { get; set; }

		/// <summary>
		/// Gets or sets the type of the frequency.
		/// </summary>
		/// <value>
		/// The type of the frequency.
		/// </value>
		public string FrequencyType { get; set; }

		/// <summary>
		/// Gets or sets the frequency type short code.
		/// </summary>
		/// <value>
		/// The frequency type short code.
		/// </value>
		public string FrequencyTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets the responsible rank identifier.
		/// </summary>
		/// <value>
		/// The responsible rank identifier.
		/// </value>
		public string ResponsibleRankId { get; set; }

		/// <summary>
		/// Gets or sets the responsible rank short code.
		/// </summary>
		/// <value>
		/// The responsible rank short code.
		/// </value>
		public string ResponsibleRankShortCode { get; set; }

		/// <summary>
		/// Gets or sets the responsible rank description.
		/// </summary>
		/// <value>
		/// The responsible rank description.
		/// </value>
		public string ResponsibleRankDescription { get; set; }

		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// Gets or sets the is critical.
		/// </summary>
		/// <value>
		/// The is critical.
		/// </value>
		public bool? IsCritical { get; set; }

		/// <summary>
		/// Gets or sets from range due date.
		/// </summary>
		/// <value>
		/// From range due date.
		/// </value>
		public DateTime? FromRangeDueDate { get; set; }

		/// <summary>
		/// Converts to rangeduedate.
		/// </summary>
		/// <value>
		/// To range due date.
		/// </value>
		public DateTime? ToRangeDueDate { get; set; }

		/// <summary>
		/// Gets or sets the rescheduled date.
		/// </summary>
		/// <value>
		/// The rescheduled date.
		/// </value>
		public DateTime? RescheduledDate { get; set; }

		/// <summary>
		/// Gets or sets the last completed date.
		/// </summary>
		/// <value>
		/// The last completed date.
		/// </value>
		public DateTime? LastCompletedDate { get; set; }

		/// <summary>
		/// Gets or sets the frequency.
		/// </summary>
		/// <value>
		/// The frequency.
		/// </value>
		public int? Frequency { get; set; }

		/// <summary>
		/// Gets or sets the request number.
		/// </summary>
		/// <value>
		/// The request number.
		/// </value>
		public string RequestNumber { get; set; }

		/// <summary>
		/// Gets or sets the required spare count.
		/// </summary>
		/// <value>
		/// The required spare count.
		/// </value>
		public int? RequiredSpareCount { get; set; }

		/// <summary>
		/// Gets or sets the is robless than allocated qty.
		/// </summary>
		/// <value>
		/// The is robless than allocated qty.
		/// </value>
		public bool? IsRoblessThanAllocatedQty { get; set; }

		/// <summary>
		/// Gets or sets the has allocated spares.
		/// </summary>
		/// <value>
		/// The has allocated spares.
		/// </value>
		public bool? HasAllocatedSpares { get; set; }
	}
}
