using System;

namespace PWAFeaturesRnd.ViewModels.Finance
{
	/// <summary>
	/// Invoice Document Response
	/// </summary>
	public class InvoiceDocumentResponseViewModel
	{
		/// <summary>
		/// Gets or sets the accounting company identifier.
		/// </summary>
		/// <value>
		/// The accounting company identifier.
		/// </value>
		public string AccountingCompanyId { get; set; }

		/// <summary>
		/// Gets or sets the updated on.
		/// </summary>
		/// <value>
		/// The updated on.
		/// </value>
		public DateTime? UpdatedOn { get; set; }

		/// <summary>
		/// Gets or sets the updated by.
		/// </summary>
		/// <value>
		/// The updated by.
		/// </value>
		public string UpdatedBy { get; set; }

		/// <summary>
		/// Gets or sets the invoice document identifier.
		/// </summary>
		/// <value>
		/// The invoice document identifier.
		/// </value>
		public string InvoiceDocumentId { get; set; }

		/// <summary>
		/// Gets or sets the voucher number.
		/// </summary>
		/// <value>
		/// The voucher number.
		/// </value>
		public string VoucherNumber { get; set; }
	}
}
