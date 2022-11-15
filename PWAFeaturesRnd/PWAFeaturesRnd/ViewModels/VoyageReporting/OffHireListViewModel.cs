using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// Off Hire List View Model
	/// </summary>
	public class OffHireListViewModel
	{
		/// <summary>
		/// Gets or sets the activity.
		/// </summary>
		/// <value>
		/// The activity.
		/// </value>
		public string Activity { get; set; }

		/// <summary>
		/// Gets or sets the date to.
		/// </summary>
		/// <value>
		/// The date to.
		/// </value>
		public DateTime? DateTo { get; set; }

		/// <summary>
		/// Gets or sets the date from.
		/// </summary>
		/// <value>
		/// The date from.
		/// </value>
		public DateTime? DateFrom { get; set; }

		/// <summary>
		/// Gets or sets the duration of the off hire.
		/// </summary>
		/// <value>
		/// The duration of the off hire.
		/// </value>
		public TimeSpan? OffHireDuration { get; set; }

		/// <summary>
		/// Gets or sets the off hire duration hours.
		/// </summary>
		/// <value>
		/// The off hire duration hours.
		/// </value>
		public string OffHireDurationHours { get; set; }

		/// <summary>
		/// Gets or sets the off hire duration minutes.
		/// </summary>
		/// <value>
		/// The off hire duration minutes.
		/// </value>
		public string OffHireDurationMinutes { get; set; }
	}
}
