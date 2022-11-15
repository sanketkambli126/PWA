namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	/// <summary>
	/// 
	/// </summary>
	public class InspectionTypeDetail
	{
		/// <summary>
		/// Gets or sets the inspection type identifier.
		/// </summary>
		/// <value>
		/// The inspection type identifier.
		/// </value>
		public string InspectionTypeId { get; set; }

		/// <summary>
		/// Gets or sets the type of the inspection.
		/// </summary>
		/// <value>
		/// The type of the inspection.
		/// </value>
		public string InspectionType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is internal.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is internal; otherwise, <c>false</c>.
		/// </value>
		public bool IsInternal { get; set; }

		/// <summary>
		/// Gets or sets the type of the inspection header.
		/// </summary>
		/// <value>
		/// The type of the inspection header.
		/// </value>
		public string InspectionHeaderType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is audit type.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is audit type; otherwise, <c>false</c>.
		/// </value>
		public bool IsAuditType { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }
	}
}
