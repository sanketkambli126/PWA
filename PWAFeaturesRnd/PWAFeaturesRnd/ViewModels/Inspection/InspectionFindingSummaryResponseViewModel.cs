namespace PWAFeaturesRnd.ViewModels.Inspection
{
	/// <summary>
	/// 
	/// </summary>
	public class InspectionFindingSummaryResponseViewModel
	{
		/// <summary>
		/// Gets or sets the inspection finding identifier.
		/// </summary>
		/// <value>
		/// The inspection finding identifier.
		/// </value>
		public int InspectionFindingId { get; set; }

		/// <summary>
		/// Gets or sets all finding count.
		/// </summary>
		/// <value>
		/// All finding count.
		/// </value>
		public int AllFindingCount { get; set; }

		/// <summary>
		/// Gets or sets the cleared count.
		/// </summary>
		/// <value>
		/// The cleared count.
		/// </value>
		public int ClearedCount { get; set; }

		/// <summary>
		/// Gets or sets the outstanding count.
		/// </summary>
		/// <value>
		/// The outstanding count.
		/// </value>
		public int OutstandingCount { get; set; }

		/// <summary>
		/// Gets or sets the overdue count.
		/// </summary>
		/// <value>
		/// The overdue count.
		/// </value>
		public int OverdueCount { get; set; }
	}
}
