namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationChannelViewModel
    {
        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int ChannelId { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>
        /// The payload.
        /// </value>
        public string Payload { get; set; }
        /// <summary>
        /// Gets or sets the context payload.
        /// </summary>
        /// <value>
        /// The context payload.
        /// </value>
        public string ContextPayload { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int? CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the recieved date.
        /// </summary>
        /// <value>
        /// The recieved date.
        /// </value>
        public string RecievedDate { get; set; }

        /// <summary>
        /// Gets or sets the additional data.
        /// </summary>
        /// <value>
        /// The additional data.
        /// </value>
        public string AdditionalData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has unread messages.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has unread messages; otherwise, <c>false</c>.
        /// </value>
        public bool HasUnreadMessages { get; set; }

        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the vessel imo number.
        /// </summary>
        /// <value>
        /// The vessel imo number.
        /// </value>
        public string VesselIMONumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is one to one chat.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is one to one chat; otherwise, <c>false</c>.
        /// </value>
        public bool IsOneToOneChat { get; set; }

        /// <summary>
        /// Gets or sets the notification mobile chat detail URL.
        /// </summary>
        /// <value>
        /// The notification mobile chat detail URL.
        /// </value>
        public string NotificationMobileChatDetailURL { get; set; }

        /// <summary>
        /// Gets or sets the mobile unread channel status.
        /// </summary>
        /// <value>
        /// The mobile unread channel status.
        /// </value>
        public string MobileUnreadChannelStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is general.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is general; otherwise, <c>false</c>.
        /// </value>
        public bool IsGeneralCat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is save as draft.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is save as draft; otherwise, <c>false</c>.
        /// </value>
        public bool IsSaveAsDraft { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the notification mobile discussion URL.
        /// </summary>
        /// <value>
        /// The notification mobile discussion URL.
        /// </value>
        public string NotificationMobileDiscussionUrl { get; set; }

        /// <summary>
        /// Gets or sets the last name of the sender.
        /// </summary>
        /// <value>
        /// The last name of the sender.
        /// </value>
        public string LastSenderName { get; set; }

        /// <summary>
        /// Gets or sets the last sent message in channel.
        /// </summary>
        /// <value>
        /// The last sent message in channel.
        /// </value>
        public string LastSentMessageInChannel { get; set; }

        /// <summary>
        /// Gets or sets the last message details.
        /// </summary>
        /// <value>
        /// The last message details.
        /// </value>
        public string LastMessageDetails { get; set; }

        /// <summary>
        /// Gets or sets the active participants count.
        /// </summary>
        /// <value>
        /// The active participants count.
        /// </value>
        public int ActiveParticipantsCount { get; set; }

        /// <summary>
        /// Gets or sets the Attachment exists.
        /// </summary>
        /// <value>
        /// True if Attachment existss else false.
        /// </value>
        public bool IsAttachment { get; set; }

        /// <summary>
        /// Gets or sets the total record count.
        /// </summary>
        /// <value>
        /// The total record count.
        /// </value>
        public int TotalRecordCount { get; set; }

        /// <summary>
        /// Gets or sets the participants initials.
        /// </summary>
        /// <value>
        /// The participants initials.
        /// </value>
        public string ParticipantsInitials { get; set; }

        /// <summary>
        /// Gets or sets the unread message count.
        /// </summary>
        /// <value>
        /// The unread message count.
        /// </value>
        public int UnreadMessageCount { get; set; }
    }
}
