using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Haz Occ Summary Request
    /// </summary>
    public class HazOccSummaryRequest
    {
        /// <summary>
        /// Gets or sets the accident start date.
        /// </summary>
        /// <value>
        /// The accident start date.
        /// </value>
        public DateTime? AccidentStartDate { get; set; }

        /// <summary>
        /// Gets or sets the accident end date.
        /// </summary>
        /// <value>
        /// The accident end date.
        /// </value>
        public DateTime? AccidentEndDate { get; set; }

        /// <summary>
        /// Gets or sets the incident start date.
        /// </summary>
        /// <value>
        /// The incident start date.
        /// </value>
        public DateTime? IncidentStartDate { get; set; }

        /// <summary>
        /// Gets or sets the incident end date.
        /// </summary>
        /// <value>
        /// The incident end date.
        /// </value>
        public DateTime? IncidentEndDate { get; set; }

        /// <summary>
        /// Gets or sets the accident priority limit.
        /// </summary>
        /// <value>
        /// The accident priority limit.
        /// </value>
        public int AccidentPriorityLimit { get; set; }

        /// <summary>
        /// Gets or sets the incident priority limit.
        /// </summary>
        /// <value>
        /// The incident priority limit.
        /// </value>
        public int IncidentPriorityLimit { get; set; }

        /// <summary>
        /// Gets or sets the uauc priority group limit.
        /// </summary>
        /// <value>
        /// The uauc priority group limit.
        /// </value>
        public int UAUCPriorityGroupLimit { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }
    }
}
