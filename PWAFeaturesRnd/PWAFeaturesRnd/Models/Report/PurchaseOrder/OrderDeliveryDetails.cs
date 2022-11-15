using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// order delivery details
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Models.Report.PurchaseOrder.CompanyDetails" />
    public class OrderDeliveryDetails: CompanyDetails
    {
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
        /// Gets or sets the expected date.
        /// </summary>
        /// <value>
        /// The expected date.
        /// </value>
        public DateTime? ExpectedDate { get; set; }

        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        /// <value>
        /// The received date.
        /// </value>
        public DateTime? ReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the order previous status.
        /// </summary>
        /// <value>
        /// The order previous status.
        /// </value>
        public string OrderPreviousStatus { get; set; }
    }
}
