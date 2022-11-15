namespace PWAFeaturesRnd.ViewModels.JSA
{
    public class JSADetailsViewModel : BaseViewModel
    {

        /// <summary>
        /// Gets or sets the reference no.
        /// </summary>
        /// <value>
        /// The reference no.
        /// </value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the encrypted job identifier.
        /// </summary>
        /// <value>
        /// The encrypted job identifier.
        /// </value>
        public string EncryptedJobId { get; set; }

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
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }

        /// <summary>
        /// Gets or sets the active mobile tab class.
        /// </summary>
        /// <value>
        /// The active mobile tab class.
        /// </value>
        public string ActiveMobileTabClass { get; set; }

        /// <summary>
        /// Gets or sets the Max Risk.
        /// </summary>
        /// <value>
        /// The risk level.
        /// </value>
        public string MaxRisk { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is approved; otherwise, <c>false</c>.
        /// </value>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rejected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rejected; otherwise, <c>false</c>.
        /// </value>
        public bool IsRejected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reopened.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is reopened; otherwise, <c>false</c>.
        /// </value>
        public bool IsReopened { get; set; }
    }
}
