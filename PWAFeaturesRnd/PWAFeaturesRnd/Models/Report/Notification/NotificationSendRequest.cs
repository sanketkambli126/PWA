using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// Notification Send Request
    /// </summary>
    public class NotificationSendRequest
    {
        /// <summary>
        /// Gets or sets the message description.
        /// </summary>
        /// <value>
        /// The message description.
        /// </value>
        public string MessageDescription { get; set; }

        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the source application identifier.
        /// </summary>
        /// <value>
        /// The source application identifier.
        /// </value>
        public int SourceAppId { get; set; }

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
        public int ParentMessageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is attachment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is attachment; otherwise, <c>false</c>.
        /// </value>
        public int IsAttachment { get; set; }

        /// <summary>
        /// Gets or sets the attachment list.
        /// </summary>
        /// <value>
        /// The attachment list.
        /// </value>
        public List<UploadAttachmentResponse> AttachmentList { get; set; }
    }
}
