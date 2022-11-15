using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
	/// <summary>
	/// Purchase Order Authentication Required Export View Model
	/// </summary>
	public class PurchaseOrderAuthenticationRequiredExportViewModel
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
		/// Gets or sets the supplier.
		/// </summary>
		/// <value>
		/// The supplier.
		/// </value>
		[DisplayName("Supplier Name")]
		public string Supplier { get; set; }

		/// <summary>
		/// Gets or sets the cost.
		/// </summary>
		/// <value>
		/// The cost.
		/// </value>
		[DisplayName("Cost")]
		public decimal Cost { get; set; }

		/// <summary>
		/// Gets or sets the currency.
		/// </summary>
		/// <value>
		/// The currency.
		/// </value>
		[DisplayName("Currency")] 
		public string Currency { get; set; }

		/// <summary>
		/// Gets or sets the expected delivery.
		/// </summary>
		/// <value>
		/// The expected delivery.
		/// </value>
		[DisplayName("Expected Delivery Date")]
		public string ExpectedDelivery { get; set; }
	}
}
