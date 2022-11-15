using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// Summary purchase order cost view model
    /// </summary>
    public class SummaryPurchaseOrderCostViewModel
    {
        /// <summary>
        /// Gets or sets the cost list.
        /// </summary>
        /// <value>
        /// The cost list.
        /// </value>
        public List<SummaryCostViewModel> CostList { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the last po currency change log.
        /// </summary>
        /// <value>
        /// The last po currency change log.
        /// </value>
        public string LastPOCurrencyChangeLog { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show proposed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show proposed]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowProposed { get; set; }
    }
}
