using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    /// <summary>
    /// Sea Passage Summary
    /// </summary>
    public class SeaPassageSummary
    {
        /// <summary>
        /// Gets or sets the name of from port.
        /// </summary>
        /// <value>
        /// The name of from port.
        /// </value>
        public string FromPortName { get; set; }

        /// <summary>
        /// Gets or sets from country code.
        /// </summary>
        /// <value>
        /// From country code.
        /// </value>
        public string FromCountryCode { get; set; }

        /// <summary>
        /// Converts to portname.
        /// </summary>
        /// <value>
        /// The name of to port.
        /// </value>
        public string ToPortName { get; set; }

        /// <summary>
        /// Converts to countrycode.
        /// </summary>
        /// <value>
        /// To country code.
        /// </value>
        public string ToCountryCode { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        /// <value>
        /// The total distance.
        /// </value>
        public decimal? TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the weather detail.
        /// </summary>
        /// <value>
        /// The weather detail.
        /// </value>
        public List<VoyageActivityBadWeatherDetail> WeatherDetail { get; set; }

        /// <summary>
        /// Gets or sets the type of the vessel profile.
        /// </summary>
        /// <value>
        /// The type of the vessel profile.
        /// </value>
        public string VesselProfileType { get; set; }

        /// <summary>
        /// Converts to portid.
        /// </summary>
        /// <value>
        /// To port identifier.
        /// </value>
        public string ToPortId { get; set; }

        /// <summary>
        /// Gets or sets from port identifier.
        /// </summary>
        /// <value>
        /// From port identifier.
        /// </value>
        public string FromPortId { get; set; }

        /// <summary>
        /// Gets or sets the has from port alert.
        /// </summary>
        /// <value>
        /// The has from port alert.
        /// </value>
        public bool? HasFromPortAlert { get; set; }

        /// <summary>
        /// Gets or sets the has to port alert.
        /// </summary>
        /// <value>
        /// The has to port alert.
        /// </value>
        public bool? HasToPortAlert { get; set; }
    }
}
