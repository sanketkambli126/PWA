using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.ViewModels.Notification
{
    public class EditMessageViewModel
    {
		/// <summary>
		/// Gets or sets the channel identifier.
		/// </summary>
		/// <value>
		/// The channel identifier.
		/// </value>
		public int? ChannelId { get; set; }
		
		/// <summary>
		/// Gets or sets a value indicating whether this instance is save as draft.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is save as draft; otherwise, <c>false</c>.
		/// </value>
		public bool IsSaveAsDraft { get; set; }

		/// <summary>
		/// Gets or sets the message description.
		/// </summary>
		/// <value>
		/// The message description.
		/// </value>
		public string MessageDescription { get; set; }

        /// <summary>
        /// Gets or sets the message identifier.
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public int MessageId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is attachment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is attachment; otherwise, <c>false</c>.
        /// </value>
        public bool IsAttachment { get; set; }

		/// <summary>
		/// Gets or sets the attachments.
		/// </summary>
		/// <value>
		/// The attachments.
		/// </value>
		public List<AttachmentViewModel> Attachments { get; set; }
	}
}
