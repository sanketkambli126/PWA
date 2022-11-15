using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// The port event rob summary request
    /// </summary>
    public class PortEventRobSummaryRequest
    {

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the PSF identifier.
        /// </summary>
        /// <value>
        /// The PSF identifier.
        /// </value>
        public string PsfId { get; set; }

        /// <summary>
        /// Gets or sets the PPF identifier.
        /// </summary>
        /// <value>
        /// The PPF identifier.
        /// </value>
        public string PpfId { get; set; }

        /// <summary>
        /// Gets or sets the latest event date.
        /// </summary>
        /// <value>
        /// The latest event date.
        /// </value>
        public DateTime? LatestEventDate { get; set; }
    }
}
