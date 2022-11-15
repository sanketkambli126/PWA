using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Data contract for accident event details.
    /// </summary>
    public class AccidentEventDetail
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
        /// Gets or sets the pax status identifier.
        /// </summary>
        /// <value>
        /// The pax status identifier.
        /// </value>

        public string PaxStatusId { get; set; }

        /// <summary>
        /// Gets or sets the duty status identifier.
        /// </summary>
        /// <value>
        /// The duty status identifier.
        /// </value>

        public string DutyStatusId { get; set; }

        /// <summary>
        /// Gets or sets the injury type identifier.
        /// </summary>
        /// <value>
        /// The injury type identifier.
        /// </value>

        public string InjuryTypeId { get; set; }

        /// <summary>
        /// Gets or sets the accident type identifier.
        /// </summary>
        /// <value>
        /// The accident type identifier.
        /// </value>

        public string AccidentTypeId { get; set; }

        /// <summary>
        /// Gets or sets the body area affected identifier.
        /// </summary>
        /// <value>
        /// The body area affected identifier.
        /// </value>

        public string BodyAreaAffectedId { get; set; }

        /// <summary>
        /// Gets or sets the immediate action taken.
        /// </summary>
        /// <value>
        /// The immediate action taken.
        /// </value>

        public string ImmediateActionTaken { get; set; }

        /// <summary>
        /// Gets or sets the accident date.
        /// </summary>
        /// <value>
        /// The accident date.
        /// </value>

        public DateTime? AccidentDate { get; set; }

        /// <summary>
        /// Gets or sets the reported date.
        /// </summary>
        /// <value>
        /// The reported date.
        /// </value>

        public DateTime? ReportedDate { get; set; }

        /// <summary>
        /// Gets or sets the reported to.
        /// </summary>
        /// <value>
        /// The reported to.
        /// </value>

        public string ReportedTo { get; set; }

        /// <summary>
        /// Gets or sets the name of the reported to.
        /// </summary>
        /// <value>
        /// The name of the reported to.
        /// </value>

        public string ReportedToName { get; set; }

        /// <summary>
        /// Gets or sets the area supervisor.
        /// </summary>
        /// <value>
        /// The area supervisor.
        /// </value>

        public string AreaSupervisor { get; set; }

        /// <summary>
        /// Gets or sets the name of the area supervisor.
        /// </summary>
        /// <value>
        /// The name of the area supervisor.
        /// </value>

        public string AreaSupervisorName { get; set; }

        /// <summary>
        /// Gets or sets the area supervisor rank.
        /// </summary>
        /// <value>
        /// The area supervisor rank.
        /// </value>

        public string AreaSupervisorRank { get; set; }

        /// <summary>
        /// Gets or sets the name of the area supervisor rank.
        /// </summary>
        /// <value>
        /// The name of the area supervisor rank.
        /// </value>

        public string AreaSupervisorRankName { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>

        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the work hours.
        /// </summary>
        /// <value>
        /// The work hours.
        /// </value>

        public decimal? WorkHours { get; set; }

        /// <summary>
        /// Gets or sets the rest hours.
        /// </summary>
        /// <value>
        /// The rest hours.
        /// </value>

        public decimal? RestHours { get; set; }

        /// <summary>
        /// Gets or sets the days onboard.
        /// </summary>
        /// <value>
        /// The days onboard.
        /// </value>

        public string DaysOnboard { get; set; }

        /// <summary>
        /// Gets or sets the time with company.
        /// </summary>
        /// <value>
        /// The time with company.
        /// </value>

        public string TimeWithCompany { get; set; }

        /// <summary>
        /// Gets or sets the time in rank.
        /// </summary>
        /// <value>
        /// The time in rank.
        /// </value>

        public string TimeInRank { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has spectacle.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has spectacle; otherwise, <c>false</c>.
        /// </value>

        public bool HasSpectacle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [spectacles worn].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [spectacles worn]; otherwise, <c>false</c>.
        /// </value>

        public bool SpectaclesWorn { get; set; }

        /// <summary>
        /// Gets or sets the ppe worn.
        /// </summary>
        /// <value>
        /// The ppe worn.
        /// </value>

        public string PPEWorn { get; set; }

        /// <summary>
        /// Gets or sets the occupation.
        /// </summary>
        /// <value>
        /// The occupation.
        /// </value>

        public string Occupation { get; set; }

        /// <summary>
        /// Gets or sets the reported to CRW identifier tp.
        /// </summary>
        /// <value>
        /// The reported to CRW identifier tp.
        /// </value>

        public string ReportedToCrwIdTp { get; set; }

        /// <summary>
        /// Gets or sets the area supervisor CRW identifier tp.
        /// </summary>
        /// <value>
        /// The area supervisor CRW identifier tp.
        /// </value>

        public string AreaSupervisorCrwIdTp { get; set; }

        /// <summary>
        /// Gets or sets the days onboard ship.
        /// </summary>
        /// <value>
        /// The days onboard ship.
        /// </value>

        public int? DaysOnboardShip { get; set; }

        /// <summary>
        /// Gets or sets the crew time with company.
        /// </summary>
        /// <value>
        /// The crew time with company.
        /// </value>

        public decimal? CrewTimeWithCompany { get; set; }

        /// <summary>
        /// Gets or sets the crewtime in rank.
        /// </summary>
        /// <value>
        /// The crewtime in rank.
        /// </value>

        public decimal? CrewtimeInRank { get; set; }
    }
}
