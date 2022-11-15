using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationList
    {
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public int AppId { get; set; }
        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public string ApplicationName { get; set; }
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
        /// Gets or sets the notification user messages.
        /// </summary>
        /// <value>
        /// The notification user messages.
        /// </value>
        public List<NotificationUserMessage> NotificationUserMessages { get; set; }
    }
}
