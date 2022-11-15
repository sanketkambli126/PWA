using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Finance
{
    /// <summary>
    /// 
    /// </summary>
    public class OperatinCostViewModel
    {
        /// <summary>
        /// Gets or sets the budget.
        /// </summary>
        /// <value>
        /// The budget.
        /// </value>
        public double Budget { get; set; }

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
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

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
        /// Gets or sets the operating cost list.
        /// </summary>
        /// <value>
        /// The operating cost list.
        /// </value>
        public List<OperatingCostBarChartResponseViewModel> OperatingCostList { get; set; }

        /// <summary>
        /// Gets or sets the previous operation cost URL.
        /// </summary>
        /// <value>
        /// The previous operation cost URL.
        /// </value>
        public string PreviousOperationCostUrl { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the previous stage.
        /// </summary>
        /// <value>
        /// The name of the previous stage.
        /// </value>
        public string PreviousStageName { get; set; }
    }
}
