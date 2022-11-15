using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// HazOccPreviewRequest
    /// </summary>
    public class HazOccPreviewRequest
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the incident.
        /// </summary>
        /// <value>
        /// The type of the incident.
        /// </value>
        public List<string> IncidentType { get; set; }

        /// <summary>
        /// Gets or sets the incident severity.
        /// </summary>
        /// <value>
        /// The incident severity.
        /// </value>
        public List<string> IncidentSeverity { get; set; }

        /// <summary>
        /// Gets or sets the incident status.
        /// </summary>
        /// <value>
        /// The incident status.
        /// </value>
        public List<string> IncidentStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [incident deleted].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [incident deleted]; otherwise, <c>false</c>.
        /// </value>
        public bool IncidentDeleted { get; set; }

        /// <summary>
        /// Gets or sets the incident ids.
        /// </summary>
        /// <value>
        /// The incident ids.
        /// </value>
        public List<string> IncidentIds { get; set; }

        /// <summary>
        /// Gets or sets the classification ids.
        /// </summary>
        /// <value>
        /// The classification ids.
        /// </value>
        public List<string> ClassificationIds { get; set; }

        /// <summary>
        /// Gets or sets the is work related.
        /// </summary>
        /// <value>
        /// The is work related.
        /// </value>
        public bool? IsWorkRelated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [fetch total count].
        /// It Includes Crew Accident,Passenger Accident,Incident,Near Miss,Observation
        /// For Crew Accident and Passenger Accident it accepts their ClassificationIds in their separate properties 
        /// <see cref="CrewAccidentClassificationIds"> and <see cref="PassengerClassificationIds">
        /// it doesnt accept any properties for Incident,Near Miss and Observation
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fetch total count]; otherwise, <c>false</c>.
        /// </value>
        public bool FetchTotalCount { get; set; }

        /// <summary>
        /// Gets or sets the crew accident classification ids.
        /// </summary>
        /// <value>
        /// The crew accident classification ids.
        /// </value>
        public List<string> CrewAccidentClassificationIds { get; set; }

        /// <summary>
        /// Gets or sets the passenger classification ids.
        /// </summary>
        /// <value>
        /// The passenger classification ids.
        /// </value>
        public List<string> PassengerClassificationIds { get; set; }

    }
}
