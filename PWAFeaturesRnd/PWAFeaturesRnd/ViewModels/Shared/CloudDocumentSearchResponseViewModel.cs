using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Shared
{
	public class CloudDocumentSearchResponseViewModel
    {
		/// <summary>
		/// Gets or sets the cloud document identifier.
		/// </summary>
		/// <value>
		/// The cloud document identifier.
		/// </value>
		public string CloudDocumentIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the thumbnail identifier.
		/// </summary>
		/// <value>
		/// The thumbnail identifier.
		/// </value>
		public string ThumbnailIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the matched identifier.
		/// </summary>
		/// <value>
		/// The matched identifier.
		/// </value>
		public string MatchedId { get; set; }

		/// <summary>
		/// Gets or sets the document category.
		/// </summary>
		/// <value>
		/// The document category.
		/// </value>
		public DocumentCategory DocumentCategory { get; set; }

		/// <summary>
		/// Gets or sets the cloud upload status.
		/// </summary>
		/// <value>
		/// The cloud upload status.
		/// </value>
		public CloudUploadStatus CloudUploadStatus { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the coy identifier.
		/// </summary>
		/// <value>
		/// The coy identifier.
		/// </value>
		public string CoyId { get; set; }

		/// <summary>
		/// Gets or sets the company identifier.
		/// </summary>
		/// <value>
		/// The company identifier.
		/// </value>
		public string CompanyId { get; set; }

		/// <summary>
		/// Gets or sets the crew identifier.
		/// </summary>
		/// <value>
		/// The crew identifier.
		/// </value>
		public string CrewId { get; set; }

		/// <summary>
		/// Gets or sets the document cateogory identifier.
		/// </summary>
		/// <value>
		/// The document cateogory identifier.
		/// </value>
		public string DocumentCateogoryId { get; set; }

		/// <summary>
		/// Gets or sets the document cateogory description.
		/// </summary>
		/// <value>
		/// The document cateogory description.
		/// </value>
		public string DocumentCateogoryDescription { get; set; }

		/// <summary>
		/// Gets or sets the document size in bytes.
		/// </summary>
		/// <value>
		/// The document size in bytes.
		/// </value>
		public int DocumentSizeInBytes { get; set; }

		/// <summary>
		/// Gets or sets the document filename.
		/// </summary>
		/// <value>
		/// The document filename.
		/// </value>
		public string DocumentFilename { get; set; }

		/// <summary>
		/// Gets or sets the document description.
		/// </summary>
		/// <value>
		/// The document description.
		/// </value>
		public string DocumentDescription { get; set; }

		/// <summary>
		/// Gets or sets the upload date.
		/// </summary>
		/// <value>
		/// The upload date.
		/// </value>
		public DateTime UploadDate { get; set; }

		/// <summary>
		/// Gets or sets the type of the file.
		/// </summary>
		/// <value>
		/// The type of the file.
		/// </value>
		public DocumentFileType FileType { get; set; }

		/// <summary>
		/// Gets or sets the upload by.
		/// </summary>
		/// <value>
		/// The upload by.
		/// </value>
		public string UploadBy { get; set; }
		
		/// <summary>
		/// Gets or sets the entity reference2.
		/// </summary>
		/// <value>
		/// The entity reference2.
		/// </value>
		public string EntityReference2 { get; set; }
	}
}
