using System;

namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectRequisitionViewModel
    {
        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }

        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the status description.
        /// </summary>
        /// <value>
        /// The status description.
        /// </value>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the status short code.
        /// </summary>
        /// <value>
        /// The status short code.
        /// </value>
        public string StatusShortCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is status visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is status visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsStatusVisible { get; set; }

        /// <summary>
        /// Gets or sets the color of the order status.
        /// </summary>
        /// <value>
        /// The color of the order status.
        /// </value>
        public string OrderStatusColor { get; set; }

        /// <summary>
        /// Gets or sets the name of the order.
        /// </summary>
        /// <value>
        /// The name of the order.
        /// </value>
        public string OrderName { get; set; }

        /// <summary>
        /// Gets or sets the type of the order.
        /// </summary>
        /// <value>
        /// The type of the order.
        /// </value>
        public string OrderType { get; set; }

        /// <summary>
        /// Gets or sets the requested date.
        /// </summary>
        /// <value>
        /// The requested date.
        /// </value>
        public DateTime? RequestedDate { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the expected delivery date.
        /// </summary>
        /// <value>
        /// The expected delivery date.
        /// </value>
        public DateTime? ExpectedDeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        /// <value>
        /// The received date.
        /// </value>
        public DateTime? ReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the purchase order URL.
        /// </summary>
        /// <value>
        /// The purchase order URL.
        /// </value>
        public string PurchaseOrderUrl { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }
    }
}
