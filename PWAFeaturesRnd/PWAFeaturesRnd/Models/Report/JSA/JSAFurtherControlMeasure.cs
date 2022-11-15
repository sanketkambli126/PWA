using System;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// JSAFurtherControlMeasure
    /// </summary>
    public class JSAFurtherControlMeasure
    {
        /// <summary>
        /// Gets or sets the JFM identifier.
        /// </summary>
        /// <value>
        /// The JFM identifier.
        /// </value>
        public string JfmId { get; set; }

        /// <summary>
        /// Gets or sets the jah identifier.
        /// </summary>
        /// <value>
        /// The jah identifier.
        /// </value>
        public string JahId { get; set; }

        /// <summary>
        /// Gets or sets the rah identifier.
        /// </summary>
        /// <value>
        /// The rah identifier.
        /// </value>
        public string RahId { get; set; }

        /// <summary>
        /// Gets or sets the additional control rar number.
        /// </summary>
        /// <value>
        /// The additional control rar number.
        /// </value>
        public string AdditionalControlRARNumber { get; set; }

        /// <summary>
        /// Gets or sets the further risk.
        /// </summary>
        /// <value>
        /// The further risk.
        /// </value>
        public string FurtherRisk { get; set; }

        /// <summary>
        /// Gets or sets the action date.
        /// </summary>
        /// <value>
        /// The action date.
        /// </value>
        public DateTime ActionDate { get; set; }

        /// <summary>
        /// Gets or sets the review date.
        /// </summary>
        /// <value>
        /// The review date.
        /// </value>
        public DateTime ReviewDate { get; set; }

        /// <summary>
        /// Gets or sets the updated site.
        /// </summary>
        /// <value>
        /// The updated site.
        /// </value>
        public string UpdatedSite { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }
    }
}
