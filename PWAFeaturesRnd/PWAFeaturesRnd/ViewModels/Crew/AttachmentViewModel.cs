using System;

namespace PWAFeaturesRnd.ViewModels.Crew
{
    /// <summary>
    /// Attachment View Model
    /// </summary>
    public class AttachmentViewModel
    {
        /// <summary>
        /// Gets or sets the uploaded on.
        /// </summary>
        /// <value>
        /// The uploaded on.
        /// </value>
        public DateTime UploadedOn { get; set; }

        /// <summary>
        /// Gets or sets the uploaded by.
        /// </summary>
        /// <value>
        /// The uploaded by.
        /// </value>
        public string UploadedBy { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the size of the document.
        /// </summary>
        /// <value>
        /// The size of the document.
        /// </value>
        public int DocumentSize { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        /// <value>
        /// The document identifier.
        /// </value>
        public string DocumentId { get; set; }

    }
}
