namespace PWAFeaturesRnd.ViewModels.Inspection
{
	/// <summary>
	/// Inspection VIR Report Request View Model
	/// </summary>
	public class InspectionVIRReportRequestViewModel
	{
		/// <summary>
		/// Gets or sets the encrypted vessel identifier.
		/// </summary>
		/// <value>
		/// The encrypted vessel identifier.
		/// </value>
		public string EncryptedVesselId { get; set; }

		/// <summary>
		/// Gets or sets the inspection URL.
		/// </summary>
		/// <value>
		/// The inspection URL.
		/// </value>
		public string InspectionUrl { get; set; }

		/// <summary>
		/// Gets or sets the type of the report.
		/// </summary>
		/// <value>
		/// The type of the report.
		/// </value>
		public string ReportType { get; set; }

		/// <summary>
		/// Gets or sets the report format.
		/// </summary>
		/// <value>
		/// The report format.
		/// </value>
		public string ReportFormat { get; set; }
	}
}
