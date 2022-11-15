using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// 
    /// </summary>
    public class SeaPassageSummaryViewModel
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
        public List<VoyageActivityBadWeatherDetailViewModel> BadWeatherDetails { get; set; }

        /// <summary>
        /// Gets or sets the type of the vessel profile.
        /// </summary>
        /// <value>
        /// The type of the vessel profile.
        /// </value>
        public string VesselProfileType { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        /// <value>
        /// The total distance.
        /// </value>
        public decimal? SailedDistance { get; set; }

        /// <summary>
        /// Gets or sets the type of from event.
        /// </summary>
        /// <value>
        /// The type of from event.
        /// </value>
        public string FromEventType { get; set; }

        /// <summary>
        /// Converts to eventtype.
        /// </summary>
        /// <value>
        /// The type of to event.
        /// </value>
        public string ToEventType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sea passage event.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sea passage event; otherwise, <c>false</c>.
        /// </value>
        public bool IsSeaPassageEvent { get; set; }

        /// <summary>
        /// Gets or sets the last event position.
        /// </summary>
        /// <value>
        /// The last event position.
        /// </value>
        public string LastEventPosition { get; set; }
        
        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        /// <value>
        /// The request URL.
        /// </value>
        public string RequestURL { get; set; }

        /// <summary>
        /// Converts to porturl.
        /// </summary>
        /// <value>
        /// To port URL.
        /// </value>
        public string ToPortURL { get; set; }

        /// <summary>
        /// Gets or sets from port URL.
        /// </summary>
        /// <value>
        /// From port URL.
        /// </value>
        public string FromPortURL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has from port alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has from port alert; otherwise, <c>false</c>.
        /// </value>
        public bool HasFromPortAlert { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has to port alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has to port alert; otherwise, <c>false</c>.
        /// </value>
        public bool HasToPortAlert { get; set; }
    }
}
