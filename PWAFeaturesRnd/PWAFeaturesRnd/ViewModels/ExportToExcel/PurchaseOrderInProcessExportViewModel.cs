using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
	/// <summary>
	/// Purchase Order In Process Export View Model
	/// </summary>
	public class PurchaseOrderInProcessExportViewModel
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
		/// Gets or sets the date entered.
		/// </summary>
		/// <value>
		/// The date entered.
		/// </value>
		[DisplayName("Date Entered")]
		public string DateEntered { get; set; }
	}
}
