namespace PWAFeaturesRnd.ViewModels.Finance
{
	/// <summary>
	/// Opex Fleet Summary Response View Model
	/// </summary>
	public class OpexFleetSummaryResponseViewModel
    {
		/// <summary>
		/// Gets or sets the opex over budget percentage.
		/// </summary>
		/// <value>
		/// The opex over budget percentage.
		/// </value>
		public string OpexOverBudgetPercentage { get; set; }

		/// <summary>
		/// Gets or sets the opex over budget priority.
		/// </summary>
		/// <value>
		/// The opex over budget priority.
		/// </value>
		public int OpexOverBudgetPriority { get; set; }

		/// <summary>
		/// Gets or sets the opex over budget to date.
		/// </summary>
		/// <value>
		/// The opex over budget to date.
		/// </value>
		public string OpexOverBudgetToDate { get; set; }

		/// <summary>
		/// Gets or sets the opex over budget information.
		/// </summary>
		/// <value>
		/// The opex over budget information.
		/// </value>
		public string OpexOverBudgetInfo { get; set; }
	}
}
