using System;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// Psc Detention ViewModel
    /// </summary>
    public class PscDetentionViewModel
    {
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
        /// Gets or sets the detention date.
        /// </summary>
        /// <value>
        /// The detention date.
        /// </value>
        public DateTime? DetentionDate { get; set; }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public string Port { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the days detained.
        /// </summary>
        /// <value>
        /// The days detained.
        /// </value>
        public int DaysDetained { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the encrypted finding URL.
        /// </summary>
        /// <value>
        /// The encrypted finding URL.
        /// </value>
        public string EncryptedFindingURL { get; set; }
    }
}
