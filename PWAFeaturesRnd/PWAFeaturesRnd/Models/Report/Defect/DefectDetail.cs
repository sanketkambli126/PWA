using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectDetail
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is gas free.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is gas free; otherwise, <c>false</c>.
        /// </value>
        public bool IsGasFree { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is moc required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is moc required; otherwise, <c>false</c>.
        /// </value>
        public bool IsMOCRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is jsa required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is jsa required; otherwise, <c>false</c>.
        /// </value>
        public bool IsJSARequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [dispensation in place].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [dispensation in place]; otherwise, <c>false</c>.
        /// </value>
        public bool DispensationInPlace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is class approval required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is class approval required; otherwise, <c>false</c>.
        /// </value>
        public bool IsClassApprovalRequired { get; set; }

        /// <summary>
        /// Gets or sets the impact identifier.
        /// </summary>
        /// <value>
        /// The impact identifier.
        /// </value>
        public string ImpactId { get; set; }

        /// <summary>
        /// Gets or sets the off hire period identifier.
        /// </summary>
        /// <value>
        /// The off hire period identifier.
        /// </value>
        public string OffHirePeriodId { get; set; }

        /// <summary>
        /// Gets or sets the estimated period.
        /// </summary>
        /// <value>
        /// The estimated period.
        /// </value>
        public int? EstimatedPeriod { get; set; }

        /// <summary>
        /// Gets or sets the type of the PLK identifier off hire.
        /// </summary>
        /// <value>
        /// The type of the PLK identifier off hire.
        /// </value>
        public string PlkIdOffHireType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is off hire.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
        /// </value>
        public bool IsOffHire { get; set; }

        /// <summary>
        /// Gets or sets the staff detail.
        /// </summary>
        /// <value>
        /// The staff detail.
        /// </value>
        public List<DefectReportWorkOrderRank> StaffDetail { get; set; }

        /// <summary>
        /// Gets or sets the header detail.
        /// </summary>
        /// <value>
        /// The header detail.
        /// </value>
        public DefectHeaderDetail HeaderDetail { get; set; }

        /// <summary>
        /// Gets or sets the specification.
        /// </summary>
        /// <value>
        /// The specification.
        /// </value>
        public List<DefectSpecification> Specification { get; set; }

        /// <summary>
        /// Gets or sets the repair specification.
        /// </summary>
        /// <value>
        /// The repair specification.
        /// </value>
        public string RepairSpecification { get; set; }

        /// <summary>
        /// Gets or sets the defect description.
        /// </summary>
        /// <value>
        /// The defect description.
        /// </value>
        public string DefectDescription { get; set; }

        /// <summary>
        /// Gets or sets the estimated cost.
        /// </summary>
        /// <value>
        /// The estimated cost.
        /// </value>
        public decimal? EstimatedCost { get; set; }

        /// <summary>
        /// Gets or sets the current identifier.
        /// </summary>
        /// <value>
        /// The current identifier.
        /// </value>
        public string CurId { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets the yard guarantee number.
        /// </summary>
        /// <value>
        /// The yard guarantee number.
        /// </value>
        public string YardGuaranteeNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the off hire.
        /// </summary>
        /// <value>
        /// The type of the off hire.
        /// </value>
        public string OffHireType { get; set; }
    }
}
