using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationUser
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets the ssuser identifier.
        /// </summary>
        /// <value>
        /// The ssuser identifier.
        /// </value>
        public string SsuserId { get; set; }
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// Gets or sets the modified on.
        /// </summary>
        /// <value>
        /// The modified on.
        /// </value>
        public DateTime? ModifiedOn { get; set; }
        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets the notification channel created by navigations.
        /// </summary>
        /// <value>
        /// The notification channel created by navigations.
        /// </value>
        public List<NotificationChannel> NotificationChannelCreatedByNavigations { get; set; }
        /// <summary>
        /// Gets or sets the notification channel modified by navigations.
        /// </summary>
        /// <value>
        /// The notification channel modified by navigations.
        /// </value>
        public List<NotificationChannel> NotificationChannelModifiedByNavigations { get; set; }
        /// <summary>
        /// Gets or sets the notification channel subscription created by navigations.
        /// </summary>
        /// <value>
        /// The notification channel subscription created by navigations.
        /// </value>
        public List<NotificationChannelSubscription> NotificationChannelSubscriptionCreatedByNavigations { get; set; }
        /// <summary>
        /// Gets or sets the notification channel subscription modified by navigations.
        /// </summary>
        /// <value>
        /// The notification channel subscription modified by navigations.
        /// </value>
        public List<NotificationChannelSubscription> NotificationChannelSubscriptionModifiedByNavigations { get; set; }
        /// <summary>
        /// Gets or sets the notification channel subscription users.
        /// </summary>
        /// <value>
        /// The notification channel subscription users.
        /// </value>
        public List<NotificationChannelSubscription> NotificationChannelSubscriptionUsers { get; set; }
        /// <summary>
        /// Gets or sets the notification user messages.
        /// </summary>
        /// <value>
        /// The notification user messages.
        /// </value>
        public List<NotificationUserMessage> NotificationUserMessages { get; set; }
    }
}
