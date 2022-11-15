using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Supplier Details For Quote Authorization
	/// </summary>
	public class SupplierDetailsForQuoteAuthorization
	{
		/// <summary>
		/// Gets or sets the feedback supplier order identifier.
		/// </summary>
		/// <value>
		/// The feedback supplier order identifier.
		/// </value>
		public string FeedbackSupplierOrderId { get; set; }

		/// <summary>
		/// Gets or sets the supplier company identifier.
		/// </summary>
		/// <value>
		/// The supplier company identifier.
		/// </value>
		public string SupplierCompanyId { get; set; }

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		/// <value>
		/// The port.
		/// </value>
		public string Port { get; set; }

		/// <summary>
		/// Gets or sets the total items count.
		/// </summary>
		/// <value>
		/// The total items count.
		/// </value>
		public int TotalItemsCount { get; set; }

		/// <summary>
		/// Gets or sets the items not quoted count.
		/// </summary>
		/// <value>
		/// The items not quoted count.
		/// </value>
		public int ItemsNotQuotedCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is feedback required.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is feedback required; otherwise, <c>false</c>.
		/// </value>
		public bool IsFeedbackRequired { get; set; }

		/// <summary>
		/// Gets or sets the supplier order line notes.
		/// </summary>
		/// <value>
		/// The supplier order line notes.
		/// </value>
		public List<string> SupplierOrderLineNotes { get; set; }

		/// <summary>
		/// Gets or sets the expected work country.
		/// </summary>
		/// <value>
		/// The expected work country.
		/// </value>
		public string ExpectedWorkCountry { get; set; }

		/// <summary>
		/// Gets or sets the name of the spare part type.
		/// </summary>
		/// <value>
		/// The name of the spare part type.
		/// </value>
		public string SparePartTypeName { get; set; }

		/// <summary>
		/// Gets or sets the spare part type code.
		/// </summary>
		/// <value>
		/// The spare part type code.
		/// </value>
		public string SparePartTypeCode { get; set; }

		/// <summary>
		/// Gets or sets the is hazardous goods.
		/// </summary>
		/// <value>
		/// The is hazardous goods.
		/// </value>
		public bool? IsHazardousGoods { get; set; }

		/// <summary>
		/// Gets or sets the supplier notes.
		/// </summary>
		/// <value>
		/// The supplier notes.
		/// </value>
		public string SupplierNotes { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is full response.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is full response; otherwise, <c>false</c>.
		/// </value>
		public bool IsFullResponse { get; set; }

		/// <summary>
		/// Gets or sets the expected work days.
		/// </summary>
		/// <value>
		/// The expected work days.
		/// </value>
		public int ExpectedWorkDays { get; set; }

		/// <summary>
		/// Gets or sets the po vessel currency.
		/// </summary>
		/// <value>
		/// The po vessel currency.
		/// </value>
		public string PoVesselCurrency { get; set; }

		/// <summary>
		/// Gets or sets the freight accrual in po vessel currency.
		/// </summary>
		/// <value>
		/// The freight accrual in po vessel currency.
		/// </value>
		public decimal FreightAccrualInPoVesselCurrency { get; set; }

		/// <summary>
		/// Gets or sets the quote amount in po vessel currency.
		/// </summary>
		/// <value>
		/// The quote amount in po vessel currency.
		/// </value>
		public decimal QuoteAmountInPoVesselCurrency { get; set; }

		/// <summary>
		/// Gets or sets the supplier order currrency identifier.
		/// </summary>
		/// <value>
		/// The supplier order currrency identifier.
		/// </value>
		public string SupplierOrderCurrrencyId { get; set; }

		/// <summary>
		/// Gets or sets the quote amount.
		/// </summary>
		/// <value>
		/// The quote amount.
		/// </value>
		public decimal QuoteAmount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is preferred.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is preferred; otherwise, <c>false</c>.
		/// </value>
		public bool IsPreferred { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is marcas.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is marcas; otherwise, <c>false</c>.
		/// </value>
		public bool IsMARCAS { get; set; }

		/// <summary>
		/// Gets or sets the name of the supplier.
		/// </summary>
		/// <value>
		/// The name of the supplier.
		/// </value>
		public string SupplierName { get; set; }

		/// <summary>
		/// Gets or sets the supplier order identifier.
		/// </summary>
		/// <value>
		/// The supplier order identifier.
		/// </value>
		public string SupplierOrderId { get; set; }

		/// <summary>
		/// Gets or sets the supplier order status.
		/// </summary>
		/// <value>
		/// The supplier order status.
		/// </value>
		public PurchaseOrderStatus? SupplierOrderStatus { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is proforma requested.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is proforma requested; otherwise, <c>false</c>.
		/// </value>
		public bool IsProformaRequested { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [ord owner authorise].
		/// </summary>
		/// <value>
		///   <c>true</c> if [ord owner authorise]; otherwise, <c>false</c>.
		/// </value>
		public bool OrdOwnerAuthorise { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [authentication level].
		/// </summary>
		/// <value>
		///   <c>true</c> if [authentication level]; otherwise, <c>false</c>.
		/// </value>
		public int AuthLevel { get; set; }
	}
}
