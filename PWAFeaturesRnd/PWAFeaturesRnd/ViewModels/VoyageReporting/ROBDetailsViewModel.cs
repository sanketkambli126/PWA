using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    public class ROBDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the tank capacity.
        /// </summary>
        /// <value>
        /// The tank capacity.
        /// </value>
        public decimal? TankCapacity { get; set; }

        /// <summary>
        /// Gets or sets the text box previous rob.
        /// </summary>
        /// <value>
        /// The text box previous rob.
        /// </value>
        public string TextBoxPreviousROB { get; set; }

        /// <summary>
        /// Gets or sets the production qty.
        /// </summary>
        /// <value>
        /// The production qty.
        /// </value>
        public decimal? ProductionQty { get; set; }

        /// <summary>
        /// Gets or sets the consumption qty.
        /// </summary>
        /// <value>
        /// The consumption qty.
        /// </value>
        public decimal? ConsumptionQty { get; set; }

        /// <summary>
        /// Gets or sets the rob.
        /// </summary>
        /// <value>
        /// The rob.
        /// </value>
        public decimal? ROB { get; set; }

        /// <summary>
        /// Gets or sets the previous rob.
        /// </summary>
        /// <value>
        /// The previous rob.
        /// </value>
        public decimal? PreviousROB { get; set; }

        /// <summary>
        /// Gets or sets the formatted percentage.
        /// </summary>
        /// <value>
        /// The formatted percentage.
        /// </value>
        public string FormattedPercentage { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage.
        /// </value>
        public string Percentage { get; set; }
    }
}
