using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Defect
{
	/// <summary>
	/// Defect Report Work Order Summary
	/// </summary>
	public class DefectReportWorkOrderSummary
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
		/// Gets or sets a value indicating whether this instance is regulatory authority.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is regulatory authority; otherwise, <c>false</c>.
		/// </value>
		public bool IsRegulatoryAuthority { get; set; }

		/// <summary>
		/// Gets or sets the off hire period.
		/// </summary>
		/// <value>
		/// The off hire period.
		/// </value>
		public string OffHirePeriod { get; set; }

		/// <summary>
		/// Gets or sets the impact.
		/// </summary>
		/// <value>
		/// The impact.
		/// </value>
		public string Impact { get; set; }

		/// <summary>
		/// Gets or sets the dwo identifier.
		/// </summary>
		/// <value>
		/// The dwo identifier.
		/// </value>
		public string DwoId { get; set; }

		/// <summary>
		/// Gets or sets the actual time.
		/// </summary>
		/// <value>
		/// The actual time.
		/// </value>
		public int? ActualTime { get; set; }

		/// <summary>
		/// Gets or sets the off hire reason.
		/// </summary>
		/// <value>
		/// The off hire reason.
		/// </value>
		public string OffHireReason { get; set; }

		/// <summary>
		/// Gets or sets the is off hire.
		/// </summary>
		/// <value>
		/// The is off hire.
		/// </value>
		public bool? IsOffHire { get; set; }

		/// <summary>
		/// Gets or sets the action taken.
		/// </summary>
		/// <value>
		/// The action taken.
		/// </value>
		public List<DefectActionTaken> ActionTaken { get; set; }

		/// <summary>
		/// Gets or sets the additional jobs.
		/// </summary>
		/// <value>
		/// The additional jobs.
		/// </value>
		public List<RelatedJobsDetailResponse> AdditionalJobs { get; set; }

		/// <summary>
		/// Gets or sets the parts used.
		/// </summary>
		/// <value>
		/// The parts used.
		/// </value>
		public List<DefectReportWorkOrderPartsUsed> PartsUsed { get; set; }

		/// <summary>
		/// Gets or sets the hse assessment.
		/// </summary>
		/// <value>
		/// The hse assessment.
		/// </value>
		public List<DefectReportWorkOrderHSE> HSEAssessment { get; set; }

		/// <summary>
		/// Gets or sets the staff rank.
		/// </summary>
		/// <value>
		/// The staff rank.
		/// </value>
		public List<DefectReportWorkOrderRank> StaffRank { get; set; }

		/// <summary>
		/// Gets or sets the symptoms.
		/// </summary>
		/// <value>
		/// The symptoms.
		/// </value>
		public List<DefectReportWorkOrderSymptom> Symptoms { get; set; }

		/// <summary>
		/// Gets or sets the reported date.
		/// </summary>
		/// <value>
		/// The reported date.
		/// </value>
		public DateTime? ReportedDate { get; set; }

		/// <summary>
		/// Gets or sets the due date.
		/// </summary>
		/// <value>
		/// The due date.
		/// </value>
		public DateTime? DueDate { get; set; }

		/// <summary>
		/// Gets or sets the work done date.
		/// </summary>
		/// <value>
		/// The work done date.
		/// </value>
		public DateTime? WorkDoneDate { get; set; }

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
		/// Gets or sets the guarantee claim.
		/// </summary>
		/// <value>
		/// The guarantee claim.
		/// </value>
		public bool? GuaranteeClaim { get; set; }

		/// <summary>
		/// Gets or sets the jsa detail.
		/// </summary>
		/// <value>
		/// The jsa detail.
		/// </value>
		public JobSafetyAnalysisLookup JSADetail { get; set; }

		/// <summary>
		/// Gets or sets the include in damage form.
		/// </summary>
		/// <value>
		/// The include in damage form.
		/// </value>
		public bool? IncludeInDamageForm { get; set; }

		/// <summary>
		/// Gets or sets the project description.
		/// </summary>
		/// <value>
		/// The project description.
		/// </value>
		public string ProjectDescription { get; set; }

		/// <summary>
		/// Gets or sets the account code.
		/// </summary>
		/// <value>
		/// The account code.
		/// </value>
		public string AccountCode { get; set; }

		/// <summary>
		/// Gets or sets the account code description.
		/// </summary>
		/// <value>
		/// The account code description.
		/// </value>
		public string AccountCodeDescription { get; set; }

		/// <summary>
		/// Gets or sets the cost.
		/// </summary>
		/// <value>
		/// The cost.
		/// </value>
		public decimal? Cost { get; set; }

		/// <summary>
		/// Gets or sets the currency.
		/// </summary>
		/// <value>
		/// The currency.
		/// </value>
		public string Currency { get; set; }

		/// <summary>
		/// Gets or sets the remark.
		/// </summary>
		/// <value>
		/// The remark.
		/// </value>
		public string Remark { get; set; }

		/// <summary>
		/// Gets or sets the project code.
		/// </summary>
		/// <value>
		/// The project code.
		/// </value>
		public string ProjectCode { get; set; }

		/// <summary>
		/// Gets or sets the moc detail.
		/// </summary>
		/// <value>
		/// The moc detail.
		/// </value>
		public ManagementOfChangeLookup MOCDetail { get; set; }
	}
}
