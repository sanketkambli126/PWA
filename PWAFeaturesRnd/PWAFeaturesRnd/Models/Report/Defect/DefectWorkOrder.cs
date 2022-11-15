using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Defect
{
    /// <summary>
    /// 
    /// </summary>
    public class DefectWorkOrder
    {
        /// <summary>
        /// Gets or sets the dwo identifier.
        /// </summary>
        /// <value>
        /// The dwo identifier.
        /// </value>
        public string DwoId { get; set; }

        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }

        /// <summary>
        /// Gets or sets the PGR identifier.
        /// </summary>
        /// <value>
        /// The PGR identifier.
        /// </value>
        public string PgrId { get; set; }

        /// <summary>
        /// Gets or sets the PTR identifier.
        /// </summary>
        /// <value>
        /// The PTR identifier.
        /// </value>
        public string PtrId { get; set; }

        /// <summary>
        /// Gets or sets the nty identifier (Vessel system area id).
        /// </summary>
        /// <value>
        /// The nty identifier.
        /// </value>
        public string NtyId { get; set; }

        /// <summary>
        /// Gets or sets the defect system area.
        /// </summary>
        /// <value>
        /// The defect system area.
        /// </value>
        public string DefectSystemArea { get; set; }

        /// <summary>
        /// Gets or sets the VSA identifier (Vessel sub system area id).
        /// </summary>
        /// <value>
        /// The VSA identifier.
        /// </value>
        public string VsaId { get; set; }

        /// <summary>
        /// Gets or sets the defect sub system area.
        /// </summary>
        /// <value>
        /// The defect sub system area.
        /// </value>
        public string DefectSubSystemArea { get; set; }

        /// <summary>
        /// Gets or sets the category identifier.
        /// </summary>
        /// <value>
        /// The category identifier.
        /// </value>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the site type identifier.
        /// </summary>
        /// <value>
        /// The site type identifier.
        /// </value>
        public string SiteTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the site.
        /// </summary>
        /// <value>
        /// The type of the site.
        /// </value>
        public string SiteType { get; set; }

        /// <summary>
        /// Gets or sets the staff type identifier.
        /// </summary>
        /// <value>
        /// The staff type identifier.
        /// </value>
        public string StaffTypeId { get; set; }

        /// <summary>
        /// Gets or sets the type of the staff.
        /// </summary>
        /// <value>
        /// The type of the staff.
        /// </value>
        public string StaffType { get; set; }

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>
        /// The project identifier.
        /// </value>
        public string ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>
        /// The name of the project.
        /// </value>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account code description.
        /// </summary>
        /// <value>
        /// The account code description.
        /// </value>
        public string AccountCodeDescription { get; set; }

        /// <summary>
        /// Gets or sets the priority identifier.
        /// </summary>
        /// <value>
        /// The priority identifier.
        /// </value>
        public string PriorityId { get; set; }

        /// <summary>
        /// Gets or sets the priority.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets the impact identifier.
        /// </summary>
        /// <value>
        /// The impact identifier.
        /// </value>
        public string ImpactId { get; set; }

        /// <summary>
        /// Gets or sets the impact.
        /// </summary>
        /// <value>
        /// The impact.
        /// </value>
        public string Impact { get; set; }

        /// <summary>
        /// Gets or sets the off hire period identifier.
        /// </summary>
        /// <value>
        /// The off hire period identifier.
        /// </value>
        public string OffHirePeriodId { get; set; }

        /// <summary>
        /// Gets or sets the is off hire.
        /// </summary>
        /// <value>
        /// The is off hire.
        /// </value>
        public bool? IsOffHire { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the estimated period.
        /// </summary>
        /// <value>
        /// The estimated period.
        /// </value>
        public int? EstimatedPeriod { get; set; }

        /// <summary>
        /// Gets or sets the RNK identifier.
        /// </summary>
        /// <value>
        /// The RNK identifier.
        /// </value>
        public string RnkId { get; set; }

        /// <summary>
        /// Gets or sets the rank short code.
        /// </summary>
        /// <value>
        /// The rank short code.
        /// </value>
        public string RankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the rank description.
        /// </summary>
        /// <value>
        /// The rank description.
        /// </value>
        public string RankDescription { get; set; }

        /// <summary>
        /// Gets or sets the current identifier.
        /// </summary>
        /// <value>
        /// The current identifier.
        /// </value>
        public string CurId { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public string StatusId { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the defect number.
        /// </summary>
        /// <value>
        /// The defect number.
        /// </value>
        public string DefectNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the defect.
        /// </summary>
        /// <value>
        /// The name of the defect.
        /// </value>
        public string DefectName { get; set; }

        /// <summary>
        /// Gets or sets the proposed start date.
        /// </summary>
        /// <value>
        /// The proposed start date.
        /// </value>
        public DateTime? ProposedStartDate { get; set; }

        /// <summary>
        /// Gets or sets the estimated completion date.
        /// </summary>
        /// <value>
        /// The estimated completion date.
        /// </value>
        public DateTime? EstimatedCompletionDate { get; set; }

        /// <summary>
        /// Gets or sets the last complete date.
        /// </summary>
        /// <value>
        /// The last complete date.
        /// </value>
        public DateTime? LastCompleteDate { get; set; }

        /// <summary>
        /// Gets or sets the found date.
        /// </summary>
        /// <value>
        /// The found date.
        /// </value>
        public DateTime? FoundDate { get; set; }

        /// <summary>
        /// Gets or sets the estimated cost.
        /// </summary>
        /// <value>
        /// The estimated cost.
        /// </value>
        public decimal? EstimatedCost { get; set; }

        /// <summary>
        /// Gets or sets the is critical.
        /// </summary>
        /// <value>
        /// The is critical.
        /// </value>
        public bool? IsCritical { get; set; }

        /// <summary>
        /// Gets or sets the include in damage form.
        /// </summary>
        /// <value>
        /// The include in damage form.
        /// </value>
        public bool? IncludeInDamageForm { get; set; }

        /// <summary>
        /// Gets or sets the damage form number.
        /// </summary>
        /// <value>
        /// The damage form number.
        /// </value>
        public string DamageFormNumber { get; set; }

        /// <summary>
        /// Gets or sets the guarantee claim required.
        /// </summary>
        /// <value>
        /// The guarantee claim required.
        /// </value>
        public bool GuaranteeClaimRequired { get; set; }

        /// <summary>
        /// Gets or sets the guarantee claim number.
        /// </summary>
        /// <value>
        /// The guarantee claim number.
        /// </value>
        public string GuaranteeClaimNumber { get; set; }

        /// <summary>
        /// Gets or sets the defect description.
        /// </summary>
        /// <value>
        /// The defect description.
        /// </value>
        public string DefectDescription { get; set; }

        /// <summary>
        /// Gets or sets the repair specification.
        /// </summary>
        /// <value>
        /// The repair specification.
        /// </value>
        public string RepairSpecification { get; set; }

        /// <summary>
        /// Gets or sets the approval required.
        /// </summary>
        /// <value>
        /// The approval required.
        /// </value>
        public bool? ApprovalRequired { get; set; }

        /// <summary>
        /// Gets or sets the yard guarantee claim number.
        /// </summary>
        /// <value>
        /// The yard guarantee claim number.
        /// </value>
        public string YardGuaranteeClaimNumber { get; set; }

        /// <summary>
        /// Gets or sets the original due date.
        /// </summary>
        /// <value>
        /// The original due date.
        /// </value>
        public DateTime? OriginalDueDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is regulatory authority.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is regulatory authority; otherwise, <c>false</c>.
        /// </value>
        public bool IsRegulatoryAuthority { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [dispensation in place].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [dispensation in place]; otherwise, <c>false</c>.
        /// </value>
        public bool DispensationInPlace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is js arequired.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is js arequired; otherwise, <c>false</c>.
        /// </value>
        public bool IsJSARequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is moc required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is moc required; otherwise, <c>false</c>.
        /// </value>
        public bool IsMOCRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is gas free.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is gas free; otherwise, <c>false</c>.
        /// </value>
        public bool IsGasFree { get; set; }

        /// <summary>
        /// Gets or sets the accessory work.
        /// </summary>
        /// <value>
        /// The accessory work.
        /// </value>
        public List<DefectAccessoryWork> AccessoryWork { get; set; }

        /// <summary>
        /// Gets or sets the specification.
        /// </summary>
        /// <value>
        /// The specification.
        /// </value>
        public List<DefectSpecification> Specification { get; set; }

        /// <summary>
        /// Gets or sets the additional job.
        /// </summary>
        /// <value>
        /// The additional job.
        /// </value>
        public List<DefectAdditionalJob> AdditionalJob { get; set; }

        /// <summary>
        /// Gets or sets the requisition.
        /// </summary>
        /// <value>
        /// The requisition.
        /// </value>
        public List<DefectRequisition> Requisition { get; set; }

        /// <summary>
        /// Gets or sets the action taken.
        /// </summary>
        /// <value>
        /// The action taken.
        /// </value>
        public List<DefectActionTaken> ActionTaken { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public string DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the work order identifier.
        /// </summary>
        /// <value>
        /// The work order identifier.
        /// </value>
        public string WorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the inspection finding identifier.
        /// </summary>
        /// <value>
        /// The inspection finding identifier.
        /// </value>
        public string InspectionFindingId { get; set; }

        /// <summary>
        /// Gets or sets the hazzoc identifier.
        /// </summary>
        /// <value>
        /// The hazzoc identifier.
        /// </value>
        public string HazzocId { get; set; }

        /// <summary>
        /// Gets or sets the off hire period.
        /// </summary>
        /// <value>
        /// The off hire period.
        /// </value>
        public string OffHirePeriod { get; set; }

        /// <summary>
        /// Gets or sets the defect status description.
        /// </summary>
        /// <value>
        /// The defect status description.
        /// </value>
        public string DefectStatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the off hire mins.
        /// </summary>
        /// <value>
        /// The off hire mins.
        /// </value>
        public int? OffHireMins { get; set; }

        /// <summary>
        /// Gets or sets the off hire hours.
        /// </summary>
        /// <value>
        /// The off hire hours.
        /// </value>
        public int? OffHireHours { get; set; }
    }
}
