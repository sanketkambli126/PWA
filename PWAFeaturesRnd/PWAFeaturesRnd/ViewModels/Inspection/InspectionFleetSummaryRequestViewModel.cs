using System;

namespace PWAFeaturesRnd.ViewModels.Inspection
{
	/// <summary>
	/// Inspection Fleet Summary Request VM
	/// </summary> 
	public class InspectionFleetSummaryRequestViewModel
    {
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
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the PSC detention from date.
		/// </summary>
		/// <value>
		/// The PSC detention from date.
		/// </value>
		public DateTime? PSCDetentionFromDate { get; set; }

		/// <summary>
		/// Gets or sets the PSC detention to date.
		/// </summary>
		/// <value>
		/// The PSC detention to date.
		/// </value>
		public DateTime? PSCDetentionToDate { get; set; }

		/// <summary>
		/// Gets or sets the PSC detention priority limit.
		/// </summary>
		/// <value>
		/// The PSC detention priority limit.
		/// </value>
		public int PSCDetentionPriorityLimit { get; set; }

		/// <summary>
		/// Gets or sets the PSC deficiency from date.
		/// </summary>
		/// <value>
		/// The PSC deficiency from date.
		/// </value>
		public DateTime? PSCDeficiencyFromDate { get; set; }

		/// <summary>
		/// Gets or sets the PSC deficiency to date.
		/// </summary>
		/// <value>
		/// The PSC deficiency to date.
		/// </value>
		public DateTime? PSCDeficiencyToDate { get; set; }

		/// <summary>
		/// Gets or sets the PSC deficiency priority limit.
		/// </summary>
		/// <value>
		/// The PSC deficiency priority limit.
		/// </value>
		public decimal PSCDeficiencyPriorityLimit { get; set; }

		/// <summary>
		/// Gets or sets the omv findings fromdate.
		/// </summary>
		/// <value>
		/// The omv findings fromdate.
		/// </value>
		public DateTime? OMVFindingsFromdate { get; set; }

		/// <summary>
		/// Gets or sets the omv findings to date.
		/// </summary>
		/// <value>
		/// The omv findings to date.
		/// </value>
		public DateTime? OMVFindingsToDate { get; set; }

		/// <summary>
		/// Gets or sets the omv findings priority high limit.
		/// </summary>
		/// <value>
		/// The omv findings priority high limit.
		/// </value>
		public decimal OMVFindingsPriorityHighLimit { get; set; }

		/// <summary>
		/// Gets or sets the omv findings priority low limit.
		/// </summary>
		/// <value>
		/// The omv findings priority low limit.
		/// </value>
		public decimal OMVFindingsPriorityLowLimit { get; set; }

		/// <summary>
		/// Gets or sets the overdue inspections priority limit.
		/// </summary>
		/// <value>
		/// The overdue inspections priority limit.
		/// </value>
		public int OverdueInspectionsPriorityLimit { get; set; }
	}
}
