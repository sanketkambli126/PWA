using System;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// The incidentvisit viewmodel
    /// </summary>
    public class IncidentVisitViewModel
    {
        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>
        /// The deleted.
        /// </value>
        public Nullable<System.Boolean> Deleted { get; set; }

        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public System.String ImrId { get; set; }

        /// <summary>
        /// Gets or sets the imv identifier.
        /// </summary>
        /// <value>
        /// The imv identifier.
        /// </value>
        public System.String ImvId { get; set; }

        /// <summary>
        /// Gets or sets the visit no.
        /// </summary>
        /// <value>
        /// The visit no.
        /// </value>
        public Nullable<System.Int16> VisitNo { get; set; }

        /// <summary>
        /// Gets or sets the visit on.
        /// </summary>
        /// <value>
        /// The visit on.
        /// </value>
        public Nullable<System.DateTime> VisitOn { get; set; }
    }
}
