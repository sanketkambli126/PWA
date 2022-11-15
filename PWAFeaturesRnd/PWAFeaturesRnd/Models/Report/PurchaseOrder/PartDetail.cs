namespace PWAFeaturesRnd.Models.Report.PurchaseOrder
{
	/// <summary>
	/// Part Detail
	/// </summary>
	public class PartDetail
	{
		/// <summary>
		/// Gets or sets the duration of the shelf life.
		/// </summary>
		/// <value>
		/// The duration of the shelf life.
		/// </value>
		public int? ShelfLifeDuration { get; set; }

		/// <summary>
		/// Gets or sets the volume unit.
		/// </summary>
		/// <value>
		/// The volume unit.
		/// </value>
		public string VolumeUnit { get; set; }

		/// <summary>
		/// Gets or sets the volume.
		/// </summary>
		/// <value>
		/// The volume.
		/// </value>
		public decimal? Volume { get; set; }

		/// <summary>
		/// Gets or sets the weight unit.
		/// </summary>
		/// <value>
		/// The weight unit.
		/// </value>
		public string WeightUnit { get; set; }

		/// <summary>
		/// Gets or sets the weight.
		/// </summary>
		/// <value>
		/// The weight.
		/// </value>
		public decimal? Weight { get; set; }

		/// <summary>
		/// Gets or sets the unit of measurement.
		/// </summary>
		/// <value>
		/// The unit of measurement.
		/// </value>
		public string UnitOfMeasurement { get; set; }

		/// <summary>
		/// Gets or sets the drawing position.
		/// </summary>
		/// <value>
		/// The drawing position.
		/// </value>
		public string DrawingPosition { get; set; }

		/// <summary>
		/// Gets or sets the maker reference number.
		/// </summary>
		/// <value>
		/// The maker reference number.
		/// </value>
		public string MakerReferenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the operational minimum stock.
		/// </summary>
		/// <value>
		/// The operational minimum stock.
		/// </value>
		public int? OperationalMinStock { get; set; }

		/// <summary>
		/// Gets or sets the shelf life duration unit.
		/// </summary>
		/// <value>
		/// The shelf life duration unit.
		/// </value>
		public string ShelfLifeDurationUnit { get; set; }

		/// <summary>
		/// Gets or sets the technical minimum stock.
		/// </summary>
		/// <value>
		/// The technical minimum stock.
		/// </value>
		public int? TechnicalMinStock { get; set; }

		/// <summary>
		/// Gets or sets the is stock item.
		/// </summary>
		/// <value>
		/// The is stock item.
		/// </value>
		public bool? IsStockItem { get; set; }

		/// <summary>
		/// Gets or sets the harmonized weight.
		/// </summary>
		/// <value>
		/// The harmonized weight.
		/// </value>
		public decimal? HarmonizedWeight { get; set; }

		/// <summary>
		/// Gets or sets the harmonized number.
		/// </summary>
		/// <value>
		/// The harmonized number.
		/// </value>
		public string HarmonizedNumber { get; set; }

		/// <summary>
		/// Gets or sets the name of the certificate.
		/// </summary>
		/// <value>
		/// The name of the certificate.
		/// </value>
		public string CertificateName { get; set; }

		/// <summary>
		/// Gets or sets the is certificate required.
		/// </summary>
		/// <value>
		/// The is certificate required.
		/// </value>
		public bool? IsCertificateRequired { get; set; }

		/// <summary>
		/// Gets or sets the dangerous goods description.
		/// </summary>
		/// <value>
		/// The dangerous goods description.
		/// </value>
		public string DangerousGoodsDescription { get; set; }

		/// <summary>
		/// Gets or sets the is dangerous goods.
		/// </summary>
		/// <value>
		/// The is dangerous goods.
		/// </value>
		public bool? IsDangerousGoods { get; set; }

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
		/// Gets or sets the recommended rob.
		/// </summary>
		/// <value>
		/// The recommended rob.
		/// </value>
		public int? RecommendedROB { get; set; }

		/// <summary>
		/// Gets or sets the plate sheet number.
		/// </summary>
		/// <value>
		/// The plate sheet number.
		/// </value>
		public string PlateSheetNumber { get; set; }
	}
}
