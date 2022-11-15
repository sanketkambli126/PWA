using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Tres Vessel Report List
	/// </summary>
	public class TresVesselReportList
	{
		/// <summary>
		/// Gets or sets the tres event identifier.
		/// </summary>
		/// <value>
		/// The tres event identifier.
		/// </value>
		public string TresEventId { get; set; }

		/// <summary>
		/// Gets or sets the event date time.
		/// </summary>
		/// <value>
		/// The event date time.
		/// </value>
		public DateTime? EventDateTime { get; set; }

		/// <summary>
		/// Gets or sets the last updated date.
		/// </summary>
		/// <value>
		/// The last updated date.
		/// </value>
		public DateTime? LastUpdatedDate { get; set; }

		/// <summary>
		/// Gets or sets the report number.
		/// </summary>
		/// <value>
		/// The report number.
		/// </value>
		public string ReportNumber { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the submission date.
		/// </summary>
		/// <value>
		/// The submission date.
		/// </value>
		public DateTime? SubmissionDate { get; set; }

		/// <summary>
		/// Gets or sets the type of the tres event.
		/// </summary>
		/// <value>
		/// The type of the tres event.
		/// </value>
		public string TresEventType { get; set; }
	}
}
