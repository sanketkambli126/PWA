namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// Management Vessel Detail ViewModel
    /// </summary>
    public class ManagementVesselDetailViewModel
    {
        /// <summary>
        /// Gets or sets the vessel URL.
        /// </summary>
        /// <value>
        /// The vessel URL.
        /// </value>
        public string VesselURL { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the accounting company identifier.
        /// </summary>
        /// <value>
        /// The accounting company identifier.
        /// </value>
        public string AccountingCompanyId { get; set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text
        {
            get
            {
                if (!string.IsNullOrEmpty(VesselName))
                {
                    return VesselName;
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id
        {
            get
            {
                return AccountingCompanyId;
            }
        }
    }
}
