namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectSummaryResponseViewModel
    {
        #region Stats Properties

        /// <summary>
        /// Gets or sets the close defect count.
        /// </summary>
        /// <value>
        /// The close defect count.
        /// </value>
        public int CloseDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the off hire count.
        /// </summary>
        /// <value>
        /// The off hire count.
        /// </value>
        public int OffHireCount { get; set; }

        /// <summary>
        /// Gets or sets the technical defect count.
        /// </summary>
        /// <value>
        /// The technical defect count.
        /// </value>
        public int TechnicalDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the open defect count.
        /// </summary>
        /// <value>
        /// The open defect count.
        /// </value>
        public int OpenDefectCount { get; set; }

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
        public int GuaranteeClaimCount { get; set; }

        /// <summary>
        /// Gets or sets the overdue count.
        /// </summary>
        /// <value>
        /// The overdue count.
        /// </value>
        public int OverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the drydock count.
        /// </summary>
        /// <value>
        /// The drydock count.
        /// </value>
        public int DrydockCount { get; set; }

        /// <summary>
        ///  Gets or sets the awaiting spares count
        /// </summary>
        /// /// <value>
        /// The awaitingsparescount
        /// </value>
        public int AwaitingSparesCount { get; set; }

        /// <summary>
		/// Gets or sets the completed defects count.
		/// </summary>
		/// <value>
		/// The completed defects count.
		/// </value>
		public int CompletedDefectsCount { get; set; }

        #endregion

        #region Navigation Properties

        /// <summary>
        /// Gets or sets the closed defect navigation.
        /// </summary>
        /// <value>
        /// The closed defect navigation.
        /// </value>
        public string ClosedDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the open defect navigation.
        /// </summary>
        /// <value>
        /// The open defect navigation.
        /// </value>
        public string OpenDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the overdue defect navigation.
        /// </summary>
        /// <value>
        /// The overdue defect navigation.
        /// </value>
        public string OverdueDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the off hire defect navigation.
        /// </summary>
        /// <value>
        /// The off hire defect navigation.
        /// </value>
        public string OffHireDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the lay over defect navigation.
        /// </summary>
        /// <value>
        /// The lay over defect navigation.
        /// </value>
        public string LayOverDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the drydock defect navigation.
        /// </summary>
        /// <value>
        /// The drydock defect navigation.
        /// </value>
        public string DrydockDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the technical defect navigation.
        /// </summary>
        /// <value>
        /// The technical defect navigation.
        /// </value>
        public string TechnicalDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the guarantee claim defect navigation.
        /// </summary>
        /// <value>
        /// The guarantee claim defect navigation.
        /// </value>
        public string GuaranteeClaimDefectNavigation { get; set; }

        /// <summary>
        /// Gets or sets the awaiting spares navigation.
        /// </summary>
        /// <value>
        /// The awaiting spares navigation.
        /// </value>
        public string AwaitingSparesNavigation { get; set; }

		/// <summary>
		/// Gets or sets the completed defects navigation.
		/// </summary>
		/// <value>
		/// The completed defects navigation.
		/// </value>
		public string CompletedDefectsNavigation { get; set; }

		#endregion

		#region Priority Properties

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
        
        #endregion
    }
}
