using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	/// <summary>
	/// Inspection VIR Report Request
	/// </summary>
	public class InspectionVIRReportRequest
	{
		/// <summary>
		/// Gets or sets the inspection identifier.
		/// </summary>
		/// <value>
		/// The inspection identifier.
		/// </value>
		public string InspectionId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the image download for RPT temporary path.
		/// </summary>
		/// <value>
		/// The image download for RPT temporary path.
		/// </value>
		public string ImageDownloadForRptTempPath { get; set; }

		/// <summary>
		/// Gets or sets the type of the report export.
		/// </summary>
		/// <value>
		/// The type of the report export.
		/// </value>
		public ReportExportTypes ReportExportType { get; set; }
	}
}
