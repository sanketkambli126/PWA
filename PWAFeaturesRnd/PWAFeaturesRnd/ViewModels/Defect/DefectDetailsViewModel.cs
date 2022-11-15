namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectDetailsViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the encrypted dwo identifier.
        /// </summary>
        /// <value>
        /// The encrypted dwo identifier.
        /// </value>
        public string EncryptedDWOId { get; set; }

        /// <summary>
        /// Gets or sets the defect work order identifier.
        /// </summary>
        /// <value>
        /// The defect work order identifier.
        /// </value>
        public string DefectWorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public string DueDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is completed or closed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is completed or closed; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompletedOrClosed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is guarantee claim code.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is guarantee claim code; otherwise, <c>false</c>.
        /// </value>
        public bool IsGuaranteeClaimCode { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is status completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is status completed; otherwise, <c>false</c>.
        /// </value>
        public bool IsStatusCompleted { get; set; }

        /// <summary>
		/// Gets or sets the active mobile tab class.
		/// </summary>
		/// <value>
		/// The active mobile tab class.
		/// </value>
		public string ActiveMobileTabClass { get; set; }
    }
}
