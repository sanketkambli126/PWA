using System;
using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// Defect Dashboard Request
	/// </summary>
	public class DefectDashboardRequest
	{
		/// <summary>
		/// Gets or sets the item.
		/// </summary>
		/// <value>
		/// The item.
		/// </value>
		public UserMenuItem Item { get; set; }

		/// <summary>
		/// Gets or sets the reported in last n months.
		/// </summary>
		/// <value>
		/// The reported in last n months.
		/// </value>
		public int CreatedInLastNMonths { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>
		/// The start date.
		/// </value>
		public DateTime StartDate { get; set; }

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>
		/// The end date.
		/// </value>
		public DateTime EndDate { get; set; }
	}
}
