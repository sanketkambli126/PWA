namespace PWAFeaturesRnd.Models.Common
{
    /// <summary>
    /// Custom contract to hold the attribute details
    /// </summary>
    public class VesselSpecificAttributeDetail
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the attribute value.
        /// </summary>
        /// <value>
        /// The attribute value.
        /// </value>
        public string AttributeValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [attribute bool value].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [attribute bool value]; otherwise, <c>false</c>.
        /// </value>
        public bool AttributeBoolValue { get; set; }

        /// <summary>
        /// Gets or sets the attribute decimal value.
        /// </summary>
        /// <value>
        /// The attribute decimal value.
        /// </value>
        public decimal AttributeDecimalValue { get; set; }

        /// <summary>
        /// Gets or sets the attribute int value.
        /// </summary>
        /// <value>
        /// The attribute int value.
        /// </value>
        public int AttributeIntValue { get; set; }
    }
}
