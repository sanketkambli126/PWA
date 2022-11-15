using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// ChannelMessage
    /// </summary>
    public class ChannelMessage
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
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the message description.
        /// </summary>
        /// <value>
        /// The message description.
        /// </value>
        public string MessageDescription { get; set; }

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
        /// Gets or sets the created on UTC.
        /// </summary>
        /// <value>
        /// The created on UTC.
        /// </value>
        public DateTime CreatedOnUtc { get; set; }

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
