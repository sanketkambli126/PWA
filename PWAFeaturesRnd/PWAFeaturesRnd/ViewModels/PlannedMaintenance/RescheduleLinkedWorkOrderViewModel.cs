using System;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
	/// <summary>
	/// Reschedule Linked Work Order View Model
	/// </summary>
	public class RescheduleLinkedWorkOrderViewModel
	{
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
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the interval.
		/// </summary>
		/// <value>
		/// The interval.
		/// </value>
		public string Interval { get; set; }

		/// <summary>
		/// Gets or sets the department.
		/// </summary>
		/// <value>
		/// The department.
		/// </value>
		public string Department { get; set; }

		/// <summary>
		/// Gets or sets the responsibility.
		/// </summary>
		/// <value>
		/// The responsibility.
		/// </value>
		public string Responsibility { get; set; }
	}
}
