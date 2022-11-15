namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// PMS Reschedule Rules Response
	/// </summary>
	public class PMSRescheduleRulesResponse
	{
		/// <summary>
		/// Gets or sets the interval type identifier.
		/// </summary>
		/// <value>
		/// The interval type identifier.
		/// </value>
		public string IntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is critical.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
		/// </value>
		public bool IsCritical { get; set; }

		/// <summary>
		/// Gets or sets from value.
		/// </summary>
		/// <value>
		/// From value.
		/// </value>
		public int? FromValue { get; set; }

		/// <summary>
		/// Converts to value.
		/// </summary>
		/// <value>
		/// To value.
		/// </value>
		public int? ToValue { get; set; }

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public int? Value { get; set; }

		/// <summary>
		/// Gets or sets the percent value.
		/// </summary>
		/// <value>
		/// The percent value.
		/// </value>
		public int? PercentValue { get; set; }

		/// <summary>
		/// Gets or sets the months value.
		/// </summary>
		/// <value>
		/// The months value.
		/// </value>
		public int? MonthsValue { get; set; }

		/// <summary>
		/// Gets or sets the job interval type identifier.
		/// </summary>
		/// <value>
		/// The job interval type identifier.
		/// </value>
		public string JobIntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can reschedule.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can reschedule; otherwise, <c>false</c>.
		/// </value>
		public bool CanReschedule { get; set; }
	}
}
