using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
	/// <summary>
	/// Purchase Order Received 30 Days Export View Model
	/// </summary>
	public class PurchaseOrderReceived30DaysExportViewModel
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
		/// Gets or sets the supplier.
		/// </summary>
		/// <value>
		/// The supplier.
		/// </value>
		[DisplayName("Supplier Name")]
		public string Supplier { get; set; }

		/// <summary>
		/// Gets or sets the agent.
		/// </summary>
		/// <value>
		/// The agent.
		/// </value>
		[DisplayName("Agent")]
		public string Agent { get; set; }

		/// <summary>
		/// Gets or sets the received date.
		/// </summary>
		/// <value>
		/// The received date.
		/// </value>
		[DisplayName("Received On")]
		public string ReceivedDate { get; set; }

		/// <summary>
		/// Gets or sets the received port.
		/// </summary>
		/// <value>
		/// The received port.
		/// </value>
		[DisplayName("Received Port")]
		public string ReceivedPort { get; set; }
	}
}
