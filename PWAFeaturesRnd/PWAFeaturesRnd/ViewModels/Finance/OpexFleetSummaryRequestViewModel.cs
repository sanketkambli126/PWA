using System;

namespace PWAFeaturesRnd.ViewModels.Finance
{
	/// <summary>
	/// Opex Fleet Summary Request View model
	/// </summary>
	public class OpexFleetSummaryRequestViewModel
    {
		/// <summary>
		/// Gets or sets the fleet identifier.
		/// </summary>
		/// <value>
		/// The fleet identifier.
		/// </value>
		public string FleetId { get; set; }

		/// <summary>
		/// Gets or sets the type of the menu.
		/// </summary>
		/// <value>
		/// The type of the menu.
		/// </value>
		public string MenuType { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the opex to date.
		/// </summary>
		/// <value>
		/// The opex to date.
		/// </value>
		public DateTime? OpexToDate { get; set; }

		/// <summary>
		/// Gets or sets the budget days.
		/// </summary>
		/// <value>
		/// The budget days.
		/// </value>
		public int BudgetDays { get; set; }

		/// <summary>
		/// Gets or sets the budget percentage high limit.
		/// </summary>
		/// <value>
		/// The budget percentage high limit.
		/// </value>
		public decimal BudgetPercentageHighLimit { get; set; }

		/// <summary>
		/// Gets or sets the budget percentage low limit.
		/// </summary>
		/// <value>
		/// The budget percentage low limit.
		/// </value>
		public decimal BudgetPercentageLowLimit { get; set; }
	}
}
