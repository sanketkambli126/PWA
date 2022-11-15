using System;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Overdue Inspection Response
    /// </summary>
    public class OverdueInspectionResponse
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
        /// Gets or sets the inspection type desc.
        /// </summary>
        /// <value>
        /// The inspection type desc.
        /// </value>
        public string InspectionTypeDesc { get; set; }

        /// <summary>
        /// Gets or sets the detention date.
        /// </summary>
        /// <value>
        /// The detention date.
        /// </value>
        public DateTime? DetentionDate { get; set; }

        /// <summary>
        /// Gets or sets the done date.
        /// </summary>
        /// <value>
        /// The done date.
        /// </value>
        public DateTime? DoneDate { get; set; }

        /// <summary>
        /// Gets or sets the next due date.
        /// </summary>
        /// <value>
        /// The next due date.
        /// </value>
        public DateTime? NextDueDate { get; set; }
    }
}
