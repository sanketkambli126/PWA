using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Port Call Header Detail
	/// </summary>
	public class PortCallHeaderDetail
	{
		/// <summary>
		/// Gets or sets the eosp.
		/// </summary>
		/// <value>
		/// The eosp.
		/// </value>
		public DateTime? EOSP { get; set; }

		/// <summary>
		/// Gets or sets the last updated date.
		/// </summary>
		/// <value>
		/// The last updated date.
		/// </value>
		public DateTime? LastUpdatedDate { get; set; }

		/// <summary>
		/// Gets or sets the event date for validation.
		/// </summary>
		/// <value>
		/// The event date for validation.
		/// </value>
		public DateTime? EventDateForValidation { get; set; }

		/// <summary>
		/// Gets or sets the latest noon report date.
		/// </summary>
		/// <value>
		/// The latest noon report date.
		/// </value>
		public DateTime? LatestNoonReportDate { get; set; }

		/// <summary>
		/// Gets or sets the latest rob event date.
		/// </summary>
		/// <value>
		/// The latest rob event date.
		/// </value>
		public DateTime? LatestRobEventDate { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the berth.
		/// </summary>
		/// <value>
		/// The berth.
		/// </value>
		public string Berth { get; set; }

		/// <summary>
		/// Gets or sets the faop date.
		/// </summary>
		/// <value>
		/// The faop date.
		/// </value>
		public DateTime? FaopDate { get; set; }
	}
}
