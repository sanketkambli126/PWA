namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Schedule task interval values
	/// </summary>
	public class TaskIntervalValue
	{
		/// <summary>
		/// Gets or sets the PTV identifier.
		/// </summary>
		/// <value>
		/// The PTV identifier.
		/// </value>
		public string PtvId { get; set; }

		/// <summary>
		/// Gets or sets the PST identifier.
		/// </summary>
		/// <value>
		/// The PST identifier.
		/// </value>
		public string PstId { get; set; }

		/// <summary>
		/// Gets or sets the pji identifier.
		/// </summary>
		/// <value>
		/// The pji identifier.
		/// </value>
		public string PjiId { get; set; }

		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		/// <value>
		/// The interval value.
		/// </value>
		public int? IntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the interval to value.
		/// </summary>
		/// <value>
		/// The interval to value.
		/// </value>
		public int? IntervalToValue { get; set; }

		/// <summary>
		/// Gets or sets the event description.
		/// </summary>
		/// <value>
		/// The event description.
		/// </value>
		public string EventDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the type of the interval.
		/// </summary>
		/// <value>
		/// The type of the interval.
		/// </value>
		public string IntervalType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is calender based.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is calender based; otherwise, <c>false</c>.
		/// </value>
		public bool IsCalenderBased { get; set; }

		/// <summary>
		/// Shoulds the serialize interval to value.
		/// </summary>
		/// <returns><c>true</c> if [IntervalToValue property should serialize]; otherwise, <c>false</c>.</returns>
		public bool ShouldSerializeIntervalToValue()
		{
			return IntervalToValue.HasValue;
		}
	}
}
