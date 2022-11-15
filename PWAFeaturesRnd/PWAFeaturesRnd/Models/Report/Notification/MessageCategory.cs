using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageCategory
    {
        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>
        /// The cat identifier.
        /// </value>
        public int CatId { get; set; }
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string CategoryName { get; set; }
        /// <summary>
        /// Gets or sets the is attachment allowed.
        /// </summary>
        /// <value>
        /// The is attachment allowed.
        /// </value>
        public bool? IsAttachmentAllowed { get; set; }
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the message category destinations.
        /// </summary>
        /// <value>
        /// The message category destinations.
        /// </value>
        public List<MessageCategoryDestination> MessageCategoryDestinations { get; set; }
        /// <summary>
        /// Gets or sets the notification channels.
        /// </summary>
        /// <value>
        /// The notification channels.
        /// </value>
        public List<NotificationChannel> NotificationChannels { get; set; }
    }
}
