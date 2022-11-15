using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Sea Passage Activity
	/// </summary>
	public class SeaPassageActivity
	{
		/// <summary>
		/// Gets or sets the activity details.
		/// </summary>
		/// <value>
		/// The activity details.
		/// </value>
		public List<SeaPassageReportDetails> ActivityDetails { get; set; }

		/// <summary>
		/// Gets or sets the last report.
		/// </summary>
		/// <value>
		/// The last report.
		/// </value>
		public SeaPassageReportPreview LastReport { get; set; }

		/// <summary>
		/// Gets or sets the charterer.
		/// </summary>
		/// <value>
		/// The charterer.
		/// </value>
		public string Charterer { get; set; }

		/// <summary>
		/// Gets or sets the total time.
		/// </summary>
		/// <value>
		/// The total time.
		/// </value>
		public string TotalTime { get; set; }

		/// <summary>
		/// Gets or sets the total distance.
		/// </summary>
		/// <value>
		/// The total distance.
		/// </value>
		public float? TotalDistance { get; set; }

		/// <summary>
		/// Gets or sets the average speed.
		/// </summary>
		/// <value>
		/// The average speed.
		/// </value>
		public float? AverageSpeed { get; set; }

		/// <summary>
		/// Gets or sets the total fo.
		/// </summary>
		/// <value>
		/// The total fo.
		/// </value>
		public float? TotalFo { get; set; }

		/// <summary>
		/// Gets or sets the total lsfo.
		/// </summary>
		/// <value>
		/// The total lsfo.
		/// </value>
		public float? TotalLsfo { get; set; }

		/// <summary>
		/// Gets or sets the total do.
		/// </summary>
		/// <value>
		/// The total do.
		/// </value>
		public float? TotalDo { get; set; }

		/// <summary>
		/// Gets or sets the total go.
		/// </summary>
		/// <value>
		/// The total go.
		/// </value>
		public float? TotalGo { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has faop report.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has faop report; otherwise, <c>false</c>.
		/// </value>
		public bool HasFaopReport { get; set; }
	}
}
