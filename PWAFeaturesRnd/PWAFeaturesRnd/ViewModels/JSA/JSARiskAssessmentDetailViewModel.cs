namespace PWAFeaturesRnd.ViewModels.JSA
{
    /// <summary>
    /// 
    /// </summary>
    public class JSARiskAssessmentDetailViewModel
    {
        /// <summary>
        /// Gets or sets the JahId.
        /// </summary>
        /// <value>
        /// The JahId.
        /// </value>
        public string JahId { get; set; }

        /// <summary>
        /// Gets or sets the hazard number.
        /// </summary>
        /// <value>
        /// The hazard number.
        /// </value>
        public int HazardNumber { get; set; }

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

        /// <summary>
        /// Gets or sets the color of the parent risk.
        /// </summary>
        /// <value>
        /// The color of the parent risk.
        /// </value>
        public string ParentRiskColor { get; set; }

        /// <summary>
        /// Gets or sets the parentrisk factor description.
        /// </summary>
        /// <value>
        /// The parentrisk factor description.
        /// </value>
        public string ParentriskFactorDescription { get; set; }

        /// <summary>
        /// Gets or sets the average count.
        /// </summary>
        /// <value>
        /// The average count.
        /// </value>
        public string AvgCount { get; set; }

        /// <summary>
        /// Gets or sets the initial color of the risk.
        /// </summary>
        /// <value>
        /// The initial color of the risk.
        /// </value>
        public string InitialRiskColor { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initial risk visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is initial risk visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialRiskVisible { get; set; }

        /// <summary>
        /// Gets or sets the initial risk factor description.
        /// </summary>
        /// <value>
        /// The initial risk factor description.
        /// </value>
        public string InitialRiskFactorDescription { get; set; }
    }
}
