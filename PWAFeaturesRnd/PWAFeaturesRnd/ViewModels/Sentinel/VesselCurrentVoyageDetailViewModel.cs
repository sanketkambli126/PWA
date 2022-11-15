using System;

namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Vessel Current Voyage Detail View Model
    /// </summary>
    public class VesselCurrentVoyageDetailViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the activity.
        /// </summary>
        /// <value>
        /// The name of the activity.
        /// </value>
        public string ActivityName { get; set; }

        /// <summary>
        /// Gets or sets from port is alert added.
        /// </summary>
        /// <value>
        /// From port is alert added.
        /// </value>
        public bool? FromPortIsAlertAdded { get; set; }

        /// <summary>
        /// Gets or sets to port is alert added.
        /// </summary>
        /// <value>
        /// To port is alert added.
        /// </value>
        public bool? ToPortIsAlertAdded { get; set; }

        /// <summary>
        /// Gets or sets the next port is alert added.
        /// </summary>
        /// <value>
        /// The next port is alert added.
        /// </value>
        public bool? NextPortIsAlertAdded { get; set; }

        /// <summary>
        /// Gets or sets from port country.
        /// </summary>
        /// <value>
        /// From port country.
        /// </value>
        public string FromPortCountry { get; set; }

        /// <summary>
        /// Converts to portcountry.
        /// </summary>
        /// <value>
        /// To port country.
        /// </value>
        public string ToPortCountry { get; set; }

        /// <summary>
        /// Gets or sets the port date.
        /// </summary>
        /// <value>
        /// The port date.
        /// </value>
        public string PortDate { get; set; }

        /// <summary>
        /// Gets or sets from port date.
        /// </summary>
        /// <value>
        /// From port date.
        /// </value>
        public string FromPortDate { get; set; }

        /// <summary>
        /// Gets or sets the date header.
        /// </summary>
        /// <value>
        /// The date header.
        /// </value>
        public string DateHeader { get; set; }

        /// <summary>
        /// Gets or sets from date header.
        /// </summary>
        /// <value>
        /// From date header.
        /// </value>
        public string FromDateHeader { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public int? Percentage { get; set; }

        /// <summary>
        /// Gets or sets from port request URL.
        /// </summary>
        /// <value>
        /// From port request URL.
        /// </value>
        public string FromPortRequestURL { get; set; }

        /// <summary>
        /// Converts to portrequesturl.
        /// </summary>
        /// <value>
        /// To port request URL.
        /// </value>
        public string ToPortRequestUrl { get; set; }

    }
}