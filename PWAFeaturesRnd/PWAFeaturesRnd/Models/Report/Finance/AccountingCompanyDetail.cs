using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Finance
{
    /// <summary>
    /// Accounting Company Detail
    /// </summary>
    public class AccountingCompanyDetail
    {
        /// <summary>
        /// Gets or sets the order accrual control account.
        /// </summary>
        /// <value>
        /// The order accrual control account.
        /// </value>
        public string OrderAccrualControlAccount { get; set; }

        /// <summary>
        /// Gets or sets the sundry commit control account.
        /// </summary>
        /// <value>
        /// The sundry commit control account.
        /// </value>
        public string SundryCommitControlAccount { get; set; }

        /// <summary>
        /// Gets or sets the disbursements commit control account.
        /// </summary>
        /// <value>
        /// The disbursements commit control account.
        /// </value>
        public string DisbursementsCommitControlAccount { get; set; }

        /// <summary>
        /// Gets or sets the vessel cash commit control account.
        /// </summary>
        /// <value>
        /// The vessel cash commit control account.
        /// </value>
        public string VesselCashCommitControlAccount { get; set; }

        /// <summary>
        /// Gets or sets the crew wages commit control account.
        /// </summary>
        /// <value>
        /// The crew wages commit control account.
        /// </value>
        public string CrewWagesCommitControlAccount { get; set; }

        /// <summary>
        /// Gets or sets the crew travel commit control account.
        /// </summary>
        /// <value>
        /// The crew travel commit control account.
        /// </value>
        public string CrewTravelCommitControlAccount { get; set; }

        /// <summary>
        /// Gets or sets the invoice accrual control account.
        /// </summary>
        /// <value>
        /// The invoice accrual control account.
        /// </value>
        public string InvoiceAccrualControlAccount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is multi vessel company.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is multi vessel company; otherwise, <c>false</c>.
        /// </value>
        public bool IsMultiVesselCompany { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the VMD owner.
        /// </summary>
        /// <value>
        /// The VMD owner.
        /// </value>
        public string VMDOwner { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [po received only].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [po received only]; otherwise, <c>false</c>.
        /// </value>
        public bool POReceivedOnly { get; set; }

        /// <summary>
        /// Gets or sets the sundy commit date.
        /// </summary>
        /// <value>
        /// The sundy commit date.
        /// </value>
        public DateTime? SundyCommitDate { get; set; }

        /// <summary>
        /// Gets or sets the disbursements commit date.
        /// </summary>
        /// <value>
        /// The disbursements commit date.
        /// </value>
        public DateTime? DisbursementsCommitDate { get; set; }

        /// <summary>
        /// Gets or sets the vessel cash commit date.
        /// </summary>
        /// <value>
        /// The vessel cash commit date.
        /// </value>
        public DateTime? VesselCashCommitDate { get; set; }

        /// <summary>
        /// Gets or sets the crew wages commit date.
        /// </summary>
        /// <value>
        /// The crew wages commit date.
        /// </value>
        public DateTime? CrewWagesCommitDate { get; set; }

        /// <summary>
        /// Gets or sets the crew travel commit date.
        /// </summary>
        /// <value>
        /// The crew travel commit date.
        /// </value>
        public DateTime? CrewTravelCommitDate { get; set; }

        /// <summary>
        /// Gets or sets the last invoice accrual date.
        /// </summary>
        /// <value>
        /// The last invoice accrual date.
        /// </value>
        public DateTime? LastInvoiceAccrualDate { get; set; }

        /// <summary>
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the accounting company.
        /// </summary>
        /// <value>
        /// The name of the accounting company.
        /// </value>
        public string AccountingCompanyName { get; set; }

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
        /// Gets or sets the last po accrual date.
        /// </summary>
        /// <value>
        /// The last po accrual date.
        /// </value>
        public DateTime? LastPOAccrualDate { get; set; }

        /// <summary>
        /// Gets or sets the financial year start date.
        /// </summary>
        /// <value>
        /// The financial year start date.
        /// </value>
        public DateTime? FinancialYearStartDate { get; set; }

        /// <summary>
        /// Gets or sets the local currency.
        /// </summary>
        /// <value>
        /// The local currency.
        /// </value>
        public string LocalCurrency { get; set; }

        /// <summary>
        /// Gets or sets the financial year end date.
        /// </summary>
        /// <value>
        /// The financial year end date.
        /// </value>
        public DateTime? FinancialYearEndDate { get; set; }

        /// <summary>
        /// Gets or sets the management start date.
        /// </summary>
        /// <value>
        /// The management start date.
        /// </value>
        public DateTime? ManagementStartDate { get; set; }

        /// <summary>
        /// Gets or sets the general ledger cut off date.
        /// </summary>
        /// <value>
        /// The general ledger cut off date.
        /// </value>
        public DateTime? GeneralLedgerCutOffDate { get; set; }

        /// <summary>
        /// Gets or sets the sales ledger cut off date.
        /// </summary>
        /// <value>
        /// The sales ledger cut off date.
        /// </value>
        public DateTime? SalesLedgerCutOffDate { get; set; }

        /// <summary>
        /// Gets or sets the purchase ledger cut off date.
        /// </summary>
        /// <value>
        /// The purchase ledger cut off date.
        /// </value>
        public DateTime? PurchaseLedgerCutOffDate { get; set; }

        /// <summary>
        /// Gets or sets the agents ledger cutoff date.
        /// </summary>
        /// <value>
        /// The agents ledger cutoff date.
        /// </value>
        public DateTime? AgentsLedgerCutoffDate { get; set; }

        /// <summary>
        /// Gets or sets the last depreciation run date.
        /// </summary>
        /// <value>
        /// The last depreciation run date.
        /// </value>
        public DateTime? LastDepreciationRunDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is inactive.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is inactive; otherwise, <c>false</c>.
        /// </value>
        public bool IsInactive { get; set; }

        /// <summary>
        /// Gets or sets the discount account identifier.
        /// </summary>
        /// <value>
        /// The discount account identifier.
        /// </value>
        public string DiscountAccountId { get; set; }
    }
}
