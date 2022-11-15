using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectReportWorkOrder
    {
        /// <summary>
        /// Gets or sets the symptoms.
        /// </summary>
        /// <value>
        /// The symptoms.
        /// </value>
        public List<DefectReportWorkOrderSymptom> Symptoms { get; set; }

        /// <summary>
        /// Gets or sets the staff rank.
        /// </summary>
        /// <value>
        /// The staff rank.
        /// </value>
        public List<DefectReportWorkOrderRank> StaffRank { get; set; }

        /// <summary>
        /// Gets or sets the hse assessment.
        /// </summary>
        /// <value>
        /// The hse assessment.
        /// </value>
        public List<DefectReportWorkOrderHSE> HSEAssessment { get; set; }

        /// <summary>
        /// Gets or sets the parts used.
        /// </summary>
        /// <value>
        /// The parts used.
        /// </value>
        public List<DefectReportWorkOrderPartsUsed> PartsUsed { get; set; }

        /// <summary>
        /// Gets or sets the related jobs.
        /// </summary>
        /// <value>
        /// The related jobs.
        /// </value>
        public List<ReportRelatedJob> RelatedJobs { get; set; }

        /// <summary>
        /// Gets or sets the additional jobs.
        /// </summary>
        /// <value>
        /// The additional jobs.
        /// </value>
        public List<RelatedJobsDetailResponse> AdditionalJobs { get; set; }

        /// <summary>
        /// Gets or sets the JJD identifier.
        /// </summary>
        /// <value>
        /// The JJD identifier.
        /// </value>
        public string JjdId { get; set; }

        /// <summary>
        /// Gets or sets the mod identifier.
        /// </summary>
        /// <value>
        /// The mod identifier.
        /// </value>
        public string ModId { get; set; }

        /// <summary>
        /// Gets or sets the is claim approved.
        /// </summary>
        /// <value>
        /// The is claim approved.
        /// </value>
        public bool? IsClaimApproved { get; set; }

        /// <summary>
        /// Gets or sets the spares received date.
        /// </summary>
        /// <value>
        /// The spares received date.
        /// </value>
        public DateTime? SparesReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the service received date.
        /// </summary>
        /// <value>
        /// The service received date.
        /// </value>
        public DateTime? ServiceReceivedDate { get; set; }

        /// <summary>
        /// Gets or sets the closed date.
        /// </summary>
        /// <value>
        /// The closed date.
        /// </value>
        public DateTime? ClosedDate { get; set; }

        /// <summary>
        /// Gets or sets the yard guarantee number.
        /// </summary>
        /// <value>
        /// The yard guarantee number.
        /// </value>
        public string YardGuaranteeNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the PLK identifier off hire.
        /// </summary>
        /// <value>
        /// The type of the PLK identifier off hire.
        /// </value>
        public string PlkIdOffHireType { get; set; }

        /// <summary>
        /// Gets or sets the type of the off hire.
        /// </summary>
        /// <value>
        /// The type of the off hire.
        /// </value>
        public string OffHireType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is gas free.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is gas free; otherwise, <c>false</c>.
        /// </value>
        public bool IsGasFree { get; set; }

        /// <summary>
        /// Gets or sets the off hire hours.
        /// </summary>
        /// <value>
        /// The off hire hours.
        /// </value>
        public int? OffHireHours { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is moc required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is moc required; otherwise, <c>false</c>.
        /// </value>
        public bool IsMOCRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [dispensation in place].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [dispensation in place]; otherwise, <c>false</c>.
        /// </value>
        public bool DispensationInPlace { get; set; }

        /// <summary>
        /// Gets or sets the DRW identifier.
        /// </summary>
        /// <value>
        /// The DRW identifier.
        /// </value>
        public string DrwId { get; set; }

        /// <summary>
        /// Gets or sets the dwo identifier.
        /// </summary>
        /// <value>
        /// The dwo identifier.
        /// </value>
        public string DwoId { get; set; }

        /// <summary>
        /// Gets or sets the condition before identifier.
        /// </summary>
        /// <value>
        /// The condition before identifier.
        /// </value>
        public string ConditionBeforeId { get; set; }

        /// <summary>
        /// Gets or sets the condition after identifier.
        /// </summary>
        /// <value>
        /// The condition after identifier.
        /// </value>
        public string ConditionAfterId { get; set; }

        /// <summary>
        /// Gets or sets the PWR identifier.
        /// </summary>
        /// <value>
        /// The PWR identifier.
        /// </value>
        public string PwrId { get; set; }

        /// <summary>
        /// Gets or sets the work done date.
        /// </summary>
        /// <value>
        /// The work done date.
        /// </value>
        public DateTime WorkDoneDate { get; set; }

        /// <summary>
        /// Gets or sets the estimated cost.
        /// </summary>
        /// <value>
        /// The estimated cost.
        /// </value>
        public decimal? EstimatedCost { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the change in timeline comment.
        /// </summary>
        /// <value>
        /// The change in timeline comment.
        /// </value>
        public string ChangeInTimelineComment { get; set; }

        /// <summary>
        /// Gets or sets the is off hire.
        /// </summary>
        /// <value>
        /// The is off hire.
        /// </value>
        public bool? IsOffHire { get; set; }

        /// <summary>
        /// Gets or sets the off hire reason.
        /// </summary>
        /// <value>
        /// The off hire reason.
        /// </value>
        public string OffHireReason { get; set; }

        /// <summary>
        /// Gets or sets the actual time.
        /// </summary>
        /// <value>
        /// The actual time.
        /// </value>
        public int? ActualTime { get; set; }

        /// <summary>
        /// Gets or sets the off hire period identifier.
        /// </summary>
        /// <value>
        /// The off hire period identifier.
        /// </value>
        public string OffHirePeriodId { get; set; }

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
        /// Gets or sets a value indicating whether this instance is jsa required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is jsa required; otherwise, <c>false</c>.
        /// </value>
        public bool IsJSARequired { get; set; }

        /// <summary>
        /// Gets or sets the off hire mins.
        /// </summary>
        /// <value>
        /// The off hire mins.
        /// </value>
        public int? OffHireMins { get; set; }
    }
}
