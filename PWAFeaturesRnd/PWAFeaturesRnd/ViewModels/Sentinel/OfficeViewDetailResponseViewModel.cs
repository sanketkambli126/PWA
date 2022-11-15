namespace PWAFeaturesRnd.ViewModels.Sentinel
{
	/// <summary>
	/// Office View Detail Response View Model
	/// </summary>
	public class OfficeViewDetailResponseViewModel
	{
		/// <summary>
		/// Gets or sets the office identifier.
		/// </summary>
		/// <value>
		/// The office identifier.
		/// </value>
		public string OfficeId { get; set; }

		/// <summary>
		/// Gets or sets the name of the office.
		/// </summary>
		/// <value>
		/// The name of the office.
		/// </value>
		public string OfficeName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has active override.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has active override; otherwise, <c>false</c>.
		/// </value>
		public bool HasActiveOverride { get; set; }

		/// <summary>
		/// Gets or sets the total vessel count.
		/// </summary>
		/// <value>
		/// The total vessel count.
		/// </value>
		public int TotalVesselCount { get; set; }

		/// <summary>
		/// Gets or sets the grey vessel count.
		/// </summary>
		/// <value>
		/// The grey vessel count.
		/// </value>
		public int GreyVesselCount { get; set; }

		/// <summary>
		/// Gets or sets the red vessel count.
		/// </summary>
		/// <value>
		/// The red vessel count.
		/// </value>
		public int RedVesselCount { get; set; }

		/// <summary>
		/// Gets or sets the amber vessel count.
		/// </summary>
		/// <value>
		/// The amber vessel count.
		/// </value>
		public int AmberVesselCount { get; set; }

		/// <summary>
		/// Gets or sets the green vessel count.
		/// </summary>
		/// <value>
		/// The green vessel count.
		/// </value>
		public int GreenVesselCount { get; set; }

        /// <summary>
        /// Gets or sets the fleet request.
        /// </summary>
        /// <value>
        /// The fleet request.
        /// </value>
        public string FleetRequest { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel list request red.
        /// </summary>
        /// <value>
        /// The encrypted vessel list request red.
        /// </value>
        public string EncryptedVesselListRequestRed { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel list request amber.
        /// </summary>
        /// <value>
        /// The encrypted vessel list request amber.
        /// </value>
        public string EncryptedVesselListRequestAmber { get; set; }

        /// <summary>
        /// Gets or sets the encrypted fleet vessel list request green.
        /// </summary>
        /// <value>
        /// The encrypted fleet vessel list request green.
        /// </value>
        public string EncryptedFleetVesselListRequestGreen { get; set; }
	}
}
