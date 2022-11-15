namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// ViewModel for clientAuthoriseOrder
    /// </summary>
    public class ClientAuthoriseOrderRequestViewModel
    {
        /// <summary>
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approve.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is approve; otherwise, <c>false</c>.
        /// </value>
        public bool IsApprove { get; set; }

    }
}
