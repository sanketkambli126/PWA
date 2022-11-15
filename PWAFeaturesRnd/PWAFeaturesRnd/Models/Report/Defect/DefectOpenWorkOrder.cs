namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// Defect Open Work Order
	/// </summary>
	public class DefectOpenWorkOrder
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the month.
		/// </summary>
		/// <value>
		/// The month.
		/// </value>
		public string Month { get; set; }

		/// <summary>
		/// Gets or sets the current month count.
		/// </summary>
		/// <value>
		/// The current month count.
		/// </value>
		public int CurrentMonthCount { get; set; }

		/// <summary>
		/// Gets or sets the previous months count.
		/// </summary>
		/// <value>
		/// The previous months count.
		/// </value>
		public int PreviousMonthsCount { get; set; }
	}
}
