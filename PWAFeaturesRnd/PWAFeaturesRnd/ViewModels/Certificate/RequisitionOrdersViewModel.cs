using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Certificate
{
	/// <summary>
	/// RequisitionOrdersViewModel for requisition link
	/// </summary>
	public class RequisitionOrdersViewModel
    {
		/// <summary>
		/// Gets or sets the certificate status.
		/// </summary>
		/// <value>
		/// The certificate status.
		/// </value>
		public string CertificateStatus { get; set; }

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
		/// Gets or sets a value indicating whether this instance has defects mapped.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has defects mapped; otherwise, <c>false</c>.
		/// </value>
		public bool HasDefectsMapped { get; set; }

		/// <summary>
		/// Gets or sets a value Purchase Order Url
		/// </summary>
		public string PurchaseOrderUrl { get; set; }

		/// <summary>
		/// Order Status Description Setter Getter
		/// </summary>
		public string StatusDescription { get; set; }

		/// <summary>
		/// Order Status Color Setter Getter
		/// </summary>
		public string OrderStatusColor { get; set; }

		/// <summary>
		/// Is StatusVisible Setter Getter
		/// </summary>
		public bool IsStatusVisible { get; set; }
	}
}
