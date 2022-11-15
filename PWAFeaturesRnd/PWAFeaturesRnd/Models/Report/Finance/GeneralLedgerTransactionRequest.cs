using System;

namespace PWAFeaturesRnd.Models.Report.Finance
{
	/// <summary>
	/// General Ledger Transaction Request
	/// </summary>
	public class GeneralLedgerTransactionRequest
	{
		/// <summary>
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime ToDate { get; set; }

		/// <summary>
		/// Gets or sets the financial year start date.
		/// </summary>
		/// <value>
		/// The financial year start date.
		/// </value>
		public DateTime FinancialYearStartDate { get; set; }

		/// <summary>
		/// Gets or sets the account ids.
		/// </summary>
		/// <value>
		/// The account ids.
		/// </value>
		public string AccountIds { get; set; }

		/// <summary>
		/// Gets or sets the account identifier from.
		/// </summary>
		/// <value>
		/// The account identifier from.
		/// </value>
		public string AccountIdFrom { get; set; }

		/// <summary>
		/// Gets or sets the account identifier to.
		/// </summary>
		/// <value>
		/// The account identifier to.
		/// </value>
		public string AccountIdTo { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [exclude intermediate accruals].
		/// </summary>
		/// <value>
		///   <c>true</c> if [exclude intermediate accruals]; otherwise, <c>false</c>.
		/// </value>
		public bool ExcludeIntermediateAccruals { get; set; }
	}
}
