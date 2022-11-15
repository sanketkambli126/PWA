namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// DefectReportWorkOrderPartsUsed
	/// </summary>
	public class DefectReportWorkOrderPartsUsed
	{
		/// <summary>
		/// Gets or sets the reorder quantity.
		/// </summary>
		/// <value>
		/// The reorder quantity.
		/// </value>
		public int? ReorderQuantity { get; set; }

		/// <summary>
		/// Gets or sets the remarks.
		/// </summary>
		/// <value>
		/// The remarks.
		/// </value>
		public string Remarks { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is marked for reorder.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is marked for reorder; otherwise, <c>false</c>.
		/// </value>
		public bool IsMarkedForReorder { get; set; }

		/// <summary>
		/// Gets or sets the quantity used.
		/// </summary>
		/// <value>
		/// The quantity used.
		/// </value>
		public int? QuantityUsed { get; set; }

		/// <summary>
		/// Gets or sets the stock.
		/// </summary>
		/// <value>
		/// The stock.
		/// </value>
		public int? Stock { get; set; }

		/// <summary>
		/// Gets or sets the condition.
		/// </summary>
		/// <value>
		/// The condition.
		/// </value>
		public string Condition { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the drawing position number.
		/// </summary>
		/// <value>
		/// The drawing position number.
		/// </value>
		public string DrawingPositionNumber { get; set; }

		/// <summary>
		/// Gets or sets the is critical.
		/// </summary>
		/// <value>
		/// The is critical.
		/// </value>
		public bool? IsCritical { get; set; }

		/// <summary>
		/// Gets or sets the plate sheet number.
		/// </summary>
		/// <value>
		/// The plate sheet number.
		/// </value>
		public string PlateSheetNumber { get; set; }

		/// <summary>
		/// Gets or sets the name of the part.
		/// </summary>
		/// <value>
		/// The name of the part.
		/// </value>
		public string PartName { get; set; }

		/// <summary>
		/// Gets or sets the type of the spare.
		/// </summary>
		/// <value>
		/// The type of the spare.
		/// </value>
		public string SpareType { get; set; }

		/// <summary>
		/// Gets or sets the mla identifier.
		/// </summary>
		/// <value>
		/// The mla identifier.
		/// </value>
		public string MlaId { get; set; }

		/// <summary>
		/// Gets or sets the PLT identifier.
		/// </summary>
		/// <value>
		/// The PLT identifier.
		/// </value>
		public string PltId { get; set; }

		/// <summary>
		/// Gets or sets the viv identifier.
		/// </summary>
		/// <value>
		/// The viv identifier.
		/// </value>
		public string VivId { get; set; }

		/// <summary>
		/// Gets or sets the pil identifier.
		/// </summary>
		/// <value>
		/// The pil identifier.
		/// </value>
		public string PilId { get; set; }

		/// <summary>
		/// Gets or sets the DRW identifier.
		/// </summary>
		/// <value>
		/// The DRW identifier.
		/// </value>
		public string DrwId { get; set; }

		/// <summary>
		/// Gets or sets the DRP identifier.
		/// </summary>
		/// <value>
		/// The DRP identifier.
		/// </value>
		public string DrpId { get; set; }

		/// <summary>
		/// Gets or sets the maker reference number.
		/// </summary>
		/// <value>
		/// The maker reference number.
		/// </value>
		public string MakerReferenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the adjust stock.
		/// </summary>
		/// <value>
		/// The adjust stock.
		/// </value>
		public int? AdjustStock { get; set; }
	}
}
