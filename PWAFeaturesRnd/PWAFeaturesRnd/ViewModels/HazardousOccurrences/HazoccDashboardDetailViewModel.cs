using System.Collections.Generic;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.HazardousOccurrences;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// HazoccDashboardDetailViewModel
    /// </summary>
    public class HazoccDashboardDetailViewModel
    {
        /// <summary>
        /// Gets or sets the open items.
        /// </summary>
        /// <value>
        /// The open items.
        /// </value>
        public int OpenItems { get; set; }

        /// <summary>
        /// Gets or sets the office rev.
        /// </summary>
        /// <value>
        /// The office rev.
        /// </value>
        public int OfficeRev { get; set; }

        /// <summary>
        /// Gets or sets the incident very serious.
        /// </summary>
        /// <value>
        /// The incident very serious.
        /// </value>
        public int IncidentVerySerious { get; set; }

        /// <summary>
        /// Gets or sets the incident serious.
        /// </summary>
        /// <value>
        /// The incident serious.
        /// </value>
        public int IncidentSerious { get; set; }

        /// <summary>
        /// Gets or sets the incident moderate.
        /// </summary>
        /// <value>
        /// The incident moderate.
        /// </value>
        public int IncidentModerate { get; set; }

        /// <summary>
        /// Gets or sets the incident minor.
        /// </summary>
        /// <value>
        /// The incident minor.
        /// </value>
        public int IncidentMinor { get; set; }

        /// <summary>
        /// Gets or sets the crew accident fatal.
        /// </summary>
        /// <value>
        /// The crew accident fatal.
        /// </value>
        public int CrewAccidentFatal { get; set; }

        /// <summary>
        /// Gets or sets the crew accident LWC.
        /// </summary>
        /// <value>
        /// The crew accident LWC.
        /// </value>
        public int CrewAccidentLWC { get; set; }

        /// <summary>
        /// Gets or sets the crew accident RWC.
        /// </summary>
        /// <value>
        /// The crew accident RWC.
        /// </value>
        public int CrewAccidentRWC { get; set; }

        /// <summary>
        /// Gets or sets the crew accident MTC.
        /// </summary>
        /// <value>
        /// The crew accident MTC.
        /// </value>
        public int CrewAccidentMTC { get; set; }

        /// <summary>
        /// Gets or sets the crew accident fac.
        /// </summary>
        /// <value>
        /// The crew accident fac.
        /// </value>
        public int CrewAccidentFAC { get; set; }

        /// <summary>
        /// Gets or sets the statistics lti.
        /// </summary>
        /// <value>
        /// The statistics lti.
        /// </value>
        public int StatisticsLTI { get; set; }

        /// <summary>
        /// Gets or sets the statistics TRC.
        /// </summary>
        /// <value>
        /// The statistics TRC.
        /// </value>
        public int StatisticsTRC { get; set; }

        /// <summary>
        /// Gets or sets the statistics mexphs.
        /// </summary>
        /// <value>
        /// The statistics mexphs.
        /// </value>
        public decimal StatisticsMEXPHS { get; set; }

        /// <summary>
        /// Gets or sets the near miss safe acts.
        /// </summary>
        /// <value>
        /// The near miss safe acts.
        /// </value>
        public int NearMissSafeActs { get; set; }

        /// <summary>
        /// Gets or sets the near miss count.
        /// </summary>
        /// <value>
        /// The near miss count.
        /// </value>
        public int NearMissCount { get; set; }

        /// <summary>
        /// Gets or sets the near miss unsafe acts.
        /// </summary>
        /// <value>
        /// The near miss unsafe acts.
        /// </value>
        public int NearMissUnsafeActs { get; set; }

        /// <summary>
        /// Gets or sets the near miss unsafe cond.
        /// </summary>
        /// <value>
        /// The near miss unsafe cond.
        /// </value>
        public int NearMissUnsafeCond { get; set; }

        /// <summary>
        /// Gets or sets the passenger fatal.
        /// </summary>
        /// <value>
        /// The passenger fatal.
        /// </value>
        public int PassengerFatal { get; set; }

        /// <summary>
        /// Gets or sets the passenger MTC.
        /// </summary>
        /// <value>
        /// The passenger MTC.
        /// </value>
        public int PassengerMTC { get; set; }

        /// <summary>
        /// Gets or sets the passenger fac.
        /// </summary>
        /// <value>
        /// The passenger fac.
        /// </value>
        public int PassengerFAC { get; set; }

        /// <summary>
        /// Gets or sets the open items URL.
        /// </summary>
        /// <value>
        /// The open items URL.
        /// </value>
        public string OpenItemsUrl { get; set; }

        /// <summary>
        /// Gets or sets the office rev URL.
        /// </summary>
        /// <value>
        /// The office rev URL.
        /// </value>
        public string OfficeRevUrl { get; set; }

        /// <summary>
        /// Gets or sets the incident very serious URL.
        /// </summary>
        /// <value>
        /// The incident very serious URL.
        /// </value>
        public string IncidentVerySeriousUrl { get; set; }

        /// <summary>
        /// Gets or sets the incident serious URL.
        /// </summary>
        /// <value>
        /// The incident serious URL.
        /// </value>
        public string IncidentSeriousUrl { get; set; }

        /// <summary>
        /// Gets or sets the incident moderate URL.
        /// </summary>
        /// <value>
        /// The incident moderate URL.
        /// </value>
        public string IncidentModerateUrl { get; set; }

        /// <summary>
        /// Gets or sets the incident minor URL.
        /// </summary>
        /// <value>
        /// The incident minor URL.
        /// </value>
        public string IncidentMinorUrl { get; set; }

        /// <summary>
        /// Gets or sets the crew accident fatal URL.
        /// </summary>
        /// <value>
        /// The crew accident fatal URL.
        /// </value>
        public string CrewAccidentFatalUrl { get; set; }

        /// <summary>
        /// Gets or sets the crew accident LWC URL.
        /// </summary>
        /// <value>
        /// The crew accident LWC URL.
        /// </value>
        public string CrewAccidentLWCUrl { get; set; }

        /// <summary>
        /// Gets or sets the crew accident RWC URL.
        /// </summary>
        /// <value>
        /// The crew accident RWC URL.
        /// </value>
        public string CrewAccidentRWCUrl { get; set; }

        /// <summary>
        /// Gets or sets the crew accident MTC URL.
        /// </summary>
        /// <value>
        /// The crew accident MTC URL.
        /// </value>
        public string CrewAccidentMTCUrl { get; set; }

        /// <summary>
        /// Gets or sets the crew accident fac URL.
        /// </summary>
        /// <value>
        /// The crew accident fac URL.
        /// </value>
        public string CrewAccidentFACUrl { get; set; }

        /// <summary>
        /// Gets or sets the statistics lti URL.
        /// </summary>
        /// <value>
        /// The statistics lti URL.
        /// </value>
        public string StatisticsLTIUrl { get; set; }

        /// <summary>
        /// Gets or sets the statistics TRC URL.
        /// </summary>
        /// <value>
        /// The statistics TRC URL.
        /// </value>
        public string StatisticsTRCUrl { get; set; }

        /// <summary>
        /// Gets or sets the statistics mexphs URL.
        /// </summary>
        /// <value>
        /// The statistics mexphs URL.
        /// </value>
        public string StatisticsMEXPHSUrl { get; set; }

        /// <summary>
        /// Gets or sets the near miss safe acts URL.
        /// </summary>
        /// <value>
        /// The near miss safe acts URL.
        /// </value>
        public string NearMissSafeActsUrl { get; set; }

        /// <summary>
        /// Gets or sets the near miss count URL.
        /// </summary>
        /// <value>
        /// The near miss count URL.
        /// </value>
        public string NearMissCountUrl { get; set; }

        /// <summary>
        /// Gets or sets the near miss unsafe acts URL.
        /// </summary>
        /// <value>
        /// The near miss unsafe acts URL.
        /// </value>
        public string NearMissUnsafeActsUrl { get; set; }

        /// <summary>
        /// Gets or sets the near miss unsafe cond URL.
        /// </summary>
        /// <value>
        /// The near miss unsafe cond URL.
        /// </value>
        public string NearMissUnsafeCondUrl { get; set; }

        /// <summary>
        /// Gets or sets the passenger fatal URL.
        /// </summary>
        /// <value>
        /// The passenger fatal URL.
        /// </value>
        public string PassengerFatalUrl { get; set; }

        /// <summary>
        /// Gets or sets the passenger MTC URL.
        /// </summary>
        /// <value>
        /// The passenger MTC URL.
        /// </value>
        public string PassengerMTCUrl { get; set; }

        /// <summary>
        /// Gets or sets the passenger fac URL.
        /// </summary>
        /// <value>
        /// The passenger fac URL.
        /// </value>
        public string PassengerFACUrl { get; set; }

        /// <summary>
        /// Gets or sets the total passenger accident.
        /// </summary>
        /// <value>
        /// The total passenger accident.
        /// </value>
        public int TotalPassengerAccident { get; set; }

        /// <summary>
        /// Gets or sets the total passenger accident URL.
        /// </summary>
        /// <value>
        /// The total passenger accident URL.
        /// </value>
        public string TotalPassengerAccidentUrl { get; set; }


        /// <summary>
        /// Gets or sets the total incidents.
        /// </summary>
        /// <value>
        /// The total incidents.
        /// </value>
        public int TotalIncidents { get; set; }

        /// <summary>
        /// Gets or sets the total incidents URL.
        /// </summary>
        /// <value>
        /// The total incidents URL.
        /// </value>
        public string TotalIncidentsUrl { get; set; }


        /// <summary>
        /// Gets or sets the total accidents.
        /// </summary>
        /// <value>
        /// The total accidents.
        /// </value>
        public int TotalAccidents { get; set; }

        /// <summary>
        /// Gets or sets the total crew accidents Url.
        /// </summary>
        /// <value>
        /// The total crew accidents.
        /// </value>
        public string TotalCrewAccidentsUrl { get; set; }


        /// <summary>
        /// Gets or sets the total near miss observations.
        /// </summary>
        /// <value>
        /// The total near miss observations.
        /// </value>
        public int TotalNearMissObservations { get; set; }

        /// <summary>
        /// Gets or sets the total near miss observations URL.
        /// </summary>
        /// <value>
        /// The total near miss observations URL.
        /// </value>
        public string TotalNearMissObservationsUrl { get; set; }


        /// <summary>
        /// Gets or sets the lti count.
        /// </summary>
        /// <value>
        /// The lti count.
        /// </value>
        public int LtiCount { get; set; }

        /// <summary>
        /// Gets or sets the TRC count.
        /// </summary>
        /// <value>
        /// The TRC count.
        /// </value>
        public int TrcCount { get; set; }

        /// <summary>
        /// Gets or sets the mexphs count.
        /// </summary>
        /// <value>
        /// The mexphs count.
        /// </value>
        public decimal MexpHrs { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the total count URL.
        /// </summary>
        /// <value>
        /// The total count URL.
        /// </value>
        public string TotalCountUrl { get; set; }


        /// <summary>
        /// Gets or sets the fatalities.
        /// </summary>
        /// <value>
        /// The fatalities.
        /// </value>
        public int TotalFatalities { get; set; }

        /// <summary>
        /// Gets or sets the total fatalities URL.
        /// </summary>
        /// <value>
        /// The total fatalities URL.
        /// </value>
        public string TotalFatalitiesURL { get; set; }


        /// <summary>
        /// Gets or sets the very serious.
        /// </summary>
        /// <value>
        /// The very serious.
        /// </value>
        public int TotalVerySerious { get; set; }

        /// <summary>
        /// Gets or sets the total very serious URL.
        /// </summary>
        /// <value>
        /// The total very serious URL.
        /// </value>
        public string TotalVerySeriousURL { get; set; }

        /// <summary>
        /// Gets or sets the lti URL.
        /// </summary>
        /// <value>
        /// The lti URL.
        /// </value>
        public string LtiUrl { get; set; }

        /// <summary>
        /// Gets or sets the TRC URL.
        /// </summary>
        /// <value>
        /// The TRC URL.
        /// </value>
        public string TrcUrl { get; set; }

        /// <summary>
        /// Gets or sets the third party accidents.
        /// </summary>
        /// <value>
        /// The third party accidents.
        /// </value>
        public int ThirdPartyAccidents { get; set; }

        /// <summary>
        /// Gets or sets the third party accidents URL.
        /// </summary>
        /// <value>
        /// The third party accidents URL.
        /// </value>
        public string ThirdPartyAccidentsUrl { get; set; }


        /// <summary>
        /// Gets or sets the accident classifications.
        /// </summary>
        /// <value>
        /// The accident classifications.
        /// </value>
        public List<Lookup> AccidentClassifications { get; set; }

        /// <summary>
        /// Gets or sets the open accident details.
        /// </summary>
        /// <value>
        /// The open accident details.
        /// </value>
        public List<OpenAccidentDetail> OpenAccidentDetails { get; set; }

        /// <summary>
        /// Gets or sets the open incident details.
        /// </summary>
        /// <value>
        /// The open incident details.
        /// </value>
        public List<OpenIncidentDetail> OpenIncidentDetails { get; set; }
    }
}
