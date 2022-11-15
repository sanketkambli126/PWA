namespace PWAFeaturesRnd.ViewModels.Common
{
	/// <summary>
	/// Vessel Preview View Model
	/// </summary>
	public class VesselPreviewViewModel
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the imo.
		/// </summary>
		/// <value>
		/// The imo.
		/// </value>
		public string Imo { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the vessel built date.
		/// </summary>
		/// <value>
		/// The vessel built date.
		/// </value>
		public string VesselBuiltDate { get; set; }

		/// <summary>
		/// Gets or sets the vessel age.
		/// </summary>
		/// <value>
		/// The vessel age.
		/// </value>
		public string VesselAge { get; set; }

		/// <summary>
		/// Gets or sets the flag.
		/// </summary>
		/// <value>
		/// The flag.
		/// </value>
		public string Flag { get; set; }

		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>
		/// The image.
		/// </value>
		public string image { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is vessel in management.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vessel in management; otherwise, <c>false</c>.
        /// </value>
        public bool IsVesselInManagement { get; set; }
    }
}
