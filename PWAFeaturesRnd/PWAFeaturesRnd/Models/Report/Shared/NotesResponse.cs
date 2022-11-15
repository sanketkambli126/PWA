using System;

namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// Notes Response
    /// </summary>
    public class NotesResponse
    {
        /// <summary>
        /// Gets or sets the notes identifier.
        /// </summary>
        /// <value>
        /// The notes identifier.
        /// </value>
        public int NoteId { get; set; }

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
        /// Gets or sets the execution date time.
        /// </summary>
        /// <value>
        /// The execution date time.
        /// </value>
        public DateTime? ExecutionDateTime { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the total records.
        /// </summary>
        /// <value>
        /// The total records.
        /// </value>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the is reminder expired.
        /// </summary>
        /// <value>
        /// The is reminder expired.
        /// </value>
        public bool? IsReminderExpired { get; set; }

        /// <summary>
        /// Gets or sets the is attachment.
        /// </summary>
        /// <value>
        /// The is attachment.
        /// </value>
        public bool? IsAttachment { get; set; }

        /// <summary>
        /// Gets or sets the attachment details.
        /// </summary>
        /// <value>
        /// The attachment details.
        /// </value>
        public string AttachmentDetails { get; set; }
    }
}
