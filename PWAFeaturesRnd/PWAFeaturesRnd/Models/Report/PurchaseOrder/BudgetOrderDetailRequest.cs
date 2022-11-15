using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// Budget Order Detail Request
    /// </summary>
    public class BudgetOrderDetailRequest
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

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
        /// Gets or sets the purchase order request URL.
        /// </summary>
        /// <value>
        /// The purchase order request URL.
        /// </value>
        public string PurchaseOrderRequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the purchase order request vessl identifier.
        /// </summary>
        /// <value>
        /// The purchase order request vessl identifier.
        /// </value>
        public string PurchaseOrderRequestVesslId { get; set; }
    }
}
