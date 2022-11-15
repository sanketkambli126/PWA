using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Models.Report.JSA;

namespace PWAFeaturesRnd.ViewModels.JSA
{
    public class JSAHazardDetailsViewModel
    {

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
        /// Gets or sets the control measures.
        /// </summary>
        /// <value>
        /// The control measures.
        /// </value>
        public string ControlMeasures { get; set; }

        /// <summary>
        /// Gets or sets the consequences.
        /// </summary>
        /// <value>
        /// The consequences.
        /// </value>
        public string Consequences { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [hazard active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [hazard active]; otherwise, <c>false</c>.
        /// </value>
        public bool HazardActive { get; set; }

        /// <summary>
        /// Gets or sets the likelihood description.
        /// </summary>
        /// <value>
        /// The likelihood description.
        /// </value>
        public string LikelihoodDescription { get; set; }

        /// <summary>
        /// Gets or sets the severity description.
        /// </summary>
        /// <value>
        /// The severity description.
        /// </value>
        public string SeverityDescription { get; set; }

        /// <summary>
        /// Gets or sets the risk factor description.
        /// </summary>
        /// <value>
        /// The risk factor description.
        /// </value>
        public string RiskFactorDescription { get; set; }

        /// <summary>
        /// Gets or sets the risk count.
        /// </summary>
        /// <value>
        /// The risk count.
        /// </value>
        public int RiskCount { get; set; }

        /// <summary>
        /// Gets or sets the further control measure.
        /// </summary>
        /// <value>
        /// The further control measure.
        /// </value>
        public string FurtherControlMeasure { get; set; }

        /// <summary>
        /// Gets or sets the further control measure.
        /// </summary>
        /// <value>
        /// The further control measure.
        /// </value>
        public int FurtherControlMeasureCount { get; set; }

        /// <summary>
        /// Gets or sets the reported date.
        /// </summary>
        /// <value>
        /// The reported date.
        /// </value>
        public string ReportedDate { get; set; }

        /// <summary>
        /// Gets or sets the severity definition.
        /// </summary>
        /// <value>
        /// The severity definition.
        /// </value>
        public string SeverityDefinition { get; set; }

        /// <summary>
        /// Gets or sets the likelihood definition.
        /// </summary>
        /// <value>
        /// The likelihood definition.
        /// </value>
        public string LikelihoodDefinition { get; set; }

        /// <summary>
        /// Gets or sets the risk factor definition.
        /// </summary>
        /// <value>
        /// The risk factor definition.
        /// </value>
        public string RiskFactorDefinition { get; set; }

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
        /// Gets or sets the color of the risk.
        /// </summary>
        /// <value>
        /// The color of the risk.
        /// </value>
        public string RiskColor { get; set; }
    }
}
