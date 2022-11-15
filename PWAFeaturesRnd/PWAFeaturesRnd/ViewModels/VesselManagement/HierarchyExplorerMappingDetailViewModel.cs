namespace PWAFeaturesRnd.ViewModels.VesselManagement
{
    /// <summary>
    /// The hierarchy explorer mapping detail view model
    /// </summary>
    public class HierarchyExplorerMappingDetailViewModel
    {
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the maker.
        /// </summary>
        /// <value>
        /// The maker.
        /// </value>

        public string Maker { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>

        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the designer.
        /// </summary>
        /// <value>
        /// The designer.
        /// </value>

        public string Designer { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>

        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the system area.
        /// </summary>
        /// <value>
        /// The system area.
        /// </value>

        public string SystemArea { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is critical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is critical; otherwise, <c>false</c>.
        /// </value>

        public bool IsCritical { get; set; }

        /// <summary>
        /// Gets or sets the class code.
        /// </summary>
        /// <value>
        /// The class code.
        /// </value>
        public string ClassCode { get; set; }

    }
}
