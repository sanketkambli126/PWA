namespace PWAFeaturesRnd.Models.Report.Certificate
{
    /// <summary>
    /// request object for certificate summary
    /// </summary>
    public class VesselCertificateSummaryStatRequest
    {

        /// <summary>
        /// Gets or sets the expiring in period.
        /// </summary>
        /// <value>
        /// The expiring in period.
        /// </value>
        public int? ExpiringInPeriod { get; set; }

        /// <summary>
        /// Gets or sets the certificate due now range.
        /// </summary>
        /// <value>
        /// The certificate due now range.
        /// </value>
        public int? CertificateDueNowRange { get; set; }

    }
}
