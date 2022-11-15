using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Position List Preview Event
	/// </summary>
	public class PositionListPreviewEvent
	{
		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public PositionListEventStatus Status { get; set; }

		/// <summary>
		/// Gets or sets the event date.
		/// </summary>
		/// <value>
		/// The event date.
		/// </value>
		public DateTime? EventDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is estimated event date.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is estimated event date; otherwise, <c>false</c>.
		/// </value>
		public bool IsEstimatedEventDate { get; set; }
	}
}
