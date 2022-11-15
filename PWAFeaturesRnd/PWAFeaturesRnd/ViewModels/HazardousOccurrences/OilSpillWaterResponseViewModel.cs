using System;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// Oil Spill Water Response ViewModel
    /// </summary>
    public class OilSpillWaterResponseViewModel
    {
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the incident date.
        /// </summary>
        /// <value>
        /// The incident date.
        /// </value>
        public DateTime? IncidentDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the incident.
        /// </summary>
        /// <value>
        /// The type of the incident.
        /// </value>
        public string IncidentType { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the loc of vessel.
        /// </summary>
        /// <value>
        /// The loc of vessel.
        /// </value>
        public string LocOfVessel { get; set; }

        /// <summary>
        /// Gets or sets the quantity spilled.
        /// </summary>
        /// <value>
        /// The quantity spilled.
        /// </value>
        public decimal QuantitySpilled { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the ship reference no.
        /// </summary>
        /// <value>
        /// The ship reference no.
        /// </value>
        public string ShipRefNo { get; set; }

        /// <summary>
        /// Gets or sets the haz occ request URL.
        /// </summary>
        /// <value>
        /// The haz occ request URL.
        /// </value>
        public string HazOccRequestURL { get; set; }
    }
}
