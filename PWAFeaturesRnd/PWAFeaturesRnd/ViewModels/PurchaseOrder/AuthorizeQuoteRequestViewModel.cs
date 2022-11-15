namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizeQuoteRequestViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

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
        /// Gets or sets the supplier order identifier.
        /// </summary>
        /// <value>
        /// The supplier order identifier.
        /// </value>
        public string SupplierOrderId { get; set; }

        /// <summary>
        /// Gets or sets the purchase order status.
        /// </summary>
        /// <value>
        /// The purchase order status.
        /// </value>
        public string PurchaseOrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the supplier order status.
        /// </summary>
        /// <value>
        /// The supplier order status.
        /// </value>
        public string SupplierOrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the purchase order stage.
        /// </summary>
        /// <value>
        /// The purchase order stage.
        /// </value>
        public string PurchaseOrderStage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [check prefunding flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [check prefunding flag]; otherwise, <c>false</c>.
        /// </value>
        public bool CheckPrefundingFlag { get; set; }

    }
}
