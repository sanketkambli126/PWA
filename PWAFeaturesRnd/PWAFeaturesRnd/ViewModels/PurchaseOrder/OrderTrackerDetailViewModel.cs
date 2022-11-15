using System;

namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// Order tracker details viewmodel
    /// </summary>
    public class OrderTrackerDetailViewModel
    {
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the requested date.
        /// </summary>
        /// <value>
        /// The requested date.
        /// </value>
        public string RequestedDate { get; set; }

        /// <summary>
        /// Gets or sets the authorise date.
        /// </summary>
        /// <value>
        /// The authorise date.
        /// </value>
        public string AuthoriseDate { get; set; }

        /// <summary>
        /// Gets or sets the order date.
        /// </summary>
        /// <value>
        /// The order date.
        /// </value>
        public string OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the due for delivery date.
        /// </summary>
        /// <value>
        /// The due for delivery date.
        /// </value>
        public string DueForDeliveryDate { get; set; }

        /// <summary>
        /// Gets or sets the received date.
        /// </summary>
        /// <value>
        /// The received date.
        /// </value>
        public string ReceivedDate { get; set; }
    }
}
