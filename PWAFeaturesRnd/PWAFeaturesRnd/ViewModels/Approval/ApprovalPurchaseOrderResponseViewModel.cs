using System;

namespace PWAFeaturesRnd.ViewModels.Approval
{
	/// <summary>
	/// Approval Purchase Order Response ViewModel
	/// </summary>
	public class ApprovalPurchaseOrderResponseViewModel
	{
		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

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
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public string PriorityDescription { get; set; }

		/// <summary>
		/// Gets or sets the date originated.
		/// </summary>
		/// <value>
		/// The date originated.
		/// </value>
		public DateTime? DateOriginated { get; set; }

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
		public string EncryptedVesselId { get; set; }

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
		public bool HasClientAuthorised { get; set; }
	}
}
