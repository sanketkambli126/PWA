using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Approval
{
	/// <summary>
	/// Approval purchase order request
	/// </summary>
	public class ApprovalPurchaseOrderRequest
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
		/// Gets or sets the order stages.
		/// </summary>
		/// <value>
		/// The order stages.
		/// </value>
		public List<string> OrderStages { get; set; }

		/// <summary>
		/// Gets or sets the order statuses.
		/// </summary>
		/// <value>
		/// The order statuses.
		/// </value>
		public List<string> OrderStatuses { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show only authentication required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show only authentication required]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowOnlyAuthRequired { get; set; }
	}
}
