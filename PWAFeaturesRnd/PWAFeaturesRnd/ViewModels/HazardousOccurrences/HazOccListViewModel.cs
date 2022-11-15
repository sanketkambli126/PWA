using System;

namespace PWAFeaturesRnd.ViewModels.HazardousOccurrences
{
    /// <summary>
    /// HazOccListViewModel
    /// </summary>
    public class HazOccListViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }
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
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the name of the stage.
        /// </summary>
        /// <value>
        /// The name of the stage.
        /// </value>
        public string StageName { get; set; }

        /// <summary>
        /// Gets or sets the selected incident types.
        /// </summary>
        /// <value>
        /// The selected incident types.
        /// </value>
        public string SelectedIncidentTypes { get; set; }

        /// <summary>
        /// Gets or sets the selected incident severitys.
        /// </summary>
        /// <value>
        /// The selected incident severitys.
        /// </value>
        public string SelectedIncidentSeveritys { get; set; }

        /// <summary>
        /// Gets or sets the selected incident status.
        /// </summary>
        /// <value>
        /// The selected incident status.
        /// </value>
        public string SelectedIncidentStatus { get; set; }

        /// <summary>
        /// Gets or sets the clear button URL.
        /// </summary>
        /// <value>
        /// The clear button URL.
        /// </value>
        public string ClearButtonUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is searched click.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is searched click; otherwise, <c>false</c>.
        /// </value>
        public bool IsSearchedClick { get; set; }

        /// <summary>
        /// Gets or sets the active mobile tab class.
        /// </summary>
        /// <value>
        /// The active mobile tab class.
        /// </value>
        public string ActiveMobileTabClass { get; set; }

        /// <summary>
        /// Gets or sets the grid sub title.
        /// </summary>
        /// <value>
        /// The grid sub title.
        /// </value>
        public string GridSubTitle { get; set; }

        /// <summary>
        /// Gets or sets the stage description.
        /// </summary>
        /// <value>
        /// The stage description.
        /// </value>
        public string StageDescription { get; set; }


    }
}
