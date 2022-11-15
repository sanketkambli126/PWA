namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
    /// <summary>
    /// InventoryManager
    /// </summary>
    public class InventoryManager
    {
        /// <summary>
        /// Gets or sets the order in process count.
        /// </summary>
        /// <value>
        /// The order in process count.
        /// </value>
        public int OrderInProcessCount { get; set; }

        /// <summary>
        /// Gets or sets the ordered count.
        /// </summary>
        /// <value>
        /// The ordered count.
        /// </value>
        public int OrderedCount { get; set; }

        /// <summary>
        /// Gets or sets the order delivery on the way count.
        /// </summary>
        /// <value>
        /// The order delivery on the way count.
        /// </value>
        public int OrderDeliveryOnTheWayCount { get; set; }

        /// <summary>
        /// Gets or sets the requested information count.
        /// </summary>
        /// <value>
        /// The requested information count.
        /// </value>
        public int RequestedInformationCount { get; set; }

        /// <summary>
        /// Gets or sets the authorisation count.
        /// </summary>
        /// <value>
        /// The authorisation count.
        /// </value>
        public int AuthorisationCount { get; set; }

        /// <summary>
        /// Gets or sets the recieved in30 days count.
        /// </summary>
        /// <value>
        /// The recieved in30 days count.
        /// </value>
        public int RecievedIn30DaysCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is order in process urgent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is order in process urgent; otherwise, <c>false</c>.
        /// </value>
		public bool IsOrderInProcessUrgent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ordered urgent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ordered urgent; otherwise, <c>false</c>.
        /// </value>
        public bool IsOrderedUrgent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is order delivery on the way urgent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is order delivery on the way urgent; otherwise, <c>false</c>.
        /// </value>
        public bool IsOrderDeliveryOnTheWayUrgent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is authorisation urgent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is authorisation urgent; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthorisationUrgent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is recieved in30 days urgent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is recieved in30 days urgent; otherwise, <c>false</c>.
        /// </value>
        public bool IsRecievedIn30DaysUrgent { get; set; }

        /// <summary>
        /// Gets or sets the authentication required count.
        /// </summary>
        /// <value>
        /// The authentication required count.
        /// </value>
        public int AuthRequiredCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [authentication required urgent].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [authentication required urgent]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAuthRequiredUrgent { get; set; }

        /// <summary>
        /// Gets or sets the awaiting authorisation count.
        /// </summary>
        /// <value>
        /// The awaiting authorisation count.
        /// </value>
        public int AwaitingAuthorisationCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [awaiting authorisation urgent].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [awaiting authorisation urgent]; otherwise, <c>false</c>.
        /// </value>
        public bool IsAwaitingAuthorisationUrgent { get; set; }

        /// <summary>
		/// Gets or sets the in process requisition priority.
		/// </summary>
		/// <value>
		/// The in process requisition priority.
		/// </value>
		public int RequisitionPriority { get; set; }

        /// <summary>
        /// Gets or sets the ordered priority.
        /// </summary>
        /// <value>
        /// The ordered priority.
        /// </value>
        public int OrderedPriority { get; set; }

        /// <summary>
        /// Gets or sets the tender awaiting authentication priority.
        /// </summary>
        /// <value>
        /// The tender awaiting authentication priority.
        /// </value>
        public int TenderAwaitingAuthPriority { get; set; }

        /// <summary>
        /// Gets or sets the awaiting SNR authentication priority.
        /// </summary>
        /// <value>
        /// The awaiting SNR authentication priority.
        /// </value>
        public int AwaitingSnrAuthPriority { get; set; }

        /// Gets or sets the requisition older than x days count.
		/// </summary>
		/// <value>
		/// The requisition older than x days count.
		/// </value>
		public int RequisitionOlderThanXDaysCount { get; set; }

        /// <summary>
        /// Gets or sets the requisition count.
        /// </summary>
        /// <value>
        /// The requisition count.
        /// </value>
        public int RequisitionCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [requisition urgent].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [requisition urgent]; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequisitionUrgent { get; set; }

        /// <summary>
        /// Gets or sets the enquiries count.
        /// </summary>
        /// <value>
        /// The enquiries count.
        /// </value>
        public int EnquiriesCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enquiries urgent].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enquiries urgent]; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnquiriesUrgent { get; set; }

        /// <summary>
        /// Gets or sets the on hold count.
        /// </summary>
        /// <value>
        /// The on hold count.
        /// </value>
        public int OnHoldCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [on hold urgent].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [on hold urgent]; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnHoldUrgent { get; set; }
    }
}
