using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
	/// <summary>
	/// 
	/// </summary>
	public class AllInspectionExportViewModel
	{
		/// <summary>
		/// Gets or sets the type of the inspection.
		/// </summary>
		/// <value>
		/// The type of the inspection.
		/// </value>
		[DisplayName("Inspection Type")]
		public string InspectionType { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		[DisplayName("Status")]
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the inspection date.
		/// </summary>
		/// <value>
		/// The inspection date.
		/// </value>
		[DisplayName("Done")]
		public string InspectionDate { get; set; }

		/// <summary>
		/// Gets or sets the next due date.
		/// </summary>
		/// <value>
		/// The next due date.
		/// </value>
		[DisplayName("Next Due")]
		public string NextDueDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the company.
		/// </summary>
		/// <value>
		/// The name of the company.
		/// </value>
		[DisplayName("Company")]
		public string CompanyName { get; set; }
	}
}
