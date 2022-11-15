using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// /Order tracker details contract
    /// </summary>
    public class OrderTrackerDetail
    {
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the requested date.
        /// </summary>
        /// <value>
        /// The requested date.
        /// </value>
        public DateTime? RequestedDate { get; set; }

        /// <summary>
        /// Gets or sets the authorise date.
        /// </summary>
        /// <value>
        /// The authorise date.
        /// </value>
        public DateTime? AuthoriseDate { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public DateTime? OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the due for delivery date.
        /// </summary>
        /// <value>
        /// The due for delivery date.
        /// </value>
        public DateTime? DueForDeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        /// <value>
        /// The received date.
        /// </value>
        public DateTime? ReceivedDate { get; set; }
    }
}
