using System;

namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Report Download History Request
    /// </summary>
    public class ReportDownloadHistoryRequest
    {
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the downloaded by.
        /// </summary>
        /// <value>
        /// The downloaded by.
        /// </value>
        public string DownloadedBy { get; set; }

        /// <summary>
        /// Gets or sets the download mode.
        /// </summary>
        /// <value>
        /// The download mode.
        /// </value>

        public string DownloadMode { get; set; }


        /// <summary>
        /// Gets or sets the task message identifier.
        /// </summary>
        /// <value>
        /// The task message identifier.
        /// </value>

        public Guid TaskMessageId { get; set; }
    }
}
