using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Work History Request
	/// </summary>
	public class WorkHistoryRequest
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets the top system area identifier.
		/// </summary>
		/// <value>
		/// The top system area identifier.
		/// </value>
		public string TopSystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the category identifier.
		/// </summary>
		/// <value>
		/// The category identifier.
		/// </value>
		public string CategoryId { get; set; }

		/// <summary>
		/// Gets or sets the component identifier.
		/// </summary>
		/// <value>
		/// The component identifier.
		/// </value>
		public string ComponentId { get; set; }

		/// <summary>
		/// Gets or sets the parent component identifier.
		/// </summary>
		/// <value>
		/// The parent component identifier.
		/// </value>
		public string ParentComponentId { get; set; }

		/// <summary>
		/// Gets or sets the department ids.
		/// </summary>
		/// <value>
		/// The department ids.
		/// </value>
		public List<string> DepartmentIds { get; set; }

		/// <summary>
		/// Gets or sets the responsibility ids.
		/// </summary>
		/// <value>
		/// The responsibility ids.
		/// </value>
		public List<string> ResponsibilityIds { get; set; }

		/// <summary>
		/// Gets or sets the job type ids.
		/// </summary>
		/// <value>
		/// The job type ids.
		/// </value>
		public List<string> JobTypeIds { get; set; }

		/// <summary>
		/// Gets or sets the class ids.
		/// </summary>
		/// <value>
		/// The class ids.
		/// </value>
		public List<string> ClassIds { get; set; }

		/// <summary>
		/// Gets or sets the criticality.
		/// </summary>
		/// <value>
		/// The criticality.
		/// </value>
		public bool? Criticality { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [show rescheduled].
		/// </summary>
		/// <value>
		///   <c>true</c> if [show rescheduled]; otherwise, <c>false</c>.
		/// </value>
		public bool ShowRescheduled { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the reschedue type ids.
		/// </summary>
		/// <value>
		/// The reschedue type ids.
		/// </value>
		public List<string> ReschedueTypeIds { get; set; }
	}
}
