namespace PWAFeaturesRnd.ViewModels.PurchaseOrder
{
    /// <summary>
    /// Summary Purchase order component viewmodel
    /// </summary>
    public class SummaryPurchaseOrderComponentViewModel
    {
        /// <summary>
        /// Gets or sets the name of the component.
        /// </summary>
        /// <value>
        /// The name of the component.
        /// </value>
        public string ComponentName { get; set; }

        /// <summary>
        /// Gets or sets the maker.
        /// </summary>
        /// <value>
        /// The maker.
        /// </value>
        public string Maker { get; set; }

        /// <summary>
        /// Gets or sets the serial.
        /// </summary>
        /// <value>
        /// The serial.
        /// </value>
        public string Serial { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the designer.
        /// </summary>
        /// <value>
        /// The designer.
        /// </value>
        public string Designer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [critical component].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [critical component]; otherwise, <c>false</c>.
        /// </value>
        public bool CriticalComponent { get; set; }
    }
}
