namespace PWAFeaturesRnd.ViewModels.Notification
{
	/// <summary>
	/// Upload Attachment Request View Model
	/// </summary>
	public class UploadAttachmentRequestViewModel
	{
		/// <summary>
		/// Gets or sets the name of the file.
		/// </summary>
		/// <value>
		/// The name of the file.
		/// </value>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the file base64 string.
		/// </summary>
		/// <value>
		/// The file base64 string.
		/// </value>
		public string FileBase64String { get; set; }

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