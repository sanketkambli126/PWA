﻿using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.Sentinel
{
	/// <summary>
	/// Fleet Vessel Detail Response
	/// </summary>
	public class FleetVesselDetailResponse
	{
		/// <summary>
		/// Gets or sets the office identifier.
		/// </summary>
		/// <value>
		/// The office identifier.
		/// </value>
		public string OfficeId { get; set; }

		/// <summary>
		/// Gets or sets the name of the office.
		/// </summary>
		/// <value>
		/// The name of the office.
		/// </value>
		public string OfficeName { get; set; }

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
		/// Gets or sets the vessel model combined value.
		/// </summary>
		/// <value>
		/// The vessel model combined value.
		/// </value>
		public decimal? VesselModelCombinedValue { get; set; }

		/// <summary>
		/// Gets or sets the color of the vessel model combined.
		/// </summary>
		/// <value>
		/// The color of the vessel model combined.
		/// </value>
		public string VesselModelCombinedColor { get; set; }

		/// <summary>
		/// Gets or sets the model combined value.
		/// </summary>
		/// <value>
		/// The model combined value.
		/// </value>
		public decimal? ModelCombinedValue { get; set; }

		/// <summary>
		/// Gets or sets the color of the model combined.
		/// </summary>
		/// <value>
		/// The color of the model combined.
		/// </value>
		public string ModelCombinedColor { get; set; }

		/// <summary>
		/// Gets or sets the model dimension value change status.
		/// </summary>
		/// <value>
		/// The model dimension value change status.
		/// </value>
		public int? ModelDimensionValueChangeStatus { get; set; }

		/// <summary>
		/// Gets or sets the model dimension value difference.
		/// </summary>
		/// <value>
		/// The model dimension value difference.
		/// </value>
		public decimal? ModelDimensionValueDifference { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has active override.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has active override; otherwise, <c>false</c>.
		/// </value>
		public bool? HasActiveOverride { get; set; }

		/// <summary>
		/// Gets or sets the latest history date.
		/// </summary>
		/// <value>
		/// The latest history date.
		/// </value>
		public DateTime? LatestHistoryDate { get; set; }

		/// <summary>
		/// Gets or sets the total records.
		/// </summary>
		/// <value>
		/// The total records.
		/// </value>
		public int TotalRecords { get; set; }

		/// <summary>
		/// Gets or sets the name of the model category.
		/// </summary>
		/// <value>
		/// The name of the model category.
		/// </value>
		public string ModelCategoryName { get; set; }

		/// <summary>
		/// Gets or sets the vessel current voyage detail.
		/// </summary>
		/// <value>
		/// The vessel current voyage detail.
		/// </value>
		public List<SentinelDashboardVesselCurrentVoyageDetail> VesselCurrentVoyageDetail { get; set; }
	}
}
