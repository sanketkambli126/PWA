using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Shared
{
	/// <summary>
	/// 
	/// </summary>
	public class CloudDocumentRequestViewModel
    {
		/// <summary>
		/// Gets or sets the document category.
		/// </summary>
		/// <value>
		/// The document category.
		/// </value>
		public DocumentCategory? DocumentCategory { get; set; }

		/// <summary>
		/// Gets or sets the document filename.
		/// </summary>
		/// <value>
		/// The document filename.
		/// </value>
		public string DocumentFilename { get; set; }

		/// <summary>
		/// Gets or sets the document size in bytes.
		/// </summary>
		/// <value>
		/// The document size in bytes.
		/// </value>
		public int DocumentSizeInBytes { get; set; }

		/// <summary>
		/// Gets or sets the matched identifier.
		/// </summary>
		/// <value>
		/// The matched identifier.
		/// </value>
		public string MatchedId { get; set; }

		/// <summary>
		/// Gets or sets the type of the file.
		/// </summary>
		/// <value>
		/// The type of the file.
		/// </value>
		public DocumentFileType? FileType { get; set; }

		/// <summary>
		/// Gets or sets the cloud document identifier.
		/// </summary>
		/// <value>
		/// The cloud document identifier.
		/// </value>
		public string CloudDocumentIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the document description.
		/// </summary>
		/// <value>
		/// The document description.
		/// </value>
		public string DocumentDescription { get; set; }
	}
}
