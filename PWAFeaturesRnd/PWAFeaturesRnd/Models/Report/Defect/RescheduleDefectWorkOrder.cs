using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// Reschedule Defect Work Order
	/// </summary>
	public class RescheduleDefectWorkOrder
	{
		/// <summary>
		/// Gets or sets the approved on.
		/// </summary>
		/// <value>
		/// The approved on.
		/// </value>
		public DateTime? ApprovedOn { get; set; }

		/// <summary>
		/// Gets or sets the approver role.
		/// </summary>
		/// <value>
		/// The approver role.
		/// </value>
		public string ApproverRole { get; set; }

		/// <summary>
		/// Gets or sets the approved by.
		/// </summary>
		/// <value>
		/// The approved by.
		/// </value>
		public string ApprovedBy { get; set; }

		/// <summary>
		/// Gets or sets the requested on.
		/// </summary>
		/// <value>
		/// The requested on.
		/// </value>
		public DateTime RequestedOn { get; set; }

		/// <summary>
		/// Gets or sets the requester role.
		/// </summary>
		/// <value>
		/// The requester role.
		/// </value>
		public string RequesterRole { get; set; }

		/// <summary>
		/// Gets or sets the requested by.
		/// </summary>
		/// <value>
		/// The requested by.
		/// </value>
		public string RequestedBy { get; set; }

		/// <summary>
		/// Gets or sets the requested due date.
		/// </summary>
		/// <value>
		/// The requested due date.
		/// </value>
		public DateTime? RequestedDueDate { get; set; }

		/// <summary>
		/// Gets or sets the previous due date.
		/// </summary>
		/// <value>
		/// The previous due date.
		/// </value>
		public DateTime PreviousDueDate { get; set; }

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
		/// Gets or sets the approver comment.
		/// </summary>
		/// <value>
		/// The approver comment.
		/// </value>
		public string ApproverComment { get; set; }

		/// <summary>
		/// Gets or sets the requester comment.
		/// </summary>
		/// <value>
		/// The requester comment.
		/// </value>
		public string RequesterComment { get; set; }

		/// <summary>
		/// Gets or sets the status short code.
		/// </summary>
		/// <value>
		/// The status short code.
		/// </value>
		public string StatusShortCode { get; set; }

		/// <summary>
		/// Gets or sets the reschedule work order status.
		/// </summary>
		/// <value>
		/// The reschedule work order status.
		/// </value>
		public string RescheduleWorkOrderStatus { get; set; }

		/// <summary>
		/// Gets or sets the PWR identifier.
		/// </summary>
		/// <value>
		/// The PWR identifier.
		/// </value>
		public string PwrId { get; set; }

		/// <summary>
		/// Gets or sets the dwo identifier.
		/// </summary>
		/// <value>
		/// The dwo identifier.
		/// </value>
		public string DwoId { get; set; }

		/// <summary>
		/// Gets or sets the dor identifier.
		/// </summary>
		/// <value>
		/// The dor identifier.
		/// </value>
		public string DorId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status.
		/// </summary>
		/// <value>
		/// The reschedule status.
		/// </value>
		public DefectWorkOrderRescheduleStatus RescheduleStatus { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive { get; set; }
	}
}
