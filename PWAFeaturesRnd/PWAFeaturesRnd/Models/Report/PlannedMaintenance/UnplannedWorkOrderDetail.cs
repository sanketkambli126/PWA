using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.PlannedMaintenance
{
    /// <summary>
    /// UnplannedWorkOrderDetail
    /// </summary>
    public class UnplannedWorkOrderDetail
    {
        /// <summary>
        /// Gets or sets the responsible rank short code.
        /// </summary>
        /// <value>
        /// The responsible rank short code.
        /// </value>
        public string ResponsibleRankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the job type short code.
        /// </summary>
        /// <value>
        /// The job type short code.
        /// </value>
        public string JobTypeShortCode { get; set; }

        /// <summary>
        /// Gets or sets the job type description.
        /// </summary>
        /// <value>
        /// The job type description.
        /// </value>
        public string JobTypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the work order history identifier.
        /// </summary>
        /// <value>
        /// The work order history identifier.
        /// </value>
        public string WorkOrderHistoryId { get; set; }

        /// <summary>
        /// Gets or sets the mapped components.
        /// </summary>
        /// <value>
        /// The mapped components.
        /// </value>
        public List<MappedComponentDetail> MappedComponents { get; set; }

        /// <summary>
        /// Gets or sets the associated jobs.
        /// </summary>
        /// <value>
        /// The associated jobs.
        /// </value>
        public List<AssociatedJob> AssociatedJobs { get; set; }

        /// <summary>
        /// Gets or sets the responsible rank description.
        /// </summary>
        /// <value>
        /// The responsible rank description.
        /// </value>
        public string ResponsibleRankDescription { get; set; }

        /// <summary>
        /// Gets or sets the por request identifier.
        /// </summary>
        /// <value>
        /// The por request identifier.
        /// </value>
        public string PorRequestId { get; set; }

        /// <summary>
        /// Gets or sets the recurrence interval identifier.
        /// </summary>
        /// <value>
        /// The recurrence interval identifier.
        /// </value>
        public string RecurrenceIntervalId { get; set; }

        /// <summary>
        /// Gets or sets the recurrence interval.
        /// </summary>
        /// <value>
        /// The recurrence interval.
        /// </value>
        public string RecurrenceInterval { get; set; }

        /// <summary>
        /// Gets or sets the recurrence interval short code.
        /// </summary>
        /// <value>
        /// The recurrence interval short code.
        /// </value>
        public string RecurrenceIntervalShortCode { get; set; }

        /// <summary>
        /// Gets or sets the recurrence value.
        /// </summary>
        /// <value>
        /// The recurrence value.
        /// </value>
        public int? RecurrenceValue { get; set; }

        /// <summary>
        /// Gets or sets the no of work order to create.
        /// </summary>
        /// <value>
        /// The no of work order to create.
        /// </value>
        public int? NoOfWorkOrderToCreate { get; set; }

        /// <summary>
        /// Gets or sets the status short code.
        /// </summary>
        /// <value>
        /// The status short code.
        /// </value>
        public string StatusShortCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has rescheduled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has rescheduled; otherwise, <c>false</c>.
        /// </value>
        public bool HasRescheduled { get; set; }

        /// <summary>
        /// Gets or sets the work order status.
        /// </summary>
        /// <value>
        /// The work order status.
        /// </value>
        public JobStatus WorkOrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the job description.
        /// </summary>
        /// <value>
        /// The job description.
        /// </value>
        public JobDescription JobDescription { get; set; }

        /// <summary>
        /// Gets or sets the work order identifier.
        /// </summary>
        /// <value>
        /// The work order identifier.
        /// </value>
        public string WorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets the work order type identifier.
        /// </summary>
        /// <value>
        /// The work order type identifier.
        /// </value>
        public string WorkOrderTypeId { get; set; }

        /// <summary>
        /// Gets or sets the system area identifier.
        /// </summary>
        /// <value>
        /// The system area identifier.
        /// </value>
        public string SystemAreaId { get; set; }

        /// <summary>
        /// Gets or sets the component identifier.
        /// </summary>
        /// <value>
        /// The component identifier.
        /// </value>
        public string ComponentId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the job type identifier.
        /// </summary>
        /// <value>
        /// The job type identifier.
        /// </value>
        public string JobTypeId { get; set; }

        /// <summary>
        /// Gets or sets the job description identifier.
        /// </summary>
        /// <value>
        /// The job description identifier.
        /// </value>
        public string JobDescriptionId { get; set; }

        /// <summary>
        /// Gets or sets the approver identifier.
        /// </summary>
        /// <value>
        /// The approver identifier.
        /// </value>
        public string ApproverId { get; set; }

        /// <summary>
        /// Gets or sets the responsible rank identifier.
        /// </summary>
        /// <value>
        /// The responsible rank identifier.
        /// </value>
        public string ResponsibleRankId { get; set; }

        /// <summary>
        /// Gets or sets the responsible department identifier.
        /// </summary>
        /// <value>
        /// The responsible department identifier.
        /// </value>
        public string ResponsibleDepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the name of the work order.
        /// </summary>
        /// <value>
        /// The name of the work order.
        /// </value>
        public string WorkOrderName { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is shore staff involved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is shore staff involved; otherwise, <c>false</c>.
        /// </value>
        public bool IsShoreStaffInvolved { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is office approval required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is office approval required; otherwise, <c>false</c>.
        /// </value>
        public bool IsOfficeApprovalRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hod approval required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is hod approval required; otherwise, <c>false</c>.
        /// </value>
        public bool IsHODApprovalRequired { get; set; }

        /// <summary>
        /// Gets or sets the status description.
        /// </summary>
        /// <value>
        /// The status description.
        /// </value>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the done date.
        /// </summary>
        /// <value>
        /// The done date.
        /// </value>
        public DateTime? DoneDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the department.
        /// </summary>
        /// <value>
        /// The name of the department.
        /// </value>
        public string ResponsibleDepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the approver rank description.
        /// </summary>
        /// <value>
        /// The approver rank description.
        /// </value>
        public string ApproverRankDescription { get; set; }
    }
}
