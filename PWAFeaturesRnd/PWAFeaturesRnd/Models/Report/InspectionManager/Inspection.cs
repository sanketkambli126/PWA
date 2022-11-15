using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
    /// <summary>
    /// Inspection contract class
    /// </summary>
    public class Inspection
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the updated by user.
        /// </summary>
        /// <value>
        /// The name of the updated by user.
        /// </value>
        public string UpdatedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the TPL identifier.
        /// </summary>
        /// <value>
        /// The TPL identifier.
        /// </value>
        public string TplId { get; set; }

        /// <summary>
        /// Converts to portname.
        /// </summary>
        /// <value>
        /// The name of to port.
        /// </value>
        public string ToPortName { get; set; }

        /// <summary>
        /// Converts to portid.
        /// </summary>
        /// <value>
        /// To port identifier.
        /// </value>
        public string ToPortId { get; set; }

        /// <summary>
        /// Gets or sets the rit identifier risk rating.
        /// </summary>
        /// <value>
        /// The rit identifier risk rating.
        /// </value>
        public string RitIdRiskRating { get; set; }

        /// <summary>
        /// Gets or sets the risk rating action.
        /// </summary>
        /// <value>
        /// The risk rating action.
        /// </value>
        public string RiskRatingAction { get; set; }

        /// <summary>
        /// Gets or sets the risk rating.
        /// </summary>
        /// <value>
        /// The risk rating.
        /// </value>
        public string RiskRating { get; set; }

        /// <summary>
        /// Gets or sets the report status short code.
        /// </summary>
        /// <value>
        /// The report status short code.
        /// </value>
        public string ReportStatusShortCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the report status.
        /// </summary>
        /// <value>
        /// The name of the report status.
        /// </value>
        public string ReportStatusName { get; set; }

        /// <summary>
        /// Gets or sets the report required.
        /// </summary>
        /// <value>
        /// The report required.
        /// </value>
        public byte ReportRequired { get; set; }

        /// <summary>
        /// Gets or sets the report issued.
        /// </summary>
        /// <value>
        /// The report issued.
        /// </value>
        public DateTime? ReportIssued { get; set; }

        /// <summary>
        /// Gets or sets the refusalof access issued.
        /// </summary>
        /// <value>
        /// The refusalof access issued.
        /// </value>
        public byte? RefusalofAccessIssued { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public float? Rating { get; set; }

        /// <summary>
        /// Gets or sets the vessel detained.
        /// </summary>
        /// <value>
        /// The vessel detained.
        /// </value>
        public byte? VesselDetained { get; set; }

        /// <summary>
        /// Gets or sets the name of the port.
        /// </summary>
        /// <value>
        /// The name of the port.
        /// </value>
        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the vessel rating.
        /// </summary>
        /// <value>
        /// The vessel rating.
        /// </value>
        public int? VesselRating { get; set; }

        /// <summary>
        /// Gets or sets the office reviewer detail.
        /// </summary>
        /// <value>
        /// The office reviewer detail.
        /// </value>
        public List<VesselInspectionOfficeReviewerDetail> OfficeReviewerDetail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is inspected by third party.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is inspected by third party; otherwise, <c>false</c>.
        /// </value>
        public bool IsInspectedByThirdParty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is inspected by office.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is inspected by office; otherwise, <c>false</c>.
        /// </value>
        public bool IsInspectedByOffice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is inspected by ship staff.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is inspected by ship staff; otherwise, <c>false</c>.
        /// </value>
        public bool IsInspectedByShipStaff { get; set; }

        /// <summary>
        /// Gets or sets the schedule details.
        /// </summary>
        /// <value>
        /// The schedule details.
        /// </value>
        public List<InspectionScheduleDetail> ScheduleDetails { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public string StatusId { get; set; }

        /// <summary>
        /// Gets or sets the where.
        /// </summary>
        /// <value>
        /// The where.
        /// </value>
        public string Where { get; set; }

        /// <summary>
        /// Gets or sets the VST report editable by.
        /// </summary>
        /// <value>
        /// The VST report editable by.
        /// </value>
        public string VstReportEditableBy { get; set; }

        /// <summary>
        /// Gets or sets the VST originator.
        /// </summary>
        /// <value>
        /// The VST originator.
        /// </value>
        public string VstOriginator { get; set; }

        /// <summary>
        /// Gets or sets the VRP type3.
        /// </summary>
        /// <value>
        /// The VRP type3.
        /// </value>
        public string VrpType3 { get; set; }

        /// <summary>
        /// Gets or sets the VRP type2.
        /// </summary>
        /// <value>
        /// The VRP type2.
        /// </value>
        public int? VrpType2 { get; set; }

        /// <summary>
        /// Gets or sets the VRP other3.
        /// </summary>
        /// <value>
        /// The VRP other3.
        /// </value>
        public string VrpOther3 { get; set; }

        /// <summary>
        /// Gets or sets the VRP other2.
        /// </summary>
        /// <value>
        /// The VRP other2.
        /// </value>
        public int? VrpOther2 { get; set; }

        /// <summary>
        /// Gets or sets the VRP other1.
        /// </summary>
        /// <value>
        /// The VRP other1.
        /// </value>
        public int? VrpOther1 { get; set; }

        /// <summary>
        /// Gets or sets the VRP abbr.
        /// </summary>
        /// <value>
        /// The VRP abbr.
        /// </value>
        public string VrpAbbr { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is complete report.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is complete report; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleteReport { get; set; }

        /// <summary>
        /// Gets or sets the port identifier.
        /// </summary>
        /// <value>
        /// The port identifier.
        /// </value>
        public string PortId { get; set; }

        /// <summary>
        /// Gets or sets the office approval required.
        /// </summary>
        /// <value>
        /// The office approval required.
        /// </value>
        public bool? OfficeApprovalRequired { get; set; }

        /// <summary>
        /// Gets or sets the executive summary.
        /// </summary>
        /// <value>
        /// The executive summary.
        /// </value>
        public string ExecutiveSummary { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        /// <value>
        /// The name of the department.
        /// </value>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public string DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the days detained.
        /// </summary>
        /// <value>
        /// The days detained.
        /// </value>
        public float? DaysDetained { get; set; }

        /// <summary>
        /// Gets or sets the date closed.
        /// </summary>
        /// <value>
        /// The date closed.
        /// </value>
        public DateTime? DateClosed { get; set; }

        /// <summary>
        /// Gets or sets the country identifier.
        /// </summary>
        /// <value>
        /// The country identifier.
        /// </value>
        public string CountryId { get; set; }

        /// <summary>
        /// Gets or sets the concentrated inspection campaign.
        /// </summary>
        /// <value>
        /// The concentrated inspection campaign.
        /// </value>
        public bool? ConcentratedInspectionCampaign { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the cargo type identifier.
        /// </summary>
        /// <value>
        /// The cargo type identifier.
        /// </value>
        public string CargoTypeId { get; set; }

        /// <summary>
        /// Gets or sets the cargo type description.
        /// </summary>
        /// <value>
        /// The cargo type description.
        /// </value>
        public string CargoTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        /// <value>
        /// The application identifier.
        /// </value>
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the type of the activity.
        /// </summary>
        /// <value>
        /// The type of the activity.
        /// </value>
        public string ActivityType { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the pla identifier.
        /// </summary>
        /// <value>
        /// The pla identifier.
        /// </value>
        public string PlaId { get; set; }

        /// <summary>
        /// Gets or sets the ial identifier report status.
        /// </summary>
        /// <value>
        /// The ial identifier report status.
        /// </value>
        public string IalIdReportStatus { get; set; }

        /// <summary>
        /// Gets or sets the inspection identifier.
        /// </summary>
        /// <value>
        /// The inspection identifier.
        /// </value>
        public string InspectionId { get; set; }

        /// <summary>
        /// Gets or sets the next visit.
        /// </summary>
        /// <value>
        /// The next visit.
        /// </value>
        public DateTime? NextVisit { get; set; }

        /// <summary>
        /// Gets or sets the type of the is internal.
        /// </summary>
        /// <value>
        /// The type of the is internal.
        /// </value>
        public bool? IsInternalType { get; set; }

        /// <summary>
        /// Gets or sets the is deleted.
        /// </summary>
        /// <value>
        /// The is deleted.
        /// </value>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the inspector title.
        /// </summary>
        /// <value>
        /// The inspector title.
        /// </value>
        public string InspectorTitle { get; set; }

        /// <summary>
        /// Gets or sets the inspector surname.
        /// </summary>
        /// <value>
        /// The inspector surname.
        /// </value>
        public string InspectorSurname { get; set; }

        /// <summary>
        /// Gets or sets the name of the inspector.
        /// </summary>
        /// <value>
        /// The name of the inspector.
        /// </value>
        public string InspectorName { get; set; }

        /// <summary>
        /// Gets or sets the inspector forename.
        /// </summary>
        /// <value>
        /// The inspector forename.
        /// </value>
        public string InspectorForename { get; set; }

        /// <summary>
        /// Gets or sets the inspection type require document.
        /// </summary>
        /// <value>
        /// The inspection type require document.
        /// </value>
        public bool? InspectionTypeRequireDocument { get; set; }

        /// <summary>
        /// Gets or sets the inspection type required next due date.
        /// </summary>
        /// <value>
        /// The inspection type required next due date.
        /// </value>
        public bool? InspectionTypeRequiredNextDueDate { get; set; }

        /// <summary>
        /// Gets or sets the inspection type next due date editable.
        /// </summary>
        /// <value>
        /// The inspection type next due date editable.
        /// </value>
        public bool? InspectionTypeNextDueDateEditable { get; set; }

        /// <summary>
        /// Gets or sets the inspection type identifier.
        /// </summary>
        /// <value>
        /// The inspection type identifier.
        /// </value>
        public string InspectionTypeId { get; set; }

        /// <summary>
        /// Gets or sets the inspection type default interval.
        /// </summary>
        /// <value>
        /// The inspection type default interval.
        /// </value>
        public int? InspectionTypeDefaultInterval { get; set; }

        /// <summary>
        /// Gets or sets the type of the inspection.
        /// </summary>
        /// <value>
        /// The type of the inspection.
        /// </value>
        public string InspectionType { get; set; }

        /// <summary>
        /// Gets or sets the inspection template version no.
        /// </summary>
        /// <value>
        /// The inspection template version no.
        /// </value>
        public decimal? InspectionTemplateVersionNo { get; set; }

        /// <summary>
        /// Gets or sets the name of the inspection template.
        /// </summary>
        /// <value>
        /// The name of the inspection template.
        /// </value>
        public string InspectionTemplateName { get; set; }

        /// <summary>
        /// Gets or sets the inspection category.
        /// </summary>
        /// <value>
        /// The inspection category.
        /// </value>
        public string InspectionCategory { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is execution summary required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is execution summary required; otherwise, <c>false</c>.
        /// </value>
        public bool IsExecutionSummaryRequired { get; set; }

        /// <summary>
        /// Gets or sets the mapped questions.
        /// </summary>
        /// <value>
        /// The mapped questions.
        /// </value>
        public List<MarineQuestionAnswerDetailResponse> MappedQuestions { get; set; }
    }
}
