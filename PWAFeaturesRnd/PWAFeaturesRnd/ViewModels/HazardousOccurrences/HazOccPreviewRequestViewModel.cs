using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PWAFeaturesRnd.Common.Converter;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// HazOccPreviewRequestViewModel
    /// </summary>
    public class HazOccPreviewRequestViewModel
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
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
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
        /// Gets or sets the name of the stage.
        /// </summary>
        /// <value>
        /// The name of the stage.
        /// </value>
        public string StageName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is searched click.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is searched click; otherwise, <c>false</c>.
        /// </value>
        public bool IsSearchedClick { get; set; }

    }
}
