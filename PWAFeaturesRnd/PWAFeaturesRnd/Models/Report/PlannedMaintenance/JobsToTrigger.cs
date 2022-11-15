using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// JobsToTrigger
	/// </summary>
	public class JobsToTrigger
	{
		/// <summary>
		/// Gets or sets the PST identifier.
		/// </summary>
		/// <value>
		/// The PST identifier.
		/// </value>
		public string PstId { get; set; }

		/// <summary>
		/// Gets or sets the job class short code.
		/// </summary>
		/// <value>
		/// The job class short code.
		/// </value>
		public string JobClassShortCode { get; set; }

		/// <summary>
		/// Gets or sets the job class.
		/// </summary>
		/// <value>
		/// The job class.
		/// </value>
		public string JobClass { get; set; }

		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		/// <value>
		/// The interval value.
		/// </value>
		public int IntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the name of the interval.
		/// </summary>
		/// <value>
		/// The name of the interval.
		/// </value>
		public string IntervalName { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the PJB identifier.
		/// </summary>
		/// <value>
		/// The PJB identifier.
		/// </value>
		public string PjbId { get; set; }

		/// <summary>
		/// Gets or sets the color of the condition.
		/// </summary>
		/// <value>
		/// The color of the condition.
		/// </value>
		public string ConditionColor { get; set; }

		/// <summary>
		/// Gets or sets the condition description.
		/// </summary>
		/// <value>
		/// The condition description.
		/// </value>
		public string ConditionDescription { get; set; }

		/// <summary>
		/// Gets or sets the original due date.
		/// </summary>
		/// <value>
		/// The original due date.
		/// </value>
		public DateTime? OriginalDueDate { get; set; }

		/// <summary>
		/// Gets or sets the trigger due date.
		/// </summary>
		/// <value>
		/// The trigger due date.
		/// </value>
		public DateTime? TriggerDueDate { get; set; }
	}
}
