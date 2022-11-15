namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectReportWorkOrderViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is off hire required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is off hire required; otherwise, <c>false</c>.
        /// </value>
        public bool IsOffHireRequired { get; set; }

        /// <summary>
        /// Gets or sets the actual time.
        /// </summary>
        /// <value>
        /// The actual time.
        /// </value>
        public int ActualTime { get; set; }

        /// <summary>
        /// Gets or sets the off hire mins.
        /// </summary>
        /// <value>
        /// The off hire mins.
        /// </value>
        public int OffHireMins { get; set; }

        /// <summary>
        /// Gets or sets the off hire mins.
        /// </summary>
        /// <value>
        /// The off hire mins.
        /// </value>
        public int OffHireHours { get; set; }

        /// <summary>
        /// Gets or sets the offhire type identifier.
        /// </summary>
        /// <value>
        /// The offhire type identifier.
        /// </value>
        public string OffhireTypeId { get; set; }

        /// <summary>
        /// Gets or sets the impact identifier.
        /// </summary>
        /// <value>
        /// The impact identifier.
        /// </value>
        public string ImpactId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is regulatory authority.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is regulatory authority; otherwise, <c>false</c>.
        /// </value>
        public bool IsRegulatoryAuthority { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [dispensation in place].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [dispensation in place]; otherwise, <c>false</c>.
        /// </value>
        public bool DispensationInPlace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is gas free.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is gas free; otherwise, <c>false</c>.
        /// </value>
        public bool IsGasFree { get; set; }

        /// <summary>
        /// Gets or sets the off hire reason.
        /// </summary>
        /// <value>
        /// The off hire reason.
        /// </value>
        public string OffHireReason { get; set; }
    }
}
