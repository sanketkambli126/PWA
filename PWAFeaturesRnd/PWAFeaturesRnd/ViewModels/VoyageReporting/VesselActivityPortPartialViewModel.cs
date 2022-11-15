using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// 
    /// </summary>
    public class VesselActivityPortPartialViewModel
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
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>
        /// The name of the port.
        /// </value>
        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the port country.
        /// </summary>
        /// <value>
        /// The port country.
        /// </value>
        public string PortCountry { get; set; }
        /// <summary>
        /// Gets or sets the port loc code.
        /// </summary>
        /// <value>
        /// The port loc code.
        /// </value>
        public string PortLocCode { get; set; }
        /// <summary>
        /// Gets or sets the port key hub.
        /// </summary>
        /// <value>
        /// The port key hub.
        /// </value>
        public string PortKeyHub { get; set; }
        /// <summary>
        /// Gets or sets the port latitude.
        /// </summary>
        /// <value>
        /// The port latitude.
        /// </value>
        public string PortLatitude { get; set; }
        /// <summary>
        /// Gets or sets the port longitude.
        /// </summary>
        /// <value>
        /// The port longitude.
        /// </value>
        public string PortLongitude { get; set; }

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
        /// Gets or sets the position date1.
        /// </summary>
        /// <value>
        /// The position date1.
        /// </value>
        public string PosDate1 { get; set; }
        /// <summary>
        /// Gets or sets the position date2.
        /// </summary>
        /// <value>
        /// The position date2.
        /// </value>
        public string PosDate2 { get; set; }
        /// <summary>
        /// Gets or sets the position date3.
        /// </summary>
        /// <value>
        /// The position date3.
        /// </value>
        public string PosDate3 { get; set; }
        /// <summary>
        /// Gets or sets the position date4.
        /// </summary>
        /// <value>
        /// The position date4.
        /// </value>
        public string PosDate4 { get; set; }

        /// <summary>
        /// Gets or sets the position status1.
        /// </summary>
        /// <value>
        /// The position status1.
        /// </value>
        public string PosStatus1 { get; set; }
        /// <summary>
        /// Gets or sets the position status2.
        /// </summary>
        /// <value>
        /// The position status2.
        /// </value>
        public string PosStatus2 { get; set; }
        /// <summary>
        /// Gets or sets the position status3.
        /// </summary>
        /// <value>
        /// The position status3.
        /// </value>
        public string PosStatus3 { get; set; }
        /// <summary>
        /// Gets or sets the position status4.
        /// </summary>
        /// <value>
        /// The position status4.
        /// </value>
        public string PosStatus4 { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent1.
        /// </summary>
        /// <value>
        /// The name of the agent1.
        /// </value>
        public string Agent1Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent2.
        /// </summary>
        /// <value>
        /// The name of the agent2.
        /// </value>
        public string Agent2Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the agent3.
        /// </summary>
        /// <value>
        /// The name of the agent3.
        /// </value>
        public string Agent3Name { get; set; }

        /// <summary>
        /// Gets or sets the agent count.
        /// </summary>
        /// <value>
        /// The agent count.
        /// </value>
        public int AgentCount { get; set; }

        /// <summary>
        /// Gets or sets the type of the agent1.
        /// </summary>
        /// <value>
        /// The type of the agent1.
        /// </value>
        public string Agent1Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the agent2.
        /// </summary>
        /// <value>
        /// The type of the agent2.
        /// </value>
        public string Agent2Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the agent3.
        /// </summary>
        /// <value>
        /// The type of the agent3.
        /// </value>
        public string Agent3Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is port alert.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is port alert; otherwise, <c>false</c>.
        /// </value>
        public bool IsPortAlert { get; set; }

        /// <summary>
        /// Gets or sets the port identifier.
        /// </summary>
        /// <value>
        /// The port identifier.
        /// </value>
        public string PortId { get; set; }

        /// <summary>
        /// Gets or sets the activity identifier.
        /// </summary>
        /// <value>
        /// The activity identifier.
        /// </value>
        public string ActivityId { get; set; }

        /// <summary>
        /// Gets or sets the port request URL.
        /// </summary>
        /// <value>
        /// The port request URL.
        /// </value>
        public string PortRequestUrl { get; set; }

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

    }
}
