namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
    /// <summary>
    /// PMS Dashboard Summary Response
    /// </summary>
    public class PMSDashboardSummaryResponse
    {
        /// <summary>
        /// Gets or sets the work order due count.
        /// </summary>
        /// <value>
        /// The work order due count.
        /// </value>
        public int WorkOrderDueCount { get; set; }

        /// <summary>
        /// Gets or sets the work order overdue count.
        /// </summary>
        /// <value>
        /// The work order overdue count.
        /// </value>
        public int WorkOrderOverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the wo prior month overdue count.
        /// </summary>
        /// <value>
        /// The wo prior month overdue count.
        /// </value>
        public int WOPriorMonthOverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the work order done count.
        /// </summary>
        /// <value>
        /// The work order done count.
        /// </value>
        public int WorkOrderDoneCount { get; set; }

        /// <summary>
        /// Gets or sets the work order overdue percentage.
        /// </summary>
        /// <value>
        /// The work order overdue percentage.
        /// </value>
        public int WorkOrderOverduePercentage { get; set; }

        /// <summary>
        /// Gets or sets the critical component wo due count.
        /// </summary>
        /// <value>
        /// The critical component wo due count.
        /// </value>
        public int CriticalComponentWODueCount { get; set; }

        /// <summary>
        /// Gets or sets the critical component wo overdue count.
        /// </summary>
        /// <value>
        /// The critical component wo overdue count.
        /// </value>
        public int CriticalComponentWOOverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the critical component wo prior month overdue count.
        /// </summary>
        /// <value>
        /// The critical component wo prior month overdue count.
        /// </value>
        public int CriticalComponentWOPriorMonthOverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the critical component wo done count.
        /// </summary>
        /// <value>
        /// The critical component wo done count.
        /// </value>
        public int CriticalComponentWODoneCount { get; set; }

        /// <summary>
        /// Gets or sets the critical component wo overdue percentage.
        /// </summary>
        /// <value>
        /// The critical component wo overdue percentage.
        /// </value>
        public int CriticalComponentWOOverduePercentage { get; set; }

        /// <summary>
        /// Gets or sets the ships work order planned count.
        /// </summary>
        /// <value>
        /// The ships work order planned count.
        /// </value>
        public int ShipsWorkOrderPlannedCount { get; set; }

        /// <summary>
        /// Gets or sets the ships work order done count.
        /// </summary>
        /// <value>
        /// The ships work order done count.
        /// </value>
        public int ShipsWorkOrderDoneCount { get; set; }

        /// <summary>
        /// Gets or sets the due priority.
        /// </summary>
        /// <value>
        /// The due priority.
        /// </value>
        public int DuePriority { get; set; }

        /// <summary>
        /// Gets or sets the prior month overdue priority.
        /// </summary>
        /// <value>
        /// The prior month overdue priority.
        /// </value>
        public int PriorMonthOverduePriority { get; set; }

        /// <summary>
        /// Gets or sets the critical due priority.
        /// </summary>
        /// <value>
        /// The critical due priority.
        /// </value>
        public int CriticalDuePriority { get; set; }

        /// <summary>
        /// Gets or sets the prior month critical overdue priority.
        /// </summary>
        /// <value>
        /// The prior month critical overdue priority.
        /// </value>
        public int PriorMonthCriticalOverduePriority { get; set; }

        /// <summary>
        /// Gets or sets the critical wo.
        /// </summary>
        /// <value>
        /// The critical wo.
        /// </value>
        public int CriticalWO { get; set; }

        /// <summary>
        /// Gets or sets the planned for wo.
        /// </summary>
        /// <value>
        /// The planned for wo.
        /// </value>
        public int PlannedForWO { get; set; }

        /// <summary>
        /// Gets or sets the req reschedule wo.
        /// </summary>
        /// <value>
        /// The req reschedule wo.
        /// </value>
        public int ReqRescheduleWO { get; set; }

        /// <summary>
        /// Gets or sets the schedule wo.
        /// </summary>
        /// <value>
        /// The schedule wo.
        /// </value>
        public int ScheduleWO { get; set; }

		/// <summary>
		/// Gets or sets the wo office approval count.
		/// </summary>
		/// <value>
		/// The wo office approval count.
		/// </value>
		public int WOOfficeApprovalCount { get; set; }
    }
}
