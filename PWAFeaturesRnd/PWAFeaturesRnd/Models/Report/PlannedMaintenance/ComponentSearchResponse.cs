using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Component Search Response
	/// </summary>
	public class ComponentSearchResponse
	{
		/// <summary>
		/// Gets or sets the inventory new stock.
		/// </summary>
		/// <value>
		/// The inventory new stock.
		/// </value>
		public int? InventoryNewStock { get; set; }

		/// <summary>
		/// Gets or sets the inventory reconditioned stock.
		/// </summary>
		/// <value>
		/// The inventory reconditioned stock.
		/// </value>
		public int? InventoryReconditionedStock { get; set; }

		/// <summary>
		/// Gets or sets the inventory unit.
		/// </summary>
		/// <value>
		/// The inventory unit.
		/// </value>
		public string InventoryUnit { get; set; }

		/// <summary>
		/// Gets or sets the type of the alternative number.
		/// </summary>
		/// <value>
		/// The type of the alternative number.
		/// </value>
		public string AlternativeNumberType { get; set; }

		/// <summary>
		/// Gets or sets the alternative number.
		/// </summary>
		/// <value>
		/// The alternative number.
		/// </value>
		public string AlternativeNumber { get; set; }

		/// <summary>
		/// Gets or sets the name of the system area.
		/// </summary>
		/// <value>
		/// The name of the system area.
		/// </value>
		public string SystemAreaName { get; set; }

		/// <summary>
		/// Gets or sets the maker identifier.
		/// </summary>
		/// <value>
		/// The maker identifier.
		/// </value>
		public string MakerId { get; set; }

		/// <summary>
		/// Gets or sets the model identifier.
		/// </summary>
		/// <value>
		/// The model identifier.
		/// </value>
		public string ModelId { get; set; }

		/// <summary>
		/// Gets or sets the designer identifier.
		/// </summary>
		/// <value>
		/// The designer identifier.
		/// </value>
		public string DesignerId { get; set; }

		/// <summary>
		/// Gets or sets the parent component identifier.
		/// </summary>
		/// <value>
		/// The parent component identifier.
		/// </value>
		public string ParentComponentId { get; set; }

		/// <summary>
		/// Gets or sets the is active.
		/// </summary>
		/// <value>
		/// The is active.
		/// </value>
		public bool? IsActive { get; set; }

		/// <summary>
		/// Gets or sets the is deleted.
		/// </summary>
		/// <value>
		/// The is deleted.
		/// </value>
		public bool? IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the running HRS.
		/// </summary>
		/// <value>
		/// The running HRS.
		/// </value>
		public int? RunningHrs { get; set; }

		/// <summary>
		/// Gets or sets the last reading date.
		/// </summary>
		/// <value>
		/// The last reading date.
		/// </value>
		public DateTime? LastReadingDate { get; set; }

		/// <summary>
		/// Gets or sets the spare account code.
		/// </summary>
		/// <value>
		/// The spare account code.
		/// </value>
		public string SpareAccountCode { get; set; }

		/// <summary>
		/// Gets or sets the spare account code description.
		/// </summary>
		/// <value>
		/// The spare account code description.
		/// </value>
		public string SpareAccountCodeDescription { get; set; }

		/// <summary>
		/// Gets or sets the service account code.
		/// </summary>
		/// <value>
		/// The service account code.
		/// </value>
		public string ServiceAccountCode { get; set; }

		/// <summary>
		/// Gets or sets the service account code description.
		/// </summary>
		/// <value>
		/// The service account code description.
		/// </value>
		public string ServiceAccountCodeDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has running hours counter.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has running hours counter; otherwise, <c>false</c>.
		/// </value>
		public bool HasRunningHoursCounter { get; set; }

		/// <summary>
		/// Gets or sets the inventory rob.
		/// </summary>
		/// <value>
		/// The inventory rob.
		/// </value>
		public int? InventoryROB { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has revolutions counter.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has revolutions counter; otherwise, <c>false</c>.
		/// </value>
		public bool HasRevolutionsCounter { get; set; }

		/// <summary>
		/// Gets or sets the inventory plate sheet number.
		/// </summary>
		/// <value>
		/// The inventory plate sheet number.
		/// </value>
		public string InventoryPlateSheetNumber { get; set; }

		/// <summary>
		/// Gets or sets the inventory maker reference number.
		/// </summary>
		/// <value>
		/// The inventory maker reference number.
		/// </value>
		public string InventoryMakerReferenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the catalogue identifier.
		/// </summary>
		/// <value>
		/// The catalogue identifier.
		/// </value>
		public string CatalogueId { get; set; }

		/// <summary>
		/// Gets or sets the name of the catalogue.
		/// </summary>
		/// <value>
		/// The name of the catalogue.
		/// </value>
		public string CatalogueName { get; set; }

		/// <summary>
		/// Gets or sets the base unit cost.
		/// </summary>
		/// <value>
		/// The base unit cost.
		/// </value>
		public decimal? BaseUnitCost { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the component code.
		/// </summary>
		/// <value>
		/// The component code.
		/// </value>
		public string ComponentCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the component position.
		/// </summary>
		/// <value>
		/// The component position.
		/// </value>
		public string ComponentPosition { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsCritical { get; set; }

		/// <summary>
		/// Gets or sets the component class code.
		/// </summary>
		/// <value>
		/// The component class code.
		/// </value>
		public string ComponentClassCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the maker.
		/// </summary>
		/// <value>
		/// The name of the maker.
		/// </value>
		public string MakerName { get; set; }

		/// <summary>
		/// Gets or sets the designer.
		/// </summary>
		/// <value>
		/// The designer.
		/// </value>
		public string Designer { get; set; }

		/// <summary>
		/// Gets or sets the model.
		/// </summary>
		/// <value>
		/// The model.
		/// </value>
		public string Model { get; set; }

		/// <summary>
		/// Gets or sets the serial no.
		/// </summary>
		/// <value>
		/// The serial no.
		/// </value>
		public string SerialNo { get; set; }

		/// <summary>
		/// Gets or sets the warranty date.
		/// </summary>
		/// <value>
		/// The warranty date.
		/// </value>
		public DateTime? WarrantyDate { get; set; }

		/// <summary>
		/// Gets or sets the system area identifier.
		/// </summary>
		/// <value>
		/// The system area identifier.
		/// </value>
		public string SystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the department identifier.
		/// </summary>
		/// <value>
		/// The department identifier.
		/// </value>
		public string DepartmentId { get; set; }

		/// <summary>
		/// Gets or sets the department.
		/// </summary>
		/// <value>
		/// The department.
		/// </value>
		public string Department { get; set; }

		/// <summary>
		/// Gets or sets the inventory identifier.
		/// </summary>
		/// <value>
		/// The inventory identifier.
		/// </value>
		public string InventoryId { get; set; }

		/// <summary>
		/// Gets or sets the name of the inventory.
		/// </summary>
		/// <value>
		/// The name of the inventory.
		/// </value>
		public string InventoryName { get; set; }

		/// <summary>
		/// Gets or sets the inventory drawing position.
		/// </summary>
		/// <value>
		/// The inventory drawing position.
		/// </value>
		public string InventoryDrawingPosition { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has events counter.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has events counter; otherwise, <c>false</c>.
		/// </value>
		public bool HasEventsCounter { get; set; }
	}
}
