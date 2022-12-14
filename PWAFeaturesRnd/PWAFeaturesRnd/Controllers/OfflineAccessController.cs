using Microsoft.AspNetCore.Mvc;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.ViewModels.OfflineUrlMapping;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace PWAFeaturesRnd.Controllers
{
    public class OfflineAccessController : Controller
    {
        public static List<Views> Views { get; set; }
        public static List<Views> UserPreferences = new List<Views>();
        static void InitializeViewsDetails()
        {
            Views = new List<Views>();

            PWAFeaturesRnd.ViewModels.OfflineUrlMapping.Views DashboardView = new Views();
            DashboardView.ViewId = 1;
            DashboardView.ViewName = "Dashboard";
            DashboardView.Url = new OfflineModuleURL()
            {
                Url = "/Dashboard/index",
                toStoreInCache = true,
            };


            PWAFeaturesRnd.ViewModels.OfflineUrlMapping.Views Defects = new Views();
            Defects.ViewId = 3;
            Defects.ViewName = "Defect Details";
            Defects.Url = new OfflineModuleURL()
            {
                toStoreInCache = true,
                Url = "/Defect/Details"
            };

            PWAFeaturesRnd.ViewModels.OfflineUrlMapping.Views InspectionsAndAudit = new Views();
            InspectionsAndAudit.ViewId = 4;
            InspectionsAndAudit.ViewName = "Inspection & Audits Details";
            InspectionsAndAudit.Url = new OfflineModuleURL()
            {
                toStoreInCache = true,
                Url = "/Inspection/Findings"
            };

            PWAFeaturesRnd.ViewModels.OfflineUrlMapping.Views JobSafetyAnalysis = new Views();
            JobSafetyAnalysis.ViewId = 5;
            JobSafetyAnalysis.ViewName = "Job Safety & Analysis Details";
            JobSafetyAnalysis.Url = new OfflineModuleURL()
            {
                toStoreInCache = true,
                Url = "/JSA/Details"
            };

            PWAFeaturesRnd.ViewModels.OfflineUrlMapping.Views PlannedMaintainance = new Views();
            PlannedMaintainance.ViewId = 6;
            PlannedMaintainance.ViewName = "Planned Maintainance Details";
            PlannedMaintainance.Url = new OfflineModuleURL()
            {
                toStoreInCache = true,
                Url = "/PlannedMaintenance/Detail"
            };

            PWAFeaturesRnd.ViewModels.OfflineUrlMapping.Views PurchaseOrder = new Views();
            PurchaseOrder.ViewId = 7;
            PurchaseOrder.ViewName = "Purchase Order Details";
            PurchaseOrder.Url = new OfflineModuleURL()
            {
                toStoreInCache = true,
                Url = "/PurchaseOrder/Detail"
            };

            Modules keyIndicator = new Modules();
            keyIndicator.ModuleId = 1;
            keyIndicator.ModuleName = "Key Indicator";
            keyIndicator.OfflineModuleURLs = new List<OfflineModuleURL>()
            {
                new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetInspectionFleetSummary",
                    dbStoreName = "InspectionFleetSummary",
                    toStoreInDb = true,
                    requestDataString = "{ \"FleetId\":null, \"MenuType\":\"R\", \"VesselId\":null, \"PSCDetentionFromDate\":\"2022-09-14\", \"PSCDetentionToDate\":\"2022-12-14\",\"PSCDetentionPriorityLimit\":0, \"PSCDeficiencyFromDate\":\"2022-09-14\",\"PSCDeficiencyToDate\":\"2022-12-14\", \"PSCDeficiencyPriorityLimit\":0.88, \"OMVFindingsFromdate\":\"2022-06-14\", \"OMVFindingsToDate\":\"2022-12-14\", \"OMVFindingsPriorityHighLimit\":2.8, \"OMVFindingsPriorityLowLimit\":2.7, \"OverdueInspectionsPriorityLimit\":0 }"
                },
                 new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetCrewFleetSummary",
                    dbStoreName = "CrewFleetSummary",
                    toStoreInDb = true,
                    requestDataString = "{\"FleetId\" : null, \"VesselId\" : null, \"MenuType\" : \"R\"}"
                },
                  new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetOpexFleetSummary",
                    dbStoreName = "OpexFleetSummary",
                    toStoreInDb = true,
                    requestDataString = "{ \"FleetId\" : null, \"VesselId\" : null, \"MenuType\" : \"R\", \"BudgetDays\" : 90, \"BudgetPercentageHighLimit\" : 10, \"BudgetPercentageLowLimit\" : 5, \"OpexToDate\" : \"2022-10-31\" }"
                },
                   new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetHazOccFleetSummary",
                    dbStoreName = "HazOccFleetSummary",
                    toStoreInDb = true,
                    requestDataString = "{ \"FleetId\" : null, \"VesselId\" : null,  \"MenuType\" : \"R\", \"IncidentEndDate\" : \"2022-12-14\", \"IncidentStartDate\" : \"2022-09-14\", \"LtiFromDate\" : \"2021-12-14\", \"LtiToDate\" : \"2022-12-14\", \"LtifPriority\" : 7, \"OilSpillFromDate\" : \"2022-09-14\", \"OilSpillPriorityLimit\" : 0, \"OilSpillToDate\" : \"2022-12-14\", \"SeriousIncidentsPriority\" : 0 }"
                },
                 new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetCommercialFleetSummary",
                    dbStoreName = "CommercialFleetSummary",
                    toStoreInDb = true,
                    requestDataString = "{ \"FleetId\" : null, \"FuelEfficiencyFromDate\" : \"2022-09-14\", \"FuelEfficiencyPriorityHighLimit\" : 5, \"FuelEfficiencyPriorityLowLimit\" : 0, \"FuelEfficiencyToDate\" : \"2022-12-14\", \"MenuType\" : \"R\", \"OffHireEndDate\" : \"2022-12-14\", \"OffHirePriority\" : \"00:17:26\", \"OffHireStartDate\" : \"2022-09-14\", \"VesselId\" : null }"
                },
                 new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetRightshipFleetSummary",
                    dbStoreName = "RightshipFleetSummary",
                    toStoreInDb = true,
                    requestDataString = "{ \"FleetId\" : null,  \"VesselId\" : null, \"MenuType\" : \"R\",  \"RightShipPriority\" : 3 }"
                },
                 new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetPMSFleetSummary",
                    dbStoreName = "PMSFleetSummary",
                    toStoreInDb = true,
                    requestDataString = "{\"FleetId\":null,\"MenuType\":\"R\",\"VesselId\":null,\"CriticalPmspriority\":0}"
                },
                 new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetSeriousIncidents",
                    dbStoreName = "SeriousIncidentDetails",
                    toStoreInDb = true,
                    requestDataString = "{ \"IncidentStartDate\":\"0001-01-01\", \"IncidentEndDate\": \"0001-01-01\", \"FleetId\":null, \"MenuType\":\"R\", \"VesselId\":null}"
                },
                  new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetRightShipDetails",
                    dbStoreName = "RightShipDetails",
                    toStoreInDb = true,
                    requestDataString = "{\"FleetId\":null,\"MenuType\":\"R\",\"VesselId\":null,\"MonthDate\":null}"
                },
                   new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetOverdueInspectionDetails",
                    dbStoreName = "OverdueInspectionDetails",
                    toStoreInDb = true,
                    requestDataString = "{\"MenuType\":\"R\",\"FleetId\":null,\"EncryptedVesselId\":null}"
                },
             new OfflineModuleURL()
                {
                    Url = "/Dashboard/GetCriticalPMS",
                    dbStoreName = "CriticalPMS",
                    toStoreInDb = true,
                    requestDataString = "{\"VesselId\":null,\"MenuType\":\"R\",\"FleetId\":null}"
                }
            };

            DashboardView.Modules.Add(keyIndicator);

            Modules Chat = new Modules();
            Chat.ModuleId = 2;
            Chat.ModuleName = "Chat";
            DashboardView.Modules.Add(Chat);

            //DashboardView.Modules.Add(ApprovalModule);

            Modules DefectsApprovalModule = new Modules();
            DefectsApprovalModule.ModuleName = "Defect Approval";
            DefectsApprovalModule.ModuleId = 3;
            DefectsApprovalModule.Views.Add(Defects);

            //ApprovalView.Modules.Add(DefectsApprovalModule);

            Modules InspectionsAndAuditApprovalModule = new Modules();
            InspectionsAndAuditApprovalModule.ModuleName = "Inspection & Audit";
            InspectionsAndAuditApprovalModule.ModuleId = 4;
            InspectionsAndAuditApprovalModule.Views.Add(InspectionsAndAudit);
            //ApprovalView.Modules.Add(InspectionsAndAuditApprovalModule);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Modules JobSafetyAnalysislModule = new Modules();
            JobSafetyAnalysislModule.ModuleName = "Job Safety Analysis";
            JobSafetyAnalysislModule.ModuleId = 5;
            JobSafetyAnalysislModule.Views.Add(JobSafetyAnalysis);

            //ApprovalView.Modules.Add(JobSafetyAnalysislModule);

            Modules PlannedMaintainanceModule = new Modules();
            PlannedMaintainanceModule.ModuleName = "Planned Maintainance";
            PlannedMaintainanceModule.ModuleId = 6;
            PlannedMaintainanceModule.Views.Add(PlannedMaintainance);

            //ApprovalView.Modules.Add(PlannedMaintainanceModule);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            Modules PurchaseOrderModule = new Modules();
            PurchaseOrderModule.ModuleName = "Purchase Order";
            PurchaseOrderModule.ModuleId = 7;
            PurchaseOrderModule.Views.Add(PurchaseOrder);

            //ApprovalView.Modules.Add(PurchaseOrderModule);

            Views.AddRange(new List<Views>()
            {
                DashboardView,
                //ApprovalView,
                Defects,
                InspectionsAndAudit,
                JobSafetyAnalysis,
                PlannedMaintainance,
                PurchaseOrder
            });
        }
        public OfflineAccessController()
        {
            InitializeViewsDetails();
        }

        public ActionResult GetAppOfflineURLs(List<OfflineRequestVM> data)
        {
            List<OfflineModuleURL> Urls = new List<OfflineModuleURL>();
            var viewids = data.Where(x => x.viewid != null).Select(x => Convert.ToInt32(x.viewid)).ToList();
            var moduleids = data.Where(x => x.moduleid != null).Select(x => Convert.ToInt32(x.moduleid)).ToList();
            foreach (var view in Views)
            {
                AddUrlsToList(view, viewids, moduleids, Urls);
            }
            return new JsonResult(new { success = true, message = "Successfull", urls = Urls });
        }

        public async Task<IActionResult> GetOfflineDetailsModal()
        {
            string viewResult = await this.RenderViewToStringAsync("OfflineModal", Views);
            return new JsonResult(new { view = viewResult });
        }

        private void AddUrlsToList(Views view, List<int> viewIds, List<int> ModuleIds, List<OfflineModuleURL> Urls)
        {
            if (viewIds.Contains(view.ViewId))
            {
                if (!Urls.Select(x => x.Url).Contains(view.Url.Url))
                {
                    Urls.Add(view.Url);
                }
                var Modules = view.Modules.Where(x => ModuleIds.Contains(x.ModuleId)).ToList();
                if (Modules.Any())
                {
                    foreach (var module in Modules)
                    {
                        foreach (var url in module.OfflineModuleURLs)
                        {
                            if (!Urls.Select(x => x.Url).Contains(url.Url))
                            {
                                Urls.Add(url);
                            }
                        }
                        List<Views> nestedViews = module.Views.Where(x => viewIds.Contains(x.ViewId)).ToList();
                        if (nestedViews.Any())
                        {
                            foreach (var nestedView in nestedViews)
                            {
                                AddUrlsToList(nestedView, viewIds, ModuleIds, Urls);
                            }
                        }
                    }
                }
            }
        }
    }
}
