using System;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Psc Deficiencies Response
    /// </summary>
    public class PscDeficienciesResponse
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
        /// Gets or sets the inspection identifier.
        /// </summary>
        /// <value>
        /// The inspection identifier.
        /// </value>
        public string InspectionId { get; set; }

        /// <summary>
        /// Gets or sets the inspection type identifier.
        /// </summary>
        /// <value>
        /// The inspection type identifier.
        /// </value>
        public string InspectionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the detention date.
        /// </summary>
        /// <value>
        /// The detention date.
        /// </value>
        public DateTime? InspectionDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the where location.
        /// </summary>
        /// <value>
        /// The where location.
        /// </value>
        public string WhereLocation { get; set; }

        /// <summary>
        /// Gets or sets the finding count.
        /// </summary>
        /// <value>
        /// The finding count.
        /// </value>
        public int? FindingCount { get; set; }

        /// <summary>
        /// Gets or sets the name of the inspector.
        /// </summary>
        /// <value>
        /// The name of the inspector.
        /// </value>
        public string InspectorName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is detained.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is detained; otherwise, <c>false</c>.
        /// </value>
        public bool IsDetained { get; set; }

        /// <summary>
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }

        /// <summary>
        /// Gets or sets the inspection type desc.
        /// </summary>
        /// <value>
        /// The inspection type desc.
        /// </value>
        public string InspectionTypeDesc { get; set; }
    }
}
