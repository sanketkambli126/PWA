namespace PWAFeaturesRnd.ViewModels.Finance
{
	/// <summary>
	/// Over Budget Details Response View Model
	/// </summary>
	public class OverBudgetDetailsResponseViewModel
	{
		/// <summary>
		/// Gets or sets the encrypted vessel identifier.
		/// </summary>
		/// <value>
		/// The encrypted vessel identifier.
		/// </value>
		public string EncryptedVesselId { get; set; }

		/// <summary>
		/// Gets or sets the encrypted opex URL.
		/// </summary>
		/// <value>
		/// The encrypted opex URL.
		/// </value>
		public string EncryptedOpexURL { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the total.
		/// </summary>
		/// <value>
		/// The total.
		/// </value>
		public decimal? Total { get; set; }

		/// <summary>
		/// Gets or sets the budget.
		/// </summary>
		/// <value>
		/// The budget.
		/// </value>
		public decimal? Budget { get; set; }

		/// <summary>
		/// Gets or sets the variance.
		/// </summary>
		/// <value>
		/// The variance.
		/// </value>
		public decimal? Variance { get; set; }

		/// <summary>
		/// Gets or sets the budget percenatge.
		/// </summary>
		/// <value>
		/// The budget percenatge.
		/// </value>
		public decimal? BudgetPercenatge { get; set; }
	}
}
