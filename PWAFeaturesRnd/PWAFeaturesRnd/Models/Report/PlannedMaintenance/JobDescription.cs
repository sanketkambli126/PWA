namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Job Description
	/// </summary>
	public class JobDescription
	{
		/// <summary>
		/// Gets or sets the PJD identifier.
		/// </summary>
		/// <value>
		/// The PJD identifier.
		/// </value>
		public string PjdId { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the PGT identifier.
		/// </summary>
		/// <value>
		/// The PGT identifier.
		/// </value>
		public string PgtId { get; set; }

		/// <summary>
		/// Gets or sets the guideline text.
		/// </summary>
		/// <value>
		/// The guideline text.
		/// </value>
		public string GuidelineText { get; set; }
	}
}
