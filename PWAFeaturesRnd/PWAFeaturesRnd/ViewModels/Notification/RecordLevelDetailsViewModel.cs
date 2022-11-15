using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// RecordLevelDetailsViewModel
    /// </summary>
    public class RecordLevelDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public List<KeyValuePair<string, string>> Details { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }
    }
}
