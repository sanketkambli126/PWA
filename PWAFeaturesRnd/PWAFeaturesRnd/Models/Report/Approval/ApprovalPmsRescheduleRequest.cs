using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Approval
{
	/// <summary>
	/// Approval PMS Pending Reschedule  request
	/// </summary>
	public class ApprovalPmsRescheduleRequest
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


	}
}
