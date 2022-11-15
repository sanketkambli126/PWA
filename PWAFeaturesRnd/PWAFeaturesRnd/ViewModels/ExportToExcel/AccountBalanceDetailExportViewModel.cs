using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
    /// <summary>
    /// AccountBalanceDetail ExportViewModel
    /// </summary>
    public class AccountBalanceDetailExportViewModel
    {
        /// <summary>
		/// Gets or sets the account identifier.
		/// </summary>
		/// <value>
		/// The account identifier.
		/// </value>
        [DisplayName("Account Code")]
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [DisplayName("Account Name")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the type of the account.
        /// </summary>
        /// <value>
        /// The type of the account.
        /// </value>
        [DisplayName("Account Type")]
        public string AccountType { get; set; }

        /// <summary>
        /// Gets or sets the applicable account auxiliaries.
        /// </summary>
        /// <value>
        /// The applicable account auxiliaries.
        /// </value>
        [DisplayName("AUX")]
        public string Auxiliaries { get; set; }

        /// <summary>
        /// Gets or sets the type of the currency.
        /// </summary>
        /// <value>
        /// The type of the currency.
        /// </value>
        [DisplayName("CUR")]
        public string CurrencyType { get; set; }

        /// <summary>
        /// Gets or sets the original balance.
        /// </summary>
        /// <value>
        /// The original balance.
        /// </value>
        [DisplayName("Original Balance")]
        public string OriginalBalance { get; set; }

        /// <summary>
        /// Gets or sets the base balance usd.
        /// </summary>
        /// <value>
        /// The base balance usd.
        /// </value>
        [DisplayName("Functional Balance")]
        public string BaseBalanceUSD { get; set; }
    }
}
