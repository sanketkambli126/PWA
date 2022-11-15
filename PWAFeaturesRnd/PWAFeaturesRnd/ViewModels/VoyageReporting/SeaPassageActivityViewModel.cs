using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.VoyageReporting;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// Sea Passage Activity View Model
    /// </summary>
    public class SeaPassageActivityViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the activity details.
        /// </summary>
        /// <value>
        /// The activity details.
        /// </value>
        public List<SeaPassageReportDetailsViewModel> ActivityDetails { get; set; }

        /// <summary>
        /// Gets or sets the speed statistics.
        /// </summary>
        /// <value>
        /// The speed statistics.
        /// </value>
        public SeaPassageHeaderStatisticViewModel SpeedStatistics { get; set; }

        /// <summary>
        /// Gets or sets the bar chart stats.
        /// </summary>
        /// <value>
        /// The bar chart stats.
        /// </value>
        public SeaPassageFuelConsumptionDetailsViewModel BarChartStats { get; set; }

        /// <summary>
        /// Gets or sets the total time.
        /// </summary>
        /// <value>
        /// The total time.
        /// </value>
        public string TotalTime { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        /// <value>
        /// The total distance.
        /// </value>
        public float? TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the total fo.
        /// </summary>
        /// <value>
        /// The total fo.
        /// </value>
        public float? TotalFo { get; set; }

        /// <summary>
        /// Gets or sets the total lsfo.
        /// </summary>
        /// <value>
        /// The total lsfo.
        /// </value>
        public float? TotalLsfo { get; set; }
        /// <summary>
        /// Gets or sets the total do.
        /// </summary>
        /// <value>
        /// The total do.
        /// </value>
        public float? TotalDo { get; set; }
        /// <summary>
        /// Gets or sets the total go.
        /// </summary>
        /// <value>
        /// The total go.
        /// </value>
        public float? TotalGo { get; set; }
        /// <summary>
        /// Gets or sets the total LNG.
        /// </summary>
        /// <value>
        /// The total LNG.
        /// </value>
        public float? TotalLNG { get; set; }

        /// <summary>
        /// Gets or sets the total fresh water consumption domestic.
        /// </summary>
        /// <value>
        /// The total fresh water consumption domestic.
        /// </value>
        public float? TotalFreshWaterConsumptionDomestic { get; set; }
        /// <summary>
        /// Gets or sets the total fresh water consumption technial.
        /// </summary>
        /// <value>
        /// The total fresh water consumption technial.
        /// </value>
        public float? TotalFreshWaterConsumptionTechnial { get; set; }
        /// <summary>
        /// Gets or sets the total lube oil consumption clo.
        /// </summary>
        /// <value>
        /// The total lube oil consumption clo.
        /// </value>
        public float? TotalLubeOilConsumptionClo { get; set; }
        /// <summary>
        /// Gets or sets the total lube oil consumption crank case.
        /// </summary>
        /// <value>
        /// The total lube oil consumption crank case.
        /// </value>
        public float? TotalLubeOilConsumptionCrankCase { get; set; }
        /// <summary>
        /// Gets or sets the total lube oil consumption aux.
        /// </summary>
        /// <value>
        /// The total lube oil consumption aux.
        /// </value>
        public float? TotalLubeOilConsumptionAux { get; set; }
        /// <summary>
        /// Gets or sets the total general lub oil consumption.
        /// </summary>
        /// <value>
        /// The total general lub oil consumption.
        /// </value>
        public float? TotalGeneralLubOilConsumption { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is vessel loaded flag.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is vessel loaded flag; otherwise, <c>false</c>.
        /// </value>
        public bool IsVesselLoadedFlag { get; set; }

        #endregion


        #region Constrtuctor

        /// <summary>
        /// Initializes a new instance of the <see cref="SeaPassageActivityViewModel" /> class.
        /// </summary>
        public SeaPassageActivityViewModel()
        {

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the view model property.
        /// </summary>
        /// <param name="Entity">The entity.</param>
        /// <returns></returns>
        public SeaPassageActivityViewModel SetViewModelProperty(SeaPassageActivity Entity)
        {
            SeaPassageActivityViewModel response = new SeaPassageActivityViewModel();
            response.TotalTime = Entity.TotalTime;
            response.TotalDistance = Entity.TotalDistance;
            response.TotalFo = Entity.TotalFo;
            response.TotalLsfo = Entity.TotalLsfo;
            response.TotalDo = Entity.TotalDo;
            response.TotalGo = Entity.TotalGo;

            response.TotalLNG = 0;
            response.TotalFreshWaterConsumptionDomestic = 0;
            response.TotalFreshWaterConsumptionTechnial = 0;
            response.TotalLubeOilConsumptionClo = 0;
            response.TotalLubeOilConsumptionCrankCase = 0;
            response.TotalLubeOilConsumptionAux = 0;
            response.TotalGeneralLubOilConsumption = 0;
            return response;
        }

        #endregion
    }
}
