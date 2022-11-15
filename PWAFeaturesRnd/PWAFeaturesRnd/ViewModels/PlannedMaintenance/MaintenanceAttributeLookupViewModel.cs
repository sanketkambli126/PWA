namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// The maintenance attribute lookup viewmodel
    /// </summary>
    public class MaintenanceAttributeLookupViewModel
    {

        /// <summary>
        /// Gets or sets the attribute identifier.
        /// </summary>
        /// <value>
        /// The attribute identifier.
        /// </value>
        public string AttributeId { get; set; }

        /// <summary>
        /// Gets or sets the attribute name.
        /// </summary>
        /// <value>
        /// The attribute name.
        /// </value>
        public string AttributeName { get; set; }

        /// <summary>
        /// Gets or sets the attribute description.
        /// </summary>
        /// <value>
        /// The attribute description.
        /// </value>
        public string AttributeDescription { get; set; }

        /// <summary>
        /// Gets or sets the path button image.
        /// </summary>
        /// <value>
        /// The path button image.
        /// </value>
        public string PathButtonImage { get; set; }

        /// <summary>
        /// Gets or sets the lookup code.
        /// </summary>
        /// <value>
        /// The LookupCode
        /// </value>
        public string LookupCode { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The SortOrder
        /// </value>
        public int SortOrder { get; set; }
    }
}
