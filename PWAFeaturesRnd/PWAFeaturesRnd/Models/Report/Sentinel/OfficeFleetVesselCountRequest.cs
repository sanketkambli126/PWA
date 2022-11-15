namespace PWAFeaturesRnd.Models.Report.Sentinel
{
    /// <summary>
    /// Office Fleet Vessel Count Request
    /// </summary>
    public class OfficeFleetVesselCountRequest
    {
        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public string MenuType { get; set; }

        /// <summary>
        /// Gets or sets the fleet identifier.
        /// </summary>
        /// <value>
        /// The fleet identifier.
        /// </value>
        public string FleetId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the fleet.
        /// </summary>
        /// <value>
        /// The name of the fleet.
        /// </value>
        public string FleetName { get; set; }

        /// <summary>
        /// Gets or sets the color status.
        /// </summary>
        /// <value>
        /// The color status.
        /// </value>
        public string ColorStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [consider override score category].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [consider override score category]; otherwise, <c>false</c>.
        /// </value>
        public bool ConsiderOverrideScoreCategory { get; set; }

        /// <summary>
        /// Gets or sets the sort by.
        /// </summary>
        /// <value>
        /// The sort by.
        /// </value>
        public int SortBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is all selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is all selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsAllSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [get fleet list].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [get fleet list]; otherwise, <c>false</c>.
        /// </value>
        public bool GetFleetList { get; set; }
    }
}
