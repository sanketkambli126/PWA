namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// Summary purchase order details view model
	/// </summary>
	public class SummaryPurchaseOrderDetailsViewModel
    {
		/// <summary>
		/// Gets or sets the items count.
		/// </summary>
		/// <value>
		/// The items count.
		/// </value>
		public int ItemsCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has notes.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has notes; otherwise, <c>false</c>.
		/// </value>
		public bool HasNotes { get; set; }

		/// <summary>
		/// Gets or sets the memo count.
		/// </summary>
		/// <value>
		/// The memo count.
		/// </value>
		public int MemoCount { get; set; }

		/// <summary>
		/// Gets or sets the attachment count.
		/// </summary>
		/// <value>
		/// The attachment count.
		/// </value>
		public int AttachmentCount { get; set; }

		/// <summary>
		/// Gets or sets the has flight details.
		/// </summary>
		/// <value>
		/// The has flight details.
		/// </value>
		public bool? HasFlightDetails { get; set; }

		/// <summary>
		/// Gets or sets the name of the port.
		/// </summary>
		/// <value>
		/// The name of the port.
		/// </value>
		public string PortName { get; set; }

		/// <summary>
		/// Gets or sets the port country.
		/// </summary>
		/// <value>
		/// The port country.
		/// </value>
		public string PortCountry { get; set; }

		/// <summary>
		/// Gets or sets the expected date.
		/// </summary>
		/// <value>
		/// The expected date.
		/// </value>
		public string ExpectedDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has material declaration received.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has material declaration received; otherwise, <c>false</c>.
		/// </value>
		public bool HasMaterialDeclarationReceived { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is hazardous order.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is hazardous order; otherwise, <c>false</c>.
		/// </value>
		public bool IsHazardousOrder { get; set; }
	}
}
