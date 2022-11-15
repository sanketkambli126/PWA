using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// Maintenance Dashboard Response
	/// </summary>
	public class MaintenanceDashboardResponse
	{
		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the vessel description.
		/// </summary>
		/// <value>
		/// The vessel description.
		/// </value>
		public string VesselDescription { get; set; }

		/// <summary>
		/// Gets or sets the vessel age.
		/// </summary>
		/// <value>
		/// The vessel age.
		/// </value>
		public DateTime? VesselBuiltDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is next position available.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is next position available; otherwise, <c>false</c>.
		/// </value>
		public bool IsNextPositionAvailable { get; set; }

		/// <summary>
		/// Gets or sets the due count.
		/// </summary>
		/// <value>
		/// The due count.
		/// </value>
		public int? DueCount { get; set; }

		/// <summary>
		/// Gets or sets the overdue count.
		/// </summary>
		/// <value>
		/// The overdue count.
		/// </value>
		public int? OverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the period overdue count.
		/// </summary>
		/// <value>
		/// The period overdue count.
		/// </value>
		public int? PeriodOverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the work order office approval count.
		/// </summary>
		/// <value>
		/// The work order office approval count.
		/// </value>
		public int? WOOfficeApprovalCount { get; set; }

		/// <summary>
		/// Gets or sets the wo vessel approval count.
		/// </summary>
		/// <value>
		/// The wo vessel approval count.
		/// </value>
		public int? WOVesselApprovalCount { get; set; }

		/// <summary>
		/// Gets or sets the calendar jobs reschedule count.
		/// </summary>
		/// <value>
		/// The calendar jobs reschedule count.
		/// </value>
		public int? CalendarJobsRescheduleCount { get; set; }

		/// <summary>
		/// Gets or sets the counter jobs reschedule count.
		/// </summary>
		/// <value>
		/// The counter jobs reschedule count.
		/// </value>
		public int? CounterJobsRescheduleCount { get; set; }

		/// <summary>
		/// Gets or sets the counter last updated date.
		/// </summary>
		/// <value>
		/// The counter last updated date.
		/// </value>
		public DateTime? CounterLastUpdatedDate { get; set; }

		/// <summary>
		/// Gets or sets the tech minimum count.
		/// </summary>
		/// <value>
		/// The tech minimum count.
		/// </value>
		public int? TechMinCount { get; set; }

		/// <summary>
		/// Gets or sets the opr minimum count.
		/// </summary>
		/// <value>
		/// The opr minimum count.
		/// </value>
		public int? OprMinCount { get; set; }

		/// <summary>
		/// Gets or sets the being processed count.
		/// </summary>
		/// <value>
		/// The being processed count.
		/// </value>
		public int? BeingProcessedCount { get; set; }

		/// <summary>
		/// Gets or sets the ordered count.
		/// </summary>
		/// <value>
		/// The ordered count.
		/// </value>
		public int? OrderedCount { get; set; }

		/// <summary>
		/// Gets or sets the delivery on the way count.
		/// </summary>
		/// <value>
		/// The delivery on the way count.
		/// </value>
		public int? DeliveryOnTheWayCount { get; set; }

		/// <summary>
		/// Gets or sets the reported jobs detail.
		/// </summary>
		/// <value>
		/// The reported jobs detail.
		/// </value>
		public List<ReportedJobs> ReportedJobsDetail { get; set; }

		/// <summary>
		/// Gets or sets the job type detail.
		/// </summary>
		/// <value>
		/// The job type detail.
		/// </value>
		public List<WorkOrderJobTypeDetail> JobTypeDetail { get; set; }

		/// <summary>
		/// Gets or sets the planned maintenance header detail.
		/// </summary>
		/// <value>
		/// The planned maintenance header detail.
		/// </value>
		public PMSHeaderDetail PlannedMaintenanceHeaderDetail { get; set; }

		/// <summary>
		/// Gets or sets the pending component request count.
		/// </summary>
		/// <value>
		/// The pending component request count.
		/// </value>
		public int? PendingComponentRequestCount { get; set; }

		/// <summary>
		/// Gets or sets the pending job request count.
		/// </summary>
		/// <value>
		/// The pending job request count.
		/// </value>
		public int? PendingJobRequestCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is in non trading period.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is in non trading period; otherwise, <c>false</c>.
		/// </value>
		public bool IsInNonTradingPeriod { get; set; }
	}
}
