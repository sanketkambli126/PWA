namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Maintenance Dashboard Response
	/// </summary>
	public class WorkOrderJobTypeDetail
    {
		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the PJC identifier.
		/// </summary>
		/// <value>
		/// The PJC identifier.
		/// </value>
		public string PjcId { get; set; }

		/// <summary>
		/// Gets or sets the job type short code.
		/// </summary>
		/// <value>
		/// The job type short code.
		/// </value>
		public string JobTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets the job type description.
		/// </summary>
		/// <value>
		/// The job type description.
		/// </value>
		public string JobTypeDescription { get; set; }

		/// <summary>
		/// Gets or sets the count.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public int Count { get; set; }

		/// <summary>
		/// Gets or sets the critical job count.
		/// </summary>
		/// <value>
		/// The critical job count.
		/// </value>
		public int CriticalJobCount { get; set; }

		/// <summary>
		/// Gets or sets the insufficient spare wo count.
		/// </summary>
		/// <value>
		/// The insufficient spare wo count.
		/// </value>
		public int InsufficientSpareWOCount { get; set; }
	}
}
