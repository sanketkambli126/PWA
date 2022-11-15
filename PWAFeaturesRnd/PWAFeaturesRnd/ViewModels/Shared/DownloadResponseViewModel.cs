using System.IO;

namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public class DownloadResponseViewModel
    {
        /// <summary>
        /// Gets or sets the document stream.
        /// </summary>
        /// <value>
        /// The document stream.
        /// </value>
        public Stream DocumentStream { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>
        /// The type of the media.
        /// </value>
        public string MediaType { get; set; }
    }
}
