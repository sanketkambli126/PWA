using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// RelatedJobsDetailResponse
	/// </summary>
	public class RelatedJobsDetailResponse
	{
		/// <summary>
		/// Gets or sets the work order status.
		/// </summary>
		/// <value>
		/// The work order status.
		/// </value>
		public string WorkOrderStatus { get; set; }

		/// <summary>
		/// Gets or sets the DWR identifier.
		/// </summary>
		/// <value>
		/// The DWR identifier.
		/// </value>
		public string DwrId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is job completed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is job completed; otherwise, <c>false</c>.
		/// </value>
		public bool IsJobCompleted { get; set; }

		/// <summary>
		/// Gets or sets the report form required.
		/// </summary>
		/// <value>
		/// The report form required.
		/// </value>
		public bool? ReportFormRequired { get; set; }

		/// <summary>
		/// Gets or sets the hse assessment required.
		/// </summary>
		/// <value>
		/// The hse assessment required.
		/// </value>
		public bool? HSEAssessmentRequired { get; set; }

		/// <summary>
		/// Gets or sets the job description.
		/// </summary>
		/// <value>
		/// The job description.
		/// </value>
		public string JobDescription { get; set; }

		/// <summary>
		/// Gets or sets the work order status identifier.
		/// </summary>
		/// <value>
		/// The work order status identifier.
		/// </value>
		public string WorkOrderStatusId { get; set; }

		/// <summary>
		/// Gets or sets the job guideline text.
		/// </summary>
		/// <value>
		/// The job guideline text.
		/// </value>
		public string JobGuidelineText { get; set; }

		/// <summary>
		/// Gets or sets the interval.
		/// </summary>
		/// <value>
		/// The interval.
		/// </value>
		public int Interval { get; set; }

		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime DueDate { get; set; }

		/// <summary>
		/// Gets or sets the type of the job.
		/// </summary>
		/// <value>
		/// The type of the job.
		/// </value>
		public string JobType { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the PST identifier.
		/// </summary>
		/// <value>
		/// The PST identifier.
		/// </value>
		public string PstId { get; set; }

		/// <summary>
		/// Gets or sets the pwo identifier.
		/// </summary>
		/// <value>
		/// The pwo identifier.
		/// </value>
		public string PwoId { get; set; }

		/// <summary>
		/// Gets or sets the type of the interval.
		/// </summary>
		/// <value>
		/// The type of the interval.
		/// </value>
		public string IntervalType { get; set; }

		/// <summary>
		/// Gets or sets the last completed date.
		/// </summary>
		/// <value>
		/// The last completed date.
		/// </value>
		public DateTime? LastCompletedDate { get; set; }
	}
}
