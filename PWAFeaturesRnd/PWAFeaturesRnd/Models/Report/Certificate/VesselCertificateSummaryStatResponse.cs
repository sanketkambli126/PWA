using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Certificate
{
	/// <summary>
	/// Vessel Certificate Summary Stat Response
	/// </summary>
	public class VesselCertificateSummaryStatResponse
    {
		/// <summary>
		/// Gets or sets the statistic.
		/// </summary>
		/// <value>
		/// The statistic.
		/// </value>
		public VesselCertificateStatistic Statistic { get; set; }

		/// <summary>
		/// Gets or sets the certificate count.
		/// </summary>
		/// <value>
		/// The certificate count.
		/// </value>
		public int CertificateCount { get; set; }

		/// <summary>
		/// Gets or sets the kpi priority.
		/// </summary>
		/// <value>
		/// The kpi priority.
		/// </value>
		public int KPIPriority { get; set; }
	}
}
