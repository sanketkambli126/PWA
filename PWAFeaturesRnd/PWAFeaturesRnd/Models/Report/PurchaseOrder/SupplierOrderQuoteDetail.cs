using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// This contract is used to hold supplier order quote details while entering quote
	/// </summary>
	public class SupplierOrderQuoteDetail
	{
		/// <summary>
		/// Gets or sets the supplier order identifier.
		/// </summary>
		/// <value>
		/// The supplier order identifier.
		/// </value>
		public string SupplierOrderId { get; set; }

		/// <summary>
		/// Gets or sets the currency identifier.
		/// </summary>
		/// <value>
		/// The currency identifier.
		/// </value>
		public string CurrencyId { get; set; }

		/// <summary>
		/// Gets or sets the supplier order port.
		/// </summary>
		/// <value>
		/// The supplier order port.
		/// </value>
		public string SupplierOrderPortId { get; set; }

		/// <summary>
		/// Gets or sets the name of the supplier order port.
		/// </summary>
		/// <value>
		/// The name of the supplier order port.
		/// </value>
		public string SupplierOrderPortName { get; set; }

		/// <summary>
		/// Gets or sets the supplier reference.
		/// </summary>
		/// <value>
		/// The supplier reference.
		/// </value>
		public string SupplierReference { get; set; }

		/// <summary>
		/// Gets or sets the date received.
		/// </summary>
		/// <value>
		/// The date received.
		/// </value>
		public DateTime? DateReceived { get; set; }

		/// <summary>
		/// Gets or sets the delivery cost.
		/// </summary>
		/// <value>
		/// The delivery cost.
		/// </value>
		public decimal? DeliveryCost { get; set; }

		/// <summary>
		/// Gets or sets the discount percent.
		/// </summary>
		/// <value>
		/// The discount percent.
		/// </value>
		public decimal? DiscountPercent { get; set; }

		/// <summary>
		/// Gets or sets the ex work country identifier.
		/// </summary>
		/// <value>
		/// The ex work country identifier.
		/// </value>
		public string ExWorkCountryId { get; set; }

		/// <summary>
		/// Gets or sets the ex work location.
		/// </summary>
		/// <value>
		/// The ex work location.
		/// </value>
		public string ExWorkLocation { get; set; }

		/// <summary>
		/// Gets or sets the maximum ex work days.
		/// </summary>
		/// <value>
		/// The maximum ex work days.
		/// </value>
		public int? MaxExWorkDays { get; set; }

		/// <summary>
		/// Gets or sets the quote valid till date.
		/// </summary>
		/// <value>
		/// The quote valid till date.
		/// </value>
		public DateTime? QuoteValidTillDate { get; set; }

		/// <summary>
		/// Gets or sets the is hazardous goods.
		/// </summary>
		/// <value>
		/// The is hazardous goods.
		/// </value>
		public bool? IsHazardousGoods { get; set; }

		/// <summary>
		/// Gets or sets the spare part type identifier.
		/// </summary>
		/// <value>
		/// The spare part type identifier.
		/// </value>
		public string SparePartTypeId { get; set; }

		/// <summary>
		/// Gets or sets the supplier order status.
		/// </summary>
		/// <value>
		/// The supplier order status.
		/// </value>
		public string SupplierOrderStatus { get; set; }

		/// <summary>
		/// Gets or sets the supplier order notes.
		/// </summary>
		/// <value>
		/// The supplier order notes.
		/// </value>
		public string SupplierOrderNotes { get; set; }

		/// <summary>
		/// Gets or sets the total cost.
		/// </summary>
		/// <value>
		/// The total cost.
		/// </value>
		public decimal? TotalCost { get; set; }

		/// <summary>
		/// Gets or sets the name of the supplier.
		/// </summary>
		/// <value>
		/// The name of the supplier.
		/// </value>
		public string SupplierName { get; set; }

		/// <summary>
		/// Gets or sets the supplier company identifier.
		/// </summary>
		/// <value>
		/// The supplier company identifier.
		/// </value>
		public string SupplierCompanyId { get; set; }

		/// <summary>
		/// Gets or sets the is complete quote.
		/// </summary>
		/// <value>
		/// The is complete quote.
		/// </value>
		public bool? IsCompleteQuote { get; set; }

		/// <summary>
		/// Gets or sets the port country code.
		/// </summary>
		/// <value>
		/// The port country code.
		/// </value>
		public string PortCountryCode { get; set; }

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
		/// Gets or sets a value indicating whether this instance is proforma requested.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is proforma requested; otherwise, <c>false</c>.
		/// </value>
		public bool IsProformaRequested { get; set; }
	}
}
