namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// Defect Summary Response
    /// </summary>
    public class DefectSummaryResponse
    {
        /// <summary>
        /// Gets or sets the close defect count.
        /// </summary>
        /// <value>
        /// The close defect count.
        /// </value>
        public int? CloseDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the off hire count.
        /// </summary>
        /// <value>
        /// The off hire count.
        /// </value>
        public int? OffHireCount { get; set; }

        /// <summary>
        /// Gets or sets the technical defect count.
        /// </summary>
        /// <value>
        /// The technical defect count.
        /// </value>
        public int? TechnicalDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the open defect count.
        /// </summary>
        /// <value>
        /// The open defect count.
        /// </value>
        public int? OpenDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the layover count.
        /// </summary>
        /// <value>
        /// The layover count.
        /// </value>
        public int? LayoverCount { get; set; }

        /// <summary>
        /// Gets or sets the guarantee claim count.
        /// </summary>
        /// <value>
        /// The guarantee claim count.
        /// </value>
        public int? GuaranteeClaimCount { get; set; }

        /// <summary>
        /// Gets or sets the overdue count.
        /// </summary>
        /// <value>
        /// The overdue count.
        /// </value>
        public int? OverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the drydock count.
        /// </summary>
        /// <value>
        /// The drydock count.
        /// </value>
        public int? DrydockCount { get; set; }

        /// <summary>
        ///  Gets or sets the awaiting spares count
        /// </summary>
        /// /// <value>
        /// The awaitingsparescount
        /// </value>
        public int? AwaitingSparesCount { get; set; }


        /// <summary>
        /// Gets or sets the off hire priority.
        /// </summary>
        /// <value>
        /// The off hire priority.
        /// </value>
        public int OffHirePriority { get; set; }

        /// <summary>
        /// Gets or sets the open defect priority.
        /// </summary>
        /// <value>
        /// The open defect priority.
        /// </value>
        public int OpenDefectPriority { get; set; }

        /// <summary>
		/// Gets or sets the overdue priority.
		/// </summary>
		/// <value>
		/// The overdue priority.
		/// </value>OpenDefectCount
		public int OverduePriority { get; set; }

        /// <summary>
		/// Gets or sets the awaiting spares priority.
		/// </summary>
		/// <value>
		/// The awaiting spares priority.
		/// </value>
		public int AwaitingSparesPriority { get; set; }

        /// <summary>
		/// Gets or sets the completed defects count.
		/// </summary>
		/// <value>
		/// The completed defects count.
		/// </value>
		public int CompletedDefectsCount { get; set; }
    }
}
