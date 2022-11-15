using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    ///
    /// </summary>
    public class OrderPreview
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

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
        /// Gets or sets the accounting company identifier (coyId).
        /// </summary>
        /// <value>
        /// The account company identifier.
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
        /// Gets or sets the order title.
        /// </summary>
        /// <value>
        /// The order title.
        /// </value>
        public string OrderTitle { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

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
        /// Gets or sets the type of the order.
        /// </summary>
        /// <value>
        /// The type of the order.
        /// </value>
        public string OrderType { get; set; }

        /// <summary>
        /// Gets or sets the order priority.
        /// </summary>
        /// <value>
        /// The order priority.
        /// </value>
        public string OrderPriority { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime? OrderDate { get; set; }//PO Issue date, (ORD_ORDERDATE)

        /// <summary>
        /// Gets or sets the supplier Name.
        /// </summary>
        /// <value>
        /// The supplier Name.
        /// </value>
        public string SupplierName { get; set; }

        /// <summary>
        /// Gets or sets the supplier identifier.
        /// </summary>
        /// <value>
        /// The supplier identifier.
        /// </value>
        public string SupplierId { get; set; }

        /// <summary>
        /// Gets or sets the warehouse name.
        /// </summary>
        /// <value>
        /// The warehouse name.
        /// </value>
        public string WarehouseName { get; set; }

        /// <summary>
        /// Gets or sets the requisition date.
        /// </summary>
        /// <value>
        /// The requisition date.
        /// </value>
        public DateTime? RequisitionDate { get; set; }//Date entered, Requested date (ORD_DATEENTERED)

        /// <summary>
        /// Gets or sets the order stage.
        /// </summary>
        /// <value>
        /// The order stage.
        /// </value>
        public string OrderStage { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the is tracked.
        /// </summary>
        /// <value>
        /// The is tracked.
        /// </value>
        public bool IsTracked { get; set; }

        /// <summary>
        /// Gets or sets the is request additional information.
        /// </summary>
        /// <value>
        /// The is request additional information.
        /// </value>
        public bool? IsRequestAdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the certificate received.
        /// </summary>
        /// <value>
        /// The certificate received.
        /// </value>
        public bool? CertificateReceived { get; set; }

        /// <summary>
        /// Gets or sets the certificate required.
        /// </summary>
        /// <value>
        /// The certificate required.
        /// </value>
        public bool? CertificateRequired { get; set; }

        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        /// <value>
        /// The received date.
        /// </value>
        public DateTime? ReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the received port.
        /// </summary>
        /// <value>
        /// The received port.
        /// </value>
        public string ReceivedPort { get; set; }

        /// <summary>
        /// Gets or sets the urgent order reason.
        /// </summary>
        /// <value>
        /// The urgent order reason.
        /// </value>
        public string UrgentOrderReason { get; set; }


        //This field is used only to achieve sorting on client side.
        //No need to make this  since this field is not needed on client side.
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the mapped defect count.
        /// </summary>
        /// <value>
        /// The mapped defect count.
        /// </value>
        public int MappedDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the catalog identifier.
        /// </summary>
        /// <value>
        /// The catalog identifier.
        /// </value>
        public string CatalogId { get; set; }

        /// <summary>
        /// Gets or sets the insurance claims identifier.
        /// </summary>
        /// <value>
        /// The insurance claims identifier.
        /// </value>
        public string InsuranceClaimsId { get; set; }

        /// <summary>
        /// Gets or sets the insurance claims description.
        /// </summary>
        /// <value>
        /// The insurance claims description.
        /// </value>
        public string InsuranceClaimsDescription { get; set; }

        /// <summary>
        /// Gets or sets the insurance claims short code.
        /// </summary>
        /// <value>
        /// The insurance claims short code.
        /// </value>
        public string InsuranceClaimsShortCode { get; set; }

        /// <summary>
        /// Gets or sets the insurance claims date.
        /// </summary>
        /// <value>
        /// The insurance claims date.
        /// </value>
        public DateTime? InsuranceClaimsDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is smart split applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is smart split applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsSmartSplitApplicable { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the expected delivery.
        /// </summary>
        /// <value>
        /// The expected delivery.
        /// </value>
        public DateTime? ExpectedDelivery { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent.
        /// </summary>
        /// <value>
        /// The name of the agent.
        /// </value>
        public string AgentName { get; set; }

        /// <summary>
        /// Gets or sets the expected port.
        /// </summary>
        /// <value>
        /// The expected port.
        /// </value>
        public string ExpectedPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is haz material.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is haz material; otherwise, <c>false</c>.
        /// </value>
        public bool IsHazMaterial { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is damaged item.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is damaged item; otherwise, <c>false</c>.
        /// </value>
        public bool IsDamagedItem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is poor quality.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is poor quality; otherwise, <c>false</c>.
        /// </value>
        public bool IsPoorQuality { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is poor packaging.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is poor packaging; otherwise, <c>false</c>.
        /// </value>
        public bool IsPoorPackaging { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is incorrect item.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is incorrect item; otherwise, <c>false</c>.
        /// </value>
        public bool IsIncorrectItem { get; set; }

        /// <summary>
        /// Gets or sets the is certificate received.
        /// </summary>
        /// <value>
        /// The is certificate received.
        /// true = Yes, Flase = No, Null = N/A
        /// </value>
        public bool? IsCertificateReceived { get; set; }      
    }
}
