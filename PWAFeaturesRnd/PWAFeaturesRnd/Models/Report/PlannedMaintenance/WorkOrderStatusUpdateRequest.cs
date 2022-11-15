using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// WorkOrderStatusUpdateRequest
	/// </summary>
	public class WorkOrderStatusUpdateRequest
	{
		/// <summary>
		/// Gets or sets the work order ids.
		/// </summary>
		/// <value>
		/// The work order ids.
		/// </value>
		public List<string> WorkOrderIds { get; set; }

		/// <summary>
		/// Gets or sets the job status.
		/// </summary>
		/// <value>
		/// The job status.
		/// </value>
		public JobStatus JobStatus { get; set; }

		/// <summary>
		/// Gets or sets the reopen comment.
		/// </summary>
		/// <value>
		/// The reopen comment.
		/// </value>
		public string ReopenComment { get; set; }
	}
}
