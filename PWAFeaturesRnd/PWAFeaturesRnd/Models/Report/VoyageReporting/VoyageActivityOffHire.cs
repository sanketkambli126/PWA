using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Voyage Activity Off Hire
	/// </summary>
	public class VoyageActivityOffHire
	{
		/// <summary>
		/// Gets or sets the activity identifier.
		/// </summary>
		/// <value>
		/// The activity identifier.
		/// </value>
		public string ActivityId { get; set; }

		/// <summary>
		/// Gets or sets the activity description.
		/// </summary>
		/// <value>
		/// The activity description.
		/// </value>
		public string ActivityDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is sea passage event.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is sea passage event; otherwise, <c>false</c>.
		/// </value>
		public bool IsSeaPassageEvent { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

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
		/// Gets or sets the voyage identifier.
		/// </summary>
		/// <value>
		/// The voyage identifier.
		/// </value>
		public string VoyageId { get; set; }

		/// <summary>
		/// Gets or sets the event description.
		/// </summary>
		/// <value>
		/// The event description.
		/// </value>
		public string EventDescription { get; set; }

		/// <summary>
		/// Gets or sets the event identifier.
		/// </summary>
		/// <value>
		/// The event identifier.
		/// </value>
		public string EventId { get; set; }
	}
}
