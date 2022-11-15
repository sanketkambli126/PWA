using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// IncidentPreview
    /// </summary>
    public class IncidentPreview
    {

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier { get; set; }

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
        /// Gets or sets the type of the vessel.
        /// </summary>
        /// <value>
        /// The type of the vessel.
        /// </value>
        public string VesselType { get; set; }

        /// <summary>
        /// Gets or sets the vessel master.
        /// </summary>
        /// <value>
        /// The vessel master.
        /// </value>
        public string VesselMaster { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public string StatusId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        /// <value>
        /// The class.
        /// </value>
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the class identifier.
        /// </summary>
        /// <value>
        /// The class identifier.
        /// </value>
        public string ClassID { get; set; }

        /// <summary>
        /// Gets or sets the report date.
        /// </summary>
        /// <value>
        /// The report date.
        /// </value>
        public DateTime ReportDate { get; set; }

        /// <summary>
        /// Gets or sets the incident date.
        /// </summary>
        /// <value>
        /// The incident date.
        /// </value>
        public System.DateTime IncidentDate { get; set; }

        /// <summary>
        /// Gets or sets the actions overdue count.
        /// </summary>
        /// <value>
        /// The actions overdue count.
        /// </value>
        public int ActionsOverdueCount { get; set; }

        /// <summary>
        /// Gets or sets the ship reference number.
        /// </summary>
        /// <value>
        /// The ship reference number.
        /// </value>
        public string ShipReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the report close date.
        /// </summary>
        /// <value>
        /// The report close date.
        /// </value>
        public DateTime? ReportCloseDate { get; set; }

        /// <summary>
        /// Gets or sets the type identifier.
        /// </summary>
        /// <value>
        /// The type identifier.
        /// </value>
        public string TypeID { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        /// <value>
        /// The severity.
        /// </value>
        public string Severity { get; set; }

        /// <summary>
        /// Gets or sets the severity desc.
        /// </summary>
        /// <value>
        /// The severity desc.
        /// </value>
        public string SeverityDesc { get; set; }

        /// <summary>
        /// Gets or sets the substandard acts comment.
        /// </summary>
        /// <value>
        /// The substandard acts comment.
        /// </value>
        public string SubstandardActsComment { get; set; }

        /// <summary>
        /// Gets or sets the substandard conds comment.
        /// </summary>
        /// <value>
        /// The substandard conds comment.
        /// </value>
        public string SubstandardCondsComment { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IncidentPreview"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [closure pending].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [closure pending]; otherwise, <c>false</c>.
        /// </value>
        public bool ClosurePending { get; set; } //Fld for showing Closure awaiting sign-off

        /// <summary>
        /// Gets or sets a value indicating whether this instance is parent report.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is parent report; otherwise, <c>false</c>.
        /// </value>
        public bool IsParentReport { get; set; } //Fld for showing whether other reports linked to this one

        /// <summary>
        /// Gets or sets a value indicating whether this instance is child report.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is child report; otherwise, <c>false</c>.
        /// </value>
        public bool IsChildReport { get; set; } //Fld for showing whether linked to other report

        /// <summary>
        /// Gets or sets the ship operation.
        /// </summary>
        /// <value>
        /// The ship operation.
        /// </value>
        public string ShipOperation { get; set; }

        /// <summary>
        /// Gets or sets the type of the ves gen.
        /// </summary>
        /// <value>
        /// The type of the ves gen.
        /// </value>
        public string VesGenType { get; set; }

        /// <summary>
        /// Gets or sets the tech office.
        /// </summary>
        /// <value>
        /// The tech office.
        /// </value>
        public string TechOffice { get; set; }
    }
}
