using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    /// <summary>
    /// JSAListViewModel
    /// </summary>
    public class JSAListViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the active mobile tab class.
        /// </summary>
        /// <value>
        /// The active mobile tab class.
        /// </value>
        public string ActiveMobileTabClass { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is search clicked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is search clicked; otherwise, <c>false</c>.
        /// </value>
        public bool IsSearchClicked { get; set; }

        /// <summary>
        /// Gets or sets the name of the stage.
        /// </summary>
        /// <value>
        /// The name of the stage.
        /// </value>
        public string StageName { get; set; }

        /// <summary>
        /// Gets or sets the selected status.
        /// </summary>
        /// <value>
        /// The selected status.
        /// </value>
        public List<string> SelectedStatus { get; set; }

        /// <summary>
        /// Gets or sets the selected status.
        /// </summary>
        /// <value>
        /// The selected status.
        /// </value>
        public List<string> SelectedSystemArea { get; set; }

        /// <summary>
        /// Gets or sets the selected status ids.
        /// </summary>
        /// <value>
        /// The selected status ids.
        /// </value>
        public string SelectedStatusIds { get; set; }

        /// <summary>
        /// Gets or sets the selected status ids.
        /// </summary>
        /// <value>
        /// The selected status ids.
        /// </value>
        public string SelectedSystemAreaIds { get; set; }

        /// <summary>
        /// Gets or sets the selected status ids.
        /// </summary>
        /// <value>
        /// The selected status ids.
        /// </value>
        public string SelectedRiskFilterIds { get; set; }

        /// <summary>
        /// Gets or sets the selected risk filter.
        /// </summary>
        /// <value>
        /// The selected risk filter.
        /// </value>
        public List<string> SelectedRiskFilter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [overdue for closure].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [overdue for closure]; otherwise, <c>false</c>.
        /// </value>
        public bool OverdueForClosure { get; set; }


        /// <summary>
        /// Gets or sets the Grid Sub Title.
        /// </summary>
        /// <value>
        /// The Grid Sub Title.
        /// </value>
        public string GridSubTitle { get; set; }
    }
}
