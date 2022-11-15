using System;
using System.Collections.Generic;
using PWAFeaturesRnd.ViewModels.Notification;

namespace PWAFeaturesRnd.ViewModels.Shared
{
    /// <summary>
    /// Note Details Response ViewModel
    /// </summary>
    public class NoteDetailsResponseViewModel
    {
        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
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
        /// Gets or sets the note date UTC.
        /// </summary>
        /// <value>
        /// The note date UTC.
        /// </value>
        public DateTime NoteDateUTC { get; set; }

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
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int? Status { get; set; }

        /// <summary>
        /// Gets or sets the notes reminders.
        /// </summary>
        /// <value>
        /// The notes reminders.
        /// </value>
        public List<NotesReminderViewModel> NotesReminders { get; set; }

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

        /// <summary>
        /// Gets or sets the reference identifier.
        /// </summary>
        /// <value>
        /// The reference identifier.
        /// </value>
        public string ReferenceIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the context parameters.
        /// </summary>
        /// <value>
        /// The context parameters.
        /// </value>
        public string ContextParams { get; set; }
    }
}
