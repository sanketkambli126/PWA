using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// Rolled Up Forecast Alert
    /// </summary>
    public class RolledUpForecastAlert
    {
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }
        /// <summary>
        /// Gets or sets the imo.
        /// </summary>
        /// <value>
        /// The imo.
        /// </value>
        public string Imo { get; set; }
        /// <summary>
        /// Gets or sets the distance travelled.
        /// </summary>
        /// <value>
        /// The distance travelled.
        /// </value>
        public int DistanceTravelled { get; set; }
        /// <summary>
        /// Gets or sets the start name of the port.
        /// </summary>
        /// <value>
        /// The start name of the port.
        /// </value>
        public string StartPortName { get; set; }
        /// <summary>
        /// Gets or sets the start port country.
        /// </summary>
        /// <value>
        /// The start port country.
        /// </value>
        public string StartPortCountry { get; set; }
        /// <summary>
        /// Gets or sets the journey start date.
        /// </summary>
        /// <value>
        /// The journey start date.
        /// </value>
        public DateTime JourneyStartDate { get; set; }
        /// <summary>
        /// Gets or sets the start coordinate.
        /// </summary>
        /// <value>
        /// The start coordinate.
        /// </value>
        public string StartCoordinate { get; set; }
        /// <summary>
        /// Gets or sets the end name of the port.
        /// </summary>
        /// <value>
        /// The end name of the port.
        /// </value>
        public string EndPortName { get; set; }
        /// <summary>
        /// Gets or sets the end port country.
        /// </summary>
        /// <value>
        /// The end port country.
        /// </value>
        public string EndPortCountry { get; set; }
        /// <summary>
        /// Gets or sets the journey completed date.
        /// </summary>
        /// <value>
        /// The journey completed date.
        /// </value>
        public DateTime? JourneyCompletedDate { get; set; }
        /// <summary>
        /// Gets or sets the end coordinate.
        /// </summary>
        /// <value>
        /// The end coordinate.
        /// </value>
        public string EndCoordinate { get; set; }
        /// <summary>
        /// Gets or sets the wind warning.
        /// </summary>
        /// <value>
        /// The wind warning.
        /// </value>
        public string WindWarning { get; set; }
        /// <summary>
        /// Gets or sets the route forecast alerts.
        /// </summary>
        /// <value>
        /// The route forecast alerts.
        /// </value>
        public List<JourneyRoutesWeather> RouteForecastAlerts { get; set; }
        /// <summary>
        /// Gets or sets the vessel details.
        /// </summary>
        /// <value>
        /// The vessel details.
        /// </value>
        public string VesselDetails { get; set; }

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

        /// <summary>
        /// Gets the name of the formatted vessel.
        /// </summary>
        /// <value>
        /// The name of the formatted vessel.
        /// </value>
        public string FormattedVesselName
        {
            get { return string.Format("{0} - ({1})", VesselName, Imo); }
        }

        /// <summary>
        /// Gets the start coordinate object.
        /// </summary>
        /// <value>
        /// The start coordinate object.
        /// </value>
        public SeaRoutesCoordinates StartCoordinateObj
        {
            get { return JsonConvert.DeserializeObject<SeaRoutesCoordinates>(StartCoordinate); }
        }

        /// <summary>
        /// Gets the end coordinate object.
        /// </summary>
        /// <value>
        /// The end coordinate object.
        /// </value>
        public SeaRoutesCoordinates EndCoordinateObj
        {
            get { return JsonConvert.DeserializeObject<SeaRoutesCoordinates>(EndCoordinate); }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class JourneyRoutesWeather
    {
        /// <summary>
        /// Gets or sets the vessel imo.
        /// </summary>
        /// <value>
        /// The vessel imo.
        /// </value>
        public string VesselImo { get; set; }
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }
        /// <summary>
        /// Gets or sets the ecdis waypoint identifier.
        /// </summary>
        /// <value>
        /// The ecdis waypoint identifier.
        /// </value>
        public int EcdisWaypointId { get; set; }
        /// <summary>
        /// Gets or sets the weather date.
        /// </summary>
        /// <value>
        /// The weather date.
        /// </value>
        public DateTime WeatherDate { get; set; }
        /// <summary>
        /// Gets or sets the speed beaufort.
        /// </summary>
        /// <value>
        /// The speed beaufort.
        /// </value>
        public int SpeedBeaufort { get; set; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        /// <value>
        /// The direction.
        /// </value>
        public int Direction { get; set; }
        /// <summary>
        /// Gets or sets the track distance.
        /// </summary>
        /// <value>
        /// The track distance.
        /// </value>
        public long TrackDistance { get; set; }
        /// <summary>
        /// Gets the distance.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public int Distance
        {
            get
            {
                return (int)(TrackDistance / 1852);
            }
        }
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public long Duration { get; set; }

        /// <summary>
        /// The speed ms
        /// </summary>
        private decimal _speedMS;
        /// <summary>
        /// Gets or sets the speed ms.
        /// </summary>
        /// <value>
        /// The speed ms.
        /// </value>
        public decimal SpeedMS
        {
            get { return Math.Round(_speedMS, 2); }
            set { _speedMS = value; }
        }

        /// <summary>
        /// The latitude
        /// </summary>
        private double _latitude;
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Latitude
        {
            get { return Math.Round(_latitude, 2); }
            set { _latitude = value; }
        }

        /// <summary>
        /// The longitude
        /// </summary>
        private double _longitude;
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Longitude
        {
            get { return Math.Round(_longitude, 2); }
            set { _longitude = value; }
        }

        /// <summary>
        /// Gets the location string.
        /// </summary>
        /// <value>
        /// The location string.
        /// </value>
        public string LocationStr
        {
            get { return string.Format("{0}° {1}, {2}° {3}", Math.Abs(Latitude), Latitude >= 0 ? "N" : "S", Math.Abs(Longitude), Longitude >= 0 ? "E" : "W"); }
        }

        /// <summary>
        /// Gets the beaufort colour.
        /// </summary>
        /// <value>
        /// The beaufort colour.
        /// </value>
        public string BeaufortColour
        {
            get
            {
                switch (SpeedBeaufort)
                {
                    case 0:
                        return "#73CBFD";
                    case 1:
                        return "#AEF1F9";
                    case 2:
                        return "#96F7DC";
                    case 3:
                        return "#96F7B4";
                    case 4:
                        return "#6FF46F";
                    case 5:
                        return "#73ED12";
                    case 6:
                        return "#A4ED12";
                    case 7:
                        return "#DAED12";
                    case 8:
                        return "#EDC212";
                    case 9:
                        return "#ED8F12";
                    case 10:
                        return "#ED6312";
                    case 11:
                        return "#ED2912";
                    case 12:
                        return "#D5102D";
                    default:
                        return "#FFFFFF";
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SeaRoutesCoordinates
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        [JsonProperty("Latitude")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        [JsonProperty("Longitude")]
        public decimal Longitude { get; set; }
    }
}
