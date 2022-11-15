namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// Defect Report Work Order Symptom
	/// </summary>
	public class DefectReportWorkOrderSymptom
	{
		/// <summary>
		/// Gets or sets the DRS identifier.
		/// </summary>
		/// <value>
		/// The DRS identifier.
		/// </value>
		public string DrsId { get; set; }

		/// <summary>
		/// Gets or sets the DRW identifier.
		/// </summary>
		/// <value>
		/// The DRW identifier.
		/// </value>
		public string DrwId { get; set; }

		/// <summary>
		/// Gets or sets the PWS identifier.
		/// </summary>
		/// <value>
		/// The PWS identifier.
		/// </value>
		public string PwsId { get; set; }

		/// <summary>
		/// Gets or sets the symptom description.
		/// </summary>
		/// <value>
		/// The symptom description.
		/// </value>
		public string SymptomDescription { get; set; }

		/// <summary>
		/// Gets or sets the symptom comment.
		/// </summary>
		/// <value>
		/// The symptom comment.
		/// </value>
		public string SymptomComment { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }
	}
}
