using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// Route forecast alert
    /// </summary>
    public class RouteForecastAlert
    {
        /// <summary>
        /// Gets or sets the charter speed.
        /// </summary>
        /// <value>
        /// The charter speed.
        /// </value>
        [JsonProperty("charterSpeed")]
        public string CharterSpeed { get; set; }

        /// <summary>
        /// Gets or sets the date recorded.
        /// </summary>
        /// <value>
        /// The date recorded.
        /// </value>
        [JsonProperty("dateRecorded")]
        public string DateRecorded { get; set; }

        /// <summary>
        /// Gets or sets the end port code.
        /// </summary>
        /// <value>
        /// The end port code.
        /// </value>
        [JsonProperty("endPortCode")]
        public string EndPortCode { get; set; }

        /// <summary>
        /// Gets or sets the end port coordinate.
        /// </summary>
        /// <value>
        /// The end port coordinate.
        /// </value>
        [JsonProperty("endPortCoordinate")]
        public string EndPortCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the end port country.
        /// </summary>
        /// <value>
        /// The end port country.
        /// </value>
        [JsonProperty("endPortCountry")]
        public string EndPortCountry { get; set; }

        /// <summary>
        /// Gets or sets the end name of the port.
        /// </summary>
        /// <value>
        /// The end name of the port.
        /// </value>
        [JsonProperty("endPortName")]
        public string EndPortName { get; set; }

        /// <summary>
        /// Gets or sets the imo.
        /// </summary>
        /// <value>
        /// The imo.
        /// </value>
        [JsonProperty("imo")]
        public string Imo { get; set; }

        /// <summary>
        /// Gets or sets the journey identifier.
        /// </summary>
        /// <value>
        /// The journey identifier.
        /// </value>
        [JsonProperty("journeyId")]
        public string JourneyId { get; set; }

        /// <summary>
        /// Gets or sets the journey start date.
        /// </summary>
        /// <value>
        /// The journey start date.
        /// </value>
        [JsonProperty("journeyStartDate")]
        public DateTime JourneyStartDate { get; set; }

        /// <summary>
        /// Gets or sets the start port code.
        /// </summary>
        /// <value>
        /// The start port code.
        /// </value>
        [JsonProperty("startPortCode")]
        public string StartPortCode { get; set; }

        /// <summary>
        /// Gets or sets the start port coordinate.
        /// </summary>
        /// <value>
        /// The start port coordinate.
        /// </value>
        [JsonProperty("startPortCoordinate")]
        public string StartPortCoordinate { get; set; }

        /// <summary>
        /// Gets or sets the start port country.
        /// </summary>
        /// <value>
        /// The start port country.
        /// </value>
        [JsonProperty("startPortCountry")]
        public string StartPortCountry { get; set; }

        /// <summary>
        /// Gets or sets the start name of the port.
        /// </summary>
        /// <value>
        /// The start name of the port.
        /// </value>
        [JsonProperty("startPortName")]
        public string StartPortName { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        [JsonProperty("vesselName")]
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the warning data.
        /// </summary>
        /// <value>
        /// The warning data.
        /// </value>
        [JsonProperty("warningData")]
        public string WarningData { get; set; }

        /// <summary>
        /// Gets or sets the warning for date.
        /// </summary>
        /// <value>
        /// The warning for date.
        /// </value>
        [JsonProperty("warningForDate")]
        public DateTime WarningForDate { get; set; }

        /// <summary>
        /// Gets or sets the warning identifier.
        /// </summary>
        /// <value>
        /// The warning identifier.
        /// </value>
        [JsonProperty("warningId")]
        public long WarningId { get; set; }

        /// <summary>
        /// Gets or sets the journey completed date.
        /// </summary>
        /// <value>
        /// The journey completed date.
        /// </value>
        [JsonProperty("journeyCompletedDate")]
        public DateTime? JourneyCompletedDate { get; set; }

        /// <summary>
        /// Gets or sets the vessel details.
        /// </summary>
        /// <value>
        /// The vessel details.
        /// </value>
        [JsonProperty("VesselDetails")]
        public string VesselDetails { get; set; }

        /// <summary>
        /// Gets the weather warning.
        /// </summary>
        /// <value>
        /// The weather warning.
        /// </value>
        public JourneyRoutesWeather WeatherWarning { get { return JsonConvert.DeserializeObject<JourneyRoutesWeather>(WarningData); } }

        /// <summary>
        /// Gets or sets the remaining trip weather.
        /// </summary>
        /// <value>
        /// The remaining trip weather.
        /// </value>
        public List<JourneyRoutesWeather> RemainingTripWeather { get; set; }

        /// <summary>
        /// Gets the vessel details object.
        /// </summary>
        /// <value>
        /// The vessel details object.
        /// </value>
        public VesselRoutesVesselDetails VesselDetailsObj
        {
            get { return JsonConvert.DeserializeObject<VesselRoutesVesselDetails>(VesselDetails); }
        }
    }
}
