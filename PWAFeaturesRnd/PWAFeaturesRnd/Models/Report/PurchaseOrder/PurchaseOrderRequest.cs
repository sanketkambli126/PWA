using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Purchase Order Request
	/// </summary>
	public class PurchaseOrderRequest
	{
		/// <summary>
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the order number.
		/// </summary>
		/// <value>
		/// The order number.
		/// </value>
		public string OrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the order title.
		/// </summary>
		/// <value>
		/// The order title.
		/// </value>
		public string OrderTitle { get; set; }

		/// <summary>
		/// Gets or sets the supplier identifier.
		/// </summary>
		/// <value>
		/// The supplier identifier.
		/// </value>
		public string SupplierId { get; set; }

		/// <summary>
		/// Gets or sets the order stages.
		/// </summary>
		/// <value>
		/// The order stages.
		/// </value>
		public List<string> OrderStages { get; set; }

		/// <summary>
		/// Gets or sets the order statuses.
		/// </summary>
		/// <value>
		/// The order statuses.
		/// </value>
		public List<string> OrderStatuses { get; set; }

		/// <summary>
		/// Converts to reqorddate.
		/// </summary>
		/// <value>
		/// To req ord date.
		/// </value>
		public DateTime? ToReqOrdDate { get; set; }

		/// <summary>
		/// Gets or sets from req ord date.
		/// </summary>
		/// <value>
		/// From req ord date.
		/// </value>
		public DateTime? FromReqOrdDate { get; set; }

		/// <summary>
		/// Gets or sets the received date.
		/// </summary>
		/// <value>
		/// The received date.
		/// </value>
		public DateTime? ReceivedDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show only authentication required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show only authentication required]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowOnlyAuthRequired { get; set; }
	}
}
