using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Report.Crew;
using PWAFeaturesRnd.Models.Report.Dashboard;
using PWAFeaturesRnd.Models.Report.Finance;
using PWAFeaturesRnd.Models.Report.HazardousOccurrences;
using PWAFeaturesRnd.Models.Report.InspectionManager;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.PlannedMaintenance;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.Models.Report.VoyageReporting;
using PWAFeaturesRnd.ViewModels.Certificate;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Crew;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.Defect;
using PWAFeaturesRnd.ViewModels.Enviroment;
using PWAFeaturesRnd.ViewModels.Finance;
using PWAFeaturesRnd.ViewModels.HazardousOccurrences;
using PWAFeaturesRnd.ViewModels.HazOcc;
using PWAFeaturesRnd.ViewModels.Inspection;
using PWAFeaturesRnd.ViewModels.JSA;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.PlannedMaintenance;
using PWAFeaturesRnd.ViewModels.PurchaseOrder;
using PWAFeaturesRnd.ViewModels.Sentinel;
using PWAFeaturesRnd.ViewModels.Shared;
using PWAFeaturesRnd.ViewModels.VesselManagement;
using PWAFeaturesRnd.ViewModels.VoyageReporting;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// Dashboard Controller
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    public class DashboardController : AuthenticatedController
    {
        #region Private Properties

        /// <summary>
        /// The client
        /// </summary>
        private readonly MarineClient _marineClient;

        /// <summary>
        /// The vessel routing client
        /// </summary>
        private readonly VesselRoutingClient _vesselRoutingClient;

        /// <summary>
        /// The purchasing client
        /// </summary>
        private readonly PurchasingClient _purchasingClient;

        /// <summary>
        /// The finance client
        /// </summary>
        private readonly FinanceClient _financeClient;

        /// <summary>
        /// The crew client
        /// </summary>
        private readonly CrewClient _crewClient;

        /// <summary>
        /// The technical dashboard client
        /// </summary>
        private readonly TechnicalDashboardClient _technicalDashboardClient;

        /// <summary>
        /// The document client
        /// </summary>
        private readonly DocumentClient _documentClient;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// The client
        /// </summary>
        private SharedClient _client;

        /// <summary>
        /// The client
        /// </summary>
        private NotificationClient _notificationClient;

        /// <summary>
        /// The marine WCF client
        /// </summary>
        private readonly MarineWCFClient _marineWCFClient;

        /// <summary>
        /// The HSEQ manager dashboard client
        /// </summary>
        private readonly HSEQManagerDashboardClient _hseqManagerDashboardClient;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController" /> class.
        /// </summary>
        /// <param name="crewClient">The crew client.</param>
        /// <param name="financeClient">The finance client.</param>
        /// <param name="parchasingClient">The parchasing client.</param>
        /// <param name="marineClient">The marine client.</param>
        /// <param name="client">The client.</param>
        /// <param name="technicalDashboardClient">The technical dashboard client.</param>
        /// <param name="documentClient">The client.</param>
        /// <param name="vesselRoutingClient">The vessel routing client.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="notificationClient">The notification client.</param>
        /// <param name="marineWCFClient"></param>
        /// <param name="hseqManagerDashboardClient"></param>
        public DashboardController(CrewClient crewClient, FinanceClient financeClient, PurchasingClient parchasingClient, MarineClient marineClient, SharedClient client, TechnicalDashboardClient technicalDashboardClient, DocumentClient documentClient, VesselRoutingClient vesselRoutingClient, IDataProtectionProvider provider, NotificationClient notificationClient, MarineWCFClient marineWCFClient, HSEQManagerDashboardClient hseqManagerDashboardClient)
        {
            _provider = provider;
            _client = client;
            _marineClient = marineClient;
            _purchasingClient = parchasingClient;
            _financeClient = financeClient;
            _crewClient = crewClient;
            _technicalDashboardClient = technicalDashboardClient;
            _documentClient = documentClient;
            _vesselRoutingClient = vesselRoutingClient;
            _notificationClient = notificationClient;
            _marineWCFClient = marineWCFClient;
            _hseqManagerDashboardClient = hseqManagerDashboardClient;            
        }

        /// <summary>
        /// Views the PPM list.
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewPPMList()
        {
            return View();
        }

        /// <summary>
        /// Dashboards the option1.
        /// </summary>
        /// <returns></returns>
        public IActionResult DashboardOption1()
        {
            return View();
        }

        /// <summary>
        /// Dashboards the option2.
        /// </summary>
        /// <returns></returns>
        public IActionResult DashboardOption2()
        {
            return View();
        }

        /// <summary>
        /// Indexes the specified dashboard request.
        /// </summary>
        /// <param name="FleetRequest">The fleet request.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string FleetRequest, string VesselId, string isHome)
        {
            CommonUtil.SetSessionObject(HttpContext.Session, Constants.LogFileName, DateTime.Now.ToString(Constants.LogFileDateTimeFormat) + "_" + Request.Cookies["UserId"] + ".txt");
            var startTime = DateTime.Now;

            ClearPageNavigationSession();
            DashboardViewModel dashboardViewModel = new DashboardViewModel();

            if (!string.IsNullOrWhiteSpace(FleetRequest))
            {
                dashboardViewModel.DashboardParameter = CommonUtil.GetDecryptedFleetRequest(_provider, FleetRequest);
                dashboardViewModel.ActiveMobileTabClass = SetTab(Constants.DashboardFilterKey, dashboardViewModel.ActiveMobileTabClass, Constants.Tab1);
            }
            else
            {
                DashboardParameter parameter = GetDashboardFilter();
                if (parameter != null)
                {
                    parameter.MobileVesselId = null;
                    if (!string.IsNullOrWhiteSpace(VesselId))
                    {
                        parameter.VesselId = VesselId;
                        parameter.Title = CommonUtil.GetDecryptedVessel(_provider, VesselId).Split(Constants.Separator)[1].Split("-")[0].Trim();
                    }
                    dashboardViewModel.DashboardParameter = parameter;
                }
                else
                {
                    NavigationTreeViewModel navigationTreeVM = new NavigationTreeViewModel();
                    _client.AccessToken = GetAccessToken();
                    SetNavigationTreeViewModel(navigationTreeVM);
                    navigationTreeVM.AllowFleetSelection = true;
                    List<NavigationTreeViewModel> response = await _client.GetNavigationTreeTopLevel(navigationTreeVM);
                    NavigationTreeViewModel node = null;

                    if (navigationTreeVM.UserType == UserType.Internal)
                    {
                        node = response.FirstOrDefault(x => x.UserMenuItemType == UserMenuItemType.MyResponsibilities);
                    }

                    if (node == null)
                    {
                        node = response.FirstOrDefault();
                    }

                    // To bind first child fleet from the first parent node when user logins
                    if (node != null && node.UserMenuItemType != UserMenuItemType.MyResponsibilities)
                    {
                        NavigationTreeViewModel childResponse = new NavigationTreeViewModel();
                        node.ParentUserMenuTypeShortCode = node.UserMenuTypeShortCode;
                        var childResponseList = await _client.GetNavigationTreeLevel(node);
                        if (childResponseList != null && childResponseList.Any())
                        {
                            childResponse = childResponseList.FirstOrDefault();
                            dashboardViewModel.DashboardParameter = new DashboardParameter
                            {
                                MenuType = childResponse.UserMenuTypeShortCode,
                                Title = childResponse.Title,
                                FleetId = childResponse.Key
                            };
                        }
                    }

                    if (dashboardViewModel.DashboardParameter == null)
                    {
                        if (node != null)
                        {
                            dashboardViewModel.DashboardParameter = new DashboardParameter
                            {
                                MenuType = node.UserMenuTypeShortCode,
                                Title = node.Title
                            };
                        }
                        else
                        {
                            dashboardViewModel.DashboardParameter = new DashboardParameter();
                        }
                    }
                }
            }
            dashboardViewModel.VesselLists = new List<VesselDashboardViewModel>();
            dashboardViewModel.FleetTrackerURL = CommonUtil.GetFleetTrackerURL(_provider, Request.Cookies["UserId"], dashboardViewModel.DashboardParameter);


            SetDashboardFilter(dashboardViewModel.DashboardParameter);
            if (string.IsNullOrWhiteSpace(FleetRequest) && string.IsNullOrWhiteSpace(VesselId) && !string.IsNullOrWhiteSpace(isHome))
            {
                dashboardViewModel.ActiveMobileTabClass = Constants.Tab1;
            }
            else
                dashboardViewModel.ActiveMobileTabClass = SetTab(Constants.DashboardFilterKey, dashboardViewModel.ActiveMobileTabClass, Constants.Tab1);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nVesselId - {4} \nFleetRequest - {5} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), VesselId, FleetRequest));

            return View("DashboardNew", dashboardViewModel);
        }

        /// <summary>
        /// Gets the dashboard vessel list.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDashboardVesselList(VesselDashboardRequest input)
        {
            var startTime = DateTime.Now;

            input.PageNumber = 1;
            List<VesselDashboardViewModel> response = await LoadVesselsByPage(input);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(input);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nVesselDashboardRequest - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return PartialView("VesselDashboardContainerPartialView", response);
        }

        /// <summary>
        /// Loads the more vessels.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> LoadMoreVessels(VesselDashboardRequest input)
        {
            var startTime = DateTime.Now;

            var vesselList = await LoadVesselsByPage(input);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(input);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nVesselDashboardRequest - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return PartialView("VesselDashboardContainerPartialView", vesselList);
        }

        /// <summary>
        /// Gets the vessel dashboard request object.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [NonAction]
      
        private async Task<List<VesselDashboardViewModel>> LoadVesselsByPage(VesselDashboardRequest input)
        {
            DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
            pageRequest.Length = 20;
            pageRequest.Start = (pageRequest.Length * (input.PageNumber - 1)) + 1;
            pageRequest.Columns = new List<Column>();
            pageRequest.Columns.Add(new Column() { Name = "VesselName" });

            pageRequest.Order = new List<Order>();
            pageRequest.Order.Add(new Order()
            {
                Column = 0,
                Dir = "asc"
            });
            string vesId = null;

            if (!string.IsNullOrWhiteSpace(input.VesselIds))
            {
                string decryptedVesselId = _provider.CreateProtector("Vessel").Unprotect(input.VesselIds);
                vesId = decryptedVesselId.Split(Constants.Separator)[0];
            }

            VesselDashboardRequest request = new VesselDashboardRequest()
            {
                UserId = Request.Cookies["UserId"],
                MenuType = input.MenuType,
                FleetId = input.FleetId,
                VesselIds = vesId,
                OpexOverSpendHigh = -5,
                OpexOverSpendMid = -2.5m,
                HazOccSeriousAccidentStartDate = DateTime.Now.AddMonths(-3),
                HazOccSeriousAccidentEndDate = DateTime.Now,
                HazOccSeriousIncidentStartDate = DateTime.Now.AddMonths(-3),
                HazOccSeriousIncidentEndDate = DateTime.Now,
                HazOccUnsafeActCheckCount = 3,
                CertStopSailingTradingStartDate = DateTime.Now,
                CertStopSailingTradingEndDate = DateTime.Now.AddDays(29),
                InspPSCDetentionStartDate = DateTime.Now.Date.AddMonths(-3),
                InspPSCDetentionEndDate = DateTime.Now.Date,
                InspOMVRejectionStartDate = DateTime.Now.Date.AddMonths(-3),
                InspOMVRejectionEndDate = DateTime.Now.Date,
                InspOverduefindingsCheckCount = 0,
                DefectsAwaitingSparesCheckCount = 0,
                DefectsOffHireCheckCount = 0,
                PMSCriticalOverdueCheckCount = 0,
                PMSOverdueCheckCount = 0,
                MedAndHighRisk = 2,
                PurchAwaitingSnrAuthCheckDaysCount = 4

            };
            _client.AccessToken = GetAccessToken();
            DataTablePageResponse<List<VesselDashboardViewModel>> response = await _client.PostGetVesselDashboardListPaged(pageRequest, request);

			if (response != null && response.Data != null && response.Data.Any())
			{
                List<string> vesselIds = response.Data.Select(x => x.VesselIdentifier).ToList();
                _hseqManagerDashboardClient.AccessToken = GetAccessToken();
                List<VesselSentinelScoreResponseViewModel> sentinelScore = await _hseqManagerDashboardClient.GetVesselSentinelScore(vesselIds);

                foreach (var item in response.Data)
                {
                    VesselSentinelScoreResponseViewModel obj = sentinelScore.FirstOrDefault(x => x.VesselId.Equals(item.VesselIdentifier));
					if (obj != null)
					{
                        item.SentinelTotalValue = obj.SentinelTotalValue;
                        item.SentinelTotalValueColor = obj.SentinelTotalValueColor;
                    }
                }
            }

            return response.Data;
        }

        /// <summary>
        /// Gets the dashboard vessel header.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDashboardVesselHeader(VesselDashboardRequest input)
        {
            input.PageNumber = 1;
            List<VesselDashboardViewModel> response = await LoadVesselsByPage(input);

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the PPM list.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        public JsonResult GetPPMList(string searchText)
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                List<PPMDetailViewModel> ppmAll = new List<PPMDetailViewModel>(){
                            new PPMDetailViewModel{
                                ComponentName="A/B Main Burner", Job="Burner monthly Check ", DueDate="", Specifics="", Type="CHK", Status="WO",Interval="1 M", Resp="3/Engr", LeftHours="", Category = "Done",
                                AdvDays ="-126",Attachments="", ClosedDate="09 Nov 2020",Dept="Deck",DoneDate="01 Nov 2020",ReportForm="",RunningHour="0",StDeleted="No",WorkOrderType="WO",ClassCode =""
                            },
                            new PPMDetailViewModel{ ComponentName="Vapour Monitoring System", Job="Calibration VEC Oxygen sensor", DueDate="", Specifics="", Type="TST", Status="WO",Interval="1 M", Resp="C/O", LeftHours="", Category = "Done",
                            AdvDays ="2",Attachments="", ClosedDate="30 Oct 2020",Dept="Engg",DoneDate="30 Oct 2020",ReportForm="",RunningHour="0",StDeleted="No",WorkOrderType="WO",ClassCode =""
                            },
                            new PPMDetailViewModel{ ComponentName="DG1 Turbocharger", Job="Cleaning DG Turbine (Water Washing)", DueDate="29 Jun 2020", Specifics="", Type="WCL", Status="WO",Interval="1 M", Resp="3/Engr", LeftHours="", Category = "DUE",
                             AdvDays ="",Attachments="", ClosedDate="",Dept="",DoneDate="", ReportForm="",RunningHour="",StDeleted="",WorkOrderType="",ClassCode =""
                            },
                            new PPMDetailViewModel{ ComponentName="Clean F.W. Tank (P)", Job="FW Tank vent. head maintenance.", DueDate="29 Jun 2020", Specifics="", Type="CHK", Status="WO",Interval="12 M", Resp="C/O", LeftHours="", Category = "DUE",
                             AdvDays ="",Attachments="", ClosedDate="",Dept="",DoneDate="", ReportForm="",RunningHour="",StDeleted="",WorkOrderType="",ClassCode =""},
                            new PPMDetailViewModel{ ComponentName="Cargo System &amp; Equipment - Services", Job="Loading Computer Annual Test", DueDate="29 Jun 2020", Specifics="", Type="CERT", Status="WO",Interval="12 M", Resp="Master", LeftHours="", Category = "DUE",
                            AdvDays ="",Attachments="", ClosedDate="",Dept="",DoneDate="", ReportForm="",RunningHour="",StDeleted="",WorkOrderType="",ClassCode =""},
                        };
                List<PPMDetailViewModel> result = new List<PPMDetailViewModel>();

                if (searchText.Equals("ALL"))
                {
                    result = ppmAll;
                }
                else
                {
                    result = ppmAll.Where(x => x.Category.Equals(searchText)).ToList();
                }
                return Json(new
                {
                    success = true,
                    data = result
                });
            }

            return Json(new
            {
                success = false
            });
        }

        /// <summary>
        /// Grids to cards.
        /// </summary>
        /// <returns></returns>
        public IActionResult GridToCards()
        {
            return View();
        }

        /// <summary>
        /// Vessels the details.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult VesselDetails(string vesselId)
        {
            var startTime = DateTime.Now;

            InspectionRequestViewModel request = new InspectionRequestViewModel();
            request.FromDate = DateTime.Now.AddMonths(-6);
            request.ToDate = DateTime.Now;
            request.InspectionType = InspectionDashboardType.InspectionOverdueType;
            request.IsSummaryClicked = true;

            string inspectionURL = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));

            ViewBag.InspectionURL = inspectionURL;
            ViewBag.VesselId = vesselId;

            PurchaseOrderRequestViewModel poRequest = new PurchaseOrderRequestViewModel();
            poRequest.FromDate = DateTime.Now.AddMonths(-6);
            poRequest.ToDate = DateTime.Now;
            poRequest.POStage = EnumsHelper.GetKeyValue(PoStagesFilter.InProcess);
            string purchaseOrderURL = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(poRequest));
            ViewBag.PurchaseOrderURL = purchaseOrderURL;

            CertificateRequestViewModel certficateRequest = new CertificateRequestViewModel();
            certficateRequest.StageName = EnumsHelper.GetKeyValue(VesselCertificates.TotalActive);
            certficateRequest.MenuType = UserMenuItemType.Vessel;
            string certificateURL = _provider.CreateProtector("CertificateURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(certficateRequest));
            ViewBag.CertificateURL = certificateURL;

            //create here url for operation cost - OperatingCostBarChartRequest
            OperatingCostBarChartRequest operationCostRequest = new OperatingCostBarChartRequest();
            operationCostRequest.ToDate = new DateTime(2020, 3, 31);
            operationCostRequest.AccountLevel = -1;
            operationCostRequest.ReportDefinitionType = ReportDefinitionType.S;
            operationCostRequest.CoyId = "4251";
            operationCostRequest.AccountId = null;
            string operationCostURL = _provider.CreateProtector("OperationCostURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(operationCostRequest));
            ViewBag.OperationCostUrl = operationCostURL;

            //URL for planned maintenance
            PlannedMaintenanceListViewModel plannedMaintenance = new PlannedMaintenanceListViewModel();
            plannedMaintenance.ToDate = DateTime.Now.AddMonths(1).AddDays(-1);
            plannedMaintenance.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            plannedMaintenance.StageName = EnumsHelper.GetKeyValue(PlannedMaintenanceStages.Due);
            string plannedMaintanaceUrl = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(plannedMaintenance));
            ViewBag.PlannedMaintenanceUrl = plannedMaintanaceUrl;

            VoyageReportingRequestViewModel voyageReportingRequest = new VoyageReportingRequestViewModel();
            //TODO Kavita -- From date and To date comes from the vessel's voyage detail
            voyageReportingRequest.FromDate = DateTime.Now.AddMonths(-6);
            voyageReportingRequest.ToDate = DateTime.Now;
            voyageReportingRequest.MenuType = UserMenuItemType.Vessel;
            string voyageReportingURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequest));
            ViewBag.VoyageReportingURL = voyageReportingURL;

            //url for Defect Module Navigation
            DefectListViewModel defectVM = new DefectListViewModel();
            defectVM.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            defectVM.ToDate = defectVM.FromDate.Value.AddMonths(1).AddDays(-1);
            defectVM.MenuType = EnumsHelper.GetKeyValue(UserMenuItemType.Vessel);
            defectVM.StageName = EnumsHelper.GetKeyValue(DefectStages.Due);
            string defectUrl = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(defectVM));
            ViewBag.DefectUrl = defectUrl;

            VesselDetailViewModel vesselDetails = new VesselDetailViewModel();
            vesselDetails.VesselId = vesselId;

            var endTime = DateTime.Now;

            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nvesselId - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), vesselId));

            return View(vesselDetails);
        }

        //public IActionResult ViewPurchasingList()
        //{
        //	return View();
        //}

        /// <summary>
        /// Gets the purchasing list.
        /// </summary>
        /// <returns></returns>
        public IActionResult DemoDashboard()
        {
            return View();
        }

        /// <summary>
        /// Changes the order status.
        /// </summary>
        /// <returns></returns>
        public IActionResult ChangeOrderStatus()
        {
            return View();
        }

        /// <summary>
        /// Views the defects list.
        /// </summary>
        /// <returns></returns>
        public IActionResult ViewDefectsList()
        {
            return View();
        }

        /// <summary>
        /// Gets the defects list.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <returns></returns>
        public IActionResult GetDefectsList(DataTablePageRequest<string> pageRequest)
        {
            List<DefectsViewModel> list = new List<DefectsViewModel>() {

                new DefectsViewModel { DefectNo = "V/2019/013", TechDefect = "", Category = "Failure", Title="Reverse Osmosis Conductivity sensor failure", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="09 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="DUE" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/012", TechDefect = "", Category = "Failure", Title="ME/Booster module Flowmeters defect", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="31 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="DUE" , OffHire = "" },

                new DefectsViewModel { DefectNo = "V/2019/017", TechDefect = "", Category = "Repair", Title="Overboard discharge ballast valve (S) leakage", Status="DWO", SystemArea="8. Cargo operations", CurrentDueDate="27 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OVERDUE" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/016", TechDefect = "", Category = "Repair", Title="AE No.1 Cooler box crack", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="27 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OVERDUE" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/015", TechDefect = "", Category = "Repair", Title="AE No.1 broken Starter", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="17 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OVERDUE" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/014", TechDefect = "", Category = "Failure", Title="Catfine filter broken Solenoid valve", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="10 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OVERDUE" , OffHire = "" },

                new DefectsViewModel { DefectNo = "V/2019/017", TechDefect = "", Category = "Repair", Title="Overboard discharge ballast valve (S) leakage", Status="DWO", SystemArea="8. Cargo operations", CurrentDueDate="27 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OPEN" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/016", TechDefect = "", Category = "Repair", Title="AE No.1 Cooler box crack", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="27 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OPEN" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/015", TechDefect = "", Category = "Repair", Title="AE No.1 broken Starter", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="17 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OPEN" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/014", TechDefect = "", Category = "Failure", Title="Catfine filter broken Solenoid valve", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="10 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OPEN" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/013", TechDefect = "", Category = "Failure", Title="Reverse Osmosis Conductivity sensor failure", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="09 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OPEN" , OffHire = "" },

                new DefectsViewModel { DefectNo = "V/2019/017", TechDefect = "", Category = "Repair", Title="Overboard discharge ballast valve (S) leakage", Status="DWO", SystemArea="8. Cargo operations", CurrentDueDate="27 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="CLOSED" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/016", TechDefect = "", Category = "Repair", Title="AE No.1 Cooler box crack", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="27 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="CLOSED" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/015", TechDefect = "", Category = "Repair", Title="AE No.1 broken Starter", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="17 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="CLOSED" , OffHire = "" },

                new DefectsViewModel { DefectNo = "V/2019/013", TechDefect = "", Category = "Failure", Title="Reverse Osmosis Conductivity sensor failure", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="09 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OFFHIRE", OffHire = "24 Hours" },
                new DefectsViewModel { DefectNo = "V/2019/012", TechDefect = "", Category = "Failure", Title="ME/Booster module Flowmeters defect", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="31 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OFFHIRE", OffHire = "24 Hours" },

                new DefectsViewModel { DefectNo = "V/2019/011", TechDefect = "", Category = "Failure", Title="ECDIS NO.1 UPS FAILURE", Status="DWO", SystemArea="4. Navigation\\Communications", CurrentDueDate="30 Sep 2019", DueDateBeforeResc="", AdvODDays="" , Type="OPENORDERS" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/010", TechDefect = "", Category = "Failure", Title="ATLAS-COPCO DECK AIR COMPRESSOR", Status="DWO", SystemArea="1. General", CurrentDueDate="01 Nov 2019", DueDateBeforeResc="", AdvODDays="" , Type="OPENORDERS" , OffHire = "" },
                new DefectsViewModel { DefectNo = "V/2019/009", TechDefect = "", Category = "Failure", Title="FWG SW Ejector Pump", Status="DWO", SystemArea="10. Engine\\Steering Compartm.", CurrentDueDate="02 Oct 2019", DueDateBeforeResc="", AdvODDays="" , Type="OPENORDERS" , OffHire = "" },

            };

            List<DefectsViewModel> result = null;

            if (pageRequest.SearchFilter.Equals("ALL"))
            {
                result = list;
            }
            else
            {
                result = list.Where(x => x.Type.Equals(pageRequest.SearchFilter)).ToList();
            }

            return new JsonResult(new DataTablePageResponse<List<DefectsViewModel>>
            {
                Draw = pageRequest.Draw,
                RecordsFiltered = result.Count,
                Data = result,
                RecordsTotal = result.Count
            });
        }

        /// <summary>
        /// Gets the navigation tree top level.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetNavigationTreeTopLevel(NavigationTreeViewModel navigationTreeViewModel)
        {
            _client.AccessToken = GetAccessToken();
            SetNavigationTreeViewModel(navigationTreeViewModel);
            List<NavigationTreeViewModel> response = await _client.GetNavigationTreeTopLevel(navigationTreeViewModel);

            return new JsonResult(response);
        }

        /// <summary>
        /// Determines whether [is from dashboard map full screen].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is from dashboard map full screen]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsFromDashboardMapFullScreen()
        {
            string lastReferer = GetLastReferrer();
            return lastReferer.Equals("DashboardMapFullScreen", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the navigation tree top level without vessel.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetNavigationTreeTopLevelWithoutVessel(NavigationTreeViewModel navigationTreeViewModel)
        {
            _client.AccessToken = GetAccessToken();
            SetNavigationTreeViewModel(navigationTreeViewModel);

            List<NavigationTreeViewModel> response = await _client.GetNavigationTreeTopLevelWithoutVessel(navigationTreeViewModel);
            return new JsonResult(response);
        }

        /// <summary>
        /// Sets the navigation TreeView model.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        [NonAction]
        private void SetNavigationTreeViewModel(NavigationTreeViewModel navigationTreeViewModel)
        {
            navigationTreeViewModel.Exclusion = new List<UserMenuItemType>();

            navigationTreeViewModel.UserType = (UserType)Enum.Parse(typeof(UserType), HttpContext.Session.GetString("UserType"));
            if (navigationTreeViewModel.UserType == UserType.Client)
            {
                navigationTreeViewModel.PreloadUserFleet = false;
                navigationTreeViewModel.Exclusion.AddRange(new List<UserMenuItemType> { UserMenuItemType.MyResponsibilities, UserMenuItemType.MyFleets, UserMenuItemType.MyOffices, UserMenuItemType.MotoMoco });
            }
            else
            {
                navigationTreeViewModel.PreloadUserFleet = true;
                navigationTreeViewModel.Exclusion.AddRange(new List<UserMenuItemType> { UserMenuItemType.MotoMoco });
            }
        }

        /// <summary>
        /// Gets the navigation tree level.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetNavigationTreeLevel(NavigationTreeViewModel navigationTreeViewModel)
        {
            _client.AccessToken = GetAccessToken();

            List<NavigationTreeViewModel> response = new List<NavigationTreeViewModel>();
            response = await _client.GetNavigationTreeLevel(navigationTreeViewModel);

            return new JsonResult(response);
        }

        /// <summary>
        /// Updates the demo mode setting.
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateDemoModeSetting()
        {
            ToggleDemoMode();
            SetDemoData();
            return new JsonResult(true);
        }

        /// <summary>
        /// Set demo data
        /// </summary>
        private void SetDemoData()
        {
            if (IsDemoMode)
            {
                Dictionary<string, string> demoValuePairs = new Dictionary<string, string>();
                demoValuePairs.Add("PHOENIX BEAUTY", "Athens Alpha");
                demoValuePairs.Add("ODORI", "Athens Beta");
                demoValuePairs.Add("Sea Calm", "Athens Gama");
                demoValuePairs.Add("Adafera", "Athens Delta");
                demoValuePairs.Add("DENYS GUMENYUK", "Punit Jain");
                demoValuePairs.Add("OLEG TRIFONOV", "Rashid Khan");
                demoValuePairs.Add("Sudip Paul", "Khyati Parikh");
                HttpContext.Session.SetString("DemoValueList", JsonConvert.SerializeObject(demoValuePairs));

            }
            else
            {
                HttpContext.Session.SetString("DemoValueList", JsonConvert.SerializeObject(new Dictionary<string, string>()));
            }
        }

        /// <summary>
        /// Gets the navigation tree level without vessel.
        /// </summary>
        /// <param name="navigationTreeViewModel">The navigation TreeView model.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetNavigationTreeLevelWithoutVessel(NavigationTreeViewModel navigationTreeViewModel)
        {
            _client.AccessToken = GetAccessToken();

            List<NavigationTreeViewModel> response = new List<NavigationTreeViewModel>();

            response = await _client.GetNavigationTreeLevelWithoutVessel(navigationTreeViewModel);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the available vessel list.
        /// </summary>
        /// <param name="fleetId">The fleet identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetAvailableVesselList(string fleetId)
        {
            _client.AccessToken = GetAccessToken();
            List<VesselManagementDetailForUserFleetViewModel> response = await _client.PostGetGetUnMappedVesselForUserFleet(fleetId);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the user management available vessel list.
        /// </summary>
        /// <param name="fleetId">The fleet identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetUserManagementAvailableVesselList(string fleetId)
        {
            _client.AccessToken = GetAccessToken();
            List<VesselManagementDetailForUserFleetViewModel> response = await _client.GetUserManagementVessel(fleetId);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the mapped vessel list.
        /// </summary>
        /// <param name="fleetId">The fleet identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetMappedVesselList(string fleetId)
        {
            _client.AccessToken = GetAccessToken();
            List<VesselManagementDetailForUserFleetViewModel> response = new List<VesselManagementDetailForUserFleetViewModel>();
            if (!string.IsNullOrWhiteSpace(fleetId))
            {
                response = await _client.PostGetMappedVesselForUserFleet(fleetId);
            }
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the fleet list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetFleetList()
        {
            UserFleetsRequestViewModel input = new UserFleetsRequestViewModel();
            input.UserId = Request.Cookies["UserId"];
            _client.AccessToken = GetAccessToken();
            List<FleetDetailViewModel> response = new List<FleetDetailViewModel>();

            response = await _client.PostGetUserFleets(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the fleet details by fleet identifier.
        /// </summary>
        /// <param name="fleetId">The fleet identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetFleetDetailsByFleetId(string fleetId)
        {
            _client.AccessToken = GetAccessToken();

            FleetDetailViewModel response = await _client.PostGetFleetDetailById(fleetId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Saves the user fleet.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="vesselManagementIds">The vessel management ids.</param>
        /// <returns></returns>
        public async Task<IActionResult> SaveUserFleet(FleetDetailViewModel request, List<string> vesselManagementIds)
        {
            _client.AccessToken = GetAccessToken();
            request.UserId = Request.Cookies["UserId"];
            var response = await _client.PostCheckCanCreateUserFleetPWA(request);

            if (response.IsValid)
            {
                var isValid = await _client.PostSaveUserFleet(request, vesselManagementIds);

                return new JsonResult(new { response = isValid, message = isValid ? Constants.FleetSavedSuccessfully : Constants.FleetSavedFailed });
            }
            return new JsonResult(new { response = response.IsValid, message = response.ErrorMessage });
        }

        /// <summary>
        /// Determines whether this instance [can create user fleet] the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> CanCreateUserFleet(FleetDetailViewModel request)
        {
            _client.AccessToken = GetAccessToken();
            request.UserId = Request.Cookies["UserId"];
            var response = await _client.PostCheckCanCreateUserFleetPWA(request);

            return new JsonResult(new { response = response.IsValid, message = response.ErrorMessage });
        }

        /// <summary>
        /// Vessels the details options.
        /// </summary>
        /// <returns></returns>
        public IActionResult VesselDetailsOptions()
        {
            return View();
        }

        /// <summary>
        /// Vessels the details mobile.
        /// </summary>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult VesselDetailsMobile(string VesselId)
        {
            if (string.IsNullOrWhiteSpace(VesselId))
            {
                DashboardParameter parameter = GetDashboardFilter();
                if (parameter != null)
                {
                    if (string.IsNullOrWhiteSpace(parameter.MobileVesselId))
                    {
                        SetDashboardFilter(parameter);
                        return RedirectToAction("Index", null);
                    }
                    VesselId = parameter.MobileVesselId;
                }
            }
            VesselDetailsMobileViewModel viewModel = new VesselDetailsMobileViewModel();
            viewModel.EncryptedVesselId = VesselId;
            string decreptedString = CommonUtil.GetDecryptedVessel(_provider, VesselId);
            viewModel.VesselName = decreptedString.Split(Constants.Separator)[1].Split("-")[0].Trim();
            viewModel.VesselIdentifier = decreptedString.Split(Constants.Separator)[0];

            return View(viewModel);
        }

        /// <summary>
        /// Posts the get client logo.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetUserClientLogo()
        {
            var startTime = DateTime.Now;

            _documentClient.AccessToken = GetAccessToken();
            _client.AccessToken = GetAccessToken();

            string clientName = null;
            string image = null;

            UserType userType = (UserType)Enum.Parse(typeof(UserType), HttpContext.Session.GetString("UserType"));

            if (userType == UserType.Client)
            {
                UserViewModel userDetail = await _client.GetUserDetail();

                if (userDetail != null)
                {
                    clientName = userDetail.ClientName;
                    if (!string.IsNullOrWhiteSpace(userDetail.ClientId) && userDetail.IsClientLogoAvailable)
                    {
                        var result = await _documentClient.DownloadCompanyLogoImage(userDetail.ClientId);
                        byte[] byteData = result != null ? CommonUtil.ConvertStreamToByte(result) : null;
                        image = byteData != null ? Convert.ToBase64String(byteData) : null;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(clientName))
            {
                clientName = Constants.VShipsClientName;
            }

            HttpContext.Session.SetString(Constants.UserClientNameSessionKey, clientName);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime)));

            return new JsonResult(new { Image = image, ClientName = clientName });
        }

        /// <summary>
        /// Gets the user task messages.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetUserTaskMessages()
        {
            _client.AccessToken = GetAccessToken();
            List<TaskMessageDetailViewModel> response = await _client.GetTaskMessages();
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Downloads the document.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> DownloadDocument(string input)
        {
            string encryptedData = _provider.CreateProtector("DownloadDocument").Unprotect(input);
            TaskMessageDetail detail = new TaskMessageDetail();
            detail = JsonConvert.DeserializeObject<TaskMessageDetail>(encryptedData);

            string reportName = !string.IsNullOrWhiteSpace(detail.GeneratedFilename) ? detail.GeneratedFilename : detail.MessageContent;
            string friendlyFileName = !string.IsNullOrWhiteSpace(detail.FriendlyFileName) ? detail.FriendlyFileName : reportName;
            DocumentFileType documentFileType = EnumsHelper.GetValues<DocumentFileType>().Where(x => EnumsHelper.GetKeyValue(x) == Path.GetExtension(reportName)).FirstOrDefault();

            ReportDownloadHistoryRequest request = new ReportDownloadHistoryRequest
            {
                FileName = reportName,
                TaskMessageId = detail.TaskMessageId,
                DownloadMode = "Manual",
                DownloadedBy = Request.Cookies["UserId"]
            };

            if (!string.IsNullOrWhiteSpace(reportName) &&
                    (reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.ExcelXLSX))
                    || reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.ExcelXLSM))
                    || reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Word))
                    || reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.PDF))
                    || reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Excel))
                    || reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.CSV))
                    || reportName.EndsWith(EnumsHelper.GetKeyValue(ReportExportTypes.Text))
                    || reportName.EndsWith(Constants.ZipExtension, StringComparison.OrdinalIgnoreCase)))
            {
                _documentClient.AccessToken = GetAccessToken();
                Stream documentResult = await _documentClient.DownloadReportAndLogHistory(request);

                if (documentResult != null)
                {
                    byte[] byteData = CommonUtil.ConvertStreamToByte(documentResult);
                    string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
                    return new JsonResult(new { filename = friendlyFileName, bytes = byteString, fileType = EnumsHelper.GetDescription(documentFileType) });
                }
                else
                {
                    return new JsonResult(new { filename = request.FileName, bytes = "", fileType = EnumsHelper.GetDescription(documentFileType) });
                }
            }
            return new JsonResult("");
        }

        #region Inspection - Dashboard

        /// <summary>
        /// Gets the inspection dashboard details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetInspectionDashboardDetails(InspectionManagerDashboardRequestViewModel request)
        {
            _marineClient.AccessToken = GetAccessToken();
            InspectionManagerDashboardDetailViewModel inspectionDashboard = await _marineClient.PostGetInspectionManagerDashboardDetail(request);
            return new JsonResult(inspectionDashboard);
        }

        #endregion

        #region Certificate - Dashboard

        /// <summary>
        /// Gets the certificate dashboard details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCertificateDashboardDetails(CertificateRequestViewModel input)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            input.MenuType = UserMenuItemType.Vessel;
            VesselCertificateSummaryStatResponseViewModel response = await _marineClient.PostGetVesCertSummaryStats(input);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(input);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nCertificateRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        #endregion

        #region Open Order - Dashboard

        /// <summary>
        /// Gets the order summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOrderSummary(PurchasingFilter request)
        {
            var startTime = DateTime.Now;

            _purchasingClient.AccessToken = GetAccessToken();
            string decryptedVesselId = _provider.CreateProtector("Vessel").Unprotect(request.VesselId);
            string vesId = decryptedVesselId.Split(Constants.Separator)[0];
            string coyId = decryptedVesselId.Split(Constants.Separator)[2];

            PurchaseOrderSummaryViewModel response = new PurchaseOrderSummaryViewModel();

            if (!string.IsNullOrWhiteSpace(vesId))
            {
                request.VesselId = vesId;
                request.CoyId = coyId;
                response = await _purchasingClient.GetPurchaseOrderOrderCountByVessel(request);
            }

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nPurchasingFilter - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        #endregion

        #region OPEX - Dashboard

        /// <summary>
        /// Gets the opex details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOpexDetails(OperationCostDrillDownViewModel request)
        {
            var startTime = DateTime.Now;

            _financeClient.AccessToken = GetAccessToken();
            OperatingCostBarChartResponseViewModel response = new OperatingCostBarChartResponseViewModel();

            response = await _financeClient.GetOperationCostDetailsForVessel(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nOperationCostDrillDownViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        #endregion

        #region Vessel Details

        /// <summary>
        /// Gets the vessel details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVesselDetails(DashboardParameter input)
        {
            var startTime = DateTime.Now;

            DashboardParameter parameter = GetDashboardFilter();
            if (parameter != null)
            {
                parameter.IsFleetSelection = input.IsFleetSelection;
                parameter.VesselIdentifier = input.VesselIdentifier;
                parameter.MobileVesselId = input.VesselId;
                SetDashboardFilter(parameter);
            }
            _marineClient.AccessToken = GetAccessToken();
            VesselPreviewViewModel previewViewModel = await _marineClient.PostGetVesselPreview(input.VesselId);

            //Calling shared api for getting document parameter details
            _client.AccessToken = GetAccessToken();
            DocumentDetailRequest documentDetailRequest = new DocumentDetailRequest();
            string decryptedString = _provider.CreateProtector("Vessel").Unprotect(input.VesselId);
            documentDetailRequest.SourceId = decryptedString.Split(Constants.Separator)[0];
            documentDetailRequest.SsmId = EnumsHelper.GetKeyValue(SubModule.Vessel);
            documentDetailRequest.DctId = EnumsHelper.GetKeyValue<DocumentCategoryType>(DocumentCategoryType.VesselImage);
            string documentParameter = _provider.CreateProtector("DocumentURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(documentDetailRequest));

            List<DocumentDetail> response = await _client.PostGetDocumentDetails(documentParameter);

            //Calling document api for getting vessel picture.
            _documentClient.AccessToken = GetAccessToken();
            var result = await _documentClient.PostGetVesselPicture(response);
            byte[] byteData = result != null ? CommonUtil.ConvertStreamToByte(result) : null;
            previewViewModel.image = byteData != null ? Convert.ToBase64String(byteData) : null;

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(input);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \ninput - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(previewViewModel);
        }


        /// <summary>
        /// Gets the vessel office details.
        /// </summary>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVesselOfficeDetails(string VesselId)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            OnboardVesselOfficerDetailsViewModel crewDetails = await _marineClient.PostGetVesselSeniorOfficer(VesselId);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nVesselId - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), VesselId));

            return new JsonResult(crewDetails);
        }

        #endregion

        #region Voyage - Dashboard

        /// <summary>
        /// Gets the voyage landing page detail.
        /// </summary>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVoyageLandingPageDetail(string VesselId)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            VoyageLandingPageDetailsViewModel voyageReport = await _marineClient.PostGetVoyageLandingPageDetail(VesselId);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nVesselId - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), VesselId));

            return new JsonResult(voyageReport);
        }

        /// <summary>
        /// Gets the voyage landing page details.
        /// </summary>
        /// <param name="vesselIds">The vessel ids.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVoyageDetails(List<string> vesselIds)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();

            List<VoyageDetailsViewModel> voyageReports = await _marineClient.PostGetVoyageDetail(vesselIds);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams:  \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime)));
            return new JsonResult(voyageReports);
        }

        /// <summary>
        /// Gets the sea passage details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSeaPassageDetails(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            CharterDetailViewModel response = await _marineClient.PostGetSeaPassageDetails(input);
            response.CharterName = response.CharterName ?? " - ";
            response.VoyageNumber = response.VoyageNumber ?? " - ";
            response.CharterNumber = response.CharterNumber ?? " - ";
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the port call detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortCallDetail(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            PortCallDetailViewModel response = await _marineClient.PostGetPortCallDetail(input);
            response.CharterName = response.CharterName ?? " - ";
            response.VoyageNumber = response.VoyageNumber ?? " - ";
            response.CharterNumber = response.CharterNumber ?? " - ";
            return new JsonResult(new { data = response });
        }

        //public async Task<IActionResult> GetBreaksAndBadWeatherDetail(string inputRequest) 
        //{
        //    _marineClient.AccessToken = GetAccessToken();
        //    BreaksAndBadWeatherDetailViewModel response = await _marineClient.PostGetBreaksAndBadWeatherDetail(inputRequest);
        //    return new JsonResult(new { data = response });
        //}

        /// <summary>
        /// Gets the agent details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetAgentDetails(string input)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            _client.AccessToken = GetAccessToken();

            PositionListDetailsViewModel response = await _marineClient.PostGetPosListByVesselAndPosId(input);
            List<CompanyEntityViewModel> Agents = new List<CompanyEntityViewModel>();
            List<CompanyDetails> tasks = new List<CompanyDetails>();

            if (response != null && !string.IsNullOrWhiteSpace(response.PosAgent1))
            {
                var encryptedCompany1 = _provider.CreateProtector("Company").Protect(response.PosAgent1);
                CompanyDetails companyDetails1 = await _client.PostGetCompanyDetail(encryptedCompany1);
                companyDetails1.TypeOfCompany = EnumsHelper.GetEnumNameFromKeyValue(typeof(VesselAgentType), response.PosAgentStatus1);
                tasks.Add(companyDetails1);
            }
            if (response != null && !string.IsNullOrWhiteSpace(response.PosAgent2))
            {
                var encryptedCompany2 = _provider.CreateProtector("Company").Protect(response.PosAgent2);
                CompanyDetails companyDetails2 = await _client.PostGetCompanyDetail(encryptedCompany2);
                companyDetails2.TypeOfCompany = EnumsHelper.GetEnumNameFromKeyValue(typeof(VesselAgentType), response.PosAgentStatus2);
                tasks.Add(companyDetails2);
            }
            if (response != null && !string.IsNullOrWhiteSpace(response.PosAgent3))
            {
                var encryptedCompany3 = _provider.CreateProtector("Company").Protect(response.PosAgent3);
                CompanyDetails companyDetails3 = await _client.PostGetCompanyDetail(encryptedCompany3);
                companyDetails3.TypeOfCompany = EnumsHelper.GetEnumNameFromKeyValue(typeof(VesselAgentType), response.PosAgentStatus3);
                tasks.Add(companyDetails3);
            }

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nGetAgentDetails - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), input));
            return new JsonResult(tasks);
        }

        /// <summary>
        /// Seas the passage event details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> SeaPassageEventDetails(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            SeaPassageActivityViewModel response = await _marineClient.PostGetSeaPassageReports(input);
            return new JsonResult(response);
        }

        #endregion

        #region Dashboard - Crew

        /// <summary>
        /// Gets the crew summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCrewSummary(string input)
        {
            var startTime = DateTime.Now;

            _crewClient.AccessToken = GetAccessToken();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input);
            CrewSummaryRequest summaryRequest = new CrewSummaryRequest();
            summaryRequest.VesselId = decreptedString.Split(Constants.Separator)[0];
            summaryRequest.FromDate = DateTime.Now.Date.AddMonths(-1);
            summaryRequest.ToDate = DateTime.Now.Date;
            summaryRequest.PastNumberOfDays = 30;
            CrewSummaryResponseViewModel response = await _crewClient.PostGetCrewSummary(summaryRequest);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \ninput - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), input));

            return new JsonResult(response);
        }

        #endregion

        #region Hazocc - Dashboard

        /// <summary>
        /// Gets the hazocc dashboard details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazoccDashboardDetails(HazoccDashboardRequestViewModel request)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazoccDashboardDetailViewModel inspectionDashboard = await _marineClient.PostGetHazoccDashboardDetail(request);
            return new JsonResult(inspectionDashboard);
        }

        /// <summary>
        /// Hazs the occ summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> HazOccSummary(HazOccSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            HazOccSummaryResponseViewModel summaryVM = await _marineClient.PostGetHazOccSummaryDetail(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nHazOccSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(summaryVM);
        }

        #endregion

        #region Dashboard - Defect

        /// <summary>
        /// Gets the defect header count.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDefectDashboardDetails(DefectListViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            DefectSummaryResponseViewModel response = await _marineClient.GetDefectDashboarSummarydDetail(request.EncryptedVesselId);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nDefectListViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));
            return new JsonResult(response);
        }

        #endregion

        #region PMS - Dashboard

        /// <summary>
        /// Gets the hazocc dashboard details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPMSDashboardDetails(PMSDashboardRequestViewModel request)
        {
            _marineClient.AccessToken = GetAccessToken();
            PMSDashboardDetailViewModel inspectionDashboard = await _marineClient.PostGetPMSDashboardDetail(request);
            return new JsonResult(inspectionDashboard);
        }

        /// <summary>
        /// PMSs the dashboard summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> PMSDashboardSummary(PMSDashboardRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            PMSDashboardSummaryViewModel pmsSummary = await _marineClient.PMSDashboardSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nPMSDashboardRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(pmsSummary);
        }


        #endregion

        #region RightShip - Dashboard
        /// <summary>
        /// Gets the olt vessel performance.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOltVesselPerformance(PMSDashboardRequestViewModel request)
        {
            _technicalDashboardClient.AccessToken = GetAccessToken();
            string RightShip = await _technicalDashboardClient.GetOltVesselPerformance(request.VesselId);
            return new JsonResult(RightShip);
        }
        #endregion

        #region Vessel Lookup

        /// <summary>
        /// Gets the vessel lookup.
        /// </summary>
        /// <param name="term">The term.</param>
        /// <param name="q">The q.</param>
        /// <param name="type">The type.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public async Task<JsonResult> GetVesselLookup(string term, string q, string type, int page)
        {
            var startTime = DateTime.Now;

            _client.AccessToken = GetAccessToken();

            NavigationTreeViewModel navigationTreeVM = new NavigationTreeViewModel();
            SetNavigationTreeViewModel(navigationTreeVM);

            ManagementVesselFilter request = new ManagementVesselFilter();
            request.FetchOnlyActivatedCoys = true;

            if (string.IsNullOrWhiteSpace(q))
            {
                request.FleetMenuType = navigationTreeVM.UserType == UserType.Internal ? Constants.Responsibilities : Constants.Client;
            }
            else
            {
                request.FleetMenuType = null;
                request.FetchOnlyActivatedCoys = null;
            }

            request.VesselName = q;
            request.AccountingCompanyId = q;
            request.FetchOnlyActivatedAccountingCompanies = false;
            request.AvoidCoyIdCheck = null;
            request.IsFetchForAllCompanies = false;
            request.IsFetchForAllLocalCompanies = false;
            request.IsQuickSearch = true;
            request.IsVesselInPurchasingManagement = false;
            request.IsVesselsCurrentlyInManagement = true;

            List<ManagementVesselDetailViewModel> response = await _client.PostGetManagementVesselLookup(request);
            Select2ResponseViewModel<List<ManagementVesselDetailViewModel>> select2ResponseViewModel = new Select2ResponseViewModel<List<ManagementVesselDetailViewModel>>();
            select2ResponseViewModel.Results = new List<ManagementVesselDetailViewModel>();
            select2ResponseViewModel.Results = response;
            select2ResponseViewModel.Pagination = new Pagination();

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nterm - {4} \nq - {5} \ntype - {6} \npage - {7} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), term, q, type, page));
            return new JsonResult(select2ResponseViewModel);

        }

        #endregion

        #region Dashboard - Commercial Inspection & Environment Summary
        /// <summary>
        /// Gets the commercial summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCommercialSummary(string input)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();

            CommercialSummaryResponseViewModel response = await _marineClient.GetCommercialSummary(input);
            VoyageLandingPageDetailsViewModel voyageReport = await _marineClient.PostGetVoyageLandingPageDetail(input);
            List<RouteForecastAlert> forecastAlertResponse = await _vesselRoutingClient.GetAlertData();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input);
            string vesselId = decreptedString.Split(Constants.Separator)[0];
            response.VesselName = decreptedString.Split(Constants.Separator)[1];
            response.VesselId = input;
            response.ViewMoreURL = voyageReport.RequestURL;
            response.PredictedBadWeather = forecastAlertResponse != null && forecastAlertResponse.Any(x => x.VesselDetailsObj.VesselId == vesselId) ? forecastAlertResponse.Count(x => x.VesselDetailsObj.VesselId == vesselId) : 0;
            response.PredictedBadWeatherPriority = response.PredictedBadWeather > 0 ? 1 : 0;

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \ninput - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), input));
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the predicted bad weather.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPredictedBadWeather(string input)
        {
            var startTime = DateTime.Now;

            List<RouteForecastAlert> forecastAlertResponse = await _vesselRoutingClient.GetAlertData();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input);
            string vesId = decreptedString.Split(Constants.Separator)[0];


            List<RolledUpForecastAlertViewModel> response = new List<RolledUpForecastAlertViewModel>();


            if (forecastAlertResponse != null && forecastAlertResponse.Any(x => x.VesselDetailsObj.VesselId == vesId))
            {

                RolledUpForecastAlert _rolledUpForecastAlerts = forecastAlertResponse.GroupBy(g => new { g.VesselName, g.Imo, g.StartPortName, g.StartPortCountry, g.JourneyStartDate, g.StartPortCoordinate, g.EndPortName, g.EndPortCountry, g.JourneyCompletedDate, g.EndPortCoordinate, g.VesselDetails })
                .Select(s => new RolledUpForecastAlert
                {
                    VesselName = s.Key.VesselName,
                    Imo = s.Key.Imo,
                    DistanceTravelled = (int)((DateTime.Now - s.Key.JourneyStartDate).TotalSeconds / ((s.Key.JourneyCompletedDate != null ? s.Key.JourneyCompletedDate.GetValueOrDefault() : DateTime.Now) - s.Key.JourneyStartDate).TotalSeconds * 100),
                    StartPortName = s.Key.StartPortName,
                    StartPortCountry = s.Key.StartPortCountry,
                    JourneyStartDate = s.Key.JourneyStartDate,
                    StartCoordinate = s.Key.StartPortCoordinate,
                    EndPortName = s.Key.EndPortName,
                    EndPortCountry = s.Key.EndPortCountry,
                    JourneyCompletedDate = s.Key.JourneyCompletedDate,
                    EndCoordinate = s.Key.EndPortCoordinate,
                    WindWarning = GetDTGroupedAlert(s.Select(t => t)),
                    RouteForecastAlerts = GroupByDateTime(s.Select(t => t), s.Key.JourneyStartDate, s.Key.JourneyCompletedDate),
                    VesselDetails = s.Key.VesselDetails
                }).Where(x => x.VesselDetailsObj.VesselId == vesId).OrderBy(o => o.VesselName).ThenBy(o => o.EndPortName).FirstOrDefault();

                foreach (var item in _rolledUpForecastAlerts.RouteForecastAlerts)
                {
                    RolledUpForecastAlertViewModel badWeatherDetail = new RolledUpForecastAlertViewModel()
                    {
                        BeaufortColour = item.BeaufortColour,
                        Direction = item.Direction,
                        LocationStr = item.LocationStr,
                        SpeedBeaufort = item.SpeedBeaufort,
                        SpeedMS = item.SpeedMS,
                        WeatherDate = item.WeatherDate
                    };
                    response.Add(badWeatherDetail);
                }

            }

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \ninput - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), input));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Groups the by date time.
        /// </summary>
        /// <param name="routeForecastAlerts">The route forecast alerts.</param>
        /// <param name="journeyStartDate">The journey start date.</param>
        /// <param name="journeyCompletedDate">The journey completed date.</param>
        /// <returns></returns>
        private List<JourneyRoutesWeather> GroupByDateTime(IEnumerable<RouteForecastAlert> routeForecastAlerts, DateTime journeyStartDate, DateTime? journeyCompletedDate)
        {
            var groupedRouteForecastAlerts = routeForecastAlerts.Select(s => s.WeatherWarning).GroupBy(g => g.WeatherDate, (key, gs) => gs.OrderByDescending(o => o.SpeedMS).First()).ToList();
            return groupedRouteForecastAlerts;
        }

        /// <summary>
        /// Gets the dt grouped alert.
        /// </summary>
        /// <param name="routeForecastAlerts">The route forecast alerts.</param>
        /// <returns></returns>
        private string GetDTGroupedAlert(IEnumerable<RouteForecastAlert> routeForecastAlerts)
        {
            var response = string.Empty;

            var dtGroupedWeatherWarnings = routeForecastAlerts.Select(s => s.WeatherWarning).GroupBy(g => g.WeatherDate, (key, gs) => gs.OrderByDescending(o => o.SpeedMS).First()).ToList();

            foreach (var weatherWarning in dtGroupedWeatherWarnings)
            {
                response += (!string.IsNullOrWhiteSpace(response) ? "\n" : string.Empty) + string.Format("Wind: [F{0}, {1}°] location: [{2}] at {3:HH:mm} {3:d MMMM}",
                       weatherWarning.SpeedBeaufort, weatherWarning.Direction, weatherWarning.LocationStr, weatherWarning.WeatherDate);
            }

            return response;
        }

        /// <summary>
        /// Gets the inspection manager summary
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetInspectionManagerSummary(InspectionManagerDashboardRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();

            InspectionManagerDashboardDetailViewModel response = await _marineClient.PostGetInspectionManagerDashboardDetail(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nInspectionManagerDashboardRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the environment summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetEnvironmentSummary(EnvironmentSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();

            EnvironmentSummaryResponseViewModel response = await _marineClient.GetEnvironmentSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nEnvironmentSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the jsa summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJSASummary(string vesselId)
        {
            var startTime = DateTime.Now;

            string decrypted = CommonUtil.GetDecryptedVessel(_provider, vesselId);

            JobSafetyAnalysisDashboardRequestViewModel request = new JobSafetyAnalysisDashboardRequestViewModel
            {
                Item = new UserMenuItem
                {
                    DisplayText = decrypted.Split(Constants.Separator)[1],
                    Identifier = decrypted.Split(Constants.Separator)[0],
                    UserMenuItemType = UserMenuItemType.Vessel
                },
                EncryptedVesselId = vesselId
            };

            _marineClient.AccessToken = GetAccessToken();
            JobSafetyAnalysisDashboardViewModel result = await _marineClient.GetJSAGraphAndSummary(request);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nvesselId - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), vesselId));

            return new JsonResult(result);
        }


        #endregion

        #region Dashboard - Fleet Summary

        /// <summary>
        /// Gets the fleet summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetFleetSummary(FleetSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _client.AccessToken = GetAccessToken();

            FleetSummaryResponseViewModel response = await _client.GetFleetSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nFleetSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the serious incidents.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSeriousIncidents(SeriousIncidentsRequest request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<SeriousIncidentsViewModel> response = await _marineClient.SeriousIncidentDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nSeriousIncidentsRequest - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the PSC deficiencies.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPSCDeficiencies(PscDeficienciesRequest request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<PscDeficienciesResponseViewModel> response = await _marineClient.InspectionPscDeficienciesDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nPscDeficienciesRequest - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the PSC detention details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPscDetentionDetails(PscDetentionRequest request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<PscDetentionViewModel> response = await _marineClient.InspectionPscDetentionDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nPscDetentionRequest - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the critical PMS.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCriticalPMS(CriticalPMSOverdueRequest request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<CriticalPMSOverdueResponseViewModel> response = await _marineClient.CriticalPMSDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nCriticalPMSOverdueRequest - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the oil spills to water details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOilSpillsToWaterDetails(OilSpillWaterRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<OilSpillWaterResponseViewModel> response = await _marineClient.OilSpillToWaterDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nOilSpillWaterRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the overdue inspection details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOverdueInspectionDetails(OverdueInspectionRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<OverdueInspectionResponseViewModel> response = await _marineClient.OverdueInspectionDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nOverdueInspectionRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the omv findings details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOmvFindingsDetails(OmvFindingsRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<OmvFindingsResponseViewModel> response = await _marineClient.GetOmvFindingsDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nOmvFindingsRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the over budget details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOverBudgetDetails(OverBudgetDetailsRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _financeClient.AccessToken = GetAccessToken();
            List<OverBudgetDetailsResponseViewModel> response = await _financeClient.GetOverBudgetDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nOverBudgetDetailsRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the performance summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPerformanceSummary(PerformanceSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _client.AccessToken = GetAccessToken();

            List<PerformanceSummaryResponseViewModel> response = await _client.GetPerformanceSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nPerformanceSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the off hire summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOffHireSummary(OffHireRequest request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<OffHireResponseViewModel> response = await _marineClient.GetOffHireKPIFleetSummaryDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nOffHireRequest - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the fuel efficiency details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetFuelEfficiencyDetails(FuelEfficiencyDetailsRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<FuelEfficiencyDetailsResponseViewModel> response = await _marineClient.GetFuelEfficiencyDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nFuelEfficiencyDetailsRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the right ship details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetRightShipDetails(RightShipRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            List<RightShipResponseViewModel> response = await _marineClient.GetRightShipDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nRightShipRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the experience matrix details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetExperienceMatrixDetails(CrewExperienceMatrixDetailsRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _crewClient.AccessToken = GetAccessToken();
            List<CrewExperienceMatrixDetailsResponseViewModel> response = await _crewClient.GetCrewExperienceMatrixDetails(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nCrewExperienceMatrixDetailsRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the crew fleet summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCrewFleetSummary(CrewFleetSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _crewClient.AccessToken = GetAccessToken();
            CrewFleetSummaryResponseViewModel response = await _crewClient.GetCrewFleetSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nCrewFleetSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the opex fleet summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOpexFleetSummary(OpexFleetSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _financeClient.AccessToken = GetAccessToken();
            OpexFleetSummaryResponseViewModel response = await _financeClient.GetOpexFleetSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nOpexFleetSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the inspection fleet summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetInspectionFleetSummary(InspectionFleetSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            InspectionFleetSummaryResponseViewModel response = await _marineClient.GetInspectionFleetSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nInspectionFleetSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ fleet summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccFleetSummary(HazOccFleetSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            HazOccFleetSummaryResponseViewModel response = await _marineClient.GetHazOccFleetSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nHazOccFleetSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the commercial fleet summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCommercialFleetSummary(CommercialFleetSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            CommercialFleetSummaryResponseViewModel response = await _marineClient.GetCommercialFleetSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nCommercialFleetSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the rightship fleet summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetRightshipFleetSummary(RightshipFleetSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            RightshipFleetSummaryResponseViewModel response = await _marineClient.GetRightshipFleetSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nRightshipFleetSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the PMS fleet summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPMSFleetSummary(PMSFleetSummaryRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _marineClient.AccessToken = GetAccessToken();
            PMSFleetSummaryResponseViewModel response = await _marineClient.GetPMSFleetSummary(request);

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nPMSFleetSummaryRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));
            return new JsonResult(response);
        }

        #endregion

        /// <summary>
        /// Notifications this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Notification()
        {
            return View();
        }

        /// <summary>
        /// Dashboards the map full screen.
        /// </summary>
        /// <param name="mapurl">The mapurl.</param>
        /// <param name="fleetRequest">The fleet request.</param>
        /// <returns></returns>
        public IActionResult DashboardMapFullScreen(string mapurl, string fleetRequest)
        {
            var startTime = DateTime.Now;

            DashboardMapViewModel mapViewModel = CommonUtil.GetDecryptedRequest<DashboardMapViewModel>(_provider, Constants.FullMapDetails, mapurl);

            if (mapViewModel != null)
            {
                if (!string.IsNullOrWhiteSpace(fleetRequest))
                {
                    DashboardParameter dashboardParameter = new DashboardParameter();
                    dashboardParameter = JsonConvert.DeserializeObject<DashboardParameter>(CommonUtil.DecryptStringAES(fleetRequest));
                    mapViewModel.FleetTrackerURL = CommonUtil.GetFleetTrackerURL(_provider, Request.Cookies["UserId"], dashboardParameter);
                    mapurl = CommonUtil.GetEncryptedURL(_provider, Constants.FullMapDetails, mapViewModel);
                }
                SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.DashboardFullMapPageKey), null, mapurl);
            }
            else
            {
                mapViewModel = new DashboardMapViewModel();
            }

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nmapurl - {4} \nfleetRequest - {5} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), mapurl, fleetRequest));

            return View(mapViewModel);
        }

        /// <summary>
        /// Gets the dashboard full map URL.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDashboardFullMapUrl()
        {
            var startTime = DateTime.Now;

            DashboardMapViewModel mapViewModel = new DashboardMapViewModel();
            DashboardParameter parameter = GetDashboardFilter();
            mapViewModel.FleetTrackerURL = CommonUtil.GetFleetTrackerURL(_provider, Request.Cookies["UserId"], parameter);
            string mapurl = CommonUtil.GetEncryptedURL(_provider, Constants.FullMapDetails, mapViewModel);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime)));
            return new JsonResult(mapurl);
        }

        /// <summary>
        /// Gets the user preferences.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUserPreferences()
        {
            _client.AccessToken = GetAccessToken();
            List<UserPreferenceDetailViewModel> response = await _client.GetUserPreferences();
            MyPreferencesViewModel result = new MyPreferencesViewModel();
            if (response != null && response.Any())
            {
                foreach (var item in response)
                {

                    if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.Commercial)))
                    {
                        result.IsCommercialEnabled = item.IsPreferred;
                        result.Commercial = EnumsHelper.GetDescription(UserPreferences.Commercial);
                    }
                    else if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.HazOcc)))
                    {
                        result.IsHazoccEnabled = item.IsPreferred;
                        result.Hazocc = EnumsHelper.GetDescription(UserPreferences.HazOcc);
                    }
                    else if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.Crewing)))
                    {
                        result.IsCrewEnabled = item.IsPreferred;
                        result.Crew = EnumsHelper.GetDescription(UserPreferences.Crewing);
                    }
                    else if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.Environment)))
                    {
                        result.IsEnvironmentEnabled = item.IsPreferred;
                        result.Environment = EnumsHelper.GetDescription(UserPreferences.Environment);
                    }
                    else if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.Financials)))
                    {
                        result.IsFinancialEnabled = item.IsPreferred;
                        result.Financial = EnumsHelper.GetDescription(UserPreferences.Financials);
                    }
                    else if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.Certificates)))
                    {
                        result.IsCertificateEnabled = item.IsPreferred;
                        result.Certificate = EnumsHelper.GetDescription(UserPreferences.Certificates);
                    }
                    else if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.Defects)))
                    {
                        result.IsDefectEnabled = item.IsPreferred;
                        result.Defect = EnumsHelper.GetDescription(UserPreferences.Defects);
                    }
                    else if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.PMS)))
                    {
                        result.IsPMSEnabled = item.IsPreferred;
                        result.PMS = EnumsHelper.GetDescription(UserPreferences.PMS);
                    }
                    else if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.Procurement)))
                    {
                        result.IsProcurementEnabled = item.IsPreferred;
                        result.Procurement = EnumsHelper.GetDescription(UserPreferences.Procurement);
                    }
                    else if (item.PreferenceId.Equals(EnumsHelper.GetKeyValue(UserPreferences.Inspections)))
                    {
                        result.IsInspectionEnabled = item.IsPreferred;
                        result.Inspection = EnumsHelper.GetDescription(UserPreferences.Inspections);
                    }

                }
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// Saves the user preferences.
        /// </summary>
        /// <param name="userPreferences">The user preferences.</param>
        /// <returns></returns>
        public async Task<IActionResult> SaveUserPreferences(List<UserPreferenceDetailViewModel> userPreferences)
        {
            _client.AccessToken = GetAccessToken();
            List<UserPreferenceDetailViewModel> response = await _client.GetUserPreferences();
            bool result = false;
            if (response != null && response.Any())
            {
                foreach (var input in userPreferences)
                {
                    input.MappingId = response.Where(x => x.PreferenceKey.Equals(input.PreferenceKey)).Select(x => x.MappingId).FirstOrDefault();
                    input.PreferenceId = response.Where(x => x.PreferenceKey.Equals(input.PreferenceKey)).Select(x => x.PreferenceId).FirstOrDefault();
                }
                result = await _client.SaveUserPreference(userPreferences);
            }
            string resultMsg = result ? Constants.PreferencesSavedSuccess : Constants.PreferencesSavedFailed;

            return new JsonResult(new { res = result, msg = resultMsg });
        }

        /// <summary>
        /// Modules the access denied.
        /// </summary>
        /// <returns></returns>
        public IActionResult ModuleAccessDenied()
        {
            return View();
        }

        //TODO: Need to check below code for session usability
        /// <summary>
        /// Set TimeZone Session
        /// </summary>
        /// <param name="timeZoneOffSet">The time zone off set.</param>
        /// <returns></returns>
        public EmptyResult SetTimeZoneSession(int timeZoneOffSet)
        {
            //SetSessionIntValue(Constants.TimeZoneDiffSessionKey, timeZoneOffSet);
            //HttpContext.Session.SetInt32(Constants.TimeZoneDiffSessionKey, timeZoneOffSet);
            Set(Constants.TimeZoneDiffSessionKey, timeZoneOffSet.ToString(), 30);
            return new EmptyResult();
        }

        /// <summary>
        /// Set time zone difference value in Cookies
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expireTime">The expire time.</param>
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }

        /// <summary>
        /// Adds the messaging user if not exists.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AddMessagingUserIfNotExists()
        {
            var startTime = DateTime.Now;

            _notificationClient.AccessToken = GetAccessToken();
            NotificationChannelSubscription userToBeAdded = new NotificationChannelSubscription();
            userToBeAdded.SSUserId = Request.Cookies["UserId"];
            userToBeAdded.Username = HttpContext.Session.GetString(Constants.UserNameSessionKey);
            userToBeAdded.UsrEmail = Request.Cookies["EmailId"];
            bool response = await _notificationClient.AddMessagingUserIfNotExists(userToBeAdded);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime)));

            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the reminder alert.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetReminderAlert()
        {
            _notificationClient.AccessToken = GetAccessToken();
            int timeZoneOffSet = 0;
            string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
            Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);
            List<ReminderAlertResponseViewModel> response = await _notificationClient.GetReminderAlert(timeZoneOffSet);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the default parameter create channel.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDefaultParameterCreateChannel()
        {
            NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
            {
                CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.General)),

                ApplicationId = AppSettings.ApplicationId,
                UserId = Request.Cookies["UserId"],
                NotificationJwtToken = GetAccessToken()
            };
            return new JsonResult(JsonConvert.SerializeObject(newMessageDetails));
        }

        /// <summary>
        /// Creates the parameter from notes.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public IActionResult CreateParameterFromNotes(string request)
        {
            NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
            {
                CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.General)),
                ApplicationId = AppSettings.ApplicationId,
                UserId = Request.Cookies["UserId"],
                NotificationJwtToken = GetAccessToken(),
                EncryptedNoteId = request
            };
            return new JsonResult(JsonConvert.SerializeObject(newMessageDetails));
        }

        /// <summary>
        /// Gets the default parameter notification.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns></returns>
        public IActionResult GetDefaultParameterNotification(int channelId)
        {
            NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
            {
                ChannelId = channelId,
                ApplicationId = AppSettings.ApplicationId,
                UserId = Request.Cookies["UserId"],
                NotificationJwtToken = GetAccessToken()
            };
            return new JsonResult(JsonConvert.SerializeObject(newMessageDetails));
        }

        /// <summary>
        /// Notifications the chat view.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public IActionResult NotificationChatView(string searchRequest)
        {
            NotificationChatViewModel viewModel = new NotificationChatViewModel() { UrlParameter = searchRequest, IsFilterChange = !string.IsNullOrWhiteSpace(searchRequest) };
            viewModel.SessionStorageDetails = SetSessionStorageDetail(_provider, viewModel);
            return View(viewModel);
        }

        /// <summary>
        /// Notifications the chat detail view.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public IActionResult NotificationChatDetailView(string searchRequest)
        {
            NotificationChatViewModel viewModel = new NotificationChatViewModel() { UrlParameter = searchRequest, IsFilterChange = !string.IsNullOrWhiteSpace(searchRequest) };
            viewModel.SessionStorageDetails = SetSessionStorageDetail(_provider, viewModel);
            return View(viewModel);
        }

        /// <summary>
        /// Notifications the chat discussion view.
        /// </summary>
        /// <param name="searchRequest">The search request.</param>
        /// <returns></returns>
        public IActionResult NotificationChatDiscussionView(string searchRequest)
        {
            NotificationChatViewModel viewModel = new NotificationChatViewModel() { UrlParameter = searchRequest, IsFilterChange = !string.IsNullOrWhiteSpace(searchRequest) };
            viewModel.SessionStorageDetails = SetSessionStorageDetail(_provider, viewModel);
            return View(viewModel);
        }

        /// <summary>
        /// Notifications the chat view partial.
        /// </summary>
        /// <returns></returns>
        public IActionResult NotificationChatViewPartial()
        {
            return PartialView("NotificationContainer");
        }

        /// <summary>
        /// Gets the unread messages.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetUnreadMessages()
        {
            _notificationClient.AccessToken = GetAccessToken();
            int timeZoneOffSet = 0;
            string cookieValueFromReq = Request.Cookies[Constants.TimeZoneDiffSessionKey];
            Int32.TryParse(cookieValueFromReq, out timeZoneOffSet);

            DataTablePageRequest<string> pageRequest = new DataTablePageRequest<string>();
            pageRequest.Length = 20;
            pageRequest.Start = 1;

            pageRequest.Columns = new List<Column>();
            pageRequest.Columns.Add(new Column() { Name = "ChannelTitle" });

            pageRequest.Order = new List<Order>();
            pageRequest.Order.Add(new Order()
            {
                Column = 0,
                Dir = "asc"
            });

            List<UnreadChannelMessageResponseViewModel> result = await _notificationClient.GetUnreadMessages(timeZoneOffSet, pageRequest);
            return new JsonResult(new { data = result });
        }

        /// <summary>
        /// Gets the unread channel count.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetUnreadChannelCount()
        {
            _notificationClient.AccessToken = GetAccessToken();
            int response = await _notificationClient.GetUnreadChannelCount();
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the record level discussion and notes counts.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetRecordLevelDiscussionAndNotesCounts(RecordLevelDiscussionCountViewModel request)
        {
            _notificationClient.AccessToken = GetAccessToken();
            RecordDiscussionResponseViewModel result = await _notificationClient.GetRecordLevelDiscussionAndNotesCount(request);
            return new JsonResult(result);
        }

        public IActionResult Demo()
        {
            return View();
        }

        /// <summary>
        /// Gets the list level record discussion count by reference identifier.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<RecordDiscussionResponse>> GetListLevelRecordDiscussionCountByReferenceId(List<NewMessageParametersViewModel> input)
        {
            RecordDiscussionRequestViewModel request = new RecordDiscussionRequestViewModel();
            request.CategoryId = input.Select(x => x.CategoryId).FirstOrDefault();
            request.ReferenceIds = input.Select(x => x.ReferenceIdentifier).Distinct().ToList();

            _notificationClient.AccessToken = GetAccessToken();
            List<RecordDiscussionResponse> discussionAndNotesCountList = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(request);

            return discussionAndNotesCountList;
        }

        /// <summary>
        /// Get encoded base64 string
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IActionResult GetEncodedBase64String(string request)
        {
            return new JsonResult(Convert.ToBase64String(Encoding.UTF8.GetBytes(request)));
        }

        /// <summary>
        /// The Get Approval Summary method
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetApprovalSummary(ApprovalSummaryRequestViewModel request)
        {
            _client.AccessToken = GetAccessToken();
            List<ApprovalSummaryResponseViewModel> response = await _client.GetApprovalSummary(request);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the encrypted fleet request.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public JsonResult GetEncryptedFleetRequest(DashboardParameter parameter)
        {
            string key = CommonUtil.GetEncryptedFleetRequest(_provider, parameter);
            return new JsonResult(key);
        }

        /// <summary>
        /// Gets the source URL for notification.
        /// </summary>
        /// <param name="sessionDetails">The page key.</param>
        /// <returns></returns>
        public IActionResult GetSourceURLForNotification(string sessionDetails)
        {
            return new JsonResult(CommonUtil.GetSessionStorageSourceURL(_provider, sessionDetails));
        }

        /// <summary>
        /// Gets the right ship GHG rating details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetRightShipGHGRatingDetails(RightShipRequestViewModel request)
        {
            var startTime = DateTime.Now;

            _technicalDashboardClient.AccessToken = GetAccessToken();
            List<FPHistoricDateDetailsViewModel> historicResponse = await _technicalDashboardClient.GetFPDateListForHistoricData(Convert.ToInt32(EnumsHelper.GetKeyValue(ReportType.KPIReports)));
            RightShipResponseViewModel response = new RightShipResponseViewModel();
            if (historicResponse != null && historicResponse.Any())
            {
                request.MonthDate = historicResponse.Max(x => x.MonthDate);
                response = await _technicalDashboardClient.GetRightShipGHGRatingDetails(request);
            }

            var endTime = DateTime.Now;
            var objDetails = CommonUtil.GetObjDetails(request);
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nRightShipRequestViewModel - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), objDetails));
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the vessel communcation.
        /// </summary>
        /// <param name="VesselId"></param>
        /// <param name="typeCategoryId"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetVesselCommunication(string VesselId, string typeCategoryId)
        {
            var startTime = DateTime.Now;

            _client.AccessToken = GetAccessToken();
            List<VesselCommunicationDetailViewModel> communicationDetails = await _client.GetVesselCommunications(VesselId, typeCategoryId);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nVesselId - {4} \ntypeCategoryId - {5} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), VesselId, typeCategoryId));

            return new JsonResult(communicationDetails);
        }

        /// <summary>
        /// Deletes the channel by identifier.
        /// </summary>
        /// <param name="channelId">The channel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteChannelById(string channelId)
        {
            var startTime = DateTime.Now;

            int intChannelId = Convert.ToInt16(channelId);

            _notificationClient.AccessToken = GetAccessToken();
            int applicationId = AppSettings.ApplicationId;
            NotificationChannel response = await _notificationClient.DeleteChannelById(intChannelId, applicationId);

            var endTime = DateTime.Now;
            string logTemplate = "ActionMethod: {0} \nStart Time: {1} \nEnd Time: {2} \nDuration: {3} \nParams: \nchannelId - {4} \n";
            AppendLog(CommonUtil.GetSessionObject<string>(HttpContext.Session, Constants.LogFileName), String.Format(logTemplate, this.ControllerContext.RouteData.Values["action"].ToString(), startTime.ToString(Constants.LogFileStartEndTimeFormat), endTime.ToString(Constants.LogFileStartEndTimeFormat), (endTime - startTime), channelId));

            return new JsonResult(response);
        }


        /// <summary>
        /// Get session storage filter for chat
        /// </summary>
        /// <param name="sessionDetails"></param>
        /// <returns></returns>
        public IActionResult GetSessionStorageFilterForChat(string sessionDetails)
        {
            return new JsonResult(CommonUtil.GetSessionStorageFilter<NotificationChatViewModel>(_provider, sessionDetails));
        }

        /// <summary>
        /// Set session storage filter for chat
        /// </summary>
        /// <param name="sessionDetails"></param>
        /// <param name="urlParameter"></param>
        /// <returns></returns>
        public IActionResult SetSessionStorageFilterForChat(string sessionDetails, string urlParameter)
        {
            NotificationChatViewModel filter = CommonUtil.GetSessionStorageFilter<NotificationChatViewModel>(_provider, sessionDetails);
            if (filter != null)
            {
                filter.UrlParameter = urlParameter;
            }
            return new JsonResult(CommonUtil.SetSessionStorageFilter(_provider, sessionDetails, filter));
        }

        /// <summary>Gets the sentinel value.</summary>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public async Task<IActionResult> GetSentinelValue(string VesselId)
        {
            _hseqManagerDashboardClient.AccessToken = GetAccessToken();
            var vesselId = CommonUtil.GetDecryptedVesselId(_provider, VesselId);
            VesselSentinelValueViewModel result = await _hseqManagerDashboardClient.GetVesselSentinelValueById(vesselId);
            return new JsonResult(result);
        }
    }
}