using System;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;

namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// Summary purchase order supplier view model
	/// </summary>
	public class SummaryPurchaseOrderSupplierViewModel
    {
		/// <summary>
		/// Gets or sets the supplier reference.
		/// </summary>
		/// <value>
		/// The supplier reference.
		/// </value>
		public string SupplierReference { get; set; }

		/// <summary>
		/// Gets or sets the date authorised.
		/// </summary>
		/// <value>
		/// The date authorised.
		/// </value>
		public string DateAuthorised { get; set; }

		/// <summary>
		/// Gets or sets the authorised by.
		/// </summary>
		/// <value>
		/// The authorised by.
		/// </value>
		public string AuthorisedBy { get; set; }

		/// <summary>
		/// Gets or sets the company details.
		/// </summary>
		/// <value>
		/// The company details.
		/// </value>
		public CompanyDetailsViewModel CompanyDetails { get; set; }

		/// <summary>
		/// Gets or sets the confirmation date.
		/// </summary>
		/// <value>
		/// The confirmation date.
		/// </value>
		public string ConfirmationDate { get; set; }

		/// <summary>
		/// Gets or sets the purchase order identifier.
		/// </summary>
		/// <value>
		/// The purchase order identifier.
		/// </value>
		public string PurchaseOrderId { get; set; }

		/// <summary>
		/// Gets or sets the purchase order chased date.
		/// </summary>
		/// <value>
		/// The purchase order chased date.
		/// </value>
		public string PurchaseOrderChasedDate { get; set; }

		/// <summary>
		/// Gets or sets the invoice chased date.
		/// </summary>
		/// <value>
		/// The invoice chased date.
		/// </value>
		public string InvoiceChasedDate { get; set; }
	}
}
