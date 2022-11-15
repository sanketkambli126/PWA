using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// Voyage Reporting Request View Model
    /// </summary>
    public class VoyageReportingRequestViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel detail.
        /// </summary>
        /// <value>
        /// The encrypted vessel detail.
        /// </value>
        public string EncryptedVesselDetail { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public UserMenuItemType MenuType { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime ToDate { get; set; }

        /// <summary>
        /// Gets or sets the position list identifier.
        /// </summary>
        /// <value>
        /// The position list identifier.
        /// </value>
        public string PositionListId { get; set; }

        /// <summary>
        /// Gets or sets the list URL.
        /// </summary>
        /// <value>
        /// The list URL.
        /// </value>
        public string ListURL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is vessel loaded flag.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vessel loaded flag; otherwise, <c>false</c>.
        /// </value>
        public bool IsVesselLoadedFlag { get; set; }

        /// <summary>
        /// Gets or sets the port identifier.
        /// </summary>
        /// <value>
        /// The port identifier.
        /// </value>
        public string PortId { get; set; }

        /// <summary>
        /// Gets or sets the schedule activities.
        /// </summary>
        /// <value>
        /// The schedule activities.
        /// </value>
        public List<VoyageActivityReportViewModel> ScheduleActivities { get; set; }

        /// <summary>
        /// Gets or sets the current activity.
        /// </summary>
        /// <value>
        /// The current activity.
        /// </value>
        public VoyageActivityReportViewModel CurrentActivity { get; set; }

        /// <summary>
        /// Gets or sets the previous activities.
        /// </summary>
        /// <value>
        /// The previous activities.
        /// </value>
        public List<VoyageActivityReportViewModel> PreviousActivities { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is from fuel efficiency.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is from fuel efficiency; otherwise, <c>false</c>.
        /// </value>
        public bool IsFromFuelEfficiency { get; set; }

        /// <summary>
        /// Gets or sets the fleet tracker URL.
        /// </summary>
        /// <value>
        /// The fleet tracker URL.
        /// </value>
        public string FleetTrackerURL { get; set; }

		/// <summary>
		/// Gets or sets the message details json.
		/// </summary>
		/// <value>
		/// The message details json.
		/// </value>
		public string MessageDetailsJSON { get; set; }

        /// <summary>
        /// Gets or sets the active mobile tab class.
        /// </summary>
        /// <value>
        /// The active mobile tab class.
        /// </value>
        public string ActiveMobileTabClass { get; set; }

        /// <summary>
        /// Gets or sets the NextActivityId.
        /// </summary>
        /// <value>
        /// The NextActivityId.
        /// </value>
        public string NextActivityId { get; set; }

        /// <summary>
        /// Gets or sets the NextActivityId.
        /// </summary>
        /// <value>
        /// The active PreviousActivityId.
        /// </value>
        public string PreviousActivityId { get; set; }
    }
}
