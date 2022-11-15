using System;

namespace PWAFeaturesRnd.ViewModels.Shared
{
	/// <summary>
	/// The fleetsummary request viewmodel
	/// </summary>
	public class FleetSummaryRequestViewModel
	{
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

		/// <summary>
		/// Gets or sets the type of the menu.
		/// </summary>
		/// <value>
		/// The type of the menu.
		/// </value>
		public string MenuType { get; set; }

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
		public double PSCDeficiencyPriorityLimit { get; set; }

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

		/// <summary>
		/// Gets or sets the incident start date.
		/// </summary>
		/// <value>
		/// The incident start date.
		/// </value>
		public DateTime? IncidentStartDate { get; set; }

		/// <summary>
		/// Gets or sets the incident end date.
		/// </summary>
		/// <value>
		/// The incident end date.
		/// </value>
		public DateTime? IncidentEndDate { get; set; }

		/// <summary>
		/// Gets or sets the serious incidents priority.
		/// </summary>
		/// <value>
		/// The serious incidents priority.
		/// </value>
		public int SeriousIncidentsPriority { get; set; }

		/// <summary>
		/// Gets or sets the critical pmspriority.
		/// </summary>
		/// <value>
		/// The critical pmspriority.
		/// </value>
		public int CriticalPmspriority { get; set; }

		/// <summary>
		/// Gets or sets the ltifrom date.
		/// </summary>
		/// <value>
		/// The ltifrom date.
		/// </value>
		public DateTime? LtiFromDate { get; set; }

		/// <summary>
		/// Gets or sets the ltito date.
		/// </summary>
		/// <value>
		/// The ltito date.
		/// </value>
		public DateTime? LtiToDate { get; set; }

		/// <summary>
		/// Gets or sets the ltifpriority.
		/// </summary>
		/// <value>
		/// The ltifpriority.
		/// </value>
		public int LtifPriority { get; set; }

		/// <summary>
		/// Gets or sets the off hire start date.
		/// </summary>
		/// <value>
		/// The off hire start date.
		/// </value>
		public DateTime? OffHireStartDate { get; set; }

		/// <summary>
		/// Gets or sets the off hire end date.
		/// </summary>
		/// <value>
		/// The off hire end date.
		/// </value>
		public DateTime? OffHireEndDate { get; set; }

		/// <summary>
		/// Gets or sets the off hire priority.
		/// </summary>
		/// <value>
		/// The off hire priority.
		/// </value>
		public string OffHirePriority { get; set; }

		/// <summary>
		/// Gets or sets the right ship priority.
		/// </summary>
		/// <value>
		/// The right ship priority.
		/// </value>
		public int RightShipPriority { get; set; }

		/// <summary>
		/// Gets or sets the oil spill from date.
		/// </summary>
		/// <value>
		/// The oil spill from date.
		/// </value>
		public DateTime? OilSpillFromDate { get; set; }

		/// <summary>
		/// Gets or sets the oil spill to date.
		/// </summary>
		/// <value>
		/// The oil spill to date.
		/// </value>
		public DateTime? OilSpillToDate { get; set; }

		/// <summary>
		/// Gets or sets the oil spill priority limit.
		/// </summary>
		/// <value>
		/// The oil spill priority limit.
		/// </value>
		public int OilSpillPriorityLimit { get; set; }

		/// <summary>
		/// Gets or sets the opex to date.
		/// </summary>
		/// <value>
		/// The opex to date.
		/// </value>
		public DateTime? OpexToDate { get; set; }

		/// <summary>
		/// Gets or sets the budget days.
		/// </summary>
		/// <value>
		/// The budget days.
		/// </value>
		public int BudgetDays { get; set; }

		/// <summary>
		/// Gets or sets the budget percentage high limit.
		/// </summary>
		/// <value>
		/// The budget percentage high limit.
		/// </value>
		public decimal BudgetPercentageHighLimit { get; set; }

		/// <summary>
		/// Gets or sets the budget percentage low limit.
		/// </summary>
		/// <value>
		/// The budget percentage low limit.
		/// </value>
		public decimal BudgetPercentageLowLimit { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency from date.
		/// </summary>
		/// <value>
		/// The fuel efficiency from date.
		/// </value>
		public DateTime? FuelEfficiencyFromDate { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency to date.
		/// </summary>
		/// <value>
		/// The fuel efficiency to date.
		/// </value>
		public DateTime? FuelEfficiencyToDate { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency priority high limit.
		/// </summary>
		/// <value>
		/// The fuel efficiency priority high limit.
		/// </value>
		public decimal FuelEfficiencyPriorityHighLimit { get; set; }

		/// <summary>
		/// Gets or sets the fuel efficiency priority low limit.
		/// </summary>
		/// <value>
		/// The fuel efficiency priority low limit.
		/// </value>
		public decimal FuelEfficiencyPriorityLowLimit { get; set; }
	}
}
