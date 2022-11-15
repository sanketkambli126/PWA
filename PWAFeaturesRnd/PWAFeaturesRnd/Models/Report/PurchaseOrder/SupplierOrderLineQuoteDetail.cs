namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// This contract is used to hold supplier order line quote details while entering quote
	/// </summary>
	public class SupplierOrderLineQuoteDetail
	{
		/// <summary>
		/// Gets or sets the supplier order line identifier.
		/// </summary>
		/// <value>
		/// The supplier order line identifier.
		/// </value>
		public string SupplierOrderLineId { get; set; }

		/// <summary>
		/// Gets or sets the item no.
		/// </summary>
		/// <value>
		/// The item no.
		/// </value>
		public int ItemNo { get; set; }

		/// <summary>
		/// Gets or sets the name of the part.
		/// </summary>
		/// <value>
		/// The name of the part.
		/// </value>
		public string PartName { get; set; }

		/// <summary>
		/// Gets or sets the vessel part identifier.
		/// </summary>
		/// <value>
		/// The vessel part identifier.
		/// </value>
		public string VesselPartId { get; set; }

		/// <summary>
		/// Gets or sets the makers reference.
		/// </summary>
		/// <value>
		/// The makers reference.
		/// </value>
		public string MakersReference { get; set; }

		/// <summary>
		/// Gets or sets the quantity enquired.
		/// </summary>
		/// <value>
		/// The quantity enquired.
		/// </value>
		public int? QuantityEnquired { get; set; }

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
		public decimal? DiscountPercent { get; set; }

		/// <summary>
		/// Gets or sets the ex work days.
		/// </summary>
		/// <value>
		/// The ex work days.
		/// </value>
		public int? ExWorkDays { get; set; }

		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public string Notes { get; set; }

		/// <summary>
		/// Gets or sets the Units.
		/// </summary>
		/// <value>
		/// The Units.
		/// </value>
		public int? Units { get; set; }

		/// <summary>
		/// Gets or sets the supplier order identifier.
		/// </summary>
		/// <value>
		/// The supplier order identifier.
		/// </value>
		public string SupplierOrderId { get; set; }

		/// <summary>
		/// Gets or sets the order line identifier.
		/// </summary>
		/// <value>
		/// The order line identifier.
		/// </value>
		public string OrderLineId { get; set; }

		/// <summary>
		/// Gets or sets the base unit price.
		/// </summary>
		/// <value>
		/// The base unit price.
		/// </value>
		public decimal? BaseUnitPrice { get; set; }

		/// <summary>
		/// Gets or sets the unit of measurement.
		/// </summary>
		/// <value>
		/// The unit of measurement.
		/// </value>
		public string UnitOfMeasurement { get; set; }
	}
}
