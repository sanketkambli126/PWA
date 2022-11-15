using System;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    public class PortEngineRunningHoursViewModel
    {

        /// <summary>
        /// Gets or sets the name of the part.
        /// </summary>
        /// <value>
        /// The name of the part.
        /// </value>
        public string PartName { get; set; }

        /// <summary>
        /// Gets or sets the PRH identifier.
        /// </summary>
        /// <value>
        /// The PRH identifier.
        /// </value>
        public string PrhId { get; set; }

        /// <summary>
        /// Gets or sets the daily.
        /// </summary>
        /// <value>
        /// The daily.
        /// </value>
        public decimal? Daily { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public decimal? Total { get; set; }

        /// <summary>
        /// Gets or sets the previous hours.
        /// </summary>
        /// <value>
        /// The previous hours.
        /// </value>
        public decimal? PreviousHours { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

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
        /// Gets or sets the PSF identifier.
        /// </summary>
        /// <value>
        /// The PSF identifier.
        /// </value>
        public string PsfId { get; set; }

        /// <summary>
        /// Gets or sets the index of the sort.
        /// </summary>
        /// <value>
        /// The index of the sort.
        /// </value>
        public int SortIndex { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets the todate.
        /// </summary>
        /// <value>
        /// The todate.
        /// </value>
        public DateTime? Todate { get; set; }

        /// <summary>
        /// Gets or sets the power output.
        /// </summary>
        /// <value>
        /// The power output.
        /// </value>
        public decimal? PowerOutput { get; set; }
    }
}
