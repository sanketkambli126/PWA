using System;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    public class ObservationSummaryViewModel
    {
        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>
        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the report date.
        /// </summary>
        /// <value>
        /// The report date.
        /// </value>
        public string ReportDate { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public string UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the observation date time.
        /// </summary>
        /// <value>
        /// The observation date time.
        /// </value>
        public string ObservationDateTime { get; set; }

        /// <summary>
        /// Gets or sets the safety officer.
        /// </summary>
        /// <value>
        /// The safety officer.
        /// </value>
        public string SafetyOfficer { get; set; }

        /// <summary>
        /// Gets or sets the parent report identifier.
        /// </summary>
        /// <value>
        /// The parent report identifier.
        /// </value>
        public string ParentReportId { get; set; }

        /// <summary>
        /// Gets or sets the parent report.
        /// </summary>
        /// <value>
        /// The parent report.
        /// </value>
        public string ParentReport { get; set; }

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
        /// Gets or sets the name of the master.
        /// </summary>
        /// <value>
        /// The name of the master.
        /// </value>
        public string MasterName { get; set; }

        /// <summary>
        /// Gets or sets the ship reference no.
        /// </summary>
        /// <value>
        /// The ship reference no.
        /// </value>
        public string ShipRefNo { get; set; }

        /// <summary>
        /// Gets or sets the potential severity identifier.
        /// </summary>
        /// <value>
        /// The potential severity identifier.
        /// </value>
        public string PotentialSeverityId { get; set; }

        /// <summary>
        /// Gets or sets the potential severity.
        /// </summary>
        /// <value>
        /// The potential severity.
        /// </value>
        public string PotentialSeverity { get; set; }

        /// <summary>
        /// Gets or sets the rank identifier.
        /// </summary>
        /// <value>
        /// The rank identifier.
        /// </value>
        public string RankId { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets the observation raised by.
        /// </summary>
        /// <value>
        /// The observation raised by.
        /// </value>
        public string ObservationRaisedBy { get; set; }

        /// <summary>
        /// Gets or sets the observation raised by crew identifier tp.
        /// </summary>
        /// <value>
        /// The observation raised by crew identifier tp.
        /// </value>
        public string ObservationRaisedByCrewIdTp { get; set; }

        /// <summary>
        /// Gets or sets the name of the observation raised by.
        /// </summary>
        /// <value>
        /// The name of the observation raised by.
        /// </value>
        public string ObservationRaisedByName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ship operation.
        /// </summary>
        /// <value>
        /// The ship operation.
        /// </value>
        public string ShipOperation { get; set; }

        /// <summary>
        /// Gets or sets the possible consequence.
        /// </summary>
        /// <value>
        /// The possible consequence.
        /// </value>
        public string PossibleConsequence { get; set; }

        /// <summary>
        /// Gets or sets the immediate action taken.
        /// </summary>
        /// <value>
        /// The immediate action taken.
        /// </value>
        public string ImmediateActionTaken { get; set; }

        /// <summary>
        /// Gets or sets the act identifier.
        /// </summary>
        /// <value>
        /// The act identifier.
        /// </value>
        public string ActId { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the has work stopped.
        /// </summary>
        /// <value>
        /// The has work stopped.
        /// </value>
        public string HasWorkStopped { get; set; }

        /// <summary>
        /// Gets or sets the closed date.
        /// </summary>
        /// <value>
        /// The closed date.
        /// </value>
        public string ClosedDate { get; set; }

        /// <summary>
        /// Gets or sets the MSQ comments.
        /// </summary>
        /// <value>
        /// The MSQ comments.
        /// </value>
        public string MsqComments { get; set; }

        /// <summary>
        /// Gets or sets the fleet manager comments.
        /// </summary>
        /// <value>
        /// The fleet manager comments.
        /// </value>
        public string FleetManagerComments { get; set; }

        /// <summary>
        /// Gets or sets the report type identifier.
        /// </summary>
        /// <value>
        /// The report type identifier.
        /// </value>
        public string ReportTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the parent report.
        /// </summary>
        /// <value>
        /// The type of the parent report.
        /// </value>
        public string ParentReportType{ get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is safe obs.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is safe obs; otherwise, <c>false</c>.
        /// </value>
        public bool IsSafeObs{ get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unsafe obs.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is unsafe obs; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnsafeObs{ get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is closure comment visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is closure comment visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsClosureCommentVisible { get; set; }

        /// <summary>
        /// Gets or sets the closure comments.
        /// </summary>
        /// <value>
        /// The closure comments.
        /// </value>
        public string ClosureComments { get; set; }

        /// <summary>
        /// Gets or sets the acts label.
        /// </summary>
        /// <value>
        /// The acts label.
        /// </value>
        public string ActsLabel { get; set; }

        /// <summary>
        /// Gets or sets the acts label value.
        /// </summary>
        /// <value>
        /// The acts label value.
        /// </value>
        public string ActsLabelValue { get; set; }
    }
}
