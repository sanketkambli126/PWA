using System;

namespace PWAFeaturesRnd.Models.Report.Finance
{
	/// <summary>
	/// 
	/// </summary>
	public class CostAnalysisTransactionResponse
	{
		/// <summary>
		/// Gets or sets the transaction identifier.
		/// </summary>
		/// <value>
		/// The transaction identifier.
		/// </value>
		public string TransactionId { get; set; }

		/// <summary>
		/// Gets or sets the transaction date.
		/// </summary>
		/// <value>
		/// The transaction date.
		/// </value>
		public DateTime TransactionDate { get; set; }

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
		public string Text { get; set; }

		/// <summary>
		/// Gets or sets the type of the journal.
		/// </summary>
		/// <value>
		/// The type of the journal.
		/// </value>
		public string JournalType { get; set; }

		/// <summary>
		/// Gets or sets the journal desc.
		/// </summary>
		/// <value>
		/// The journal desc.
		/// </value>
		public string JournalDesc { get; set; }

		/// <summary>
		/// Gets or sets the order no.
		/// </summary>
		/// <value>
		/// The order no.
		/// </value>
		public string OrderNo { get; set; }

		/// <summary>
		/// Gets or sets the voucher no.
		/// </summary>
		/// <value>
		/// The voucher no.
		/// </value>
		public string VoucherNo { get; set; }

		/// <summary>
		/// Gets or sets the supplier.
		/// </summary>
		/// <value>
		/// The supplier.
		/// </value>
		public string Supplier { get; set; }

		/// <summary>
		/// Gets or sets the reference.
		/// </summary>
		/// <value>
		/// The reference.
		/// </value>
		public string Reference { get; set; }

		/// <summary>
		/// Gets or sets the amount.
		/// </summary>
		/// <value>
		/// The amount.
		/// </value>
		public decimal Amount { get; set; }

		/// <summary>
		/// Gets or sets the invoice document identifier.
		/// </summary>
		/// <value>
		/// The invoice document identifier.
		/// </value>
		public string InvoiceDocumentId { get; set; }

		/// <summary>
		/// Gets or sets the document count.
		/// </summary>
		/// <value>
		/// The document count.
		/// </value>
		public int DocumentCount { get; set; }

		/// <summary>
		/// Gets or sets the amount original.
		/// </summary>
		/// <value>
		/// The amount original.
		/// </value>
		public decimal AmountOriginal { get; set; }

		/// <summary>
		/// Gets or sets the currency.
		/// </summary>
		/// <value>
		/// The currency.
		/// </value>
		public string Currency { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is open.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is open; otherwise, <c>false</c>.
		/// </value>
		public bool IsOpen { get; set; }
	}
}
