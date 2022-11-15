using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// FurtherControlMeasures
	/// </summary>
	public class FurtherControlMeasures
	{
		/// <summary>
		/// Gets or sets the RFC identifier.
		/// </summary>
		/// <value>
		/// The RFC identifier.
		/// </value>
		public string RfcId { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the PRZ identifier.
		/// </summary>
		/// <value>
		/// The PRZ identifier.
		/// </value>
		public string PrzId { get; set; }

		/// <summary>
		/// Gets or sets the RFC number.
		/// </summary>
		/// <value>
		/// The RFC number.
		/// </value>
		public int RfcNumber { get; set; }

		/// <summary>
		/// Gets or sets the further risk.
		/// </summary>
		/// <value>
		/// The further risk.
		/// </value>
		public string FurtherRisk { get; set; }

		/// <summary>
		/// Gets or sets the action date.
		/// </summary>
		/// <value>
		/// The action date.
		/// </value>
		public DateTime? ActionDate { get; set; }

		/// <summary>
		/// Gets or sets the review date.
		/// </summary>
		/// <value>
		/// The review date.
		/// </value>
		public DateTime? ReviewDate { get; set; }

		/// <summary>
		/// Gets or sets the updated site.
		/// </summary>
		/// <value>
		/// The updated site.
		/// </value>
		public string UpdatedSite { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the updated by.
		/// </summary>
		/// <value>
		/// The updated by.
		/// </value>
		public string UpdatedBy { get; set; }
	}
}
