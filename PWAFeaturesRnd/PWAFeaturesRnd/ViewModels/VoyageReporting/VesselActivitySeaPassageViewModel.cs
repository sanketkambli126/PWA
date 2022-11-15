using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// 
    /// </summary>
    public class VesselActivitySeaPassageViewModel
    {
        /// <summary>
        /// Gets or sets the activity.
        /// </summary>
        /// <value>
        /// The activity.
        /// </value>
        public string Activity { get; set; }

        /// <summary>
        /// Gets or sets the charterer no.
        /// </summary>
        /// <value>
        /// The charterer no.
        /// </value>
        public string ChartererNo { get; set; }

        /// <summary>
        /// Gets or sets the voyage no.
        /// </summary>
        /// <value>
        /// The voyage no.
        /// </value>
        public string VoyageNo { get; set; }

        /// <summary>
        /// Gets or sets the last reported event.
        /// </summary>
        /// <value>
        /// The last reported event.
        /// </value>
        public DateTime? LastReportedEvent { get; set; }

        /// <summary>
        /// Gets or sets the last24 hour speed.
        /// </summary>
        /// <value>
        /// The last24 hour speed.
        /// </value>
        public double Last24HourSpeed { get; set; }

        /// <summary>
        /// Gets or sets the last24 hour speed consumption.
        /// </summary>
        /// <value>
        /// The last24 hour speed consumption.
        /// </value>
        public double Last24HourSpeedConsumption { get; set; }

        /// <summary>
        /// Gets or sets the voyage average speed.
        /// </summary>
        /// <value>
        /// The voyage average speed.
        /// </value>
        public double VoyageAverageSpeed { get; set; }

        /// <summary>
        /// Gets or sets the voyage average consumption.
        /// </summary>
        /// <value>
        /// The voyage average consumption.
        /// </value>
        public double VoyageAverageConsumption { get; set; }

        /// <summary>
        /// Gets or sets the cp adjusted speed.
        /// </summary>
        /// <value>
        /// The cp adjusted speed.
        /// </value>
        public double CpAdjustedSpeed { get; set; }

        /// <summary>
        /// Gets or sets the cp adjusted consumption.
        /// </summary>
        /// <value>
        /// The cp adjusted consumption.
        /// </value>
        public double CpAdjustedConsumption { get; set; }

        /// <summary>
        /// Gets or sets the cp orders speed.
        /// </summary>
        /// <value>
        /// The cp orders speed.
        /// </value>
        public double CpOrdersSpeed { get; set; }

        /// <summary>
        /// Gets or sets the cp orders consumption.
        /// </summary>
        /// <value>
        /// The cp orders consumption.
        /// </value>
        public double CpOrdersConsumption { get; set; }

        /// <summary>
        /// Gets or sets from status.
        /// </summary>
        /// <value>
        /// From status.
        /// </value>
        public string FromStatus { get; set; }

        /// <summary>
        /// Converts to status.
        /// </summary>
        /// <value>
        /// To status.
        /// </value>
        public string ToStatus { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the current voyage class.
        /// </summary>
        /// <value>
        /// The name of the current voyage class.
        /// </value>
        public string CurrentVoyageClassName { get; set; }

        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        /// <value>
        /// The activity identifier.
        /// </value>
        public string ActivityId { get; set; }

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

        /// <summary>
        /// Gets or sets from port request URL.
        /// </summary>
        /// <value>
        /// From port request URL.
        /// </value>
        public string FromPortRequestUrl { get; set; }

        /// <summary>
        /// Converts to portrequesturl.
        /// </summary>
        /// <value>
        /// To port request URL.
        /// </value>
        public string ToPortRequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the request URL.
        /// </summary>
        /// <value>
        /// The request URL.
        /// </value>
        public string RequestUrl { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel detail.
        /// </summary>
        /// <value>
        /// The encrypted vessel detail.
        /// </value>
        public string EncryptedVesselDetail { get; set; }

        /// <summary>
        /// Gets or sets the channel count.
        /// </summary>
        /// <value>
        /// The channel count.
        /// </value>
        public int ChannelCount { get; set; }

        /// <summary>
        /// Gets or sets the notes count.
        /// </summary>
        /// <value>
        /// The notes count.
        /// </value>
        public int NotesCount { get; set; }

        /// <summary>
        /// Gets or sets the message details json.
        /// </summary>
        /// <value>
        /// The message details json.
        /// </value>
        public string MessageDetailsJSON { get; set; }

        /// <summary>
        /// Converts FromAgentRequestURL.
        /// </summary>
        /// <value>
        /// FromAgentRequestURL
        /// </value>
        public string FromAgentRequestURL { get; set; }

        /// <summary>
        /// Converts ToAgentRequestURL.
        /// </summary>
        /// <value>
        /// ToAgentRequestURL.
        /// </value>
        public string ToAgentRequestURL { get; set; }

        /// <summary>
        /// The from port name
        /// </summary>
        public string FromPort { get; set; }

        /// <summary>
        /// The to port name
        /// </summary>
        public string ToPort { get; set; }


    }
}
