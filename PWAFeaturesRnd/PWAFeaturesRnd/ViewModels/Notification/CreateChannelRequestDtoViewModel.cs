using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.Notification;

namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// Create Channel Request Dto ViewModel
    /// </summary>
    public class CreateChannelRequestDtoViewModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the context paylod.
        /// </summary>
        /// <value>
        /// The context paylod.
        /// </value>
        public string ContextPaylod { get; set; }

        /// <summary>
        /// Gets or sets the subscribers.
        /// </summary>
        /// <value>
        /// The subscribers.
        /// </value>
        public List<ParticipantsDetailsViewModel> Subscribers { get; set; }

        /// <summary>
        /// Gets or sets the initial MSG.
        /// </summary>
        /// <value>
        /// The initial MSG.
        /// </value>
        public string InitialMsg { get; set; }

        /// <summary>
        /// Gets or sets the ss user identifier.
        /// </summary>
        /// <value>
        /// The ss user identifier.
        /// </value>
        public string SsUserId { get; set; }

        /// <summary>
        /// Gets or sets the source application identifier.
        /// </summary>
        /// <value>
        /// The source application identifier.
        /// </value>
        public int SourceAppId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        /// The channel identifier.
        /// </value>
        public int ChannelId { get; set; }

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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is save as draft.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is save as draft; otherwise, <c>false</c>.
        /// </value>
        public bool IsSaveAsDraft { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public string ReferenceIdentifier { get; set; }
    }
}
