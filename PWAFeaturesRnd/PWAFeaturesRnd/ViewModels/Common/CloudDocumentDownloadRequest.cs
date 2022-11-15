using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Common
{
	/// <summary>
	/// Cloud Document Download Request
	/// </summary>
	public class CloudDocumentDownloadRequest
    {
		/// <summary>
		/// Gets or sets the DocumentFileType.
		/// </summary>
		/// <value>
		/// The DocumentFileType.
		/// </value>
		public DocumentFileType DocumentFileType { get; set; }

		/// <summary>
		/// Gets or sets the DocumentCategory.
		/// </summary>
		/// <value>
		/// The DocumentCategory.
		/// </value>
		public DocumentCategory DocumentCategory { get; set; }

		/// <summary>
		/// Gets or sets the Identifier.
		/// </summary>
		/// <value>
		/// The Identifier.
		/// </value>
		public string Identifier { get; set; }

		/// <summary>
		/// Gets or sets the UserId.
		/// </summary>
		/// <value>
		/// The UserId.
		/// </value>
		public string FileName { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string UserId { get; set; }

		/// <summary>
		/// Gets or sets the UserLogId.
		/// </summary>
		/// <value>
		/// The UserLogId.
		/// </value>
		public string UserLogId { get; set; }

		/// <summary>
		/// Gets or sets the VersionId.
		/// </summary>
		/// <value>
		/// The VersionId.
		/// </value>
		public string VersionId { get; set; }
    }
}
