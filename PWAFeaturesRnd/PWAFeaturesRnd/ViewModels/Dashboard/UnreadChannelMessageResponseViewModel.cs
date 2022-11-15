using System;

namespace PWAFeaturesRnd.ViewModels.Dashboard
{
    /// <summary>
    /// UnreadChannelMessageResponseViewModel
    /// </summary>
    public class UnreadChannelMessageResponseViewModel
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
        /// Gets or sets the created local date.
        /// </summary>
        /// <value>
        /// The created local date.
        /// </value>
        public string CreatedLocalDate { get; set; }

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
        /// Gets or sets the Last Sender.
        /// </summary>
        /// <value>
        /// The LastSender.
        /// </value>
        public string LastSender { get; set; }

        /// <summary>
        /// Gets or sets the Last Message Description.
        /// </summary>
        /// <value>
        /// The Last Message Description.
        /// </value>
        public string LastMessageDescription { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is draft.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is draft; otherwise, <c>false</c>.
		/// </value>
		public bool IsDraft { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [message has attachment].
		/// </summary>
		/// <value>
		///   <c>true</c> if [message has attachment]; otherwise, <c>false</c>.
		/// </value>
		public bool MessageHasAttachment { get; set; }

        /// <summary>
        /// Gets or sets the participants initials.
        /// </summary>
        /// <value>
        /// The participants initials.
        /// </value>
        public string ParticipantsInitials { get; set; }

        /// <summary>
        /// Gets or sets the participants names.
        /// </summary>
        /// <value>
        /// The participants names.
        /// </value>
        public string ParticipantsNames { get; set; }

    }
}
