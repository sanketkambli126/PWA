using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Shared
{
	/// <summary>
	/// DocumentDetailViewModel
	/// </summary>
	public class DocumentDetailViewModel
	{
		/// <summary>
		/// Gets or sets the created on.
		/// </summary>
		/// <value>
		/// The created on.
		/// </value>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		public string Type { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance can request document.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance can request document; otherwise, <c>false</c>.
		/// </value>
		public bool CanRequestDocument { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is web address editable.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is web address editable; otherwise, <c>false</c>.
		/// </value>
		public bool IsWebAddressEditable { get; set; }

		/// <summary>
		/// Gets or sets the web address.
		/// </summary>
		/// <value>
		/// The web address.
		/// </value>
		public string WebAddress { get; set; }

		/// <summary>
		/// Gets or sets the ett identifier.
		/// </summary>
		/// <value>
		/// The ett identifier.
		/// </value>
		public string EttId { get; set; }

		/// <summary>
		/// Gets or sets the name of the cloud file.
		/// </summary>
		/// <value>
		/// The name of the cloud file.
		/// </value>
		public string CloudFileName { get; set; }

		/// <summary>
		/// Gets or sets the type of the document category.
		/// </summary>
		/// <value>
		/// The type of the document category.
		/// </value>
		public DocumentCategory DocumentCategory { get; set; }

	}
}
