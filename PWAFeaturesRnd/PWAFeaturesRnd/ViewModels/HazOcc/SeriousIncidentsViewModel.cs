using System;

namespace PWAFeaturesRnd.ViewModels.HazOcc
{
    /// <summary>
    /// Serious Incidents View Model
    /// </summary>
    public class SeriousIncidentsViewModel
    {
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the ship reference no.
        /// </summary>
        /// <value>
        /// The ship reference no.
        /// </value>
        public string ShipRefNo { get; set; }

        /// <summary>
        /// Gets or sets the occurrance date.
        /// </summary>
        /// <value>
        /// The occurrance date.
        /// </value>
        public DateTime OccurranceDate { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the classification.
        /// </summary>
        /// <value>
        /// The classification.
        /// </value>
        public string Classification { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the haz occ details URL data.
        /// </summary>
        /// <value>
        /// The haz occ details URL data.
        /// </value>
        public string HazOccDetailsUrlData { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }
    }
}
