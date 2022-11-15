namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// PMS Header Detail
	/// </summary>
	public class PMSHeaderDetail
	{
		/// <summary>
		/// Gets or sets the work order due count.
		/// </summary>
		/// <value>
		/// The work order due count.
		/// </value>
		public int WorkOrderDueCount { get; set; }

		/// <summary>
		/// Gets or sets the work order overdue count.
		/// </summary>
		/// <value>
		/// The work order overdue count.
		/// </value>
		public int WorkOrderOverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the work order done count.
		/// </summary>
		/// <value>
		/// The work order done count.
		/// </value>
		public int WorkOrderDoneCount { get; set; }

		/// <summary>
		/// Gets or sets the work order overdue percentage.
		/// </summary>
		/// <value>
		/// The work order overdue percentage.
		/// </value>
		public int WorkOrderOverduePercentage { get; set; }

		/// <summary>
		/// Gets or sets the critical component wo due count.
		/// </summary>
		/// <value>
		/// The critical component wo due count.
		/// </value>
		public int CriticalComponentWODueCount { get; set; }

		/// <summary>
		/// Gets or sets the critical component wo overdue count.
		/// </summary>
		/// <value>
		/// The critical component wo overdue count.
		/// </value>
		public int CriticalComponentWOOverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the critical component wo done count.
		/// </summary>
		/// <value>
		/// The critical component wo done count.
		/// </value>
		public int CriticalComponentWODoneCount { get; set; }

		/// <summary>
		/// Gets or sets the critical component wo overdue percentage.
		/// </summary>
		/// <value>
		/// The critical component wo overdue percentage.
		/// </value>
		public int CriticalComponentWOOverduePercentage { get; set; }

		/// <summary>
		/// Gets or sets the ships work order planned count.
		/// </summary>
		/// <value>
		/// The ships work order planned count.
		/// </value>
		public int ShipsWorkOrderPlannedCount { get; set; }

		/// <summary>
		/// Gets or sets the ships work order done count.
		/// </summary>
		/// <value>
		/// The ships work order done count.
		/// </value>
		public int ShipsWorkOrderDoneCount { get; set; }
	}
}
