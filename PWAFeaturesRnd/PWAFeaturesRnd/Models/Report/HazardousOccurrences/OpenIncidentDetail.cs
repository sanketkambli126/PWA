namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
	/// <summary>
	/// 
	/// </summary>
	public class OpenIncidentDetail
	{
		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the severity identifier.
		/// </summary>
		/// <value>
		/// The severity identifier.
		/// </value>
		public string SeverityId { get; set; }

		/// <summary>
		/// Gets or sets the severity.
		/// </summary>
		/// <value>
		/// The severity.
		/// </value>
		public string Severity { get; set; }

		/// <summary>
		/// Gets or sets the total count.
		/// </summary>
		/// <value>
		/// The total count.
		/// </value>
		public int TotalCount { get; set; }

		/// <summary>
		/// Gets or sets the sort order.
		/// </summary>
		/// <value>
		/// The sort order.
		/// </value>
		public int SortOrder { get; set; }
	}
}