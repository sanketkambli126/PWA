using System;

namespace PWAFeaturesRnd.ViewModels.Finance
{
	public class GeneralLedgerTransactionResponseViewModel
    {
		/// <summary>
		/// Gets or sets the account code.
		/// </summary>
		/// <value>
		/// The account code.
		/// </value>
		public string AccountCode { get; set; }

		/// <summary>
		/// Gets or sets the account desc.
		/// </summary>
		/// <value>
		/// The account desc.
		/// </value>
		public string AccountDesc { get; set; }

		/// <summary>
		/// Gets or sets the base amount.
		/// </summary>
		/// <value>
		/// The base amount.
		/// </value>
		public decimal? BaseAmount { get; set; }

		/// <summary>
		/// Gets or sets the original amount.
		/// </summary>
		/// <value>
		/// The original amount.
		/// </value>
		public decimal? OriginalAmount { get; set; }

		/// <summary>
		/// Gets or sets the transaction original currency.
		/// </summary>
		/// <value>
		/// The transaction original currency.
		/// </value>
		public string TransactionOriginalCurrency { get; set; }

		/// <summary>
		/// Gets or sets the transaction date.
		/// </summary>
		/// <value>
		/// The transaction date.
		/// </value>
		public DateTime? TransactionDate { get; set; }

		/// <summary>
		/// Gets or sets the voucher no.
		/// </summary>
		/// <value>
		/// The voucher no.
		/// </value>
		public string VoucherNo { get; set; }

		/// <summary>
		/// Gets or sets the reference.
		/// </summary>
		/// <value>
		/// The reference.
		/// </value>
		public string Ref { get; set; }

		/// <summary>
		/// Gets or sets the voy code.
		/// </summary>
		/// <value>
		/// The voy code.
		/// </value>
		public string VoyCode { get; set; }

		/// <summary>
		/// Gets or sets the contra.
		/// </summary>
		/// <value>
		/// The contra.
		/// </value>
		public string Contra { get; set; }

		/// <summary>
		/// Gets or sets the sort order.
		/// </summary>
		/// <value>
		/// The sort order.
		/// </value>
		public decimal? SortOrder { get; set; }

		/// <summary>
		/// Gets or sets the record no.
		/// </summary>
		/// <value>
		/// The record no.
		/// </value>
		public int? RecNo { get; set; }

		/// <summary>
		/// Gets or sets the journal type desc.
		/// </summary>
		/// <value>
		/// The journal type desc.
		/// </value>
		public string JournalTypeDesc { get; set; }

		/// <summary>
		/// Gets or sets the journal type seq.
		/// </summary>
		/// <value>
		/// The journal type seq.
		/// </value>
		public int? JournalTypeSeq { get; set; }

		/// <summary>
		/// Gets or sets the transaction text.
		/// </summary>
		/// <value>
		/// The transaction text.
		/// </value>
		public string TransactionText { get; set; }

		/// <summary>
		/// Gets or sets the is transaction debit.
		/// </summary>
		/// <value>
		/// The is transaction debit.
		/// </value>
		public int? IsTransactionDebit { get; set; }

		/// <summary>
		/// Gets or sets the opening bal flag.
		/// </summary>
		/// <value>
		/// The opening bal flag.
		/// </value>
		public int? OpeningBalFlag { get; set; }

		/// <summary>
		/// Gets or sets the transaction date entered.
		/// </summary>
		/// <value>
		/// The transaction date entered.
		/// </value>
		public DateTime? TransactionDateEntered { get; set; }


		public string AccountName { get; set; }

		public decimal FunctionalAmount { get; set; }

		public decimal FunctionalBalance { get; set; }
	}
}
