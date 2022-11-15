using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PWAFeaturesRnd.Common.Converter;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Defect
{
    /// <summary>
    /// Defect List ViewModel
    /// </summary>
    public class DefectListViewModel
    {
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
        /// Gets or sets the created in last n months.
        /// </summary>
        /// <value>
        /// The created in last n months.
        /// </value>
        public int CreatedInLastNMonths { get; set; }

        /// <summary>
        /// Gets or sets the type of the menu.
        /// </summary>
        /// <value>
        /// The type of the menu.
        /// </value>
        public string MenuType { get; set; }

        /// <summary>
        /// Gets or sets the name of the stage.
        /// </summary>
        /// <value>
        /// The name of the stage.
        /// </value>
        public string StageName { get; set; }

        /// <summary>
        /// Gets or sets the encrypted vessel identifier.
        /// </summary>
        /// <value>
        /// The encrypted vessel identifier.
        /// </value>
        public string EncryptedVesselId { get; set; }

        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }

        /// <summary>
        /// Gets or sets the selected due status.
        /// </summary>
        /// <value>
        /// The selected due status.
        /// </value>
        public string SelectedDueStatus { get; set; }

        /// <summary>
        /// Gets or sets the selected critical status.
        /// </summary>
        /// <value>
        /// The selected critical status.
        /// </value>
        public string SelectedCriticalStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is search clicked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is search clicked; otherwise, <c>false</c>.
        /// </value>
        public bool IsSearchClicked { get; set; }

        /// <summary>
        /// Gets or sets the selected planned for.
        /// </summary>
        /// <value>
        /// The selected planned for.
        /// </value>
        public List<string> SelectedPlannedFor { get; set; }

        /// <summary>
        /// Gets or sets the selected status.
        /// </summary>
        /// <value>
        /// The selected status.
        /// </value>
        public List<string> SelectedStatus { get; set; }

        /// <summary>
        /// Gets or sets the selected system area.
        /// </summary>
        /// <value>
        /// The selected system area.
        /// </value>
        public List<string> SelectedSystemArea { get; set; }

        /// <summary>
        /// Gets or sets the defect title.
        /// </summary>
        /// <value>
        /// The defect title.
        /// </value>
        public string DefectTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is overdue.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is overdue; otherwise, <c>false</c>.
        /// </value>
        public bool IsOverdue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is off hire.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
        /// </value>
        public bool IsOffHire { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [add in damage form].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [add in damage form]; otherwise, <c>false</c>.
        /// </value>
        public bool AddInDamageForm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [guarantee claim required].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [guarantee claim required]; otherwise, <c>false</c>.
        /// </value>
        public bool GuaranteeClaimRequired { get; set; }

        /// <summary>
        /// Gets or sets the active mobile tab class.
        /// </summary>
        /// <value>
        /// The active mobile tab class.
        /// </value>
        public string ActiveMobileTabClass { get; set; }
        /// <summary>
        /// Gets or sets the workplannedforids
        /// </summary>
        public string SelectedPlannedForIds { get; set; }
        /// <summary>
        /// Gets or sets the workstatusids
        /// </summary>
        public string SelectedStatusIds { get; set; }
        /// <summary>
        /// Gets or sets the systemareaids
        /// </summary>
        public string SelectedSystemAreaIds { get; set; }

        public string GridSubTitle { get; set; }

        /// <summary>
		/// Gets or sets the fleet identifier.
		/// </summary>
		/// <value>
		/// The fleet identifier.
		/// </value>
		public string FleetId { get; set; }

        /// <summary>
        /// Gets or sets the vessel identifier.
        /// </summary>
        /// <value>
        /// The vessel identifier.
        /// </value>
        public string VesselId { get; set; }
    }
}
