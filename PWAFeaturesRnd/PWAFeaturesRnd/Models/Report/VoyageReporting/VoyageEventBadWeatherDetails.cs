using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Voyage Event Bad Weather Details
	/// </summary>
	public class VoyageEventBadWeatherDetails
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

		/// <summary>
		/// Gets or sets the maximum wind force scale.
		/// </summary>
		/// <value>
		/// The maximum wind force scale.
		/// </value>
		public int? MaxWindForceScale { get; set; }

		/// <summary>
		/// Gets or sets the maximum length of the swell.
		/// </summary>
		/// <value>
		/// The maximum length of the swell.
		/// </value>
		public string MaxSwellLength { get; set; }

		/// <summary>
		/// Gets or sets the event identifier.
		/// </summary>
		/// <value>
		/// The event identifier.
		/// </value>
		public string EventId { get; set; }
	}
}
