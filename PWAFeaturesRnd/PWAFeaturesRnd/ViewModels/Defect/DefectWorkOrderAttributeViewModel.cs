namespace PWAFeaturesRnd.ViewModels.Defect
{
	/// <summary>
	/// DefectWorkOrderAttributeViewModel
	/// </summary>
	public class DefectWorkOrderAttributeViewModel
    {
		/// <summary>
		/// Gets or sets the dal identifier.
		/// </summary>
		/// <value>
		/// The dal identifier.
		/// </value>
		public string DalId { get; set; }

		/// <summary>
		/// Gets or sets the name of the attribute.
		/// </summary>
		/// <value>
		/// The name of the attribute.
		/// </value>
		public string AttributeName { get; set; }

        /// <summary>
        /// Gets or sets the lookup code.
        /// </summary>
        /// <value>
        /// The lookup code.
        /// </value>
        public string LookupCode { get; set; }
    }
}
