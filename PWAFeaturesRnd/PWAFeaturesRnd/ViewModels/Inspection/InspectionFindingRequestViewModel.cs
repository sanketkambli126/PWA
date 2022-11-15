using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
	/// <summary>
	/// 
	/// </summary>
	public class InspectionFindingRequestViewModel : BaseViewModel
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
		/// Gets or sets the inspection identifier.
		/// </summary>
		/// <value>
		/// The inspection identifier.
		/// </value>
		public string InspectionId { get; set; }

		/// <summary>
		/// Gets or sets the inspection finding filter.
		/// </summary>
		/// <value>
		/// The inspection finding filter.
		/// </value>
		public InspectionFindingFilter InspectionFindingFilter { get; set; }

		/// <summary>
		/// Gets or sets the inspection type identifier.
		/// </summary>
		/// <value>
		/// The inspection type identifier.
		/// </value>
		public string InspectionTypeId { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is PSC visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is PSC visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsPscVisible { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is omv type.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is omv type; otherwise, <c>false</c>.
		/// </value>
		public bool IsOMVType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is causes section visible.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is causes section visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsCausesSectionVisible { get; set; }

		/// <summary>
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		public DateTime? FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		public DateTime? ToDate { get; set; }

		/// <summary>
		/// Gets or sets the type of the inspection.
		/// </summary>
		/// <value>
		/// The type of the inspection.
		/// </value>
		public InspectionDashboardType? InspectionType { get; set; }

		/// <summary>
		/// Gets or sets the is planning or clouser.
		/// </summary>
		/// <value>
		/// The is planning or clouser.
		/// </value>
		public string IsPlanningOrClouser { get; set; }

		/// <summary>
		/// Gets or sets the inspection URL.
		/// </summary>
		/// <value>
		/// The inspection URL.
		/// </value>
		public string InspectionUrl { get; set; }

		/// <summary>
		/// Gets or sets the inspection finding.
		/// </summary>
		/// <value>
		/// The inspection finding.
		/// </value>
		public string InspectionName { get; set; }

		/// <summary>
		/// Gets or sets the occured date.
		/// </summary>
		/// <value>
		/// The occured date.
		/// </value>
		public string OccuredDate { get; set; }

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
		/// Gets or sets the status list.
		/// </summary>
		/// <value>
		/// The status list.
		/// </value>
		public List<string> StatusList { get; set; }

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
		/// Gets or sets a value indicating whether this instance is show detained.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is show detained; otherwise, <c>false</c>.
		/// </value>
		public bool IsShowDetained { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is status changed.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is status changed; otherwise, <c>false</c>.
		/// </value>
		public bool IsStatusChanged { get; set; }

		/// <summary>
		/// Gets or sets the string inspection type ids.
		/// </summary>
		/// <value>
		/// The string inspection type ids.
		/// </value>
		public string strInspectionTypeIds { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is summary clicked.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is summary clicked; otherwise, <c>false</c>.
		/// </value>
		public bool IsSummaryClicked { get; set; }

		/// <summary>
		/// Gets or sets the inspection filter.
		/// </summary>
		/// <value>
		/// The inspection filter.
		/// </value>
		public InspectionsFilter? InspectionFilter { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is inspection.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is inspection; otherwise, <c>false</c>.
		/// </value>
		public bool IsInspection { get; set; }

		/// <summary>
		/// Gets or sets the TPL identifier.
		/// </summary>
		/// <value>
		/// The TPL identifier.
		/// </value>
		public string TplId { get; set; }

		/// <summary>
		/// Gets or sets the message details json.
		/// </summary>
		/// <value>
		/// The message details json.
		/// </value>
		public string MessageDetailsJSON { get; set; }

        /// <summary>
        /// Gets or sets the encrypted inspection identifier.
        /// </summary>
        /// <value>
        /// The encrypted inspection identifier.
        /// </value>
        public string EncryptedInspectionId { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

		/// <summary>
		/// Gets or sets the active mobile tab class.
		/// </summary>
		/// <value>
		/// The active mobile tab class.
		/// </value>
		public string ActiveMobileTabClass { get; set; }
	}
}
