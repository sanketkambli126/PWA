using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.Crew
{
	/// <summary>
	/// OBCrewListDetail custom contract
	/// </summary>
	public class OBCrewListDetail
	{
		/// <summary>
		/// Gets or sets the SET identifier.
		/// </summary>
		/// <value>
		/// The SET identifier.
		/// </value>

		public string SetId { get; set; }

		/// <summary>
		/// Gets or sets the rank identifier.
		/// </summary>
		/// <value>
		/// The rank identifier.
		/// </value>

		public string RankId { get; set; }

		/// <summary>
		/// Gets or sets the rank.
		/// </summary>
		/// <value>
		/// The rank.
		/// </value>

		public string Rank { get; set; }

		/// <summary>
		/// Gets or sets the rank description.
		/// </summary>
		/// <value>
		/// The rank description.
		/// </value>

		public string RankDescription { get; set; }

		/// <summary>
		/// Gets or sets the crew identifier.
		/// </summary>
		/// <value>
		/// The crew identifier.
		/// </value>

		public string CrewId { get; set; }

		/// <summary>
		/// Gets or sets the safe manning.
		/// </summary>
		/// <value>
		/// The safe manning.
		/// </value>

		public bool SafeManning { get; set; }

		/// <summary>
		/// Gets or sets the department name.
		/// </summary>
		/// <value>
		/// The department name.
		/// </value>

		public string DepartmentShortName { get; set; }

		/// <summary>
		/// Gets or sets the name of the department.
		/// </summary>
		/// <value>
		/// The name of the department.
		/// </value>

		public string DepartmentName { get; set; }

		/// <summary>
		/// Gets or sets the nationality.
		/// </summary>
		/// <value>
		/// The nationality.
		/// </value>

		public string Nationality { get; set; }

		/// <summary>
		/// Gets or sets the crew first name.
		/// </summary>
		/// <value>
		/// The crew first name.
		/// </value>

		public string CrewFirstName { get; set; }

		/// <summary>
		/// Gets or sets the crew middle name.
		/// </summary>
		/// <value>
		/// The crew middle name.
		/// </value>

		public string CrewMiddleName { get; set; }

		/// <summary>
		/// Gets or sets the crew last name.
		/// </summary>
		/// <value>
		/// The crew last name.
		/// </value>

		public string CrewLastName { get; set; }

		/// <summary>
		/// Gets or sets the name of the cabin.
		/// </summary>
		/// <value>
		/// The name of the cabin.
		/// </value>

		public string CabinName { get; set; }

		/// <summary>
		/// Gets or sets the crew sign on.
		/// </summary>
		/// <value>
		/// The crew sign on.
		/// </value>

		public DateTime? CrewSignOn { get; set; }

		/// <summary>
		/// Gets or sets the crew due relief.
		/// </summary>
		/// <value>
		/// The crew due relief.
		/// </value>

		public DateTime? CrewDueRelief { get; set; }

		/// <summary>
		/// Gets or sets the crew status.
		/// </summary>
		/// <value>
		/// The crew status.
		/// </value>

		public CrewStatus? CrewStatus { get; set; }

		/// <summary>
		/// Gets or sets the reliever identifier.
		/// </summary>
		/// <value>
		/// The reliever identifier.
		/// </value>

		public string RelieverId { get; set; }

		/// <summary>
		/// Gets or sets the reliever first name.
		/// </summary>
		/// <value>
		/// The reliever first name.
		/// </value>

		public string RelieverFirstName { get; set; }

		/// <summary>
		/// Gets or sets the reliever middle name.
		/// </summary>
		/// <value>
		/// The reliever middle name.
		/// </value>

		public string RelieverMiddleName { get; set; }

		/// <summary>
		/// Gets or sets the reliever last name.
		/// </summary>
		/// <value>
		/// The reliever last name.
		/// </value>

		public string RelieverLastName { get; set; }

		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>

		public string Notes { get; set; }

		/// <summary>
		/// Gets or sets the extension.
		/// </summary>
		/// <value>
		/// The extension.
		/// </value>

		public int? Extension { get; set; }

		/// <summary>
		/// Gets or sets the extension unit.
		/// </summary>
		/// <value>
		/// The extension unit.
		/// </value>

		public string ExtensionUnit { get; set; }

		/// <summary>
		/// Gets or sets the over lap days.
		/// </summary>
		/// <value>
		/// The over lap days.
		/// </value>

		public int? OverLapDays { get; set; }

		/// <summary>
		/// Gets or sets the length of the contract.
		/// </summary>
		/// <value>
		/// The length of the contract.
		/// </value>

		public int? ContractLength { get; set; }

		/// <summary>
		/// Gets or sets the contract length description.
		/// </summary>
		/// <value>
		/// The contract length description.
		/// </value>

		public string ContractLengthDescription { get; set; }

		/// <summary>
		/// Gets or sets the SVL identifier.
		/// </summary>
		/// <value>
		/// The SVL identifier.
		/// </value>

		public string SvlId { get; set; }

		/// <summary>
		/// Gets or sets the status identifier.
		/// </summary>
		/// <value>
		/// The status identifier.
		/// </value>

		public string StatusId { get; set; }

		/// <summary>
		/// Gets or sets the rank sequence number.
		/// </summary>
		/// <value>
		/// The rank sequence number.
		/// </value>

		public int RankSequenceNumber { get; set; }

		/// <summary>
		/// Gets or sets the planning status identifier.
		/// </summary>
		/// <value>
		/// The planning status identifier.
		/// </value>

		public string PlanningStatusId { get; set; }

		/// <summary>
		/// Gets or sets the service active status identifier.
		/// </summary>
		/// <value>
		/// The service active status identifier.
		/// </value>

		public int ServiceActiveStatusId { get; set; }

		/// <summary>
		/// Gets or sets the line up.
		/// </summary>
		/// <value>
		/// The line up.
		/// </value>

		public string LineUp { get; set; }

		/// <summary>
		/// Gets or sets the crew personal rank identifier.
		/// </summary>
		/// <value>
		/// The crew personal rank identifier.
		/// </value>

		public string CrewPersonalRankId { get; set; }

		/// <summary>
		/// Gets or sets the berth count.
		/// </summary>
		/// <value>
		/// The berth count.
		/// </value>

		public int BerthCount { get; set; }

		/// <summary>
		/// Gets or sets the CRW identifier tp.
		/// </summary>
		/// <value>
		/// The CRW identifier tp.
		/// </value>

		public string CrwIdTp { get; set; }

		/// <summary>
		/// Gets or sets the last login in seafarer portal.
		/// </summary>
		/// <value>
		/// The last login in seafarer portal.
		/// </value>

		public DateTime? LastLoginInSeafarerPortal { get; set; }

		/// <summary>
		/// Gets or sets the CMP identifier.
		/// </summary>
		/// <value>
		/// The CMP identifier.
		/// </value>

		public string CmpId { get; set; }

		/// <summary>
		/// Gets or sets the name of the supplier.
		/// </summary>
		/// <value>
		/// The name of the supplier.
		/// </value>

		public string CompanyName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active in work and rest.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active in work and rest; otherwise, <c>false</c>.
		/// </value>

		public bool IsActiveInWorkAndRest { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>

		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the port identifier.
		/// </summary>
		/// <value>
		/// The port identifier.
		/// </value>

		public string PortId { get; set; }

		/// <summary>
		/// Gets or sets the PCN number.
		/// </summary>
		/// <value>
		/// The PCN number.
		/// </value>

		public string PcnNumber { get; set; }

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		/// <value>
		/// The gender.
		/// </value>

		public string Gender { get; set; }

		/// <summary>
		/// Gets or sets the date of birth.
		/// </summary>
		/// <value>
		/// The date of birth.
		/// </value>

		public DateTime? DateOfBirth { get; set; }

		/// <summary>
		/// Gets or sets the seamans book number.
		/// </summary>
		/// <value>
		/// The seamans book number.
		/// </value>

		public string SeamansBookNumber { get; set; }

		/// <summary>
		/// Gets or sets the name of the agency.
		/// </summary>
		/// <value>
		/// The name of the agency.
		/// </value>

		public string AgencyName { get; set; }

		/// <summary>
		/// Gets or sets the rank short code.
		/// </summary>
		/// <value>
		/// The rank short code.
		/// </value>

		public string RankShortCode { get; set; }

		/// <summary>
		/// Gets or sets the dep identifier.
		/// </summary>
		/// <value>
		/// The dep identifier.
		/// </value>

		public string DepId { get; set; }

		/// <summary>
		/// Gets or sets the usr identifier.
		/// </summary>
		/// <value>
		/// The usr identifier.
		/// </value>

		public string UsrId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>

		public bool IsActive { get; set; }

		/// <summary>
		/// Gets or sets the place of birth.
		/// </summary>
		/// <value>
		/// The place of birth.
		/// </value>

		public string PlaceOfBirth { get; set; }

		/// <summary>
		/// Gets or sets the type of the obl identifier supply.
		/// </summary>
		/// <value>
		/// The type of the obl identifier supply.
		/// </value>

		public string OblIdSupplyType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is access to shipsure.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is access to shipsure; otherwise, <c>false</c>.
		/// </value>

		public bool IsAccessToShipsure { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is include in imo crew list.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is include in imo crew list; otherwise, <c>false</c>.
		/// </value>

		public bool IsIncludeInIMOCrewList { get; set; }

		/// <summary>
		/// Gets or sets the type of the supply.
		/// </summary>
		/// <value>
		/// The type of the supply.
		/// </value>

		public string SupplyType { get; set; }

		/// <summary>
		/// Gets or sets the current appraisal status.
		/// </summary>
		/// <value>
		/// The current appraisal status.
		/// </value>

		public string CurrentAppraisalStatus { get; set; }

		/// <summary>
		/// Gets or sets the current appraisal date.
		/// </summary>
		/// <value>
		/// The current appraisal date.
		/// </value>

		public DateTime? CurrentAppraisalDate { get; set; }

		/// <summary>
		/// Gets or sets the type of the document.
		/// </summary>
		/// <value>
		/// The type of the document.
		/// </value>

		public string DocumentType { get; set; }

		/// <summary>
		/// Gets or sets the name of the country.
		/// </summary>
		/// <value>
		/// The name of the country.
		/// </value>

		public string CountryName { get; set; }

		/// <summary>
		/// Gets or sets the sign off port identifier.
		/// </summary>
		/// <value>
		/// The sign off port identifier.
		/// </value>

		public string SignOffPortId { get; set; }

		/// <summary>
		/// Gets or sets the sign on port.
		/// </summary>
		/// <value>
		/// The sign on port.
		/// </value>

		public string SignOnPort { get; set; }

		/// <summary>
		/// Gets or sets the sign off port.
		/// </summary>
		/// <value>
		/// The sign off port.
		/// </value>

		public string SignOffPort { get; set; }

		/// <summary>
		/// Gets or sets the document detail.
		/// </summary>
		/// <value>
		/// The document detail.
		/// </value>

		public List<UnsyncCrewDocumentDetail> DocumentDetail { get; set; }

		/// <summary>
		/// Gets or sets the current appraisal status identifier.
		/// </summary>
		/// <value>
		/// The current appraisal status identifier.
		/// </value>

		public string CurrentAppraisalStatusId { get; set; }

		/// <summary>
		/// Gets or sets the shipsure last login.
		/// </summary>
		/// <value>
		/// The shipsure last login.
		/// </value>

		public DateTime? ShipsureLastLogin { get; set; }

		/// <summary>
		/// Gets or sets the role identifier.
		/// </summary>
		/// <value>
		/// The role identifier.
		/// </value>

		public string RoleIdentifier { get; set; }

		/// <summary>
		/// Gets or sets the email address.
		/// </summary>
		/// <value>
		/// The email address.
		/// </value>

		public string EmailAddress { get; set; }

		/// <summary>
		/// Gets or sets the shipsure role.
		/// </summary>
		/// <value>
		/// The shipsure role.
		/// </value>

		public string ShipsureRole { get; set; }

		/// <summary>
		/// Gets or sets the WRK identifier.
		/// </summary>
		/// <value>
		/// The WRK identifier.
		/// </value>

		public string WrkId { get; set; }

		/// <summary>
		/// Gets or sets the office sign on date.
		/// </summary>
		/// <value>
		/// The office sign on date.
		/// </value>
		/// This property used only while editing sync crew member details. The date is captured from CRWSrvDetail (Set_StartDate) table.

		public DateTime? OfficeSignOnDate { get; set; }

		/// <summary>
		/// Gets or sets the office sign off date.
		/// </summary>
		/// <value>
		/// The office sign off date.
		/// </value>
		/// This property used only while editing sync crew member details. The date is captured from CRWSrvDetail (Set_EndDate) table.

		public DateTime? OfficeSignOffDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is email use for login.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is email use for login; otherwise, <c>false</c>.
		/// </value>

		public bool IsEmailUseForLogin { get; set; }

		/// <summary>
		/// Gets or sets the personal email address.
		/// </summary>
		/// <value>
		/// The personal email address.
		/// </value>

		public string PersonalEmailAddress { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [cca is officer].
		/// </summary>
		/// <value>
		///   <c>true</c> if [cca is officer]; otherwise, <c>false</c>.
		/// </value>
		public bool CcaIsOfficer { get; set; }

		/// <summary>
		/// Gets or sets the berth type short code.
		/// </summary>
		/// <value>
		/// The berth type short code.
		/// </value>
		public string BerthTypeShortCode { get; set; }

		/// <summary>
		/// Gets or sets the berth type description.
		/// </summary>
		/// <value>
		/// The berth type description.
		/// </value>
		public string BerthTypeDescription { get; set; }

		/// <summary>
		/// Gets or sets the berth to date.
		/// </summary>
		/// <value>
		/// The berth to date.
		/// </value>
		public DateTime? BerthToDate { get; set; }
	}
}
