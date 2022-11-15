using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.Notification;

namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Create Note Request
    /// </summary>
    public class CreateNoteRequest
    {
        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public long NoteId { get; set; }

        /// <summary>
        /// Gets or sets the note title.
        /// </summary>
        /// <value>
        /// The note title.
        /// </value>
        public string NoteTitle { get; set; }

        /// <summary>
        /// Gets or sets the note description.
        /// </summary>
        /// <value>
        /// The note description.
        /// </value>
        public string NoteDescription { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the context parameters.
        /// </summary>
        /// <value>
        /// The context parameters.
        /// </value>
        public string ContextParams { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>
        /// The cat identifier.
        /// </value>
        public int? CatId { get; set; }

        /// <summary>
        /// Gets or sets the notes reminders.
        /// </summary>
        /// <value>
        /// The notes reminders.
        /// </value>
        public List<NotesReminder> NotesReminders { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is attachment.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is attachment; otherwise, <c>false</c>.
        /// </value>
        public bool IsAttachment { get; set; }

        /// <summary>
        /// Gets or sets the attachment list.
        /// </summary>
        /// <value>
        /// The attachment list.
        /// </value>
        public List<UploadAttachmentResponse> AttachmentList { get; set; }

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public string ReferenceIdentifier { get; set; }
    }
}
