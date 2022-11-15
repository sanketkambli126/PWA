namespace PWAFeaturesRnd.ViewModels.Finance
{
	/// <summary>
	/// Over Budget Details Request View Model
	/// </summary>
	public class OverBudgetDetailsRequestViewModel
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
		/// Gets or sets the encrypted vessel identifier.
		/// Not an API contract property.
		/// </summary>
		/// <value>
		/// The encrypted vessel identifier.
		/// </value>
		public string EncryptedVesselId { get; set; }
	}
}
