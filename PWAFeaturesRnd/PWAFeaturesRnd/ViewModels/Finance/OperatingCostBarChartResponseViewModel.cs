namespace PWAFeaturesRnd.ViewModels.Finance
{
    /// <summary>
    /// Operating Cost Bar  Chart Response ViewModel
    /// </summary>
    public class OperatingCostBarChartResponseViewModel
    {

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account description.
        /// </summary>
        /// <value>
        /// The account description.
        /// </value>
        public string AccountDescription { get; set; }

        /// <summary>
        /// Gets or sets the accurals.
        /// </summary>
        /// <value>
        /// The accurals.
        /// </value>
        public double Accurals { get; set; }

        /// <summary>
        /// Gets or sets the actual.
        /// </summary>
        /// <value>
        /// The actual.
        /// </value>
        public double Actual { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public double Total { get; set; }

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
        /// Gets or sets the drill down level.
        /// </summary>
        /// <value>
        /// The drill down level.
        /// </value>
        public int DrillDownLevel { get; set; }

        /// <summary>
        /// Gets or sets the operating cost URL.
        /// </summary>
        /// <value>
        /// The operating cost URL.
        /// </value>
        public string OperatingCostUrl { get; set; }

        /// <summary>
        /// Gets or sets the operating cost vessel identifier.
        /// </summary>
        /// <value>
        /// The operating cost vessel identifier.
        /// </value>
        public string operatingCostVesselId { get; set; }

        /// <summary>
        /// Gets or sets the transaction request URL.
        /// </summary>
        /// <value>
        /// The transaction request URL.
        /// </value>
        public string TransactionRequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the previous of transaction request URL.
        /// </summary>
        /// <value>
        /// The previous of transaction request URL.
        /// </value>
        public string PreviousOfTransactionRequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the opex URL.
        /// </summary>
        /// <value>
        /// The opex URL.
        /// </value>
        public string OpexDashboardUrl { get; set; }

        /// <summary>
        /// Gets or sets the dashboard cost currency.
        /// </summary>
        /// <value>
        /// The dashboard cost currency.
        /// </value>
        public string DashboardCostCurrency { get; set; }

        /// <summary>
        /// Gets or sets the dashboard drill down period.
        /// </summary>
        /// <value>
        /// The dashboard drill down period.
        /// </value>
        public string DashboardDrillDownPeriod { get; set; }

        /// <summary>
        /// Gets or sets the variance percent.
        /// </summary>
        /// <value>
        /// The variance percent.
        /// </value>
        public string VariancePercent { get; set; }

        /// <summary>
        /// Gets or sets the variance kpi priority.
        /// </summary>
        /// <value>
        /// The variance kpi priority.
        /// </value>
        public int VarianceKPIPriority { get; set; }

        /// <summary>
        /// Gets or sets the budget kpi priority.
        /// </summary>
        /// <value>
        /// The budget kpi priority.
        /// </value>
        public int BudgetKPIPriority { get; set; }

        /// <summary>
        /// Gets or sets the actual kpi priority.
        /// </summary>
        /// <value>
        /// The actual kpi priority.
        /// </value>
        public int ActualKPIPriority { get; set; }

        /// <summary>
        /// Gets or sets the accrual kpi priority.
        /// </summary>
        /// <value>
        /// The accrual kpi priority.
        /// </value>
        public int AccrualKPIPriority { get; set; }

        /// <summary>
        /// Gets or sets the accurals string.
        /// </summary>
        /// <value>
        /// The accurals string.
        /// </value>
        public string AccuralsStr { get; set; }

        /// <summary>
        /// Gets or sets the actual string.
        /// </summary>
        /// <value>
        /// The actual string.
        /// </value>
        public string ActualStr { get; set; }

        /// <summary>
        /// Gets or sets the budget string.
        /// </summary>
        /// <value>
        /// The budget string.
        /// </value>
        public string BudgetStr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is variance percent negative.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is variance percent negative; otherwise, <c>false</c>.
        /// </value>
        public bool IsVariancePercentNegative { get; set; }

    }
}
