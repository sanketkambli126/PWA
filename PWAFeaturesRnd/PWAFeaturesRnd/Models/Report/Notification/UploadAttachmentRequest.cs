namespace PWAFeaturesRnd.Models.Report.Notification
{
	/// <summary>
	/// Upload Attachment Request
	/// </summary>
	public class UploadAttachmentRequest
	{
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>
		/// The name of the file.
		/// </value>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the document stream.
		/// </summary>
		/// <value>
		/// The document stream.
		/// </value>
		public byte[] DocumentStream { get; set; }

		/// <summary>
		/// Gets or sets the sequence.
		/// </summary>
		/// <value>
		/// The sequence.
		/// </value>
		public int Sequence { get; set; }

		/// <summary>
		/// Gets or sets the name of the login user.
		/// </summary>
		/// <value>
		/// The name of the login user.
		/// </value>
		public string LoginUserName { get; set; }
	}
}
