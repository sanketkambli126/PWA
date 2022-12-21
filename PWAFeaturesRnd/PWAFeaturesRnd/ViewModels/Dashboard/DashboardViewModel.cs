using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.Dashboard;

namespace PWAFeaturesRnd.ViewModels.Dashboard
{
	/// <summary>
	/// Dashboard View Model
	/// </summary>
	public class DashboardViewModel
	{
		/// <summary>
		/// Gets or sets the vessel lists.
		/// </summary>
		/// <value>
		/// The vessel lists.
		/// </value>
		public List<VesselDashboardViewModel> VesselLists { get; set; }

		/// <summary>
		/// Gets or sets the vessel list.
		/// </summary>
		/// <value>
		/// The vessel list.
		/// </value>
		public List<VesselDetailViewModel> VesselList { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier corinthian phoenix.
		/// </summary>
		/// <value>
		/// The vessel identifier corinthian phoenix.
		/// </value>
		public string VesselIdCorinthianPhoenix { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier spirit discovery.
		/// </summary>
		/// <value>
		/// The vessel identifier spirit discovery.
		/// </value>
		public string VesselIdSpiritDiscovery { get; set; }

		/// <summary>
		/// Gets or sets the dashboard parameter.
		/// </summary>
		/// <value>
		/// The dashboard parameter.
		/// </value>
		public DashboardParameter DashboardParameter { get; set; }

		/// <summary>
		/// Gets or sets the fleet tracker URL.
		/// </summary>
		/// <value>
		/// The fleet tracker URL.
		/// </value>
		public string FleetTrackerURL { get; set; }

		/// <summary>
		/// Gets or sets the active mobile tab class.
		/// </summary>
		/// <value>
		/// The active mobile tab class.
		/// </value>
		public string ActiveMobileTabClass { get; set; }
        public string SessionStorageDetails { get; set; }
    }
}
