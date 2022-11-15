using System;
using System.Collections.Generic;
using PWAFeaturesRnd.Models.Report.VoyageReporting;

namespace PWAFeaturesRnd.ViewModels.VoyageReporting
{
    /// <summary>
    /// The faop detail view model
    /// </summary>
    public class FaopDetailViewModel
    {
        /// <summary>
        /// Gets or sets the name of the vessel.
        /// </summary>
        /// <value>
        /// The name of the vessel.
        /// </value>
        public string VesselName { get; set; }
        /// <summary>
        /// Gets or sets the rob clo.
        /// </summary>
        /// <value>
        /// The rob clo.
        /// </value>
        public decimal? RobClo { get; set; }
        /// <summary>
        /// Gets or sets the rob domestic.
        /// </summary>
        /// <value>
        /// The rob domestic.
        /// </value>
        public decimal? RobDomestic { get; set; }
        /// <summary>
        /// Gets or sets the rob fo.
        /// </summary>
        /// <value>
        /// The rob fo.
        /// </value>
        public decimal? RobFo { get; set; }
        /// <summary>
        /// Gets or sets the rob go.
        /// </summary>
        /// <value>
        /// The rob go.
        /// </value>
        public decimal? RobGo { get; set; }
        /// <summary>
        /// Gets or sets the rob LNG.
        /// </summary>
        /// <value>
        /// The rob LNG.
        /// </value>
        public decimal? RobLng { get; set; }
        /// <summary>
        /// Gets or sets the rob lsfo.
        /// </summary>
        /// <value>
        /// The rob lsfo.
        /// </value>
        public decimal? RobLsfo { get; set; }
        /// <summary>
        /// Gets or sets the rob LNG cargo.
        /// </summary>
        /// <value>
        /// The rob LNG cargo.
        /// </value>
        public decimal? RobLngCargo { get; set; }
        /// <summary>
        /// Gets or sets the rob sewage.
        /// </summary>
        /// <value>
        /// The rob sewage.
        /// </value>
        public decimal? RobSewage { get; set; }
        /// <summary>
        /// Gets or sets the rob slops.
        /// </summary>
        /// <value>
        /// The rob slops.
        /// </value>
        public decimal? RobSlops { get; set; }
        /// <summary>
        /// Gets or sets the rob sludge.
        /// </summary>
        /// <value>
        /// The rob sludge.
        /// </value>
        public decimal? RobSludge { get; set; }
        /// <summary>
        /// Gets or sets the rob technical.
        /// </summary>
        /// <value>
        /// The rob technical.
        /// </value>
        public decimal? RobTechnical { get; set; }
        /// <summary>
        /// Gets or sets the spa ballast qty.
        /// </summary>
        /// <value>
        /// The spa ballast qty.
        /// </value>
        public string SpaBallastQty { get; set; }
        /// <summary>
        /// Gets or sets the spa cargo qty.
        /// </summary>
        /// <value>
        /// The spa cargo qty.
        /// </value>
        public string SpaCargoQty { get; set; }
        /// <summary>
        /// Gets or sets the spa date.
        /// </summary>
        /// <value>
        /// The spa date.
        /// </value>
        public string SpaDate { get; set; }
        /// <summary>
        /// Gets or sets the spa general lube oil rob.
        /// </summary>
        /// <value>
        /// The spa general lube oil rob.
        /// </value>
        public decimal? SpaGeneralLubeOilRob { get; set; }
        /// <summary>
        /// Gets or sets the spa identifier.
        /// </summary>
        /// <value>
        /// The spa identifier.
        /// </value>
        public string SpaId { get; set; }
        /// <summary>
        /// Gets or sets the spa reference identifier.
        /// </summary>
        /// <value>
        /// The spa reference identifier.
        /// </value>
        public string SpaReferenceId { get; set; }
        /// <summary>
        /// Gets or sets the spa reference information.
        /// </summary>
        /// <value>
        /// The spa reference information.
        /// </value>
        public string SpaReferenceInfo { get; set; }
        /// <summary>
        /// Gets or sets the spa reference information.
        /// </summary>
        /// <value>
        /// The spa reference information.
        /// </value>
        public string SpaRefInfo { get; set; }
        /// <summary>
        /// Gets or sets the spa route changed.
        /// </summary>
        /// <value>
        /// The spa route changed.
        /// </value>
        public string SpaRouteChanged { get; set; }
        /// <summary>
        /// Gets or sets the spa security.
        /// </summary>
        /// <value>
        /// The spa security.
        /// </value>
        public string SpaSecurity { get; set; }
        /// <summary>
        /// Gets or sets the tot rev2.
        /// </summary>
        /// <value>
        /// The tot rev2.
        /// </value>
        public string TotRev2 { get; set; }
        /// <summary>
        /// Gets or sets the rob do.
        /// </summary>
        /// <value>
        /// The rob do.
        /// </value>
        public decimal? RobDo { get; set; }
        /// <summary>
        /// Gets or sets the rob crank case.
        /// </summary>
        /// <value>
        /// The rob crank case.
        /// </value>
        public decimal? RobCrankCase { get; set; }
        /// <summary>
        /// Gets or sets the mean draft.
        /// </summary>
        /// <value>
        /// The mean draft.
        /// </value>
        public decimal? MeanDraft { get; set; }
        /// <summary>
        /// Gets or sets the rob bilge.
        /// </summary>
        /// <value>
        /// The rob bilge.
        /// </value>
        public decimal? RobBilge { get; set; }
        /// <summary>
        /// Gets or sets the aft.
        /// </summary>
        /// <value>
        /// The aft.
        /// </value>
        public float? Aft { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }
        /// <summary>
        /// Gets or sets the count fit.
        /// </summary>
        /// <value>
        /// The count fit.
        /// </value>
        public byte CountFit { get; set; }
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the draft mean.
        /// </summary>
        /// <value>
        /// The draft mean.
        /// </value>
        public string DraftMean { get; set; }
        /// <summary>
        /// Gets or sets the DWT.
        /// </summary>
        /// <value>
        /// The DWT.
        /// </value>
        public string Dwt { get; set; }
        /// <summary>
        /// Gets or sets the fore.
        /// </summary>
        /// <value>
        /// The fore.
        /// </value>
        public float? Fore { get; set; }
        /// <summary>
        /// Gets or sets the lat degree.
        /// </summary>
        /// <value>
        /// The lat degree.
        /// </value>
        public string LatDegree { get; set; }
        /// <summary>
        /// Gets or sets the lat direction.
        /// </summary>
        /// <value>
        /// The lat direction.
        /// </value>
        public string LatDirection { get; set; }
        /// <summary>
        /// Gets or sets the ves identifier.
        /// </summary>
        /// <value>
        /// The ves identifier.
        /// </value>
        public string VesId { get; set; }
        /// <summary>
        /// Gets or sets the lat minute.
        /// </summary>
        /// <value>
        /// The lat minute.
        /// </value>
        public string LatMinute { get; set; }
        /// <summary>
        /// Gets or sets the long direction.
        /// </summary>
        /// <value>
        /// The long direction.
        /// </value>
        public string LongDirection { get; set; }
        /// <summary>
        /// Gets or sets the long minute.
        /// </summary>
        /// <value>
        /// The long minute.
        /// </value>
        public string LongMinute { get; set; }
        /// <summary>
        /// Gets or sets the mid.
        /// </summary>
        /// <value>
        /// The mid.
        /// </value>
        public decimal? Mid { get; set; }
        /// <summary>
        /// Gets or sets the mun identifier unit.
        /// </summary>
        /// <value>
        /// The mun identifier unit.
        /// </value>
        public string MunIdUnit { get; set; }
        /// <summary>
        /// Gets or sets the PLK identifier route change reason.
        /// </summary>
        /// <value>
        /// The PLK identifier route change reason.
        /// </value>
        public string PlkIdRouteChangeReason { get; set; }
        /// <summary>
        /// Gets or sets the position identifier.
        /// </summary>
        /// <value>
        /// The position identifier.
        /// </value>
        public string PosId { get; set; }
        /// <summary>
        /// Gets or sets the poslist.
        /// </summary>
        /// <value>
        /// The poslist.
        /// </value>
        public Poslist Poslist { get; set; }
        /// <summary>
        /// Gets or sets the type of the report.
        /// </summary>
        /// <value>
        /// The type of the report.
        /// </value>
        public string ReportType { get; set; }
        /// <summary>
        /// Gets or sets the rob aux lube oil.
        /// </summary>
        /// <value>
        /// The rob aux lube oil.
        /// </value>
        public decimal? RobAuxLubeOil { get; set; }
        /// <summary>
        /// Gets or sets the long degree.
        /// </summary>
        /// <value>
        /// The long degree.
        /// </value>
        public string LongDegree { get; set; }
        /// <summary>
        /// Gets or sets the event date for validation.
        /// </summary>
        /// <value>
        /// The event date for validation.
        /// </value>
        public DateTime? EventDateForValidation { get; set; }
        /// <summary>
        /// Gets or sets the ves maximum draft.
        /// </summary>
        /// <value>
        /// The ves maximum draft.
        /// </value>
        public float? VesMaxDraft { get; set; }
        /// <summary>
        /// Gets or sets the rob previous sludge.
        /// </summary>
        /// <value>
        /// The rob previous sludge.
        /// </value>
        public float? RobPrevSludge { get; set; }
        /// <summary>
        /// Gets or sets the rob previous bilge.
        /// </summary>
        /// <value>
        /// The rob previous bilge.
        /// </value>
        public float? RobPrevBilge { get; set; }
        /// <summary>
        /// Gets or sets the rob previous slops.
        /// </summary>
        /// <value>
        /// The rob previous slops.
        /// </value>
        public float? RobPrevSlops { get; set; }
        /// <summary>
        /// Gets or sets the rob previous sewage.
        /// </summary>
        /// <value>
        /// The rob previous sewage.
        /// </value>
        public float? RobPrevSewage { get; set; }
        /// <summary>
        /// Gets or sets the rob previous aux lube oil.
        /// </summary>
        /// <value>
        /// The rob previous aux lube oil.
        /// </value>
        public float? RobPrevAuxLubeOil { get; set; }
        /// <summary>
        /// Gets or sets the rob previous clo.
        /// </summary>
        /// <value>
        /// The rob previous clo.
        /// </value>
        public float? RobPrevClo { get; set; }
        /// <summary>
        /// Gets or sets the rob previous crank case.
        /// </summary>
        /// <value>
        /// The rob previous crank case.
        /// </value>
        public float? RobPrevCrankCase { get; set; }
        /// <summary>
        /// Gets or sets the rob previous general lube oil.
        /// </summary>
        /// <value>
        /// The rob previous general lube oil.
        /// </value>
        public float? RobPrevGeneralLubeOil { get; set; }
        /// <summary>
        /// Gets or sets the rob previous do.
        /// </summary>
        /// <value>
        /// The rob previous do.
        /// </value>
        public float? RobPrevDo { get; set; }
        /// <summary>
        /// Gets or sets the rob previous domestic.
        /// </summary>
        /// <value>
        /// The rob previous domestic.
        /// </value>
        public float? RobPrevDomestic { get; set; }
        /// <summary>
        /// Gets or sets the rob previous fo.
        /// </summary>
        /// <value>
        /// The rob previous fo.
        /// </value>
        public float? RobPrevFo { get; set; }
        /// <summary>
        /// Gets or sets the rob previous go.
        /// </summary>
        /// <value>
        /// The rob previous go.
        /// </value>
        public float? RobPrevGo { get; set; }
        /// <summary>
        /// Gets or sets the rob previous LNG.
        /// </summary>
        /// <value>
        /// The rob previous LNG.
        /// </value>
        public float? RobPrevLng { get; set; }
        /// <summary>
        /// Gets or sets the previous faop ballast qty.
        /// </summary>
        /// <value>
        /// The previous faop ballast qty.
        /// </value>
        public float? PrevFaopBallastQty { get; set; }
        /// <summary>
        /// Gets or sets the rob previous LNG cargo.
        /// </summary>
        /// <value>
        /// The rob previous LNG cargo.
        /// </value>
        public float? RobPrevLngCargo { get; set; }
        /// <summary>
        /// Gets or sets the rob previous lsfo.
        /// </summary>
        /// <value>
        /// The rob previous lsfo.
        /// </value>
        public float? RobPrevLsfo { get; set; }
        /// <summary>
        /// Gets or sets the rob previous technical.
        /// </summary>
        /// <value>
        /// The rob previous technical.
        /// </value>
        public float? RobPrevTechnical { get; set; }


        /// <summary>
        /// Gets or sets the go capacity.
        /// </summary>
        /// <value>
        /// The go capacity.
        /// </value>
        public float? GoCapacity { get; set; }
        /// <summary>
        /// Gets or sets the do capacity.
        /// </summary>
        /// <value>
        /// The do capacity.
        /// </value>
        public float? DoCapacity { get; set; }
        /// <summary>
        /// Gets or sets the LNG capacity.
        /// </summary>
        /// <value>
        /// The LNG capacity.
        /// </value>
        public float? LngCapacity { get; set; }
        /// <summary>
        /// Gets or sets the fw domestic capacity.
        /// </summary>
        /// <value>
        /// The fw domestic capacity.
        /// </value>
        public float? FWDomesticCapacity { get; set; }
        /// <summary>
        /// Gets or sets the fw technical capacity.
        /// </summary>
        /// <value>
        /// The fw technical capacity.
        /// </value>
        public float? FWTechnicalCapacity { get; set; }
        /// <summary>
        /// Gets or sets the aux luboil capacity.
        /// </summary>
        /// <value>
        /// The aux luboil capacity.
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
        /// Gets or sets the lsfo capacity.
        /// </summary>
        /// <value>
        /// The lsfo capacity.
        /// </value>
        public float? LsfoCapacity { get; set; }
        /// <summary>
        /// Gets or sets the ves count fit.
        /// </summary>
        /// <value>
        /// The ves count fit.
        /// </value>
        public int VesCountFit { get; set; }
        /// <summary>
        /// Gets or sets the slop capacity.
        /// </summary>
        /// <value>
        /// The slop capacity.
        /// </value>
        public float? SlopCapacity { get; set; }
        /// <summary>
        /// Gets or sets the bilge capacity.
        /// </summary>
        /// <value>
        /// The bilge capacity.
        /// </value>
        public float? BilgeCapacity { get; set; }
        /// <summary>
        /// Gets or sets the sewage capacity.
        /// </summary>
        /// <value>
        /// The sewage capacity.
        /// </value>
        public float? SewageCapacity { get; set; }
        /// <summary>
        /// Gets or sets the sludge rob.
        /// </summary>
        /// <value>
        /// The sludge rob.
        /// </value>
        public float? SludgeRob { get; set; }
        /// <summary>
        /// Gets or sets the slop rob.
        /// </summary>
        /// <value>
        /// The slop rob.
        /// </value>
        public float? SlopRob { get; set; }
        /// <summary>
        /// Gets or sets the sewage rob.
        /// </summary>
        /// <value>
        /// The sewage rob.
        /// </value>
        public float? SewageRob { get; set; }
        /// <summary>
        /// Gets or sets the bilge rob.
        /// </summary>
        /// <value>
        /// The bilge rob.
        /// </value>
        public float? BilgeRob { get; set; }
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
        /// Gets or sets the sludge capacity.
        /// </summary>
        /// <value>
        /// The sludge capacity.
        /// </value>
        public float? SludgeCapacity { get; set; }
        /// <summary>
        /// Gets or sets the mid draft.
        /// </summary>
        /// <value>
        /// The mid draft.
        /// </value>
        public float? MidDraft { get; set; }
        /// <summary>
        /// Gets or sets the fo capacity.
        /// </summary>
        /// <value>
        /// The fo capacity.
        /// </value>
        public float? FoCapacity { get; set; }
        /// <summary>
        /// Gets or sets the NCR.
        /// </summary>
        /// <value>
        /// The NCR.
        /// </value>
        public decimal? NCR { get; set; }
        /// <summary>
        /// Gets or sets the distance to go.
        /// </summary>
        /// <value>
        /// The distance to go.
        /// </value>
        public int DistanceToGo { get; set; }
        /// <summary>
        /// Gets or sets the total revolution.
        /// </summary>
        /// <value>
        /// The total revolution.
        /// </value>
        public int TotalRevolution { get; set; }
        /// <summary>
        /// Gets or sets the fo rob.
        /// </summary>
        /// <value>
        /// The fo rob.
        /// </value>
        public float? FoRob { get; set; }
        /// <summary>
        /// Gets or sets the lsfo rob.
        /// </summary>
        /// <value>
        /// The lsfo rob.
        /// </value>
        public float? LsfoRob { get; set; }
        /// <summary>
        /// Gets or sets the do rob.
        /// </summary>
        /// <value>
        /// The do rob.
        /// </value>
        public float? DoRob { get; set; }
        /// <summary>
        /// Gets or sets the go rob.
        /// </summary>
        /// <value>
        /// The go rob.
        /// </value>
        public float? GoRob { get; set; }
        /// <summary>
        /// Gets or sets the NCR revolution.
        /// </summary>
        /// <value>
        /// The NCR revolution.
        /// </value>
        public decimal? NCRRevolution { get; set; }
        /// <summary>
        /// Gets or sets the LNG rob.
        /// </summary>
        /// <value>
        /// The LNG rob.
        /// </value>
        public float? LngRob { get; set; }
        /// <summary>
        /// Gets or sets the fw tech.
        /// </summary>
        /// <value>
        /// The fw tech.
        /// </value>
        public float? FwTech { get; set; }
        /// <summary>
        /// Gets or sets the cly lube oil rob.
        /// </summary>
        /// <value>
        /// The cly lube oil rob.
        /// </value>
        public float? ClyLubeOilRob { get; set; }
        /// <summary>
        /// Gets or sets the crank case rob.
        /// </summary>
        /// <value>
        /// The crank case rob.
        /// </value>
        public float? CrankCaseRob { get; set; }
        /// <summary>
        /// Gets or sets the aux lub oil rob.
        /// </summary>
        /// <value>
        /// The aux lub oil rob.
        /// </value>
        public float? AuxLubOilRob { get; set; }
        /// <summary>
        /// Gets or sets the general lub oil rob.
        /// </summary>
        /// <value>
        /// The general lub oil rob.
        /// </value>
        public float? GeneralLubOilRob { get; set; }
        /// <summary>
        /// Gets or sets the voyage running hour list.
        /// </summary>
        /// <value>
        /// The voyage running hour list.
        /// </value>
        public List<VoyageRunningHourViewModel> VoyageRunningHourList { get; set; }
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
        /// Gets or sets the fw rob.
        /// </summary>
        /// <value>
        /// The fw rob.
        /// </value>
        public float? FwRob { get; set; }
        /// <summary>
        /// Gets or sets the ves property pitch.
        /// </summary>
        /// <value>
        /// The ves property pitch.
        /// </value>
        public float? VesPropPitch { get; set; }
        /// <summary>
        /// Gets or sets the LNG cargo capacity.
        /// </summary>
        /// <value>
        /// The LNG cargo capacity.
        /// </value>
        public float? LngCargoCapacity { get; set; }
        /// <summary>
        /// Gets or sets the ballast tank capacity.
        /// </summary>
        /// <value>
        /// The ballast tank capacity.
        /// </value>
        public decimal? BallastTankCapacity { get; set; }
        /// <summary>
        /// Gets or sets the is spa route changed.
        /// </summary>
        /// <value>
        /// The is spa route changed.
        /// </value>
        public bool? IsSpaRouteChanged { get; set; }
        /// <summary>
        /// Gets or sets the fuel list.
        /// </summary>
        /// <value>
        /// The fuel list.
        /// </value>
        public List<FullAwayOnPassageROBViewModel> FuelList { get; set; }
        /// <summary>
        /// Gets or sets the waste list.
        /// </summary>
        /// <value>
        /// The waste list.
        /// </value>
        public List<FullAwayOnPassageROBViewModel> WasteList { get; set; }
        /// <summary>
        /// Gets or sets the lube oil list.
        /// </summary>
        /// <value>
        /// The lube oil list.
        /// </value>
        public List<FullAwayOnPassageROBViewModel> LubeOilList { get; set; }
        /// <summary>
        /// Gets or sets the fresh water list.
        /// </summary>
        /// <value>
        /// The fresh water list.
        /// </value>
        public List<FullAwayOnPassageROBViewModel> FreshWaterList { get; set; }
        /// <summary>
        /// Gets or sets the spa cargo ballast list.
        /// </summary>
        /// <value>
        /// The spa cargo ballast list.
        /// </value>
        public List<FullAwayOnPassageROBViewModel> SpaCargoBallastList { get; set; }
    }
}
