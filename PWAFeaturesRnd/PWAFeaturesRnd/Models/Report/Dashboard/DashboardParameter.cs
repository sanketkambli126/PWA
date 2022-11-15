namespace PWAFeaturesRnd.Models.Report.Dashboard
{
	/// <summary>
	/// Dashboard Parameter
	/// </summary>
	public class DashboardParameter
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
		/// Gets or sets the vessel id.
		/// </summary>
		/// <value>
		/// The vessel id.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is fleet selection.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is fleet selection; otherwise, <c>false</c>.
		/// </value>
		public bool IsFleetSelection { get; set; }


		/// <summary>
		/// Gets or sets the active mobile tab class.
		/// </summary>
		/// <value>
		/// The active mobile tab class.
		/// </value>
		public string ActiveMobileTabClass { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselIdentifier { get; set; }


		/// <summary>
		/// Gets or sets the mobile vessel identifier.
		/// </summary>
		/// <value>
		/// The mobile vessel identifier.
		/// </value>
		public string MobileVesselId { get; set; }
	}
}
