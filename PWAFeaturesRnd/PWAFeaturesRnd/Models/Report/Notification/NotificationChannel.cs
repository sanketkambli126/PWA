using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationChannel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
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
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>
        /// The cat identifier.
        /// </value>
        public int? CatId { get; set; }
        /// <summary>
        /// Gets or sets the context payload.
        /// </summary>
        /// <value>
        /// The context payload.
        /// </value>
        public string ContextPayload { get; set; }
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int? CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public DateTime? ModifiedOn { get; set; }
        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        /// <value>
        /// The modified by.
        /// </value>
        public int? ModifiedBy { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets the cat.
        /// </summary>
        /// <value>
        /// The cat.
        /// </value>
        public MessageCategory Cat { get; set; }
        /// <summary>
        /// Gets or sets the created by navigation.
        /// </summary>
        /// <value>
        /// The created by navigation.
        /// </value>
        public NotificationUser CreatedByNavigation { get; set; }
        /// <summary>
        /// Gets or sets the modified by navigation.
        /// </summary>
        /// <value>
        /// The modified by navigation.
        /// </value>
        public NotificationUser ModifiedByNavigation { get; set; }
        /// <summary>
        /// Gets or sets the notification channel subscriptions.
        /// </summary>
        /// <value>
        /// The notification channel subscriptions.
        /// </value>
        public List<NotificationChannelSubscription> NotificationChannelSubscriptions { get; set; }
        /// <summary>
        /// Gets or sets the notification user messages.
        /// </summary>
        /// <value>
        /// The notification user messages.
        /// </value>
        public List<NotificationUserMessage> NotificationUserMessages { get; set; }

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
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is one to one chat.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is one to one chat; otherwise, <c>false</c>.
        /// </value>
        public bool IsOneToOneChat { get; set; }

        /// <summary>
        /// Gets or sets the name of the ves.
        /// </summary>
        /// <value>
        /// The name of the ves.
        /// </value>
        public string VesName { get; set; }

        /// <summary>
        /// Gets or sets the ves imo number.
        /// </summary>
        /// <value>
        /// The ves imo number.
        /// </value>
        public string VesIMONumber { get; set; }

        /// <summary>
        /// Gets or sets the participant count.
        /// </summary>
        /// <value>
        /// The participant count.
        /// </value>
        public int ParticipantCount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is save as draft.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is save as draft; otherwise, <c>false</c>.
        /// </value>
        public bool IsSaveAsDraft { get; set; }

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
        /// Gets or sets the unread message count.
        /// </summary>
        /// <value>
        /// The unread message count.
        /// </value>
        public int UnreadMessageCount { get; set; }

        /// <summary>
        /// Gets or sets the participants initials.
        /// </summary>
        /// <value>
        /// The participants initials.
        /// </value>
        public string ParticipantsInitials { get; set; }
    }
}
