using System;

namespace PWAFeaturesRnd.ViewModels.Certificate
{
	/// <summary>
	/// Certificate preview view model
	/// </summary>
	public class CertificatePreviewViewModel
	{
		/// <summary>
		/// Gets or sets the full name of the certificate.
		/// </summary>
		/// <value>
		/// The full name of the certificate.
		/// </value>
		public string CertificateFullName { get; set; }

		/// <summary>
		/// Gets or sets the certificate number.
		/// </summary>
		/// <value>
		/// The certificate number.
		/// </value>
		public string CertificateNumber { get; set; }

		/// <summary>
		/// Gets or sets the validity.
		/// </summary>
		/// <value>
		/// The validity.
		/// </value>
		public string Validity { get; set; }

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
		/// Gets or sets the end of window date.
		/// </summary>
		/// <value>
		/// The end of window date.
		/// </value>
		public DateTime? EndOfWindowDate { get; set; }

		/// <summary>
		/// Gets or sets the window formatted string.
		/// </summary>
		/// <value>
		/// The window formatted string.
		/// </value>
		public string WindowFormattedString { get; set; }

		/// <summary>
		/// Gets or sets the is mandatory certificate.
		/// </summary>
		/// <value>
		/// The is mandatory certificate.
		/// </value>
		public string IsMandatoryCertificate { get; set; }

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
		/// Gets or sets the issued by.
		/// </summary>
		/// <value>
		/// The issued by.
		/// </value>
		public string IssuedBy { get; set; }

		/// <summary>
		/// Gets or sets the is active.
		/// </summary>
		/// <value>
		/// The is active.
		/// </value>
		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets the is active text.
		/// </summary>
		/// <value>
		/// The is active text.
		/// </value>
		public string IsActiveText { get; set; }

		/// <summary>
		/// Gets or sets the type of the range.
		/// </summary>
		/// <value>
		/// The type of the range.
		/// </value>
		public string RangeType { get; set; }

		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public string Notes { get; set; }

		/// <summary>
		/// Gets or sets the DocumentCount.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public int DocumentCount { get; set; }

		/// <summary>
		/// Gets or sets the VesselCertificateLogId.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public string VesselCertificateLogId { get; set; }

		/// <summary>
		/// Gets or sets the vessel certificate identifier.
		/// </summary>
		/// <value>
		/// The vessel certificate identifier.
		/// </value>
		public string VesselCertificateId { get; set; }

		/// <summary>
		/// Gets or sets the mapped order count.
		/// </summary>
		/// <value>
		/// The mapped order count.
		/// </value>
		public int MappedOrderCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is certificate deleted.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is certificate deleted; otherwise, <c>false</c>.
		/// </value>
		public bool IsCertificateDeleted { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is certificate in active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is certificate in active; otherwise, <c>false</c>.
		/// </value>
		public bool IsCertificateInActive { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is certificate incomplete.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is certificate incomplete; otherwise, <c>false</c>.
		/// </value>
		public bool IsCertificateIncomplete { get; set; }

		/// <summary>
		/// Gets or sets the grid title.
		/// </summary>
		/// <value>
		/// The grid title.
		/// </value>
		public string GridTitle{ get; set; }

		/// <summary>
		/// Gets or sets the certificate details URL.
		/// </summary>
		/// <value>
		/// The certificate details URL.
		/// </value>
		public string CertificateDetailsUrl{ get; set; }

		/// <summary>
		/// Gets or sets the channel count.
		/// </summary>
		/// <value>
		/// The channel count.
		/// </value>
		public int ChannelCount { get; set; }

		/// <summary>
		/// Gets or sets the notes count.
		/// </summary>
		/// <value>
		/// The notes count.
		/// </value>
		public int NotesCount { get; set; }

		/// <summary>
		/// Gets or sets the message details json.
		/// </summary>
		/// <value>
		/// The message details json.
		/// </value>
		public string MessageDetailsJSON { get; set; }
	}
}
