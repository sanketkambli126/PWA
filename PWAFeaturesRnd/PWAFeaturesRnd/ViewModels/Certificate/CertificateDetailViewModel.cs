namespace PWAFeaturesRnd.ViewModels.Certificate
{
	/// <summary>
	/// The certificate detail view model
	/// </summary>
	public class CertificateDetailViewModel : BaseViewModel
	{
		/// <summary>
		/// Gets or sets the vessel certificate identifier.
		/// </summary>
		/// <value>
		/// The vessel certificate identifier.
		/// </value>
		public string VesselCertificateId { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the message details json.
		/// </summary>
		/// <value>
		/// The message details json.
		/// </value>
		public string MessageDetailsJSON { get; set; }

		/// <summary>
		/// Gets or sets the certificate number.
		/// </summary>
		/// <value>
		/// The certificate number.
		/// </value>
		public string CertificateNumber { get; set; }

		/// <summary>
		/// Gets or sets the name of the certificate.
		/// </summary>
		/// <value>
		/// The name of the certificate.
		/// </value>
		public string CertificateName { get; set; }

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
		public string IssueDate { get; set; }

		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		public string Notes { get; set; }
	}
}
