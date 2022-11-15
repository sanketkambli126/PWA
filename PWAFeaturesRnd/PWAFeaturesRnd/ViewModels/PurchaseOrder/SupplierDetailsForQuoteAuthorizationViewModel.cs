namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// Supplier Details For Quote Authorization ViewModel
    /// </summary>
    public class SupplierDetailsForQuoteAuthorizationViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is supplier given notes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is supplier given notes; otherwise, <c>false</c>.
        /// </value>
        public bool IsSupplierGivenNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is feedback required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is feedback required; otherwise, <c>false</c>.
        /// </value>
        public bool IsFeedbackRequired { get; set; }

        /// <summary>
        /// Gets or sets the supplier notes.
        /// </summary>
        /// <value>
        /// The supplier notes.
        /// </value>
        public string SupplierNotes { get; set; }

        /// <summary>
        /// Gets or sets the supplier order status.
        /// </summary>
        /// <value>
        /// The supplier order status.
        /// </value>
        public string SupplierOrderStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is status tr.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is status tr; otherwise, <c>false</c>.
        /// </value>
        public bool IsStatusTR { get; set; }

        /// <summary>
        /// Gets or sets the supplier order status warning.
        /// </summary>
        /// <value>
        /// The supplier order status warning.
        /// </value>
        public string SupplierOrderStatusWarning { get; set; }

        /// <summary>
        /// Gets or sets the name of the supplier.
        /// </summary>
        /// <value>
        /// The name of the supplier.
        /// </value>
        public string SupplierName { get; set; }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>
        /// The name of the port.
        /// </value>
        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the expected work country.
        /// </summary>
        /// <value>
        /// The expected work country.
        /// </value>
        public string ExpectedWorkCountry { get; set; }

        /// <summary>
        /// Gets or sets the expected work days.
        /// </summary>
        /// <value>
        /// The expected work days.
        /// </value>
        public string ExpectedWorkDays { get; set; }

        /// <summary>
        /// Gets or sets the is hazardous goods.
        /// </summary>
        /// <value>
        /// The is hazardous goods.
        /// </value>
        public string IsHazardousGoods { get; set; }

        /// <summary>
        /// Gets or sets the spare part type detail.
        /// </summary>
        /// <value>
        /// The spare part type detail.
        /// </value>
        public string SparePartTypeDetail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is full items quoted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is full items quoted; otherwise, <c>false</c>.
        /// </value>
        public bool IsFullItemsQuoted { get; set; }

        /// <summary>
        /// Gets or sets the full items quoted.
        /// </summary>
        /// <value>
        /// The full items quoted.
        /// </value>
        public string FullItemsQuoted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is items not quoted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is items not quoted; otherwise, <c>false</c>.
        /// </value>
        public bool IsItemsNotQuoted { get; set; }

        /// <summary>
        /// Gets or sets the items not quoted count.
        /// </summary>
        /// <value>
        /// The items not quoted count.
        /// </value>
        public int ItemsNotQuotedCount { get; set; }

        /// <summary>
        /// Gets or sets the is proforma requested.
        /// </summary>
        /// <value>
        /// The is proforma requested.
        /// </value>
        public string IsProformaRequested { get; set; }

        /// <summary>
        /// Gets or sets the encrypted supplier company identifier.
        /// </summary>
        /// <value>
        /// The encrypted supplier company identifier.
        /// </value>
        public string EncryptedSupplierCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the quoted amount.
        /// </summary>
        /// <value>
        /// The quoted amount.
        /// </value>
        public string QuotedAmount { get; set; }

        /// <summary>
        /// Gets or sets the quote amount in po vessel currency.
        /// </summary>
        /// <value>
        /// The quote amount in po vessel currency.
        /// </value>
        public string QuoteAmountInPoVesselCurrency { get; set; }

        /// <summary>
        /// Gets or sets the freight accrual in po vessel currency.
        /// </summary>
        /// <value>
        /// The freight accrual in po vessel currency.
        /// </value>
        public string FreightAccrualInPoVesselCurrency { get; set; }

        /// <summary>
        /// Gets or sets the quote amount in po vessel currency decimal.
        /// </summary>
        /// <value>
        /// The quote amount in po vessel currency decimal.
        /// </value>
        public decimal QuoteAmountInPoVesselCurrencyDecimal { get; set; }

        /// <summary>
        /// Gets or sets the freight accrual in po vessel currency decimal.
        /// </summary>
        /// <value>
        /// The freight accrual in po vessel currency decimal.
        /// </value>
        public decimal FreightAccrualInPoVesselCurrencyDecimal { get; set; }

        /// <summary>
        /// Gets or sets the feedback supplier order identifier.
        /// </summary>
        /// <value>
        /// The feedback supplier order identifier.
        /// </value>
        public string FeedbackSupplierOrderId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is client authentication required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is client authentication required; otherwise, <c>false</c>.
        /// </value>
        public bool IsClientAuthRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is authentication level zero.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is authentication level zero; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthLevelAuthorizationRequired { get; set; }
    }
}
