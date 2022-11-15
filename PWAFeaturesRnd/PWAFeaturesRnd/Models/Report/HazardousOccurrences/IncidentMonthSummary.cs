using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
	/// <summary>
	/// 
	/// </summary>
	public class IncidentMonthSummary
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the report type identifier.
		/// </summary>
		/// <value>
		/// The report type identifier.
		/// </value>
		public string ReportTypeId { get; set; }

		/// <summary>
		/// Gets or sets the month total.
		/// </summary>
		/// <value>
		/// The month total.
		/// </value>
		public List<IncidentMonthTotal> MonthTotal { get; set; }

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>
		/// The maximum value.
		/// </value>
		public int MaxValue { get; set; }
	}
}