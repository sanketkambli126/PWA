using System;

namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// Supplier Detail View Model
    /// </summary>
    public class SupplierDetailViewModel
    {
        /// <summary>
        /// Gets or sets the name of the supplier.
        /// </summary>
        /// <value>
        /// The name of the supplier.
        /// </value>
        public string SupplierName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is order authorised.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is order authorised; otherwise, <c>false</c>.
        /// </value>
        public bool IsOrderAuthorised { get; set; }

        /// <summary>
        /// Gets or sets the supplier order status.
        /// </summary>
        /// <value>
        /// The supplier order status.
        /// </value>
        public string SupplierOrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the order stage.
        /// </summary>
        /// <value>
        /// The order stage.
        /// </value>
        public string OrderStage { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is marcas.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is marcas; otherwise, <c>false</c>.
        /// </value>
        public bool IsMarcas { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is preferred.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is preferred; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreferred { get; set; }

        /// <summary>
        /// Gets or sets the supplier preferred reason.
        /// </summary>
        /// <value>
        /// The supplier preferred reason.
        /// </value>
        public string SupplierPreferredReason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is contracted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is contracted; otherwise, <c>false</c>.
        /// </value>
        public bool IsContracted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is base total not matched.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is base total not matched; otherwise, <c>false</c>.
        /// </value>
        public bool IsBaseTotalNotMatched { get; set; }

        /// <summary>
        /// Gets or sets the is complete quote.
        /// </summary>
        /// <value>
        /// The is complete quote.
        /// </value>
        public bool? IsCompleteQuote { get; set; }

        /// <summary>
        /// Gets or sets the maximum ex works days.
        /// </summary>
        /// <value>
        /// The maximum ex works days.
        /// </value>
        public int MaxExWorksDays { get; set; }

        /// <summary>
        /// Gets or sets the goods cost.
        /// </summary>
        /// <value>
        /// The goods cost.
        /// </value>
        public string GoodsCost { get; set; }

        /// <summary>
        /// Gets or sets the freight cost.
        /// </summary>
        /// <value>
        /// The freight cost.
        /// </value>
        public string FreightCost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is discount applied.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is discount applied; otherwise, <c>false</c>.
        /// </value>
        public bool IsDiscountApplied { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public string Total { get; set; }

        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public string Cur { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has notes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has notes; otherwise, <c>false</c>.
        /// </value>
        public bool HasNotes { get; set; }

        /// <summary>
        /// Gets or sets the base.
        /// </summary>
        /// <value>
        /// The base.
        /// </value>
        public string Base { get; set; }

        /// <summary>
        /// Gets or sets the RFQ issued.
        /// </summary>
        /// <value>
        /// The RFQ issued.
        /// </value>
        public DateTime? RFQIssued { get; set; }

        /// <summary>
        /// Gets or sets the base currency.
        /// </summary>
        /// <value>
        /// The base currency.
        /// </value>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Gets or sets the supplier order identifier.
        /// </summary>
        /// <value>
        /// The supplier order identifier.
        /// </value>
        public string SupplierOrderId { get; set; }

        /// <summary>
        /// Gets or sets the protected supplier order identifier.
        /// </summary>
        /// <value>
        /// The protected supplier order identifier.
        /// </value>
        public string ProtectedSupplierOrderId { get; set; }

        /// <summary>
        /// Gets or sets the supplier notes.
        /// </summary>
        /// <value>
        /// The supplier notes.
        /// </value>
        public string SupplierNotes { get; set; }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>
        /// The name of the port.
        /// </value>
        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the purchase order request URL.
        /// </summary>
        /// <value>
        /// The purchase order request URL.
        /// </value>
        public string PurchaseOrderRequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the purchase order request vessel identifier.
        /// </summary>
        /// <value>
        /// The purchase order request vessel identifier.
        /// </value>
        public string PurchaseOrderRequestVesselId { get; set; }

        /// <summary>
        /// Gets or sets the authorize quote request URL.
        /// </summary>
        /// <value>
        /// The authorize quote request URL.
        /// </value>
        public string AuthorizeQuoteRequestUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid for enquiry.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid for enquiry; otherwise, <c>false</c>.
        /// </value>
        public bool IsValidForEnquiry { get; set; }

        /// <summary>
        /// Gets or sets the name of the supplier order status.
        /// </summary>
        /// <value>
        /// The name of the supplier order status.
        /// </value>
        public string SupplierOrderStatusName { get; set; }

        /// <summary>
        /// Gets or sets the quote received date.
        /// </summary>
        /// <value>
        /// The quote received date.
        /// </value>
        public DateTime? QuoteReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the supplier rating.
        /// </summary>
        /// <value>
        /// The supplier rating.
        /// </value>
        public string SupplierRating { get; set; }

        /// <summary>
        /// Gets or sets the document count.
        /// </summary>
        /// <value>
        /// The document count.
        /// </value>
        public int DocumentCount { get; set; }

        /// <summary>
        /// Gets or sets the supplier company identifier.
        /// </summary>
        /// <value>
        /// The supplier company identifier.
        /// </value>
        public string SupplierCompanyId { get; set; }
    }
}
