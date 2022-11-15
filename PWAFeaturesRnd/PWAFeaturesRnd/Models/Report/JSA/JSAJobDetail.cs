using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    public class JSAJobDetail
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the job type identifier.
        /// </summary>
        /// <value>
        /// The job type identifier.
        /// </value>
        public string JobTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the job type.
        /// </summary>
        /// <value>
        /// The name of the job type.
        /// </value>
        public string JobTypeName { get; set; }

        /// <summary>
        /// Gets or sets the name of the job.
        /// </summary>
        /// <value>
        /// The name of the job.
        /// </value>
        public string JobName { get; set; }

        /// <summary>
        /// Gets or sets the system area identifier.
        /// </summary>
        /// <value>
        /// The system area identifier.
        /// </value>
        public string SystemAreaId { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>
        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets the name of the crew fore.
        /// </summary>
        /// <value>
        /// The name of the crew fore.
        /// </value>
        public string CrewForeName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the crew.
        /// </summary>
        /// <value>
        /// The last name of the crew.
        /// </value>
        public string CrewLastName { get; set; }

        /// <summary>
        /// Gets or sets the rank identifier.
        /// </summary>
        /// <value>
        /// The rank identifier.
        /// </value>
        public string RankId { get; set; }

        /// <summary>
        /// Gets or sets the meeting date.
        /// </summary>
        /// <value>
        /// The meeting date.
        /// </value>
        public DateTime? MeetingDate { get; set; }

        /// <summary>
        /// Gets or sets the start date time.
        /// </summary>
        /// <value>
        /// The start date time.
        /// </value>
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// Gets or sets the end date time.
        /// </summary>
        /// <value>
        /// The end date time.
        /// </value>
        public DateTime? EndDateTime { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [job active].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [job active]; otherwise, <c>false</c>.
        /// </value>
        public bool JobActive { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the is ra adquately defined.
        /// </summary>
        /// <value>
        /// The is ra adquately defined.
        /// </value>
        public bool? IsRaAdquatelyDefined { get; set; }

        /// <summary>
        /// Gets or sets the is critical operation.
        /// </summary>
        /// <value>
        /// The is critical operation.
        /// </value>
        public bool? IsCriticalOperation { get; set; }

        /// <summary>
        /// Gets or sets the office comments.
        /// </summary>
        /// <value>
        /// The office comments.
        /// </value>
        public string OfficeComments { get; set; }

        /// <summary>
        /// Gets or sets the CRW identifier tp.
        /// </summary>
        /// <value>
        /// The CRW identifier tp.
        /// </value>
        public string CrwIdTp { get; set; }

        /// <summary>
        /// Gets or sets the work order identifier.
        /// </summary>
        /// <value>
        /// The work order identifier.
        /// </value>
        public string WorkOrderId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is work authority responsible person.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is work authority responsible person; otherwise, <c>false</c>.
        /// </value>
        public bool IsWorkAuthorityResponsiblePerson { get; set; }

        /// <summary>
        /// Gets or sets the work authority crew identifier.
        /// </summary>
        /// <value>
        /// The work authority crew identifier.
        /// </value>
        public string WorkAuthorityCrewId { get; set; }

        /// <summary>
        /// Gets or sets the work authority crew temporary identifier.
        /// </summary>
        /// <value>
        /// The work authority crew temporary identifier.
        /// </value>
        public string WorkAuthorityCrewTempId { get; set; }

        /// <summary>
        /// Gets or sets the work authority rank identifier.
        /// </summary>
        /// <value>
        /// The work authority rank identifier.
        /// </value>
        public string WorkAuthorityRankId { get; set; }

        /// <summary>
        /// Gets or sets the name of the work authority crew fore.
        /// </summary>
        /// <value>
        /// The name of the work authority crew fore.
        /// </value>
        public string WorkAuthorityCrewForeName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the work authority crew.
        /// </summary>
        /// <value>
        /// The last name of the work authority crew.
        /// </value>
        public string WorkAuthorityCrewLastName { get; set; }

        /// <summary>
        /// Gets or sets the control measures acknowledged by user identifier.
        /// </summary>
        /// <value>
        /// The control measures acknowledged by user identifier.
        /// </value>
        public string ControlMeasuresAcknowledgedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the control measures acknowledged by user.
        /// </summary>
        /// <value>
        /// The name of the control measures acknowledged by user.
        /// </value>
        public string ControlMeasuresAcknowledgedByUserName { get; set; }

        /// <summary>
        /// Gets or sets the control measures acknowledged by user role identifier.
        /// </summary>
        /// <value>
        /// The control measures acknowledged by user role identifier.
        /// </value>
        public string ControlMeasuresAcknowledgedByUserRoleId { get; set; }

        /// <summary>
        /// Gets or sets the control measures acknowledged by user role.
        /// </summary>
        /// <value>
        /// The control measures acknowledged by user role.
        /// </value>
        public string ControlMeasuresAcknowledgedByUserRole { get; set; }

        /// <summary>
        /// Gets or sets the control measures acknowledged date.
        /// </summary>
        /// <value>
        /// The control measures acknowledged date.
        /// </value>
        public DateTime? ControlMeasuresAcknowledgedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is control measures acknowledged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is control measures acknowledged; otherwise, <c>false</c>.
        /// </value>
        public bool IsControlMeasuresAcknowledged { get; set; }
                
    }
}
