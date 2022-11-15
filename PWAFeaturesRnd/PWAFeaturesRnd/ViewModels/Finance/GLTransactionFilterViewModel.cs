using System;

namespace PWAFeaturesRnd.ViewModels.Finance
{
    /// <summary>
    /// GLTransactionFilter ViewModel
    /// </summary>
    public class GLTransactionFilterViewModel
    {
        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets the account code.
        /// </summary>
        /// <value>
        /// The account code.
        /// </value>
        public string AccountCode { get; set; }

        /// <summary>
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the accounting company CHH identifier.
        /// </summary>
        /// <value>
        /// The accounting company CHH identifier.
        /// </value>
        public string AccountingCompanyChhId { get; set; }

        /// <summary>
        /// Gets or sets the financial year start date.
        /// </summary>
        /// <value>
        /// The financial year start date.
        /// </value>
        public DateTime FinancialYearStartDate { get; set; }
    }
}
