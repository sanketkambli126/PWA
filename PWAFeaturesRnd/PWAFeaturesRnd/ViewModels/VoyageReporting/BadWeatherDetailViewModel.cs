using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
	/// <summary>
	/// BadWeatherDetailViewModel
	/// </summary>
	public class BadWeatherDetailViewModel
	{
		/// <summary>
		/// Gets or sets the name of the event.
		/// </summary>
		/// <value>
		/// The name of the event.
		/// </value>
		public string EventName { get; set; }

		/// <summary>
		/// Gets or sets the event date.
		/// </summary>
		/// <value>
		/// The event date.
		/// </value>
		public DateTime EventDate { get; set; }

		/// <summary>
		/// Gets or sets the maximum wind force.
		/// </summary>
		/// <value>
		/// The maximum wind force.
		/// </value>
		public string MaxWindForce { get; set; }

		/// <summary>
		/// Gets or sets the maximum swell length dscription.
		/// </summary>
		/// <value>
		/// The maximum swell length dscription.
		/// </value>
		public string MaxSwellLengthDscription { get; set; }

	}
}
