using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
	/// <summary>
	/// ReportWorkOrder
	/// </summary>
	public class ReportWorkOrder
	{
		/// <summary>
		/// Gets or sets the CBM sampling point.
		/// </summary>
		/// <value>
		/// The CBM sampling point.
		/// </value>
		public string CbmSamplingPoint { get; set; }

		/// <summary>
		/// Gets or sets the GRD identifier.
		/// </summary>
		/// <value>
		/// The GRD identifier.
		/// </value>
		public string GrdId { get; set; }

		/// <summary>
		/// Gets or sets the CBM comments.
		/// </summary>
		/// <value>
		/// The CBM comments.
		/// </value>
		public string CBMComments { get; set; }

		/// <summary>
		/// Gets or sets the work order history CBM values.
		/// </summary>
		/// <value>
		/// The work order history CBM values.
		/// </value>
		public List<WorkOrderHistoryCBMValue> WorkOrderHistoryCBMValues { get; set; }

		/// <summary>
		/// Gets or sets the status identifier.
		/// </summary>
		/// <value>
		/// The status identifier.
		/// </value>
		public string StatusId { get; set; }

		/// <summary>
		/// Gets or sets the counter events reading.
		/// </summary>
		/// <value>
		/// The counter events reading.
		/// </value>
		public int? CounterEventsReading { get; set; }

		/// <summary>
		/// Gets or sets the counter revolutions reading.
		/// </summary>
		/// <value>
		/// The counter revolutions reading.
		/// </value>
		public int? CounterRevolutionsReading { get; set; }

		/// <summary>
		/// Gets or sets the job interval values.
		/// </summary>
		/// <value>
		/// The job interval values.
		/// </value>
		public List<TaskIntervalValue> JobIntervalValues { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has event counter.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has event counter; otherwise, <c>false</c>.
		/// </value>
		public bool HasEventCounter { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has revolution counter.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has revolution counter; otherwise, <c>false</c>.
		/// </value>
		public bool HasRevolutionCounter { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has running hours counter.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has running hours counter; otherwise, <c>false</c>.
		/// </value>
		public bool HasRunningHoursCounter { get; set; }

		/// <summary>
		/// Gets or sets the job interval identifier.
		/// </summary>
		/// <value>
		/// The job interval identifier.
		/// </value>
		public string JobIntervalId { get; set; }

		/// <summary>
		/// Gets or sets the currency.
		/// </summary>
		/// <value>
		/// The currency.
		/// </value>
		public string Currency { get; set; }

		/// <summary>
		/// Gets or sets the shore staff cost.
		/// </summary>
		/// <value>
		/// The shore staff cost.
		/// </value>
		public decimal? ShoreStaffCost { get; set; }

		/// <summary>
		/// Gets or sets the shore staff man hours.
		/// </summary>
		/// <value>
		/// The shore staff man hours.
		/// </value>
		public int? ShoreStaffManHours { get; set; }

		/// <summary>
		/// Gets or sets the work order symptoms.
		/// </summary>
		/// <value>
		/// The work order symptoms.
		/// </value>
		public List<WorkOrderSymptomDetail> WorkOrderSymptoms { get; set; }

		/// <summary>
		/// Gets or sets the related jobs.
		/// </summary>
		/// <value>
		/// The related jobs.
		/// </value>
		public List<ReportRelatedJob> RelatedJobs { get; set; }

		/// <summary>
		/// Gets or sets the risks.
		/// </summary>
		/// <value>
		/// The risks.
		/// </value>
		public List<ScheduleTaskHSERisk> Risks { get; set; }

		/// <summary>
		/// Gets or sets the parts used.
		/// </summary>
		/// <value>
		/// The parts used.
		/// </value>
		public List<SearchPartResponse> PartsUsed { get; set; }

		/// <summary>
		/// Gets or sets the work order ranks.
		/// </summary>
		/// <value>
		/// The work order ranks.
		/// </value>
		public List<WorkOrderRank> WorkOrderRanks { get; set; }

		/// <summary>
		/// Gets or sets the supporting notes.
		/// </summary>
		/// <value>
		/// The supporting notes.
		/// </value>
		public List<ScheduleTaskSupportingNotes> SupportingNotes { get; set; }

		/// <summary>
		/// Gets or sets the CBM sampling method.
		/// </summary>
		/// <value>
		/// The CBM sampling method.
		/// </value>
		public string CbmSamplingMethod { get; set; }

		/// <summary>
		/// Gets or sets the comment for reason.
		/// </summary>
		/// <value>
		/// The comment for reason.
		/// </value>
		public string CommentForReason { get; set; }

		/// <summary>
		/// Gets or sets the frequency.
		/// </summary>
		/// <value>
		/// The frequency.
		/// </value>
		public decimal? Frequency { get; set; }

		/// <summary>
		/// Gets or sets the CMP identifier.
		/// </summary>
		/// <value>
		/// The CMP identifier.
		/// </value>
		public string CmpId { get; set; }

		/// <summary>
		/// Gets or sets the ships work order.
		/// </summary>
		/// <value>
		/// The ships work order.
		/// </value>
		public List<UnplannedWorkOrderDetail> ShipsWorkOrder { get; set; }

		/// <summary>
		/// Gets or sets the ships work order identifier.
		/// </summary>
		/// <value>
		/// The ships work order identifier.
		/// </value>
		public List<string> ShipsWorkOrderId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule por identifier.
		/// </summary>
		/// <value>
		/// The reschedule por identifier.
		/// </value>
		public string ReschedulePorId { get; set; }

		/// <summary>
		/// Gets or sets the reschedule por request identifier.
		/// </summary>
		/// <value>
		/// The reschedule por request identifier.
		/// </value>
		public string ReschedulePorRequestId { get; set; }

		/// <summary>
		/// Gets or sets the round job component title.
		/// </summary>
		/// <value>
		/// The round job component title.
		/// </value>
		public string RoundJobComponentTitle { get; set; }

		/// <summary>
		/// Gets or sets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		public List<RoundsJobParameter> Parameters { get; set; }

		/// <summary>
		/// Gets or sets the por identifier.
		/// </summary>
		/// <value>
		/// The por identifier.
		/// </value>
		public string PorId { get; set; }

		/// <summary>
		/// Gets or sets the associated jobs.
		/// </summary>
		/// <value>
		/// The associated jobs.
		/// </value>
		public List<AssociatedJob> AssociatedJobs { get; set; }

		/// <summary>
		/// Gets or sets the jsa job identifier.
		/// </summary>
		/// <value>
		/// The jsa job identifier.
		/// </value>
		public string JsaJobId { get; set; }

		/// <summary>
		/// Gets or sets the name of the parent job.
		/// </summary>
		/// <value>
		/// The name of the parent job.
		/// </value>
		public string ParentJobName { get; set; }

		/// <summary>
		/// Gets or sets the last completed date.
		/// </summary>
		/// <value>
		/// The last completed date.
		/// </value>
		public DateTime? LastCompletedDate { get; set; }

		/// <summary>
		/// Gets or sets the indication type identifier.
		/// </summary>
		/// <value>
		/// The indication type identifier.
		/// </value>
		public string IndicationTypeId { get; set; }

		/// <summary>
		/// Gets or sets the triggered jobs.
		/// </summary>
		/// <value>
		/// The triggered jobs.
		/// </value>
		public List<JobsToTrigger> TriggeredJobs { get; set; }

		/// <summary>
		/// Gets or sets the approver rank identifier.
		/// </summary>
		/// <value>
		/// The approver rank identifier.
		/// </value>
		public string ApproverRankId { get; set; }

		/// <summary>
		/// Gets or sets the responsible department identifier.
		/// </summary>
		/// <value>
		/// The responsible department identifier.
		/// </value>
		public string ResponsibleDepartmentId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has rescheduled.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has rescheduled; otherwise, <c>false</c>.
		/// </value>
		public bool HasRescheduled { get; set; }

		/// <summary>
		/// Gets or sets the reported on.
		/// </summary>
		/// <value>
		/// The reported on.
		/// </value>
		public DateTime? ReportedOn { get; set; }

		/// <summary>
		/// Gets or sets the reported by.
		/// </summary>
		/// <value>
		/// The reported by.
		/// </value>
		public string ReportedBy { get; set; }

		/// <summary>
		/// Gets or sets the PJL identifier.
		/// </summary>
		/// <value>
		/// The PJL identifier.
		/// </value>
		public string PjlId { get; set; }

		/// <summary>
		/// Gets or sets the component counter readings.
		/// </summary>
		/// <value>
		/// The component counter readings.
		/// </value>
		public List<ComponentCounterDetail> ComponentCounterReadings { get; set; }

		/// <summary>
		/// Gets or sets the mla source identifier.
		/// </summary>
		/// <value>
		/// The mla source identifier.
		/// </value>
		public string MlaSourceId { get; set; }

		/// <summary>
		/// Gets or sets the TCM identifier.
		/// </summary>
		/// <value>
		/// The TCM identifier.
		/// </value>
		public string TcmId { get; set; }

		/// <summary>
		/// Gets or sets from range interval value.
		/// </summary>
		/// <value>
		/// From range interval value.
		/// </value>
		public int? FromRangeIntervalValue { get; set; }

		/// <summary>
		/// Gets or sets the reason.
		/// </summary>
		/// <value>
		/// The reason.
		/// </value>
		public string Reason { get; set; }

		/// <summary>
		/// Gets or sets the remark.
		/// </summary>
		/// <value>
		/// The remark.
		/// </value>
		public string Remark { get; set; }

		/// <summary>
		/// Gets or sets the type of the due date interval.
		/// </summary>
		/// <value>
		/// The type of the due date interval.
		/// </value>
		public string DueDateIntervalType { get; set; }

		/// <summary>
		/// Gets or sets the due date interval type identifier.
		/// </summary>
		/// <value>
		/// The due date interval type identifier.
		/// </value>
		public string DueDateIntervalTypeId { get; set; }

		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime DueDate { get; set; }

		/// <summary>
		/// Gets or sets the vessel job description.
		/// </summary>
		/// <value>
		/// The vessel job description.
		/// </value>
		public string VesselJobDescription { get; set; }

		/// <summary>
		/// Gets or sets the job description.
		/// </summary>
		/// <value>
		/// The job description.
		/// </value>
		public string JobDescription { get; set; }

		/// <summary>
		/// Gets or sets the job guideline text.
		/// </summary>
		/// <value>
		/// The job guideline text.
		/// </value>
		public string JobGuidelineText { get; set; }

		/// <summary>
		/// Gets or sets the attachment required.
		/// </summary>
		/// <value>
		/// The attachment required.
		/// </value>
		public bool? AttachmentRequired { get; set; }

		/// <summary>
		/// Gets or sets the spares required.
		/// </summary>
		/// <value>
		/// The spares required.
		/// </value>
		public bool? SparesRequired { get; set; }

		/// <summary>
		/// Gets or sets the type of the job.
		/// </summary>
		/// <value>
		/// The type of the job.
		/// </value>
		public string JobType { get; set; }

		/// <summary>
		/// Gets or sets the job type identifier.
		/// </summary>
		/// <value>
		/// The job type identifier.
		/// </value>
		public string JobTypeId { get; set; }

		/// <summary>
		/// Gets or sets the name of the job.
		/// </summary>
		/// <value>
		/// The name of the job.
		/// </value>
		public string JobName { get; set; }

		/// <summary>
		/// Gets or sets the name of the component.
		/// </summary>
		/// <value>
		/// The name of the component.
		/// </value>
		public string ComponentName { get; set; }

		/// <summary>
		/// Gets or sets the pwo identifier.
		/// </summary>
		/// <value>
		/// The pwo identifier.
		/// </value>
		public string PwoId { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the ves identifier.
		/// </summary>
		/// <value>
		/// The ves identifier.
		/// </value>
		public string VesId { get; set; }

		/// <summary>
		/// Gets or sets the system area.
		/// </summary>
		/// <value>
		/// The system area.
		/// </value>
		public string SystemArea { get; set; }

		/// <summary>
		/// Gets or sets the system area identifier.
		/// </summary>
		/// <value>
		/// The system area identifier.
		/// </value>
		public string SystemAreaId { get; set; }

		/// <summary>
		/// Gets or sets the CBT identifier.
		/// </summary>
		/// <value>
		/// The CBT identifier.
		/// </value>
		public string CbtId { get; set; }

		/// <summary>
		/// Gets or sets the PJB identifier.
		/// </summary>
		/// <value>
		/// The PJB identifier.
		/// </value>
		public string PjbId { get; set; }

		/// <summary>
		/// Gets or sets the PTR identifier.
		/// </summary>
		/// <value>
		/// The PTR identifier.
		/// </value>
		public string PtrId { get; set; }

		/// <summary>
		/// Gets or sets the PST identifier.
		/// </summary>
		/// <value>
		/// The PST identifier.
		/// </value>
		public string PstId { get; set; }

		/// <summary>
		/// Gets or sets the due interval.
		/// </summary>
		/// <value>
		/// The due interval.
		/// </value>
		public int DueInterval { get; set; }

		/// <summary>
		/// Gets or sets the reason identifier.
		/// </summary>
		/// <value>
		/// The reason identifier.
		/// </value>
		public string ReasonId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is calender based.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is calender based; otherwise, <c>false</c>.
		/// </value>
		public bool IsCalenderBased { get; set; }

		/// <summary>
		/// Gets or sets the responsibility rank.
		/// </summary>
		/// <value>
		/// The responsibility rank.
		/// </value>
		public string ResponsibilityRank { get; set; }

		/// <summary>
		/// Gets or sets the symptom.
		/// </summary>
		/// <value>
		/// The symptom.
		/// </value>
		public string Symptom { get; set; }

		/// <summary>
		/// Gets or sets the symptom identifier.
		/// </summary>
		/// <value>
		/// The symptom identifier.
		/// </value>
		public string SymptomId { get; set; }

		/// <summary>
		/// Gets or sets the after condition.
		/// </summary>
		/// <value>
		/// The after condition.
		/// </value>
		public string AfterCondition { get; set; }

		/// <summary>
		/// Gets or sets the after condition identifier.
		/// </summary>
		/// <value>
		/// The after condition identifier.
		/// </value>
		public string AfterConditionId { get; set; }

		/// <summary>
		/// Gets or sets the before condition.
		/// </summary>
		/// <value>
		/// The before condition.
		/// </value>
		public string BeforeCondition { get; set; }

		/// <summary>
		/// Gets or sets the before condition identifier.
		/// </summary>
		/// <value>
		/// The before condition identifier.
		/// </value>
		public string BeforeConditionId { get; set; }

		/// <summary>
		/// Gets or sets the work done date.
		/// </summary>
		/// <value>
		/// The work done date.
		/// </value>
		public DateTime WorkDoneDate { get; set; }

		/// <summary>
		/// Gets or sets the PWH identifier.
		/// </summary>
		/// <value>
		/// The PWH identifier.
		/// </value>
		public string PwhId { get; set; }

		/// <summary>
		/// Gets or sets the approver rank short code.
		/// </summary>
		/// <value>
		/// The approver rank short code.
		/// </value>
		public string ApproverRankShortCode { get; set; }

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
		public bool? ApproverRequired { get; set; }

		/// <summary>
		/// Gets or sets the planned date.
		/// </summary>
		/// <value>
		/// The planned date.
		/// </value>
		public DateTime PlannedDate { get; set; }

		/// <summary>
		/// Gets or sets the is shore staff involved.
		/// </summary>
		/// <value>
		/// The is shore staff involved.
		/// </value>
		public bool? IsShoreStaffInvolved { get; set; }

		/// <summary>
		/// Gets or sets the counter reading.
		/// </summary>
		/// <value>
		/// The counter reading.
		/// </value>
		public int? CounterReading { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [report form data migration required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [report form data migration required]; otherwise, <c>false</c>.
		/// </value>
		public bool ReportFormDataMigrationRequired { get; set; }

		/// <summary>
		/// Gets or sets the report form required.
		/// </summary>
		/// <value>
		/// The report form required.
		/// </value>
		public bool? ReportFormRequired { get; set; }

		/// <summary>
		/// Gets or sets the hse assessment required.
		/// </summary>
		/// <value>
		/// The hse assessment required.
		/// </value>
		public bool? HSEAssessmentRequired { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [office approval required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [office approval required]; otherwise, <c>false</c>.
		/// </value>
		public bool OfficeApprovalRequired { get; set; }

		/// <summary>
		/// Gets or sets the approver rank.
		/// </summary>
		/// <value>
		/// The approver rank.
		/// </value>
		public string ApproverRank { get; set; }

		/// <summary>
		/// Gets or sets the responsible department.
		/// </summary>
		/// <value>
		/// The responsible department.
		/// </value>
		public string ResponsibleDepartment { get; set; }

		/// <summary>
		/// Gets or sets the responsibility rank identifier.
		/// </summary>
		/// <value>
		/// The responsibility rank identifier.
		/// </value>
		public string ResponsibilityRankId { get; set; }

		/// <summary>
		/// Gets or sets from due date.
		/// </summary>
		/// <value>
		/// From due date.
		/// </value>
		public DateTime? FromDueDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [jsa required].
		/// </summary>
		/// <value>
		///   <c>true</c> if [jsa required]; otherwise, <c>false</c>.
		/// </value>
		public bool? JSARequired { get; set; }


		/// <summary>
		/// Gets or sets the jsa permit required.
		/// </summary>
		/// <value>
		/// The jsa permit required.
		/// </value>
		public bool? JSAPermitRequired { get; set; }

		/// <summary>
		/// Gets or sets the critical.
		/// </summary>
		/// <value>
		/// The critical.
		/// </value>
		public bool? Critical { get; set; }
	}
}
