using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// Breaks And Bad Weather Detail View Model
    /// </summary>
    public class BreaksAndBadWeatherDetailViewModel
    {
        /// <summary>
        /// Gets or sets the spa date.
        /// </summary>
        /// <value>
        /// The spa date.
        /// </value>
        public string SpaDate { get; set; }

        /// <summary>
        /// Gets or sets the break and bad weather list.
        /// </summary>
        /// <value>
        /// The break and bad weather list.
        /// </value>
        public List<BreaksAndBadWeatherListViewModel> BreakAndBadWeatherList { get; set; }

        /// <summary>
        /// Gets or sets the list of breaks.
        /// </summary>
        /// <value>
        /// The list of breaks.
        /// </value>
        public List<VoyageActivityDelayViewModel> ListOfBreaks { get; set; }
    }
}
