using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Voyage Activity Delay
	/// </summary>
	public class VoyageActivityDelay
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

		/// <summary>
		/// Gets or sets a value indicating whether this instance is off hire.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
		/// </value>
		public bool IsOffHire { get; set; }

		/// <summary>
		/// Gets or sets the type of the off hire.
		/// </summary>
		/// <value>
		/// The type of the off hire.
		/// </value>
		public string OffHireType { get; set; }

		/// <summary>
		/// Gets or sets the comments.
		/// </summary>
		/// <value>
		/// The comments.
		/// </value>
		public string Comments { get; set; }
	}
}
