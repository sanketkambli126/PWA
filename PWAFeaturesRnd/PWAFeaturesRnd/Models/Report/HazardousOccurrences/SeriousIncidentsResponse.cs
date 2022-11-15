using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Serious Incidents Response
    /// </summary>
    public class SeriousIncidentsResponse
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
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the ship reference no.
        /// </summary>
        /// <value>
        /// The ship reference no.
        /// </value>
        public string ShipRefNo { get; set; }

        /// <summary>
        /// Gets or sets the occurance date.
        /// </summary>
        /// <value>
        /// The occurance date.
        /// </value>
        public DateTime OccuranceDate { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the classification identifier.
        /// </summary>
        /// <value>
        /// The classification identifier.
        /// </value>
        public string ClassificationId { get; set; }

        /// <summary>
        /// Gets or sets the classification.
        /// </summary>
        /// <value>
        /// The classification.
        /// </value>
        public string Classification { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public string StatusId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the severity identifier.
        /// </summary>
        /// <value>
        /// The severity identifier.
        /// </value>
        public string SeverityId { get; set; }

        /// <summary>
        /// Gets or sets the severity description.
        /// </summary>
        /// <value>
        /// The severity description.
        /// </value>
        public string SeverityDescription { get; set; }

        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }
    }
}
