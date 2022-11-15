using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    public class AccidentEventDetailViewModel
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

        public string ImmediateActionTaken { get; set; }

        /// <summary>
        /// Gets or sets the accident date.
        /// </summary>
        /// <value>
        /// The accident date.
        /// </value>

        public string AccidentDate { get; set; }

        /// <summary>
        /// Gets or sets the reported date.
        /// </summary>
        /// <value>
        /// The reported date.
        /// </value>

        public string ReportedDate { get; set; }

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

        public string HasSpectacle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [spectacles worn].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [spectacles worn]; otherwise, <c>false</c>.
        /// </value>

        public string SpectaclesWorn { get; set; }

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
        public string CrewTimeWithCompany { get; set; }

        /// <summary>
        /// Gets or sets the crewtime in rank.
        /// </summary>
        /// <value>
        /// The crewtime in rank.
        /// </value>
        public string CrewtimeInRank { get; set; }

        /// <summary>
        /// Gets or sets the duty status.
        /// </summary>
        /// <value>
        /// The duty status.
        /// </value>
        public string DutyStatus { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the type injury.
        /// </summary>
        /// <value>
        /// The type injury.
        /// </value>
        public string TypeInjury { get; set; }

        /// <summary>
        /// Gets or sets the type accident.
        /// </summary>
        /// <value>
        /// The type accident.
        /// </value>
        public string TypeAccident { get; set; }

        /// <summary>
        /// Gets or sets the ppe worn description.
        /// </summary>
        /// <value>
        /// The ppe worn description.
        /// </value>
        public string PPEWornDescription { get; set; }

        /// <summary>
        /// Gets or sets the body areas affected.
        /// </summary>
        /// <value>
        /// The body areas affected.
        /// </value>
        public string BodyAreasAffected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is crew or thirty prty accident.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is crew or thirty prty accident; otherwise, <c>false</c>.
        /// </value>
        public bool IsCrewOrThirtyPrtyAccident { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is crew accident.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is crew accident; otherwise, <c>false</c>.
        /// </value>
        public bool IsCrewAccident { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is third party accident.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is third party accident; otherwise, <c>false</c>.
        /// </value>
        public bool IsThirdPartyAccident { get; set; }
    }
}
