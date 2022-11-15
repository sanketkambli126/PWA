namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// Cost details request
    /// </summary>
    public class POSummaryCostDetailsRequest
    {
        /// <summary>
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the order stage.
        /// </summary>
        /// <value>
        /// The order stage.
        /// </value>
        public string OrderStage { get; set; }
    }
}
