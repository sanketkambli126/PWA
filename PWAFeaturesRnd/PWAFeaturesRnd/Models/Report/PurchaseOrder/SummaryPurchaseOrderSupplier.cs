using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// summary purchase order supplier contract
    /// </summary>
    public class SummaryPurchaseOrderSupplier
    {
        /// <summary>
        /// Gets or sets the supplier reference.
        /// </summary>
        /// <value>
        /// The supplier reference.
        /// </value>
        public string SupplierReference { get; set; }

        /// <summary>
        /// Gets or sets the date authorised.
        /// </summary>
        /// <value>
        /// The date authorised.
        /// </value>
        public DateTime? DateAuthorised { get; set; }

        /// <summary>
        /// Gets or sets the authorised by.
        /// </summary>
        /// <value>
        /// The authorised by.
        /// </value>
        public string AuthorisedBy { get; set; }

        /// <summary>
        /// Gets or sets the company details.
        /// </summary>
        /// <value>
        /// The company details.
        /// </value>
        public CompanyDetails CompanyDetails { get; set; }

        /// <summary>
        /// Gets or sets the confirmation date.
        /// </summary>
        /// <value>
        /// The confirmation date.
        /// </value>
        public DateTime? ConfirmationDate { get; set; }

        /// <summary>
        /// Gets or sets the purchase order identifier.
        /// </summary>
        /// <value>
        /// The purchase order identifier.
        /// </value>
        public string PurchaseOrderId { get; set; }

        /// <summary>
        /// Gets or sets the purchase order chased date.
        /// </summary>
        /// <value>
        /// The purchase order chased date.
        /// </value>
        public DateTime? PurchaseOrderChasedDate { get; set; }

        /// <summary>
        /// Gets or sets the invoice chased date.
        /// </summary>
        /// <value>
        /// The invoice chased date.
        /// </value>
        public DateTime? InvoiceChasedDate { get; set; }
    }
}
