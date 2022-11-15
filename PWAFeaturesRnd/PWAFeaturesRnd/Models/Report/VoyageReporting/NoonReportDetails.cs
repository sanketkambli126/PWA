using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    public class NoonReportDetails
    {
        /// <summary>
        /// Gets or sets the noon report nvaigation.
        /// </summary>
        /// <value>
        /// The noon report nvaigation.
        /// </value>

        public NoonReportNavigation NoonReportNvaigation { get; set; }

        /// <summary>
        /// Gets or sets the noon report weather.
        /// </summary>
        /// <value>
        /// The noon report weather.
        /// </value>

        public NoonReportWeather NoonReportWeather { get; set; }


        /// <summary>
        /// Gets or sets the noon report24 weather.
        /// </summary>
        /// <value>
        /// The noon report24 weather.
        /// </value>

        public List<NoonReport24Hweather> NoonReport24Weather { get; set; }

        /// <summary>
        /// Gets or sets the noon report vessel draft.
        /// </summary>
        /// <value>
        /// The noon report vessel draft.
        /// </value>

        public NoonReportVesselDraft NoonReportVesselDraft { get; set; }

        /// <summary>
        /// Gets or sets the noon report slip and power output.
        /// </summary>
        /// <value>
        /// The noon report slip and power output.
        /// </value>

        public NoonReportSlipAndPowerOutput NoonReportSlipAndPowerOutput { get; set; }

        /// <summary>
        /// Gets or sets the noon report consumption capacity rob.
        /// </summary>
        /// <value>
        /// The noon report consumption capacity rob.
        /// </value>

        public NoonReportConsumptionCapacityRob NoonReportConsumptionCapacityRob { get; set; }

        /// <summary>
        /// Gets or sets the noon report fresh water lube oil.
        /// </summary>
        /// <value>
        /// The noon report fresh water lube oil.
        /// </value>

        public NoonReportFreshWaterLubeOil NoonReportFreshWaterLubeOil { get; set; }

        /// <summary>
        /// Gets or sets the voyage running hour list.
        /// </summary>
        /// <value>
        /// The voyage running hour list.
        /// </value>

        public List<VoyageRunningHour> VoyageRunningHourList { get; set; }

        /// <summary>
        /// Gets or sets the sea passage breaks.
        /// </summary>
        /// <value>
        /// The sea passage breaks.
        /// </value>

        public List<SeaPassageBreak> SeaPassageBreaks { get; set; }

        /// <summary>
        /// Gets or sets the fuel rob.
        /// </summary>
        /// <value>
        /// The fuel rob.
        /// </value>

        public NoonReportFuelRob FuelRob { get; set; }

        /// <summary>
        /// Gets or sets the noon comments.
        /// </summary>
        /// <value>
        /// The noon comments.
        /// </value>

        public List<NoonReportComment> NoonComments { get; set; }

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

        public List<SeapassageNoonSynopsis> NoonSynopsis { get; set; }

        /// <summary>
        /// Gets or sets the event details.
        /// </summary>
        /// <value>
        /// The event details.
        /// </value>
        public EventXMLDetails EventDetails { get; set; }
    }
}
