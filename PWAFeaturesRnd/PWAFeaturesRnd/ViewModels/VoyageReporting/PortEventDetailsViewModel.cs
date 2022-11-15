using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    public class PortEventDetailsViewModel
    {

        /// <summary>
        /// Gets or sets a value indicating whether this instance is off hire.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is off hire; otherwise, <c>false</c>.
        /// </value>
        public bool IsOffHire { get; set; }

        /// <summary>
        ///    /// <summary>
        /// Gets or sets type of Off Hire.
        /// </summary>
        /// <value>
        ///   Off Hire type
        /// </value>
        public string OffHireType { get; set; }
        /// Gets or sets the shaft generator power.
        /// </summary>
        /// <value>
        /// The shaft generator power.
        /// </value>
        public decimal? ShaftGeneratorPower { get; set; }

        /// <summary>
        /// Gets or sets the turbogenerator power.
        /// </summary>
        /// <value>
        /// The turbogenerator power.
        /// </value>
        public decimal? TurbogeneratorPower { get; set; }

        /// <summary>
        /// Gets or sets the diesel generator power.
        /// </summary>
        /// <value>
        /// The diesel generator power.
        /// </value>
        public decimal? DieselGeneratorPower { get; set; }

        /// <summary>
        /// Gets or sets the fuel rob break down.
        /// </summary>
        /// <value>
        /// The fuel rob break down.
        /// </value>
        public List<PortCallFuelRobDetailsViewModel> FuelRobBreakDown { get; set; }

        /// <summary>
        /// Gets or sets the fuel rob break down.
        /// </summary>
        /// <value>
        /// The fuel rob break down.
        /// </value>
        public List<string> FuelRobBreakDownNameList { get; set; }

        /// <summary>
        /// Gets or sets the waste rob break down.
        /// </summary>
        /// <value>
        /// The waste rob break down.
        /// </value>
        public List<PortCallFuelRobDetailsViewModel> WasteRobBreakDown { get; set; }

        /// <summary>
        /// Gets or sets the Waste Rob break down name list.
        /// </summary>
        /// <value>
        /// The Waste Rob break down name list.
        /// </value>
        public List<string> WasteRobBreakDownNameList { get; set; }
        /// <summary>
        /// Gets or sets the lub oil break down.
        /// </summary>
        /// <value>
        /// The lub oil break down.
        /// </value>
        public List<PortCallFuelRobDetailsViewModel> LubOilBreakDown { get; set; }

        /// <summary>
        /// Gets or sets the lub oil break down name list.
        /// </summary>
        /// <value>
        /// The lub oil break down name list.
        /// </value>
        public List<string> LubOilBreakDownNameList { get; set; }

        /// <summary>
        /// Gets or sets the fresh water break down name list.
        /// </summary>
        /// <value>
        /// The fresh water break down name list.
        /// </value>
        public List<string> FreshWaterBreakDownNameList { get; set; }

        /// <summary>
        /// Gets or sets the fresh water rob break down.
        /// </summary>
        /// <value>
        /// The fresh water rob break down.
        /// </value>
        public List<PortCallFuelRobDetailsViewModel> FreshWaterRobBreakDown { get; set; }

        /// <summary>
        /// Gets or sets the ballast details.
        /// </summary>
        /// <value>
        /// The ballast details.
        /// </value>
        public List<BallastDetailViewModel> BallastDetails { get; set; }

        /// <summary>
        /// Gets or sets the ballast break down name list.
        /// </summary>
        /// <value>
        /// The ballast break down name list.
        /// </value>
        public List<string> BallastBreakDownNameList { get; set; }
        /// <summary>
        /// Gets or sets the pos24 HRS weather.
        /// </summary>
        /// <value>
        /// The pos24 HRS weather.
        /// </value>
        public List<Port24HrsWeatherViewModel> Pos24HrsWeather { get; set; }

        /// <summary>
        /// Gets or sets the engine running hours.
        /// </summary>
        /// <value>
        /// The engine running hours.
        /// </value>
        public List<PortEngineRunningHoursViewModel> EngineRunningHours { get; set; }

        /// <summary>
        /// Gets or sets the bar movement.
        /// </summary>
        /// <value>
        /// The bar movement.
        /// </value>
        public string BarMovement { get; set; }

        /// <summary>
        /// Gets or sets the speed over ground.
        /// </summary>
        /// <value>
        /// The speed over ground.
        /// </value>
        public decimal? SpeedOverGround { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        /// <value>
        /// The total distance.
        /// </value>
        public decimal? TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the water density.
        /// </summary>
        /// <value>
        /// The water density.
        /// </value>
        public decimal? WaterDensity { get; set; }

        /// <summary>
        /// Gets or sets the security level.
        /// </summary>
        /// <value>
        /// The security level.
        /// </value>
        public string SecurityLevel { get; set; }

        /// <summary>
        /// Gets or sets the is in bound.
        /// </summary>
        /// <value>
        /// The is in bound.
        /// </value>
        public bool? IsInBound { get; set; }

        /// <summary>
        /// Gets or sets the sea dir.
        /// </summary>
        /// <value>
        /// The sea dir.
        /// </value>
        public string SeaDir { get; set; }

        /// <summary>
        /// Gets or sets the sea heig.
        /// </summary>
        /// <value>
        /// The sea heig.
        /// </value>
        public string SeaHeig { get; set; }

        /// <summary>
        /// Gets or sets the state of the sea.
        /// </summary>
        /// <value>
        /// The state of the sea.
        /// </value>
        public string SeaState { get; set; }

        /// <summary>
        /// Gets or sets the PSF is in complete event.
        /// </summary>
        /// <value>
        /// The PSF is in complete event.
        /// </value>
        public bool? PsfIsInCompleteEvent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cargo added from service.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is cargo added from service; otherwise, <c>false</c>.
        /// </value>
        public bool IsCargoAddedFromService { get; set; }

        /// <summary>
        /// Gets or sets the PSF reference identifier.
        /// </summary>
        /// <value>
        /// The PSF reference identifier.
        /// </value>
        public string PSFReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the PSF reference information.
        /// </summary>
        /// <value>
        /// The PSF reference information.
        /// </value>
        public string PSFReferenceInfo { get; set; }

        /// <summary>
        /// Gets or sets the atmospheric pressure.
        /// </summary>
        /// <value>
        /// The atmospheric pressure.
        /// </value>
        public decimal? AtmosphericPressure { get; set; }

        /// <summary>
        /// Gets or sets the current wave direction.
        /// </summary>
        /// <value>
        /// The current wave direction.
        /// </value>
        public string CurrentWaveDirection { get; set; }

        /// <summary>
        /// Gets or sets the waves.
        /// </summary>
        /// <value>
        /// The waves.
        /// </value>
        public string Waves { get; set; }

        /// <summary>
        /// Gets or sets the true wind direction.
        /// </summary>
        /// <value>
        /// The true wind direction.
        /// </value>
        public decimal? TrueWindDirection { get; set; }

        /// <summary>
        /// Gets or sets the PSF identifier.
        /// </summary>
        /// <value>
        /// The PSF identifier.
        /// </value>
        public string PsfId { get; set; }

        /// <summary>
        /// Gets or sets the PPF identifier.
        /// </summary>
        /// <value>
        /// The PPF identifier.
        /// </value>
        public string PpfId { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PosId { get; set; }

        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }

        /// <summary>
        /// Converts to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the is lop issued.
        /// </summary>
        /// <value>
        /// The is lop issued.
        /// </value>
        public byte? IsLopIssued { get; set; }

        /// <summary>
        /// Gets or sets the ship time.
        /// </summary>
        /// <value>
        /// The ship time.
        /// </value>
        public int? ShipTime { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the duration in numeric.
        /// </summary>
        /// <value>
        /// The duration in numeric.
        /// </value>
        public float? DurationInNumeric { get; set; }

        /// <summary>
        /// Gets or sets the forward draft.
        /// </summary>
        /// <value>
        /// The forward draft.
        /// </value>
        public decimal? ForwardDraft { get; set; }

        /// <summary>
        /// Gets or sets the event details.
        /// </summary>
        /// <value>
        /// The event details.
        /// </value>
        public EventXMLDetailsViewModel EventDetails { get; set; }

        /// <summary>
        /// Gets or sets the mid draft.
        /// </summary>
        /// <value>
        /// The mid draft.
        /// </value>
        public decimal? MidDraft { get; set; }

        /// <summary>
        /// Gets or sets the mean draft.
        /// </summary>
        /// <value>
        /// The mean draft.
        /// </value>
        public decimal? MeanDraft { get; set; }

        /// <summary>
        /// Gets or sets the swell direction.
        /// </summary>
        /// <value>
        /// The swell direction.
        /// </value>
        public string SwellDirection { get; set; }

        /// <summary>
        /// Gets or sets the wind direction.
        /// </summary>
        /// <value>
        /// The wind direction.
        /// </value>
        public string WindDirection { get; set; }

        /// <summary>
        /// Gets or sets the height of the swell.
        /// </summary>
        /// <value>
        /// The height of the swell.
        /// </value>
        public string SwellHeight { get; set; }

        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        /// <value>
        /// The speed.
        /// </value>
        public float? Speed { get; set; }

        /// <summary>
        /// Gets or sets the length of the swell.
        /// </summary>
        /// <value>
        /// The length of the swell.
        /// </value>
        public string SwellLength { get; set; }

        /// <summary>
        /// Gets or sets the wind force.
        /// </summary>
        /// <value>
        /// The wind force.
        /// </value>
        public string WindForce { get; set; }

        /// <summary>
        /// Gets or sets the sea temporary.
        /// </summary>
        /// <value>
        /// The sea temporary.
        /// </value>
        public decimal? SeaTemp { get; set; }

        /// <summary>
        /// Gets or sets the air temporary.
        /// </summary>
        /// <value>
        /// The air temporary.
        /// </value>
        public decimal? AirTemp { get; set; }

        /// <summary>
        /// Gets or sets the true wind speed.
        /// </summary>
        /// <value>
        /// The true wind speed.
        /// </value>
        public decimal? TrueWindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the bad weather hours.
        /// </summary>
        /// <value>
        /// The bad weather hours.
        /// </value>
        public decimal? BadWeatherHours { get; set; }

        /// <summary>
        /// Gets or sets the bad weather distance.
        /// </summary>
        /// <value>
        /// The bad weather distance.
        /// </value>
        public decimal? BadWeatherDistance { get; set; }

        /// <summary>
        /// Gets or sets the aft draft.
        /// </summary>
        /// <value>
        /// The aft draft.
        /// </value>
        public decimal? AftDraft { get; set; }

        /// <summary>
        /// Gets or sets the reason for rob mismatch.
        /// </summary>
        /// <value>
        /// The reason for rob mismatch.
        /// </value>
        public string ReasonForRobMismatch { get; set; }

        /// <summary>
        /// The isEospFaopModeEnable
        /// </summary>
        public bool IsEospFaopModeEnable { get; set; }

        /// <summary>
        /// The isConsumptionModeEnable
        /// </summary>
        public bool IsConsumptionModeEnable { get; set; }
    }
}
