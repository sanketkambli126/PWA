using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{

    /// <summary>
    /// Order invoice delivery details contract
    /// </summary>
    public class OrderInvoiceDeliveryDetail
    {
        /// <summary>
        /// Gets or sets the total cubic meters.
        /// </summary>
        /// <value>
        /// The total cubic meters.
        /// </value>
        public decimal? TotalCubicMeters { get; set; }

        /// <summary>
        /// Gets or sets the requisition date.
        /// </summary>
        /// <value>
        /// The requisition date.
        /// </value>
        public DateTime? RequisitionDate { get; set; }

        /// <summary>
        /// Gets or sets the order authorised date.
        /// </summary>
        /// <value>
        /// The order authorised date.
        /// </value>
        public DateTime? OrderAuthorisedDate { get; set; }

        /// <summary>
        /// Gets or sets the order authorised by.
        /// </summary>
        /// <value>
        /// The order authorised by.
        /// </value>
        public string OrderAuthorisedBy { get; set; }

        /// <summary>
        /// Gets or sets the order confirmed date.
        /// </summary>
        /// <value>
        /// The order confirmed date.
        /// </value>
        public DateTime? OrderConfirmedDate { get; set; }

        /// <summary>
        /// Gets or sets the expected delivery date.
        /// </summary>
        /// <value>
        /// The expected delivery date.
        /// </value>
        public DateTime? ExpectedDeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the weight in kg.
        /// </summary>
        /// <value>
        /// The weight in kg.
        /// </value>
        public decimal? WeightInKG { get; set; }

        /// <summary>
        /// Gets or sets the order received date.
        /// </summary>
        /// <value>
        /// The order received date.
        /// </value>
        public DateTime? OrderReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the order received on port.
        /// </summary>
        /// <value>
        /// The order received on port.
        /// </value>
        public string OrderReceivedOnPort { get; set; }

        /// <summary>
        /// Gets or sets the vessel currency identifier.
        /// </summary>
        /// <value>
        /// The vessel currency identifier.
        /// </value>
        public string VesselCurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the supplier delivery date.
        /// </summary>
        /// <value>
        /// The supplier delivery date.
        /// </value>
        public DateTime? SupplierDeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the account code description.
        /// </summary>
        /// <value>
        /// The account code description.
        /// </value>
        public string AccountCodeDescription { get; set; }

        /// <summary>
        /// Gets or sets the proposed goods service amount.
        /// </summary>
        /// <value>
        /// The proposed goods service amount.
        /// </value>
        public decimal? ProposedGoodsServiceAmount { get; set; }

        /// <summary>
        /// Gets or sets the proposed delivery amount.
        /// </summary>
        /// <value>
        /// The proposed delivery amount.
        /// </value>
        public decimal? ProposedDeliveryAmount { get; set; }

        /// <summary>
        /// Gets or sets the order expected on port.
        /// </summary>
        /// <value>
        /// The order expected on port.
        /// </value>
        public string OrderExpectedOnPort { get; set; }

        /// <summary>
        /// Gets or sets the number of pieces.
        /// </summary>
        /// <value>
        /// The number of pieces.
        /// </value>
        public int? NumberOfPieces { get; set; }

        /// <summary>
        /// Gets or sets the delivery date.
        /// </summary>
        /// <value>
        /// The delivery date.
        /// </value>
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the delivery days.
        /// </summary>
        /// <value>
        /// The delivery days.
        /// </value>
        public int? DeliveryDays { get; set; }

        /// <summary>
        /// Gets or sets the order stage.
        /// </summary>
        /// <value>
        /// The order stage.
        /// </value>
        public string OrderStage { get; set; }

        /// <summary>
        /// Gets or sets the supplier reference number.
        /// </summary>
        /// <value>
        /// The supplier reference number.
        /// </value>
        public string SupplierReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the order amount.
        /// </summary>
        /// <value>
        /// The order amount.
        /// </value>
        public decimal? OrderAmount { get; set; }

        /// <summary>
        /// Gets or sets the outstanding order amount.
        /// </summary>
        /// <value>
        /// The outstanding order amount.
        /// </value>
        public decimal? OutstandingOrderAmount { get; set; }

        /// <summary>
        /// Gets or sets the delivery cost.
        /// </summary>
        /// <value>
        /// The delivery cost.
        /// </value>
        public decimal? DeliveryCost { get; set; }

        /// <summary>
        /// Gets or sets the outstanding delivery cost.
        /// </summary>
        /// <value>
        /// The outstanding delivery cost.
        /// </value>
        public decimal? OutstandingDeliveryCost { get; set; }

        /// <summary>
        /// Gets or sets the outstanding total amount.
        /// </summary>
        /// <value>
        /// The outstanding total amount.
        /// </value>
        public decimal? OutstandingTotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value>
        public string CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the base order amount.
        /// </summary>
        /// <value>
        /// The base order amount.
        /// </value>
        public decimal BaseOrderAmount { get; set; }

        /// <summary>
        /// Gets or sets the total order base amount.
        /// </summary>
        /// <value>
        /// The total order base amount.
        /// </value>
        public decimal TotalOrderBaseAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is memo reply pending.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is memo reply pending; otherwise, <c>false</c>.
        /// </value>
        public bool IsMemoReplyPending { get; set; }

        /// <summary>
        /// Gets or sets the transport mode.
        /// </summary>
        /// <value>
        /// The transport mode.
        /// </value>
        public string TransportMode { get; set; }
    }
}
