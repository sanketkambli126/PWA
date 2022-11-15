using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public class GLTransactionFilter
    {
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
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the state of the transaction.
        /// </summary>
        /// <value>
        /// The state of the transaction.
        /// </value>
        public TransactionState TransactionState { get; set; }

        /// <summary>
        /// Gets or sets the name of the aux.
        /// </summary>
        /// <value>
        /// The name of the aux.
        /// </value>
        public Auxiliary? AuxName { get; set; }

        /// <summary>
        /// Gets or sets the aux value.
        /// </summary>
        /// <value>
        /// The aux value for Given Auxiliary
        /// </value>
        public string AuxValue { get; set; }

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
        /// Gets or sets the financial year start date.
        /// </summary>
        /// <value>
        /// The financial year start date.
        /// </value>
        public DateTime FinancialYearStartDate { get; set; }

        /// <summary>
        /// Gets or sets the actual.
        /// </summary>
        /// <value>
        /// The actual.
        /// </value>
        public double Actual { get; set; }
        /// <summary>
        /// Gets or sets the accurals.
        /// </summary>
        /// <value>
        /// The accurals.
        /// </value>
        public double Accurals { get; set; }
        /// <summary>
        /// Gets or sets the budget.
        /// </summary>
        /// <value>
        /// The budget.
        /// </value>
        public double Budget { get; set; }
        /// <summary>
        /// Gets or sets the variance.
        /// </summary>
        /// <value>
        /// The variance.
        /// </value>
        public double Variance { get; set; }
        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public double Total { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the Coy Type.
        /// </summary>
        /// <value>
        /// The name of the Coy Type.
        /// </value>
        public string CoyType { get; set; }

        /// <summary>
        /// Gets or sets the base Coy curr.
        /// </summary>
        public string BaseCoyCurr { get; set; }

        /// <summary>
        /// Gets or sets the accounting company CHH identifier.
        /// </summary>
        /// <value>
        /// The accounting company CHH identifier.
        /// </value>
        public string AccountingCompanyChhId { get; set; }
    }
}
