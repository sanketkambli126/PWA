using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Vessel Preview
	/// </summary>
	public class VesselPreview
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the imo.
		/// </summary>
		/// <value>
		/// The imo.
		/// </value>
		public string Imo { get; set; }

		/// <summary>
		/// Gets or sets the mangagement start.
		/// </summary>
		/// <value>
		/// The mangagement start.
		/// </value>
		public DateTime? MangagementStart { get; set; }

		/// <summary>
		/// Gets or sets the mangagement end.
		/// </summary>
		/// <value>
		/// The mangagement end.
		/// </value>
		public DateTime? MangagementEnd { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the registered owner.
		/// </summary>
		/// <value>
		/// The registered owner.
		/// </value>
		public string RegisteredOwner { get; set; }

		/// <summary>
		/// Gets or sets the vessel built date.
		/// </summary>
		/// <value>
		/// The vessel built date.
		/// </value>
		public DateTime? VesselBuiltDate { get; set; }

		/// <summary>
		/// Gets or sets the VGT identifier.
		/// </summary>
		/// <value>
		/// The VGT identifier.
		/// </value>
		public string VgtId { get; set; }

		/// <summary>
		/// Gets or sets the flag.
		/// </summary>
		/// <value>
		/// The flag.
		/// </value>
		public string Flag { get; set; }

		/// <summary>
		/// Gets or sets the vty identifier.
		/// </summary>
		/// <value>
		/// The vty identifier.
		/// </value>
		public string VtyId { get; set; }
	}
}
