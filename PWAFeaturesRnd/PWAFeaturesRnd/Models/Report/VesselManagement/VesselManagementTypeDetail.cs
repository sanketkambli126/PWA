using System;

namespace PWAFeaturesRnd.Models.Report.VesselManagement
{
	/// <summary>
	/// Vessel Management Type Detail
	/// </summary>
	public class VesselManagementTypeDetail
	{
		/// <summary>
		/// Gets or sets the vessel management identifier.
		/// </summary>
		/// <value>
		/// The vessel management identifier.
		/// </value>
		public string VesselManagementId { get; set; }

		/// <summary>
		/// Gets or sets the type of the vessel management.
		/// </summary>
		/// <value>
		/// The type of the vessel management.
		/// </value>
		public string VesselManagementType { get; set; }

		/// <summary>
		/// Gets or sets the accounting company identifier.
		/// </summary>
		/// <value>
		/// The accounting company identifier.
		/// </value>
		public string AccountingCompanyId { get; set; }

		/// <summary>
		/// Gets or sets the management start date.
		/// </summary>
		/// <value>
		/// The management start date.
		/// </value>
		public DateTime? ManagementStartDate { get; set; }

		/// <summary>
		/// Gets or sets the management end date.
		/// </summary>
		/// <value>
		/// The management end date.
		/// </value>
		public DateTime? ManagementEndDate { get; set; }
	}
}
