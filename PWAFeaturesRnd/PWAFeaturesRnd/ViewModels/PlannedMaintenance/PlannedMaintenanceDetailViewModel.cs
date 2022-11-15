namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Planned Maintenance Detail ViewModel
    /// </summary>
    public class PlannedMaintenanceDetailViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the planned maintenance request details URL.
        /// </summary>
        /// <value>
        /// The planned maintenance request details URL.
        /// </value>
        public string PlannedMaintenanceRequestDetailsURL { get; set; }

        /// <summary>
        /// Gets or sets the component identifier.
        /// </summary>
        /// <value>
        /// The component identifier.
        /// </value>
        public string ComponentId { get; set; }

        /// <summary>
        /// Gets or sets the planned maintainance list URL.
        /// </summary>
        /// <value>
        /// The planned maintainance list URL.
        /// </value>
        public string PlannedMaintainanceListUrl { get; set; }

        /// <summary>
        /// Gets or sets the pwo identifier.
        /// </summary>
        /// <value>
        /// The pwo identifier.
        /// </value>
        public string PwoId { get; set; }

        /// <summary>
        /// Gets or sets the PWH identifier.
        /// </summary>
        /// <value>
        /// The PWH identifier.
        /// </value>
        public string PwhId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is navigated from done.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is navigated from done; otherwise, <c>false</c>.
        /// </value>
        public bool IsNavigatedFromDone { get; set; }

        /// <summary>
        /// Gets or sets the encrypted system area identifier.
        /// </summary>
        /// <value>
        /// The encrypted system area identifier.
        /// </value>
        public string EncryptedSystemAreaId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is swo.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is swo; otherwise, <c>false</c>.
        /// </value>
        public bool IsSWO { get; set; }

		/// <summary>
		/// Gets or sets the message details json.
		/// </summary>
		/// <value>
		/// The message details json.
		/// </value>
		public string MessageDetailsJSON { get; set; }
	}
}
