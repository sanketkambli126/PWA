using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;

namespace PWAFeaturesRnd.Models.Report.Finance
{
    public class AccountBalanceDetail
    {
		/// <summary>
		/// Gets or sets the account identifier.
		/// </summary>
		/// <value>
		/// The account identifier.
		/// </value>
		public string AccountID { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the currency identifier.
		/// </summary>
		/// <value>
		/// The currency identifier.
		/// </value>
		public string CurrencyID { get; set; }

		/// <summary>
		/// Gets or sets the type of the account.
		/// </summary>
		/// <value>
		/// The type of the account.
		/// </value>
		public string AccountType { get; set; }

		/// <summary>
		/// Gets or sets the original balance.
		/// </summary>
		/// <value>
		/// The original balance.
		/// </value>
		public decimal? OriginalBalance { get; set; }

		/// <summary>
		/// Gets or sets the base balance.
		/// </summary>
		/// <value>
		/// The base balance.
		/// </value>
		public decimal? BaseBalance { get; set; }

		/// <summary>
		/// Gets or sets the base balance usd.
		/// </summary>
		/// <value>
		/// The base balance usd.
		/// </value>
		public decimal? BaseBalanceUSD { get; set; }

		/// <summary>
		/// Gets or sets the budget data.
		/// </summary>
		/// <value>
		/// The budget data.
		/// </value>
		public AccountBalanceBudgetDetail BudgetData { get; set; }

		/// <summary>
		/// Gets or sets the type of the currency.
		/// </summary>
		/// <value>
		/// The type of the currency.
		/// </value>
		public string CurrencyType { get; set; }

		/// <summary>
		/// Gets or sets the local balance.
		/// </summary>
		/// <value>
		/// The local balance.
		/// </value>
		public decimal? LocalBalance { get; set; }

		/// <summary>
		/// Gets or sets the chart detail sequence.
		/// </summary>
		/// <value>
		/// The chart detail sequence.
		/// </value>
		public int ChartDetailSequence { get; set; }

		/// <summary>
		/// Gets or sets the applicable account auxiliaries.
		/// </summary>
		/// <value>
		/// The applicable account auxiliaries.
		/// </value>
		public ApplicableAccountAuxiliaries ApplicableAccountAuxiliaries { get; set; }

		/// <summary>
		/// Gets or sets the master chart account.
		/// </summary>
		/// <value>
		/// The master chart account.
		/// </value>
		public string MasterChartAccount { get; set; }
	}
}
