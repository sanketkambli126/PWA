using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Serious Incidents Request
    /// </summary>
    public class SeriousIncidentsRequest
    {
        /// <summary>
        /// Gets or sets the incident start date.
        /// </summary>
        /// <value>
        /// The incident start date.
        /// </value>
        public DateTime IncidentStartDate { get; set; }

        /// <summary>
        /// Gets or sets the incident end date.
        /// </summary>
        /// <value>
        /// The incident end date.
        /// </value>
        public DateTime IncidentEndDate { get; set; }

        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>
        public string FleetId { get; set; }

        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public string MenuType { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

    }
}
