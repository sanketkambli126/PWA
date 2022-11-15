namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// Summary purchase order cost model
    /// </summary>
    public class SummaryPurchaseOrderCost
    {
        /// <summary>
        /// Gets or sets the supplier reference.
        /// </summary>
        /// <value>
        /// The supplier reference.
        /// </value>
        public string SupplierReference { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the delivery amount.
        /// </summary>
        /// <value>
        /// The delivery amount.
        /// </value>
        public decimal? DeliveryAmount { get; set; }

        /// <summary>
        /// Gets or sets the goods service amount.
        /// </summary>
        /// <value>
        /// The goods service amount.
        /// </value>
        public decimal? GoodsServiceAmount { get; set; }

        /// <summary>
        /// Gets or sets the proposed delivery amount.
        /// </summary>
        /// <value>
        /// The proposed delivery amount.
        /// </value>
        public decimal? ProposedDeliveryAmount { get; set; }

        /// <summary>
        /// Gets or sets the proposed goods service amount.
        /// </summary>
        /// <value>
        /// The proposed goods service amount.
        /// </value>
        public decimal? ProposedGoodsServiceAmount { get; set; }

        /// <summary>
        /// Gets or sets the outstanding goods services amount.
        /// </summary>
        /// <value>
        /// The outstanding goods services amount.
        /// </value>
        public decimal? OutstandingGoodsServicesAmount { get; set; }

        /// <summary>
        /// Gets or sets the outstanding delivery amount.
        /// </summary>
        /// <value>
        /// The outstanding delivery amount.
        /// </value>
        public decimal? OutstandingDeliveryAmount { get; set; }

        /// <summary>
        /// Gets or sets the total order amount.
        /// </summary>
        /// <value>
        /// The total order amount.
        /// </value>
        public decimal? TotalOrderAmount { get; set; }

        /// <summary>
        /// Gets or sets the total proposed amount.
        /// </summary>
        /// <value>
        /// The total proposed amount.
        /// </value>
        public decimal? TotalProposedAmount { get; set; }

        /// <summary>
        /// Gets or sets the last po currency change log.
        /// </summary>
        /// <value>
        /// The last po currency change log.
        /// </value>
        public string LastPOCurrencyChangeLog { get; set; }
    }
}
