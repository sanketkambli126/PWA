using System;

namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// summary purchase order invoice view model
	/// </summary>
	public class SummaryPurchaseOrderInvoicingViewModel
    {
		/// <summary>
		/// Gets or sets the reference.
		/// </summary>
		/// <value>
		/// The reference.
		/// </value>
		public string Reference { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the status category.
		/// </summary>
		/// <value>
		/// The status category.
		/// </value>
		public string StatusCategory { get; set; }

		/// <summary>
		/// Gets or sets the amount.
		/// </summary>
		/// <value>
		/// The amount.
		/// </value>
		public string Amount { get; set; }

		/// <summary>
		/// Gets or sets the currency.
		/// </summary>
		/// <value>
		/// The currency.
		/// </value>
		public string Currency { get; set; }

		/// <summary>
		/// Gets or sets the invoice document identifier.
		/// </summary>
		/// <value>
		/// The invoice document identifier.
		/// </value>
		public string InvoiceDocumentId { get; set; }

		/// <summary>
		/// Gets or sets the invoice date.
		/// </summary>
		/// <value>
		/// The invoice date.
		/// </value>
		public DateTime? InvoiceDate { get; set; }

		/// <summary>
		/// Gets or sets the invoice paid date.
		/// </summary>
		/// <value>
		/// The invoice paid date.
		/// </value>
		public DateTime? InvoicePaidDate { get; set; }
	}
}
