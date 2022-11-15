namespace PWAFeaturesRnd.ViewModels.Sentinel
{
    /// <summary>
    /// Override Dimension Vessel Response View Model
    /// </summary>
    public class OverrideDimensionVesselResponseViewModel
    {
        /// <summary>
        /// Gets or sets the parent model dimension description.
        /// </summary>
        /// <value>
        /// The parent model dimension description.
        /// </value>
        public string ParentModelDimensionDescription { get; set; }

        /// <summary>
        /// Gets or sets the model dimension description.
        /// </summary>
        /// <value>
        /// The model dimension description.
        /// </value>
        public string ModelDimensionDescription { get; set; }

        /// <summary>
        /// Gets or sets the override dimension.
        /// </summary>
        /// <value>
        /// The override dimension.
        /// </value>
        public string OverrideDimension { get; set; }

        /// <summary>
        /// Gets or sets the vessel count.
        /// </summary>
        /// <value>
        /// The vessel count.
        /// </value>
        public int VesselCount { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel list request.
        /// </summary>
        /// <value>
        /// The encrypted vessel list request.
        /// </value>
        public string EncryptedVesselListRequest { get; set; }

        /// <summary>
        /// Gets or sets the fleet request.
        /// </summary>
        /// <value>
        /// The fleet request.
        /// </value>
        public string FleetRequest { get; set; }
    }
}
