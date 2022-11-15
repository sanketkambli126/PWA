namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
	/// <summary>
	/// 
	/// </summary>
	public class ThirdPartyAccidentDetail
	{
		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the imc identifier.
		/// </summary>
		/// <value>
		/// The imc identifier.
		/// </value>
		public string ImcId { get; set; }

		/// <summary>
		/// Gets or sets the classification short code.
		/// </summary>
		/// <value>
		/// The classification short code.
		/// </value>
		public string ClassificationShortCode { get; set; }

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

		/// <summary>
		/// Gets or sets the classification.
		/// </summary>
		/// <value>
		/// The classification.
		/// </value>
		public string Classification { get; set; }

		/// <summary>
		/// Gets or sets the classification ids.
		/// </summary>
		/// <value>
		/// The classification ids.
		/// </value>
		public string ClassificationIds { get; set; }
	}
}