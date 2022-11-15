using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// WorkOrderHistoryDetail
	/// </summary>
	public class WorkOrderHistoryDetail
    {
		/// <summary>
		/// Gets or sets the work order detail.
		/// </summary>
		/// <value>
		/// The work order detail.
		/// </value>
		public ReportWorkOrder WorkOrderDetail { get; set; }

		/// <summary>
		/// Gets or sets the related jobs.
		/// </summary>
		/// <value>
		/// The related jobs.
		/// </value>
		public List<RelatedJobsDetailResponse> RelatedJobs { get; set; }
	}
}
