namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// WorkOrderHistoryCBMValue
	/// </summary>
	public class WorkOrderHistoryCBMValue
	{
		/// <summary>
		/// Gets or sets the PHC identifier.
		/// </summary>
		/// <value>
		/// The PHC identifier.
		/// </value>
		public string PhcId { get; set; }

		/// <summary>
		/// Gets or sets the CTV identifier.
		/// </summary>
		/// <value>
		/// The CTV identifier.
		/// </value>
		public string CtvId { get; set; }

		/// <summary>
		/// Gets or sets the PHC unit.
		/// </summary>
		/// <value>
		/// The PHC unit.
		/// </value>
		public string PhcUnit { get; set; }

		/// <summary>
		/// Gets or sets the PHC value.
		/// </summary>
		/// <value>
		/// The PHC value.
		/// </value>
		public decimal? PhcValue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [PHC deleted].
		/// </summary>
		/// <value>
		///   <c>true</c> if [PHC deleted]; otherwise, <c>false</c>.
		/// </value>
		public bool PhcDeleted { get; set; }

		/// <summary>
		/// Gets or sets the name of the parameter.
		/// </summary>
		/// <value>
		/// The name of the parameter.
		/// </value>
		public string ParameterName { get; set; }

		/// <summary>
		/// Gets or sets the minimum value.
		/// </summary>
		/// <value>
		/// The minimum value.
		/// </value>
		public decimal? MinimumValue { get; set; }

		/// <summary>
		/// Gets or sets the maximum value.
		/// </summary>
		/// <value>
		/// The maximum value.
		/// </value>
		public decimal? MaximumValue { get; set; }
	}
}
