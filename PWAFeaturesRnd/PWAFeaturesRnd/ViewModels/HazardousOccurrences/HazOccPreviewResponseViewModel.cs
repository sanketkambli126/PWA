using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// HazOccPreviewResponseViewModel
    /// </summary>
    public class HazOccPreviewResponseViewModel
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
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the ship reference number.
        /// </summary>
        /// <value>
        /// The ship reference number.
        /// </value>
        public string ShipReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>
        /// The type identifier.
        /// </value>
        public string TypeId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

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
        /// Gets or sets the class identifier.
        /// </summary>
        /// <value>
        /// The class identifier.
        /// </value>
        public string ClassId { get; set; }

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        /// <value>
        /// The class.
        /// </value>
        public string Class { get; set; }

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
        /// Gets or sets the incident date.
        /// </summary>
        /// <value>
        /// The incident date.
        /// </value>
        public DateTime? IncidentDate { get; set; }

        /// <summary>
        /// Gets or sets the report date.
        /// </summary>
        /// <value>
        /// The report date.
        /// </value>
        public DateTime? ReportDate { get; set; }

        /// <summary>
        /// Gets or sets the actual severity.
        /// </summary>
        /// <value>
        /// The actual severity.
        /// </value>
        public string ActualSeverity { get; set; }

        /// <summary>
        /// Gets or sets the potential severity.
        /// </summary>
        /// <value>
        /// The potential severity.
        /// </value>
        public string PotentialSeverity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has parent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has parent; otherwise, <c>false</c>.
        /// </value>
        public bool HasParent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has child reports.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has child reports; otherwise, <c>false</c>.
        /// </value>
        public bool HasChildReports { get; set; }

        /// <summary>
        /// Gets or sets the type of the vessel.
        /// </summary>
        /// <value>
        /// The type of the vessel.
        /// </value>
        public string VesselType { get; set; }

        /// <summary>
        /// Gets or sets the MSQ comment date.
        /// </summary>
        /// <value>
        /// The MSQ comment date.
        /// </value>
        public DateTime? MsqCommentDate { get; set; }

        /// <summary>
        /// Gets or sets the fleet manager comment date.
        /// </summary>
        /// <value>
        /// The fleet manager comment date.
        /// </value>
        public DateTime? FleetManagerCommentDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the actual severity identifier.
        /// </summary>
        /// <value>
        /// The actual severity identifier.
        /// </value>
        public string ActualSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the potential severity identifier.
        /// </summary>
        /// <value>
        /// The potential severity identifier.
        /// </value>
        public string PotentialSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the mapped defect count.
        /// </summary>
        /// <value>
        /// The mapped defect count.
        /// </value>
        public int MappedDefectCount { get; set; }

        /// <summary>
        /// Gets or sets the reopen authorised by.
        /// </summary>
        /// <value>
        /// The reopen authorised by.
        /// </value>
        public string ReopenAuthorisedBy { get; set; }

        /// <summary>
        /// Gets or sets the reopen comments.
        /// </summary>
        /// <value>
        /// The reopen comments.
        /// </value>
        public string ReopenComments { get; set; }

        /// <summary>
        /// Gets or sets the closed date.
        /// </summary>
        /// <value>
        /// The closed date.
        /// </value>
        public DateTime? ClosedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is work related.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is work related; otherwise, <c>false</c>.
        /// </value>
        public bool IsWorkRelated { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the status kpi.
        /// </summary>
        /// <value>
        /// The status kpi.
        /// </value>
        public KPI? StatusKPI { get; set; }

        /// <summary>
        /// Gets or sets the haz occ details URL data.
        /// </summary>
        /// <value>
        /// The haz occ details URL data.
        /// </value>
        public string HazOccDetailsUrlData { get; set; }

        /// <summary>
        /// Gets or sets the channel count.
        /// </summary>
        /// <value>
        /// The channel count.
        /// </value>
        public int ChannelCount { get; set; }

        /// <summary>
        /// Gets or sets the notes count.
        /// </summary>
        /// <value>
        /// The notes count.
        /// </value>
        public int NotesCount { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }

    }
}
