using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    public class SeaPassageDetail
    {
        /// <summary>
        /// Gets or sets the voyage running hour list.
        /// </summary>
        /// <value>
        /// The voyage running hour list.
        /// </value>
        public List<VoyageRunningHour> VoyageRunningHourList { get; set; }
        /// <summary>
        /// Gets or sets the position sea passage entity.
        /// </summary>
        /// <value>
        /// The position sea passage entity.
        /// </value>
        public FaopDetail PosSeaPassageEntity { get; set; }
        /// <summary>
        /// Gets or sets the event details.
        /// </summary>
        /// <value>
        /// The event details.
        /// </value>
        public EventXMLDetails EventDetails { get; set; }
    }
}
