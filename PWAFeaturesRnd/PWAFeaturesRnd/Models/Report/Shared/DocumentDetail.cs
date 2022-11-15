using System;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	/// <summary>
	/// DocumentDetail
	/// </summary>
	public class DocumentDetail
	{
		/// <summary>
		/// Gets or sets the web address.
		/// </summary>
		/// <value>
		/// The web address.
		/// </value>
		public string WebAddress { get; set; }

		/// <summary>
		/// Gets or sets the type of the document.
		/// </summary>
		/// <value>
		/// The type of the document.
		/// </value>
		public int? DocumentType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is new document.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is new document; otherwise, <c>false</c>.
		/// </value>
		public bool IsNewDocument { get; set; }

		/// <summary>
		/// Gets or sets the cloud document status.
		/// </summary>
		/// <value>
		/// The cloud document status.
		/// </value>
		public string CloudDocumentStatus { get; set; }

		/// <summary>
		/// Gets or sets the name of the cloud file.
		/// </summary>
		/// <value>
		/// The name of the cloud file.
		/// </value>
		public string CloudFileName { get; set; }

		/// <summary>
		/// Gets or sets the name of the category.
		/// </summary>
		/// <value>
		/// The name of the category.
		/// </value>
		public string CategoryName { get; set; }

		/// <summary>
		/// Gets or sets the THB identifier.
		/// </summary>
		/// <value>
		/// The THB identifier.
		/// </value>
		public string ThbId { get; set; }

		/// <summary>
		/// Gets or sets the created on.
		/// </summary>
		/// <value>
		/// The created on.
		/// </value>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// Gets or sets the file path.
		/// </summary>
		/// <value>
		/// The file path.
		/// </value>
		public string FilePath { get; set; }

		/// <summary>
		/// Gets or sets the unique identifier.
		/// </summary>
		/// <value>
		/// The unique identifier.
		/// </value>
		public Guid UniqueIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the ett identifier.
		/// </summary>
		/// <value>
		/// The ett identifier.
		/// </value>
		public string EttId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [required to synchronize].
		/// </summary>
		/// <value>
		///   <c>true</c> if [required to synchronize]; otherwise, <c>false</c>.
		/// </value>
		public bool RequiredToSync { get; set; }

		/// <summary>
		/// Gets or sets the originator.
		/// </summary>
		/// <value>
		/// The originator.
		/// </value>
		public string Originator { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the source identifier.
		/// </summary>
		/// <value>
		/// The source identifier.
		/// </value>
		public string SourceId { get; set; }

		/// <summary>
		/// Gets or sets the SSM identifier.
		/// </summary>
		/// <value>
		/// The SSM identifier.
		/// </value>
		public string SsmId { get; set; }

		/// <summary>
		/// Gets or sets the DCT identifier.
		/// </summary>
		/// <value>
		/// The DCT identifier.
		/// </value>
		public string DctId { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the dat identifier.
		/// </summary>
		/// <value>
		/// The dat identifier.
		/// </value>
		public string DatId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can request document.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can request document; otherwise, <c>false</c>.
		/// </value>
		public bool CanRequestDocument { get; set; }

		/// <summary>
		/// Gets or sets the replicate to vessel.
		/// </summary>
		/// <value>
		/// The replicate to vessel.
		/// </value>
		public bool? ReplicateToVessel { get; set; }
	}
}
