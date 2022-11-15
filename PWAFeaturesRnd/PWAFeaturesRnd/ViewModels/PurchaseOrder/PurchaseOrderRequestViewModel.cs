using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PWAFeaturesRnd.Common.Converter;

namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// 
	/// </summary>
	public class PurchaseOrderRequestViewModel
    {
		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the account company identifier.
		/// </summary>
		/// <value>
		/// The account company identifier.
		/// </value>
		public string AccountCompanyId { get; set; }

		/// <summary>
		/// Gets or sets the order number.
		/// </summary>
		/// <value>
		/// The order number.
		/// </value>
		public string OrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the po stage.
		/// </summary>
		/// <value>
		/// The po stage.
		/// </value>
		public string POStage { get; set; }

		/// <summary>
		/// Gets or sets the search order number.
		/// </summary>
		/// <value>
		/// The search order number.
		/// </value>
		public string SearchOrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the purchase order types.
		/// </summary>
		/// <value>
		/// The purchase order types.
		/// </value>
		public string PurchaseOrderTypes { get; set; }

		/// <summary>
		/// Gets or sets the selected order stage.
		/// </summary>
		/// <value>
		/// The selected order stage.
		/// </value>
		public string SelectedOrderStage { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is purchase order stage selected.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is purchase order stage selected; otherwise, <c>false</c>.
		/// </value>
		public bool IsSearchClicked { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; } //check ORD Title using Start With

		/// <summary>
		/// Gets or sets the supplier identifier.
		/// </summary>
		/// <value>
		/// The supplier identifier.
		/// </value>
		public string SupplierId { get; set; }

		/// <summary>
		/// Gets or sets the name of the supplier.
		/// </summary>
		/// <value>
		/// The name of the supplier.
		/// </value>
		public string SupplierName { get; set; }

		/// <summary>
		/// Gets or sets the search filter.
		/// </summary>
		/// <value>
		/// The search filter.
		/// </value>
		public string SearchFilter { get; set; }

		/// <summary>
		/// Gets or sets the selected status.
		/// </summary>
		/// <value>
		/// The selected status.
		/// </value>
		public string SelectedStatus { get; set; }

		/// <summary>
		/// Gets or sets the active mobile tab class.
		/// </summary>
		/// <value>
		/// The active mobile tab class.
		/// </value>
		public string ActiveMobileTabClass { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show only authentication required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show only authentication required]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowOnlyAuthRequired { get; set; }

		public string strOrderStatusIds { get; set; }

		public List<string> OrderStatusIds { get; set; }

	}
}
