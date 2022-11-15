namespace PWAFeaturesRnd.ViewModels.Defect
{
	/// <summary>
	/// Defect Report Work Order Parts Used ViewModel
	/// </summary>
	public class DefectReportWorkOrderPartsUsedViewModel
    {
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
		/// Gets or sets the drawing position number.
		/// </summary>
		/// <value>
		/// The drawing position number.
		/// </value>
		public string DrawingPositionNumber { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the condition.
		/// </summary>
		/// <value>
		/// The condition.
		/// </value>
		public string Condition { get; set; }

		/// <summary>
		/// Gets or sets the quantity used.
		/// </summary>
		/// <value>
		/// The quantity used.
		/// </value>
		public int? QuantityUsed { get; set; }
	}
}
