namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// LastReadMessageResponseViewModel
    /// </summary>
    public class LastReadMessageResponseViewModel
    {
        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public int MessageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is seen.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is seen; otherwise, <c>false</c>.
        /// </value>
        public bool IsSeen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is delivered.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is delivered; otherwise, <c>false</c>.
        /// </value>
        public bool IsDelivered { get; set; }
    }
}
