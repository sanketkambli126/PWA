namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderDetailsForQuoteAuthorizationViewModel
    {
        /// <summary>
        /// Gets or sets the order title.
        /// </summary>
        /// <value>
        /// The order title.
        /// </value>
        public string OrderTitle { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account description.
        /// </summary>
        /// <value>
        /// The account description.
        /// </value>
        public string AccountDescription { get; set; }

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
        /// Gets or sets the vessel currency identifier.
        /// </summary>
        /// <value>
        /// The vessel currency identifier.
        /// </value>
        public string VesselCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the auxiliaries.
        /// </summary>
        /// <value>
        /// The auxiliaries.
        /// </value>
        public AuxiliaryDetailViewModel Auxiliaries { get; set; }
    }
}
