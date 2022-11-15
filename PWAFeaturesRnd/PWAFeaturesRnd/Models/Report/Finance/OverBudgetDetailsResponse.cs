using System;

namespace PWAFeaturesRnd.Models.Report.Finance
{
	/// <summary>
	/// Over Budget Details Response
	/// </summary>
	public class OverBudgetDetailsResponse
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
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

		/// <summary>
		/// Gets or sets the type of the chart.
		/// </summary>
		/// <value>
		/// The type of the chart.
		/// </value>
		public string ChartType { get; set; }

		/// <summary>
		/// Gets or sets the budget to date.
		/// </summary>
		/// <value>
		/// The budget to date.
		/// </value>
		public DateTime? BudgetToDate { get; set; }

		/// <summary>
		/// Gets or sets the current identifier.
		/// </summary>
		/// <value>
		/// The current identifier.
		/// </value>
		public string CurId { get; set; }

		/// <summary>
		/// Gets or sets the current rate.
		/// </summary>
		/// <value>
		/// The current rate.
		/// </value>
		public decimal? CurRate { get; set; }

		/// <summary>
		/// Gets or sets the budget.
		/// </summary>
		/// <value>
		/// The budget.
		/// </value>
		public decimal? Budget { get; set; }

		/// <summary>
		/// Gets or sets the actual.
		/// </summary>
		/// <value>
		/// The actual.
		/// </value>
		public decimal? Actual { get; set; }

		/// <summary>
		/// Gets or sets the accrual.
		/// </summary>
		/// <value>
		/// The accrual.
		/// </value>
		public decimal? Accrual { get; set; }

		/// <summary>
		/// Gets or sets the total.
		/// </summary>
		/// <value>
		/// The total.
		/// </value>
		public decimal? Total { get; set; }

		/// <summary>
		/// Gets or sets the budget percenatge.
		/// </summary>
		/// <value>
		/// The budget percenatge.
		/// </value>
		public decimal? BudgetPercenatge { get; set; }

		/// <summary>
		/// Gets or sets the variance.
		/// </summary>
		/// <value>
		/// The variance.
		/// </value>
		public decimal? Variance { get; set; }

		/// <summary>
		/// Gets or sets the usd accrual.
		/// </summary>
		/// <value>
		/// The usd accrual.
		/// </value>
		public decimal? UsdAccrual { get; set; }

		/// <summary>
		/// Gets or sets the usd actual.
		/// </summary>
		/// <value>
		/// The usd actual.
		/// </value>
		public decimal? UsdActual { get; set; }

		/// <summary>
		/// Gets or sets the usd budget.
		/// </summary>
		/// <value>
		/// The usd budget.
		/// </value>
		public decimal? UsdBudget { get; set; }

		/// <summary>
		/// Gets or sets the usd total.
		/// </summary>
		/// <value>
		/// The usd total.
		/// </value>
		public decimal? UsdTotal { get; set; }

		/// <summary>
		/// Gets or sets the usd variance.
		/// </summary>
		/// <value>
		/// The usd variance.
		/// </value>
		public decimal? UsdVariance { get; set; }

		/// <summary>
		/// Gets or sets the usd budget percenatge.
		/// </summary>
		/// <value>
		/// The usd budget percenatge.
		/// </value>
		public decimal? UsdBudgetPercenatge { get; set; }
	}
}
