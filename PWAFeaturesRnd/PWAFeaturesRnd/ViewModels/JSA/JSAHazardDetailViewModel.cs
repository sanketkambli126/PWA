using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    /// <summary>
    /// 
    /// </summary>
    public class JSAHazardDetailViewModel
    {
        /// <summary>
        /// Gets or sets the hazard number.
        /// </summary>
        /// <value>
        /// The hazard number.
        /// </value>
        public int HazardNumber { get; set; }

        /// <summary>
        /// Gets or sets the JahId.
        /// </summary>
        /// <value>
        /// The JahId.
        /// </value>
        public string JahId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the likelihood description.
        /// </summary>
        /// <value>
        /// The likelihood description.
        /// </value>
        public string LikelihoodDescription { get; set; }

        /// <summary>
        /// Gets or sets the color of the likelihood.
        /// </summary>
        /// <value>
        /// The color of the likelihood.
        /// </value>
        public string LikelihoodColor { get; set; }

        /// <summary>
        /// Gets or sets the color of the severity.
        /// </summary>
        /// <value>
        /// The color of the severity.
        /// </value>
        public string SeverityColor { get; set; }

        /// <summary>
        /// Gets or sets the severity description.
        /// </summary>
        /// <value>
        /// The severity description.
        /// </value>
        public string SeverityDescription { get; set; }

        /// <summary>
        /// Gets or sets the color of the risk.
        /// </summary>
        /// <value>
        /// The color of the risk.
        /// </value>
        public string RiskColor { get; set; }

        /// <summary>
        /// Gets or sets the risk factor description.
        /// </summary>
        /// <value>
        /// The risk factor description.
        /// </value>
        public string RiskFactorDescription { get; set; }

        /// <summary>
        /// Gets or sets the work activity description.
        /// </summary>
        /// <value>
        /// The work activity description.
        /// </value>
        public string WorkActivityDescription { get; set; }

    }
}
