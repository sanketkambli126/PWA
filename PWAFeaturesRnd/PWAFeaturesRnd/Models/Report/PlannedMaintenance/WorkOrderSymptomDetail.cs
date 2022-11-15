namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// WorkOrderSymptomDetail
	/// </summary>
	public class WorkOrderSymptomDetail
	{
		/// <summary>
		/// Gets or sets the PHS identifier.
		/// </summary>
		/// <value>
		/// The PHS identifier.
		/// </value>
		public string PhsId { get; set; }

		/// <summary>
		/// Gets or sets the PWH identifier.
		/// </summary>
		/// <value>
		/// The PWH identifier.
		/// </value>
		public string PwhId { get; set; }

		/// <summary>
		/// Gets or sets the PWS identifier.
		/// </summary>
		/// <value>
		/// The PWS identifier.
		/// </value>
		public string PwsId { get; set; }

		/// <summary>
		/// Gets or sets the is deleted.
		/// </summary>
		/// <value>
		/// The is deleted.
		/// </value>
		public bool? IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the symptom comment.
		/// </summary>
		/// <value>
		/// The symptom comment.
		/// </value>
		public string SymptomComment { get; set; }

		/// <summary>
		/// Gets or sets the symptom description.
		/// </summary>
		/// <value>
		/// The symptom description.
		/// </value>
		public string SymptomDescription { get; set; }
	}
}
