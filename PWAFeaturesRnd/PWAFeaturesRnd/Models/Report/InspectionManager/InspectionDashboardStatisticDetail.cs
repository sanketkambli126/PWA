namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	/// <summary>
	/// Inspection Dashboard Statistic Detail
	/// </summary>
	public class InspectionDashboardStatisticDetail
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the total omv inspection count.
		/// </summary>
		/// <value>
		/// The total omv inspection count.
		/// </value>
		public int TotalOmvInspectionCount { get; set; }

		/// <summary>
		/// Gets or sets the total PSC inspection count.
		/// </summary>
		/// <value>
		/// The total PSC inspection count.
		/// </value>
		public int TotalPscInspectionCount { get; set; }

		/// <summary>
		/// Gets or sets the omv defect rate.
		/// </summary>
		/// <value>
		/// The omv defect rate.
		/// </value>
		public decimal OMVDefectRate { get; set; }

		/// <summary>
		/// Gets or sets the omv flawless rate.
		/// </summary>
		/// <value>
		/// The omv flawless rate.
		/// </value>
		public decimal OMVFlawlessRate { get; set; }

		/// <summary>
		/// Gets or sets the PSC defect rate.
		/// </summary>
		/// <value>
		/// The PSC defect rate.
		/// </value>
		public decimal PSCDefectRate { get; set; }

		/// <summary>
		/// Gets or sets the PSC flawless rate.
		/// </summary>
		/// <value>
		/// The PSC flawless rate.
		/// </value>
		public decimal PSCFlawlessRate { get; set; }

		/// <summary>
		/// Gets or sets the PSC detaintion count.
		/// </summary>
		/// <value>
		/// The PSC detaintion count.
		/// </value>
		public int PSCDetaintionCount { get; set; }

		/// <summary>
		/// Gets or sets the omv inspection average risk.
		/// </summary>
		/// <value>
		/// The omv inspection average risk.
		/// </value>
		public decimal OMVInspectionAverageRisk { get; set; }

		/// <summary>
		/// Gets or sets the total omv finding count.
		/// </summary>
		/// <value>
		/// The total omv finding count.
		/// </value>
		public int TotalOMVFindingCount { get; set; }

		/// <summary>
		/// Gets or sets the total PSC finding count.
		/// </summary>
		/// <value>
		/// The total PSC finding count.
		/// </value>
		public int TotalPSCFindingCount { get; set; }

		/// <summary>
		/// Gets or sets the total omv risk rating.
		/// </summary>
		/// <value>
		/// The total omv risk rating.
		/// </value>
		public int TotalOMVRiskRating { get; set; }
	}
}
