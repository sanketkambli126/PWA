using System;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	/// <summary>
	/// Inspection Manager Dashboard Response
	/// </summary>
	public class InspectionManagerDashboardResponse
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the vessel description.
		/// </summary>
		/// <value>
		/// The vessel description.
		/// </value>
		public string VesselDescription { get; set; }

		/// <summary>
		/// Gets or sets the vessel age.
		/// </summary>
		/// <value>
		/// The vessel age.
		/// </value>
		public DateTime? VesselBuiltDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is next position available.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is next position available; otherwise, <c>false</c>.
		/// </value>
		public bool IsNextPositionAvailable { get; set; }

		/// <summary>
		/// Gets or sets the inspection statistic detail.
		/// </summary>
		/// <value>
		/// The inspection statistic detail.
		/// </value>
		public InspectionDashboardStatisticDetail InspectionStatisticDetail { get; set; }

		/// <summary>
		/// Gets or sets the inspection detail.
		/// </summary>
		/// <value>
		/// The inspection detail.
		/// </value>
		public InspectionDashboardDetail InspectionDetail { get; set; }

		/// <summary>
		/// Gets or sets the inspection finding detail.
		/// </summary>
		/// <value>
		/// The inspection finding detail.
		/// </value>
		public InspectionDashboardFindingDetail InspectionFindingDetail { get; set; }
	}
}
