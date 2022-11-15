using System;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationChannelSubscription
    {
        /// <summary>
        /// Gets or sets the subscription identifier.
        /// </summary>
        /// <value>
        /// The subscription identifier.
        /// </value>
        public int SubscriptionId { get; set; }
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
        /// Gets or sets the last read time UTC.
        /// </summary>
        /// <value>
        /// The last read time UTC.
        /// </value>
        public DateTime? LastReadTimeUtc { get; set; }
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
        /// Gets or sets the ss user identifier.
        /// </summary>
        /// <value>
        /// The ss user identifier.
        /// </value>
        public string SSUserId { get; set; }
        /// <summary>
        /// Gets or sets the user email id.
        /// </summary>
        /// <value>
        /// The user email id.
        /// </value>
        public string UsrEmail { get; set; }
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
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public NotificationUser User { get; set; }
    }
}
