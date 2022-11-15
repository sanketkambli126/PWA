using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.Models.Report.InspectionManager
{
	/// <summary>
	/// InspectionOverview Request Contract
	/// </summary>
	public class InspectionsOverviewForVesselsRequest
	{
		/// <summary>
		/// Gets or sets the office identifier.
		/// </summary>
		/// <value>
		/// The office identifier.
		/// </value>
		public string OfficeId { get; set; }

		/// <summary>
		/// Gets or sets the fleet identifier.
		/// </summary>
		/// <value>
		/// The fleet identifier.
		/// </value>
		public string FleetId { get; set; }

		/// <summary>
		/// Gets or sets the vessel ids.
		/// </summary>
		/// <value>
		/// The vessel ids.
		/// </value>
		public List<string> VesselIds { get; set; }

		/// <summary>
		/// Gets or sets the start date.
		/// </summary>
		/// <value>
		/// The start date.
		/// </value>
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// Gets or sets the end date.
		/// </summary>
		/// <value>
		/// The end date.
		/// </value>
		public DateTime? EndDate { get; set; }

		/// <summary>
		/// Gets or sets the inspection finding types.
		/// </summary>
		/// <value>
		/// The inspection finding types.
		/// </value>
		public List<InspectionFindingType> InspectionFindingTypes { get; set; }

		/// <summary>
		/// Gets or sets the inspectiontype identifier.
		/// </summary>
		/// <value>
		/// The inspectiontype identifier.
		/// </value>
		public string InspectiontypeId { get; set; }

		/// <summary>
		/// Gets or sets the type of the inspection.
		/// </summary>
		/// <value>
		/// The type of the inspection.
		/// </value>
		public string InspectionType { get; set; }

		/// <summary>
		/// Gets or sets the company.
		/// </summary>
		/// <value>
		/// The company.
		/// </value>
		public string Company { get; set; }

		/// <summary>
		/// Gets or sets the company identifier.
		/// </summary>
		/// <value>
		/// The company identifier.
		/// </value>
		public string CompanyId { get; set; }

		/// <summary>
		/// Gets or sets the department member identifier.
		/// </summary>
		/// <value>
		/// The department member identifier.
		/// </value>
		public string DepartmentMemberId { get; set; }

		/// <summary>
		/// Gets or sets the name of the inspector.
		/// </summary>
		/// <value>
		/// The name of the inspector.
		/// </value>
		public string InspectorName { get; set; }

		/// <summary>
		/// Gets or sets the type of the vessel.
		/// </summary>
		/// <value>
		/// The type of the vessel.
		/// </value>
		public string VesselType { get; set; }

		/// <summary>
		/// Gets or sets the vessel flag.
		/// </summary>
		/// <value>
		/// The vessel flag.
		/// </value>
		public string VesselFlag { get; set; }

		/// <summary>
		/// Gets or sets the port identifier.
		/// </summary>
		/// <value>
		/// The port identifier.
		/// </value>
		public string PortId { get; set; }

		/// <summary>
		/// Gets or sets the where.
		/// </summary>
		/// <value>
		/// The where.
		/// </value>
		public string Where { get; set; }

		/// <summary>
		/// Gets or sets the inspections filter.
		/// </summary>
		/// <value>
		/// The inspections filter.
		/// </value>
		public InspectionsFilter? InspectionsFilter { get; set; }

		/// <summary>
		/// Gets or sets the filter string.
		/// </summary>
		/// <value>
		/// The filter string.
		/// </value>
		public string FilterString { get; set; }

		/// <summary>
		/// Gets or sets the is show detained.
		/// </summary>
		/// <value>
		/// The is show detained.
		/// </value>
		public bool IsShowDetained { get; set; }

		/// <summary>
		/// Gets or sets the in days.
		/// </summary>
		/// <value>
		/// The in days.
		/// </value>
		public int InDays { get; set; }

		/// <summary>
		/// Gets or sets the inspection type ids.
		/// </summary>
		/// <value>
		/// The inspection type ids.
		/// </value>
		public List<string> InspectionTypeIds { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is all selected.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is all selected; otherwise, <c>false</c>.
		/// </value>
		public bool IsAllSelected { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is finding outstanding.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is finding outstanding; otherwise, <c>false</c>.
		/// </value>
		public bool IsFindingOutstanding { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is finding overdue.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is finding overdue; otherwise, <c>false</c>.
		/// </value>
		public bool IsFindingOverdue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is pending closure.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is pending closure; otherwise, <c>false</c>.
		/// </value>
		public bool IsPendingClosure { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is closed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is closed; otherwise, <c>false</c>.
		/// </value>
		public bool IsClosed { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is due.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is due; otherwise, <c>false</c>.
		/// </value>
		public bool IsDue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is overdue.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is overdue; otherwise, <c>false</c>.
		/// </value>
		public bool IsOverdue { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is at sea.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is at sea; otherwise, <c>false</c>.
		/// </value>
		public bool IsAtSea { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is at port.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is at port; otherwise, <c>false</c>.
		/// </value>
		public bool IsAtPort { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is detention.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is detention; otherwise, <c>false</c>.
		/// </value>
		public bool IsDetention { get; set; }

		/// <summary>
		/// Gets or sets the type of the menu.
		/// </summary>
		/// <value>
		/// The type of the menu.
		/// </value>
		public string MenuType { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the inspection identifier.
		/// </summary>
		/// <value>
		/// The inspection identifier.
		/// </value>
		public string InspectionId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is omv rejection.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is omv rejection; otherwise, <c>false</c>.
		/// </value>
		public bool IsOMVRejection { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is PSC deficency.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is PSC deficency; otherwise, <c>false</c>.
		/// </value>
		public bool IsPSCDeficency { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is deleted inspection.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is deleted inspection; otherwise, <c>false</c>.
		/// </value>
		public bool IsDeletedInspection { get; set; }
	}
}
