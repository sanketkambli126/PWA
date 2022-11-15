namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
	/// <summary>
	/// View Quote Order Line View Model
	/// </summary>
	public class ViewQuoteOrderLineViewModel
	{
		/// <summary>
		/// Gets or sets the name of the part.
		/// </summary>
		/// <value>
		/// The name of the part.
		/// </value>
		public string PartName { get; set; }

		/// <summary>
		/// Gets or sets the makers reference.
		/// </summary>
		/// <value>
		/// The makers reference.
		/// </value>
		public string MakersReference { get; set; }

		/// <summary>
		/// Gets or sets the qty enq.
		/// </summary>
		/// <value>
		/// The qty enq.
		/// </value>
		public int? QtyEnq { get; set; }

		/// <summary>
		/// Gets or sets the supplier qty.
		/// </summary>
		/// <value>
		/// The supplier qty.
		/// </value>
		public string SupplierQty { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is supplier qty mismatch.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is supplier qty mismatch; otherwise, <c>false</c>.
		/// </value>
		public bool IsSupplierQtyMismatch { get; set; }

		/// <summary>
		/// Gets or sets the uom.
		/// </summary>
		/// <value>
		/// The uom.
		/// </value>
		public string UOM { get; set; }

		/// <summary>
		/// Gets or sets the unit price.
		/// </summary>
		/// <value>
		/// The unit price.
		/// </value>
		public decimal? UnitPrice { get; set; }

		/// <summary>
		/// Gets or sets the discount percent.
		/// </summary>
		/// <value>
		/// The discount percent.
		/// </value>
		public string DiscountPercent { get; set; }

		/// <summary>
		/// Gets or sets the sub total.
		/// </summary>
		/// <value>
		/// The sub total.
		/// </value>
		public string SubTotal { get; set; }

		/// <summary>
		/// Gets or sets the ex days.
		/// </summary>
		/// <value>
		/// The ex days.
		/// </value>
		public string ExDays { get; set; }

		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public string Notes { get; set; }

		/// <summary>
		/// Gets or sets the item no.
		/// </summary>
		/// <value>
		/// The item no.
		/// </value>
		public int ItemNo { get; set; }

		/// <summary>
		/// Gets or sets the vessel part identifier.
		/// </summary>
		/// <value>
		/// The vessel part identifier.
		/// </value>
		public string VesselPartId { get; set; }

	}
}
