using System;

namespace PWAFeaturesRnd.Models.Report.HazardousOccurrences
{
    /// <summary>
    /// The get crew illness detail
    /// </summary>
    public class CrewIllnessDetail
    {
        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>

        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the imr identifier.
        /// </summary>
        /// <value>
        /// The imr identifier.
        /// </value>

        public string ImrId { get; set; }

        /// <summary>
        /// Gets or sets the ida identifier.
        /// </summary>
        /// <value>
        /// The ida identifier.
        /// </value>

        public string IdaId { get; set; }

        /// <summary>
        /// Gets or sets the medical manager identifier.
        /// </summary>
        /// <value>
        /// The medical manager identifier.
        /// </value>

        public string MedicalManagerId { get; set; }

        /// <summary>
        /// Gets or sets the crew identifier.
        /// </summary>
        /// <value>
        /// The crew identifier.
        /// </value>

        public string CrewId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the crew.
        /// </summary>
        /// <value>
        /// The first name of the crew.
        /// </value>

        public string CrewFirstName { get; set; }

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
        /// Gets or sets the rank description.
        /// </summary>
        /// <value>
        /// The rank description.
        /// </value>

        public string RankDescription { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>

        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [crew not found].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [crew not found]; otherwise, <c>false</c>.
        /// </value>

        public bool CrewNotFound { get; set; }

        /// <summary>
        /// Gets or sets the nationality.
        /// </summary>
        /// <value>
        /// The nationality.
        /// </value>

        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets the passport number.
        /// </summary>
        /// <value>
        /// The passport number.
        /// </value>

        public string PassportNumber { get; set; }

        /// <summary>
        /// Gets or sets the marital status.
        /// </summary>
        /// <value>
        /// The marital status.
        /// </value>

        public string MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>

        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the PCN.
        /// </summary>
        /// <value>
        /// The PCN.
        /// </value>

        public string PCN { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>

        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the book number.
        /// </summary>
        /// <value>
        /// The book number.
        /// </value>

        public string BookNumber { get; set; }

        /// <summary>
        /// Gets or sets the injury type identifier.
        /// </summary>
        /// <value>
        /// The injury type identifier.
        /// </value>

        public string InjuryTypeId { get; set; }

        /// <summary>
        /// Gets or sets the compression board code.
        /// </summary>
        /// <value>
        /// The compression board code.
        /// </value>

        public string CompressionBoardCode { get; set; }

        /// <summary>
        /// Gets or sets the engagement date.
        /// </summary>
        /// <value>
        /// The engagement date.
        /// </value>

        public DateTime? EngagementDate { get; set; }

        /// <summary>
        /// Gets or sets the reported date.
        /// </summary>
        /// <value>
        /// The reported date.
        /// </value>

        public DateTime? ReportedDate { get; set; }

        /// <summary>
        /// Gets or sets the occurence date.
        /// </summary>
        /// <value>
        /// The occurence date.
        /// </value>

        public DateTime? OccurenceDate { get; set; }

        /// <summary>
        /// Gets or sets the classification identifier.
        /// </summary>
        /// <value>
        /// The classification identifier.
        /// </value>

        public string ClassificationId { get; set; }

        /// <summary>
        /// Gets or sets the parent report identifier.
        /// </summary>
        /// <value>
        /// The parent report identifier.
        /// </value>

        public string ParentReportId { get; set; }

        /// <summary>
        /// Gets or sets the CRW identifier tp.
        /// </summary>
        /// <value>
        /// The CRW identifier tp.
        /// </value>

        public string CrwIdTp { get; set; }

        /// <summary>
        /// Gets or sets the idp identifier.
        /// </summary>
        /// <value>
        /// The idp identifier.
        /// </value>

        public string IdpId { get; set; }

        /// <summary>
        /// Gets or sets the set identifier.
        /// </summary>
        /// <value>
        /// The set identifier.
        /// </value>

        public string SetId { get; set; }
    }
}
