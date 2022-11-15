namespace PWAFeaturesRnd.Models.Report.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public class OperatingCostBarChartResponse
    {
        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the actual.
        /// </summary>
        /// <value>
        /// The actual.
        /// </value>
        public double Actual { get; set; }

        /// <summary>
        /// Gets or sets the accrual.
        /// </summary>
        /// <value>
        /// The accrual.
        /// </value>
        public double Accrual { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public double Total { get; set; }

        /// <summary>
        /// Gets or sets the variance.
        /// </summary>
        /// <value>
        /// The variance.
        /// </value>
        public double Variance { get; set; }

        /// <summary>
        /// Gets or sets the budget.
        /// </summary>
        /// <value>
        /// The budget.
        /// </value>
        public double Budget { get; set; }

        /// <summary>
        /// Gets or sets the variance percent.
        /// </summary>
        /// <value>
        /// The variance percent.
        /// </value>
        public double VariancePercent { get; set; }

        /// <summary>
        /// Gets or sets the po accrual.
        /// </summary>
        /// <value>
        /// The po accrual.
        /// </value>
        public double POAccrual { get; set; }

        /// <summary>
        /// Gets or sets the po total.
        /// </summary>
        /// <value>
        /// The po total.
        /// </value>
        public double POTotal { get; set; }

        /// <summary>
        /// Gets or sets the po variance.
        /// </summary>
        /// <value>
        /// The po variance.
        /// </value>
        public double POVariance { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>
        /// The level.
        /// </value>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the parent3 acc and desc.
        /// </summary>
        /// <value>
        /// The parent3 acc and desc.
        /// </value>
        public string Parent3AccAndDesc { get; set; }

        /// <summary>
        /// Gets or sets the parent2 acc and desc.
        /// </summary>
        /// <value>
        /// The parent2 acc and desc.
        /// </value>
        public string Parent2AccAndDesc { get; set; }

        /// <summary>
        /// Gets or sets the parent1 acc and desc.
        /// </summary>
        /// <value>
        /// The parent1 acc and desc.
        /// </value>
        public string Parent1AccAndDesc { get; set; }

        /// <summary>
        /// Gets or sets the budget ytd priority.
        /// </summary>
        /// <value>
        /// The budget ytd priority.
        /// </value>
        public int BudgetYTDPriority { get; set; }

        /// <summary>
        /// Gets or sets the actual priority.
        /// </summary>
        /// <value>
        /// The actual priority.
        /// </value>
        public int ActualPriority { get; set; }

        /// <summary>
        /// Gets or sets the accrual priority.
        /// </summary>
        /// <value>
        /// The accrual priority.
        /// </value>
        public int AccrualPriority { get; set; }

        /// <summary>
        /// Gets or sets the variance priority.
        /// </summary>
        /// <value>
        /// The variance priority.
        /// </value>
        public int VariancePriority { get; set; }
    }
}
