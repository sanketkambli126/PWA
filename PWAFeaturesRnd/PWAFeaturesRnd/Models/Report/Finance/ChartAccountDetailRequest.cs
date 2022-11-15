using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Finance
{
	public class ChartAccountDetailRequest
	{
		/// <summary>
		/// Gets or sets the chart header identifier.
		/// </summary>
		/// <value>
		/// The chart header identifier.
		/// </value>
		public string ChartHeaderId { get; set; }

		/// <summary>
		/// Gets or sets the CHD posting.
		/// </summary>
		/// <value>
		/// The CHD posting.
		/// </value>
		public string ChdPosting { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is paying account.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is paying account; otherwise, <c>false</c>.
		/// </value>
		public bool IsPayingAccount { get; set; }

		/// <summary>
		/// Gets or sets the search parameter.
		/// </summary>
		/// <value>
		/// The search parameter.
		/// </value>
		public string SearchParameter { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [fetch inactive accounts].
		/// </summary>
		/// <value>
		///   <c>true</c> if [fetch inactive accounts]; otherwise, <c>false</c>.
		/// </value>
		public bool FetchOnlyActiveAccounts { get; set; }

		/// <summary>
		/// Gets or sets the currency identifier.
		/// </summary>
		/// <value>
		/// The currency identifier.
		/// </value>
		public string CurrencyId { get; set; }

		/// <summary>
		/// Gets or sets the currency identifier.
		/// </summary>
		/// <value>
		/// The currency identifier.
		/// </value>
		public string BaseCurrencyId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is debtor account required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is debtor account required; otherwise, <c>false</c>.
		/// </value>
		public bool IsDebtorAccountRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is zp discarded.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is zp discarded; otherwise, <c>false</c>.
		/// </value>
		public bool IsZPDiscarded { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [depreciation accounts].
		/// </summary>
		/// <value>
		///   <c>true</c> if [depreciation accounts]; otherwise, <c>false</c>.
		/// </value>
		public bool? DepreciationAccounts { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [accum depreciation accounts].
		/// </summary>
		/// <value>
		///   <c>true</c> if [accum depreciation accounts]; otherwise, <c>false</c>.
		/// </value>
		public bool? AccumDepreciationAccounts { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [roll clear accounts].
		/// </summary>
		/// <value>
		///   <c>true</c> if [roll clear accounts]; otherwise, <c>false</c>.
		/// </value>
		public bool? RollClearAccounts { get; set; }

		/// <summary>
		/// Gets or sets the type of the chart account.
		/// </summary>
		/// <value>
		/// The type of the chart account.
		/// </value>
		public ChartDetailAccountType? ChartAccountType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [restrict control accounts].
		/// </summary>
		/// <value>
		///   <c>true</c> if [restrict control accounts]; otherwise, <c>false</c>.
		/// </value>
		public bool RestrictControlAccounts { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is account tax out required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is account tax out required; otherwise, <c>false</c>.
		/// </value>
		public bool IsAccountTaxOutRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is exclude bank accounts.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is exclude bank accounts; otherwise, <c>false</c>.
		/// </value>
		public bool IsExcludeBankAccounts { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is search with contains.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is search with contains; otherwise, <c>false</c>.
		/// </value>
		public bool IsSearchDescriptionWithContains { get; set; }
	}
}
