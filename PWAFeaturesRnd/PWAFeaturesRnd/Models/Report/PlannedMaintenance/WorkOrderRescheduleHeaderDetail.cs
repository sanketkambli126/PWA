using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Work Order Reschedule Header Detail
	/// </summary>
	public class WorkOrderRescheduleHeaderDetail
	{
		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// Gets or sets the por request identifier.
		/// </summary>
		/// <value>
		/// The por request identifier.
		/// </value>
		public string PorRequestId { get; set; }

		/// <summary>
		/// Gets or sets the por identifier.
		/// </summary>
		/// <value>
		/// The por identifier.
		/// </value>
		public string PorId { get; set; }

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
		/// Gets or sets the reschedule status short code.
		/// </summary>
		/// <value>
		/// The reschedule status short code.
		/// </value>
		public string RescheduleStatusShortCode { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status identifier.
		/// </summary>
		/// <value>
		/// The reschedule status identifier.
		/// </value>
		public string RescheduleStatusId { get; set; }

		/// <summary>
		/// Gets or sets the interval to value.
		/// </summary>
		/// <value>
		/// The interval to value.
		/// </value>
		public int? IntervalToValue { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status.
		/// </summary>
		/// <value>
		/// The reschedule status.
		/// </value>
		public string RescheduleStatus { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		/// <value>
		/// The interval value.
		/// </value>
		public int? IntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the short name of the interval type.
		/// </summary>
		/// <value>
		/// The short name of the interval type.
		/// </value>
		public string IntervalTypeShortName { get; set; }

		/// <summary>
		/// Gets or sets the due date interval type identifier.
		/// </summary>
		/// <value>
		/// The due date interval type identifier.
		/// </value>
		public string DueDateIntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the pwo identifier.
		/// </summary>
		/// <value>
		/// The pwo identifier.
		/// </value>
		public string PwoId { get; set; }

		/// <summary>
		/// Gets or sets the guideline text.
		/// </summary>
		/// <value>
		/// The guideline text.
		/// </value>
		public string GuidelineText { get; set; }

		/// <summary>
		/// Gets or sets the type of the job interval.
		/// </summary>
		/// <value>
		/// The type of the job interval.
		/// </value>
		public string JobIntervalType { get; set; }
	}
}
