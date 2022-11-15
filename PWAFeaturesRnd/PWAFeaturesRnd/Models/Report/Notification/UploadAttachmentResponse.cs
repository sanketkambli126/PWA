namespace PWAFeaturesRnd.Models.Report.Notification
{
	/// <summary>
	/// Upload Attachment Response
	/// </summary>
	public class UploadAttachmentResponse
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the sequence.
		/// </summary>
		/// <value>
		/// The sequence.
		/// </value>
		public int Sequence { get; set; }

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
		/// Gets or sets a value indicating whether this instance is operation success.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is operation success; otherwise, <c>false</c>.
		/// </value>
		public bool IsOperationSuccess { get; set; }

		/// <summary>
		/// Gets or sets the error message.
		/// </summary>
		/// <value>
		/// The error message.
		/// </value>
		public string ErrorMessage { get; set; }
	}
}
