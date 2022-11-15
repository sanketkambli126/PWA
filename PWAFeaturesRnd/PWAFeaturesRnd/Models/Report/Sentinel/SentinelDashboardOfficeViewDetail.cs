namespace PWAFeaturesRnd.Models.Report.Sentinel
{
	/// <summary>
	/// Sentinel Dashboard Office View Detail
	/// </summary>
	public class SentinelDashboardOfficeViewDetail
	{
		/// <summary>
		/// Gets or sets the office identifier.
		/// </summary>
		/// <value>
		/// The office identifier.
		/// </value>
		public string OfficeId { get; set; }

		/// <summary>
		/// Gets or sets the name of the office.
		/// </summary>
		/// <value>
		/// The name of the office.
		/// </value>
		public string OfficeName { get; set; }

		/// <summary>
		/// Gets or sets the total vessel count.
		/// </summary>
		/// <value>
		/// The total vessel count.
		/// </value>
		public int? TotalVesselCount { get; set; }

		/// <summary>
		/// Gets or sets the lowest score.
		/// </summary>
		/// <value>
		/// The lowest score.
		/// </value>
		public decimal? LowestScore { get; set; }

		/// <summary>
		/// Gets or sets the color of the lowest score.
		/// </summary>
		/// <value>
		/// The color of the lowest score.
		/// </value>
		public string LowestScoreColor { get; set; }

		/// <summary>
		/// Gets or sets the average score.
		/// </summary>
		/// <value>
		/// The average score.
		/// </value>
		public decimal? AverageScore { get; set; }

		/// <summary>
		/// Gets or sets the average color of the score.
		/// </summary>
		/// <value>
		/// The average color of the score.
		/// </value>
		public string AverageScoreColor { get; set; }

		/// <summary>
		/// Gets or sets the highest score.
		/// </summary>
		/// <value>
		/// The highest score.
		/// </value>
		public decimal? HighestScore { get; set; }

		/// <summary>
		/// Gets or sets the color of the highest score.
		/// </summary>
		/// <value>
		/// The color of the highest score.
		/// </value>
		public string HighestScoreColor { get; set; }

		/// <summary>
		/// Gets or sets the new to management vessel count.
		/// </summary>
		/// <value>
		/// The new to management vessel count.
		/// </value>
		public int? NewToManagementVesselCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is all office row.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is all office row; otherwise, <c>false</c>.
		/// </value>
		public bool IsAllOfficeRow { get; set; }

		/// <summary>
		/// Gets or sets the active override count.
		/// </summary>
		/// <value>
		/// The active override count.
		/// </value>
		public string ActiveOverrideCount { get; set; }

        /// <summary>
        /// Gets or sets the override count.
        /// </summary>
        /// <value>
        /// The override count.
        /// </value>
        public int? OverrideCount { get; set; }
	}
}
