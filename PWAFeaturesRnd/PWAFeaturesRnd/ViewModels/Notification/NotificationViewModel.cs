using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// Notification View Model
    /// </summary>
    public class NotificationViewModel
    {
        /// <summary>
        /// Gets or sets the channel list.
        /// </summary>
        /// <value>
        /// The channel list.
        /// </value>
        public List<NotificationChannelViewModel> ChannelList { get; set; }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>
        /// The search text.
        /// </value>
        public string SearchText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is search clicked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is search clicked; otherwise, <c>false</c>.
        /// </value>
        public bool IsSearchClicked { get; set; }

        /// <summary>
		/// Gets or sets a value indicating whether [open create new channel].
		/// </summary>
		/// <value>
		///   <c>true</c> if [open create new channel]; otherwise, <c>false</c>.
		/// </value>
		public bool OpenCreateNewChannel { get; set; }

        /// <summary>
        /// Creates new messagedetails.
        /// </summary>
        /// <value>
        /// The new message details.
        /// </value>
        public NewMessageParametersViewModel NewMessageDetails { get; set; }

        /// <summary>
        /// Session Storage Details
        /// </summary>
        public string SessionStorageDetails { get; set; }

        /// <summary>
		/// Gets or sets a value indicating whether [clear session storage].
		/// </summary>
		/// <value>
		///   <c>true</c> if [clear session storage]; otherwise, <c>false</c>.
		/// </value>
        public bool ClearSessionStorage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is filter change.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is filter change; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilterChange { get; set; }
    }
}
