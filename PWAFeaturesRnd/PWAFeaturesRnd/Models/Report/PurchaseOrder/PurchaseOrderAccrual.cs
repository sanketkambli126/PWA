namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// Purchase order accural
    /// </summary>
    public class PurchaseOrderAccrual
    {
        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the order no.
        /// </summary>
        /// <value>
        /// The order no.
        /// </value>
        public string OrderNo { get; set; }

        /// <summary>
        /// Gets or sets the order balance.
        /// </summary>
        /// <value>
        /// The order balance.
        /// </value>
        public decimal OrderBalance { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value>
        public string CurrencyId { get; set; }
    }
}
