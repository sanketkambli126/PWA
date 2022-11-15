using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Planned Maintenance List ViewModel
    /// </summary>
    public class PlannedMaintenanceListViewModel
    {
        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the stage.
        /// </summary>
        /// <value>
        /// The name of the stage.
        /// </value>
        public string StageName { get; set; }

        /// <summary>
        /// Gets or sets the name of the status Ids.
        /// </summary>
        /// <value>
        /// The name of the status Ids.
        /// </value>
        public List<string> StatusIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the selected WB status Ids.
        /// </summary>
        ///  /// <value>
        /// The name of the selected work basket statusids.
        /// </value>
        public string SelectedWBStatusIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the priority ids.
        /// </summary>
        /// <value>
        /// The name of the priority ids.
        /// </value>
        public List<string> PriorityIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the selected  work basket priority ids.
        /// </summary>
        ///  /// <value>
        /// The name of the selected work basket priority ids.
        /// </value>
        public string SelectedWBPriorityIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the responsibility ids.
        /// </summary>
        ///  /// <value>
        /// The name of the responsibility ids.
        /// </value>
        public List<string> ResponsibilityIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the selected work basket responsibility Ids.
        /// </summary>
        ///  /// <value>
        /// The name of the selected work basket responsibility.
        /// </value>
        public string SelectedWBResponsibilityIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the rescheduled Ids.
        /// </summary>
        ///  /// <value>
        /// The name of the rescheduled.
        /// </value>
        public List<string> RescheduledIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the selected work basket rescheduled ids.
        /// </summary>
        ///  /// <value>
        /// The name of the selected work basket rescheduled ids.
        /// </value>
        public string SelectedWBRescheduledIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the is searched click.
        /// </summary>
        ///  /// <value>
        /// The name of the is searched click
        /// </value>
        public bool isSearchedClick { get; set; }

        /// <summary>
        /// Gets or sets the selected wb job type ids.
        /// </summary>
        /// <value>
        /// The selected wb job type ids.
        /// </value>
        public string SelectedWBJobTypeIds { get; set; }

        /// <summary>
        /// Gets or sets the job type ids.
        /// </summary>
        /// <value>
        /// The job type ids.
        /// </value>
        public List<string> JobTypeIds { get; set; }

        /// <summary>
        /// Gets or sets the other filters.
        /// </summary>
        /// <value>
        /// The other filters.
        /// </value>
        public List<string> OtherFilters { get; set; }

        /// <summary>
        /// Gets or sets the selected other filters.
        /// </summary>
        /// <value>
        /// The selected other filters.
        /// </value>
        public string SelectedOtherFilters { get; set; }

        /// <summary>
        /// Gets or sets the parent component identifier.
        /// </summary>
        /// <value>
        /// The parent component identifier.
        /// </value>
        public string ParentComponentId { get; set; }

        /// <summary>
        /// Gets or sets the component identifier.
        /// </summary>
        /// <value>
        /// The component identifier.
        /// </value>
        public string ComponentId { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the top system area identifier.
        /// </summary>
        /// <value>
        /// The top system area identifier.
        /// </value>
        public string TopSystemAreaId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is critical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
        /// </value>
        public bool? IsCritical { get; set; }

        /// <summary>
        /// Gets or sets the active mobile tab class.
        /// </summary>
        /// <value>
        /// The active mobile tab class.
        /// </value>
        public string ActiveMobileTabClass { get; set; }

        /// <summary>
        /// Gets or sets the component title.
        /// </summary>
        /// <value>
        /// The component title.
        /// </value>
        public string ComponentTitle { get; set; }

        /// <summary>
        /// Gets or sets the status title.
        /// </summary>
        /// <value>
        /// The status title.
        /// </value>
        public string StatusTitles { get; set; }

        /// <summary>
        /// Gets or sets the priority title.
        /// </summary>
        /// <value>
        /// The priority title.
        /// </value>
        public string PriorityTitles { get; set; }

        /// <summary>
        /// Gets or sets the responsiblity title.
        /// </summary>
        /// <value>
        /// The responsiblity title.
        /// </value>
        public string ResponsiblityTitles { get; set; }

        /// <summary>
        /// Gets or sets the reschedule title.
        /// </summary>
        /// <value>
        /// The reschedule title.
        /// </value>
        public string RescheduleTitles { get; set; }

        /// <summary>
        /// Gets or sets the job type title.
        /// </summary>
        /// <value>
        /// The job type title.
        /// </value>
        public string JobTypeTitles { get; set; }

        /// <summary>
        /// Gets or sets the other filter title.
        /// </summary>
        /// <value>
        /// The other filter title.
        /// </value>
        public string OtherFilterTitles { get; set; }

        /// <summary>
        /// Gets or sets the grid sub title.
        /// </summary>
        /// <value>
        /// The grid sub title.
        /// </value>
        public string GridSubTitle { get; set; }

    }
}
