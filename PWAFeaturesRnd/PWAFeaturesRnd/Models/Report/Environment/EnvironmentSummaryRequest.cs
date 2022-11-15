using System;

namespace PWAFeaturesRnd.Models.Report.Environment
{

    /// <summary>
    /// Environment Summary Request
    /// </summary>
    public class EnvironmentSummaryRequest
    {
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

        /// <summary>
        /// Gets or sets the oil spill start date.
        /// </summary>
        /// <value>
        /// The oil spill start date.
        /// </value>
        public DateTime? OilSpillStartDate { get; set; }

        /// <summary>
        /// Gets or sets the oil spill end date.
        /// </summary>
        /// <value>
        /// The oil spill end date.
        /// </value>
        public DateTime? OilSpillEndDate { get; set; }

        /// <summary>
        /// Gets or sets the bilge start date.
        /// </summary>
        /// <value>
        /// The bilge start date.
        /// </value>
        public DateTime? BilgeStartDate { get; set; }

        /// <summary>
        /// Gets or sets the bilge end date.
        /// </summary>
        /// <value>
        /// The bilge end date.
        /// </value>
        public DateTime? BilgeEndDate { get; set; }

        /// <summary>
        /// Gets or sets the ae utilisation start date.
        /// </summary>
        /// <value>
        /// The ae utilisation start date.
        /// </value>
        public DateTime? AEUtilisationStartDate { get; set; }

        /// <summary>
        /// Gets or sets the ae utilisation end date.
        /// </summary>
        /// <value>
        /// The ae utilisation end date.
        /// </value>
        public DateTime? AEUtilisationEndDate { get; set; }
    }
}
