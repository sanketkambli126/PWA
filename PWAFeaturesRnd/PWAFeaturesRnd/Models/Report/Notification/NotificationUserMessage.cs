using System;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationUserMessage
    {
        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public int MessageId { get; set; }
        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int? ChannelId { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }
        /// <summary>
        /// Gets or sets the message description.
        /// </summary>
        /// <value>
        /// The message description.
        /// </value>
        public string MessageDescription { get; set; }
        /// <summary>
        /// Gets or sets the created on UTC.
        /// </summary>
        /// <value>
        /// The created on UTC.
        /// </value>
        public DateTime? CreatedOnUtc { get; set; }
        /// <summary>
        /// Gets or sets the source application identifier.
        /// </summary>
        /// <value>
        /// The source application identifier.
        /// </value>
        public int? SourceAppId { get; set; }
        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }
        /// <summary>
        /// Gets or sets the parent message identifier.
        /// </summary>
        /// <value>
        /// The parent message identifier.
        /// </value>
        public int? ParentMessageId { get; set; }
        /// <summary>
        /// Gets or sets the ss user identifier.
        /// </summary>
        /// <value>
        /// The ss user identifier.
        /// </value>
        public string SSUserId { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the channel.
        /// </summary>
        /// <value>
        /// The channel.
        /// </value>
        public NotificationChannel Channel { get; set; }
        /// <summary>
        /// Gets or sets the source application.
        /// </summary>
        /// <value>
        /// The source application.
        /// </value>
        public ApplicationList SourceApp { get; set; }
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public NotificationUser User { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is sent; otherwise, <c>false</c>.
        /// </value>
        public bool IsSent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is seen.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is seen; otherwise, <c>false</c>.
        /// </value>
        public bool IsSeen { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is unread message.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is unread message; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnreadMessage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is attachment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is attachment; otherwise, <c>false</c>.
        /// </value>
        public int IsAttachment { get; set; }

        /// <summary>
        /// Gets or sets the attachment details.
        /// </summary>
        /// <value>
        /// The attachment details.
        /// </value>
        public string AttachmentDetails { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public int? Status { get; set; }

        /// <summary>
        /// Gets or sets the modified on UTC.
        /// </summary>
        /// <value>
        /// The modified on UTC.
        /// </value>
        public DateTime? ModifiedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the read participant count.
        /// </summary>
        /// <value>
        /// The read participant count.
        /// </value>
        public int ReadParticipantCount { get; set; }
    }
}
