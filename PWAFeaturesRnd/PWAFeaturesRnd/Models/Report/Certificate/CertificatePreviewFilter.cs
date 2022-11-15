using System;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Models.Common;

namespace PWAFeaturesRnd.Models.Report.Certificate
{
	/// <summary>
	/// Certificate Preview Filter
	/// </summary>
	public class CertificatePreviewFilter
	{
		/// <summary>
		/// Gets or sets the menu item.
		/// </summary>
		/// <value>
		/// The menu item.
		/// </value>
		public UserMenuItem MenuItem { get; set; }

		/// <summary>
		/// Gets or sets the type of the range.
		/// </summary>
		/// <value>
		/// The type of the range.
		/// </value>
		public CertificateRangeType? RangeType { get; set; }

		/// <summary>
		/// Gets or sets the certificate identifier.
		/// </summary>
		/// <value>
		/// The certificate identifier.
		/// </value>
		public string CertificateId { get; set; }

		/// <summary>
		/// Gets or sets the certificate impact identifier.
		/// </summary>
		/// <value>
		/// The certificate impact identifier.
		/// </value>
		public CertificateImpact CertificateImpactId { get; set; }

		/// <summary>
		/// Gets or sets the certificate impact ids.
		/// </summary>
		/// <value>
		/// The certificate impact ids.
		/// </value>
		public string CertificateImpactIds { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [include window].
		/// </summary>
		/// <value>
		///   <c>true</c> if [include window]; otherwise, <c>false</c>.
		/// </value>
		public bool IncludeWindow { get; set; }

		/// <summary>
		/// Gets or sets the type of the certificate.
		/// </summary>
		/// <value>
		/// The type of the certificate.
		/// </value>
		public CertificateType? CertificateType { get; set; }

		/// <summary>
		/// Gets or sets the is certificate active.
		/// </summary>
		/// <value>
		/// The is certificate active.
		/// </value>
		public bool? IsCertificateActive { get; set; }

		/// <summary>
		/// Gets or sets the is certificates changed.
		/// </summary>
		/// <value>
		/// The is certificates changed.
		/// </value>
		public bool? IsCertificatesChanged { get; set; }

		/// <summary>
		/// Gets or sets the is deleted.
		/// </summary>
		/// <value>
		/// The is deleted.
		/// </value>
		public bool? IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the search keyword.
		/// </summary>
		/// <value>
		/// The search keyword.
		/// </value>
		public string SearchKeyword { get; set; }

	}
}
