﻿using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Approval
{
	/// <summary>
	/// PMS Pending Approvals Request View Model
	/// </summary>
	public class ApprovalPmsRequestViewModel
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
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the work order status ids.
		/// </summary>
		/// <value>
		/// The work order status ids.
		/// </value>
		public List<string> WorkOrderStatusIds { get; set; }
		/// <summary>
		/// Gets or sets the type of the node.
		/// </summary>
		/// <value>
		/// The type of the node.
		/// </value>
		public string NodeType { get; set; }
	}
}
