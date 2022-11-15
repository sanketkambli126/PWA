namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
	/// <summary>
	/// PMS Reschedule Rules Response ViewModel
	/// </summary>
	public class PMSRescheduleRulesResponseViewModel
	{
		/// <summary>
		/// Gets or sets the extended days note.
		/// </summary>
		/// <value>
		/// The extended days note.
		/// </value>
		public string ExtendedDaysNote { get; set; }

		/// <summary>
		/// Gets or sets the maximum counter extension value.
		/// </summary>
		/// <value>
		/// The maximum counter extension value.
		/// </value>
		public int MaximumCounterExtensionValue { get; set; }

		/// <summary>
		/// Gets or sets the months value.
		/// </summary>
		/// <value>
		/// The months value.
		/// </value>
		public int MaximumIntervalDays { get; set; }
	}
}
