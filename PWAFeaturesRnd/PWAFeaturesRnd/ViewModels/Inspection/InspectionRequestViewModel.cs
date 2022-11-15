using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PWAFeaturesRnd.Common.Converter;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
    /// <summary>
    /// 
    /// </summary>
    public class InspectionRequestViewModel
    {
        /// <summary>
        /// Gets or sets the type of the inspection.
        /// </summary>
        /// <value>
        /// The type of the inspection.
        /// </value>
        public InspectionDashboardType? InspectionType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is planning.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is planning; otherwise, <c>false</c>.
        /// </value>
        public bool IsPlanning { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is statistics.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is statistics; otherwise, <c>false</c>.
        /// </value>
        public bool IsStatistics { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        [DataType(DataType.Date)]
        [JsonConverter(typeof(JsonDateConverter))]
        public DateTime? ToDate { get; set; }

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
		/// Gets or sets the fleet identifier.
		/// </summary>
		/// <value>
		/// The fleet identifier.
		/// </value>
		public string FleetId { get; set; }

        /// <summary>
		/// Gets or sets the type of the menu.
		/// </summary>
		/// <value>
		/// The type of the menu.
		/// </value>
		public string MenuType { get; set; }

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
        /// Gets or sets the string inspection type ids.
        /// </summary>
        /// <value>
        /// The string inspection type ids.
        /// </value>
        public string strInspectionTypeIds { get; set; }

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
        /// Gets or sets a value indicating whether this instance is overdue.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is overdue; otherwise, <c>false</c>.
        /// </value>
        public bool IsNeverdone { get; set; }

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
        /// Gets or sets a value indicating whether this instance is type identifier required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is type identifier required; otherwise, <c>false</c>.
        /// </value>
        public bool IsTypeIdRequired { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is type changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is type changed; otherwise, <c>false</c>.
        /// </value>
        public bool IsTypeChanged { get; set; }

        /// <summary>
        /// Gets or sets the company.
        /// </summary>
        /// <value>
        /// The company.
        /// </value>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the inspection.
        /// </summary>
        /// <value>
        /// The inspection.
        /// </value>
        public string Inspector { get; set; }

        /// <summary>
        /// Gets or sets the company identifier.
        /// </summary>
        /// <value>
        /// The company identifier.
        /// </value>
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets the inspection type text field.
        /// </summary>
        /// <value>
        /// The inspection type text field.
        /// </value>
        public string InspectionTypeTextField { get; set; }

        /// <summary>
        /// Gets or sets active tab calss. eg. tab-1 ,tab-2.
        /// </summary>
        /// <value>
        /// The Active Mobile Tab Classe text field.
        /// </value>
        public string ActiveMobileTabClass { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is omv rejection.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is omv rejection; otherwise, <c>false</c>.
        /// </value>
        public bool IsOMVRejection { get; set; }

        /// <summary>
        /// Gets or sets the grid sub title.
        /// </summary>
        /// <value>
        /// The grid sub title.
        /// </value>
        public string GridSubTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is PSC deficency.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is PSC deficency; otherwise, <c>false</c>.
        /// </value>
        public bool IsPSCDeficency { get; set; }
        
    }
}
