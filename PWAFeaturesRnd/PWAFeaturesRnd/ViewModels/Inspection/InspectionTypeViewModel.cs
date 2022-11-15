namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// ViewModel for holding Inspection Type details
    /// </summary>
    public class InspectionTypeViewModel
    {
        /// <summary>
        /// Gets or sets the VRP abbr.
        /// </summary>
        /// <value>
        /// The VRP abbr.
        /// </value>
        public string VRP_Abbr { get; set; }
        /// <summary>
        /// Gets or sets the default interval.
        /// </summary>
        /// <value>
        /// The default interval.
        /// </value>
        public int DefaultInterval { get; set; }

		/// <summary>
		/// Gets or sets the type of the inspection.
		/// </summary>
		/// <value>
		/// The type of the inspection.
		/// </value>
		public string InspectionType { get; set; }
        /// <summary>
        /// Gets or sets the VRP applicable for.
        /// </summary>
        /// <value>
        /// The VRP applicable for.
        /// </value>
        public string VrpApplicableFor { get; set; }

		/// <summary>
		/// Gets or sets the inspection type identifier.
		/// </summary>
		/// <value>
		/// The inspection type identifier.
		/// </value>
		public string InspectiontypeId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is internal.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is internal; otherwise, <c>false</c>.
		/// </value>
		public bool IsInternalType { get; set; }
        /// <summary>
        /// Gets or sets the VRP abbr.
        /// </summary>
        /// <value>
        /// The VRP abbr.
        /// </value>
        public string VrpAbbr { get; set; }

        /// <summary>
        /// Gets or sets the VRP other1.
        /// </summary>
        /// <value>
        /// The VRP other1.
        /// </value>
        public int VrpOther1 { get; set; }
        /// <summary>
        /// Gets or sets the VRP other2.
        /// </summary>
        /// <value>
        /// The VRP other2.
        /// </value>
        public int VrpOther2 { get; set; }

        /// <summary>
        /// Gets or sets the VRP type3.
        /// </summary>
        /// <value>
        /// The VRP type3.
        /// </value>
        public string VrpType3 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is next due date editable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is next due date editable; otherwise, <c>false</c>.
        /// </value>
        public bool IsNextDueDateEditable { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is required next due date.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is required next due date; otherwise, <c>false</c>.
        /// </value>
        public bool IsRequiredNextDueDate { get; set; }

        /// <summary>
        /// Gets or sets the initial interval.
        /// </summary>
        /// <value>
        /// The initial interval.
        /// </value>
        public int InitialInterval { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has mapped questions.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has mapped questions; otherwise, <c>false</c>.
        /// </value>
        public bool HasMappedQuestions { get; set; }
    }
}
