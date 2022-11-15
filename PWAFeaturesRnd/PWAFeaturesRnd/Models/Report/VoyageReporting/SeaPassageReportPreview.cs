using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Sea Passage Report Preview
	/// </summary>
	public class SeaPassageReportPreview
	{
		/// <summary>
		/// Gets or sets the steaming time.
		/// </summary>
		/// <value>
		/// The steaming time.
		/// </value>
		public string SteamingTime { get; set; }

		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>
		/// The distance.
		/// </value>
		public float? Distance { get; set; }

		/// <summary>
		/// Gets or sets the average speed.
		/// </summary>
		/// <value>
		/// The average speed.
		/// </value>
		public float? AverageSpeed { get; set; }

		/// <summary>
		/// Gets or sets the total distance.
		/// </summary>
		/// <value>
		/// The total distance.
		/// </value>
		public float? TotalDistance { get; set; }

		/// <summary>
		/// Gets or sets the distance to go.
		/// </summary>
		/// <value>
		/// The distance to go.
		/// </value>
		public float? DistanceToGo { get; set; }

		/// <summary>
		/// Gets or sets the name of the activity.
		/// </summary>
		/// <value>
		/// The name of the activity.
		/// </value>
		public string ActivityName { get; set; }

		/// <summary>
		/// Gets or sets the activity date.
		/// </summary>
		/// <value>
		/// The activity date.
		/// </value>
		public DateTime? ActivityDate { get; set; }
	}
}
