using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// Company vessel Budgedt view model
    /// </summary>
    public class CompanyVesselBudgetViewModel
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
        public string BudgetStartDate { get; set; }

        /// <summary>
        /// Gets or sets the budget start end.
        /// </summary>
        /// <value>
        /// The budget start end.
        /// </value>
        public string BudgetStartEnd { get; set; }

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
        public string BudgetAmountAllocated { get; set; }

        /// <summary>
        /// Gets or sets the budget actual spend.
        /// </summary>
        /// <value>
        /// The budget actual spend.
        /// </value>
        public string BudgetActualSpend { get; set; }

        /// <summary>
        /// Gets or sets the budget invoice accrual.
        /// </summary>
        /// <value>
        /// The budget invoice accrual.
        /// </value>
        public string BudgetInvoiceAccrual { get; set; }

        /// <summary>
        /// Gets or sets the budget purchase order accrual.
        /// </summary>
        /// <value>
        /// The budget purchase order accrual.
        /// </value>
        public string BudgetPurchaseOrderAccrual { get; set; }

        /// <summary>
        /// Gets or sets the purchase orders accruals.
        /// </summary>
        /// <value>
        /// The purchase orders accruals.
        /// </value>
        public List<PurchaseOrderAccrualViewModel> PurchaseOrdersAccruals { get; set; }

        /// <summary>
        /// Gets or sets the total accruals.
        /// </summary>
        /// <value>
        /// The total accruals.
        /// </value>
        public string TotalAccruals { get; set; }

        /// <summary>
        /// Gets or sets the total budget.
        /// </summary>
        /// <value>
        /// The total budget.
        /// </value>
        public string TotalBudget { get; set; }

        /// <summary>
        /// Gets or sets the variance amount.
        /// </summary>
        /// <value>
        /// The variance amount.
        /// </value>
        public string VarianceAmount { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the variance amount decimal.
        /// </summary>
        /// <value>
        /// The variance amount decimal.
        /// </value>
        public decimal? VarianceAmountDecimal { get; set; }

        /// <summary>
        /// Gets or sets the total accruals decimal.
        /// </summary>
        /// <value>
        /// The total accruals decimal.
        /// </value>
        public decimal TotalAccrualsDecimal { get; set; }

        /// <summary>
        /// Gets or sets the budget actual spend decimal.
        /// </summary>
        /// <value>
        /// The budget actual spend decimal.
        /// </value>
        public decimal BudgetActualSpendDecimal { get; set; }

        /// <summary>
        /// Gets or sets the budget amount allocated decimal.
        /// </summary>
        /// <value>
        /// The budget amount allocated decimal.
        /// </value>
        public decimal BudgetAmountAllocatedDecimal { get; set; }
    }
}
