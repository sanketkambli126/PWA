using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Budget Order Detail
	/// </summary>
	public class BudgetOrderDetail
    {

		/// <summary>
		/// Gets or sets the accounting company identifier.
		/// </summary>
		/// <value>
		/// The accounting company identifier.
		/// </value>
		public string AccountingCompanyId { get; set; }

		/// <summary>
		/// Gets or sets the order number.
		/// </summary>
		/// <value>
		/// The order number.
		/// </value>
		public string OrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the order title.
		/// </summary>
		/// <value>
		/// The order title.
		/// </value>
		public string OrderTitle { get; set; }

		/// <summary>
		/// Gets or sets the order date.
		/// </summary>
		/// <value>
		/// The order date.
		/// </value>
		public DateTime? OrderDate { get; set; }

		/// <summary>
		/// Gets or sets the local cost.
		/// </summary>
		/// <value>
		/// The local cost.
		/// </value>
		public decimal? LocalCost { get; set; }

		/// <summary>
		/// Gets or sets the currency identifier.
		/// </summary>
		/// <value>
		/// The currency identifier.
		/// </value>
		public string CurrencyId { get; set; }
	}
}
