using System;

namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// Budget Order Detail
	/// </summary>
	public class BudgetOrderDetailsViewModel
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
		public string LocalCost { get; set; }

		/// <summary>
		/// Gets or sets the currency identifier.
		/// </summary>
		/// <value>
		/// The currency identifier.
		/// </value>
		public string CurrencyId { get; set; }

		/// <summary>
		/// Gets or sets the protected order number.
		/// </summary>
		/// <value>
		/// The protected order number.
		/// </value>
		public string ProtectedOrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the protected accounting company identifier.
		/// </summary>
		/// <value>
		/// The protected accounting company identifier.
		/// </value>
		public string ProtectedAccountingCompanyId { get; set; }

		/// <summary>
		/// Gets or sets the purchase order request URL.
		/// </summary>
		/// <value>
		/// The purchase order request URL.
		/// </value>
		public string PurchaseOrderRequestUrl { get; set; }

		/// <summary>
		/// Gets or sets the purchase order request vessel identifier.
		/// </summary>
		/// <value>
		/// The purchase order request vessel identifier.
		/// </value>
		public string PurchaseOrderRequestVesselId { get; set; }

	}
}
