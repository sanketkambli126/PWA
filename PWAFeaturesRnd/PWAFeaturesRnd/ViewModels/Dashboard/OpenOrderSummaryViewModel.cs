using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.ViewModels.Dashboard
{
	public class OpenOrderSummaryViewModel
	{
        /// <summary>
        /// Gets or sets the selected user menu item.
        /// </summary>
        /// <value>
        /// The selected user menu item.
        /// </value>
        UserMenuItem SelectedUserMenuItem { get; set; }

        /// <summary>
        /// Gets or sets the order in process count.
        /// </summary>
        /// <value>
        /// The order in process count.
        /// </value>
        public int OrderInProcessCount { get; set; }

        /// <summary>
        /// Gets or sets the ordered count.
        /// </summary>
        /// <value>
        /// The ordered count.
        /// </value>
        public int OrderedCount { get; set; }

        /// <summary>
        /// Gets or sets the order delivery on the way count.
        /// </summary>
        /// <value>
        /// The order delivery on the way count.
        /// </value>
        public int OrderDeliveryOnTheWayCount { get; set; }

        /// <summary>
        /// Gets or sets the requested information count.
        /// </summary>
        /// <value>
        /// The requested information count.
        /// </value>
        public int RequestedInformationCount { get; set; }

        /// <summary>
        /// Gets or sets the authorisation count.
        /// </summary>
        /// <value>
        /// The authorisation count.
        /// </value>
        public int AuthorisationCount { get; set; }

        /// <summary>
        /// Gets or sets the recieved in30 days count.
        /// </summary>
        /// <value>
        /// The recieved in30 days count.
        /// </value>
        public int RecievedIn30DaysCount { get; set; }
    }
}
