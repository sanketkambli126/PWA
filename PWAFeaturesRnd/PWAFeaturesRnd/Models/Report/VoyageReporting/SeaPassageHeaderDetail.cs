using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Sea Passage Header Detail
	/// </summary>
	public class SeaPassageHeaderDetail
	{
		/// <summary>
		/// Gets or sets the position list identifier.
		/// </summary>
		/// <value>
		/// The position list identifier.
		/// </value>
		public string PositionListId { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>
		/// The start date.
		/// </value>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>
		/// The end date.
		/// </value>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// Gets or sets the last updated event date.
		/// </summary>
		/// <value>
		/// The last updated event date.
		/// </value>
		public DateTime? LastUpdatedEventDate { get; set; }

		/// <summary>
		/// Gets or sets the event date for validation.
		/// </summary>
		/// <value>
		/// The event date for validation.
		/// </value>
		public DateTime? EventDateForValidation { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }
	}
}
