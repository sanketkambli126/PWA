namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// Voyage Reporting Modal Request ViewModel
    /// </summary>
    public class VoyageReportingModalRequestViewModel
    {
        /// <summary>
        /// Gets or sets the spa identifier.
        /// </summary>
        /// <value>
        /// The spa identifier.
        /// </value>
        public string SpaId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is break in passage.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is break in passage; otherwise, <c>false</c>.
        /// </value>
        public bool IsBreakInPassage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [bad weather alert].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [bad weather alert]; otherwise, <c>false</c>.
        /// </value>
        public bool BadWeatherAlert { get; set; }

    }
}
