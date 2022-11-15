using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Notification
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageType
    {
        /// <summary>
        /// Gets or sets the message type identifier.
        /// </summary>
        /// <value>
        /// The message type identifier.
        /// </value>
        public int MessageTypeId { get; set; }
        /// <summary>
        /// Gets or sets the name of the message type.
        /// </summary>
        /// <value>
        /// The name of the message type.
        /// </value>
        public string MessageTypeName { get; set; }
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
        public virtual List<MessageCategoryDestination> MessageCategoryDestinations { get; set; }
    }
}
