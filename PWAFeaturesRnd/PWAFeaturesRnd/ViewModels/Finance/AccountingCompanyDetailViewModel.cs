using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Finance
{

    /// <summary>
    /// Accounting Company Detail
    /// </summary>
    public class AccountingCompanyDetailViewModel
    {
     

        /// <summary>
        /// Gets or sets the VMD owner.
        /// </summary>
        /// <value>
        /// The VMD owner.
        /// </value>
        public string VMDOwner { get; set; }

        /// <summary>
        /// Gets or sets the type of the accounting company.
        /// </summary>
        /// <value>
        /// The type of the accounting company.
        /// </value>
        public string AccountingCompanyType { get; set; }

        /// <summary>
        /// Gets or sets the base currency.
        /// </summary>
        /// <value>
        /// The base currency.
        /// </value>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Gets or sets the financial year start date.
        /// </summary>
        /// <value>
        /// The financial year start date.
        /// </value>
        public string FinancialYearStartDate { get; set; }

        /// <summary>
        /// Gets or sets the financial year end date.
        /// </summary>
        /// <value>
        /// The financial year end date.
        /// </value>
        public string FinancialYearEndDate { get; set; }

        /// <summary>
        /// Gets or sets the management start date.
        /// </summary>
        /// <value>
        /// The management start date.
        /// </value>
        public string ManagementStartDate { get; set; }

        /// <summary>
        /// Gets or sets the general ledger cut off date.
        /// </summary>
        /// <value>
        /// The general ledger cut off date.
        /// </value>
        public string GeneralLedgerCutOffDate { get; set; }
        
        /// <summary>
        /// Gets or sets type
        /// </summary>
        /// <value>
        /// The type
        /// </value>
        public string Type { get; set; }
    }
}
