namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// Defect Dashboard Response View Model
    /// </summary>
    public class DefectDashboardResponseViewModel
    {
        /// <summary>
        /// Gets or sets all defect count.
        /// </summary>
        /// <value>
        /// All defect count.
        /// </value>
        public int AllDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the due count.
        /// </summary>
        /// <value>
        /// The due count.
        /// </value>
        public int DueCount { get; set; }

        /// <summary>
        /// Gets or sets the overdue count.
        /// </summary>
        /// <value>
        /// The overdue count.
        /// </value>
        public int OverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the open defect count.
        /// </summary>
        /// <value>
        /// The open defect count.
        /// </value>
		public int OpenDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the closed defect count.
        /// </summary>
        /// <value>
        /// The closed defect count.
        /// </value>
        public int ClosedDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the off hire required count.
        /// </summary>
        /// <value>
        /// The off hire required count.
        /// </value>
		public int OffHireRequiredCount { get; set; }

        /// <summary>
        /// Gets or sets the order count.
        /// </summary>
        /// <value>
        /// The order count.
        /// </value>
        public int OrderCount { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public string Month { get; set; }


        /// <summary>
        ///  Gets or sets the awaiting spares count
        /// </summary>
        /// /// <value>
        /// The awaitingsparescount
        /// </value>
        public int AwaitingSparesCount { get; set; }

        #region URL Properties

        /// <summary>
        /// Gets or sets all navigation.
        /// </summary>
        /// <value>
        /// All navigation.
        /// </value>
        public string AllNavigation { get; set; }

        /// <summary>
        /// Gets or sets the due navigation.
        /// </summary>
        /// <value>
        /// The due navigation.
        /// </value>
        public string DueNavigation { get; set; }

        /// <summary>
        /// Gets or sets the overdue navigation.
        /// </summary>
        /// <value>
        /// The overdue navigation.
        /// </value>
        public string OverdueNavigation { get; set; }

        /// <summary>
        /// Gets or sets the open defect navigation.
        /// </summary>
        /// <value>
        /// The open defect navigation.
        /// </value>
        public string OpenDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the closed defect navigation.
        /// </summary>
        /// <value>
        /// The closed defect navigation.
        /// </value>
        public string ClosedDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the off hire navigation.
        /// </summary>
        /// <value>
        /// The off hire navigation.
        /// </value>
        public string OffHireNavigation { get; set; }

        /// <summary>
        /// Gets or sets the order navigation.
        /// </summary>
        /// <value>
        /// The order navigation.
        /// </value>
        public string OrderNavigation { get; set; }

        #endregion
    }
}
