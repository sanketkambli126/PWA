using System;

namespace PWAFeaturesRnd.Models.Report.JSA
{
    /// <summary>
    /// This is custom contract for JSACrewDetail.
    /// </summary>
    public class JSACrewDetail
    {
        /// <summary>
        /// Gets or sets the job Crew list identifier.
        /// </summary>
        /// <value>
        /// The job Crew list identifier.
        /// </value>
        public string JclId { get; set; }

        /// <summary>
        /// Gets or sets the job identifier.
        /// </summary>
        /// <value>
        /// The job identifier.
        /// </value>
        public string JobId { get; set; }

        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>
        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets the rank identifier.
        /// </summary>
        /// <value>
        /// The rank identifier.
        /// </value>
        public string RankId { get; set; }

        /// <summary>
        /// Gets or sets the department identifier.
        /// </summary>
        /// <value>
        /// The department identifier.
        /// </value>
        public string DeptId { get; set; }

        /// <summary>
        /// Gets or sets the department.
        /// </summary>
        /// <value>
        /// The department.
        /// </value>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the department short code.
        /// </summary>
        /// <value>
        /// The department short code.
        /// </value>
        public string DepartmentShortCode { get; set; }

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public string Rank { get; set; }

        /// <summary>
        /// Gets or sets the rank short code.
        /// </summary>
        /// <value>
        /// The rank short code.
        /// </value>
        public string RankShortCode { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the has meeting attended.
        /// </summary>
        /// <value>
        /// The has meeting attended.
        /// </value>
        public bool? HasMeetingAttended { get; set; }

        /// <summary>
        /// Gets or sets the has work instruction understood.
        /// </summary>
        /// <value>
        /// The has work instruction understood.
        /// </value>
        public bool? HasWorkInstructionUnderstood { get; set; }

        /// <summary>
        /// Gets or sets the has satisfied with precaution.
        /// </summary>
        /// <value>
        /// The has satisfied with precaution.
        /// </value>
        public bool? HasSatisfiedWithPrecaution { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the join date.
        /// </summary>
        /// <value>
        /// The join date.
        /// </value>
        public DateTime? JoinDate { get; set; }

        /// <summary>
        /// Gets or sets the CRW identifier tp.
        /// </summary>
        /// <value>
        /// The CRW identifier tp.
        /// </value>
        public String CRW_ID_TP { get; set; }

        /// <summary>
        /// Gets or sets the rank sequence number.
        /// </summary>
        /// <value>
        /// The rank sequence number.
        /// </value>
        public int RankSequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the other identity no.
        /// </summary>
        /// <value>
        /// The other identity no.
        /// </value>
        public string OtherIdentityNo { get; set; }

        /// <summary>
        /// Gets or sets the name of the other crew.
        /// </summary>
        /// <value>
        /// The name of the other crew.
        /// </value>
        public string OtherCrewName { get; set; }

        /// <summary>
        /// Gets or sets the other position.
        /// </summary>
        /// <value>
        /// The other position.
        /// </value>
        public string OtherPosition { get; set; }

        /// <summary>
        /// Gets or sets the other company.
        /// </summary>
        /// <value>
        /// The other company.
        /// </value>
        public string OtherCompany { get; set; }
    }
}
