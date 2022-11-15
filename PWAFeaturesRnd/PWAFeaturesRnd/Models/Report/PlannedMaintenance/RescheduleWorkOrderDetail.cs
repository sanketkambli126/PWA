using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Reschedule Work Order Detail
	/// </summary>
	public class RescheduleWorkOrderDetail
	{
		/// <summary>
		/// Gets or sets the type of the reschedule request.
		/// </summary>
		/// <value>
		/// The type of the reschedule request.
		/// </value>
		public string RescheduleRequestType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is risk assessment mapped.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is risk assessment mapped; otherwise, <c>false</c>.
		/// </value>
		public bool IsRiskAssessmentMapped { get; set; }

		/// <summary>
		/// Gets or sets the risk assessment mapped comment.
		/// </summary>
		/// <value>
		/// The risk assessment mapped comment.
		/// </value>
		public string RiskAssessmentMappedComment { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is job history linked.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is job history linked; otherwise, <c>false</c>.
		/// </value>
		public bool IsJobHistoryLinked { get; set; }

		/// <summary>
		/// Gets or sets the job history linked comment.
		/// </summary>
		/// <value>
		/// The job history linked comment.
		/// </value>
		public string JobHistoryLinkedComment { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is supporting wo created.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is supporting wo created; otherwise, <c>false</c>.
		/// </value>
		public bool IsSupportingWOCreated { get; set; }

		/// <summary>
		/// Gets or sets the supporting wo created comment.
		/// </summary>
		/// <value>
		/// The supporting wo created comment.
		/// </value>
		public string SupportingWOCreatedComment { get; set; }

		/// <summary>
		/// Gets or sets the revisit comment.
		/// </summary>
		/// <value>
		/// The revisit comment.
		/// </value>
		public string RevisitComment { get; set; }

		/// <summary>
		/// Gets or sets the requested by.
		/// </summary>
		/// <value>
		/// The requested by.
		/// </value>
		public string RequestedBy { get; set; }

		/// <summary>
		/// Gets or sets the requested on.
		/// </summary>
		/// <value>
		/// The requested on.
		/// </value>
		public DateTime RequestedOn { get; set; }

		/// <summary>
		/// Gets or sets the reschedule request type identifier.
		/// </summary>
		/// <value>
		/// The reschedule request type identifier.
		/// </value>
		public string RescheduleRequestTypeId { get; set; }

		/// <summary>
		/// Gets or sets the approved by.
		/// </summary>
		/// <value>
		/// The approved by.
		/// </value>
		public string ApprovedBy { get; set; }

		/// <summary>
		/// Gets or sets the requester role description.
		/// </summary>
		/// <value>
		/// The requester role description.
		/// </value>
		public string RequesterRoleDescription { get; set; }

		/// <summary>
		/// Gets or sets the approver role description.
		/// </summary>
		/// <value>
		/// The approver role description.
		/// </value>
		public string ApproverRoleDescription { get; set; }

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
		/// Gets or sets the reschedule status identifier.
		/// </summary>
		/// <value>
		/// The reschedule status identifier.
		/// </value>
		public string RescheduleStatusId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule request number.
		/// </summary>
		/// <value>
		/// The reschedule request number.
		/// </value>
		public string RescheduleRequestNumber { get; set; }

		/// <summary>
		/// Gets or sets the index of the reschedule request sort.
		/// </summary>
		/// <value>
		/// The index of the reschedule request sort.
		/// </value>
		public decimal? RescheduleRequestSortIndex { get; set; }

		/// <summary>
		/// Gets or sets the frequency value.
		/// </summary>
		/// <value>
		/// The frequency value.
		/// </value>
		public int? FrequencyValue { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the system area identifier.
		/// </summary>
		/// <value>
		/// The system area identifier.
		/// </value>
		public string SystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the approved on.
		/// </summary>
		/// <value>
		/// The approved on.
		/// </value>
		public DateTime? ApprovedOn { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsCritical { get; set; }

		/// <summary>
		/// Gets or sets the supporting work orders.
		/// </summary>
		/// <value>
		/// The supporting work orders.
		/// </value>
		public List<UnplannedWorkOrderDetail> SupportingWorkOrders { get; set; }

		/// <summary>
		/// Gets or sets the risk assessment.
		/// </summary>
		/// <value>
		/// The risk assessment.
		/// </value>
		public List<RiskAssessmentDetail> RiskAssessment { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

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
		/// Gets or sets the PST identifier.
		/// </summary>
		/// <value>
		/// The PST identifier.
		/// </value>
		public string PstId { get; set; }

		/// <summary>
		/// Gets or sets the PWR identifier.
		/// </summary>
		/// <value>
		/// The PWR identifier.
		/// </value>
		public string PwrId { get; set; }

		/// <summary>
		/// Gets or sets the work order reason description.
		/// </summary>
		/// <value>
		/// The work order reason description.
		/// </value>
		public string WorkOrderReasonDescription { get; set; }

		/// <summary>
		/// Gets or sets the requester comment.
		/// </summary>
		/// <value>
		/// The requester comment.
		/// </value>
		public string RequesterComment { get; set; }

		/// <summary>
		/// Gets or sets the approver comment.
		/// </summary>
		/// <value>
		/// The approver comment.
		/// </value>
		public string ApproverComment { get; set; }

		/// <summary>
		/// Creates new duedate.
		/// </summary>
		/// <value>
		/// The new due date.
		/// </value>
		public DateTime? NewDueDate { get; set; }

		/// <summary>
		/// Gets or sets the additional hazard list.
		/// </summary>
		/// <value>
		/// The additional hazard list.
		/// </value>
		public List<HazardDetail> AdditionalHazardList { get; set; }

		/// <summary>
		/// Gets or sets the original due date.
		/// </summary>
		/// <value>
		/// The original due date.
		/// </value>
		public DateTime OriginalDueDate { get; set; }

		/// <summary>
		/// Gets or sets the original interval.
		/// </summary>
		/// <value>
		/// The original interval.
		/// </value>
		public int? OriginalInterval { get; set; }

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
		/// Gets or sets the pji identifier.
		/// </summary>
		/// <value>
		/// The pji identifier.
		/// </value>
		public string PjiId { get; set; }

		/// <summary>
		/// Gets or sets the extended by.
		/// </summary>
		/// <value>
		/// The extended by.
		/// </value>
		public decimal? ExtendedBy { get; set; }

		/// <summary>
		/// Gets or sets the approved extended by.
		/// </summary>
		/// <value>
		/// The approved extended by.
		/// </value>
		public decimal? ApprovedExtendedBy { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets the schedule job interval type identifier.
		/// </summary>
		/// <value>
		/// The schedule job interval type identifier.
		/// </value>
		public string ScheduleJobIntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the work orders to reschedule.
		/// </summary>
		/// <value>
		/// The work orders to reschedule.
		/// </value>
		public List<RescheduleMappedWODetail> WorkOrdersToReschedule { get; set; }

		/// <summary>
		/// Gets or sets the mapped work order.
		/// </summary>
		/// <value>
		/// The mapped work order.
		/// </value>
		public List<RescheduleLinkedWorkOrder> MappedWorkOrder { get; set; }

		/// <summary>
		/// Gets or sets the requested due date.
		/// </summary>
		/// <value>
		/// The requested due date.
		/// </value>
		public DateTime? RequestedDueDate { get; set; }

		/// <summary>
		/// Gets or sets the frequency type identifier.
		/// </summary>
		/// <value>
		/// The frequency type identifier.
		/// </value>
		public string FrequencyTypeId { get; set; }
    }
}
