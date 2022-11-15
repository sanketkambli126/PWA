using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Oil Spill Water Response
    /// </summary>
    public class OilSpillWaterResponse
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
        /// Gets or sets the coy identifier.
        /// </summary>
        /// <value>
        /// The coy identifier.
        /// </value>
        public string CoyId { get; set; }

        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the imr ship reference no.
        /// </summary>
        /// <value>
        /// The imr ship reference no.
        /// </value>
        public string IMRShipRefNo { get; set; }

        /// <summary>
        /// Gets or sets the imr inc date.
        /// </summary>
        /// <value>
        /// The imr inc date.
        /// </value>
        public DateTime? IMRIncDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the inc.
        /// </summary>
        /// <value>
        /// The type of the inc.
        /// </value>
        public string IncType { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the location of vessel.
        /// </summary>
        /// <value>
        /// The location of vessel.
        /// </value>
        public string LocationOfVessel { get; set; }

        /// <summary>
        /// Gets or sets the imr oil LTRS sea.
        /// </summary>
        /// <value>
        /// The imr oil LTRS sea.
        /// </value>
        public decimal ImrOilLtrsSea { get; set; }
    }
}
