using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Approval
{
	/// <summary>
	/// Approval Purchase Order Request View Model
	/// </summary>
	public class ApprovalPurchaseOrderRequestViewModel
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
		/// Gets or sets the type of the node.
		/// </summary>
		/// <value>
		/// The type of the node.
		/// </value>
		public string NodeType { get; set; }
	}
}
