using System;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class RescheduleMappedWODetailViewModel
	{
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime? DueDate { get; set; }

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
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the interval.
		/// </summary>
		/// <value>
		/// The interval.
		/// </value>
		public string Interval { get; set; }

		/// <summary>
		/// Gets or sets the responsibility.
		/// </summary>
		/// <value>
		/// The responsibility.
		/// </value>
		public string Responsibility { get; set; }
	}
}
