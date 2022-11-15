
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Crew
{
    /// <summary>
    /// Service History View Model
    /// </summary>
    public class ServiceHistoryViewModel
    {
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public string StatusId { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets the type of the vessel.
        /// </summary>
        /// <value>
        /// The type of the vessel.
        /// </value>
        public string VesselType { get; set; }

        /// <summary>
        /// Gets or sets the flag.
        /// </summary>
        /// <value>
        /// The flag.
        /// </value>
        public string Flag { get; set; }

        /// <summary>
        /// Gets or sets the DWT.
        /// </summary>
        /// <value>
        /// The DWT.
        /// </value>
        public int DWT { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the days.
        /// </summary>
        /// <value>
        /// The days.
        /// </value>
        public int Days { get; set; }

        /// <summary>
        /// Gets or sets the engine.
        /// </summary>
        /// <value>
        /// The engine.
        /// </value>
        public string Engine { get; set; }

        /// <summary>
        /// Gets or sets the power.
        /// </summary>
        /// <value>
        /// The power.
        /// </value>
        public int? Power { get; set; }

        /// <summary>
        /// Gets or sets the service active status identifier.
        /// </summary>
        /// <value>
        /// The service active status identifier.
        /// </value>

        public int ServiceActiveStatusId { get; set; }

        /// <summary>
        /// Gets or sets the color of the planning back ground.
        /// </summary>
        /// <value>
        /// The color of the planning back ground.
        /// </value>
        public string PlanningBackGroundColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the planning foreground.
        /// </summary>
        /// <value>
        /// The color of the planning foreground.
        /// </value>
        public string PlanningForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the short color of the code back ground.
        /// </summary>
        /// <value>
        /// The short color of the code back ground.
        /// </value>
        public string ShortCodeBackGroundColor { get; set; }

        /// <summary>
        /// Gets or sets the short color of the code foreground.
        /// </summary>
        /// <value>
        /// The short color of the code foreground.
        /// </value>
        public string ShortCodeForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the planning status short code.
        /// </summary>
        /// <value>
        /// The planning status short code.
        /// </value>
        public string PlanningStatusShortCode { get; set; }

        /// <summary>
        /// Gets or sets the planning status description.
        /// </summary>
        /// <value>
        /// The planning status description.
        /// </value>
        public string PlanningStatusDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is future onshore record.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is future onshore record; otherwise, <c>false</c>.
        /// </value>
        public bool IsFutureOnshoreRecord { get; set; }


    }
}
