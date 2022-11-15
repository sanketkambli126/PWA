using System;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// 
	/// </summary>
	public class PlannedMaintenanceSummary
	{
		/// <summary>
		/// Gets or sets the critical wo done count.
		/// </summary>
		/// <value>
		/// The critical wo done count.
		/// </value>
		public int CriticalWODoneCount { get; set; }

		/// <summary>
		/// Gets or sets the critical wo due count.
		/// </summary>
		/// <value>
		/// The critical wo due count.
		/// </value>
		public int CriticalWODueCount { get; set; }

		/// <summary>
		/// Gets or sets the critical wo overdue count.
		/// </summary>
		/// <value>
		/// The critical wo overdue count.
		/// </value>
		public int CriticalWOOverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the critical wo prior overdue count.
		/// </summary>
		/// <value>
		/// The critical wo prior overdue count.
		/// </value>
		public int CriticalWOPriorOverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the critical wo overdue percentage.
		/// </summary>
		/// <value>
		/// The critical wo overdue percentage.
		/// </value>
		public int CriticalWOOverduePercentage { get; set; }

		/// <summary>
		/// Gets or sets the non critical wo done count.
		/// </summary>
		/// <value>
		/// The non critical wo done count.
		/// </value>
		public int NonCriticalWODoneCount { get; set; }

		/// <summary>
		/// Gets or sets the non critical wo due count.
		/// </summary>
		/// <value>
		/// The non critical wo due count.
		/// </value>
		public int NonCriticalWODueCount { get; set; }

		/// <summary>
		/// Gets or sets the non critical wo overdue count.
		/// </summary>
		/// <value>
		/// The non critical wo overdue count.
		/// </value>
		public int NonCriticalWOOverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the non critical wo prior overdue count.
		/// </summary>
		/// <value>
		/// The non critical wo prior overdue count.
		/// </value>
		public int NonCriticalWOPriorOverdueCount { get; set; }

		/// <summary>
		/// Gets or sets the non critical wo overdue percentage.
		/// </summary>
		/// <value>
		/// The non critical wo overdue percentage.
		/// </value>
		public int NonCriticalWOOverduePercentage { get; set; }

		/// <summary>
		/// Gets or sets the ships wo planned count.
		/// </summary>
		/// <value>
		/// The ships wo planned count.
		/// </value>
		public int ShipsWOPlannedCount { get; set; }

		/// <summary>
		/// Gets or sets the ships wo done count.
		/// </summary>
		/// <value>
		/// The ships wo done count.
		/// </value>
		public int ShipsWODoneCount { get; set; }

		/// <summary>
		/// Gets or sets the wo awaiting office approval count.
		/// </summary>
		/// <value>
		/// The wo awaiting office approval count.
		/// </value>
		public int WOAwaitingOfficeApprovalCount { get; set; }

		/// <summary>
		/// Gets or sets the wo awaiting vessel approval count.
		/// </summary>
		/// <value>
		/// The wo awaiting vessel approval count.
		/// </value>
		public int WOAwaitingVesselApprovalCount { get; set; }

		/// <summary>
		/// Gets or sets the calendar based wo reschedule count.
		/// </summary>
		/// <value>
		/// The calendar based wo reschedule count.
		/// </value>
		public int CalendarBasedWORescheduleCount { get; set; }

		/// <summary>
		/// Gets or sets the running HRS wo reschedule count.
		/// </summary>
		/// <value>
		/// The running HRS wo reschedule count.
		/// </value>
		public int RunningHrsWORescheduleCount { get; set; }

		/// <summary>
		/// Gets or sets the running hours last updated date.
		/// </summary>
		/// <value>
		/// The running hours last updated date.
		/// </value>
		public DateTime? RunningHoursLastUpdatedDate { get; set; }

		/// <summary>
		/// Gets or sets the spare below tech minimum count.
		/// </summary>
		/// <value>
		/// The spare below tech minimum count.
		/// </value>
		public int SpareBelowTechMinCount { get; set; }

		/// <summary>
		/// Gets or sets the spare below opr minimum count.
		/// </summary>
		/// <value>
		/// The spare below opr minimum count.
		/// </value>
		public int SpareBelowOprMinCount { get; set; }
	}
}
