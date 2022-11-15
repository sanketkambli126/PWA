using System;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageCategoryDestination
    {
        /// <summary>
        /// Gets or sets the mcdid.
        /// </summary>
        /// <value>
        /// The mcdid.
        /// </value>
        public int Mcdid { get; set; }
        /// <summary>
        /// Gets or sets the destination application identifier.
        /// </summary>
        /// <value>
        /// The destination application identifier.
        /// </value>
        public int? DestinationAppId { get; set; }
        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>
        /// The cat identifier.
        /// </value>
        public int? CatId { get; set; }
        /// <summary>
        /// Gets or sets the navigation parameters.
        /// </summary>
        /// <value>
        /// The navigation parameters.
        /// </value>
        public string NavigationParameters { get; set; }
        /// <summary>
        /// Gets or sets the message type identifier.
        /// </summary>
        /// <value>
        /// The message type identifier.
        /// </value>
        public int? MessageTypeId { get; set; }
        /// <summary>
        /// Gets or sets the message template.
        /// </summary>
        /// <value>
        /// The message template.
        /// </value>
        public string MessageTemplate { get; set; }
        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the cat.
        /// </summary>
        /// <value>
        /// The cat.
        /// </value>
        public MessageCategory Cat { get; set; }
        /// <summary>
        /// Gets or sets the destination application.
        /// </summary>
        /// <value>
        /// The destination application.
        /// </value>
        public ApplicationList DestinationApp { get; set; }
        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <value>
        /// The type of the message.
        /// </value>
        public MessageType MessageType { get; set; }
    }
}
