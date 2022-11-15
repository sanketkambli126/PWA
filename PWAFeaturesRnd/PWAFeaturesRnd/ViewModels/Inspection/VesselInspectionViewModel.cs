using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
	public class VesselInspectionViewModel
	{
		/// <summary>
		/// Gets or sets the inspection identifier.
		/// </summary>
		/// <value>
		/// The inspection identifier.
		/// </value>
		public string InspectionId { get; set; }

		/// <summary>
		/// Gets or sets the inspection type identifier.
		/// </summary>
		/// <value>
		/// The inspection type identifier.
		/// </value>
		public string InspectionTypeId { get; set; }

		/// <summary>
		/// Gets or sets the name of the inspection type.
		/// </summary>
		/// <value>
		/// The name of the inspection type.
		/// </value>
		public string InspectionTypeName { get; set; }

		/// <summary>
		/// Gets or sets the inspection date.
		/// </summary>
		/// <value>
		/// The inspection date.
		/// </value>
		public string InspectionDate { get; set; }

		/// <summary>
		/// Gets or sets the next due date.
		/// </summary>
		/// <value>
		/// The next due date.
		/// </value>
		public string NextDueDate { get; set; }

		/// <summary>
		/// Gets or sets the date closed.
		/// </summary>
		/// <value>
		/// The date closed.
		/// </value>
		public string DateClosed { get; set; }

		/// <summary>
		/// Gets or sets the inspection status.
		/// </summary>
		/// <value>
		/// The inspection status.
		/// </value>
		public List<InspectionsFilter> InspectionStatus { get; set; }

		/// <summary>
		/// Gets or sets the location.
		/// </summary>
		/// <value>
		/// The location.
		/// </value>
		public string Location { get; set; }

		/// <summary>
		/// Gets or sets the name of the company.
		/// </summary>
		/// <value>
		/// The name of the company.
		/// </value>
		public string CompanyName { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the vessel built.
		/// </summary>
		/// <value>
		/// The vessel built.
		/// </value>
		public DateTime? VesselBuilt { get; set; }

		/// <summary>
		/// Gets or sets the vessel age.
		/// </summary>
		/// <value>
		/// The vessel age.
		/// </value>
		public string VesselAge { get; set; }

		/// <summary>
		/// Gets or sets the type of the vessel.
		/// </summary>
		/// <value>
		/// The type of the vessel.
		/// </value>
		public string VesselType { get; set; }

		/// <summary>
		/// Gets or sets the name of the vessel.
		/// </summary>
		/// <value>
		/// The name of the vessel.
		/// </value>
		public string VesselName { get; set; }

		/// <summary>
		/// Gets or sets the overdue.
		/// </summary>
		/// <value>
		/// The overdue.
		/// </value>
		public int Overdue { get; set; }

		/// <summary>
		/// Gets or sets the outstanding.
		/// </summary>
		/// <value>
		/// The outstanding.
		/// </value>
		public int Outstanding { get; set; }

		/// <summary>
		/// Gets or sets the cleared.
		/// </summary>
		/// <value>
		/// The cleared.
		/// </value>
		public int Cleared { get; set; }

		/// <summary>
		/// Gets or sets the total.
		/// </summary>
		/// <value>
		/// The total.
		/// </value>
		public int Total { get; set; }

		/// <summary>
		/// Gets or sets the oma identifier.
		/// </summary>
		/// <value>
		/// The oma identifier.
		/// </value>
		public string OMAId { get; set; }

		/// <summary>
		/// Gets or sets the action code.
		/// </summary>
		/// <value>
		/// The action code.
		/// </value>
		public string ActionCode { get; set; }

		/// <summary>
		/// Gets or sets the linked defect count.
		/// </summary>
		/// <value>
		/// The linked defect count.
		/// </value>
		public int LinkedDefectCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is root and direct clause mandatory.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is root and direct clause mandatory; otherwise, <c>false</c>.
		/// </value>
		public bool IsRootAndDirectClauseMandatory { get; set; }

		/// <summary>
		/// Gets or sets the TPL identifier.
		/// </summary>
		/// <value>
		/// The TPL identifier.
		/// </value>
		public string TplId { get; set; }

		/// <summary>
		/// Gets or sets the inspection status.
		/// </summary>
		/// <value>
		/// The inspection status.
		/// </value>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the company identifier.
		/// </summary>
		/// <value>
		/// The company identifier.
		/// </value>
		public string CompanyId { get; set; }

		/// <summary>
		/// Gets or sets the total records.
		/// </summary>
		/// <value>
		/// The total records.
		/// </value>
		public int TotalRecords { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is inspection overdue.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is inspection overdue; otherwise, <c>false</c>.
		/// </value>
		public bool IsInspectionOverdue { get; set; }

		/// <summary>
		/// Gets or sets the finding URL.
		/// </summary>
		/// <value>
		/// The finding URL.
		/// </value>
		public string FindingURL { get; set; }

		/// <summary>
		/// Gets or sets the total finding count.
		/// </summary>
		/// <value>
		/// The total finding count.
		/// </value>
		public int TotalFindingCount { get; set; }

		/// <summary>
		/// Gets or sets the out standing finding count.
		/// </summary>
		/// <value>
		/// The out standing finding count.
		/// </value>
		public int OutStandingFindingCount { get; set; }

		/// <summary>
		/// Gets or sets the overdue finding count.
		/// </summary>
		/// <value>
		/// The overdue finding count.
		/// </value>
		public int OverdueFindingCount { get; set; }

		/// <summary>
		/// Gets or sets the days detained.
		/// </summary>
		/// <value>
		/// The days detained.
		/// </value>
		public int? DaysDetained { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is PSC detention.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is PSC detention; otherwise, <c>false</c>.
		/// </value>
		public bool IsPSCDetention { get; set; }

		/// <summary>
		/// Gets or sets the channel count.
		/// </summary>
		/// <value>
		/// The channel count.
		/// </value>
		public int ChannelCount { get; set; }

		/// <summary>
		/// Gets or sets the notes count.
		/// </summary>
		/// <value>
		/// The notes count.
		/// </value>
		public int NotesCount { get; set; }

		/// <summary>
		/// Gets or sets the message details json.
		/// </summary>
		/// <value>
		/// The message details json.
		/// </value>
		public string MessageDetailsJSON { get; set; }
	}
}
