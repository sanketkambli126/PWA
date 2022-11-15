using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Acknowledge Alert Request
	/// </summary>
	public class AcknowledgeAlertRequest
	{
		/// <summary>
		/// Gets or sets the port identifier.
		/// </summary>
		/// <value>
		/// The port identifier.
		/// </value>
		public string PortId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is vessel view.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is vessel view; otherwise, <c>false</c>.
		/// </value>
		public bool IsVesselView { get; set; }

		/// <summary>
		/// Gets or sets the PRT ids.
		/// </summary>
		/// <value>
		/// The PRT ids.
		/// </value>
		public List<string> PrtIds { get; set; }
	}
}
