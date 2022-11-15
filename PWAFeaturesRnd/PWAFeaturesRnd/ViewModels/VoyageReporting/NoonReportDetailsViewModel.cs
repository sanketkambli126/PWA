using System;
using System.Collections.Generic;
using PWAFeaturesRnd.ViewModels.Common;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// NoonReportDetailsViewModel
    /// </summary>
    public class NoonReportDetailsViewModel
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public string Date { get; set; }
        /// <summary>
        /// Gets or sets the selected start UTC.
        /// </summary>
        /// <value>
        /// The selected start UTC.
        /// </value>
        public string SelectedStartUTC { get; set; }
        /// <summary>
        /// Gets or sets the crossed IDL days.
        /// </summary>
        /// <value>
        /// The crossed IDL days.
        /// </value>
        public string CrossedIDLDays { get; set; }
        /// <summary>
        /// Gets or sets the business time.
        /// </summary>
        /// <value>
        /// The business time.
        /// </value>
        public string BusinessTime { get; set; }
        /// <summary>
        /// Creates new shiptime.
        /// </summary>
        /// <value>
        /// The new ship time.
        /// </value>
        public string NewShipTime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can show IDL.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can show IDL; otherwise, <c>false</c>.
        /// </value>
        public bool CanShowIDL { get; set; }
        /// <summary>
        /// Gets or sets the spa route changed yes.
        /// </summary>
        /// <value>
        /// The spa route changed yes.
        /// </value>
        public bool? SpaRouteChangedYes { get; set; }
        /// <summary>
        /// Gets or sets the PLK identifier route change reason.
        /// </summary>
        /// <value>
        /// The PLK identifier route change reason.
        /// </value>
        public string PlkIdRouteChangeReason { get; set; }
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }
        /// <summary>
        /// Gets or sets the security level.
        /// </summary>
        /// <value>
        /// The security level.
        /// </value>
        public string SecurityLevel { get; set; }

        /// <summary>
        /// Gets or sets the vessel preview.
        /// </summary>
        /// <value>
        /// The vessel preview.
        /// </value>
        public VesselPreviewViewModel vesselPreview { get; set; }

        /// <summary>
        /// Gets or sets the sea passage breaks.
        /// </summary>
        /// <value>
        /// The sea passage breaks.
        /// </value>
        public List<SeaPassageBreakViewModel> SeaPassageBreaks { get; set; }

        /// <summary>
        /// Gets or sets the noon report nvaigation.
        /// </summary>
        /// <value>
        /// The noon report nvaigation.
        /// </value>

        public NoonReportNavigationViewModel NoonReportNvaigation { get; set; }

        /// <summary>
        /// Gets or sets the noon report weather.
        /// </summary>
        /// <value>
        /// The noon report weather.
        /// </value>

        public NoonReportWeatherViewModel NoonReportWeather { get; set; }


        /// <summary>
        /// Gets or sets the noon report24 weather.
        /// </summary>
        /// <value>
        /// The noon report24 weather.
        /// </value>

        public List<NoonReport24HweatherViewModel> NoonReport24Weather { get; set; }

        /// <summary>
        /// Gets or sets the noon report vessel draft.
        /// </summary>
        /// <value>
        /// The noon report vessel draft.
        /// </value>

        public NoonReportVesselDraftViewModel NoonReportVesselDraft { get; set; }

        /// <summary>
        /// Gets or sets the noon report slip and power output.
        /// </summary>
        /// <value>
        /// The noon report slip and power output.
        /// </value>

        public NoonReportSlipAndPowerOutputViewModel NoonReportSlipAndPowerOutput { get; set; }

        /// <summary>
        /// Gets or sets the noon report consumption capacity rob.
        /// </summary>
        /// <value>
        /// The noon report consumption capacity rob.
        /// </value>

        public NoonReportConsumptionCapacityRobViewModel NoonReportConsumptionCapacityRob { get; set; }

        /// <summary>
        /// Gets or sets the noon report fresh water lube oil.
        /// </summary>
        /// <value>
        /// The noon report fresh water lube oil.
        /// </value>

        public NoonReportFreshWaterLubeOilViewModel NoonReportFreshWaterLubeOil { get; set; }

        /// <summary>
        /// Gets or sets the voyage running hour list.
        /// </summary>
        /// <value>
        /// The voyage running hour list.
        /// </value>

        public List<VoyageRunningHourViewModel> VoyageRunningHourList { get; set; }

        /// <summary>
        /// Gets or sets the sea passage breaks.
        /// </summary>
        /// <value>
        /// The sea passage breaks.
        /// </value>
        //public List<SeaPassageBreakViewModel> SeaPassageBreaks { get; set; }

        /// <summary>
        /// Gets or sets the fuel rob.
        /// </summary>
        /// <value>
        /// The fuel rob.
        /// </value>

        public NoonReportFuelRobViewModel FuelRob { get; set; }

        /// <summary>
        /// Gets or sets the noon comments.
        /// </summary>
        /// <value>
        /// The noon comments.
        /// </value>

        public List<NoonReportCommentViewModel> NoonComments { get; set; }

        /// <summary>
        /// Gets or sets the spa is incomplete event.
        /// </summary>
        /// <value>
        /// The spa is incomplete event.
        /// </value>

        public bool? SPAIsIncompleteEvent { get; set; }

        /// <summary>
        /// Gets or sets the noon synopsis.
        /// </summary>
        /// <value>
        /// The noon synopsis.
        /// </value>

        public List<SeapassageNoonSynopsisViewModel> NoonSynopsis { get; set; }

        /// <summary>
        /// Gets or sets the event details.
        /// </summary>
        /// <value>
        /// The event details.
        /// </value>
        public EventXMLDetailsViewModel EventDetails { get; set; }

        /// <summary>
        /// Gets or sets the fresh water domestic.
        /// </summary>
        /// <value>
        /// The fresh water domestic.
        /// </value>
        public ROBDetailsViewModel FreshWaterDomestic { get; set; }

        /// <summary>
        /// Gets or sets the fresh water technical.
        /// </summary>
        /// <value>
        /// The fresh water technical.
        /// </value>
        public ROBDetailsViewModel FreshWaterTechnical { get; set; }

        /// <summary>
        /// Gets or sets the lube oil clo.
        /// </summary>
        /// <value>
        /// The lube oil clo.
        /// </value>
        public ROBDetailsViewModel LubeOilClo { get; set; }

        /// <summary>
        /// Gets or sets the lube oil crank case.
        /// </summary>
        /// <value>
        /// The lube oil crank case.
        /// </value>
        public ROBDetailsViewModel LubeOilCrankCase { get; set; }

        /// <summary>
        /// Gets or sets the lube oil aux.
        /// </summary>
        /// <value>
        /// The lube oil aux.
        /// </value>
        public ROBDetailsViewModel LubeOilAux { get; set; }

        /// <summary>
        /// Gets or sets the lube oil general.
        /// </summary>
        /// <value>
        /// The lube oil general.
        /// </value>
        public ROBDetailsViewModel LubeOilGeneral { get; set; }

        /// <summary>
        /// Gets or sets the waster rob sludge.
        /// </summary>
        /// <value>
        /// The waster rob sludge.
        /// </value>
        public ROBDetailsViewModel WasterRobSludge { get; set; }

        /// <summary>
        /// Gets or sets the waster rob bilge.
        /// </summary>
        /// <value>
        /// The waster rob bilge.
        /// </value>
        public ROBDetailsViewModel WasterRobBilge { get; set; }

        /// <summary>
        /// Gets or sets the waster rob slops.
        /// </summary>
        /// <value>
        /// The waster rob slops.
        /// </value>
        public ROBDetailsViewModel WasterRobSlops { get; set; }

        /// <summary>
        /// Gets or sets the waster rob sewage.
        /// </summary>
        /// <value>
        /// The waster rob sewage.
        /// </value>
        public ROBDetailsViewModel WasterRobSewage { get; set; }

        /// <summary>
        /// Gets or sets the fuel rob items.
        /// </summary>
        /// <value>
        /// The fuel rob items.
        /// </value>
        public List<FuelROBDetailsViewModel> FuelRobItems { get; set; }

        /// <summary>
        /// Gets or sets the weather24 hours label.
        /// </summary>
        /// <value>
        /// The weather24 hours label.
        /// </value>
        public string Weather24HoursLabel { get; set; }

        /// <summary>
        /// Gets or sets the is noon incomplete.
        /// </summary>
        /// <value>
        /// The is noon incomplete.
        /// </value>
        public bool? IsNoonIncomplete { get; set; }
    }
}
