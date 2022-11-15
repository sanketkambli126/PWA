namespace PWAFeaturesRnd.ViewModels.Certificate
{
    /// <summary>
    /// Vessel Certificate Summary Stat Response View model
    /// </summary>
    public class VesselCertificateSummaryStatResponseViewModel
    {
        /// <summary>
        /// Gets or sets all active certificate count.
        /// </summary>
        /// <value>
        /// All active certificate count.
        /// </value>
        public int AllActiveCertificateCount { get; set; }

        /// <summary>
        /// Gets or sets all active certificate count URL.
        /// </summary>
        /// <value>
        /// All active certificate count URL.
        /// </value>
        public string AllActiveCertificateCountURL { get; set; }

        /// <summary>
        /// Gets or sets the over due certificate count.
        /// </summary>
        /// <value>
        /// The over due certificate count.
        /// </value>
        public int OverDueCertificateCount { get; set; }

        /// <summary>
        /// Gets or sets the over due certificate count URL.
        /// </summary>
        /// <value>
        /// The over due certificate count URL.
        /// </value>
        public string OverDueCertificateCountURL { get; set; }

        /// <summary>
        /// Gets or sets the expires30 days certificate count.
        /// </summary>
        /// <value>
        /// The expires30 days certificate count.
        /// </value>
        public int Expires30DaysCertificateCount { get; set; }

        /// <summary>
        /// Gets or sets the expires30 days certificate count URL.
        /// </summary>
        /// <value>
        /// The expires30 days certificate count URL.
        /// </value>
        public string Expires30DaysCertificateCountURL { get; set; }

        /// <summary>
        /// Gets or sets the survey range certificate count.
        /// </summary>
        /// <value>
        /// The survey range certificate count.
        /// </value>
        public int SurveyRangeCertificateCount { get; set; }

        /// <summary>
        /// Gets or sets the survey range certificate count URL.
        /// </summary>
        /// <value>
        /// The survey range certificate count URL.
        /// </value>
        public string SurveyRangeCertificateCountURL { get; set; }

        /// <summary>
        /// Gets or sets the stop sailing trading expiring in30 days.
        /// </summary>
        /// <value>
        /// The stop sailing trading expiring in30 days.
        /// </value>
        public string StopSailingTradingExpiringIn30DaysUrl { get; set; }

        /// <summary>
        /// Gets or sets the stop sailing trading expiring in30 days coungt.
        /// </summary>
        /// <value>
        /// The stop sailing trading expiring in30 days coungt.
        /// </value>
        public int StopSailingTradingExpiringIn30DaysCount { get; set; }

        /// <summary>
        /// Gets or sets the stop sailing trading expiring in30 days kpi.
        /// </summary>
        /// <value>
        /// The stop sailing trading expiring in30 days kpi.
        /// </value>
        public int StopSailingTradingExpiringIn30DaysKPI { get; set; }

        /// <summary>
        /// Gets or sets the overdue priority.
        /// </summary>
        /// <value>
        /// The overdue priority.
        /// </value>
        public int OverDueCertificatePriority { get; set; }

        /// <summary>
        /// Gets or sets the expiring within x days priority.
        /// </summary>
        /// <value>
        /// The expiring within x days priority.
        /// </value>
        public int ExpiringXDaysCertificatePriority { get; set; }

        /// <summary>
        /// Gets or sets the survey range priority.
        /// </summary>
        /// <value>
        /// The survey range priority.
        /// </value>
        public int SurveyRangeCertificatePriority { get; set; }

    }
}
