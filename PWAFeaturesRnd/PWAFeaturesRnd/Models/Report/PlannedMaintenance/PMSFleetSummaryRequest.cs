namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// PMS Fleet Summary Request
	/// </summary>
	public class PMSFleetSummaryRequest
	{
		/// <summary>
		/// Gets or sets the fleet identifier.
		/// </summary>
		/// <value>
		/// The fleet identifier.
		/// </value>
		public string FleetId { get; set; }

		/// <summary>
		/// Gets or sets the type of the menu.
		/// </summary>
		/// <value>
		/// The type of the menu.
		/// </value>
		public string MenuType { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the critical pmspriority.
		/// </summary>
		/// <value>
		/// The critical pmspriority.
		/// </value>
		public int CriticalPmspriority { get; set; }
	}
}
