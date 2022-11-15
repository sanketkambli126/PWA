namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// Sea Passage Fuel Consumption Details ViewModel
    /// </summary>
    public class SeaPassageFuelConsumptionDetailsViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the do value.
        /// </summary>
        /// <value>
        /// The do value.
        /// </value>
        public decimal SecondItemCharterValue { get; set; }

        /// <summary>
        /// Gets or sets the do actual value.
        /// </summary>
        /// <value>
        /// The do actual value.
        /// </value>
        public decimal SecondItemActualValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the second item label.
        /// </summary>
        /// <value>
        /// The name of the second item label.
        /// </value>
        public string SecondItemLabelName { get; set; }

        /// <summary>
        /// Gets or sets the first name of the item label.
        /// </summary>
        /// <value>
        /// The first name of the item label.
        /// </value>
        public string FirstItemLabelName { get; set; }

        /// <summary>
        /// Gets or sets the go value.
        /// </summary>
        /// <value>
        /// The go value.
        /// </value>
        public decimal FirstItemCharterValue { get; set; }

        /// <summary>
        /// Gets or sets the go actual value.
        /// </summary>
        /// <value>
        /// The go actual value.
        /// </value>
        public decimal FirstItemActualValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the charter.
        /// </summary>
        /// <value>
        /// The name of the charter.
        /// </value>
        public string CharterName { get; set; }

        #endregion
    }
}
