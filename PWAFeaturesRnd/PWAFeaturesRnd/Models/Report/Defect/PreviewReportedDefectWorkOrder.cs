using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class PreviewReportedDefectWorkOrder
    {
        /// <summary>
        /// Gets or sets the dwo identifier.
        /// </summary>
        /// <value>
        /// The dwo identifier.
        /// </value>
        public string DwoId { get; set; }

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
        public DateTime? CompletedOn { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime? DueDate { get; set; }

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
        /// Gets or sets the remark and findings.
        /// </summary>
        /// <value>
        /// The remark and findings.
        /// </value>
        public string RemarkAndFindings { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is additional job reported.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is additional job reported; otherwise, <c>false</c>.
        /// </value>
        public bool IsAdditionalJobReported { get; set; }

        /// <summary>
        /// Gets or sets the symptoms.
        /// </summary>
        /// <value>
        /// The symptoms.
        /// </value>
        public List<DefectReportWorkOrderSymptom> Symptoms { get; set; }

        /// <summary>
        /// Gets or sets the parts used.
        /// </summary>
        /// <value>
        /// The parts used.
        /// </value>
        public List<DefectReportWorkOrderPartsUsed> PartsUsed { get; set; }
    }
}
