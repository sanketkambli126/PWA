namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// 
    /// </summary>
    public class PMSDashboardSummaryViewModel
    {
        #region Count Poperties

        /// <summary>
        /// Gets or sets the due.
        /// </summary>
        /// <value>
        /// The due.
        /// </value>
        public int Due { get; set; }

        /// <summary>
        /// Gets or sets the overdue.
        /// </summary>
        /// <value>
        /// The overdue.
        /// </value>
        public int Overdue { get; set; }

        /// <summary>
        /// Gets or sets the critical due.
        /// </summary>
        /// <value>
        /// The critical due.
        /// </value>
        public int CriticalDue { get; set; }

        /// <summary>
        /// Gets or sets the critical overdue.
        /// </summary>
        /// <value>
        /// The critical overdue.
        /// </value>
        public int CriticalOverdue { get; set; }
                
        /// <summary>
        /// Gets or sets the critical.
        /// </summary>
        /// <value>
        /// The critical.
        /// </value>
        public int Critical { get; set; }

        /// <summary>
        /// Gets or sets the planned for.
        /// </summary>
        /// <value>
        /// The planned for.
        /// </value>
        public int PlannedFor { get; set; }

        /// <summary>
        /// Gets or sets the req reschedule.
        /// </summary>
        /// <value>
        /// The req reschedule.
        /// </value>
        public int ReqReschedule { get; set; }

		/// <summary>
		/// Gets or sets the completed wo.
		/// </summary>
		/// <value>
		/// The completed wo.
		/// </value>
		public int CompletedWO { get; set; }

        #endregion

        #region KPI Priority

        /// <summary>
        /// Gets or sets the due priority.
        /// </summary>
        /// <value>
        /// The due priority.
        /// </value>
        public int DuePriority { get; set; }

        /// <summary>
        /// Gets or sets the overdue priority.
        /// </summary>
        /// <value>
        /// The overdue priority.
        /// </value>
        public int OverduePriority { get; set; }

        /// <summary>
        /// Gets or sets the critical due priority.
        /// </summary>
        /// <value>
        /// The critical due priority.
        /// </value>
        public int CriticalDuePriority { get; set; }

        /// <summary>
        /// Gets or sets the critical overdue priority.
        /// </summary>
        /// <value>
        /// The critical overdue priority.
        /// </value>
        public int CriticalOverduePriority { get; set; }

        #endregion

        #region Navigation URL

        /// <summary>
        /// Gets or sets the due URL.
        /// </summary>
        /// <value>
        /// The due URL.
        /// </value>
        public string DueURL { get; set; }

        /// <summary>
        /// Gets or sets the overdue URL.
        /// </summary>
        /// <value>
        /// The overdue URL.
        /// </value>
        public string OverdueURL { get; set; }

        /// <summary>
        /// Gets or sets the critical due URL.
        /// </summary>
        /// <value>
        /// The critical due URL.
        /// </value>
        public string CriticalDueURL { get; set; }

        /// <summary>
        /// Gets or sets the critical overdue URL.
        /// </summary>
        /// <value>
        /// The critical overdue URL.
        /// </value>
        public string CriticalOverdueURL { get; set; }
                               
        /// <summary>
        /// Gets or sets the critical URL.
        /// </summary>
        /// <value>
        /// The critical URL.
        /// </value>
        public string CriticalURL { get; set; }

        /// <summary>
        /// Gets or sets the planned for URL.
        /// </summary>
        /// <value>
        /// The planned for URL.
        /// </value>
        public string PlannedForURL { get; set; }

        /// <summary>
        /// Gets or sets the req reschedule URL.
        /// </summary>
        /// <value>
        /// The req reschedule URL.
        /// </value>
        public string ReqRescheduleURL { get; set; }

        /// <summary>
        /// Gets or sets all request URL.
        /// </summary>
        /// <value>
        /// All request URL.
        /// </value>
        public string AllRequestURL { get; set; }

		/// <summary>
		/// Gets or sets the completed URL.
		/// </summary>
		/// <value>
		/// The completed URL.
		/// </value>
		public string CompletedUrl { get; set; }

		#endregion
	}
}
