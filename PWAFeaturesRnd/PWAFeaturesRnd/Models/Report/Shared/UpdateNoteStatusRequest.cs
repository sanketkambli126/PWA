namespace PWAFeaturesRnd.Models.Report.Shared
{
    /// <summary>
    /// UpdateNoteStatusRequest
    /// </summary>
    public class UpdateNoteStatusRequest
    {
        /// <summary>
        /// Gets or sets the note identifier.
        /// </summary>
        /// <value>
        /// The note identifier.
        /// </value>
        public long NoteId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int Status { get; set; }

    }
}
