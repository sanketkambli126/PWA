using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.PlannedMaintenance
{
    /// <summary>
    /// Report Work Order ViewModel
    /// </summary>
    public class ReportWorkOrderViewModel
    {
        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the responsible department short code.
        /// </summary>
        /// <value>
        /// The responsible department short code.
        /// </value>
        public string ResponsibleDepartmentShortCode { get; set; }

        /// <summary>
        /// Gets or sets the responsibility rank short code.
        /// </summary>
        /// <value>
        /// The responsibility rank short code.
        /// </value>
        public string ResponsibilityRankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the approver required.
        /// </summary>
        /// <value>
        /// The approver required.
        /// </value>
        public string ApproverRequired { get; set; }

        /// <summary>
        /// Gets or sets the approver rank short code.
        /// </summary>
        /// <value>
        /// The approver rank short code.
        /// </value>
        public string ApproverRankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the office approval required.
        /// </summary>
        /// <value>
        /// The office approval required.
        /// </value>
        public string OfficeApprovalRequired { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public string DueDate { get; set; }

        /// <summary>
        /// Gets or sets the counter reading.
        /// </summary>
        /// <value>
        /// The counter reading.
        /// </value>
        public string CounterReading { get; set; }

        /// <summary>
        /// Gets or sets the left hours.
        /// </summary>
        /// <value>
        /// The left hours.
        /// </value>
        public string LeftHours { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show counter running hours].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show counter running hours]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowCounterRunningHours { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show counter revolutions].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show counter revolutions]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowCounterRevolutions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show counter events].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show counter events]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowCounterEvents { get; set; }

        /// <summary>
        /// Gets or sets the counter revolutions reading.
        /// </summary>
        /// <value>
        /// The counter revolutions reading.
        /// </value>
        public string CounterRevolutionsReading { get; set; }

        /// <summary>
        /// Gets or sets the counter events reading.
        /// </summary>
        /// <value>
        /// The counter events reading.
        /// </value>
        public string CounterEventsReading { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is left hours visible.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is left hours visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsLeftHoursVisible { get; set; }

        /// <summary>
        /// Gets or sets the job description.
        /// </summary>
        /// <value>
        /// The job description.
        /// </value>
        public string JobDescription { get; set; }

        /// <summary>
        /// Gets or sets the job description part1.
        /// </summary>
        /// <value>
        /// The job description part1.
        /// </value>
        public string JobDescriptionPart1 { get; set; }

        /// <summary>
        /// Gets or sets the job description part2.
        /// </summary>
        /// <value>
        /// The job description part2.
        /// </value>
        public string JobDescriptionPart2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [job description check].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [job description check]; otherwise, <c>false</c>.
        /// </value>
        public bool JobDescriptionCheck { get; set; }

        /// <summary>
        /// Gets or sets the PJB identifier.
        /// </summary>
        /// <value>
        /// The PJB identifier.
        /// </value>
        public string PjbId { get; set; }

        /// <summary>
        /// Gets or sets the order rank list.
        /// </summary>
        /// <value>
        /// The order rank list.
        /// </value>
        public List<WorkOrderRankViewModel> OrderRankList{ get; set; }

        /// <summary>
        /// Gets or sets the total man hours.
        /// </summary>
        /// <value>
        /// The total man hours.
        /// </value>
        public float? TotalManHours { get; set; }

        /// <summary>
        /// Gets or sets the pwo identifier.
        /// </summary>
        /// <value>
        /// The pwo identifier.
        /// </value>
        public string PwoId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is counter based.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is counter based; otherwise, <c>false</c>.
        /// </value>
        public bool IsCounterBased { get; set; }

        /// <summary>
        /// Gets or sets the PWH identifier.
        /// </summary>
        /// <value>
        /// The PWH identifier.
        /// </value>
        public string PwhId { get; set; }

        /// <summary>
        /// Gets or sets the report work done date.
        /// </summary>
        /// <value>
        /// The report work done date.
        /// </value>
        public string ReportWorkDoneDate { get; set; }

        /// <summary>
        /// Gets or sets the RWD due date.
        /// </summary>
        /// <value>
        /// The RWD due date.
        /// </value>
        public string RWDDueDate { get; set; }

        /// <summary>
        /// Gets or sets the responsibility rank.
        /// </summary>
        /// <value>
        /// The responsibility rank.
        /// </value>
        public string ResponsibilityRank { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is CBT identifier.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is CBT identifier; otherwise, <c>false</c>.
        /// </value>
        public bool IsCbtId { get; set; }

        /// <summary>
        /// Gets or sets the before condition.
        /// </summary>
        /// <value>
        /// The before condition.
        /// </value>
        public string BeforeCondition { get; set; }

        /// <summary>
        /// Gets or sets the after condition.
        /// </summary>
        /// <value>
        /// The after condition.
        /// </value>
        public string AfterCondition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is comment for reason.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is comment for reason; otherwise, <c>false</c>.
        /// </value>
        public bool IsCommentForReason { get; set; }

        /// <summary>
        /// Gets or sets the comment for reason.
        /// </summary>
        /// <value>
        /// The comment for reason.
        /// </value>
        public string CommentForReason { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the symptoms observed.
        /// </summary>
        /// <value>
        /// The symptoms observed.
        /// </value>
        public string SymptomsObserved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show other symptom].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show other symptom]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowOtherSymptom { get; set; }

        /// <summary>
        /// Gets or sets the other symptoms.
        /// </summary>
        /// <value>
        /// The other symptoms.
        /// </value>
        public string OtherSymptoms { get; set; }

        /// <summary>
        /// Gets or sets the parts used.
        /// </summary>
        /// <value>
        /// The parts used.
        /// </value>
        public List<SearchPartResponseViewModel> PartsUsed { get; set; }

        /// <summary>
        /// Gets or sets the vessel job description.
        /// </summary>
        /// <value>
        /// The vessel job description.
        /// </value>
        public string VesselJobDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [jsa required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [jsa required]; otherwise, <c>false</c>.
        /// </value>
        public bool JSARequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [permit required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [permit required]; otherwise, <c>false</c>.
        /// </value>
        public bool PermitRequired { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ReportWorkOrderViewModel"/> is critical.
        /// </summary>
        /// <value>
        ///   <c>true</c> if critical; otherwise, <c>false</c>.
        /// </value>
        public bool Critical { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }
    }
}
