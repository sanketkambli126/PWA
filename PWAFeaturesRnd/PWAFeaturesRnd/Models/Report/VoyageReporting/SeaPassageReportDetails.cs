using System;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
	/// <summary>
	/// Sea Passage Report Details
	/// </summary>
	public class SeaPassageReportDetails
	{
		/// <summary>
		/// Gets or sets the go charter requirement loaded.
		/// </summary>
		/// <value>
		/// The go charter requirement loaded.
		/// </value>
		public float GoCharterRequirementLoaded { get; set; }

		/// <summary>
		/// Gets or sets the go charter requirement ballast.
		/// </summary>
		/// <value>
		/// The go charter requirement ballast.
		/// </value>
		public float GoCharterRequirementBallast { get; set; }

		/// <summary>
		/// Gets or sets the state of the sea.
		/// </summary>
		/// <value>
		/// The state of the sea.
		/// </value>
		public string SeaState { get; set; }

		/// <summary>
		/// Gets or sets the wind speed.
		/// </summary>
		/// <value>
		/// The wind speed.
		/// </value>
		public decimal? WindSpeed { get; set; }

		/// <summary>
		/// Gets or sets the wind direction.
		/// </summary>
		/// <value>
		/// The wind direction.
		/// </value>
		public string WindDirection { get; set; }

		/// <summary>
		/// Gets or sets the forob.
		/// </summary>
		/// <value>
		/// The forob.
		/// </value>
		public float? FOROB { get; set; }

		/// <summary>
		/// Gets or sets the lsforob.
		/// </summary>
		/// <value>
		/// The lsforob.
		/// </value>
		public float? LSFOROB { get; set; }

		/// <summary>
		/// Gets or sets the gorob.
		/// </summary>
		/// <value>
		/// The gorob.
		/// </value>
		public float? GOROB { get; set; }

		/// <summary>
		/// Gets or sets the dorob.
		/// </summary>
		/// <value>
		/// The dorob.
		/// </value>
		public float? DOROB { get; set; }

		/// <summary>
		/// Gets or sets the lngrob.
		/// </summary>
		/// <value>
		/// The lngrob.
		/// </value>
		public float? LNGROB { get; set; }

		/// <summary>
		/// Gets or sets the LNG.
		/// </summary>
		/// <value>
		/// The LNG.
		/// </value>
		public float LNG { get; set; }

		/// <summary>
		/// Gets or sets the LNG charter requirement loaded.
		/// </summary>
		/// <value>
		/// The LNG charter requirement loaded.
		/// </value>
		public float? LNGCharterRequirementLoaded { get; set; }

		/// <summary>
		/// Gets or sets the LNG charter requirement ballast.
		/// </summary>
		/// <value>
		/// The LNG charter requirement ballast.
		/// </value>
		public float? LNGCharterRequirementBallast { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has break.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has break; otherwise, <c>false</c>.
		/// </value>
		public bool HasBreak { get; set; }

		/// <summary>
		/// Gets or sets the break hours.
		/// </summary>
		/// <value>
		/// The break hours.
		/// </value>
		public string BreakHours { get; set; }

		/// <summary>
		/// Gets or sets the break distance.
		/// </summary>
		/// <value>
		/// The break distance.
		/// </value>
		public float BreakDistance { get; set; }

		/// <summary>
		/// Gets or sets the aux lub oil consumption.
		/// </summary>
		/// <value>
		/// The aux lub oil consumption.
		/// </value>
		public float? AuxLubOilConsumption { get; set; }

		/// <summary>
		/// Gets or sets the clo lub oil consumption.
		/// </summary>
		/// <value>
		/// The clo lub oil consumption.
		/// </value>
		public float? CloLubOilConsumption { get; set; }

		/// <summary>
		/// Gets or sets the crank lub oil consumption.
		/// </summary>
		/// <value>
		/// The crank lub oil consumption.
		/// </value>
		public float? CrankLubOilConsumption { get; set; }

		/// <summary>
		/// Gets or sets the general lub oil consumption.
		/// </summary>
		/// <value>
		/// The general lub oil consumption.
		/// </value>
		public float? GeneralLubOilConsumption { get; set; }

		/// <summary>
		/// Gets or sets the charter wind force.
		/// </summary>
		/// <value>
		/// The charter wind force.
		/// </value>
		public string CharterWindForce { get; set; }

		/// <summary>
		/// Gets or sets the maximum wind force.
		/// </summary>
		/// <value>
		/// The maximum wind force.
		/// </value>
		public string MaxWindForce { get; set; }

		/// <summary>
		/// Gets or sets the fw domestic rob.
		/// </summary>
		/// <value>
		/// The fw domestic rob.
		/// </value>
		public float? FWDomesticROB { get; set; }

		/// <summary>
		/// Gets or sets the fw technical rob.
		/// </summary>
		/// <value>
		/// The fw technical rob.
		/// </value>
		public float? FWTechnicalROB { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is IDL.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is IDL; otherwise, <c>false</c>.
		/// </value>
		public bool IsIDL { get; set; }

		/// <summary>
		/// Gets or sets the is noon incomplete.
		/// </summary>
		/// <value>
		/// The is noon incomplete.
		/// </value>
		public bool? IsNoonIncomplete { get; set; }

		/// <summary>
		/// Gets or sets the charter identifier.
		/// </summary>
		/// <value>
		/// The charter identifier.
		/// </value>
		public string CharterId { get; set; }

		/// <summary>
		/// Gets or sets the do charter requirement ballast.
		/// </summary>
		/// <value>
		/// The do charter requirement ballast.
		/// </value>
		public float DoCharterRequirementBallast { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is noon synopsis added.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is noon synopsis added; otherwise, <c>false</c>.
		/// </value>
		public bool IsNoonSynopsisAdded { get; set; }

		/// <summary>
		/// Gets or sets the do charter requirement loaded.
		/// </summary>
		/// <value>
		/// The do charter requirement loaded.
		/// </value>
		public float DoCharterRequirementLoaded { get; set; }

		/// <summary>
		/// Gets or sets the lsfo charter requirement loaded.
		/// </summary>
		/// <value>
		/// The lsfo charter requirement loaded.
		/// </value>
		public float LsfoCharterRequirementLoaded { get; set; }

		/// <summary>
		/// Gets or sets the vessel identifier.
		/// </summary>
		/// <value>
		/// The vessel identifier.
		/// </value>
		public string VesselId { get; set; }

		/// <summary>
		/// Gets or sets the date.
		/// </summary>
		/// <value>
		/// The date.
		/// </value>
		public DateTime? Date { get; set; }

		/// <summary>
		/// Gets or sets the activity.
		/// </summary>
		/// <value>
		/// The activity.
		/// </value>
		public string Activity { get; set; }

		/// <summary>
		/// Gets or sets the course.
		/// </summary>
		/// <value>
		/// The course.
		/// </value>
		public float? Course { get; set; }

		/// <summary>
		/// Gets or sets the time sailed.
		/// </summary>
		/// <value>
		/// The time sailed.
		/// </value>
		public string TimeSailed { get; set; }

		/// <summary>
		/// Gets or sets the stops.
		/// </summary>
		/// <value>
		/// The stops.
		/// </value>
		public string Stops { get; set; }

		/// <summary>
		/// Gets or sets the distance.
		/// </summary>
		/// <value>
		/// The distance.
		/// </value>
		public float? Distance { get; set; }

		/// <summary>
		/// Gets or sets the distance to go.
		/// </summary>
		/// <value>
		/// The distance to go.
		/// </value>
		public float? DistanceToGo { get; set; }

		/// <summary>
		/// Gets or sets the speed.
		/// </summary>
		/// <value>
		/// The speed.
		/// </value>
		public float? Speed { get; set; }

		/// <summary>
		/// Gets or sets the RPM.
		/// </summary>
		/// <value>
		/// The RPM.
		/// </value>
		public float? Rpm { get; set; }

		/// <summary>
		/// Gets or sets the true slip.
		/// </summary>
		/// <value>
		/// The true slip.
		/// </value>
		public float? TrueSlip { get; set; }

		/// <summary>
		/// Gets or sets the fo.
		/// </summary>
		/// <value>
		/// The fo.
		/// </value>
		public float? Fo { get; set; }

		/// <summary>
		/// Gets or sets the lsfo.
		/// </summary>
		/// <value>
		/// The lsfo.
		/// </value>
		public float? Lsfo { get; set; }

		/// <summary>
		/// Gets or sets the do.
		/// </summary>
		/// <value>
		/// The do.
		/// </value>
		public float? Do { get; set; }

		/// <summary>
		/// Gets or sets the go.
		/// </summary>
		/// <value>
		/// The go.
		/// </value>
		public float? Go { get; set; }

		/// <summary>
		/// Gets or sets the charter.
		/// </summary>
		/// <value>
		/// The charter.
		/// </value>
		public string Charter { get; set; }

		/// <summary>
		/// Gets or sets the wind force.
		/// </summary>
		/// <value>
		/// The wind force.
		/// </value>
		public string WindForce { get; set; }

		/// <summary>
		/// Gets or sets the sea postion activity identifier.
		/// </summary>
		/// <value>
		/// The sea postion activity identifier.
		/// </value>
		public string SeaPostionActivityId { get; set; }

		/// <summary>
		/// Gets or sets the type of the position list activity.
		/// </summary>
		/// <value>
		/// The type of the position list activity.
		/// </value>
		public string PositionListActivityType { get; set; }

		/// <summary>
		/// Gets or sets the fresh water consumption domestic.
		/// </summary>
		/// <value>
		/// The fresh water consumption domestic.
		/// </value>
		public float FreshWaterConsumptionDomestic { get; set; }

		/// <summary>
		/// Gets or sets the fresh water consumption technial.
		/// </summary>
		/// <value>
		/// The fresh water consumption technial.
		/// </value>
		public float FreshWaterConsumptionTechnial { get; set; }

		/// <summary>
		/// Gets or sets the liquefied natural gas.
		/// </summary>
		/// <value>
		/// The liquefied natural gas.
		/// </value>
		public float LiquefiedNaturalGas { get; set; }

		/// <summary>
		/// Gets or sets the position list identifier.
		/// </summary>
		/// <value>
		/// The position list identifier.
		/// </value>
		public string PositionListId { get; set; }

		/// <summary>
		/// Gets or sets the speed charter requirement loaded.
		/// </summary>
		/// <value>
		/// The speed charter requirement loaded.
		/// </value>
		public float SpeedCharterRequirementLoaded { get; set; }

		/// <summary>
		/// Gets or sets the speed charter requirement ballast.
		/// </summary>
		/// <value>
		/// The speed charter requirement ballast.
		/// </value>
		public float SpeedCharterRequirementBallast { get; set; }

		/// <summary>
		/// Gets or sets the fo charter requirement loaded.
		/// </summary>
		/// <value>
		/// The fo charter requirement loaded.
		/// </value>
		public float FoCharterRequirementLoaded { get; set; }

		/// <summary>
		/// Gets or sets the fo charter requirement ballast.
		/// </summary>
		/// <value>
		/// The fo charter requirement ballast.
		/// </value>
		public float FoCharterRequirementBallast { get; set; }

		/// <summary>
		/// Gets or sets the lsfo charter requirement ballast.
		/// </summary>
		/// <value>
		/// The lsfo charter requirement ballast.
		/// </value>
		public float LsfoCharterRequirementBallast { get; set; }

		/// <summary>
		/// Gets or sets the reference information.
		/// </summary>
		/// <value>
		/// The reference information.
		/// </value>
		public TresVesselReportList ReferenceInfo { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is delay break in passage.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is delay break in passage; otherwise, <c>false</c>.
		/// </value>
		public bool IsDelayBreakInPassage { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is medical break in passage.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is medical break in passage; otherwise, <c>false</c>.
		/// </value>
		public bool IsMedicalBreakInPassage { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is off hire break in passage.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is off hire break in passage; otherwise, <c>false</c>.
		/// </value>
		public bool IsOffHireBreakInPassage { get; set; }
	}
}
