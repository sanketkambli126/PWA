using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// This class is for reported jobs
	/// </summary>
	public class ReportedJobs
    {
		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>
		/// The date.
		/// </value>
		public DateTime? Date { get; set; }

		/// <summary>
		/// Gets or sets the count.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public int Count { get; set; }
	}
}
