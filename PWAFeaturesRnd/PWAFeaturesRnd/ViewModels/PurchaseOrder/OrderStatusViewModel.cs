namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// Order status view model
    /// </summary>
    public class OrderStatusViewModel
    {
        /// <summary>
        /// Gets or sets the name of the order stage.
        /// </summary>
        /// <value>
        /// The name of the order stage.
        /// </value>
        public string OrderStageName { get; set; }

        /// <summary>
        /// Gets or sets the order stage date.
        /// </summary>
        /// <value>
        /// The order stage date.
        /// </value>
        public string OrderStageDate { get; set; }

        /// <summary>
        /// Gets or sets the order stage.
        /// </summary>
        /// <value>
        /// The order stage.
        /// </value>
        public int OrderStage { get; set; }
    }
}
