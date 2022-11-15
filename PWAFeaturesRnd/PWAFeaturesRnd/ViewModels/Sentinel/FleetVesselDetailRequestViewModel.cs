namespace PWAFeaturesRnd.ViewModels.Sentinel
{
	public class FleetVesselDetailRequestViewModel
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
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the model dimension identifier.
		/// </summary>
		/// <value>
		/// The model dimension identifier.
		/// </value>
		public string ModelDimensionId { get; set; }

		/// <summary>
		/// Gets or sets the override dimension identifier.
		/// </summary>
		/// <value>
		/// The override dimension identifier.
		/// </value>
		public string OverrideDimensionId { get; set; }

		/// <summary>
		/// Gets or sets the color status.
		/// </summary>
		/// <value>
		/// The color status.
		/// </value>
		public string ColorStatus { get; set; }

		/// <summary>
		/// Gets or sets the biggest mover range.
		/// </summary>
		/// <value>
		/// The biggest mover range.
		/// </value>
		public int? BiggestMoverRange { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [consider override score category].
		/// </summary>
		/// <value>
		///   <c>true</c> if [consider override score category]; otherwise, <c>false</c>.
		/// </value>
		public bool ConsiderOverrideScoreCategory { get; set; }

		/// <summary>
		/// Gets or sets the name of the office.
		/// </summary>
		/// <value>
		/// The name of the office.
		/// </value>
		public string OfficeName { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the sort by.
		/// </summary>
		/// <value>
		/// The sort by.
		/// </value>
		public int SortBy { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }
	}
}
