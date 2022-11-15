namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// PMS Fleet Summary Response
	/// </summary>
	public class PMSFleetSummaryResponse
	{
		/// <summary>
		/// Gets or sets the critical PMS count.
		/// </summary>
		/// <value>
		/// The critical PMS count.
		/// </value>
		public int CriticalPMSCount { get; set; }

		/// <summary>
		/// Gets or sets the critical PMS priority.
		/// </summary>
		/// <value>
		/// The critical PMS priority.
		/// </value>
		public int CriticalPMSPriority { get; set; }

		/// <summary>
		/// Gets or sets the critical PMS information.
		/// </summary>
		/// <value>
		/// The critical PMS information.
		/// </value>
		public string CriticalPMSInfo { get; set; }
	}
}
