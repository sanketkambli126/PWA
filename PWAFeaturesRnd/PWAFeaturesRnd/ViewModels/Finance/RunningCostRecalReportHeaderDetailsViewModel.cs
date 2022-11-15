using System;

namespace PWAFeaturesRnd.ViewModels.Finance
{
    /// <summary>
    /// Running CostRecalReport HeaderDetails ViewModel
    /// </summary>
    public class RunningCostRecalReportHeaderDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the number of days.
        /// </summary>
        /// <value>
        /// The number of days.
        /// </value>
        public int NumberOfDays { get; set; }

        /// <summary>
        /// Gets or sets the rc from date time.
        /// </summary>
        /// <value>
        /// The rc from date time.
        /// </value>
        public string RCFromDateTime { get; set; }

        /// <summary>
        /// Gets or sets the rc to date time.
        /// </summary>
        /// <value>
        /// The rc to date time.
        /// </value>
        public string RCToDateTime { get; set; }

    }
}
