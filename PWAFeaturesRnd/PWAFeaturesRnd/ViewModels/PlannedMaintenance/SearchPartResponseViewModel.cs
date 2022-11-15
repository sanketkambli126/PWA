namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Search Part Response ViewModel
    /// </summary>
    public class SearchPartResponseViewModel
    {
        /// <summary>
        /// Gets or sets the name of the part.
        /// </summary>
        /// <value>
        /// The name of the part.
        /// </value>
        public string PartName { get; set; }

        /// <summary>
        /// Gets or sets the maker reference number.
        /// </summary>
        /// <value>
        /// The maker reference number.
        /// </value>
        public string MakerReferenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the plate sheet number.
        /// </summary>
        /// <value>
        /// The plate sheet number.
        /// </value>
        public string PlateSheetNumber { get; set; }

        /// <summary>
        /// Gets or sets the drawing position.
        /// </summary>
        /// <value>
        /// The drawing position.
        /// </value>
        public string DrawingPosition { get; set; }

        /// <summary>
        /// Gets or sets the pending order count.
        /// </summary>
        /// <value>
        /// The pending order count.
        /// </value>
        public int? PendingOrderCount { get; set; }

        /// <summary>
        /// Gets or sets the quantity rob.
        /// </summary>
        /// <value>
        /// The quantity rob.
        /// </value>
        public int? QuantityROB { get; set; }

        /// <summary>
        /// Gets or sets the is renew spares.
        /// </summary>
        /// <value>
        /// The is renew spares.
        /// </value>
        public string IsRenewSpares { get; set; }

        /// <summary>
        /// Gets or sets the quantity required.
        /// </summary>
        /// <value>
        /// The quantity required.
        /// </value>
        public float? QuantityRequired { get; set; }

        /// <summary>
        /// Gets or sets the is marked for reorder.
        /// </summary>
        /// <value>
        /// The is marked for reorder.
        /// </value>
        public string IsMarkedForReorder { get; set; }

        /// <summary>
        /// Gets or sets the reorder quantity.
        /// </summary>
        /// <value>
        /// The reorder quantity.
        /// </value>
        public int? ReorderQuantity { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is quantity required greater than rob.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is quantity required greater than rob; otherwise, <c>false</c>.
        /// </value>
        public bool IsQuantityRequiredGreaterThanROB { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public string Condition { get; set; }

        /// <summary>
        /// Gets or sets the quantity used.
        /// </summary>
        /// <value>
        /// The quantity used.
        /// </value>
        public int? QuantityUsed { get; set; }
    }
}
