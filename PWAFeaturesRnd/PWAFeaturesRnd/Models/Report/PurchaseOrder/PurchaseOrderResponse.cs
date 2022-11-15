using System;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Purchase Order Response
	/// </summary>
	public class PurchaseOrderResponse
	{
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
		/// Gets or sets the order identifier.
		/// </summary>
		/// <value>
		/// The order identifier.
		/// </value>
		public string OrderId { get; set; }

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
		/// Gets or sets the name of the order.
		/// </summary>
		/// <value>
		/// The name of the order.
		/// </value>
		public string OrderName { get; set; }

		/// <summary>
		/// Gets or sets the status identifier.
		/// </summary>
		/// <value>
		/// The status identifier.
		/// </value>
		public string StatusId { get; set; }

		/// <summary>
		/// Gets or sets the status description.
		/// </summary>
		/// <value>
		/// The status description.
		/// </value>
		public string StatusDescription { get; set; }

		/// <summary>
		/// Gets or sets the order priority.
		/// </summary>
		/// <value>
		/// The order priority.
		/// </value>
		public string OrderPriority { get; set; }

		/// <summary>
		/// Gets or sets the order stage.
		/// </summary>
		/// <value>
		/// The order stage.
		/// </value>
		public string OrderStage { get; set; }

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
		/// Gets or sets the expected received date.
		/// </summary>
		/// <value>
		/// The expected received date.
		/// </value>
		public DateTime? ExpectedReceivedDate { get; set; }

		/// <summary>
		/// Gets or sets the expected received port.
		/// </summary>
		/// <value>
		/// The expected received port.
		/// </value>
		public string ExpectedReceivedPort { get; set; }

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
		/// Gets or sets the agent identifier.
		/// </summary>
		/// <value>
		/// The agent identifier.
		/// </value>
		public string AgentId { get; set; }

		/// <summary>
		/// Gets or sets the name of the agent.
		/// </summary>
		/// <value>
		/// The name of the agent.
		/// </value>
		public string AgentName { get; set; }

		/// <summary>
		/// Gets or sets the warehouse identifier.
		/// </summary>
		/// <value>
		/// The warehouse identifier.
		/// </value>
		public string WarehouseId { get; set; }

		/// <summary>
		/// Gets or sets the name of the warehouse.
		/// </summary>
		/// <value>
		/// The name of the warehouse.
		/// </value>
		public string WarehouseName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is hazardous material.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is hazardous material; otherwise, <c>false</c>.
		/// </value>
		public bool IsHazardousMaterial { get; set; }

		/// <summary>
		/// Gets or sets the total amount.
		/// </summary>
		/// <value>
		/// The total amount.
		/// </value>
		public decimal TotalAmount { get; set; }

		/// <summary>
		/// Gets or sets the outstanding amount.
		/// </summary>
		/// <value>
		/// The outstanding amount.
		/// </value>
		public decimal OutstandingAmount { get; set; }

		/// <summary>
		/// Gets or sets the currency.
		/// </summary>
		/// <value>
		/// The currency.
		/// </value>
		public string Currency { get; set; }

		/// <summary>
		/// Gets or sets the coy currency.
		/// </summary>
		/// <value>
		/// The coy currency.
		/// </value>
		public string CoyCurrency { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is damaged items.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is damaged items; otherwise, <c>false</c>.
		/// </value>
		public bool IsDamagedItems { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is poor quality.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is poor quality; otherwise, <c>false</c>.
		/// </value>
		public bool IsPoorQuality { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is poor packaging.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is poor packaging; otherwise, <c>false</c>.
		/// </value>
		public bool IsPoorPackaging { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is incorrect item.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is incorrect item; otherwise, <c>false</c>.
		/// </value>
		public bool IsIncorrectItem { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is certificate required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is certificate required; otherwise, <c>false</c>.
		/// </value>
		public bool IsCertificateRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is certificate received.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is certificate received; otherwise, <c>false</c>.
		/// </value>
		public bool IsCertificateReceived { get; set; }

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
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the authentication level.
		/// </summary>
		/// <value>
		/// The authentication level.
		/// </value>
		public int? AuthLevel { get; set; }

		/// <summary>
		/// Gets or sets the authentication vessel limit.
		/// </summary>
		/// <value>
		/// The authentication vessel limit.
		/// </value>
		public decimal? AuthVesselLimit { get; set; }

		/// <summary>
		/// Gets or sets the authentication office limit.
		/// </summary>
		/// <value>
		/// The authentication office limit.
		/// </value>
		public decimal? AuthOfficeLimit { get; set; }

		/// <summary>
		/// Gets or sets the authentication level1 limit.
		/// </summary>
		/// <value>
		/// The authentication level1 limit.
		/// </value>
		public decimal? AuthLevel1Limit { get; set; }

		/// <summary>
		/// Gets or sets the authentication client limit.
		/// </summary>
		/// <value>
		/// The authentication client limit.
		/// </value>
		public decimal? AuthClientLimit { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has client authorised.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has client authorised; otherwise, <c>false</c>.
		/// </value>
		public byte? HasClientAuthorised { get; set; }
	}
}
