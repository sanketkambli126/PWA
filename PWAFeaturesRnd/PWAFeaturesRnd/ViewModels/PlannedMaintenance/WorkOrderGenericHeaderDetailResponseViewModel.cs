namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
	/// <summary>
	/// WorkOrderGenericHeaderDetailResponseViewModel
	/// </summary>
	public class WorkOrderGenericHeaderDetailResponseViewModel
	{
		/// <summary>
		/// Gets or sets the work order identifier.
		/// </summary>
		/// <value>
		/// The work order identifier.
		/// </value>
		public string WorkOrderId { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the schedule task identifier.
		/// </summary>
		/// <value>
		/// The schedule task identifier.
		/// </value>
		public string ScheduleTaskId { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the work order indication type identifier.
		/// </summary>
		/// <value>
		/// The work order indication type identifier.
		/// </value>
		public string WorkOrderIndicationTypeId { get; set; }
	}
}
