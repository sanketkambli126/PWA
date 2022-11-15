
using System;

namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    ///  Sentinel Dashboard Vessel Current VoyageDetail
    /// </summary>
    public class SentinelDashboardVesselCurrentVoyageDetail
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the pla identifier.
        /// </summary>
        /// <value>
        /// The pla identifier.
        /// </value>
        public string PlaId { get; set; }

        /// <summary>
        /// Gets or sets the name of the activity.
        /// </summary>
        /// <value>
        /// The name of the activity.
        /// </value>
        public string ActivityName { get; set; }

        /// <summary>
        /// Gets or sets from port identifier.
        /// </summary>
        /// <value>
        /// From port identifier.
        /// </value>
        public string FromPortId { get; set; }

        /// <summary>
        /// Gets or sets the name of from port.
        /// </summary>
        /// <value>
        /// The name of from port.
        /// </value>
        public string FromPortName { get; set; }

        /// <summary>
        /// Gets or sets from count code.
        /// </summary>
        /// <value>
        /// From count code.
        /// </value>
        public string FromCntCode { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets from port is alert added.
        /// </summary>
        /// <value>
        /// From port is alert added.
        /// </value>
        public bool? FromPortIsAlertAdded { get; set; }

        /// <summary>
        /// Gets or sets to port identifier.
        /// </summary>
        /// <value>
        /// To port identifier.
        /// </value>
        public string ToPortId { get; set; }

        /// <summary>
        /// Gets or sets the name of to port.
        /// </summary>
        /// <value>
        /// The name of to port.
        /// </value>
        public string ToPortName { get; set; }

        /// <summary>
        /// Gets or sets to count code.
        /// </summary>
        /// <value>
        /// To count code.
        /// </value>
        public string ToCntCode { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets to port is alert added.
        /// </summary>
        /// <value>
        /// To port is alert added.
        /// </value>
        public bool? ToPortIsAlertAdded { get; set; }

        /// <summary>
        /// Gets or sets the next port identifier.
        /// </summary>
        /// <value>
        /// The next port identifier.
        /// </value>
        public string NextPortId { get; set; }

        /// <summary>
        /// Gets or sets the name of the next port.
        /// </summary>
        /// <value>
        /// The name of the next port.
        /// </value>
        public string NextPortName { get; set; }

        /// <summary>
        /// Gets or sets the next count code.
        /// </summary>
        /// <value>
        /// The next count code.
        /// </value>
        public string NextCntCode { get; set; }

        /// <summary>
        /// Gets or sets the next date.
        /// </summary>
        /// <value>
        /// The next date.
        /// </value>
        public DateTime? NextDate { get; set; }

        /// <summary>
        /// Gets or sets the next port is alert added.
        /// </summary>
        /// <value>
        /// The next port is alert added.
        /// </value>
        public bool? NextPortIsAlertAdded { get; set; }

        /// <summary>
        /// Gets or sets the berth status.
        /// </summary>
        /// <value>
        /// The berth status.
        /// </value>
        public string BerthStatus { get; set; }

        /// <summary>
        /// Gets or sets the berth date.
        /// </summary>
        /// <value>
        /// The berth date.
        /// </value>
        public DateTime? BerthDate { get; set; }

        /// <summary>
        /// Gets or sets the un berth status.
        /// </summary>
        /// <value>
        /// The un berth status.
        /// </value>
        public string UnBerthStatus { get; set; }

        /// <summary>
        /// Gets or sets the un berth date.
        /// </summary>
        /// <value>
        /// The un berth date.
        /// </value>
        public DateTime? UnBerthDate { get; set; }

        /// <summary>
        /// Gets or sets the eosp status.
        /// </summary>
        /// <value>
        /// The eosp status.
        /// </value>
        public string EospStatus { get; set; }

        /// <summary>
        /// Gets or sets the eosp date.
        /// </summary>
        /// <value>
        /// The eosp date.
        /// </value>
        public DateTime? EospDate { get; set; }

        /// <summary>
        /// Gets or sets the faop status.
        /// </summary>
        /// <value>
        /// The faop status.
        /// </value>
        public string FaopStatus { get; set; }

        /// <summary>
        /// Gets or sets the faop date.
        /// </summary>
        /// <value>
        /// The faop date.
        /// </value>
        public DateTime? FaopDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display additional information].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [display additional information]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplayAdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        /// <value>
        /// The total distance.
        /// </value>
        public decimal? TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the distance travelled.
        /// </summary>
        /// <value>
        /// The distance travelled.
        /// </value>
        public decimal? DistanceTravelled { get; set; }
    }
}
