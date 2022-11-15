using System;
using System.Collections.Generic;
using PWAFeaturesRnd.ViewModels.Notification;

namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// Notes List View Model
    /// </summary>
    public class NotesListViewModel
    {
        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        public string NoteId { get; set; }

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
        /// Gets or sets the note date UTC.
        /// </summary>
        /// <value>
        /// The note date UTC.
        /// </value>
        public string NoteDateUTC { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the cat identifier.
        /// </summary>
        /// <value>
        /// The cat identifier.
        /// </value>
        public int? CatId { get; set; }

        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string CategoryName { get; set; }

        /// <summary>
        /// Gets or sets the execution date time.
        /// </summary>
        /// <value>
        /// The execution date time.
        /// </value>
        public DateTime? ExecutionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the expected execution date time.
        /// </summary>
        /// <value>
        /// The expected execution date time.
        /// </value>
        public string ExpectedExecutionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the short date.
        /// </summary>
        /// <value>
        /// The short date.
        /// </value>
        public string ShortDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is reminder expired.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is reminder expired; otherwise, <c>false</c>.
        /// </value>
        public bool IsReminderExpired { get; set; }

        /// <summary>
        /// Gets or sets the is attachment.
        /// </summary>
        /// <value>
        /// The is attachment.
        /// </value>
        public bool IsAttachment { get; set; }

        /// <summary>
        /// Gets or sets the attachment details.
        /// </summary>
        /// <value>
        /// The attachment details.
        /// </value>
        public List<AttachmentViewModel> AttachmentDetails { get; set; }
    }
}
