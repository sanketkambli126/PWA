using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Models.Report.BaseFilter" />
    public class PurchasingFilter : BaseFilter
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the purchase order status.
        /// </summary>
        /// <value>
        /// The purchase order status.
        /// </value>
        public List<PurchaseOrderStatus> PurchaseOrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the purchase order stage.
        /// </summary>
        /// <value>
        /// The purchase order stage.
        /// </value>
        public PurchaseOrderStage? PurchaseOrderStage { get; set; }

        /// <summary>
        /// Gets or sets the purchase order types.
        /// </summary>
        /// <value>
        /// The purchase order types.
        /// </value>
        public IEnumerable<PurchaseOrderType> PurchaseOrderTypes { get; set; }

        /// <summary>
        /// Gets or sets the purchase order priorities.
        /// </summary>
        /// <value>
        /// The purchase order priorities.
        /// </value>
        public IEnumerable<PurchaseOrderPriority> PurchaseOrderPriorities { get; set; }

        /// <summary>
        /// Gets or sets the menu item.
        /// </summary>
        /// <value>
        /// The menu item.
        /// </value>
        public UserMenuItem MenuItem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tracked order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is tracked order; otherwise, <c>false</c>.
        /// </value>
        public bool IsTrackedOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is request additional information.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is request additional information; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequestAdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is authorisation pending order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is authorisation pending order; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthorisationPendingOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is non confirmed order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is non confirmed order; otherwise, <c>false</c>.
        /// </value>
        public bool IsNonConfirmedOrder { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is show overdue order.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is show overdue order; otherwise, <c>false</c>.
        /// </value>
        public bool IsShowOverdueOrder { get; set; }

        /// <summary>
        /// Gets or sets the supplier identifier.
        /// </summary>
        /// <value>
        /// The supplier identifier.
        /// </value>
        public string SupplierId { get; set; } //for getting agreed supplier?

        /// <summary>
        /// Gets or sets the order from date.
        /// </summary>
        /// <value>
        /// The order from date.
        /// </value>
        public System.DateTime? OrderFromDate { get; set; } //checks ORD_UpdatedOn

        /// <summary>
        /// Gets or sets the order to date.
        /// </summary>
        /// <value>
        /// The order to date.
        /// </value>
        public System.DateTime? OrderToDate { get; set; } //check ORD_UpdatedOn

        /// <summary>
        /// Gets or sets the incoice reference identifier.
        /// </summary>
        /// <value>
        /// The incoice reference identifier.
        /// </value>
        public string IncoiceReferenceId { get; set; } //joins to get PO's related to the invoice SUPInv field (users startWith)

        /// <summary>
        /// Gets or sets the order no.
        /// </summary>
        /// <value>
        /// The order no.
        /// </value>
        public string OrderNo { get; set; } //gets the order number ORD_OrderNo

        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; } //gets the order number Coy_Id

        /// <summary>
        /// Gets or sets the acc identifier.
        /// </summary>
        /// <value>
        /// The acc identifier.
        /// </value>
        public string AccId { get; set; } //checks the ACCId on PO table

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; } //check ORD Title using Start With

        /// <summary>
        /// Gets or sets the type of the order number.
        /// </summary>
        /// <value>
        /// The type of the order number.
        /// </value>
        public PurchasingFilterOrderNumberType? OrderNumberType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [buyer orders].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [buyer orders]; otherwise, <c>false</c>.
        /// </value>
        public bool BuyerOrders { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is authentication required status.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is authentication required status; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthRequiredStatus { get; set; }

        /// <summary>
        /// Gets or sets the excluded stages.
        /// </summary>
        /// <value>
        /// The excluded stages.
        /// </value>
        public List<PurchaseOrderStage> ExcludedStages { get; set; }

        /// <summary>
        /// Gets or sets the recieved date.
        /// </summary>
        /// <value>
        /// The recieved date.
        /// </value>
        public DateTime? RecievedDate { get; set; }

        /// <summary>
        /// Converts to reqorddate.
        /// </summary>
        /// <value>
        /// To req ord date.
        /// </value>
        public DateTime? ToReqOrdDate { get; set; } //for filtering on ORD_DateEntered and ORD_OrderDate

        /// <summary>
        /// Gets or sets from req ord date.
        /// </summary>
        /// <value>
        /// From req ord date.
        /// </value>
        public DateTime? FromReqOrdDate { get; set; } //for filtering on ORD_DateEntered and ORD_OrderDate

        /// <summary>
        /// Gets or sets the awaiting SNR authorization days limit.
        /// </summary>
        /// <value>
        /// The awaiting SNR authorization days limit.
        /// this is for the color logic
        /// </value>
        public int AwaitingSnrAuthorizationDaysLimit { get; set; }

        /// <summary>
        /// Gets or sets the requisition limit.
        /// </summary>
        /// <value>
        /// The requisition limit.
        /// to show requisition only X Days old
        /// </value>
        public int RequisitionLimit { get; set; }

    }
}
