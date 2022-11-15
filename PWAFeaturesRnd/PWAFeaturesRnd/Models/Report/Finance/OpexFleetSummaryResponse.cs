namespace PWAFeaturesRnd.Models.Report.Finance
{
	/// <summary>
	/// Opex Fleet Summary Response
	/// </summary>
	public class OpexFleetSummaryResponse
	{
		/// <summary>
		/// Gets or sets the opex over budget percentage.
		/// </summary>
		/// <value>
		/// The opex over budget percentage.
		/// </value>
		public decimal? OpexOverBudgetPercentage { get; set; }

		/// <summary>
		/// Gets or sets the opex over budget priority.
		/// </summary>
		/// <value>
		/// The opex over budget priority.
		/// </value>
		public int OpexOverBudgetPriority { get; set; }

		/// <summary>
		/// Gets or sets the opex over budget information.
		/// </summary>
		/// <value>
		/// The opex over budget information.
		/// </value>
		public string OpexOverBudgetInfo { get; set; }
	}
}
