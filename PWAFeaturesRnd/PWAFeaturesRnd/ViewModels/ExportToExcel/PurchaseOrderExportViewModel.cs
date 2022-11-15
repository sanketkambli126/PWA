using System.ComponentModel;

namespace PWAFeaturesRnd.ViewModels.ExportToExcel
{
	/// <summary>
	/// Purchase Order Export View Model
	/// </summary>
	public class PurchaseOrderExportViewModel
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
		[DisplayName("Request Date")]
		public string DateEntered { get; set; }

		/// <summary>
		/// Gets or sets the date ordered.
		/// </summary>
		/// <value>
		/// The date ordered.
		/// </value>
		[DisplayName("Ordered Date")]
		public string DateOrdered { get; set; }

		/// <summary>
		/// Gets or sets the expected received port.
		/// </summary>
		/// <value>
		/// The expected received port.
		/// </value>
		[DisplayName("Exp'd/ Rec'd Port")]
		public string ExpectedReceivedPort { get; set; }

		/// <summary>
		/// Gets or sets the expected delivery.
		/// </summary>
		/// <value>
		/// The expected delivery.
		/// </value>
		[DisplayName("Exp'd/ Rec'd Date")]
		public string ExpectedReceivedDate { get; set; }

		/// <summary>
		/// Gets or sets the supplier.
		/// </summary>
		/// <value>
		/// The supplier.
		/// </value>
		[DisplayName("Supplier")]
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
		/// Gets or sets the warehouse.
		/// </summary>
		/// <value>
		/// The warehouse.
		/// </value>
		[DisplayName("Warehouse")]
		public string Warehouse { get; set; }

		/// <summary>
		/// Gets or sets the is haz material.
		/// </summary>
		/// <value>
		/// The is haz material.
		/// </value>
		[DisplayName("Haz Materials")]
		public string IsHazMaterial { get; set; }

		/// <summary>
		/// Gets or sets the total amount.
		/// </summary>
		/// <value>
		/// The total amount.
		/// </value>
		[DisplayName("Amount Total")]
		public decimal TotalAmount { get; set; }

		/// <summary>
		/// Gets or sets the amount os.
		/// </summary>
		/// <value>
		/// The amount os.
		/// </value>
		[DisplayName("Amount O/S")]
		public decimal AmountOS { get; set; }

		/// <summary>
		/// Gets or sets the damaged items.
		/// </summary>
		/// <value>
		/// The damaged items.
		/// </value>
		[DisplayName("Damaged Items")]
		public string DamagedItems { get; set; }

		/// <summary>
		/// Gets or sets the poor quality.
		/// </summary>
		/// <value>
		/// The poor quality.
		/// </value>
		[DisplayName("Poor Quality")]
		public string PoorQuality { get; set; }

		/// <summary>
		/// Gets or sets the poor packaging.
		/// </summary>
		/// <value>
		/// The poor packaging.
		/// </value>
		[DisplayName("Poor Packaging")]
		public string PoorPackaging { get; set; }

		/// <summary>
		/// Gets or sets the incorrect item.
		/// </summary>
		/// <value>
		/// The incorrect item.
		/// </value>
		[DisplayName("Incorrect Items")]
		public string IncorrectItem { get; set; }

		/// <summary>
		/// Gets or sets the certificate recieved.
		/// </summary>
		/// <value>
		/// The certificate recieved.
		/// </value>
		[DisplayName("Certificate Received")]
		public string CertificateRecieved { get; set; }
	}
}
