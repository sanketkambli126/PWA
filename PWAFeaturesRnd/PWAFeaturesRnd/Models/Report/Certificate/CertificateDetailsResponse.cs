using System;

namespace PWAFeaturesRnd.Models.Report.Certificate
{
	/// <summary>
	/// The CertificateDetailsResponse
	/// </summary>
	public class CertificateDetailsResponse
	{
		/// <summary>
		/// Gets or sets the vessel certificate identifier.
		/// </summary>
		/// <value>
		/// The vessel certificate identifier.
		/// </value>
		public string VesselCertificateId { get; set; }
		
		/// <summary>
		/// Gets or sets the annotation.
		/// </summary>
		/// <value>
		/// The annotation.
		/// </value>
		public string Annotation { get; set; }
		
		/// <summary>
		/// Gets or sets the name of the certificate.
		/// </summary>
		/// <value>
		/// The name of the certificate.
		/// </value>
		public string CertificateName { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }
		
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
		/// Gets or sets the issue date.
		/// </summary>
		/// <value>
		/// The issue date.
		/// </value>
		public DateTime? IssueDate { get; set; }
		
		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public string Notes { get; set; }

		/// <summary>
		/// Gets or sets the certificate extended number.
		/// </summary>
		/// <value>
		/// The certificate extended number.
		/// </value>
		public int? CertificateExtendedNumber { get; set; }

		/// <summary>
		/// Gets or sets the certificate code.
		/// </summary>
		/// <value>
		/// The certificate code.
		/// </value>
		public string CertificateCode { get; set; }
	}
}
