using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWAFeaturesRnd.Models.Report.Crew
{
    /// <summary>
    /// Contract for Crew Service History Details
    /// </summary>
    public class CrewServiceHistory
    {
        /// <summary>
        /// Gets or sets the crew service identifier.
        /// </summary>
        /// <value>
        /// The crew service identifier.
        /// </value>
        public string CrewServiceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is company record.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is company record; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompanyRecord { get; set; }

        /// <summary>
        /// Gets or sets the status short code.
        /// </summary>
        /// <value>
        /// The status short code.
        /// </value>

        public string StatusShortCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [rank short code].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [rank short code]; otherwise, <c>false</c>.
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
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>
        /// The name of the company.
        /// </value>

        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>

        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the vessel flag.
        /// </summary>
        /// <value>
        /// The vessel flag.
        /// </value>

        public string VesselFlag { get; set; }

        /// <summary>
        /// Gets or sets the vessel type short code.
        /// </summary>
        /// <value>
        /// The vessel type short code.
        /// </value>

        public string VesselTypeShortCode { get; set; }

        /// <summary>
        /// Gets or sets the vessel dead weight tonnage.
        /// </summary>
        /// <value>
        /// The vessel dead weight tonnage.
        /// </value>

        public int? VesselDeadWeightTonnage { get; set; }

        /// <summary>
        /// Gets or sets the service start date.
        /// </summary>
        /// <value>
        /// The service start date.
        /// </value>

        public DateTime? ServiceStartDate { get; set; }

        /// <summary>
        /// Gets or sets the service end date.
        /// </summary>
        /// <value>
        /// The service end date.
        /// </value>

        public DateTime? ServiceEndDate { get; set; }

        /// <summary>
        /// Gets or sets the crew sign off reason.
        /// </summary>
        /// <value>
        /// The crew sign off reason.
        /// </value>

        public string CrewSignOffReasonShortCode { get; set; }

        /// <summary>
        /// Gets or sets the type of the vessel management office.
        /// </summary>
        /// <value>
        /// The type of the vessel management office.
        /// </value>

        public string VesselManagementOfficeType { get; set; }

        /// <summary>
        /// Gets or sets the international maritime organization number.
        /// </summary>
        /// <value>
        /// The international maritime organization number.
        /// </value>

        public string InternationalMaritimeOrganizationNumber { get; set; }

        /// <summary>
        /// Gets or sets the no of attachments.
        /// </summary>
        /// <value>
        /// The no of attachments.
        /// </value>

        public int? NoOfAttachments { get; set; }

        /// <summary>
        /// Gets or sets the service active status identifier.
        /// </summary>
        /// <value>
        /// The service active status identifier.
        /// </value>

        public int ServiceActiveStatusId { get; set; }

        /// <summary>
        /// Gets or sets the onboard notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>

        public string OnboardNotes { get; set; }

        /// <summary>
        /// Gets or sets the planning notes.
        /// </summary>
        /// <value>
        /// The planning notes.
        /// </value>

        public string PlanningNotes { get; set; }

        /// <summary>
        /// Gets or sets the planning status identifier.
        /// </summary>
        /// <value>
        /// The planning status identifier.
        /// </value>

        public string PlanningStatusId { get; set; }

        /// <summary>
        /// Gets or sets the technical office.
        /// </summary>
        /// <value>
        /// The technical office.
        /// </value>

        public string TechnicalOfficeName { get; set; }

        /// <summary>
        /// Gets or sets the rank identifier.
        /// </summary>
        /// <value>
        /// The rank identifier.
        /// </value>

        public string RankId { get; set; }

        /// <summary>
        /// Gets or sets the STS identifier.
        /// </summary>
        /// <value>
        /// The STS identifier.
        /// </value>

        public string StsId { get; set; }

        /// <summary>
        /// Gets or sets the service sea days.
        /// </summary>
        /// <value>
        /// The service sea days.
        /// </value>

        public int? ServiceSeaDays { get; set; }

        /// <summary>
        /// Gets or sets the coordinating office.
        /// </summary>
        /// <value>
        /// The coordinating office.
        /// </value>

        public string CoordinatingOffice { get; set; }

        /// <summary>
        /// Gets or sets the managing office.
        /// </summary>
        /// <value>
        /// The managing office.
        /// </value>

        public string ManagingOffice { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>

        public string VesselId { get; set; }

        /// <summary>
        /// Gets or sets the vessel engine specifications.
        /// </summary>
        /// <value>
        /// The vessel engine specifications.
        /// </value>

        public VesselEngineSpecification VesselEngineSpecifications { get; set; }

        /// <summary>
        /// Gets or sets the extension reason.
        /// </summary>
        /// <value>
        /// The extension reason.
        /// </value>

        public string ExtensionReason { get; set; }

        /// <summary>
        /// Gets or sets the contract count.
        /// </summary>
        /// <value>
        /// The contract count.
        /// </value>

        public int? ContractCount { get; set; }

        /// <summary>
        /// Gets or sets the contract identifier.
        /// </summary>
        /// <value>
        /// The contract identifier.
        /// </value>

        public string ContractId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is contract exists in policy period trans header.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is contract exists in policy period trans header; otherwise, <c>false</c>.
        /// </value>

        public bool IsContractExistsInPolicyPeriodTransHeader { get; set; }

        /// <summary>
        /// Gets or sets the set transit identifier.
        /// </summary>
        /// <value>
        /// The set transit identifier.
        /// </value>

        public string SetTransitId { get; set; }

        /// <summary>
        /// Gets or sets the debriefing count.
        /// </summary>
        /// <value>
        /// The debriefing count.
        /// </value>

        public int? DebriefingCount { get; set; }

        /// <summary>
        /// Gets or sets the type of the CCC.
        /// </summary>
        /// <value>
        /// The type of the CCC.
        /// </value>

        public int? CCCType { get; set; }

        /// <summary>
        /// Gets or sets the CCCFK match.
        /// </summary>
        /// <value>
        /// The CCCFK match.
        /// </value>

        public string CCCFKMatch { get; set; }

        /// <summary>
        /// Gets or sets the color of the CCC background.
        /// </summary>
        /// <value>
        /// The color of the CCC background.
        /// </value>

        public string CCCBackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the CCC description.
        /// </summary>
        /// <value>
        /// The CCC description.
        /// </value>

        public string CCCDescription { get; set; }

        /// <summary>
        /// Gets or sets the color of the CCC foreground.
        /// </summary>
        /// <value>
        /// The color of the CCC foreground.
        /// </value>

        public string CCCForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the CRW identifier.
        /// </summary>
        /// <value>
        /// The CRW identifier.
        /// </value>

        public string CRWId { get; set; }

        /// <summary>
        /// Gets or sets the CDB identifier.
        /// </summary>
        /// <value>
        /// The CDB identifier.
        /// </value>

        public string CDBId { get; set; }
    }
}
