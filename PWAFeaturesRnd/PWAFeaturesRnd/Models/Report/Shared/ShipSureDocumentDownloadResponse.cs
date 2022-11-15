using System.IO;

namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// ShipSure Document Download Response
    /// </summary>
    public class ShipSureDocumentDownloadResponse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the mimetypestring.
        /// </summary>
        /// <value>
        /// The mimetypestring.
        /// </value>
        public string Mimetypestring { get; set; }

        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        /// <value>
        /// The exception message.
        /// </value>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the size of the file.
        /// </summary>
        /// <value>
        /// The size of the file.
        /// </value>
        public int? FileSize { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [operation success].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [operation success]; otherwise, <c>false</c>.
        /// </value>
        public bool OperationSuccess { get; set; }

        /// <summary>
        /// Gets or sets the byte stream.
        /// </summary>
        /// <value>
        /// The byte stream.
        /// </value>
        public Stream ByteStream { get; set; }
    }
}
