using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Reschedule LinkedWork Order
	/// </summary>
	public class RescheduleLinkedWorkOrder
	{
		/// <summary>
		/// Gets or sets the rank description.
		/// </summary>
		/// <value>
		/// The rank description.
		/// </value>
		public string RankDescription { get; set; }

		/// <summary>
		/// Gets or sets the rank short code.
		/// </summary>
		/// <value>
		/// The rank short code.
		/// </value>
		public string RankShortCode { get; set; }

		/// <summary>
		/// Gets or sets the department.
		/// </summary>
		/// <value>
		/// The department.
		/// </value>
		public string Department { get; set; }

		/// <summary>
		/// Gets or sets the department short code.
		/// </summary>
		/// <value>
		/// The department short code.
		/// </value>
		public string DepartmentShortCode { get; set; }

		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		/// <value>
		/// The interval value.
		/// </value>
		public int? IntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the type of the interval.
		/// </summary>
		/// <value>
		/// The type of the interval.
		/// </value>
		public string IntervalType { get; set; }

		/// <summary>
		/// Gets or sets the interval identifier.
		/// </summary>
		/// <value>
		/// The interval identifier.
		/// </value>
		public string IntervalId { get; set; }

		/// <summary>
		/// Gets or sets the done date.
		/// </summary>
		/// <value>
		/// The done date.
		/// </value>
		public DateTime? DoneDate { get; set; }

		/// <summary>
		/// Gets or sets the closed date.
		/// </summary>
		/// <value>
		/// The closed date.
		/// </value>
		public DateTime? ClosedDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the job type description.
		/// </summary>
		/// <value>
		/// The job type description.
		/// </value>
		public string JobTypeDescription { get; set; }

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
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the schedule task identifier.
		/// </summary>
		/// <value>
		/// The schedule task identifier.
		/// </value>
		public string ScheduleTaskId { get; set; }

		/// <summary>
		/// Gets or sets the work order history identifier.
		/// </summary>
		/// <value>
		/// The work order history identifier.
		/// </value>
		public string WorkOrderHistoryId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the RLW identifier.
		/// </summary>
		/// <value>
		/// The RLW identifier.
		/// </value>
		public string RlwId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the work order identifier.
		/// </summary>
		/// <value>
		/// The work order identifier.
		/// </value>
		public string WorkOrderId { get; set; }

		/// <summary>
		/// Gets or sets the job type short code.
		/// </summary>
		/// <value>
		/// The job type short code.
		/// </value>
		public string JobTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsCritical { get; set; }
	}
}
