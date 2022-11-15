using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// 
	/// </summary>
	public class DelayListViewModel
	{
		/// <summary>
		/// Gets or sets the activity description.
		/// </summary>
		/// <value>
		/// The activity description.
		/// </value>
		public string ActivityDescription { get; set; }

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
		/// Gets or sets the duration of the delay.
		/// </summary>
		/// <value>
		/// The duration of the delay.
		/// </value>
		public TimeSpan? DelayDuration { get; set; }

		/// <summary>
		/// Gets or sets the delay duration hours.
		/// </summary>
		/// <value>
		/// The delay duration hours.
		/// </value>
		public string DelayDurationHours { get; set; }

		/// <summary>
		/// Gets or sets the delay duration minutes.
		/// </summary>
		/// <value>
		/// The delay duration minutes.
		/// </value>
		public string DelayDurationMinutes { get; set; }
	}
}
