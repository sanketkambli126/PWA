using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Break In Passage Details
	/// </summary>
	public class BreakInPassageDetails
	{
		/// <summary>
		/// Gets or sets from.
		/// </summary>
		/// <value>
		/// From.
		/// </value>
		public DateTime From { get; set; }

		/// <summary>
		/// Gets or sets to.
		/// </summary>
		/// <value>
		/// To.
		/// </value>
		public DateTime To { get; set; }

		/// <summary>
		/// Gets or sets the type of the break.
		/// </summary>
		/// <value>
		/// The type of the break.
		/// </value>
		public string BreakType { get; set; }

		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>
		/// The distance.
		/// </value>
		public float? Distance { get; set; }
	}
}
