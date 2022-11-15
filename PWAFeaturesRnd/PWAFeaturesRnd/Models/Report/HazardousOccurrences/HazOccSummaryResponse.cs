namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// Haz Occ Summary Response
    /// </summary>
    public class HazOccSummaryResponse
    {
        /// <summary>
        /// Gets or sets the unsafe rate.
        /// </summary>
        /// <value>
        /// The unsafe rate.
        /// </value>
        public decimal? UnsafeRate { get; set; }

        /// <summary>
        /// Gets or sets the serious accident count.
        /// </summary>
        /// <value>
        /// The serious accident count.
        /// </value>
        public int? SeriousAccidentCount { get; set; }

        /// <summary>
        /// Gets or sets the serious incident count.
        /// </summary>
        /// <value>
        /// The serious incident count.
        /// </value>
        public int? SeriousIncidentCount { get; set; }

        /// <summary>
        /// Gets or sets the lti free days count.
        /// </summary>
        /// <value>
        /// The lti free days count.
        /// </value>
        public int? LTIFreeDaysCount { get; set; }

        /// <summary>
        /// Gets or sets the unsafe priority.
        /// </summary>
        /// <value>
        /// The unsafe priority.
        /// </value>
        public int UnsafePriority { get; set; }

        /// <summary>
        /// Gets or sets the serious accident priority.
        /// </summary>
        /// <value>
        /// The serious accident priority.
        /// </value>
        public int SeriousAccidentPriority { get; set; }

        /// <summary>
        /// Gets or sets the serious incident priority.
        /// </summary>
        /// <value>
        /// The serious incident priority.
        /// </value>
        public int SeriousIncidentPriority { get; set; }

        /// <summary>
        /// Gets or sets the lti free days priority.
        /// </summary>
        /// <value>
        /// The lti free days priority.
        /// </value>
        public int LTIFreeDaysPriority { get; set; }

        /// <summary>
        /// Gets or sets the crew accidents.
        /// </summary>
        /// <value>
        /// The crew accidents.
        /// </value>
        public int? CrewAccidents { get; set; }

        /// <summary>
        /// Gets or sets the passenger accidents.
        /// </summary>
        /// <value>
        /// The passenger accidents.
        /// </value>
        public int? PassengerAccidents { get; set; }

        /// <summary>
        /// Gets or sets the third party accidents.
        /// </summary>
        /// <value>
        /// The third party accidents.
        /// </value>
        public int? ThirdPartyAccidents { get; set; }

        /// <summary>
        /// Gets or sets the near miss observation.
        /// </summary>
        /// <value>
        /// The near miss observation.
        /// </value>
        public int? NearMissObservation { get; set; }

        /// <summary>
        /// Gets or sets the incident.
        /// </summary>
        /// <value>
        /// The incident.
        /// </value>
        public int? Incident { get; set; }

        /// <summary>
        /// Gets or sets the fatality.
        /// </summary>
        /// <value>
        /// The fatality.
        /// </value>
        public int? Fatality { get; set; }

        /// <summary>
        /// Gets or sets the very serious.
        /// </summary>
        /// <value>
        /// The very serious.
        /// </value>
        public int? VerySerious { get; set; }

        /// <summary>
        /// Gets or sets the illness.
        /// </summary>
        /// <value>
        /// The illness.
        /// </value>
        public int? Illness { get; set; }

        /// <summary>
        /// Gets or sets the lti.
        /// </summary>
        /// <value>
        /// The lti.
        /// </value>
        public int? LTI { get; set; }

        /// <summary>
        /// Gets or sets the TRC.
        /// </summary>
        /// <value>
        /// The TRC.
        /// </value>
        public int? TRC { get; set; }

        /// <summary>
        /// Gets or sets the m exp HRS CRW.
        /// </summary>
        /// <value>
        /// The m exp HRS CRW.
        /// </value>
        public decimal? MExpHrsCrw { get; set; }

        /// <summary>
        /// Gets or sets the m exp HRS pax.
        /// </summary>
        /// <value>
        /// The m exp HRS pax.
        /// </value>
        public decimal? MExpHrsPax { get; set; }
    }
}
