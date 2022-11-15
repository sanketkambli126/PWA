using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Finance
{
    public class AccountBalanceBudgetDetail
    {
		/// <summary>
		/// Gets or sets the account identifier.
		/// </summary>
		/// <value>
		/// The account identifier.
		/// </value>
		public string AccountID { get; set; }

		/// <summary>
		/// Gets or sets the year.
		/// </summary>
		/// <value>
		/// The year.
		/// </value>
		public int Year { get; set; }

		/// <summary>
		/// Gets or sets the budget start.
		/// </summary>
		/// <value>
		/// The budget start.
		/// </value>
		public DateTime? BudgetStart { get; set; }

		/// <summary>
		/// Gets or sets the budget end.
		/// </summary>
		/// <value>
		/// The budget end.
		/// </value>
		public DateTime? BudgetEnd { get; set; }

		/// <summary>
		/// Gets or sets the budget.
		/// </summary>
		/// <value>
		/// The budget.
		/// </value>
		public decimal? Budget { get; set; }
	}
}
