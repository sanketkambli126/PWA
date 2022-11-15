using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
	/// <summary>
	/// Purchase Order Ordered Export View Model
	/// </summary>
	public class PurchaseOrderOrderedExportViewModel
	{
		/// <summary>
		/// Gets or sets the order number.
		/// </summary>
		/// <value>
		/// The order number.
		/// </value>
		[DisplayName("Order No.")]
		public string OrderNumber { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[DisplayName("Order Name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		[DisplayName("Status")]
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the date ordered.
		/// </summary>
		/// <value>
		/// The date ordered.
		/// </value>
		[DisplayName("Date Ordered")]
		public string DateOrdered { get; set; }

		/// <summary>
		/// Gets or sets the expected delivery.
		/// </summary>
		/// <value>
		/// The expected delivery.
		/// </value>
		[DisplayName("Expected Delivery Date")]
		public string ExpectedDelivery { get; set; }

		/// <summary>
		/// Gets or sets the supplier.
		/// </summary>
		/// <value>
		/// The supplier.
		/// </value>
		[DisplayName("Supplier Name")]
		public string Supplier { get; set; }

		/// <summary>
		/// Gets or sets the warehouse.
		/// </summary>
		/// <value>
		/// The warehouse.
		/// </value>
		[DisplayName("Warehouse")]
		public string Warehouse { get; set; }
	}
}
