namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Reschedule Request
	/// </summary>
	public class RescheduleRequest
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the job identifier.
		/// </summary>
		/// <value>
		/// The job identifier.
		/// </value>
		public string JobId { get; set; }

		/// <summary>
		/// Gets or sets the interval type identifier.
		/// </summary>
		/// <value>
		/// The interval type identifier.
		/// </value>
		public string IntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the interval value.
		/// </summary>
		/// <value>
		/// The interval value.
		/// </value>
		public int IntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the work order identifier.
		/// </summary>
		/// <value>
		/// The work order identifier.
		/// </value>
		public string WorkOrderId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule status.
		/// </summary>
		/// <value>
		/// The reschedule status.
		/// </value>
		public string RescheduleStatus { get; set; }

		/// <summary>
		/// Gets or sets the por identifier.
		/// </summary>
		/// <value>
		/// The por identifier.
		/// </value>
		public string PorId { get; set; }

		/// <summary>
		/// Gets or sets the por request identifier.
		/// </summary>
		/// <value>
		/// The por request identifier.
		/// </value>
		public string PorRequestId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is range wo.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is range wo; otherwise, <c>false</c>.
		/// </value>
		public bool IsRangeWO { get; set; }
	}
}
