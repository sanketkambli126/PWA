using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Approval
{
	/// <summary>
	/// PMS Pending Reschedule Request View Model
	/// </summary>
	public class ApprovalPmsRescheduleRequestViewModel
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



	}
}
