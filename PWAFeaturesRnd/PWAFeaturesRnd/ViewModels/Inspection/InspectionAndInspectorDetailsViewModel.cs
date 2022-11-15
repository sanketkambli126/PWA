using System;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// ViewModel for Inspection details and Inspector Details
    /// </summary>
    public class InspectionAndInspectorDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

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
        /// Gets or sets the where.
        /// </summary>
        /// <value>
        /// The where.
        /// </value>
        public string Where { get; set; }

        /// <summary>
        /// Gets or sets the next due.
        /// </summary>
        /// <value>
        /// The next due.
        /// </value>
        public DateTime? NextDue { get; set; }

        //Inspector details

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public string Entity { get; set; }

        /// <summary>
        /// Gets or sets the inspector.
        /// </summary>
        /// <value>
        /// The inspector.
        /// </value>
        public string Inspector { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>
        /// The company.
        /// </value>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the detained days.
        /// </summary>
        /// <value>
        /// The detained days.
        /// </value>
        public float? DetainedDays { get; set; }

        /// <summary>
        /// Gets or sets the inspection type identifier.
        /// </summary>
        /// <value>
        /// The inspection type identifier.
        /// </value>
        public string InspectionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the inspection.
        /// </summary>
        /// <value>
        /// The name of the inspection.
        /// </value>
        public string InspectionName { get; set; }

        /// <summary>
        /// Gets or sets the required document types.
        /// </summary>
        /// <value>
        /// The required document types.
        /// </value>
        public string RequiredDocumentTypes { get; set; }
    }
}
