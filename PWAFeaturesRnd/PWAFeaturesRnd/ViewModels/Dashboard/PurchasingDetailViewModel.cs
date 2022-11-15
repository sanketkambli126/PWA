using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;

namespace PWAFeaturesRnd.ViewModels.Dashboard
{
    /// <summary>
    /// Purchasing Detail ViewModel
    /// </summary>
    public class PurchasingDetailViewModel
    {
        /// <summary>
        /// Gets or sets the order number.
        /// </summary>
        /// <value>
        /// The order number.
        /// </value>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is high priority.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is high priority; otherwise, <c>false</c>.
        /// </value>
        public bool IsHighPriority { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the status description.
        /// </summary>
        /// <value>
        /// The status description.
        /// </value>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the date entered.
        /// </summary>
        /// <value>
        /// The date entered.
        /// </value>
        public DateTime? DateEntered { get; set; }

        /// <summary>
        /// Gets or sets the date ordered.
        /// </summary>
        /// <value>
        /// The date ordered.
        /// </value>
        public DateTime? DateOrdered { get; set; }

        /// <summary>
        /// Gets or sets the supplier.
        /// </summary>
        /// <value>
        /// The supplier.
        /// </value>
        public string Supplier { get; set; }

        /// <summary>
        /// Gets or sets the warehouse.
        /// </summary>
        /// <value>
        /// The warehouse.
        /// </value>
        public string Warehouse { get; set; }

        /// <summary>
        /// Gets or sets the agent.
        /// </summary>
        /// <value>
        /// The agent.
        /// </value>
        public string Agent { get; set; }

        /// <summary>
        /// Gets or sets the expected delivery.
        /// </summary>
        /// <value>
        /// The expected delivery.
        /// </value>
        public DateTime? ExpectedDelivery { get; set; }

        /// <summary>
        /// Gets or sets the expected port.
        /// </summary>
        /// <value>
        /// The expected port.
        /// </value>
        public string ExpectedPort { get; set; }

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
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public decimal Cost { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the inventory manager.
        /// </summary>
        /// <value>
        /// The inventory manager.
        /// </value>
        public InventoryManager InventoryManager { get; set; }

        /// <summary>
        /// Gets or sets the protected order number.
        /// </summary>
        /// <value>
        /// The protected order number.
        /// </value>
        public string ProtectedOrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the protected accounting company identifier.
        /// </summary>
        /// <value>
        /// The protected accounting company identifier.
        /// </value>
        public string ProtectedAccountingCompanyId { get; set; }

        /// <summary>
        /// Gets or sets the purchase order URL.
        /// </summary>
        /// <value>
        /// The purchase order URL.
        /// </value>
        public string PurchaseOrderUrl { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the requisition date.
        /// </summary>
        /// <value>
        /// The requisition date.
        /// </value>
        public string RequisitionDate { get; set; }

        /// <summary>
        /// Gets or sets the expcted record date.
        /// </summary>
        /// <value>
        /// The expcted record date.
        /// </value>
        public DateTime? ExpctedRecDate { get; set; }

        /// <summary>
        /// Gets or sets the expected record port.
        /// </summary>
        /// <value>
        /// The expected record port.
        /// </value>
        public string ExpectedRecPort { get; set; }

        /// <summary>
        /// Gets or sets the amount os.
        /// </summary>
        /// <value>
        /// The amount os.
        /// </value>
        public decimal AmountOS { get; set; }

        /// <summary>
        /// Gets or sets the haz materials.
        /// </summary>
        /// <value>
        /// The haz materials.
        /// </value>
        public string IsHazMaterial { get; set; }

        /// <summary>
        /// Gets or sets the is damaged item.
        /// </summary>
        /// <value>
        /// The is damaged item.
        /// </value>
        public string IsDamagedItem { get; set; }

        /// <summary>
        /// Gets or sets the is poor quality.
        /// </summary>
        /// <value>
        /// The is poor quality.
        /// </value>
        public string IsPoorQuality { get; set; }

        /// <summary>
        /// Gets or sets the is poor packaging.
        /// </summary>
        /// <value>
        /// The is poor packaging.
        /// </value>
        public string IsPoorPackaging { get; set; }

        /// <summary>
        /// Gets or sets the is incorrect item.
        /// </summary>
        /// <value>
        /// The is incorrect item.
        /// </value>
        public string IsIncorrectItem { get; set; }

        /// <summary>
        /// Gets or sets the is certificate received.
        /// </summary>
        /// <value>
        /// The is certificate received.
        /// </value>
        public string IsCertificateReceived { get; set; }

        /// <summary>
        /// Gets or sets the encrypted supplier identifier.
        /// </summary>
        /// <value>
        /// The encrypted supplier identifier.
        /// </value>
        public string EncryptedSupplierId { get; set; }

        /// <summary>
        /// Gets or sets the encrypted warehouse identifier.
        /// </summary>
        /// <value>
        /// The encrypted warehouse identifier.
        /// </value>
        public string EncryptedWarehouseId { get; set; }

        /// <summary>
        /// Gets or sets the encrypted agent identifier.
        /// </summary>
        /// <value>
        /// The encrypted agent identifier.
        /// </value>
        public string EncryptedAgentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is supplier additional details visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is supplier additional details visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsSupplierAdditionalDetailsVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is warehouse additional details visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is warehouse additional details visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsWarehouseAdditionalDetailsVisible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is agent additional details visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is agent additional details visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsAgentAdditionalDetailsVisible { get; set; }

        /// <summary>
        /// Gets or sets the currency label.
        /// </summary>
        /// <value>
        /// The currency label.
        /// </value>
        public string CurrencyLabel { get; set; }

        /// <summary>
        /// Gets or sets the os cost.
        /// </summary>
        /// <value>
        /// The os cost.
        /// </value>
        public decimal OsCost { get; set; }

        /// <summary>
		/// Gets or sets a value indicating whether this instance is further order authorisation required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is further order authorisation required; otherwise, <c>false</c>.
		/// </value>
		public bool IsFurtherOrderAuthorisationRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is any hazardous material in order lines.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is any hazardous material in order lines; otherwise, <c>false</c>.
        /// </value>
        public bool IsAnyHazardousMaterialInOrderLines { get; set; }

        /// <summary>
        /// Gets or sets the channel count.
        /// </summary>
        /// <value>
        /// The channel count.
        /// </value>
        public int ChannelCount { get; set; }

        /// <summary>
        /// Gets or sets the notes count.
        /// </summary>
        /// <value>
        /// The notes count.
        /// </value>
        public int NotesCount { get; set; }

        /// <summary>
		/// Gets or sets the message details json.
		/// </summary>
		/// <value>
		/// The message details json.
		/// </value>
		public string MessageDetailsJSON { get; set; }

        /// <summary>
		/// Gets or sets the order identifier.
		/// </summary>
		/// <value>
		/// The order identifier.
		/// </value>
		public string OrderId { get; set; }
    }
}
