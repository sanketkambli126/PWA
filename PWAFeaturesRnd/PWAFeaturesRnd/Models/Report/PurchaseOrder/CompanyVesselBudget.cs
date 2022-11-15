using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// Company vessel budget
    /// </summary>
    public class CompanyVesselBudget
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the budget detail identifier.
        /// </summary>
        /// <value>
        /// The budget detail identifier.
        /// </value>
        public string BudgetDetailId { get; set; }

        /// <summary>
        /// Gets or sets the budget start date.
        /// </summary>
        /// <value>
        /// The budget start date.
        /// </value>
        public DateTime BudgetStartDate { get; set; }

        /// <summary>
        /// Gets or sets the budget start end.
        /// </summary>
        /// <value>
        /// The budget start end.
        /// </value>
        public DateTime BudgetStartEnd { get; set; }

        /// <summary>
        /// Gets or sets the budget currency identifier.
        /// </summary>
        /// <value>
        /// The budget currency identifier.
        /// </value>
        public string BudgetCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the budget amount allocated.
        /// </summary>
        /// <value>
        /// The budget amount allocated.
        /// </value>
        public decimal BudgetAmountAllocated { get; set; }

        /// <summary>
        /// Gets or sets the budget actual spend.
        /// </summary>
        /// <value>
        /// The budget actual spend.
        /// </value>
        public decimal BudgetActualSpend { get; set; }

        /// <summary>
        /// Gets or sets the budget invoice accrual.
        /// </summary>
        /// <value>
        /// The budget invoice accrual.
        /// </value>
        public decimal BudgetInvoiceAccrual { get; set; }

        /// <summary>
        /// Gets or sets the budget purchase order accrual.
        /// </summary>
        /// <value>
        /// The budget purchase order accrual.
        /// </value>
        public decimal BudgetPurchaseOrderAccrual { get; set; }

        /// <summary>
        /// Gets or sets the purchase orders accruals.
        /// </summary>
        /// <value>
        /// The purchase orders accruals.
        /// </value>
        public List<PurchaseOrderAccrual> PurchaseOrdersAccruals { get; set; }

        /// <summary>
        /// Gets or sets the total accruals.
        /// </summary>
        /// <value>
        /// The total accruals.
        /// </value>
        public decimal TotalAccruals { get; set; }
    }
}
