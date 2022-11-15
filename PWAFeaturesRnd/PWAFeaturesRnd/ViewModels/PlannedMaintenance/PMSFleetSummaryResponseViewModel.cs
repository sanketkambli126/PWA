namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
	/// <summary>
	/// PMS Fleet Summary Response VM
	/// </summary>
	public class PMSFleetSummaryResponseViewModel
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
