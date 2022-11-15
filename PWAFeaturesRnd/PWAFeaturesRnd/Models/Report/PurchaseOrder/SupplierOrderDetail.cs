using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// Data contract to hold details of supplier order
    /// </summary>
    public class SupplierOrderDetail
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
        /// Gets or sets the supplier order identifier.
        /// </summary>
        /// <value>
        /// The supplier order identifier.
        /// </value>
        public string SupplierOrderId { get; set; }

        /// <summary>
        /// Gets or sets the supplier company identifier.
        /// </summary>
        /// <value>
        /// The supplier company identifier.
        /// </value>
        public string SupplierCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the supplier reference number.
        /// </summary>
        /// <value>
        /// The supplier reference number.
        /// </value>
        public string SupplierReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the supplier company.
        /// </summary>
        /// <value>
        /// The name of the supplier company.
        /// </value>
        public string SupplierCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the supplier order currency.
        /// </summary>
        /// <value>
        /// The supplier order currency.
        /// </value>
        public string SupplierOrderCurrency { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is supplier ordered.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is supplier ordered; otherwise, <c>false</c>.
        /// </value>
        public bool? IsSupplierOrdered { get; set; }

        /// <summary>
        /// Gets or sets the ex work days.
        /// </summary>
        /// <value>
        /// The ex work days.
        /// </value>
        public int? ExWorkDays { get; set; }

        /// <summary>
        /// Gets or sets the supplier order valid till date.
        /// </summary>
        /// <value>
        /// The supplier order valid till date.
        /// </value>
        public DateTime? SupplierOrderValidTillDate { get; set; }

        /// <summary>
        /// Gets or sets the is complete quote.
        /// </summary>
        /// <value>
        /// The is complete quote.
        /// </value>
        public bool? IsCompleteQuote { get; set; }

        /// <summary>
        /// Gets or sets the total cost.
        /// </summary>
        /// <value>
        /// The total cost.
        /// </value>
        public decimal? TotalCost { get; set; }

        /// <summary>
        /// Gets or sets the base total cost.
        /// </summary>
        /// <value>
        /// The base total cost.
        /// </value>
        public decimal? BaseTotalCost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is order authorised.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is order authorised; otherwise, <c>false</c>.
        /// </value>
        public bool? IsOrderAuthorised { get; set; }

        /// <summary>
        /// Gets or sets the supplier order status.
        /// </summary>
        /// <value>
        /// The supplier order status.
        /// </value>
        public string SupplierOrderStatus { get; set; }

        /// <summary>
        /// The ex work country identifier
        /// </summary>
        public string ExWorkCountryId { get; set; }

        /// <summary>
        /// Gets or sets the ex work country.
        /// </summary>
        /// <value>
        /// The ex work country.
        /// </value>
        public string ExWorkCountry { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public string OrderStatus { get; set; }

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
        /// <c>true</c> if this instance is preferred; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreferred { get; set; }

        /// <summary>
        /// Gets or sets the supplier preferred reason.
        /// </summary>
        /// <value>
        /// The preferred reason.
        /// </value>
        public string SupplierPreferredReason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is contracted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is contracted; otherwise, <c>false</c>.
        /// </value>
        public bool IsContracted { get; set; }

        /// <summary>
        /// Gets or sets the port identifier.
        /// </summary>
        /// <value>
        /// The port identifier.
        /// </value>
        public string PortId { get; set; }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>
        /// The name of the port.
        /// </value>
        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the RFQ issue date.
        /// </summary>
        /// <value>
        /// The RFQ issue date.
        /// </value>
        public DateTime? RFQIssueDate { get; set; }

        /// <summary>
        /// Gets or sets the quote received date.
        /// </summary>
        /// <value>
        /// The quote received date.
        /// </value>
        public DateTime? QuoteReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the spare part type code.
        /// </summary>
        /// <value>
        /// The spare part type code.
        /// </value>
        public string SparePartTypeCode { get; set; }

        /// <summary>
        /// Gets or sets the spare part type identifier.
        /// </summary>
        /// <value>
        /// The spare part type identifier.
        /// </value>
        public string SparePartTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the spare part type.
        /// </summary>
        /// <value>
        /// The name of the spare part type.
        /// </value>
        public string SparePartTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hazardous.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is hazardous; otherwise, <c>false</c>.
        /// </value>
        public bool IsHazardous { get; set; }

        /// <summary>
        /// Gets or sets the supplier rating.
        /// </summary>
        /// <value>
        /// The supplier rating.
        /// </value>
        public string SupplierRating { get; set; }

        /// <summary>
        /// Gets or sets the purchase order date.
        /// </summary>
        /// <value>
        /// The purchase order date.
        /// </value>
        public DateTime? PurchaseOrderDate { get; set; }

        /// <summary>
        /// Gets or sets the base currency.
        /// </summary>
        /// <value>
        /// The base currency.
        /// </value>
        public string BaseCurrency { get; set; }

        /// <summary>
        /// Gets or sets the freight cost.
        /// </summary>
        /// <value>
        /// The freight cost.
        /// </value>
        public decimal? FreightCost { get; set; }

        /// <summary>
        /// Gets or sets the base freight cost.
        /// </summary>
        /// <value>
        /// The base freight cost.
        /// </value>
        public decimal? BaseFreightCost { get; set; }

        /// <summary>
        /// Gets or sets the order stage.
        /// </summary>
        /// <value>
        /// The order stage.
        /// </value>
        public string OrderStage { get; set; }

        /// <summary>
        /// Gets or sets the supplier country.
        /// </summary>
        /// <value>
        /// The supplier country.
        /// </value>
        public string SupplierCountry { get; set; }

        /// <summary>
        /// Gets or sets the supplier notes.
        /// </summary>
        /// <value>
        /// The supplier notes.
        /// </value>
        public string SupplierNotes { get; set; }

        /// <summary>
        /// Gets or sets the have read notes.
        /// </summary>
        /// <value>
        /// The have read notes.
        /// </value>
        public bool HaveReadNotes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has discount applied.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has discount applied; otherwise, <c>false</c>.
        /// </value>
        public bool HasDiscountApplied { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has supplier notes.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has supplier notes; otherwise, <c>false</c>.
        /// </value>
        public bool HasSupplierNotes { get; set; }

        /// <summary>
        /// Gets or sets the credit days.
        /// </summary>
        /// <value>
        /// The credit days.
        /// </value>
        public int? CreditDays { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is company certified.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is company certified; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompanyCertified { get; set; }

        /// <summary>
        /// Gets or sets the supplier order line total cost.
        /// </summary>
        /// <value>
        /// The supplier order line total cost.
        /// </value>
        public decimal? SupplierOrderLineTotalCost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is company compliance.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is company compliance; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompanyCompliance { get; set; }

        /// <summary>
        /// Gets or sets the company compliance document identifier.
        /// </summary>
        /// <value>
        /// The company compliance document identifier.
        /// </value>
        public string CompanyComplianceDocId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is proforma requested.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is proforma requested; otherwise, <c>false</c>.
        /// </value>
        public bool IsProformaRequested { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is supplier audit completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is supplier audit completed; otherwise, <c>false</c>.
        /// </value>
        public bool IsSupplierAuditCompleted { get; set; }

        /// <summary>
        /// Gets or sets the supplier audit start date.
        /// </summary>
        /// <value>
        /// The supplier audit start date.
        /// </value>
        public DateTime? SupplierAuditStartDate { get; set; }

        /// <summary>
        /// Gets or sets the supplier audit end date.
        /// </summary>
        /// <value>
        /// The supplier audit end date.
        /// </value>
        public DateTime? SupplierAuditEndDate { get; set; }

        /// <summary>
        /// Gets or sets the document count.
        /// </summary>
        /// <value>
        /// The document count.
        /// </value>
        public int DocumentCount { get; set; }
    }
}
