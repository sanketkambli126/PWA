namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// Purchase Order Detail View Model
    /// </summary>
    public class PurchaseOrderDetailViewModel
	{
        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the name of the order status.
        /// </summary>
        /// <value>
        /// The name of the order status.
        /// </value>
        public string OrderStatusName { get; set; }

        /// <summary>
        /// Gets or sets the order stage.
        /// </summary>
        /// <value>
        /// The order stage.
        /// </value>
        public string OrderStage { get; set; }
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
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }
        /// <summary>
        /// Gets or sets the type of the order.
        /// </summary>
        /// <value>
        /// The type of the order.
        /// </value>
        public string OrderType { get; set; }
        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }
        /// <summary>
        /// Gets or sets the account code description.
        /// </summary>
        /// <value>
        /// The account code description.
        /// </value>
        public string AccountCodeDescription { get; set; }

        /// <summary>
        /// Gets or sets the protected order number.
        /// </summary>
        /// <value>
        /// The protected order number.
        /// </value>
        public string ProtectedOrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the protected accounting company identifier.
        /// </summary>
        /// <value>
        /// The protected accounting company identifier.
        /// </value>
        public string ProtectedAccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the order priority.
        /// </summary>
        /// <value>
        /// The order priority.
        /// </value>
        public string OrderPriority { get; set; }

        /// <summary>
        /// Gets or sets the purchase order request.
        /// </summary>
        /// <value>
        /// The purchase order request.
        /// </value>
        public string PurchaseOrderRequest { get; set; }

        /// <summary>
        /// Gets or sets the purchase order vessel identifier.
        /// </summary>
        /// <value>
        /// The purchase order vessel identifier.
        /// </value>
        public string PurchaseOrderVesselId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is high priority.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is high priority; otherwise, <c>false</c>.
        /// </value>
        public bool IsHighPriority { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [selected justification reason].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [selected justification reason]; otherwise, <c>false</c>.
        /// </value>
        public bool SelectedJustificationReason { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is from view record.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is from view record; otherwise, <c>false</c>.
        /// </value>
        public bool IsFromViewRecord { get; set; }

		/// <summary>
		/// Gets or sets the active mobile tab class.
		/// </summary>
		/// <value>
		/// The active mobile tab class.
		/// </value>
		public string ActiveMobileTabClass { get; set; }
	}
}
