using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Reschedule Work Order Detail ViewModel
    /// </summary>
    public class RescheduleWorkOrderDetailViewModel
    {
        /// <summary>
        /// Gets or sets the type of the reschedule request.
        /// </summary>
        /// <value>
        /// The type of the reschedule request.
        /// </value>
        public string RescheduleRequestType { get; set; }

        /// <summary>
        /// Gets or sets the original due date.
        /// </summary>
        /// <value>
        /// The original due date.
        /// </value>
        public DateTime OriginalDueDate { get; set; }

        /// <summary>
        /// Gets or sets the requested due date.
        /// </summary>
        /// <value>
        /// The requested due date.
        /// </value>
        public DateTime? RequestedDueDate { get; set; }

        /// <summary>
        /// Creates new duedate.
        /// </summary>
        /// <value>
        /// The new due date.
        /// </value>
        public DateTime? NewDueDate { get; set; }

        /// <summary>
        /// Gets or sets the work order reason description.
        /// </summary>
        /// <value>
        /// The work order reason description.
        /// </value>
        public string WorkOrderReasonDescription { get; set; }

        /// <summary>
        /// Gets or sets the requested by.
        /// </summary>
        /// <value>
        /// The requested by.
        /// </value>
        public string RequestedBy { get; set; }

        /// <summary>
        /// Gets or sets the requester role description.
        /// </summary>
        /// <value>
        /// The requester role description.
        /// </value>
        public string RequesterRoleDescription { get; set; }

        /// <summary>
        /// Gets or sets the approved by.
        /// </summary>
        /// <value>
        /// The approved by.
        /// </value>
        public string ApprovedBy { get; set; }

        /// <summary>
        /// Gets or sets the approver role description.
        /// </summary>
        /// <value>
        /// The approver role description.
        /// </value>
        public string ApproverRoleDescription { get; set; }

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
        /// Gets or sets the reschedule status description.
        /// </summary>
        /// <value>
        /// The reschedule status description.
        /// </value>
        public string RescheduleStatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the work order reschedule identifier.
        /// </summary>
        /// <value>
        /// The work order reschedule identifier.
        /// </value>
        public string WorkOrderRescheduleId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is requester.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is requester; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequester { get; set; }

        /// <summary>
        /// Gets or sets the requested on.
        /// </summary>
        /// <value>
        /// The requested on.
        /// </value>
        public string RequestedOn { get; set; }

        /// <summary>
        /// Gets or sets the approve on.
        /// </summary>
        /// <value>
        /// The approve on.
        /// </value>
        public string ApproveOn { get; set; }

        /// <summary>
        /// Gets or sets the reschedule request type identifier.
        /// </summary>
        /// <value>
        /// The reschedule request type identifier.
        /// </value>
        public string RescheduleRequestTypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved row visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is approved row visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsApprovedRowVisible { get; set; }

		/// <summary>
		/// Gets or sets the extended by.
		/// </summary>
		/// <value>
		/// The extended by.
		/// </value>
		public int ExtendedBy { get; set; }

		/// <summary>
		/// Gets or sets the reason for reschedule.
		/// </summary>
		/// <value>
		/// The reason for reschedule.
		/// </value>
		public string ReasonForReschedule { get; set; }

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
        /// Gets or sets the associated jobs.
        /// </summary>
        /// <value>
        /// The associated jobs.
        /// </value>
        public List<RescheduleMappedWODetailViewModel> AssociatedJobs { get; set; }

		/// <summary>
		/// Gets or sets the supporting jobs history.
		/// </summary>
		/// <value>
		/// The supporting jobs history.
		/// </value>
		public List<RescheduleLinkedWorkOrderViewModel> SupportingJobsHistory { get; set; }

		/// <summary>
		/// Gets or sets the supporting work orders.
		/// </summary>
		/// <value>
		/// The supporting work orders.
		/// </value>
		public List<UnplannedWorkOrderDetailViewModel> SupportingWorkOrders { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is appprove.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is appprove; otherwise, <c>false</c>.
		/// </value>
		public bool IsApprove { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is reject.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is reject; otherwise, <c>false</c>.
		/// </value>
		public bool IsReject { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is revise.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is revise; otherwise, <c>false</c>.
		/// </value>
		public bool IsRevise{ get; set; }

		/// <summary>
		/// Gets or sets the comment.
		/// </summary>
		/// <value>
		/// The comment.
		/// </value>
		public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the approved extended by.
        /// </summary>
        /// <value>
        /// The approved extended by.
        /// </value>
        public decimal? ApprovedExtendedBy { get; set; }
    }
}