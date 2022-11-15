namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	/// <summary>
	/// Inspection Fleet Summary Response
	/// </summary>
	public class InspectionFleetSummaryResponse
	{
		/// <summary>
		/// Gets or sets the PSC detention count.
		/// </summary>
		/// <value>
		/// The PSC detention count.
		/// </value>
		public int PSCDetentionCount { get; set; }

		/// <summary>
		/// Gets or sets the PSC detention priority.
		/// </summary>
		/// <value>
		/// The PSC detention priority.
		/// </value>
		public int PSCDetentionPriority { get; set; }

		/// <summary>
		/// Gets or sets the PSC detention information.
		/// </summary>
		/// <value>
		/// The PSC detention information.
		/// </value>
		public string PSCDetentionInfo { get; set; }

		/// <summary>
		/// Gets or sets the PSC deficiency count.
		/// </summary>
		/// <value>
		/// The PSC deficiency count.
		/// </value>
		public int PSCDeficiencyCount { get; set; }

		/// <summary>
		/// Gets or sets the PSC deficiency rate.
		/// </summary>
		/// <value>
		/// The PSC deficiency rate.
		/// </value>
		public decimal PscDeficiencyRate { get; set; }

		/// <summary>
		/// Gets or sets the PSC deficiency information.
		/// </summary>
		/// <value>
		/// The PSC deficiency information.
		/// </value>
		public string PscDeficiencyInfo { get; set; }

		/// <summary>
		/// Gets or sets the PSC deficiency inspection count.
		/// </summary>
		/// <value>
		/// The PSC deficiency inspection count.
		/// </value>
		public int PscDeficiencyInspectionCount { get; set; }

		/// <summary>
		/// Gets or sets the PSC deficiency priority.
		/// </summary>
		/// <value>
		/// The PSC deficiency priority.
		/// </value>
		public int PSCDeficiencyPriority { get; set; }

		/// <summary>
		/// Gets or sets the omv findings rate.
		/// </summary>
		/// <value>
		/// The omv findings rate.
		/// </value>
		public decimal OMVFindingsRate { get; set; }

		/// <summary>
		/// Gets or sets the omv findings priority.
		/// </summary>
		/// <value>
		/// The omv findings priority.
		/// </value>
		public int OMVFindingsPriority { get; set; }

		/// <summary>
		/// Gets or sets the omv findings information.
		/// </summary>
		/// <value>
		/// The omv findings information.
		/// </value>
		public string OMVFindingsInfo { get; set; }

		/// <summary>
		/// Gets or sets the omv findings count.
		/// </summary>
		/// <value>
		/// The omv findings count.
		/// </value>
		public int OMVFindingsCount { get; set; }

		/// <summary>
		/// Gets or sets the omv inspections count.
		/// </summary>
		/// <value>
		/// The omv inspections count.
		/// </value>
		public int OMVInspectionsCount { get; set; }

		/// <summary>
		/// Gets or sets the overdue inspection count.
		/// </summary>
		/// <value>
		/// The overdue inspection count.
		/// </value>
		public int OverdueInspectionCount { get; set; }

		/// <summary>
		/// Gets or sets the overdue inspection priority.
		/// </summary>
		/// <value>
		/// The overdue inspection priority.
		/// </value>
		public int OverdueInspectionPriority { get; set; }

		/// <summary>
		/// Gets or sets the overdue inspection information.
		/// </summary>
		/// <value>
		/// The overdue inspection information.
		/// </value>
		public string OverdueInspectionInfo { get; set; }
	}
}
