using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class PreviewReportedDefectWorkOrderViewModel
    {
        /// <summary>
        /// Gets or sets the name of the defect.
        /// </summary>
        /// <value>
        /// The name of the defect.
        /// </value>
        public string DefectName { get; set; }

        /// <summary>
        /// Gets or sets the name of the system area.
        /// </summary>
        /// <value>
        /// The name of the system area.
        /// </value>
        public string SystemAreaName { get; set; }

        /// <summary>
        /// Gets or sets the completed on.
        /// </summary>
        /// <value>
        /// The completed on.
        /// </value>
        public string CompletedOn { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public string DueDate { get; set; }

        /// <summary>
        /// Gets or sets the condition before work done.
        /// </summary>
        /// <value>
        /// The condition before work done.
        /// </value>
        public string ConditionBeforeWorkDone { get; set; }

        /// <summary>
        /// Gets or sets the condition after work done.
        /// </summary>
        /// <value>
        /// The condition after work done.
        /// </value>
        public string ConditionAfterWorkDone { get; set; }

        /// <summary>
        /// Gets or sets the symptoms obeserved.
        /// </summary>
        /// <value>
        /// The symptoms obeserved.
        /// </value>
        public string SymptomsObeserved { get; set; }

        /// <summary>
        /// Gets or sets the other symptoms observed.
        /// </summary>
        /// <value>
        /// The other symptoms observed.
        /// </value>
        public string OtherSymptomsObserved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show other symptoms].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show other symptoms]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOtherSymptoms { get; set; }

        /// <summary>
        /// Gets or sets the is additional job reported.
        /// </summary>
        /// <value>
        /// The is additional job reported.
        /// </value>
        public string IsAdditionalJobReported { get; set; }

        /// <summary>
        /// Gets or sets the remark and findings.
        /// </summary>
        /// <value>
        /// The remark and findings.
        /// </value>
        public string RemarkAndFindings { get; set; }

        /// <summary>
        /// Gets or sets the parts used.
        /// </summary>
        /// <value>
        /// The parts used.
        /// </value>
        public List<DefectReportWorkOrderPartsUsedViewModel> PartsUsed { get; set; }
    }
}
