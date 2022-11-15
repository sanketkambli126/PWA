using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Certificate
{
	public class CertificateIssueDetail
	{
		/// <summary>
		/// Gets or sets the certificate log identifier.
		/// </summary>
		/// <value>
		/// The certificate log identifier.
		/// </value>
		public string CertificateLogId { get; set; }

		/// <summary>
		/// Gets or sets the issue date.
		/// </summary>
		/// <value>
		/// The issue date.
		/// </value>
		public DateTime? IssueDate { get; set; }

		/// <summary>
		/// Gets or sets the expiry date.
		/// </summary>
		/// <value>
		/// The expiry date.
		/// </value>
		public DateTime? ExpiryDate { get; set; }

		/// <summary>
		/// Gets or sets the issued by.
		/// </summary>
		/// <value>
		/// The issued by.
		/// </value>
		public string IssuedBy { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the updated by.
		/// </summary>
		/// <value>
		/// The updated by.
		/// </value>
		public string UpdatedBy { get; set; }

		/// <summary>
		/// Gets or sets the updated on.
		/// </summary>
		/// <value>
		/// The updated on.
		/// </value>
		public DateTime? UpdatedOn { get; set; }

		/// <summary>
		/// Gets or sets the updated on UTC.
		/// </summary>
		/// <value>
		/// The updated on UTC.
		/// </value>
		public DateTime? UpdatedOnUTC { get; set; }

		/// <summary>
		/// Gets or sets the user role.
		/// </summary>
		/// <value>
		/// The user role.
		/// </value>
		public string UserRole { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is documents available.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is documents available; otherwise, <c>false</c>.
		/// </value>
		public bool IsDocumentsAvailable { get; set; }

		/// <summary>
		/// Gets or sets the document count.
		/// </summary>
		/// <value>
		/// The document count.
		/// </value>
		public int DocumentCount { get; set; }

	}
}
