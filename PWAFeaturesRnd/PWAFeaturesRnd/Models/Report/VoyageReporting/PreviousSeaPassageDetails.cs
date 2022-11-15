using System;
using System.Collections.Generic;

namespace PWAFeaturesRnd.Models.Report.VoyageReporting
{
    public class PreviousSeaPassageDetails
    {
        /// <summary>
        /// Gets or sets the dist on sea passage.
        /// </summary>
        /// <value>
        /// The dist on sea passage.
        /// </value>

        public float? DistanceTravelled { get; set; }

        /// <summary>
        /// Gets or sets the charter speed.
        /// </summary>
        /// <value>
        /// The charter speed.
        /// </value>

        public float? CharterSpeed { get; set; }

        /// <summary>
        /// Gets or sets the total distance.
        /// </summary>
        /// <value>
        /// The total distance.
        /// </value>

        public float? TotalDistance { get; set; }

        /// <summary>
        /// Gets or sets the distance to go.
        /// </summary>
        /// <value>
        /// The distance to go.
        /// </value>

        public float? DistanceToGo { get; set; }

        /// <summary>
        /// Gets or sets the cly lube oil rob.
        /// </summary>
        /// <value>
        /// The cly lube oil rob.
        /// </value>

        public decimal? ClyLubeOilRob { get; set; }


        /// <summary>
        /// Gets or sets the crank case rob.
        /// </summary>
        /// <value>
        /// The crank case rob.
        /// </value>

        public decimal? CrankCaseRob { get; set; }

        /// <summary>
        /// Gets or sets the aux lub oil rob.
        /// </summary>
        /// <value>
        /// The aux lub oil rob.
        /// </value>

        public decimal? AuxLubOilRob { get; set; }
        /// <summary>
        /// Gets or sets the general lub oil rob.
        /// </summary>
        /// <value>
        /// The general lub oil rob.
        /// </value>

        public decimal? GeneralLubOilRob { get; set; }

        /// <summary>
        /// Gets or sets the sludge rob.
        /// </summary>
        /// <value>
        /// The sludge rob.
        /// </value>

        public decimal? SludgeRob { get; set; }

        /// <summary>
        /// Gets or sets the slop rob.
        /// </summary>
        /// <value>
        /// The slop rob.
        /// </value>

        public decimal? SlopRob { get; set; }

        /// <summary>
        /// Gets or sets the bilge rob.
        /// </summary>
        /// <value>
        /// The bilge rob.
        /// </value>

        public decimal? BilgeRob { get; set; }

        /// <summary>
        /// Gets or sets the sewage rob.
        /// </summary>
        /// <value>
        /// The sewage rob.
        /// </value>

        public decimal? SewageRob { get; set; }

        /// <summary>
        /// Gets or sets the fw rob.
        /// </summary>
        /// <value>
        /// The fw rob.
        /// </value>

        public decimal? FwRob { get; set; }


        /// <summary>
        /// Gets or sets the fw tech.
        /// </summary>
        /// <value>
        /// The fw tech.
        /// </value>

        public float? FwTech { get; set; }

        /// <summary>
        /// Gets or sets the total rev.
        /// </summary>
        /// <value>
        /// The total rev.
        /// </value>

        public float? TotalRev { get; set; }

        /// <summary>
        /// Gets or sets the voyage running hour figures.
        /// </summary>
        /// <value>
        /// The voyage running hour figures.
        /// </value>

        public List<VoyageRunningHour> VoyageRunningHourFigures { get; set; }

        /// <summary>
        /// Gets or sets the voyage rob.
        /// </summary>
        /// <value>
        /// The voyage rob.
        /// </value>

        public List<VoyageRob> VoyageRob { get; set; }

        /// <summary>
        /// Gets or sets the spa date.
        /// </summary>
        /// <value>
        /// The spa date.
        /// </value>

        public DateTime? SpaDate { get; set; }

        /// <summary>
        /// Gets or sets the ves propeller pitch.
        /// </summary>
        /// <value>
        /// The ves propeller pitch.
        /// </value>

        public float? VesPropellerPitch { get; set; }

        /// <summary>
        /// Gets or sets the atmospheric pressure.
        /// </summary>
        /// <value>
        /// The atmospheric pressure.
        /// </value>

        public decimal? AtmosphericPressure { get; set; }

        /// <summary>
        /// Gets or sets the time on passage.
        /// </summary>
        /// <value>
        /// The time on passage.
        /// </value>

        public string TimeOnPassage { get; set; }

        /// <summary>
        /// Gets or sets the ves count fit.
        /// </summary>
        /// <value>
        /// The ves count fit.
        /// </value>

        public int VesCountFit { get; set; }


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
        /// Gets or sets the go.
        /// </summary>
        /// <value>
        /// The go.
        /// </value>

        public float? Go { get; set; }

        /// <summary>
        /// Gets or sets the do.
        /// </summary>
        /// <value>
        /// The do.
        /// </value>

        public float? Do { get; set; }

        /// <summary>
        /// Gets or sets the LNG.
        /// </summary>
        /// <value>
        /// The LNG.
        /// </value>

        public decimal? Lng { get; set; }
        /// <summary>
        /// Gets or sets the LNG cargo.
        /// </summary>
        /// <value>
        /// The LNG cargo.
        /// </value>

        public decimal? LngCargo { get; set; }

        /// <summary>
        /// Gets or sets the fo.
        /// </summary>
        /// <value>
        /// The fo.
        /// </value>

        public float? FoCapacity { get; set; }

        /// <summary>
        /// Gets or sets the lsfo.
        /// </summary>
        /// <value>
        /// The lsfo.
        /// </value>

        public float? LsfoCapacity { get; set; }

        /// <summary>
        /// Gets or sets the go.
        /// </summary>
        /// <value>
        /// The go.
        /// </value>

        public float? GoCapacity { get; set; }

        /// <summary>
        /// Gets or sets the do.
        /// </summary>
        /// <value>
        /// The do.
        /// </value>

        public float? DoCapacity { get; set; }

        /// <summary>
        /// Gets or sets the LNG.
        /// </summary>
        /// <value>
        /// The LNG.
        /// </value>

        public float? LngCapacity { get; set; }

        /// <summary>
        /// Gets or sets the LNG cargo capacity.
        /// </summary>
        /// <value>
        /// The LNG cargo capacity.
        /// </value>

        public float? LngCargoCapacity { get; set; }

        /// <summary>
        /// Gets or sets the bilge capacity.
        /// </summary>
        /// <value>
        /// The bilge capacity.
        /// </value>

        public float? BilgeCapacity { get; set; }

        /// <summary>
        /// Gets or sets the sludge capacity.
        /// </summary>
        /// <value>
        /// The sludge capacity.
        /// </value>

        public float? SludgeCapacity { get; set; }

        /// <summary>
        /// Gets or sets the slop capacity.
        /// </summary>
        /// <value>
        /// The slop capacity.
        /// </value>

        public float? SlopCapacity { get; set; }

        /// <summary>
        /// Gets or sets the sewage capacity.
        /// </summary>
        /// <value>
        /// The sewage capacity.
        /// </value>

        public float? SewageCapacity { get; set; }

        /// <summary>
        /// Gets or sets the bunker fresh water capacity.
        /// </summary>
        /// <value>
        /// The bunker fresh water capacity.
        /// </value>

        public float? FWDomesticCapacity { get; set; }

        /// <summary>
        /// Gets or sets the bunker fw technical capacity.
        /// </summary>
        /// <value>
        /// The bunker fw technical capacity.
        /// </value>

        public float? FWTechnicalCapacity { get; set; }

        /// <summary>
        /// Gets or sets the lub oil capacity.
        /// </summary>
        /// <value>
        /// The lub oil capacity.
        /// </value>

        public float? AuxLuboilCapacity { get; set; }

        /// <summary>
        /// Gets or sets the clo luboil capacity.
        /// </summary>
        /// <value>
        /// The clo luboil capacity.
        /// </value>

        public float? CLOLuboilCapacity { get; set; }

        /// <summary>
        /// Gets or sets the crank luboil capacity.
        /// </summary>
        /// <value>
        /// The crank luboil capacity.
        /// </value>

        public float? CrankLuboilCapacity { get; set; }

        /// <summary>
        /// Gets or sets the general luboil capacity.
        /// </summary>
        /// <value>
        /// The general luboil capacity.
        /// </value>

        public float? GeneralLuboilCapacity { get; set; }

        /// <summary>
        /// Gets or sets the dist by engine.
        /// </summary>
        /// <value>
        /// The dist by engine.
        /// </value>

        public decimal? DistByEngine { get; set; }

        /// <summary>
        /// Gets or sets the ves minimum DRFT.
        /// </summary>
        /// <value>
        /// The ves minimum DRFT.
        /// </value>

        public double? VesMinDrft { get; set; }

        /// <summary>
        /// Gets or sets the MCR.
        /// </summary>
        /// <value>
        /// The MCR.
        /// </value>

        public decimal? MCR { get; set; }

        /// <summary>
        /// Gets or sets the MCR revolution.
        /// </summary>
        /// <value>
        /// The MCR revolution.
        /// </value>


        public decimal? MCRRevolution { get; set; }

        /// <summary>
        /// Gets or sets the NCR.
        /// </summary>
        /// <value>
        /// The NCR.
        /// </value>

        public decimal? NCR { get; set; }

        /// <summary>
        /// Gets or sets the NCR revolution.
        /// </summary>
        /// <value>
        /// The NCR revolution.
        /// </value>

        public decimal? NCRRevolution { get; set; }

        /// <summary>
        /// Gets or sets the electrical load.
        /// </summary>
        /// <value>
        /// The electrical load.
        /// </value>

        public decimal? ElectricalLoad { get; set; }

        /// <summary>
        /// Gets or sets the total time sailed.
        /// </summary>
        /// <value>
        /// The total time sailed.
        /// </value>

        public string TotalTimeSailed { get; set; }

        /// <summary>
        /// Gets or sets the total time sailed d.
        /// </summary>
        /// <value>
        /// The total time sailed d.
        /// </value>

        public float? TotalTimeSailedD { get; set; }

        /// <summary>
        /// Gets or sets the fo sulphur.
        /// </summary>
        /// <value>
        /// The fo sulphur.
        /// </value>

        public float? FoSulphur { get; set; }

        /// <summary>
        /// Gets or sets the go sulphur.
        /// </summary>
        /// <value>
        /// The go sulphur.
        /// </value>

        public float? GoSulphur { get; set; }

        /// <summary>
        /// Gets or sets the do sulphur.
        /// </summary>
        /// <value>
        /// The do sulphur.
        /// </value>

        public float? DoSulphur { get; set; }

        /// <summary>
        /// Gets or sets the lsfo sulphur.
        /// </summary>
        /// <value>
        /// The lsfo sulphur.
        /// </value>

        public float? LsfoSulphur { get; set; }

        /// <summary>
        /// Gets or sets the forward draft.
        /// </summary>
        /// <value>
        /// The forward draft.
        /// </value>

        public float? FwdDraft { get; set; }

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
        /// Gets or sets the eta date.
        /// </summary>
        /// <value>
        /// The eta date.
        /// </value>

        public DateTime? EtaDate { get; set; }

    }
}
