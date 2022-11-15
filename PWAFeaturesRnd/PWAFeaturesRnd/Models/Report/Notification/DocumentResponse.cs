namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// DocumentResponse
    /// </summary>
    public class DocumentResponse
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the document byte stream.
        /// </summary>
        /// <value>
        /// The document byte stream.
        /// </value>
        public byte[] DocumentByteStream { get; set; }
    }
}
