namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// SupplierPurchaseOrderRequest
    /// </summary>
    public class SupplierPurchaseOrderRequest
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
        /// Gets or sets a value indicating whether [fetch supplier order line total cost].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch supplier order line total cost]; otherwise, <c>false</c>.
        /// </value>
        public bool FetchSupplierOrderLineTotalCost { get; set; }

        /// <summary>
        /// Gets or sets the purchase order request URL.
        /// </summary>
        /// <value>
        /// The purchase order request URL.
        /// </value>
        public string PurchaseOrderRequestURL { get; set; }

        /// <summary>
        /// Gets or sets the purchase order request vessel identifier.
        /// </summary>
        /// <value>
        /// The purchase order request vessel identifier.
        /// </value>
        public string PurchaseOrderRequestVesselId { get; set; }
    }
}
