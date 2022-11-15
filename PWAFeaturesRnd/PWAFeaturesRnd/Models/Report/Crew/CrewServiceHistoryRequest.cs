using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// 
    /// </summary>
    public class CrewServiceHistoryRequest
    {
        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>
        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets from flight date.
        /// </summary>
        /// <value>
        /// From flight date.
        /// </value>
        public DateTime? FromFlightDate { get; set; }

        /// <summary>
        /// Gets or sets the type of the service status.
        /// </summary>
        /// <value>
        /// The type of the service status.
        /// </value>
        public CrewServiceStatusFilter? ServiceStatusType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is on board service event.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is on board service event; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnBoardServiceEvent { get; set; }

        /// <summary>
        /// Gets or sets the type of the flight.
        /// </summary>
        /// <value>
        /// The type of the flight.
        /// </value>
        public CrewADNOCFlightType? FlightType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can include shore.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can include shore; otherwise, <c>false</c>.
        /// </value>
        public bool CanIncludeShore { get; set; }
    }
}
