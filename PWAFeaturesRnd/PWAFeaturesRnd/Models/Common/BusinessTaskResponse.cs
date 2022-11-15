using System;

namespace PWAFeaturesRnd.Models.Common
{
    /// <summary>
    /// Business Task Response
    /// </summary>
    public class BusinessTaskResponse
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the notification identifier.
        /// </summary>
        /// <value>
        /// The notification identifier.
        /// </value>
        public string NotificationId { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        /// <value>
        /// The content of the message.
        /// </value>
        public string MessageContent { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        /// <value>
        /// The display name of the user.
        /// </value>
        public string UserDisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BusinessTaskResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        /// <value>
        /// The exception message.
        /// </value>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the message date.
        /// </summary>
        /// <value>
        /// The message date.
        /// </value>
        public DateTime MessageDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the generated file.
        /// </summary>
        /// <value>
        /// The name of the generated file.
        /// </value>
        public string GeneratedFileName { get; set; }

        /// <summary>
        /// Gets or sets the name of the friendly file.
        /// </summary>
        /// <value>
        /// The name of the friendly file.
        /// </value>
        public string FriendlyFileName { get; set; }

    }
}
