using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Sea Passage Charter Detail
	/// </summary>
	public class SeaPassageCharterDetail
	{
		/// <summary>
		/// Gets or sets the actual ls fo per day.
		/// </summary>
		/// <value>
		/// The actual ls fo per day.
		/// </value>
		public float? ActualLsFoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the actual go per day.
		/// </summary>
		/// <value>
		/// The actual go per day.
		/// </value>
		public float? ActualGoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the actual do per day.
		/// </summary>
		/// <value>
		/// The actual do per day.
		/// </value>
		public float? ActualDoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the actual LNG per day.
		/// </summary>
		/// <value>
		/// The actual LNG per day.
		/// </value>
		public float? ActualLngPerDay { get; set; }

		/// <summary>
		/// Gets or sets the departure fore draft.
		/// </summary>
		/// <value>
		/// The departure fore draft.
		/// </value>
		public float? DepartureForeDraft { get; set; }

		/// <summary>
		/// Gets or sets the departure aft draft.
		/// </summary>
		/// <value>
		/// The departure aft draft.
		/// </value>
		public float? DepartureAftDraft { get; set; }

		/// <summary>
		/// Gets or sets the departure mid draft.
		/// </summary>
		/// <value>
		/// The departure mid draft.
		/// </value>
		public float? DepartureMidDraft { get; set; }

		/// <summary>
		/// Gets or sets the departure mean draft.
		/// </summary>
		/// <value>
		/// The departure mean draft.
		/// </value>
		public float? DepartureMeanDraft { get; set; }

		/// <summary>
		/// Gets or sets the fore draft.
		/// </summary>
		/// <value>
		/// The fore draft.
		/// </value>
		public float? ForeDraft { get; set; }

		/// <summary>
		/// Gets or sets the aft draft.
		/// </summary>
		/// <value>
		/// The aft draft.
		/// </value>
		public float? AftDraft { get; set; }

		/// <summary>
		/// Gets or sets the mid draft.
		/// </summary>
		/// <value>
		/// The mid draft.
		/// </value>
		public float? MidDraft { get; set; }

		/// <summary>
		/// Gets or sets the mean draft.
		/// </summary>
		/// <value>
		/// The mean draft.
		/// </value>
		public float? MeanDraft { get; set; }

		/// <summary>
		/// Gets or sets the estimated ballast speed.
		/// </summary>
		/// <value>
		/// The estimated ballast speed.
		/// </value>
		public float? EstimatedBallastSpeed { get; set; }

		/// <summary>
		/// Gets or sets the estimated loaded speed.
		/// </summary>
		/// <value>
		/// The estimated loaded speed.
		/// </value>
		public float? EstimatedLoadedSpeed { get; set; }

		/// <summary>
		/// Gets or sets the actual speed.
		/// </summary>
		/// <value>
		/// The actual speed.
		/// </value>
		public float? ActualSpeed { get; set; }

		/// <summary>
		/// Gets or sets the RPM.
		/// </summary>
		/// <value>
		/// The RPM.
		/// </value>
		public decimal? Rpm { get; set; }

		/// <summary>
		/// Gets or sets the main engine power.
		/// </summary>
		/// <value>
		/// The main engine power.
		/// </value>
		public decimal? MainEnginePower { get; set; }

		/// <summary>
		/// Gets or sets the diesel engine power.
		/// </summary>
		/// <value>
		/// The diesel engine power.
		/// </value>
		public decimal? DieselEnginePower { get; set; }

		/// <summary>
		/// Gets or sets the break in passage details.
		/// </summary>
		/// <value>
		/// The break in passage details.
		/// </value>
		public List<BreakInPassageDetails> BreakInPassageDetails { get; set; }

		/// <summary>
		/// Gets or sets the actual fo per day.
		/// </summary>
		/// <value>
		/// The actual fo per day.
		/// </value>
		public float? ActualFoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the last reported event.
		/// </summary>
		/// <value>
		/// The last reported event.
		/// </value>
		public DateTime? LastReportedEvent { get; set; }

		/// <summary>
		/// Gets or sets the estimated ballast LNG per day.
		/// </summary>
		/// <value>
		/// The estimated ballast LNG per day.
		/// </value>
		public float? EstimatedBallastLngPerDay { get; set; }

		/// <summary>
		/// Gets or sets the estimated ballast do per day.
		/// </summary>
		/// <value>
		/// The estimated ballast do per day.
		/// </value>
		public float? EstimatedBallastDoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the CHT company code.
		/// </summary>
		/// <value>
		/// The CHT company code.
		/// </value>
		public string ChtCompanyCode { get; set; }

		/// <summary>
		/// Gets or sets the CHT company.
		/// </summary>
		/// <value>
		/// The CHT company.
		/// </value>
		public string ChtCompany { get; set; }

		/// <summary>
		/// Gets or sets the CHT vessel.
		/// </summary>
		/// <value>
		/// The CHT vessel.
		/// </value>
		public string ChtVessel { get; set; }

		/// <summary>
		/// Gets or sets the type of the CHT.
		/// </summary>
		/// <value>
		/// The type of the CHT.
		/// </value>
		public string ChtType { get; set; }

		/// <summary>
		/// Gets or sets the CHT voycode.
		/// </summary>
		/// <value>
		/// The CHT voycode.
		/// </value>
		public string ChtVoycode { get; set; }

		/// <summary>
		/// Gets or sets the CHT voyage.
		/// </summary>
		/// <value>
		/// The CHT voyage.
		/// </value>
		public string ChtVoyage { get; set; }

		/// <summary>
		/// Gets or sets the CHT number.
		/// </summary>
		/// <value>
		/// The CHT number.
		/// </value>
		public string ChtNum { get; set; }

		/// <summary>
		/// Gets or sets the type of the trade.
		/// </summary>
		/// <value>
		/// The type of the trade.
		/// </value>
		public string TradeType { get; set; }

		/// <summary>
		/// Gets or sets the estimated distance.
		/// </summary>
		/// <value>
		/// The estimated distance.
		/// </value>
		public float? EstimatedDistance { get; set; }

		/// <summary>
		/// Gets or sets the total distance.
		/// </summary>
		/// <value>
		/// The total distance.
		/// </value>
		public float? TotalDistance { get; set; }

		/// <summary>
		/// Gets or sets the total revolution.
		/// </summary>
		/// <value>
		/// The total revolution.
		/// </value>
		public float? TotalRevolution { get; set; }

		/// <summary>
		/// Gets or sets the time sailed.
		/// </summary>
		/// <value>
		/// The time sailed.
		/// </value>
		public string TimeSailed { get; set; }

		/// <summary>
		/// Gets or sets the estimated loaded fo per day.
		/// </summary>
		/// <value>
		/// The estimated loaded fo per day.
		/// </value>
		public float? EstimatedLoadedFoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the estimated ballast fo per day.
		/// </summary>
		/// <value>
		/// The estimated ballast fo per day.
		/// </value>
		public float? EstimatedBallastFoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the estimated loaded ls fo per day.
		/// </summary>
		/// <value>
		/// The estimated loaded ls fo per day.
		/// </value>
		public float? EstimatedLoadedLsFoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the estimated ballast ls fo per day.
		/// </summary>
		/// <value>
		/// The estimated ballast ls fo per day.
		/// </value>
		public float? EstimatedBallastLsFoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the estimated loaded go per day.
		/// </summary>
		/// <value>
		/// The estimated loaded go per day.
		/// </value>
		public float? EstimatedLoadedGoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the estimated ballast go per day.
		/// </summary>
		/// <value>
		/// The estimated ballast go per day.
		/// </value>
		public float? EstimatedBallastGoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the estimated loaded do per day.
		/// </summary>
		/// <value>
		/// The estimated loaded do per day.
		/// </value>
		public float? EstimatedLoadedDoPerDay { get; set; }

		/// <summary>
		/// Gets or sets the estimated loaded LNG per day.
		/// </summary>
		/// <value>
		/// The estimated loaded LNG per day.
		/// </value>
		public float? EstimatedLoadedLngPerDay { get; set; }

		/// <summary>
		/// Gets or sets the last reported event date.
		/// </summary>
		/// <value>
		/// The last reported event date.
		/// </value>
		public DateTime? LastReportedEventDate { get; set; }
	}
}
