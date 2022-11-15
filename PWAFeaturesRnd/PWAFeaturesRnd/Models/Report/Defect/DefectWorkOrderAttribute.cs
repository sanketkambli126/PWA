namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// This class is for defect work order attribute.
	/// </summary>
	public class DefectWorkOrderAttribute
	{
		/// <summary>
		/// Gets or sets the dal identifier.
		/// </summary>
		/// <value>
		/// The dal identifier.
		/// </value>
		public string DalId { get; set; }

		/// <summary>
		/// Gets or sets the lookup code.
		/// </summary>
		/// <value>
		/// The lookup code.
		/// </value>
		public string LookupCode { get; set; }

		/// <summary>
		/// Gets or sets the name of the attribute.
		/// </summary>
		/// <value>
		/// The name of the attribute.
		/// </value>
		public string AttributeName { get; set; }

		/// <summary>
		/// Gets or sets the attribute short code.
		/// </summary>
		/// <value>
		/// The attribute short code.
		/// </value>
		public string AttributeShortCode { get; set; }
	}
}
