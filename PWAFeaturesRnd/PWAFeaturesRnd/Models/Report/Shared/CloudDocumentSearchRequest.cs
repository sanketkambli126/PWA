using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.Models.Report.Shared
{
	/// <summary>
	/// 
	/// </summary>
	public class CloudDocumentSearchRequest
	{
		/// <summary>
		/// Gets or sets the field information.
		/// </summary>
		/// <value>
		/// The field information.
		/// </value>
		public FieldInfo FieldInfo { get; set; }

		/// <summary>
		/// Gets or sets the server field information.
		/// </summary>
		/// <value>
		/// The server field information.
		/// </value>
		public string ServerFieldInfo { get; set; }

		/// <summary>
		/// Gets or sets the field value.
		/// </summary>
		/// <value>
		/// The field value.
		/// </value>
		public string FieldValue { get; set; }

		/// <summary>
		/// Gets or sets the document categories.
		/// </summary>
		/// <value>
		/// The document categories.
		/// </value>
		public List<DocumentCategory> DocumentCategories { get; set; }

		/// <summary>
		/// Gets or sets the cloud document identifiers.
		/// </summary>
		/// <value>
		/// The cloud document identifiers.
		/// </value>
		public List<string> CloudDocumentIdentifiers { get; set; }

		/// <summary>
		/// Gets or sets the matched identifiers.
		/// </summary>
		/// <value>
		/// The matched identifiers.
		/// </value>
		public List<string> MatchedIdentifiers { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public CloudUploadStatus? Status { get; set; }

		/// <summary>
		/// Gets or sets the cloud document filter field.
		/// </summary>
		/// <value>
		/// The cloud document filter field.
		/// </value>
		public string CloudDocumentFilterField { get; set; }
	}
}
