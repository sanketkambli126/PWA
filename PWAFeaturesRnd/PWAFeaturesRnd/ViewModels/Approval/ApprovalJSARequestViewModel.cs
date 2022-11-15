namespace PWAFeaturesRnd.ViewModels.Approval
{
	/// <summary>
	/// Approval JSA Request ViewModel
	/// </summary>
	public class ApprovalJSARequestViewModel
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
		/// Gets or sets the Encrypted vessel identifier.
		/// </summary>
		/// <value>
		/// The Encrypted vessel identifier.
		/// </value>
		public string EncryptedVesselId { get; set; }

		/// <summary>
		/// Gets or sets the type of the node.
		/// </summary>
		/// <value>
		/// The type of the node.
		/// </value>
		public string NodeType { get; set; }
	}
}
