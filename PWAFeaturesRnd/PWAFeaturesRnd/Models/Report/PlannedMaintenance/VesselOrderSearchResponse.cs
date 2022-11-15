using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
    public class VesselOrderSearchResponse
    {

		/// <summary>
		/// Gets or sets the is component active.
		/// </summary>
		/// <value>
		/// The is component active.
		/// </value>
		public bool? IsComponentActive { get; set; }

		/// <summary>
		/// Gets or sets the certificate status.
		/// </summary>
		/// <value>
		/// The certificate status.
		/// </value>
		public string CertificateStatus { get; set; }

		/// <summary>
		/// Gets or sets the parent component identifier.
		/// </summary>
		/// <value>
		/// The parent component identifier.
		/// </value>
		public string ParentComponentId { get; set; }

		/// <summary>
		/// Gets or sets the freight order number.
		/// </summary>
		/// <value>
		/// The freight order number.
		/// </value>
		public string FreightOrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the requested date.
		/// </summary>
		/// <value>
		/// The requested date.
		/// </value>
		public DateTime? RequestedDate { get; set; }

		/// <summary>
		/// Gets or sets the received at port.
		/// </summary>
		/// <value>
		/// The received at port.
		/// </value>
		public string ReceivedAtPort { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the system area identifier.
		/// </summary>
		/// <value>
		/// The system area identifier.
		/// </value>
		public string SystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the name of the department.
		/// </summary>
		/// <value>
		/// The name of the department.
		/// </value>
		public string DepartmentName { get; set; }

		/// <summary>
		/// Gets or sets the department identifier.
		/// </summary>
		/// <value>
		/// The department identifier.
		/// </value>
		public string DepartmentId { get; set; }

		/// <summary>
		/// Gets or sets the account code description.
		/// </summary>
		/// <value>
		/// The account code description.
		/// </value>
		public string AccountCodeDescription { get; set; }

		/// <summary>
		/// Gets or sets the account identifier.
		/// </summary>
		/// <value>
		/// The account identifier.
		/// </value>
		public string AccountId { get; set; }

		/// <summary>
		/// Gets or sets the purchase order stage.
		/// </summary>
		/// <value>
		/// The purchase order stage.
		/// </value>
		public PurchaseOrderStage? PurchaseOrderStage { get; set; }

		/// <summary>
		/// Gets or sets the order date.
		/// </summary>
		/// <value>
		/// The order date.
		/// </value>
		public DateTime? OrderDate { get; set; }

		/// <summary>
		/// Gets or sets the order received date.
		/// </summary>
		/// <value>
		/// The order received date.
		/// </value>
		public DateTime? OrderReceivedDate { get; set; }

		/// <summary>
		/// Gets or sets the expect delivery date.
		/// </summary>
		/// <value>
		/// The expect delivery date.
		/// </value>
		public DateTime? ExpectDeliveryDate { get; set; }

		/// <summary>
		/// Gets or sets the expected delivery port.
		/// </summary>
		/// <value>
		/// The expected delivery port.
		/// </value>
		public string ExpectedDeliveryPort { get; set; }

		/// <summary>
		/// Gets or sets the requisition date.
		/// </summary>
		/// <value>
		/// The requisition date.
		/// </value>
		public DateTime? RequisitionDate { get; set; }

		/// <summary>
		/// Gets or sets the name of the order.
		/// </summary>
		/// <value>
		/// The name of the order.
		/// </value>
		public string OrderName { get; set; }

		/// <summary>
		/// Gets or sets the purchase order status.
		/// </summary>
		/// <value>
		/// The purchase order status.
		/// </value>
		public string PurchaseOrderStatus { get; set; }

		/// <summary>
		/// Gets or sets the purchase order priority.
		/// </summary>
		/// <value>
		/// The purchase order priority.
		/// </value>
		public string PurchaseOrderPriority { get; set; }

		/// <summary>
		/// Gets or sets the type of the purchase order.
		/// </summary>
		/// <value>
		/// The type of the purchase order.
		/// </value>
		public PurchaseOrderType? PurchaseOrderType { get; set; }

		/// <summary>
		/// Gets or sets the order number.
		/// </summary>
		/// <value>
		/// The order number.
		/// </value>
		public string OrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

		/// <summary>
		/// Gets or sets the ord identifier.
		/// </summary>
		/// <value>
		/// The ord identifier.
		/// </value>
		public string OrdId { get; set; }

		/// <summary>
		/// Gets or sets the is component deleted.
		/// </summary>
		/// <value>
		/// The is component deleted.
		/// </value>
		public bool? IsComponentDeleted { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has defects mapped.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has defects mapped; otherwise, <c>false</c>.
		/// </value>
		public bool HasDefectsMapped { get; set; }
	}
}
