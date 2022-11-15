namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// SearchPartResponse
	/// </summary>
	public class SearchPartResponse
	{
		/// <summary>
		/// Gets or sets the is critical.
		/// </summary>
		/// <value>
		/// The is critical.
		/// </value>
		public bool? IsCritical { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is shared.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is shared; otherwise, <c>false</c>.
		/// </value>
		public bool IsShared { get; set; }

		/// <summary>
		/// Gets or sets the recommended quantity.
		/// </summary>
		/// <value>
		/// The recommended quantity.
		/// </value>
		public int? RecommendedQuantity { get; set; }

		/// <summary>
		/// Gets or sets the number fitted.
		/// </summary>
		/// <value>
		/// The number fitted.
		/// </value>
		public int? NumberFitted { get; set; }

		/// <summary>
		/// Gets or sets the is certificate required.
		/// </summary>
		/// <value>
		/// The is certificate required.
		/// </value>
		public bool? IsCertificateRequired { get; set; }

		/// <summary>
		/// Gets or sets the estimated price.
		/// </summary>
		/// <value>
		/// The estimated price.
		/// </value>
		public decimal? EstimatedPrice { get; set; }

		/// <summary>
		/// Gets or sets the pending order count.
		/// </summary>
		/// <value>
		/// The pending order count.
		/// </value>
		public int? PendingOrderCount { get; set; }

		/// <summary>
		/// Gets or sets the pil identifier.
		/// </summary>
		/// <value>
		/// The pil identifier.
		/// </value>
		public string PilId { get; set; }

		/// <summary>
		/// Gets or sets the mla identifier.
		/// </summary>
		/// <value>
		/// The mla identifier.
		/// </value>
		public string MlaId { get; set; }

		/// <summary>
		/// Gets or sets the condition.
		/// </summary>
		/// <value>
		/// The condition.
		/// </value>
		public string Condition { get; set; }

		/// <summary>
		/// Gets or sets the is technical critical.
		/// </summary>
		/// <value>
		/// The is technical critical.
		/// </value>
		public bool? IsTechnicalCritical { get; set; }

		/// <summary>
		/// Gets or sets the is operational critical.
		/// </summary>
		/// <value>
		/// The is operational critical.
		/// </value>
		public bool? IsOperationalCritical { get; set; }

		/// <summary>
		/// Gets or sets the is dangerous goods.
		/// </summary>
		/// <value>
		/// The is dangerous goods.
		/// </value>
		public bool? IsDangerousGoods { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is marked for reorder.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is marked for reorder; otherwise, <c>false</c>.
		/// </value>
		public bool IsMarkedForReorder { get; set; }

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
		/// Gets or sets a value indicating whether this instance is mapped to job.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is mapped to job; otherwise, <c>false</c>.
		/// </value>
		public bool IsMappedToJob { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the operational minimum stock.
		/// </summary>
		/// <value>
		/// The operational minimum stock.
		/// </value>
		public int? OperationalMinimumStock { get; set; }

		/// <summary>
		/// Gets or sets the location identifier.
		/// </summary>
		/// <value>
		/// The location identifier.
		/// </value>
		public string LocationId { get; set; }

		/// <summary>
		/// Gets or sets the measur unit.
		/// </summary>
		/// <value>
		/// The measur unit.
		/// </value>
		public string MeasurUnit { get; set; }

		/// <summary>
		/// Gets or sets the PWP identifier.
		/// </summary>
		/// <value>
		/// The PWP identifier.
		/// </value>
		public string PwpId { get; set; }

		/// <summary>
		/// Gets or sets the PWH identifier.
		/// </summary>
		/// <value>
		/// The PWH identifier.
		/// </value>
		public string PwhId { get; set; }

		/// <summary>
		/// Gets or sets the PTS identifier.
		/// </summary>
		/// <value>
		/// The PTS identifier.
		/// </value>
		public string PtsId { get; set; }

		/// <summary>
		/// Gets or sets the vessel inventory identifier.
		/// </summary>
		/// <value>
		/// The vessel inventory identifier.
		/// </value>
		public string VesselInventoryId { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the part identifier.
		/// </summary>
		/// <value>
		/// The part identifier.
		/// </value>
		public string PartId { get; set; }

		/// <summary>
		/// Gets or sets the name of the part.
		/// </summary>
		/// <value>
		/// The name of the part.
		/// </value>
		public string PartName { get; set; }

		/// <summary>
		/// Gets or sets the maker reference number.
		/// </summary>
		/// <value>
		/// The maker reference number.
		/// </value>
		public string MakerReferenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the plate sheet number.
		/// </summary>
		/// <value>
		/// The plate sheet number.
		/// </value>
		public string PlateSheetNumber { get; set; }

		/// <summary>
		/// Gets or sets the drawing position.
		/// </summary>
		/// <value>
		/// The drawing position.
		/// </value>
		public string DrawingPosition { get; set; }

		/// <summary>
		/// Gets or sets the quantity rob.
		/// </summary>
		/// <value>
		/// The quantity rob.
		/// </value>
		public int? QuantityROB { get; set; }

		/// <summary>
		/// Gets or sets the quantity required.
		/// </summary>
		/// <value>
		/// The quantity required.
		/// </value>
		public float? QuantityRequired { get; set; }

		/// <summary>
		/// Gets or sets the available stock.
		/// </summary>
		/// <value>
		/// The available stock.
		/// </value>
		public int? AvailableStock { get; set; }

		/// <summary>
		/// Gets or sets the adjust stock.
		/// </summary>
		/// <value>
		/// The adjust stock.
		/// </value>
		public int? AdjustStock { get; set; }

		/// <summary>
		/// Creates new stock.
		/// </summary>
		/// <value>
		/// The new stock.
		/// </value>
		public int? NewStock { get; set; }

		/// <summary>
		/// Gets or sets the quantity used.
		/// </summary>
		/// <value>
		/// The quantity used.
		/// </value>
		public int? QuantityUsed { get; set; }

		/// <summary>
		/// Gets or sets the minimun stock.
		/// </summary>
		/// <value>
		/// The minimun stock.
		/// </value>
		public int MinimunStock { get; set; }

		/// <summary>
		/// Gets or sets the is renew spares.
		/// </summary>
		/// <value>
		/// The is renew spares.
		/// </value>
		public bool? IsRenewSpares { get; set; }
	}
}
