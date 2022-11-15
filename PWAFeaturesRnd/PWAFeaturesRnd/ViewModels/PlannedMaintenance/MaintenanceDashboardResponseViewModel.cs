namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Planned maintenance summary view model
    /// </summary>
    public class MaintenanceDashboardResponseViewModel
    {
        /// <summary>
        /// Gets or sets the due count.
        /// </summary>
        /// <value>
        /// The due count.
        /// </value>
        public int DueCount { get; set; }

        /// <summary>
        /// Gets or sets the over due current month count.
        /// </summary>
        /// <value>
        /// The over due current month count.
        /// </value>
        public int OverDueCurrentMonthCount { get; set; }

        /// <summary>
        /// Gets or sets the over due previous month count.
        /// </summary>
        /// <value>
        /// The over due previous month count.
        /// </value>
        public int OverDuePreviousMonthCount { get; set; }

        /// <summary>
        /// Gets or sets the done count.
        /// </summary>
        /// <value>
        /// The done count.
        /// </value>
        public int DoneCount { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public string Month { get; set; }

        /// <summary>
        /// Gets or sets all count.
        /// </summary>
        /// <value>
        /// All count.
        /// </value>
        public int AllCount { get; set; }
    }
}
