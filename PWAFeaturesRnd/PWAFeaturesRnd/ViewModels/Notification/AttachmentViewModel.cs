namespace PWAFeaturesRnd.ViewModels.Notification
{
    /// <summary>
    /// Attachment ViewModel
    /// </summary>
    public class AttachmentViewModel
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ett identifier.
        /// </summary>
        /// <value>
        /// The ett identifier.
        /// </value>
        public long EttId { get; set; }

        /// <summary>
        /// Gets or sets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        public string FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>
        /// The sequence.
        /// </value>
        public int Sequence { get; set; }
    }
}
