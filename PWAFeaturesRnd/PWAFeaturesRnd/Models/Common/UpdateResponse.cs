namespace PWAFeaturesRnd.Models.Common
{
    /// <summary>
    /// Contract to define the result set from a WCF Save or Delete Operation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UpdateResponse<T>
    {
        /// <summary>
        /// Gets or sets a value indicating whether [operation success].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [operation success]; otherwise, <c>false</c>.
        /// </value>
        public bool OperationSuccess { get; set; }

        /// <summary>
        /// Gets or sets the records affected.
        /// </summary>
        /// <value>
        /// The records affected.
        /// </value>
        public int? RecordsAffected { get; set; }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public T Result { get; set; }
    }
}
