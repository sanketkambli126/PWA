using System;

namespace PWAFeaturesRnd.Models.Report.Dashboard
{
    /// <summary>
    /// UnreadChannelMessageResponse
    /// </summary>
    public class UnreadChannelMessageResponse
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the name of the channel.
        /// </summary>
        /// <value>
        /// The name of the channel.
        /// </value>
        public string ChannelName { get; set; }

        /// <summary>
        /// Gets or sets the created date UTC.
        /// </summary>
        /// <value>
        /// The created date UTC.
        /// </value>
        public DateTime? CreatedDateUTC { get; set; }

        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int ChannelId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unread.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is unread; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnread { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets the unread message count.
        /// </summary>
        /// <value>
        /// The unread message count.
        /// </value>
        public int UnreadMessageCount { get; set; }

		/// <summary>
		/// Gets or sets the message description.
		/// </summary>
		/// <value>
		/// The message description.
		/// </value>
		public string MessageDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [message has attachment].
		/// </summary>
		/// <value>
		///   <c>true</c> if [message has attachment]; otherwise, <c>false</c>.
		/// </value>
		public bool MessageHasAttachment { get; set; }

		/// <summary>
		/// Gets or sets the message create date UTC.
		/// </summary>
		/// <value>
		/// The message create date UTC.
		/// </value>
		public DateTime MessageCreateDateUtc { get; set; }

		/// <summary>
		/// Gets or sets the is draft.
		/// </summary>
		/// <value>
		/// The is draft.
		/// </value>
		public bool? IsDraft { get; set; }
    }
}
