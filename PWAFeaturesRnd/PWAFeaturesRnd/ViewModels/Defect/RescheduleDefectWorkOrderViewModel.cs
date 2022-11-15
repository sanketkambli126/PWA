using System;

namespace PWAFeaturesRnd.ViewModels.Defect
{
	/// <summary>
	/// RescheduleDefectWorkOrderViewModel
	/// </summary>
	public class RescheduleDefectWorkOrderViewModel
    {
		/// <summary>
		/// Gets or sets the previous due date.
		/// </summary>
		/// <value>
		/// The previous due date.
		/// </value>
		public DateTime PreviousDueDate { get; set; }

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
		/// Gets or sets the requester role.
		/// </summary>
		/// <value>
		/// The requester role.
		/// </value>
		public string RequesterRole { get; set; }

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
		/// Gets or sets a value indicating whether this instance is status short code.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is status short code; otherwise, <c>false</c>.
		/// </value>
		public bool IsStatusShortCode { get; set; }

		/// <summary>
		/// Gets or sets the status colour.
		/// </summary>
		/// <value>
		/// The status colour.
		/// </value>
		public string StatusColour { get; set; }
	}
}
