using System;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Certificate
{
	public class CertificatePreview
    {
		/// <summary>
		/// Gets or sets the certificate identifier.
		/// </summary>
		/// <value>
		/// The certificate identifier.
		/// </value>
		public string CertificateId { get; set; }

		/// <summary>
		/// Gets or sets the vessel certificate identifier.
		/// </summary>
		/// <value>
		/// The vessel certificate identifier.
		/// </value>
		public string VesselCertificateId { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the annotation.
		/// </summary>
		/// <value>
		/// The annotation.
		/// </value>
		public string Annotation { get; set; }

		/// <summary>
		/// Gets or sets the vessel.
		/// </summary>
		/// <value>
		/// The vessel.
		/// </value>
		public string Vessel { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the issued by.
		/// </summary>
		/// <value>
		/// The issued by.
		/// </value>
		public string IssuedBy { get; set; }

		/// <summary>
		/// Gets or sets the validity.
		/// </summary>
		/// <value>
		/// The validity.
		/// </value>
		public int? Validity { get; set; }

		/// <summary>
		/// Gets or sets the date from.
		/// </summary>
		/// <value>
		/// The date from.
		/// </value>
		public DateTime? DateFrom { get; set; }

		/// <summary>
		/// Gets or sets the date to.
		/// </summary>
		/// <value>
		/// The date to.
		/// </value>
		public DateTime? DateTo { get; set; }

		/// <summary>
		/// Gets or sets the type of the range.
		/// </summary>
		/// <value>
		/// The type of the range.
		/// </value>
		public CertificateRangeType? RangeType { get; set; }

		/// <summary>
		/// Gets the range type description.
		/// </summary>
		/// <value>
		/// The range type description.
		/// </value>
		public string RangeTypeDescription
		{
			//get
			//{
			//    if (RangeType != null)
			//    {
			//        return EnumsHelper.GetDescription<CertificateRangeType>(RangeType.Value);
			//    }
			//    else
			//    {
			//        return "";
			//    }
			//}
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the certificate number.
		/// </summary>
		/// <value>
		/// The certificate number.
		/// </value>
		public string CertificateNumber { get; set; }

		/// <summary>
		/// Gets or sets the updated date.
		/// </summary>
		/// <value>
		/// The updated date.
		/// </value>
		public DateTime? UpdatedDate { get; set; }

		/// <summary>
		/// Gets or sets the updated date UTC.
		/// </summary>
		/// <value>
		/// The updated date UTC.
		/// </value>
		public DateTime? UpdatedDateUTC { get; set; }

		/// <summary>
		/// Gets or sets the parent vessel certificate identifier.
		/// </summary>
		/// <value>
		/// The parent vessel certificate identifier.
		/// </value>
		public string ParentVesselCertificateId { get; set; }

		/// <summary>
		/// Gets or sets the certificate section1.
		/// </summary>
		/// <value>
		/// The certificate section1.
		/// </value>
		public int? CertificateSection1 { get; set; }

		/// <summary>
		/// Gets or sets the certificate section2.
		/// </summary>
		/// <value>
		/// The certificate section2.
		/// </value>
		public int? CertificateSection2 { get; set; }

		/// <summary>
		/// Gets or sets the certificate section3.
		/// </summary>
		/// <value>
		/// The certificate section3.
		/// </value>
		public int? CertificateSection3 { get; set; }

		/// <summary>
		/// Gets or sets the issued in.
		/// </summary>
		/// <value>
		/// The issued in.
		/// </value>
		public string IssuedIn { get; set; }

		/// <summary>
		/// Gets or sets the window before expiry.
		/// </summary>
		/// <value>
		/// The window before expiry.
		/// </value>
		public int? WindowBeforeExpiry { get; set; }

		/// <summary>
		/// Gets or sets the window after expiry.
		/// </summary>
		/// <value>
		/// The window after expiry.
		/// </value>
		public int? WindowAfterExpiry { get; set; }

		/// <summary>
		/// Gets or sets the type of the certificate.
		/// </summary>
		/// <value>
		/// The type of the certificate.
		/// </value>
		public string CertificateType { get; set; }

		/// <summary>
		/// Gets or sets the certificate impact.
		/// </summary>
		/// <value>
		/// The certificate impact.
		/// </value>
		public string CertificateImpact { get; set; }

		/// <summary>
		/// Gets or sets the vessel certificate log identifier.
		/// </summary>
		/// <value>
		/// The vessel certificate log identifier.
		/// </value>
		public string VesselCertificateLogId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is document available.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is document available; otherwise, <c>false</c>.
		/// </value>
		public bool? IsDocumentAvailable { get; set; }

		/// <summary>
		/// Gets or sets the document count.
		/// </summary>
		/// <value>
		/// The document count.
		/// </value>
		public int? DocumentCount { get; set; }

		/// <summary>
		/// Gets or sets the end of window date.
		/// </summary>
		/// <value>
		/// The end of window date.
		/// </value>
		public DateTime? EndOfWindowDate { get; set; }


		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public string Notes { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>
		public bool? IsActive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is certificate modified.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is certificate modified; otherwise, <c>false</c>.
		/// </value>
		public bool? IsCertificateModified { get; set; }

		/// <summary>
		/// Gets or sets the is deleted.
		/// </summary>
		/// <value>
		/// The is deleted.
		/// </value>
		public bool? IsDeleted { get; set; }

		/// <summary>
		/// Gets or sets the cert extended no.
		/// </summary>
		/// <value>
		/// The cert extended no.
		/// </value>
		public int? CertExtendedNo { get; set; }

		/// <summary>
		/// Gets or sets the mapped order count.
		/// </summary>
		/// <value>
		/// The mapped order count.
		/// </value>
		public int MappedOrderCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is mandatory certificate.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is mandatory certificate; otherwise, <c>false</c>.
		/// </value>
		public bool IsMandatoryCertificate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is default.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is default; otherwise, <c>false</c>.
		/// </value>
		public bool IsDefault { get; set; }
	}
}
