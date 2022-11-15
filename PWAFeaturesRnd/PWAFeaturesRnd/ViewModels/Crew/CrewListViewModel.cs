using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PWAFeaturesRnd.Common.Converter;
using PWAFeaturesRnd.Common.Enums;

namespace PWAFeaturesRnd.ViewModels.Crew
{
	/// <summary>
	/// Crew List View Model
	/// </summary>
	public class CrewListViewModel
    {
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
		/// Gets or sets from date.
		/// </summary>
		/// <value>
		/// From date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime FromDate { get; set; }

		/// <summary>
		/// Converts to date.
		/// </summary>
		/// <value>
		/// To date.
		/// </value>
		[DataType(DataType.Date)]
		[JsonConverter(typeof(JsonDateConverter))]
		public DateTime ToDate { get; set; }

		/// <summary>
		/// Gets or sets the selected filter.
		/// </summary>
		/// <value>
		/// The selected filter.
		/// </value>
		public CrewStageFilter SelectedFilter { get; set; }

		/// <summary>
		/// Gets or sets from medical sign off.
		/// </summary>
		/// <value>
		/// From medical sign off.
		/// </value>
		public DateTime FromMedicalSignOff { get; set; }

		/// <summary>
		/// Converts to medicalsignoff.
		/// </summary>
		/// <value>
		/// To medical sign off.
		/// </value>
		public DateTime ToMedicalSignOff { get; set; }

		/// <summary>
		/// Gets or sets the selected stage filter.
		/// </summary>
		/// <value>
		/// The selected stage filter.
		/// </value>
		public string SelectedStageFilter { get; set; }

		/// <summary>
		/// Gets or sets the selected rank ids.
		/// </summary>
		/// <value>
		/// The selected rank ids.
		/// </value>
		public string SelectedRankIds { get; set; }

		/// <summary>
		/// Gets or sets the selected rank descriptions.
		/// </summary>
		/// <value>
		/// The selected rank descriptions.
		/// </value>
		public string SelectedRankDescriptions { get; set; }

		/// <summary>
		/// Gets or sets the selected department ids.
		/// </summary>
		/// <value>
		/// The selected department ids.
		/// </value>
		public string SelectedDepartmentIds { get; set; }

		/// <summary>
		/// Gets or sets the selected department descriptions.
		/// </summary>
		/// <value>
		/// The selected department descriptions.
		/// </value>
		public string SelectedDepartmentDescriptions { get; set; }

		/// <summary>
		/// Gets or sets the active mobile tab class.
		/// </summary>
		/// <value>
		/// The active mobile tab class.
		/// </value>
		public string ActiveMobileTabClass { get; set; }

		/// <summary>
		/// Gets or sets the crew change date.
		/// </summary>
		/// <value>
		/// The crew change date.
		/// </value>
		public DateTime CrewChangeDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is search clicked.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is search clicked; otherwise, <c>false</c>.
		/// </value>
		public bool IsSearchClicked { get; set; }

		/// <summary>
		/// Gets or sets the grid sub title.
		/// </summary>
		/// <value>
		/// The grid sub title.
		/// </value>
		public string GridSubTitle { get; set; }

		/// <summary>
		/// Gets or sets the name of the selected stage.
		/// </summary>
		/// <value>
		/// The name of the selected stage.
		/// </value>
		public string SelectedStageName { get; set; }

	}
}
