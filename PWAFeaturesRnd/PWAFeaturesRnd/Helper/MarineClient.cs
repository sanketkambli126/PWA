using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.DataAttributes;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Common.Paging;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.Approval;
using PWAFeaturesRnd.Models.Report.Certificate;
using PWAFeaturesRnd.Models.Report.Defect;
using PWAFeaturesRnd.Models.Report.Environment;
using PWAFeaturesRnd.Models.Report.HazardousOccurrences;
using PWAFeaturesRnd.Models.Report.InspectionManager;
using PWAFeaturesRnd.Models.Report.JSA;
using PWAFeaturesRnd.Models.Report.PlannedMaintenance;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.Models.Report.Vessel;
using PWAFeaturesRnd.Models.Report.VesselManagement;
using PWAFeaturesRnd.Models.Report.VoyageReporting;
using PWAFeaturesRnd.ViewModels.Approval;
using PWAFeaturesRnd.ViewModels.Certificate;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Defect;
using PWAFeaturesRnd.ViewModels.Enviroment;
using PWAFeaturesRnd.ViewModels.ExportToExcel;
using PWAFeaturesRnd.ViewModels.HazardousOccurrences;
using PWAFeaturesRnd.ViewModels.HazOcc;
using PWAFeaturesRnd.ViewModels.Inspection;
using PWAFeaturesRnd.ViewModels.JSA;
using PWAFeaturesRnd.ViewModels.PlannedMaintenance;
using PWAFeaturesRnd.ViewModels.PurchaseOrder;
using PWAFeaturesRnd.ViewModels.Vessel;
using PWAFeaturesRnd.ViewModels.VesselManagement;
using PWAFeaturesRnd.ViewModels.VoyageReporting;

namespace PWAFeaturesRnd.Helper
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Helper.BaseHttpClient" />
    public class MarineClient : BaseHttpClient
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarineClient" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="provider">The provider.</param>
        public MarineClient(HttpClient client, IConfiguration configuration, IDataProtectionProvider provider, IHttpContextAccessor httpContextAccessor) : base(client, true, httpContextAccessor)
        {
            client.BaseAddress = new Uri(AppSettings.MarineWebApiUrl);
            _client = client;
            _configuration = configuration;
            _provider = provider;
        }

        /// <summary>
        /// Gets the encrypted vessel.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="vesselName">Name of the vessel.</param>
        /// <param name="coyId">The coy identifier.</param>
        /// <returns></returns>
        private string GetEncryptedVessel(string vesselId, string vesselName, string coyId)
        {
            string encryptedVessel = _provider.CreateProtector("Vessel").Protect(vesselId + Constants.Separator + vesselName + " - " + coyId + Constants.Separator + coyId);
            return encryptedVessel;
        }

        #region Inspection

        /// <summary>
        /// Gets the encrypted inspection action URL.
        /// </summary>
        /// <param name="actionRequest">The action request.</param>
        /// <returns></returns>
        private string GetEncryptedInspectionActionURL(InspectionActionRequestViewModel actionRequest)
        {
            string encryptedActionUrl = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(actionRequest));
            return encryptedActionUrl;
        }

        /// <summary>
        /// Gets the inspection overview planning detail.
        /// </summary>
        /// <param name="inspectionOverviewPlanningRequest">The inspection overview planning request.</param>
        /// <returns></returns>
        public async Task<List<InspectionViewModel>> GetInspectionOverviewPlanningDetail(InspectionOverviewPlanningRequest inspectionOverviewPlanningRequest)
        {
            inspectionOverviewPlanningRequest.FromDate = DateTime.Now.AddDays(-29);
            inspectionOverviewPlanningRequest.ToDate = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(inspectionOverviewPlanningRequest.TypeIds))
            {
                inspectionOverviewPlanningRequest.InspectionTypeIds = inspectionOverviewPlanningRequest.TypeIds.Split(",").ToList();
            }
            else
            {
                inspectionOverviewPlanningRequest.InspectionTypeIds = null;
            }

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/InspectionOverviewPlanning"));
            List<InspectionOverviewPlanningResponse> response = await PostAsync<List<InspectionOverviewPlanningResponse>>(requestUrl, CreateHttpContent(inspectionOverviewPlanningRequest));

            List<InspectionViewModel> result = new List<InspectionViewModel>();
            InspectionFindingRequestViewModel findingRequest;

            response.ForEach(x =>
            {
                findingRequest = new InspectionFindingRequestViewModel();
                findingRequest.InspectionId = x.InspectionId;
                findingRequest.VesselId = x.VesselId;
                findingRequest.InspectionTypeId = x.InspectionTypeId;
                findingRequest.FromDate = inspectionOverviewPlanningRequest.FromDate;
                findingRequest.ToDate = inspectionOverviewPlanningRequest.ToDate;
                findingRequest.InspectionType = inspectionOverviewPlanningRequest.InspectionType;
                findingRequest.IsPlanningOrClouser = "1";
                findingRequest.InspectionName = x.InspectionType;
                findingRequest.OccuredDate = x.OccuredDate != null ? x.OccuredDate.Value.ToString("dd MMM yyyy") : "";

                result.Add(new InspectionViewModel
                {
                    FindingURL = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(findingRequest)),
                    VesselId = _provider.CreateProtector("Vessel").Protect(x.VesselId + "¥" + inspectionOverviewPlanningRequest.VesselName),
                    VesselName = x.VesselName ?? "",
                    InspectionId = x.InspectionId,
                    InspectionTypeId = x.InspectionTypeId,
                    InspectionType = x.InspectionType ?? "",
                    DefaultInterval = x.DefaultInterval.GetValueOrDefault(),
                    OccuredDate = x.OccuredDate != null ? x.OccuredDate.Value.ToString("dd-MMM-yyyy") : "",
                    NextDueDate = x.NextDueDate != null ? x.NextDueDate.Value.ToString("dd-MMM-yyyy") : "",
                    FromLocation = x.FromLocation,
                    ToLocation = x.ToLocation,
                    Location = x.FromLocation + ((!string.IsNullOrWhiteSpace(x.ToLocation)) ? " to " + x.ToLocation : ""),
                    CompanyName = x.CompanyName ?? "",
                    InspectionStatus = x.InspectionStatus ?? "",
                    ManagementStartDate = x.ManagementStartDate != null ? x.ManagementStartDate.Value.ToString("dd-MMM-yyyy") : "",
                    OccuredDateType = x.OccuredDate,
                    NextDueDateType = x.NextDueDate,
                    Interval = x.DefaultInterval.GetValueOrDefault() > 0 ? x.DefaultInterval.Value + " M" : "0",
                    ManagementStartDateType = x.ManagementStartDate
                });
            });
            return result;
        }

        /// <summary>
        /// Gets the inspection findings by inspection identifier.
        /// </summary>
        /// <param name="inspectionFindingRequest">The inspection finding request.</param>
        /// <returns></returns>
        public async Task<List<InspectionFindingResponseViewModel>> GetInspectionFindingsByInspectionId(InspectionFindingRequestViewModel inspectionFindingRequest)
        {
            string data = _provider.CreateProtector("Inspection").Unprotect(inspectionFindingRequest.InspectionUrl);
            InspectionFindingRequestViewModel inspectionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionFindingRequestViewModel>(data);
            inspectionFindingRequest.InspectionId = inspectionRequest.InspectionId;

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/Finding"));
            List<InspectionFindingResponse> response = await PostAsync<List<InspectionFindingResponse>>(requestUrl, CreateHttpContent(inspectionFindingRequest));

            List<InspectionFindingResponseViewModel> result = new List<InspectionFindingResponseViewModel>();
            InspectionActionRequestViewModel actionRequest;

            response.ForEach(x =>
            {
                actionRequest = new InspectionActionRequestViewModel();
                actionRequest.InspectionId = x.InspectionId;
                actionRequest.VesselId = x.VesselId;
                actionRequest.InspectionTypeId = x.InspectionTypeId;
                actionRequest.FromDate = inspectionRequest.FromDate;
                actionRequest.ToDate = inspectionRequest.ToDate;
                actionRequest.InspectionType = inspectionRequest.InspectionType;
                actionRequest.IsPlanningOrClouser = inspectionRequest.IsPlanningOrClouser;
                actionRequest.InspectionName = inspectionRequest.InspectionName;
                actionRequest.VesselReferenceNo = x.VesselReferenceNo;
                actionRequest.OccuredDate = inspectionRequest.OccuredDate;
                actionRequest.InspectionFindingId = x.InspectionFindingId;
                actionRequest.InspectionFindingFilter = inspectionFindingRequest.InspectionFindingFilter;
                actionRequest.IsFindingOutstanding = inspectionFindingRequest.IsFindingOutstanding;
                actionRequest.IsFindingOverdue = inspectionFindingRequest.IsFindingOverdue;
                actionRequest.IsPendingClosure = inspectionFindingRequest.IsPendingClosure;
                actionRequest.IsClosed = inspectionFindingRequest.IsClosed;
                actionRequest.IsAllSelected = inspectionFindingRequest.IsAllSelected;
                actionRequest.IsDetention = inspectionFindingRequest.IsDetention;
                actionRequest.IsShowDetained = inspectionFindingRequest.IsShowDetained;
                actionRequest.IsAtPort = inspectionFindingRequest.IsAtPort;
                actionRequest.IsAtSea = inspectionFindingRequest.IsAtSea;
                actionRequest.IsDue = inspectionFindingRequest.IsDue;
                actionRequest.IsOverdue = inspectionFindingRequest.IsOverdue;
                actionRequest.strInspectionTypeIds = inspectionFindingRequest.strInspectionTypeIds;

                result.Add(new InspectionFindingResponseViewModel
                {
                    ActionUrl = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(actionRequest)),
                    InspectionId = x.InspectionId,
                    InspectionTypeId = x.InspectionTypeId,
                    VesselId = _provider.CreateProtector("Vessel").Protect(x.VesselId + "¥" + inspectionFindingRequest.VesselName),
                    InspectionFindingId = x.InspectionFindingId,
                    InspectionFindingTypeName = x.InspectionFindingTypeName ?? "",
                    VesselReferenceNo = x.VesselReferenceNo ?? "",
                    InspectionReferenceNo = x.InspectionReferenceNo ?? "",
                    Description = x.Description ?? "",
                    RiskAssessmentCategoryName = x.RiskAssessmentCategoryName ?? "",
                    PscActionCode = x.PscActionCode ?? "",
                    PscGroupCode = x.PscGroupCode ?? "",
                    RiskAssessmentAreaName = x.RiskAssessmentAreaName ?? "",
                    DateDue = x.DateDue != null ? x.DateDue.Value.ToString("dd-MMM-yyyy") : "",
                    DateCleared = x.DateCleared != null ? x.DateCleared.Value.ToString("dd-MMM-yyyy") : ""
                });
            });
            return result;
        }

        /// <summary>
        /// Gets the finding actions by finding identifier.
        /// </summary>
        /// <param name="inspectionActionRequestViewModel">The inspection action request view model.</param>
        /// <returns></returns>
        public async Task<List<InspectionActionResponseViewModel>> GetFindingActionsByFindingId(InspectionActionRequestViewModel inspectionActionRequestViewModel)
        {
            string data = _provider.CreateProtector("Inspection").Unprotect(inspectionActionRequestViewModel.InspectionUrl);
            InspectionActionRequestViewModel inspectionRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<InspectionActionRequestViewModel>(data);
            inspectionActionRequestViewModel.InspectionFindingId = inspectionRequest.InspectionFindingId;

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/Actions"));
            List<InspectionActionResponse> response = await PostAsync<List<InspectionActionResponse>>(requestUrl, CreateHttpContent(inspectionActionRequestViewModel));

            List<InspectionActionResponseViewModel> result = new List<InspectionActionResponseViewModel>();

            response.ForEach(x =>
            {
                result.Add(new InspectionActionResponseViewModel
                {
                    InspectionId = x.InspectionId,
                    InspectionFindingId = x.InspectionFindingId,
                    VesselId = x.VesselId,
                    ActionDate = x.ActionDate != null ? x.ActionDate.Value.ToString("dd-MMM-yyyy") : "",
                    ActionDescription = x.ActionDescription ?? "",
                    IsClear = x.IsClear ? "Yes" : "No",
                    IsDeleted = x.IsDeleted,
                    ReportedBy = x.ReportedBy ?? ""
                });
            });
            return result;
        }

        /// <summary>
        /// Gets the inspection findings count by inspection identifier.
        /// </summary>
        /// <param name="inspectionId">The inspection identifier.</param>
        /// <returns></returns>
        public async Task<InspectionFindingSummaryResponseViewModel> GetInspectionFindingsCountByInspectionId(string inspectionId)
        {
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/FindingSummary"));
            InspectionFindingSummaryResponse response = await PostAsync<InspectionFindingSummaryResponse>(requestUrl, CreateHttpContent(inspectionId));

            InspectionFindingSummaryResponseViewModel result = new InspectionFindingSummaryResponseViewModel()
            {
                AllFindingCount = response.AllFindingCount,
                ClearedCount = response.ClearedCount,
                OutstandingCount = response.OutstandingCount,
                OverdueCount = response.OverdueCount
            };

            return result;
        }

        /// <summary>
        /// Gets the vessel inspection detail list.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="inspectionRequestViewModel">The inspection request view model.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<VesselInspectionViewModel>>> GetVesselInspectionDetailList(DataTablePageRequest<string> pageRequest, InspectionRequestViewModel inspectionRequestViewModel)
        {
            //PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

            InspectionsOverviewForVesselsRequest inspectionsOverviewForVesselsRequest = new InspectionsOverviewForVesselsRequest();
            inspectionsOverviewForVesselsRequest.VesselId = inspectionRequestViewModel.VesselId;
            inspectionsOverviewForVesselsRequest.FleetId = inspectionRequestViewModel.FleetId;
            inspectionsOverviewForVesselsRequest.MenuType = inspectionRequestViewModel.MenuType;
            inspectionsOverviewForVesselsRequest.StartDate = inspectionRequestViewModel.FromDate;
            inspectionsOverviewForVesselsRequest.EndDate = inspectionRequestViewModel.ToDate;
            inspectionsOverviewForVesselsRequest.InDays = inspectionRequestViewModel.InDays;
            inspectionsOverviewForVesselsRequest.InspectionTypeIds = inspectionRequestViewModel.InspectionTypeIds;
            inspectionsOverviewForVesselsRequest.IsFindingOutstanding = inspectionRequestViewModel.IsFindingOutstanding;
            inspectionsOverviewForVesselsRequest.IsFindingOverdue = inspectionRequestViewModel.IsFindingOverdue;
            inspectionsOverviewForVesselsRequest.IsPendingClosure = inspectionRequestViewModel.IsPendingClosure;
            inspectionsOverviewForVesselsRequest.IsClosed = inspectionRequestViewModel.IsClosed;
            inspectionsOverviewForVesselsRequest.IsAllSelected = inspectionRequestViewModel.IsAllSelected;
            inspectionsOverviewForVesselsRequest.IsDetention = inspectionRequestViewModel.IsDetention;
            inspectionsOverviewForVesselsRequest.IsShowDetained = inspectionRequestViewModel.IsShowDetained;
            inspectionsOverviewForVesselsRequest.IsAtPort = inspectionRequestViewModel.IsAtPort;
            inspectionsOverviewForVesselsRequest.IsAtSea = inspectionRequestViewModel.IsAtSea;
            inspectionsOverviewForVesselsRequest.IsDue = inspectionRequestViewModel.IsDue;
            inspectionsOverviewForVesselsRequest.IsOverdue = inspectionRequestViewModel.IsOverdue;
            inspectionsOverviewForVesselsRequest.Company = inspectionRequestViewModel.Company;
            inspectionsOverviewForVesselsRequest.CompanyId = inspectionRequestViewModel.CompanyId;
            inspectionsOverviewForVesselsRequest.InspectorName = inspectionRequestViewModel.Inspector;
            inspectionsOverviewForVesselsRequest.InspectionType = inspectionRequestViewModel.InspectionTypeTextField;
            inspectionsOverviewForVesselsRequest.IsOMVRejection = inspectionRequestViewModel.IsOMVRejection;
            inspectionsOverviewForVesselsRequest.IsPSCDeficency = inspectionRequestViewModel.IsPSCDeficency;

            var input = new Dictionary<string, object>()
            {
                { "filter", inspectionsOverviewForVesselsRequest }//,
                //{ "pageRequest", pagedRequest }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/AllInspections"));
            List<VesselInspectionMenuItem> response = await PostAsyncAutoPaged<VesselInspectionMenuItem>(requestUrl, input, 500);

            DataTablePageResponse<List<VesselInspectionViewModel>> result = new DataTablePageResponse<List<VesselInspectionViewModel>>();
            result.Data = new List<VesselInspectionViewModel>();
            InspectionFindingRequestViewModel findingRequest;

            if (response != null)
            {
                response.ForEach(x =>
                {
                    findingRequest = new InspectionFindingRequestViewModel();
                    findingRequest.InspectionId = x.InspectionId;
                    findingRequest.VesselId = x.VesselId;
                    findingRequest.InspectionTypeId = x.InspectionTypeId;
                    findingRequest.FromDate = inspectionRequestViewModel.FromDate;
                    findingRequest.ToDate = inspectionRequestViewModel.ToDate;
                    findingRequest.InspectionType = inspectionRequestViewModel.InspectionType;
                    findingRequest.IsPlanningOrClouser = "2";
                    findingRequest.InspectionName = x.InspectionTypeName;
                    findingRequest.OccuredDate = x.InspectionDate.ToString("dd MMM yyyy");
                    findingRequest.InspectionTypeIds = null;
                    findingRequest.IsFindingOutstanding = inspectionRequestViewModel.IsFindingOutstanding;
                    findingRequest.IsFindingOverdue = inspectionRequestViewModel.IsFindingOverdue;
                    findingRequest.IsPendingClosure = inspectionRequestViewModel.IsPendingClosure;
                    findingRequest.IsClosed = inspectionRequestViewModel.IsClosed;
                    findingRequest.IsAllSelected = inspectionRequestViewModel.IsAllSelected;
                    findingRequest.IsDetention = inspectionRequestViewModel.IsDetention;
                    findingRequest.IsShowDetained = inspectionRequestViewModel.IsShowDetained;
                    findingRequest.IsAtPort = inspectionRequestViewModel.IsAtPort;
                    findingRequest.IsAtSea = inspectionRequestViewModel.IsAtSea;
                    findingRequest.IsDue = inspectionRequestViewModel.IsDue;
                    findingRequest.IsOverdue = inspectionRequestViewModel.IsOverdue;
                    findingRequest.strInspectionTypeIds = inspectionRequestViewModel.strInspectionTypeIds;
                    findingRequest.TplId = x.TplId;

                    result.Data.Add(new VesselInspectionViewModel
                    {
                        FindingURL = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(findingRequest)),
                        VesselId = inspectionRequestViewModel.VesselName == null ? _provider.CreateProtector("Vessel").Protect(x.VesselId + "¥" + x.VesselName + "-") : _provider.CreateProtector("Vessel").Protect(x.VesselId + "¥" + inspectionRequestViewModel.VesselName),
                        VesselName = x.VesselName,
                        InspectionId = x.InspectionId,
                        InspectionTypeId = x.InspectionTypeId,
                        InspectionTypeName = x.InspectionTypeName,
                        InspectionDate = x.InspectionDate.ToString("dd-MMM-yyyy"),
                        NextDueDate = x.NextDueDate == null ? "" : x.NextDueDate.Value.ToString("dd-MMM-yyyy"),
                        DateClosed = x.DateClosed == null ? "" : x.DateClosed.Value.ToString("dd-MMM-yyyy"),
                        Location = x.Location,
                        CompanyId = x.CompanyId,
                        CompanyName = x.CompanyName,
                        OMAId = x.OMAId,
                        ActionCode = x.ActionCode,
                        TplId = x.TplId,
                        IsRootAndDirectClauseMandatory = x.IsRootAndDirectClauseMandatory,
                        Status = x.Status,
                        VesselBuilt = x.VesselBuilt,
                        VesselType = x.VesselType,
                        TotalRecords = x.TotalRecords,
                        IsInspectionOverdue = x.IsInspectionOverdue,
                        TotalFindingCount = x.TotalFindingCount,
                        OutStandingFindingCount = x.OutStandingFindingCount,
                        OverdueFindingCount = x.OverdueFindingCount,
                        DaysDetained = x.DaysDetained,
                        IsPSCDetention = x.InspectionTypeId == EnumsHelper.GetKeyValue(InspectionType.PortStateControl)
                    });
                });
            }
            result.RecordsFiltered = response.Count;
            result.RecordsTotal = response.Count;

            return result;
        }

        /// <summary>
        /// Posts the get due filter.
        /// </summary>
        /// <param name="overviewFilter">The overview filter.</param>
        /// <returns></returns>
        public async Task<List<InspectionOverviewFilterResponseViewModel>> PostGetDueFilter(InspectionManagerOverviewFilter overviewFilter)
        {
            List<InspectionOverviewFilterResponseViewModel> responseVMList = new List<InspectionOverviewFilterResponseViewModel>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/DueFilter"));
            List<InspectionOverviewFilterResponse> response = await PostAsync<List<InspectionOverviewFilterResponse>>(requestUrl, CreateHttpContent(overviewFilter));

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    responseVMList.Add(new InspectionOverviewFilterResponseViewModel
                    {
                        Description = x.Description,
                        IsDefault = x.IsDefault,
                        Value = x.Value
                    });
                });
            }
            return responseVMList;
        }

        /// <summary>
        /// Gets the inspection type filter.
        /// </summary>
        /// <returns></returns>
        public async Task<List<InspectionTypeDetailViewModel>> GetInspectionTypeFilter()
        {
            List<InspectionTypeDetailViewModel> responseVMList = new List<InspectionTypeDetailViewModel>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/Inspectiontype"));
            List<InspectionTypeDetail> response = await GetAsync<List<InspectionTypeDetail>>(requestUrl);

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    responseVMList.Add(new InspectionTypeDetailViewModel
                    {
                        InspectionHeaderType = x.InspectionHeaderType,
                        InspectionType = x.InspectionType,
                        InspectionTypeId = x.InspectionTypeId,
                        IsAuditType = x.IsAuditType,
                        IsInternal = x.IsInternal,
                        Type = x.Type
                    });
                });
            }
            return responseVMList;
        }

        /// <summary>
        /// Posts the get inspection manager dashboard detail.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<InspectionManagerDashboardDetailViewModel> PostGetInspectionManagerDashboardDetail(InspectionManagerDashboardRequestViewModel inputRequest)
        {
            //to get the count of inspection dashboard
            InspectionManagerDashboardDetailViewModel inspectionDetails = new InspectionManagerDashboardDetailViewModel();
            InspectionManagerDashboardDetail response = null;
            InspectionManagerDashboardRequest request = new InspectionManagerDashboardRequest();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(inputRequest.EncryptedVesselId);

            UserMenuItem menuItem = new UserMenuItem();
            menuItem.DisplayText = decreptedString.Split(Constants.Separator)[1];
            menuItem.Identifier = decreptedString.Split(Constants.Separator)[0];
            menuItem.UserMenuItemType = UserMenuItemType.Vessel;
            request.Item = menuItem;

            request.FromDate = inputRequest.FromDate;
            request.ToDate = inputRequest.ToDate;

            //need confirmation
            request.MenuType = "";

            request.VesselId = decreptedString.Split(Constants.Separator)[0];


            List<InspectionOverviewFilterResponseViewModel> DueInTypeList = await PostGetDueFilter(InspectionManagerOverviewFilter.InspectionManagerDueInDays);
            InspectionOverviewFilterResponseViewModel DueFilter = null;
            //need to ask about default value
            if (DueInTypeList != null && DueInTypeList.Any())
            {
                DueFilter = DueInTypeList.Any(x => x.IsDefault) ? DueInTypeList.Where(x => x.IsDefault).FirstOrDefault() : DueInTypeList.Any(x => x.Value == 30) ? DueInTypeList.Where(x => x.Value == 30).FirstOrDefault() : DueInTypeList.FirstOrDefault();
            }
            request.InDays = DueFilter != null ? DueFilter.Value : 0;

            //inspcetion type ids will be null 
            request.InspectionTypeList = null;

            //need to ask about this
            request.IsShowDetainedVessel = false;

            //will this be set true because we are checking screen of dashboard?
            request.IsFromOverview = true;
            request.IsFromDashboard = inputRequest.IsFromDashboard;
            if (inputRequest.IsFromDashboard == true)
            {
                request.PSCDetentionFromDate = inputRequest.PSCDetentionFromDate;
                request.PSCDetentionToDate = inputRequest.PSCDetentionToDate;
                request.DeficienciesPerOMVFromDate = inputRequest.FromDate;
                request.DeficienciesPerOMVToDate = inputRequest.ToDate;
                request.DeficienciesPerPSCFromDate = inputRequest.DeficienciesPerPSCFromDate;
                request.DeficienciesPerPSCToDate = inputRequest.DeficienciesPerPSCToDate;
                request.PscDeficiencyFromDate = inputRequest.PscDeficiencyFromDate;
                request.PscDeficiencyToDate = inputRequest.PscDeficiencyToDate;
                request.OmvRejectionFromDate = inputRequest.OmvRejectionFromDate;
                request.OmvRejectionToDate = inputRequest.OmvRejectionToDate;
                request.DeficienciesPerPscPriorityHighLimit = inputRequest.DeficienciesPerPscPriorityHighLimit;
                request.DeficienciesPerPscPriorityMidLimit = inputRequest.DeficienciesPerPscPriorityMidLimit;
                request.DeficienciesPerPscPriorityLowLimit = inputRequest.DeficienciesPerPscPriorityLowLimit;
                request.DeficienciesPerOmvPriorityHighLimit = inputRequest.DeficienciesPerOmvPriorityHighLimit;
                request.DeficienciesPerOmvPriorityMidLimit = inputRequest.DeficienciesPerOmvPriorityMidLimit;
                request.DeficienciesPerOmvPriorityLowLimit = inputRequest.DeficienciesPerOmvPriorityLowLimit;
                request.OverdueFindingsPriorityLimit = inputRequest.OverdueFindingsPriorityLimit;
                request.OverdueInspectionsPriorityLimit = inputRequest.OverdueInspectionsPriorityLimit;
            }

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/Summary"));
            response = await PostAsync<InspectionManagerDashboardDetail>(requestUrl, CreateHttpContent(request));

            if (response != null && response.HeaderStatisticDetail != null)
            {

                InspectionRequestViewModel inspectionURLRequest = new InspectionRequestViewModel();
                inspectionURLRequest.FromDate = inputRequest.FromDate;
                inspectionURLRequest.ToDate = inputRequest.ToDate;
                inspectionURLRequest.VesselId = decreptedString.Split(Constants.Separator)[0];
                inspectionURLRequest.IsSummaryClicked = true;

                //psc
                inspectionDetails.TotalPscInspectionCount = response.HeaderStatisticDetail.TotalPscInspectionCount;
                inspectionDetails.OpenPSCInspectionCount = response.HeaderStatisticDetail.OpenPSCInspectionCount.GetValueOrDefault();
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.DetentionType);
                inspectionDetails.DetentionTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.DetentionType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionPscDeficiencyType);
                inspectionDetails.InspectionTypePscURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionPscDeficiencyType);

                //omv
                inspectionDetails.TotalOmvInspectionCount = response.HeaderStatisticDetail.TotalOmvInspectionCount;
                inspectionDetails.OpenOMVInspectionCount = response.HeaderStatisticDetail.OpenOMVInspectionCount == null ? "0" : response.HeaderStatisticDetail.OpenOMVInspectionCount.ToString();
                inspectionDetails.OmvInspectionAverageRisk = response.HeaderStatisticDetail.OMVInspectionAverageRisk;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionOMVType);
                inspectionDetails.InspectionOMVTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionOMVType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.OpenOMVFindingsType);
                inspectionDetails.InspectionTypeOmvURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.OpenOMVFindingsType);

                //inspection & audit
                inspectionDetails.InspectionDueCount = response.HeaderStatisticDetail.InspectionDueCount;
                //inspectionDetails.InspectionOverdueCount = response.HeaderStatisticDetail.InspectionOverdueCount;
                inspectionDetails.InspectionNeverDoneCount = response.HeaderStatisticDetail.InspectionNeverDoneCount;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionDueType);
                inspectionDetails.InspectionDueTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionDueType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionOverdueType);
                inspectionDetails.InspectionOverdueTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionOverdueType);

                //inspections
                inspectionDetails.InspectionFindingOutstandingCount = response.HeaderStatisticDetail.InspectionFindingOutstandingCount;
                inspectionDetails.InspectionFindingOverdueCount = response.HeaderStatisticDetail.InspectionFindingOverdueCount;
                inspectionDetails.PendingClosureCount = response.HeaderStatisticDetail.PendingClosureCount;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionFindingOutstandingType);
                inspectionDetails.InspectionFindingOutstandingTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionFindingOutstandingType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionFindingOverdueType);
                inspectionDetails.InspectionFindingOverdueTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionFindingOverdueType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.PendingClosureByOfficeType);
                inspectionDetails.PendingClosureByOfficeTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.PendingClosureByOfficeType);

                //audits
                inspectionDetails.InspectionAuditFindingOutstandingCount = response.HeaderStatisticDetail.InspectionAuditFindingOutstandingCount;
                inspectionDetails.InspectionAuditFindingOverdueCount = response.HeaderStatisticDetail.InspectionAuditFindingOverdueCount;
                inspectionDetails.AuditPendingClosureCount = response.HeaderStatisticDetail.AuditPendingClosureCount;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.AuditInspectionFindingOutstandingType);
                inspectionDetails.AuditInspectionFindingOutstandingTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.AuditInspectionFindingOutstandingType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.AuditInspectionFindingOverdueType);
                inspectionDetails.AuditInspectionFindingOverdueTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.AuditInspectionFindingOverdueType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.AuditPendingClosureByOfficeType);
                inspectionDetails.AuditPendingClosureByOfficeTypeURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.AuditPendingClosureByOfficeType);

                //findings
                inspectionDetails.TotalOutstandingFindingCount = response.HeaderStatisticDetail.TotalOutstandingFindingCount;
                //inspectionDetails.TotalOverdueFindingCount = response.HeaderStatisticDetail.TotalOverdueFindingCount;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.FindingsOutstandingType);
                inspectionDetails.FindingsOutstandingUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.FindingsOutstandingType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.FindingsOverdueType);
                inspectionDetails.FindingsOverdueUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.FindingsOverdueType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.AllInspection);
                inspectionDetails.AllInspectionURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.AllInspection);


                //Dashboard

                var inspecResult = response.HeaderStatisticDetail;

                inspectionDetails.VesselId = inputRequest.EncryptedVesselId;
                inspectionDetails.VesselName = decreptedString.Split(Constants.Separator)[1];

                //count
                inspectionDetails.OmvDefectRate = inspecResult.OMVDefectRate == null ? "0" : string.Format(Constants.TwoDecimal_NumberFormat, inspecResult.OMVDefectRate);
                inspectionDetails.PscDefectRate = string.Format(Constants.TwoDecimal_NumberFormat, inspecResult.PSCDefectRate);
                inspectionDetails.TotalOverdueFindingCount = inspecResult.TotalOverdueFindingCount;
                inspectionDetails.InspectionOverdueCount = inspecResult.InspectionOverdueCount;
                inspectionDetails.PscDetaintionCount = inspecResult.PSCDetaintionCount;
                inspectionDetails.TotalPSCFindingCount = inspecResult.TotalPSCFindingCount;
                inspectionDetails.OMVRejCount = inspecResult.OmvRejectionCount == null ? "0" : inspecResult.OmvRejectionCount.ToString();

                //Priority
                inspectionDetails.DeficienciesPerOMVPriority = inspecResult.DeficienciesPerOMVPriority;
                inspectionDetails.DeficienciesPerPSCPriority = inspecResult.DeficienciesPerPSCPriority;
                inspectionDetails.OverdueFindingsPriority = inspecResult.OverdueFindingsPriority;
                inspectionDetails.OverdueInspectionPriority = inspecResult.OverdueInspectionPriority;
                inspectionDetails.PSCDetentionPriority = inspecResult.PSCDetentionPriority;
                inspectionDetails.PSCDeficenPriority = inspecResult.PscDeficiencyPriority;
                inspectionDetails.OMVRejPriority = inspecResult.OmvRejectionPriority;

                //URL
                inspectionURLRequest = new InspectionRequestViewModel();
                inspectionURLRequest.FromDate = inputRequest.FromDate;
                inspectionURLRequest.ToDate = inputRequest.ToDate;
                inspectionURLRequest.VesselId = inputRequest.EncryptedVesselId;
                inspectionURLRequest.IsSummaryClicked = true;
                inspectionURLRequest.ActiveMobileTabClass = Constants.Tab2;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.FindingsOverdueType);
                inspectionDetails.OverdueFindingsUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.FindingsOverdueType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionOverdueType);
                inspectionDetails.OverdueInspectionUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionOverdueType);

                inspectionURLRequest.FromDate = inputRequest.PSCDetentionFromDate;
                inspectionURLRequest.ToDate = inputRequest.PSCDetentionToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionPSCType);
                inspectionDetails.PSCDetentionUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionPSCType);

                inspectionURLRequest.FromDate = inputRequest.PscDeficiencyFromDate;
                inspectionURLRequest.ToDate = inputRequest.PscDeficiencyToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.PSCDeficiencyType);
                inspectionDetails.PSCDeficiencyUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.PSCDeficiencyType);

                inspectionURLRequest.FromDate = inputRequest.FromDate;
                inspectionURLRequest.ToDate = inputRequest.ToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.AllInspection);
                inspectionURLRequest.ActiveMobileTabClass = Constants.Tab1;
                inspectionDetails.OverviewInspectionUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.AllInspection);

                inspectionURLRequest.FromDate = inputRequest.OmvRejectionFromDate;
                inspectionURLRequest.ToDate = inputRequest.OmvRejectionToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.OMVRejectionType);
                inspectionURLRequest.ActiveMobileTabClass = Constants.Tab2;
                inspectionDetails.OMVRejectionUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.OMVRejectionType);

                inspectionURLRequest.FromDate = inputRequest.FromDate;
                inspectionURLRequest.ToDate = inputRequest.ToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.OMVFindingsType);
                inspectionDetails.DeficienciesPerOmvURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.OMVFindingsType);

                inspectionURLRequest.FromDate = inputRequest.DeficienciesPerPSCFromDate;
                inspectionURLRequest.ToDate = inputRequest.DeficienciesPerPSCToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionPscDeficiencyType);
                inspectionDetails.DeficienciesPerPscURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionPscDeficiencyType);
            }

            return inspectionDetails;
        }

        /// <summary>
        /// Sets the inspection URL.
        /// </summary>
        /// <param name="inspection">The inspection.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private string SetInspectionURL(InspectionRequestViewModel inspection, InspectionDashboardType type)
        {
            inspection.InspectionType = type;
            string inspectionURL = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(inspection));
            return inspectionURL;
        }

        /// <summary>
        /// Gets the inspection and inspector details asynchronous.
        /// </summary>
        /// <param name="inspectionId">The inspection identifier.</param>
        /// <returns>
        /// InspectionAndInspectorDetailsViewModel
        /// </returns>
        public async Task<InspectionAndInspectorDetailsViewModel> GetInspectionAndInspectorDetailsAsync(string inspectionId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/InspectionDetail/" + inspectionId));
            InspectionDetail response = await GetAsync<InspectionDetail>(requestUrl);
            InspectionAndInspectorDetailsViewModel result = new InspectionAndInspectorDetailsViewModel();
            if (response != null)
            {
                //Details Binding
                result.StartDate = response.FromDate;
                result.EndDate = response.EndDate;
                result.Where = response.Where;
                result.NextDue = response.NextVisit;
                result.InspectionTypeId = response.InspectionTypeId;
                result.InspectionName = response.InspectionType;

                var IsPortChecked = string.IsNullOrWhiteSpace(response.ToPortId);
                if (IsPortChecked)
                {
                    result.Location = EnumsHelper.GetKeyValue(InspectionLocation.InPort);
                }
                else
                {
                    result.Location = EnumsHelper.GetKeyValue(InspectionLocation.Sailing);
                }

                //Inspector Binding
                if (response.IsInspectedByOffice)
                {
                    result.Entity = EnumsHelper.GetKeyValue(InspectorEntity.Office);
                }
                else if (response.IsInspectedByShipStaff)
                {
                    result.Entity = EnumsHelper.GetKeyValue(InspectorEntity.ShipStaff);
                }
                else if (response.IsInspectedByThirdParty)
                {
                    result.Entity = EnumsHelper.GetKeyValue(InspectorEntity.ThirdParty);
                }
                result.Inspector = response.InspectorName;
                result.Department = response.DepartmentName;
                result.Rank = response.InspectorTitle;
                result.Company = response.CompanyName;
                result.DetainedDays = response.DaysDetained;
            }
            return result;
        }

        /// <summary>
        /// Gets the inspection finding and causation details.
        /// </summary>
        /// <param name="inspectionFindingId">The inspection finding identifier.</param>
        /// <returns>
        /// InspectionFindingDetailsViewModel
        /// </returns>
        public async Task<InspectionFindingDetailsViewModel> GetInspectionFindingAndCausationDetails(string inspectionFindingId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/InspectionFindingDetail/" + inspectionFindingId));
            InspectionFindingDetailsResponse response = await GetAsync<InspectionFindingDetailsResponse>(requestUrl);
            InspectionFindingDetailsViewModel result = new InspectionFindingDetailsViewModel();

            if (response != null)
            {
                //FindingDetails Binding
                result.InspectionId = response.InspectionId;
                result.VesRef = response.VesselreferenceNumber;
                result.RefNo = response.ChapterInspectionReferenceNumber;
                result.Type = response.CategoryType;
                result.CorrectionActionAssignedTo = response.CorrectionActionsAssignedTo;
                result.SystemArea = response.SysteamArea;
                result.DueDate = response.DueDate;
                result.DateCleared = response.DateCleared;
                result.Description = response.Description;
                result.InspectionName = response.InspectionName;

                //Causation
                result.SubstandardActs = response.SubstandardActs;
                result.SubstandardConditions = response.SubstandardConditions;
                result.HumanFactors = response.HumanFactors;
                result.JobFactors = response.JobFactors;
                result.ControlManagementFailure = response.ControlManagementFailure;
            }
            return result;
        }

        /// <summary>
        /// Inspections the PSC detention details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<PscDetentionViewModel>> InspectionPscDetentionDetails(PscDetentionRequest request)
        {
            List<PscDetentionViewModel> result = new List<PscDetentionViewModel>();
            request.VesselId = GetVesselId(request.VesselId);
            request.EndDate = DateTime.Now.Date;
            request.StartDate = DateTime.Now.Date.AddMonths(Constants.PscDetentionFleetLevelNMonths);
            request.InspectionTypeId = Constants.PSCInspectionTypeId;
            request.IsDetention = true;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/PWAPscDetentions"));
            List<PscDetentionResponse> response = await PostAsync<List<PscDetentionResponse>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (PscDetentionResponse item in response)
                {
                    PscDetentionViewModel detention = new PscDetentionViewModel();
                    detention.VesselName = item.VesselName;
                    detention.VesselId = item.VesselId;
                    detention.DetentionDate = item.DetentionDate;
                    detention.Port = item.PortName;
                    detention.CompanyName = item.CompanyName ?? "";
                    detention.DaysDetained = item.DaysDetained;
                    detention.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);
                    InspectionFindingRequestViewModel findingRequest = new InspectionFindingRequestViewModel();
                    findingRequest.VesselId = item.VesselId;
                    findingRequest.InspectionId = item.InspectionId;
                    findingRequest.OccuredDate = item.DetentionDate.HasValue ? item.DetentionDate.Value.ToString(Constants.DateFormat) : String.Empty;
                    findingRequest.InspectionType = InspectionDashboardType.InspectionPSCType;
                    findingRequest.InspectionName = item.InspectionTypeDesc;
                    findingRequest.InspectionTypeId = item.InspectionTypeId;
                    findingRequest.TplId = item.TplId;
                    detention.EncryptedFindingURL = GetInspectionFindingURL(findingRequest);
                    result.Add(detention);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the inspection finding URL.
        /// </summary>
        /// <param name="requestVM">The request vm.</param>
        /// <returns></returns>
        private string GetInspectionFindingURL(InspectionFindingRequestViewModel requestVM)
        {
            string encryptedFindingUrl = _provider.CreateProtector("Inspection").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(requestVM));
            return encryptedFindingUrl;
        }

        /// <summary>
        /// Inspections the PSC deficiencies details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<PscDeficienciesResponseViewModel>> InspectionPscDeficienciesDetails(PscDeficienciesRequest request)
        {
            List<PscDeficienciesResponseViewModel> result = new List<PscDeficienciesResponseViewModel>();
            request.VesselIds = GetVesselId(request.VesselIds);
            request.EndDate = DateTime.Now.Date;
            request.StartDate = DateTime.Now.Date.AddMonths(Constants.PscDeficienciesFleetLevelNMonths);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/PWAPscDefDetails"));
            List<PscDeficienciesResponse> response = await PostAsync<List<PscDeficienciesResponse>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (PscDeficienciesResponse item in response)
                {
                    PscDeficienciesResponseViewModel pscDeficiencies = new PscDeficienciesResponseViewModel();
                    pscDeficiencies.VesselId = item.VesselId;
                    pscDeficiencies.VesselName = item.VesselName;
                    pscDeficiencies.InspectionId = item.InspectionId;
                    pscDeficiencies.InspectionTypeId = item.InspectionTypeId ?? "";
                    pscDeficiencies.InspectionTypeDesc = item.InspectionTypeDesc ?? "";
                    pscDeficiencies.DetentionDate = item.InspectionDate;
                    pscDeficiencies.CompanyName = item.CompanyName ?? "";
                    pscDeficiencies.WhereLocation = item.WhereLocation ?? "";
                    pscDeficiencies.FindingCount = item.FindingCount;
                    pscDeficiencies.InspectorName = item.InspectorName ?? "";
                    pscDeficiencies.IsDetained = item.IsDetained ? "Y" : "N";
                    pscDeficiencies.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);

                    InspectionRequestViewModel inspectionURLRequest = new InspectionRequestViewModel();
                    inspectionURLRequest.FromDate = request.StartDate;
                    inspectionURLRequest.ToDate = request.EndDate;
                    inspectionURLRequest.VesselId = item.VesselId;
                    inspectionURLRequest.IsSummaryClicked = true;
                    inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionPscDeficiencyType);
                    pscDeficiencies.EncryptedInspectionURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionPscDeficiencyType);

                    result.Add(pscDeficiencies);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the omv findings details.
        /// </summary>
        /// <param name="requestVM">The request vm.</param>
        /// <returns></returns>
        public async Task<List<OmvFindingsResponseViewModel>> GetOmvFindingsDetails(OmvFindingsRequestViewModel requestVM)
        {
            OmvFindingsRequest request = new OmvFindingsRequest();
            if (requestVM != null)
            {
                request.FleetId = requestVM.FleetId;
                request.MenuType = requestVM.MenuType;

                string requestVesselId = GetVesselId(requestVM.EncryptedVesselId);
                request.VesselIds = !string.IsNullOrWhiteSpace(requestVesselId) ? new List<string>() { requestVesselId } : null;

                request.FromDate = DateTime.Now.Date.AddMonths(Constants.OMVFindingsFleetLevelNMonths);
                request.ToDate = DateTime.Now.Date;
            }

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/PWAOmvFindings"));
            List<OmvFindingsResponse> response = await PostAsync<List<OmvFindingsResponse>>(requestUrl, CreateHttpContent(request));

            List<OmvFindingsResponseViewModel> result = new List<OmvFindingsResponseViewModel>();
            InspectionRequestViewModel inspectionURLRequest = new InspectionRequestViewModel();
            inspectionURLRequest.FromDate = request.FromDate.GetValueOrDefault();
            inspectionURLRequest.ToDate = request.ToDate.GetValueOrDefault();
            inspectionURLRequest.IsSummaryClicked = true;
            inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.OMVFindingsType);
            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    inspectionURLRequest.VesselId = x.VesselId;

                    result.Add(new OmvFindingsResponseViewModel()
                    {
                        EncryptedVesselId = GetEncryptedVessel(x.VesselId, x.VesselName, x.CoyId),
                        EncryptedInspectionURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.OMVFindingsType),
                        VesselName = x.VesselName,
                        InspectionDate = x.InspectionDate,
                        CompanyName = string.IsNullOrWhiteSpace(x.CompanyName) ? "" : x.CompanyName,
                        FindingCount = x.FindingCount,
                        InspectorName = string.IsNullOrWhiteSpace(x.InspectorName) ? "" : x.InspectorName,
                        NextDueDate = x.NextDueDate,
                        Where = string.IsNullOrWhiteSpace(x.Where) ? "" : x.Where
                    });
                });
            }

            return result;
        }

        /// <summary>
        /// Initiates the vessel inspection report call.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<string> InitiateVesselInspectionReportCall(InspectionVIRReportRequest request)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/GenerateVesselInspectionReport"));
            string response = await PostAsync<string>(requestUrl, CreateHttpContent(request));

            return response;
        }

        /// <summary>
        /// Initiates the vessel inspection summary report call.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<string> InitiateVesselInspectionSummaryReportCall(InspectionVIRReportRequest request)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/GenerateVesselInspectionSummaryReport"));
            string response = await PostAsync<string>(requestUrl, CreateHttpContent(request));

            return response;
        }

        #endregion

        /// <summary>
        /// Posts the get ves cert summary stats.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<VesselCertificateSummaryStatResponseViewModel> PostGetVesCertSummaryStats(CertificateRequestViewModel input)
        {
            VesselCertificateSummaryStatResponseViewModel certificateSummaryVM = new VesselCertificateSummaryStatResponseViewModel();
            UserMenuItem menuItem = new UserMenuItem();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input.VesselId);
            menuItem.Identifier = decreptedString.Split(Constants.Separator)[0];
            menuItem.DisplayText = input.VesselName;
            menuItem.UserMenuItemType = UserMenuItemType.Vessel;

            Dictionary<string, object> request = new Dictionary<string, object>();
            request.Add("menuItem", menuItem);

            //for user preference parameters in StopSailing/TradingAndExpiringIn30Days And ExpiringIn30DaysWindow
            //VesselCertificateSummaryStatRequest summaryRequest = new VesselCertificateSummaryStatRequest();
            //summaryRequest.CertificateDueNowRange = 30;
            //summaryRequest.ExpiringInPeriod = 29;
            //request.Add("request", summaryRequest);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Certificate/Summary"));
            List<VesselCertificateSummaryStatResponse> response = new List<VesselCertificateSummaryStatResponse>();
            if (menuItem != null)
            {
                response = await PostAsync<List<VesselCertificateSummaryStatResponse>>(requestUrl, CreateHttpContent(request));
            }

            CertificateRequestViewModel certificateRequest = new CertificateRequestViewModel
            {
                MenuType = UserMenuItemType.Vessel
            };

            if (response != null)
            {
                var AllActiveCertificateCount = response.Where(x => x.Statistic == VesselCertificateStatistic.TotalActive).FirstOrDefault();
                certificateRequest.ActiveMobileTabClass = Constants.Tab1;
                certificateSummaryVM.AllActiveCertificateCount = AllActiveCertificateCount != null ? AllActiveCertificateCount.CertificateCount : 0;
                certificateSummaryVM.AllActiveCertificateCountURL = SetCertificateURL(certificateRequest, VesselCertificates.TotalActive);

                var OverDueCertificateCount = response.Where(x => x.Statistic == VesselCertificateStatistic.Overdue).FirstOrDefault();
                certificateRequest.ActiveMobileTabClass = Constants.Tab2;
                certificateSummaryVM.OverDueCertificateCount = OverDueCertificateCount != null ? OverDueCertificateCount.CertificateCount : 0;
                certificateSummaryVM.OverDueCertificateCountURL = SetCertificateURL(certificateRequest, VesselCertificates.Overdue);
                certificateSummaryVM.OverDueCertificatePriority = OverDueCertificateCount.KPIPriority;

                var Expires30DaysCertificateCount = response.Where(x => x.Statistic == VesselCertificateStatistic.ExpiringIn30Days).FirstOrDefault();
                certificateSummaryVM.Expires30DaysCertificateCount = Expires30DaysCertificateCount != null ? Expires30DaysCertificateCount.CertificateCount : 0;
                certificateSummaryVM.Expires30DaysCertificateCountURL = SetCertificateURL(certificateRequest, VesselCertificates.ExpiringIn30Days);
                certificateSummaryVM.ExpiringXDaysCertificatePriority = Expires30DaysCertificateCount.KPIPriority;

                var SurveyRangeCertificateCount = response.Where(x => x.Statistic == VesselCertificateStatistic.WithinSurveyRange).FirstOrDefault();
                certificateSummaryVM.SurveyRangeCertificateCount = SurveyRangeCertificateCount != null ? SurveyRangeCertificateCount.CertificateCount : 0;
                certificateSummaryVM.SurveyRangeCertificateCountURL = SetCertificateURL(certificateRequest, VesselCertificates.WithinSurveyRange);
                certificateSummaryVM.SurveyRangeCertificatePriority = SurveyRangeCertificateCount.KPIPriority;

                var StopSailingAndTrading = response.Where(x => x.Statistic == VesselCertificateStatistic.StopSailingAndTradingExpiring30Days).FirstOrDefault();
                certificateSummaryVM.StopSailingTradingExpiringIn30DaysCount = StopSailingAndTrading != null ? StopSailingAndTrading.CertificateCount : 0;
                certificateSummaryVM.StopSailingTradingExpiringIn30DaysKPI = StopSailingAndTrading != null ? StopSailingAndTrading.KPIPriority : 1;
                certificateSummaryVM.StopSailingTradingExpiringIn30DaysUrl = SetCertificateURL(certificateRequest, VesselCertificates.StopSailingAndTradingExpiring30Days);
            }

            return certificateSummaryVM;
        }

        /// <summary>
        /// Sets the certificate URL.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="stageName">Name of the stage.</param>
        /// <returns></returns>
        private string SetCertificateURL(CertificateRequestViewModel input, VesselCertificates stageName)
        {
            input.StageName = EnumsHelper.GetKeyValue(stageName);
            string certificateURL = _provider.CreateProtector("CertificateURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(input));
            return certificateURL;
        }

        /// <summary>
        /// Posts the get vessel certificates paged.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<CertificatePreviewViewModel>> PostGetVesselCertificatesPaged(CertificateRequestViewModel input)
        {
            string GridTitle = string.Empty;
            List<CertificatePreviewViewModel> CertificateList = new List<CertificatePreviewViewModel>();
            CertificatePreviewFilter filter = new CertificatePreviewFilter();
            filter.MenuItem = new UserMenuItem();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input.VesselId);
            filter.MenuItem.Identifier = decreptedString.Split(Constants.Separator)[0];
            filter.MenuItem.DisplayText = decreptedString.Split(Constants.Separator)[1];
            filter.MenuItem.UserMenuItemType = UserMenuItemType.Vessel;
            if (string.IsNullOrWhiteSpace(input.StageName))
            {
                if (input.CertificateImpact == EnumsHelper.GetKeyValue(CertificateImpact.All))
                {
                    filter.CertificateImpactIds = EnumsHelper.GetKeyValue(CertificateImpact.All);

                }
                else if (input.CertificateImpact == EnumsHelper.GetKeyValue(CertificateImpact.NoImpact))
                {
                    filter.CertificateImpactIds = null;
                }
                else
                {
                    filter.CertificateImpactIds = input.CertificateImpact;
                }

                filter.CertificateType = (CertificateType)Enum.Parse(typeof(CertificateType), EnumsHelper.GetEnumItemFromKeyValue(typeof(CertificateType), input.CertificateType));
                VesselCertificateStatus SelectedStatus = (VesselCertificateStatus)Enum.Parse(typeof(VesselCertificateStatus), EnumsHelper.GetEnumItemFromKeyValue(typeof(VesselCertificateStatus), input.CertificateStatus));
                if (SelectedStatus == VesselCertificateStatus.All)
                {
                    filter.IsCertificateActive = null;
                    filter.IsDeleted = null;
                }
                else if (SelectedStatus == VesselCertificateStatus.Active)
                {
                    filter.IsCertificateActive = true;
                    filter.IsDeleted = false;
                }
                else if (SelectedStatus == VesselCertificateStatus.Inactive)
                {
                    filter.IsCertificateActive = false;
                    filter.IsDeleted = false;
                }
                else if (SelectedStatus == VesselCertificateStatus.Deleted)
                {
                    filter.IsCertificateActive = null;
                    filter.IsDeleted = true;
                }
                filter.FromDate = input.FromDate;
                filter.ToDate = input.ToDate;
                filter.IncludeWindow = input.IncludeWindow;
                filter.SearchKeyword = input.SearchKeyword;
                GridTitle = string.Empty;
            }
            else if (input.StageName == EnumsHelper.GetKeyValue(VesselCertificates.TotalActive))
            {
                filter.RangeType = null;
                filter.CertificateId = null;
                filter.CertificateImpactIds = EnumsHelper.GetKeyValue(CertificateImpact.All);
                filter.FromDate = null;
                filter.ToDate = null;
                filter.IncludeWindow = true;
                filter.CertificateType = null;
                filter.IsCertificateActive = true;
                filter.IsCertificatesChanged = false;
                filter.IsDeleted = false;
                GridTitle = EnumsHelper.GetDescription(VesselCertificates.TotalActive);
            }
            else if (input.StageName == EnumsHelper.GetKeyValue(VesselCertificates.Overdue))
            {
                filter.RangeType = CertificateRangeType.OverDue;
                filter.CertificateId = null;
                filter.CertificateImpactIds = EnumsHelper.GetKeyValue(CertificateImpact.All);
                filter.FromDate = null;
                filter.ToDate = null;
                filter.IncludeWindow = false;
                filter.CertificateType = null;
                filter.IsCertificateActive = true;
                filter.IsCertificatesChanged = false;
                filter.IsDeleted = false;
                GridTitle = EnumsHelper.GetDescription(VesselCertificates.Overdue);
            }
            else if (input.StageName == EnumsHelper.GetKeyValue(VesselCertificates.ExpiringIn30Days))
            {
                filter.RangeType = null;
                filter.CertificateId = null;
                filter.CertificateImpactIds = EnumsHelper.GetKeyValue(CertificateImpact.All);
                filter.FromDate = DateTime.Now.Date;
                filter.ToDate = DateTime.Now.AddDays(Constants.CertificateDueNowRange).Date;
                filter.IncludeWindow = false;
                filter.CertificateType = null;
                filter.IsCertificateActive = true;
                filter.IsCertificatesChanged = false;
                filter.IsDeleted = false;
                GridTitle = EnumsHelper.GetDescription(VesselCertificates.ExpiringIn30Days);
            }
            else if (input.StageName == EnumsHelper.GetKeyValue(VesselCertificates.WithinSurveyRange))
            {
                filter.RangeType = CertificateRangeType.WithinSurveyRange;
                filter.CertificateId = null;
                filter.CertificateImpactIds = EnumsHelper.GetKeyValue(CertificateImpact.All);
                filter.FromDate = null;
                filter.ToDate = DateTime.Now.Date;
                filter.IncludeWindow = false;
                filter.CertificateType = null;
                filter.IsCertificateActive = true;
                filter.IsCertificatesChanged = false;
                filter.IsDeleted = false;
                GridTitle = EnumsHelper.GetDescription(VesselCertificates.WithinSurveyRange);
            }
            else if (input.StageName == EnumsHelper.GetKeyValue(VesselCertificates.StopSailingAndTradingExpiring30Days))
            {
                filter.RangeType = null;
                filter.FromDate = DateTime.Now.Date;
                filter.ToDate = DateTime.Now.AddDays(Constants.CertificateDueNowRange).Date;
                filter.CertificateImpactIds = String.Join(",", new List<string> { EnumsHelper.GetKeyValue(CertificateImpact.StopSailing), EnumsHelper.GetKeyValue(CertificateImpact.StopTrading) });
                filter.IncludeWindow = true;
                filter.CertificateType = null;
                filter.IsCertificatesChanged = false;
                filter.IsCertificateActive = null;
                filter.IsDeleted = false;
                GridTitle = EnumsHelper.GetDescription(VesselCertificates.StopSailingAndTradingExpiring30Days);
            }

            var value = new Dictionary<string, object>()
                {
                    { "filter", filter }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Certificate/PWACertificates"));
            List<CertificatePreview> response = await PostAsyncAutoPaged<CertificatePreview>(requestUrl, value, 80);

            if (response != null)
            {
                response.ForEach(x =>
                {
                    CertificateList.Add(new CertificatePreviewViewModel()
                    {
                        CertificateNumber = x.CertificateNumber,
                        CertificateFullName = x.Name + (!string.IsNullOrWhiteSpace(x.Annotation) ? (" - " + x.Annotation) : ""),
                        Validity = x.Validity == null ? "" : x.Validity.ToString(),
                        DateFrom = x.DateFrom,
                        DateTo = x.DateTo,
                        EndOfWindowDate = x.EndOfWindowDate,
                        WindowFormattedString = (x.WindowBeforeExpiry > 0 || x.WindowAfterExpiry > 0) ? ((x.WindowBeforeExpiry > 0 ? ("-" + x.WindowBeforeExpiry) : " -") + (x.WindowAfterExpiry > 0 ? (" / +" + x.WindowAfterExpiry) : " / -")) : "",
                        IsMandatoryCertificate = x.IsMandatoryCertificate == true ? "Yes" : "No",
                        CertificateType = string.IsNullOrWhiteSpace(x.CertificateType) ? "" : x.CertificateType,
                        CertificateImpact = string.IsNullOrWhiteSpace(x.CertificateImpact) ? "" : x.CertificateImpact,
                        IssuedBy = string.IsNullOrWhiteSpace(x.IssuedBy) ? "" : x.IssuedBy,
                        IsActive = x.IsActive.GetValueOrDefault(),
                        IsActiveText = x.IsActive == true ? "" : "Yes",
                        RangeType = x.RangeType != null && x.RangeType.HasValue ? EnumsHelper.GetKeyValue(x.RangeType.Value) : "",
                        Notes = !string.IsNullOrWhiteSpace(x.Notes) ? x.Notes : "",
                        DocumentCount = x.DocumentCount.GetValueOrDefault(),
                        VesselCertificateLogId = x.VesselCertificateLogId,
                        VesselCertificateId = x.VesselCertificateId,
                        MappedOrderCount = x.MappedOrderCount,
                        IsCertificateInActive = x.IsDeleted == false && x.IsActive == false,
                        IsCertificateDeleted = x.IsDeleted.GetValueOrDefault(),
                        IsCertificateIncomplete = x.IsActive == true && x.IsDeleted == false && x.DateFrom == null,
                        GridTitle = GridTitle,
                        CertificateDetailsUrl = CommonUtil.GetEncryptedURL(_provider, Constants.CertificatesDetails, new CertificateDetailViewModel() { VesselCertificateId = x.VesselCertificateId })
                    });
                });
            }
            return CertificateList;
        }

        /// <summary>
        /// Gets the impact values.
        /// </summary>
        /// <returns></returns>
        public List<CertificateImpact> GetImpactValues()
        {
            return Enum.GetValues(typeof(CertificateImpact)).Cast<CertificateImpact>().ToList();
        }

        /// <summary>
        /// Gets the certificate types.
        /// </summary>
        /// <returns></returns>
        public List<CertificateType> GetCertificateTypes()
        {
            return Enum.GetValues(typeof(CertificateType)).Cast<CertificateType>().ToList();
        }

        /// <summary>
        /// Gets the vessel certificate statuses.
        /// </summary>
        /// <returns></returns>
        public List<VesselCertificateStatus> GetVesselCertificateStatuses()
        {
            return Enum.GetValues(typeof(VesselCertificateStatus)).Cast<VesselCertificateStatus>().ToList();
        }

        /// <summary>
        /// Posts the get vessel management summary.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<VesselManagementTypeDetail>> PostGetVesselManagementSummary(string vesselId)
        {
            string queryString = "vesselId=" + vesselId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VesselManagement/VesselManagementSummary"), queryString);
            List<VesselManagementTypeDetail> response = await PostAsync<List<VesselManagementTypeDetail>>(requestUrl, CreateHttpContent(vesselId));

            return response;
        }

        /// <summary>
        /// Posts the get vessel preview.
        /// </summary>
        /// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
        /// <returns></returns>
        public async Task<VesselPreviewViewModel> PostGetVesselPreview(string encryptedVesselDetail)
        {
            VesselPreview response = new VesselPreview();
            VesselPreviewViewModel vesselPreview = new VesselPreviewViewModel();

            string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
            string vesselId = decryptedString.Split(Constants.Separator)[0];

            string urlvessel = "vesselId=" + vesselId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vessel/VesselPreview/"), urlvessel);

            if (!string.IsNullOrWhiteSpace(vesselId))
            {
                response = await PostAsync<VesselPreview>(requestUrl, CreateHttpContent(vesselId));
            }

            if (response != null)
            {
                vesselPreview.Name = response.Name;
                vesselPreview.Imo = response.Imo;
                vesselPreview.Type = response.Type;
                vesselPreview.VesselBuiltDate = response.VesselBuiltDate.HasValue ? response.VesselBuiltDate.Value.ToString("dd MMM yyyy") : "";
                vesselPreview.VesselAge = CommonUtil.CalculateVesselAge(response.VesselBuiltDate) + " years";
                vesselPreview.Flag = response.Flag;
            }
            return vesselPreview;
        }

        #region PMS Details

        /// <summary>
        /// Gets the work order generic header details.
        /// </summary>
        /// <param name="workOrderId">The work order identifier.</param>
        /// <returns></returns>
        public async Task<WorkOrderGenericHeaderDetailResponseViewModel> GetWorkOrderGenericHeaderDetails(string workOrderId)
        {
            WorkOrderGenericHeaderDetailResponse response = null;
            WorkOrderGenericHeaderDetailResponseViewModel result = null;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WOGenericHeaderDetail/" + workOrderId));

            if (!string.IsNullOrWhiteSpace(workOrderId))
            {
                response = await GetAsync<WorkOrderGenericHeaderDetailResponse>(requestUrl);
            }

            if (response != null)
            {
                result = new WorkOrderGenericHeaderDetailResponseViewModel
                {
                    ComponentId = response.ComponentId,
                    JobName = response.JobName,
                    ScheduleTaskId = response.ScheduleTaskId,
                    WorkOrderId = response.WorkOrderId,
                    WorkOrderIndicationTypeId = response.WorkOrderIndicationTypeId,
                };
            }

            return result;
        }

        /// <summary>
        /// Posts the get maintenance dashboard detail.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<MaintenanceDashboardResponseViewModel> PostGetMaintenanceDashboardDetail(PlannedMaintenanceListViewModel request)
        {
            MaintenanceDashboardResponseViewModel summaryDetails = new MaintenanceDashboardResponseViewModel();
            List<MaintenanceDashboardResponse> response = null;
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(request.EncryptedVesselId);

            MaintenanceDashboardRequest inputRequest = new MaintenanceDashboardRequest();

            UserMenuItem menuItem = new UserMenuItem();
            menuItem.DisplayText = decreptedString.Split(Constants.Separator)[1];
            menuItem.Identifier = decreptedString.Split(Constants.Separator)[0];
            menuItem.UserMenuItemType = UserMenuItemType.Vessel;
            inputRequest.Item = menuItem;

            inputRequest.StartDate = request.FromDate;
            inputRequest.EndDate = request.ToDate;
            inputRequest.VesselId = decreptedString.Split(Constants.Separator)[0];
            inputRequest.ReportedInLastNDays = Constants.ReportedInLastNDays;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/MaintenanceDashboardSummary"));
            response = await PostAsync<List<MaintenanceDashboardResponse>>(requestUrl, CreateHttpContent(inputRequest));
            if (response != null && response.Any())
            {
                var dueCount = response[0];
                var doneCount = response[1];

                if (dueCount != null)
                {
                    summaryDetails.DueCount = dueCount.DueCount.GetValueOrDefault();
                    summaryDetails.OverDueCurrentMonthCount = dueCount.PeriodOverdueCount.GetValueOrDefault();
                    summaryDetails.OverDuePreviousMonthCount = dueCount.OverdueCount.GetValueOrDefault();
                }
                else
                {
                    summaryDetails.DueCount = 0;
                    summaryDetails.OverDueCurrentMonthCount = 0;
                    summaryDetails.OverDuePreviousMonthCount = 0;
                }

                summaryDetails.DoneCount = doneCount != null && doneCount.PlannedMaintenanceHeaderDetail != null ? doneCount.PlannedMaintenanceHeaderDetail.WorkOrderDoneCount : 0;

                summaryDetails.AllCount = summaryDetails.DueCount + summaryDetails.OverDueCurrentMonthCount + summaryDetails.OverDuePreviousMonthCount + summaryDetails.DoneCount;
            }

            summaryDetails.Month = DateTime.Now.Date.ToString("MMM");
            return summaryDetails;
        }

        /// <summary>
        /// Posts the get vessel work basket detail.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<WorkBasketDetailResponseViewModel>> PostGetVesselWorkBasketDetail(PlannedMaintenanceListViewModel request)
        {
            List<WorkBasketDetailResponseViewModel> workBasketList = new List<WorkBasketDetailResponseViewModel>();
            List<WorkBasketDetailResponse> response = new List<WorkBasketDetailResponse>();

            WorkBasketDetailRequest filter = SetPMSListRequestObject(request);

            var value = new Dictionary<string, object>()
                {
                    { "request", filter }
                };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/PWAWorkBasketDetail"));
            response = await PostAsyncAutoPaged<WorkBasketDetailResponse>(requestUrl, value, 500);
            if (response != null && response.Any())
            {
                foreach (WorkBasketDetailResponse item in response)
                {
                    PlannedMaintenanceRequestViewModel plannedMaintenanceRequestUrl = new PlannedMaintenanceRequestViewModel();
                    plannedMaintenanceRequestUrl.FromDate = request.FromDate;
                    plannedMaintenanceRequestUrl.ToDate = request.ToDate;
                    plannedMaintenanceRequestUrl.EncryptedVesselId = request.EncryptedVesselId;
                    plannedMaintenanceRequestUrl.StageName = request.StageName;
                    plannedMaintenanceRequestUrl.ComponentId = item.ComponentId;
                    plannedMaintenanceRequestUrl.WorkOrderId = item.WorkOrderId;
                    plannedMaintenanceRequestUrl.ScheduleTaskId = item.ScheduleTaskId;
                    plannedMaintenanceRequestUrl.IsNavigatedFromDone = false;



                    WorkBasketDetailResponseViewModel workBasketResponse = new WorkBasketDetailResponseViewModel();
                    workBasketResponse.WorkOrderId = item.WorkOrderId;
                    workBasketResponse.Type = item.Type;
                    workBasketResponse.DueDate = item.DueDate;
                    workBasketResponse.IsCritical = item.IsCritical ?? false;
                    workBasketResponse.ComponentName = item.ComponentName;
                    workBasketResponse.Job = item.JobName;
                    workBasketResponse.Status = item.Status;
                    workBasketResponse.Interval = (item.Frequency ?? 0) + " " + item.FrequencyTypeShortCode;
                    workBasketResponse.Resp = item.ResponsibleRankShortCode ?? string.Empty;
                    workBasketResponse.RespDescription = item.ResponsibleRankDescription ?? string.Empty;
                    workBasketResponse.LeftHours = item.LeftHours;

                    workBasketResponse.EncryptedVesselId = request.EncryptedVesselId;

                    //Checking current month Overdue.
                    if (item.DueDate != null && item.DueDate.Value.Date >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) && item.DueDate.Value.Date < DateTime.Now.Date)
                    {
                        workBasketResponse.IsOverDueVisible = true;
                    }
                    else if (item.DueDate != null && item.DueDate.Value.Date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)) //Checking Overdue prior from current month  
                    {
                        workBasketResponse.IsOverduePeriodVisible = true;
                    }
                    else
                    {
                        workBasketResponse.IsDue = true;
                    }

                    //Specifics column change
                    if (item.JsaRequired)
                    {
                        workBasketResponse.HasMappedJSA = !string.IsNullOrWhiteSpace(item.MappedJsaId); //Green JobSafetyAnalysisGeometry Geometry
                        workBasketResponse.HasPermitJSA = !workBasketResponse.HasMappedJSA; //orange JobSafetyAnalysisGeometry Geometry   
                        workBasketResponse.JsaTooltip = Constants.JsaRequiredTooltip;
                    }
                    workBasketResponse.IsJSAPermitRequired = item.JsaPermitRequired;
                    workBasketResponse.IsJSAPermitRequiredTooltip = Constants.JsaPermitRequiredTooltip;


                    workBasketResponse.HasRoundsJobIcon = !string.IsNullOrWhiteSpace(item.WorkOrderIndicationTypeId) && item.WorkOrderIndicationTypeId == EnumsHelper.GetKeyValue(WorkOrderIndicationType.Round); //Gray RoundsJobGeometry Geometry

                    if (item.HasAllocatedSpares.GetValueOrDefault() == true && item.IsRoblessThanAllocatedQty.GetValueOrDefault() == true)
                    {
                        workBasketResponse.AllocatedSpareRedGeometry = true; //Red AllocateSpareGeometry 
                    }
                    if (item.HasAllocatedSpares.GetValueOrDefault() == true && item.IsRoblessThanAllocatedQty.GetValueOrDefault() == false)
                    {
                        workBasketResponse.AllocatedSparePurpleGeometry = true; //Purple AllocateSpareGeometry
                    }
                    if (item.IsRobLessThanReq.GetValueOrDefault() == true)
                    {
                        workBasketResponse.IsRobLessThanReq = true; //Red PartsGeometry
                    }

                    //required spare
                    if (!IsReportedWorkOrder(item.WorkOrderStatusId))
                    {
                        workBasketResponse.RequiredSpareCount = item.RequiredSpareCount.GetValueOrDefault();
                    }
                    else
                    {
                        workBasketResponse.RequiredSpareCount = 0;
                    }

                    //navigation
                    if (!string.IsNullOrWhiteSpace(item.ScheduleTaskId))
                    {
                        //work order WO

                    }
                    else if (!string.IsNullOrWhiteSpace(item.DwoId) && !string.IsNullOrWhiteSpace(item.WorkOrderIndicationTypeId) && item.WorkOrderIndicationTypeId == EnumsHelper.GetKeyValue(WorkOrderIndicationType.Defect))
                    {
                        //defect DWO
                        workBasketResponse.IsDefectWorkOrder = true;
                        workBasketResponse.DwoId = item.DwoId;

                        DefectDetailsViewModel defectDetails = new DefectDetailsViewModel();
                        defectDetails.DefectWorkOrderId = item.DwoId;
                        //defectDetails.DueDate = item.EstimatedCompleteDate.HasValue ? item.EstimatedCompleteDate.Value.ToString(Constants.DateFormat) : "";

                        defectDetails.IsGuaranteeClaimCode = false;

                        workBasketResponse.DefectDetailsUrl = _provider.CreateProtector("DefectDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(defectDetails));

                    }
                    else
                    {
                        //unplanned work order SWO
                        plannedMaintenanceRequestUrl.IsSWO = true;
                    }

                    string plannedMaintenanceDetailsRequest = _provider.CreateProtector("PMSDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(plannedMaintenanceRequestUrl));
                    workBasketResponse.PlannedMaintenanceDetailsRequestURL = plannedMaintenanceDetailsRequest;

                    workBasketList.Add(workBasketResponse);
                }
            }

            return workBasketList;
        }

        /// <summary>
        /// Sets the PMS list request object.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private WorkBasketDetailRequest SetPMSListRequestObject(PlannedMaintenanceListViewModel request)
        {
            WorkBasketDetailRequest filter = new WorkBasketDetailRequest();
            bool _isCritical = false, _isNonCritical = false;

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(request.EncryptedVesselId);
            filter.VesselId = decreptedString.Split(Constants.Separator)[0];

            filter.OfficeApproval = -1;
            filter.CalculateRecurrence = false;
            filter.DepartmentIds = null;
            filter.JobTypeIds = null;

            if (request.isSearchedClick)
            {
                filter.CategoryId = request.CategoryId;
                filter.ComponentId = request.ComponentId;
                filter.ParentComponentId = request.ParentComponentId;
                filter.TopSystemAreaId = request.TopSystemAreaId;

                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                if (request.OtherFilters != null && request.OtherFilters.Any())
                {
                    filter.ShowDue = request.OtherFilters.Where(x => EnumsHelper.GetKeyValue(PMSOtherFilter.Due) == x).ToList().Any();
                    filter.ShowCurrentMonthOverdue = request.OtherFilters.Where(x => EnumsHelper.GetKeyValue(PMSOtherFilter.OverdueCurrentMonth) == x).ToList().Any();
                    filter.ShowPreviousMonthsOverdue = request.OtherFilters.Where(x => EnumsHelper.GetKeyValue(PMSOtherFilter.OverduePriorMonth) == x).ToList().Any();
                    filter.ShowAllScheduleTask = false;
                }
                else
                {
                    filter.ShowDue = true;
                    filter.ShowCurrentMonthOverdue = true;
                    filter.ShowPreviousMonthsOverdue = true;
                }

                filter.WorkOrderStatusIds = request.StatusIds;
                filter.ResponsibilityIds = request.ResponsibilityIds;
                filter.RescheduleTypeIds = request.RescheduledIds;
                filter.JobTypeIds = request.JobTypeIds;
                filter.Criticality = -1;

                if (request.PriorityIds != null && request.PriorityIds.Any())
                {
                    foreach (var priority in request.PriorityIds)
                    {
                        if (priority != null)
                        {
                            if (priority == EnumsHelper.GetKeyValue(WorkBasketPriority.Critical))
                            {
                                _isCritical = true;
                            }
                            else if (priority == EnumsHelper.GetKeyValue(WorkBasketPriority.NonCritical))
                            {
                                _isNonCritical = true;
                            }
                        }
                    }
                }

                if (_isCritical && _isNonCritical)
                {
                    filter.Criticality = -1;
                }
                else if (_isCritical)
                {
                    filter.Criticality = 1;
                }
                else if (_isNonCritical)
                {
                    filter.Criticality = 0;
                }
            }
            else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.Due))
            {
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.ShowDue = true;
                filter.Criticality = -1;
            }
            else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.CriticalDue))
            {
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.ShowDue = true;
                filter.Criticality = 1;
            }
            else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.Overdue))
            {
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.ShowPreviousMonthsOverdue = true;
                filter.Criticality = -1;
                filter.ExcludeWorkOrderStatusIds = new List<string> { EnumsHelper.GetKeyValue(JobStatus.ReportedWorkOrder) };
            }
            else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.CriticalOverdue))
            {
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.ShowPreviousMonthsOverdue = true;
                filter.Criticality = 1;
            }
            else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.PlannedFor))
            {
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.ShowPreviousMonthsOverdue = true;
                filter.ShowDue = true;
                filter.ShowCurrentMonthOverdue = true;
                filter.Criticality = -1;
                filter.RescheduleTypeIds = new List<string>() { EnumsHelper.GetKeyValue(PPMAttributeLookup.RescheduleTypePlannedFor) };
            }
            else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.ReqReschedule))
            {
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.ShowPreviousMonthsOverdue = true;
                filter.ShowDue = true;
                filter.ShowCurrentMonthOverdue = true;
                filter.Criticality = -1;
                filter.WorkOrderStatusIds = new List<string>() { EnumsHelper.GetKeyValue(JobStatus.RescheduleRequested) };
            }
            else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.Critical))
            {
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.ShowPreviousMonthsOverdue = true;
                filter.ShowDue = true;
                filter.ShowCurrentMonthOverdue = true;
                filter.Criticality = 1;
            }
            else if (request.StageName == EnumsHelper.GetDescription(PMSDashboardStage.Completed))
            {
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.Criticality = -1;
                filter.ShowDue = true;
                filter.ShowCurrentMonthOverdue = true;
                filter.ShowPreviousMonthsOverdue = true;
                filter.WorkOrderStatusIds = new List<string> { EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder) };
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(PMSDashboardStage.All))
            {
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.Criticality = -1;
                filter.ShowPreviousMonthsOverdue = true;
                //filter.ShowOverDue = true; //need to check in SS
                filter.ShowCurrentMonthOverdue = true;
                filter.ShowDue = true;

            }
            else if (request.StageName == EnumsHelper.GetKeyValue(PMSDashboardStage.PMSManagedCertificates))
            {
                //referred from shipsure Certificate/StartViewModel/NavigateWorkBasket
                filter.ShowAllScheduleTask = true;
                filter.FromDate = request.FromDate;
                filter.ToDate = request.ToDate;
                filter.Criticality = -1;
                filter.OfficeApproval = -1;
                filter.JobTypeIds = new List<string>() { EnumsHelper.GetKeyValue(JobType.Certificate) };
            }
            return filter;
        }

        /// <summary>
        /// Determines whether [is reported work order] [the specified status identifier].
        /// </summary>
        /// <param name="statusId">The status identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is reported work order] [the specified status identifier]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsReportedWorkOrder(string statusId)
        {
            return statusId == EnumsHelper.GetKeyValue(JobStatus.ReportedWorkOrder) || statusId == EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder) || statusId == EnumsHelper.GetKeyValue(JobStatus.ReOpenedWorkOrder);
        }

        /// <summary>
        /// Posts the get closed work order history.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<WorkHistoryResponseViewModel>> PostGetClosedWorkOrderHistory(PlannedMaintenanceListViewModel request)
        {
            List<WorkHistoryResponseViewModel> workBaseketHistoryList = new List<WorkHistoryResponseViewModel>();
            List<WorkHistoryResponse> response = null;
            WorkHistoryRequest filter = SetMaintenanceHistoryListRequestObject(request);

            var value = new Dictionary<string, object>()
                {
                    { "request", filter }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/ClosedWorkOrderHistory"));

            if (filter != null && !string.IsNullOrWhiteSpace(filter.VesselId))
            {
                response = await PostAsyncAutoPaged<WorkHistoryResponse>(requestUrl, value, 500);
            }

            if (response != null && response.Any())
            {
                foreach (WorkHistoryResponse item in response)
                {
                    PlannedMaintenanceRequestViewModel plannedMaintenanceRequestUrl = new PlannedMaintenanceRequestViewModel();
                    plannedMaintenanceRequestUrl.FromDate = request.FromDate;
                    plannedMaintenanceRequestUrl.ToDate = request.ToDate;
                    plannedMaintenanceRequestUrl.EncryptedVesselId = request.EncryptedVesselId;
                    plannedMaintenanceRequestUrl.StageName = request.StageName;
                    plannedMaintenanceRequestUrl.ComponentId = item.ComponentId;
                    plannedMaintenanceRequestUrl.WorkOrderId = item.WorkOrderId;
                    plannedMaintenanceRequestUrl.ScheduleTaskId = item.ScheduleTaskId;
                    plannedMaintenanceRequestUrl.IsNavigatedFromDone = true;
                    plannedMaintenanceRequestUrl.JobId = item.JobId;
                    plannedMaintenanceRequestUrl.WorkOrderHistoryId = item.WorkOrderHistoryId;
                    plannedMaintenanceRequestUrl.DefectWorkOrderId = item.DefectWorkOrderId;
                    plannedMaintenanceRequestUrl.WorkOrderIndicationId = item.WorkOrderIndicationId;

                    string plannedMaintenanceDetailsRequest = _provider.CreateProtector("PMSDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(plannedMaintenanceRequestUrl));

                    WorkHistoryResponseViewModel workBasketHistoryItem = new WorkHistoryResponseViewModel();
                    workBasketHistoryItem.IsCritical = item.IsCritical;
                    workBasketHistoryItem.DoneDate = item.WOCompletedDate == null ? string.Empty : item.WOCompletedDate.GetValueOrDefault().ToString(Constants.DateFormat);
                    workBasketHistoryItem.ClosedDate = item.WOClosedDate == null ? string.Empty : item.WOClosedDate.GetValueOrDefault().ToString(Constants.DateFormat);
                    workBasketHistoryItem.ComponentName = item.ComponentName;
                    workBasketHistoryItem.ClassCode = item.ClassCode ?? string.Empty;
                    workBasketHistoryItem.JobName = item.JobName;
                    workBasketHistoryItem.Dept = item.DepartmentShortCode ?? string.Empty;
                    workBasketHistoryItem.Resp = item.ResponsibleRankShortCode ?? string.Empty;
                    workBasketHistoryItem.OrderType = item.WOClass;
                    workBasketHistoryItem.Type = item.JobTypeShortCode;
                    workBasketHistoryItem.Interval = item.Interval ?? string.Empty;
                    workBasketHistoryItem.ReportForm = item.HasReportForm ? "Yes" : string.Empty;
                    workBasketHistoryItem.Days = item.NoOfDays.GetValueOrDefault();
                    workBasketHistoryItem.StDeleted = item.ScheduleTaskExists ? "No" : "Yes";
                    workBasketHistoryItem.RunningHours = item.ReportedRunningHours.GetValueOrDefault();
                    workBasketHistoryItem.Attachments = item.HasAttachments ? "Yes" : "";
                    workBasketHistoryItem.PlannedMaintenanceDetailsRequestURL = plannedMaintenanceDetailsRequest;
                    workBasketHistoryItem.EncryptedVesselId = request.EncryptedVesselId;

                    workBaseketHistoryList.Add(workBasketHistoryItem);
                }
            }

            return workBaseketHistoryList;
        }

        /// <summary>
        /// Sets the maintenance history list request object.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private WorkHistoryRequest SetMaintenanceHistoryListRequestObject(PlannedMaintenanceListViewModel request)
        {
            WorkHistoryRequest filter = new WorkHistoryRequest();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(request.EncryptedVesselId);
            filter.ToDate = request.ToDate;
            filter.FromDate = request.FromDate;
            filter.VesselId = decreptedString.Split(Constants.Separator)[0];

            if (request.isSearchedClick == true)
            {
                filter.ResponsibilityIds = request.ResponsibilityIds;
                filter.ReschedueTypeIds = request.RescheduledIds;
                filter.JobTypeIds = request.JobTypeIds;
                filter.Criticality = request.IsCritical;

                if (!string.IsNullOrWhiteSpace(request.TopSystemAreaId))
                {
                    filter.TopSystemAreaId = request.TopSystemAreaId;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(request.ComponentId))
                    {
                        filter.ComponentId = request.ComponentId;
                        filter.ParentComponentId = request.ComponentId;
                    }
                    else
                    {
                        filter.CategoryId = request.CategoryId;
                        filter.ParentComponentId = request.ParentComponentId;
                        filter.ComponentId = request.ParentComponentId;
                    }
                }
            }
            else
            {
                if (request.StageName == EnumsHelper.GetKeyValue(ClosedWorkOrderHistoryStage.OverhaulCount))
                {
                    filter.JobTypeIds = new List<string>();
                    filter.JobTypeIds.Add(EnumsHelper.GetKeyValue(JobClassType.Overhaul));
                }
                else if (request.StageName == EnumsHelper.GetKeyValue(ClosedWorkOrderHistoryStage.RescheduledCount))
                {
                    filter.ReschedueTypeIds = new List<string>();
                    filter.ReschedueTypeIds.Add(EnumsHelper.GetKeyValue(RescheduleType.Reschedule));
                }
            }
            return filter;
        }

        /// <summary>
        /// Posts the get all work basket list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<WorkBasketAllDetailViewModel>> PostGetAllWorkBasketList(PlannedMaintenanceListViewModel request)
        {
            List<WorkBasketAllDetailViewModel> workBasketList = new List<WorkBasketAllDetailViewModel>();
            List<WorkBasketDetailResponse> response = new List<WorkBasketDetailResponse>();
            WorkBasketDetailRequest filter = new WorkBasketDetailRequest();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(request.EncryptedVesselId);
            filter.VesselId = decreptedString.Split(Constants.Separator)[0];
            filter.FromDate = request.FromDate;
            filter.ToDate = new DateTime(request.ToDate.Year, request.ToDate.Month, request.ToDate.Day, 23, 59, 59);
            filter.ShowDue = true;
            filter.ShowCurrentMonthOverdue = true;
            filter.ShowPreviousMonthsOverdue = true;

            List<string> workOrderStatusIds = new List<string>();
            workOrderStatusIds.Add(EnumsHelper.GetKeyValue<JobStatus>(JobStatus.WorkOrder));
            workOrderStatusIds.Add(EnumsHelper.GetKeyValue<JobStatus>(JobStatus.RescheduleRequested));
            workOrderStatusIds.Add(EnumsHelper.GetKeyValue<JobStatus>(JobStatus.ShipsWorkOrder));
            workOrderStatusIds.Add(EnumsHelper.GetKeyValue<JobStatus>(JobStatus.DefectWorkOrder));

            filter.WorkOrderStatusIds = workOrderStatusIds;
            filter.DepartmentIds = null;
            filter.JobTypeIds = null;
            filter.ResponsibilityIds = null;
            filter.OfficeApproval = -1;
            filter.Criticality = -1;

            var value = new Dictionary<string, object>()
                {
                    { "request", filter }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WorkBasketDetail"));
            response = await PostAsyncAutoPaged<WorkBasketDetailResponse>(requestUrl, value, 500);

            if (response != null && response.Any())
            {
                foreach (WorkBasketDetailResponse item in response)
                {
                    PlannedMaintenanceRequestViewModel plannedMaintenanceRequestUrl = new PlannedMaintenanceRequestViewModel();
                    plannedMaintenanceRequestUrl.FromDate = request.FromDate;
                    plannedMaintenanceRequestUrl.ToDate = request.ToDate;
                    plannedMaintenanceRequestUrl.EncryptedVesselId = request.EncryptedVesselId;
                    plannedMaintenanceRequestUrl.StageName = request.StageName;
                    plannedMaintenanceRequestUrl.ComponentId = item.ComponentId;
                    plannedMaintenanceRequestUrl.WorkOrderId = item.WorkOrderId;
                    plannedMaintenanceRequestUrl.ScheduleTaskId = item.ScheduleTaskId;
                    plannedMaintenanceRequestUrl.IsNavigatedFromDone = false;
                    string plannedMaintenanceDetailsRequest = _provider.CreateProtector("PMSDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(plannedMaintenanceRequestUrl));


                    WorkBasketAllDetailViewModel workBasket = new WorkBasketAllDetailViewModel();
                    workBasket.IsCritical = item.IsCritical.HasValue ? item.IsCritical.Value : false;
                    workBasket.JobType = item.Type;
                    workBasket.DueDate = item.DueDate;
                    workBasket.ComponentName = item.ComponentName;
                    workBasket.JobName = item.JobName;
                    workBasket.Status = item.Status;
                    workBasket.Interval = (item.Frequency ?? 0) + " " + item.FrequencyTypeShortCode;
                    workBasket.Resp = item.ResponsibleRankShortCode ?? "";
                    workBasket.LeftHours = item.LeftHours;
                    workBasket.PlannedMaintenanceDetailsRequestURL = plannedMaintenanceDetailsRequest;
                    workBasket.EncryptedVesselId = request.EncryptedVesselId;

                    //Checking current month Overdue.
                    if (item.DueDate != null && item.DueDate.Value.Date >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) && item.DueDate.Value.Date < DateTime.Now.Date)
                    {
                        workBasket.IsOverDueVisible = true;
                    }
                    else if (item.DueDate != null && item.DueDate.Value.Date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)) //Checking Overdue prior from current month  
                    {
                        workBasket.IsOverduePeriodVisible = true;
                    }
                    else
                    {
                        workBasket.IsDue = true;
                    }

                    workBasket.ClassCode = "";
                    workBasket.Dept = "";
                    workBasket.ReportForm = "";
                    workBasket.Days = 0;
                    workBasket.StDeleted = "";
                    workBasket.Attachments = "";
                    workBasket.OrderType = "";
                    workBasketList.Add(workBasket);
                }
            }

            List<WorkHistoryResponse> workHistoryResponse = null;
            WorkHistoryRequest workHistoryRequest = new WorkHistoryRequest();

            filter.ToDate = request.ToDate;
            filter.FromDate = request.FromDate;
            filter.VesselId = decreptedString.Split(Constants.Separator)[0];

            var workHistoryValue = new Dictionary<string, object>()
                {
                    { "request", filter }
                };

            Uri requestHistoryUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/ClosedWorkOrderHistory"));

            if (filter != null && !string.IsNullOrWhiteSpace(filter.VesselId))
            {
                workHistoryResponse = await PostAsyncAutoPaged<WorkHistoryResponse>(requestHistoryUrl, workHistoryValue, 500);
            }

            if (workHistoryResponse != null && workHistoryResponse.Any())
            {
                foreach (WorkHistoryResponse item in workHistoryResponse)
                {
                    PlannedMaintenanceRequestViewModel plannedMaintenanceRequestUrl = new PlannedMaintenanceRequestViewModel();
                    plannedMaintenanceRequestUrl.FromDate = request.FromDate;
                    plannedMaintenanceRequestUrl.ToDate = request.ToDate;
                    plannedMaintenanceRequestUrl.EncryptedVesselId = request.EncryptedVesselId;
                    plannedMaintenanceRequestUrl.StageName = request.StageName;
                    plannedMaintenanceRequestUrl.ComponentId = item.ComponentId;
                    plannedMaintenanceRequestUrl.WorkOrderId = item.WorkOrderId;
                    plannedMaintenanceRequestUrl.ScheduleTaskId = item.ScheduleTaskId;
                    plannedMaintenanceRequestUrl.IsNavigatedFromDone = true;
                    plannedMaintenanceRequestUrl.JobId = item.JobId;
                    plannedMaintenanceRequestUrl.WorkOrderHistoryId = item.WorkOrderHistoryId;
                    plannedMaintenanceRequestUrl.DefectWorkOrderId = item.DefectWorkOrderId;
                    plannedMaintenanceRequestUrl.WorkOrderIndicationId = item.WorkOrderIndicationId;

                    string plannedMaintenanceDetailsRequest = _provider.CreateProtector("PMSDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(plannedMaintenanceRequestUrl));

                    WorkBasketAllDetailViewModel workBasket = new WorkBasketAllDetailViewModel();
                    workBasket.IsCritical = item.IsCritical;
                    workBasket.DoneDate = item.WOCompletedDate;
                    workBasket.ClosedDate = item.WOClosedDate;
                    workBasket.ComponentName = item.ComponentName;
                    workBasket.ClassCode = item.ClassCode ?? "";
                    workBasket.JobName = item.JobName;
                    workBasket.Dept = item.DepartmentShortCode;
                    workBasket.Resp = item.ResponsibleRankShortCode;
                    workBasket.OrderType = item.WOClass;
                    workBasket.JobType = item.JobTypeShortCode;
                    workBasket.Interval = item.Interval;
                    workBasket.ReportForm = item.HasReportForm ? "Yes" : "";
                    workBasket.Days = item.NoOfDays.GetValueOrDefault();
                    workBasket.StDeleted = item.ScheduleTaskExists ? "No" : "Yes";
                    workBasket.RunningHours = item.ReportedRunningHours.GetValueOrDefault();
                    workBasket.Attachments = item.HasAttachments ? "Yes" : "";
                    workBasket.PlannedMaintenanceDetailsRequestURL = plannedMaintenanceDetailsRequest;
                    workBasket.EncryptedVesselId = request.EncryptedVesselId;

                    workBasketList.Add(workBasket);
                }
            }

            return workBasketList;
        }

        /// <summary>
        /// Posts the get vessel header detail.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<VesselPreviewViewModel> PostGetVesselHeaderDetail(string encryptedVesselId)
        {
            VesselPreviewViewModel vessel = new VesselPreviewViewModel();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselId);
            string vesselId = decreptedString.Split(Constants.Separator)[0];
            VesselPreview response = new VesselPreview();

            string urlvessel = "vesselId=" + vesselId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vessel/Header/"), urlvessel);

            if (!string.IsNullOrWhiteSpace(vesselId))
            {
                response = await PostAsync<VesselPreview>(requestUrl, CreateHttpContent(vesselId));
            }

            if (response != null)
            {
                vessel.Name = response.Name;
                vessel.Imo = response.Imo;
                vessel.Type = response.Type;
                vessel.VesselBuiltDate = response.VesselBuiltDate.HasValue ? response.VesselBuiltDate.Value.ToString("dd MMM yyyy") : "";
                vessel.VesselAge = CommonUtil.CalculateVesselAge(response.VesselBuiltDate) + " years";
                if ((response.MangagementEnd == null && response.MangagementStart == null) || (response.MangagementEnd != null && response.MangagementEnd < DateTime.Now))
                {
                    vessel.IsVesselInManagement = false;
                }
                else
                {
                    vessel.IsVesselInManagement = true;
                }
            }
            return vessel;
        }

        /// <summary>
        /// Posts the get component hierarchy.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<ComponentHierarchyResponseViewModel>> PostGetComponentHierarchy(PlannedMaintenanceDetailViewModel request)
        {
            List<ComponentHierarchyResponseViewModel> heirarchyResponse = new List<ComponentHierarchyResponseViewModel>();
            PlannedMaintenanceRequestViewModel detailsVM = new PlannedMaintenanceRequestViewModel();
            string data = _provider.CreateProtector("PMSDetails").Unprotect(request.PlannedMaintenanceRequestDetailsURL);
            detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);
            List<ComponentHierarchyResponse> response = null;

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(request.EncryptedVesselId);
            string vesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/ComponentHierarchy"));

            if (!string.IsNullOrWhiteSpace(vesselId) && !string.IsNullOrWhiteSpace(detailsVM.ComponentId))
            {
                var requestObject = new Dictionary<string, object>()
                {
                    { "vesselId", vesselId },
                    { "componentId", detailsVM.ComponentId},
                    { "systemAreaId", null}
                };
                response = await PostAsync<List<ComponentHierarchyResponse>>(requestUrl, CreateHttpContent(requestObject));
            }
            else
            {
                var requestObject = new Dictionary<string, object>()
                {
                    { "vesselId", vesselId },
                    { "componentId", null},
                    { "systemAreaId", detailsVM.SystemAreaId}
                };
                response = await PostAsync<List<ComponentHierarchyResponse>>(requestUrl, CreateHttpContent(requestObject));
            }

            if (response != null && response.Any())
            {
                var componentHeirarchy = response.OrderByDescending(x => x.Index).ToList();

                foreach (ComponentHierarchyResponse item in componentHeirarchy)
                {
                    ComponentHierarchyResponseViewModel component = new ComponentHierarchyResponseViewModel();
                    component.ComponentName = item.Name;
                    heirarchyResponse.Add(component);
                }
            }
            return heirarchyResponse;
        }

        /// <summary>
        /// Posts the get component header details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<ComponentSearchResponseViewModel> PostGetComponentHeaderDetails(PlannedMaintenanceDetailViewModel request)
        {
            ComponentSearchResponseViewModel component = new ComponentSearchResponseViewModel();
            PlannedMaintenanceRequestViewModel detailsVM = new PlannedMaintenanceRequestViewModel();
            string data = _provider.CreateProtector("PMSDetails").Unprotect(request.PlannedMaintenanceRequestDetailsURL);
            detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);
            ComponentSearchResponse response = null;

            if (!string.IsNullOrWhiteSpace(detailsVM.ComponentId))
            {
                string urlRequest = "componentId=" + detailsVM.ComponentId;
                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/ComponentHeaderDetail"), urlRequest);
                response = await PostAsync<ComponentSearchResponse>(requestUrl, CreateHttpContent(detailsVM.ComponentId));

            }

            if (response != null)
            {
                component.ComponentCode = response.ComponentCode ?? "";
                component.ComponentName = response.ComponentName ?? "";
                component.ComponentPosition = response.ComponentPosition ?? "";
                component.MakerName = response.MakerName ?? "";
                component.Model = response.Model ?? "";
                component.WarrantyDate = response.WarrantyDate != null && response.WarrantyDate.HasValue ? response.WarrantyDate.Value.ToString("dd MMM yyyy") : "";
                component.AlternativeNumberType = response.AlternativeNumberType ?? "";
                component.AlternativeNumber = response.AlternativeNumber ?? "";
                component.Designer = response.Designer ?? string.Empty;
            }

            return component;
        }

        /// <summary>
        /// Gets the work order details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<WorkOrderHeaderDetailViewModel> GetWorkOrderDetails(PlannedMaintenanceDetailViewModel request)
        {
            WorkOrderHeaderDetailViewModel workOrderHeader = new WorkOrderHeaderDetailViewModel();
            PlannedMaintenanceRequestViewModel detailsVM = new PlannedMaintenanceRequestViewModel();
            string data = _provider.CreateProtector("PMSDetails").Unprotect(request.PlannedMaintenanceRequestDetailsURL);
            detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);
            WorkOrderHeaderDetail response = null;

            if (!string.IsNullOrWhiteSpace(detailsVM.WorkOrderId))
            {
                if (!string.IsNullOrWhiteSpace(detailsVM.ScheduleTaskId))
                {
                    string urlRequest = "workOrderId=" + detailsVM.WorkOrderId;
                    Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WorkOrderHeader"), urlRequest);
                    response = await PostAsync<WorkOrderHeaderDetail>(requestUrl, CreateHttpContent(detailsVM.WorkOrderId));
                }
                else
                {
                    string urlRequest = "workOrderId=" + detailsVM.WorkOrderId;
                    Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/UnplannedWorkHeader"), urlRequest);
                    response = await PostAsync<WorkOrderHeaderDetail>(requestUrl, CreateHttpContent(detailsVM.WorkOrderId));
                }
            }

            if (response != null)
            {

                bool _isRunningHrsRangeWorkOrder = !string.IsNullOrWhiteSpace(response.ScheduleJobIntervalTypeId)
                         && response.ScheduleJobIntervalTypeId == EnumsHelper.GetKeyValue<ScheduleJobIntervalType>(ScheduleJobIntervalType.RunningHoursRange);
                var _isCalendarRangeWorkOrder = !string.IsNullOrWhiteSpace(response.ScheduleJobIntervalTypeId)
                                && response.ScheduleJobIntervalTypeId == EnumsHelper.GetKeyValue<ScheduleJobIntervalType>(ScheduleJobIntervalType.CalendarRange);
                workOrderHeader.IsInRangeWorkOrder = _isRunningHrsRangeWorkOrder || _isCalendarRangeWorkOrder;
                workOrderHeader.IsRunningHrsRangeWorkOrder = _isRunningHrsRangeWorkOrder;
                workOrderHeader.IsCalendarRangeWorkOrder = _isCalendarRangeWorkOrder;
                workOrderHeader.IsCbmTask = !string.IsNullOrWhiteSpace(response.CbtId);

                workOrderHeader.JobName = response.JobName;
                if (!string.IsNullOrWhiteSpace(response.CbtId))
                {
                    workOrderHeader.JobTypeDescription = response.JobTypeDescription;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(response.CbmTaskType))
                    {
                        workOrderHeader.JobTypeDescription = response.JobTypeDescription + " - " + response.CbmTaskType;
                    }
                    else
                    {
                        workOrderHeader.JobTypeDescription = response.JobTypeDescription;
                    }
                }
                workOrderHeader.WorkOrderStatusCode = response.WorkOrderStatusCode;
                workOrderHeader.IsStatusCompleted = !string.IsNullOrWhiteSpace(response.WorkOrderStatusId) && (response.WorkOrderStatusId.Equals(EnumsHelper.GetKeyValue(JobStatus.CompletedWorkOrder)));

                workOrderHeader.Interval = (response.IntervalValue != null && response.IntervalValue.HasValue ? response.IntervalValue.Value : 0) +
                    " " + response.DueDateIntervalType;

                workOrderHeader.RunningHoursRange = (response.FromIntervalValue != null && response.FromIntervalValue.HasValue ? response.FromIntervalValue.Value : 0) + " to " + (response.IntervalValue != null && response.IntervalValue.HasValue ? response.IntervalValue.Value : 0) + " " + response.DueDateIntervalType;

                workOrderHeader.CalendarRange = (response.FromIntervalValue != null && response.FromIntervalValue.HasValue ? response.FromIntervalValue.Value : 0) + " to " + (response.IntervalValue != null && response.IntervalValue.HasValue ? response.IntervalValue.Value : 0) + " " + response.DueDateIntervalType;

                workOrderHeader.DueDate = response.DueDate.ToString("dd MMM yyyy");
                workOrderHeader.CurrentDueDateRange = (response.FromDueDate != null && response.FromDueDate.HasValue ? response.FromDueDate.Value.ToString("dd MMM yyyy") : "") + " to " + response.DueDate.ToString("dd MMM yyyy");

                workOrderHeader.ComponentName = response.ComponentName;
                workOrderHeader.MakerName = response.MakerName;
                workOrderHeader.Model = response.Model;
                workOrderHeader.Designer = response.Designer;
                workOrderHeader.AlternateNumber = response.AlternateNumber;
                workOrderHeader.AlternateType = response.AlternateType;
                //For done - Round & Others navigation SystemAreaId is used
                if (!string.IsNullOrWhiteSpace(response.SystemAreaId))
                {
                    workOrderHeader.EncryptedSystemAreaId = _provider.CreateProtector("SystemAreaId").Protect(response.SystemAreaId);
                }

                workOrderHeader.CanProcessRescheduleWO = response.ScheduleJobIntervalTypeId != EnumsHelper.GetKeyValue(ScheduleJobIntervalType.CalendarRange) && response.ScheduleJobIntervalTypeId != EnumsHelper.GetKeyValue(ScheduleJobIntervalType.RunningHoursRange)
                    && (response.WorkOrderStatusId == EnumsHelper.GetKeyValue<JobStatus>(JobStatus.RescheduleRequested)
                    && response.RescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Pending));

                workOrderHeader.IntervalTypeId = response.DueDateIntervalTypeId;
                workOrderHeader.IntervalValue = response.IntervalValue;
                workOrderHeader.IsCritical = response.IsCritical;
                workOrderHeader.JobIntervalTypeId = response.ScheduleJobIntervalTypeId;
                workOrderHeader.VesselId = CommonUtil.GetDecryptedVesselId(_provider, detailsVM.EncryptedVesselId);
            }

            return workOrderHeader;
        }

        /// <summary>
        /// Posts the get work order specification.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<ReportWorkOrderViewModel> PostGetWorkOrderSpecification(PlannedMaintenanceDetailViewModel request)
        {
            ReportWorkOrderViewModel workOrderSpecification = new ReportWorkOrderViewModel();
            PlannedMaintenanceRequestViewModel detailsVM = new PlannedMaintenanceRequestViewModel();
            string data = _provider.CreateProtector("PMSDetails").Unprotect(request.PlannedMaintenanceRequestDetailsURL);
            detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);
            ReportWorkOrder response = null;

            if (!string.IsNullOrWhiteSpace(detailsVM.WorkOrderId))
            {
                string urlRequest = "workOrderId=" + detailsVM.WorkOrderId;
                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WorkOrderSpecification"), urlRequest);
                response = await PostAsync<ReportWorkOrder>(requestUrl, CreateHttpContent(detailsVM.WorkOrderId));
            }

            if (response != null)
            {
                workOrderSpecification.ResponsibleDepartmentShortCode = response.ResponsibleDepartmentShortCode;
                workOrderSpecification.ResponsibilityRankShortCode = response.ResponsibilityRankShortCode;
                if (response.ApproverRequired == true)
                {
                    if (!string.IsNullOrEmpty(response.ApproverRankShortCode))
                    {
                        workOrderSpecification.ApproverRequired = "Yes" + " - " + response.ApproverRankShortCode;
                    }
                    else
                    {
                        workOrderSpecification.ApproverRequired = "Yes";
                    }

                }
                else
                {
                    workOrderSpecification.ApproverRequired = "No";
                }

                workOrderSpecification.OfficeApprovalRequired = response.OfficeApprovalRequired == true ? "Yes" : "No";
                workOrderSpecification.DueDate = response.DueDate.ToString("dd MMM yyyy");
                workOrderSpecification.ShowCounterRunningHours = response.CounterReading != null && response.CounterReading > 0;
                workOrderSpecification.ShowCounterRevolutions = response.CounterRevolutionsReading != null && response.CounterRevolutionsReading > 0;
                workOrderSpecification.ShowCounterEvents = response.CounterEventsReading != null && response.CounterEventsReading > 0;

                workOrderSpecification.CounterReading = (response.CounterReading.HasValue && response.CounterReading != null ? response.CounterReading.Value : 0) + " hours";

                workOrderSpecification.CounterRevolutionsReading = (response.CounterRevolutionsReading.HasValue && response.CounterRevolutionsReading != null ? response.CounterRevolutionsReading.Value : 0) + " revolutions";

                workOrderSpecification.CounterEventsReading = (response.CounterEventsReading.HasValue && response.CounterEventsReading != null ? response.CounterEventsReading.Value : 0) + " events";

                int? LeftHours = null;
                if (response != null && response.ComponentCounterReadings != null && response.ComponentCounterReadings.Any())
                {
                    if (response.DueDateIntervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.RunningHours))
                    {
                        LeftHours = (response.CounterReading ?? 0) + response.DueInterval
                                        - response.ComponentCounterReadings.FirstOrDefault(x => string.IsNullOrWhiteSpace(x.IntervalTypeId) || x.IntervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.RunningHours)).RunningHours;
                    }
                    else if (response.DueDateIntervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Revolutions))
                    {
                        LeftHours = (response.CounterRevolutionsReading ?? 0) + response.DueInterval
                                        - response.ComponentCounterReadings.FirstOrDefault(x => x.IntervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Revolutions)).RunningHours;
                    }
                    else if (response.DueDateIntervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Events))
                    {
                        LeftHours = (response.CounterEventsReading ?? 0) + response.DueInterval
                                        - response.ComponentCounterReadings.FirstOrDefault(x => x.IntervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Events)).RunningHours;
                    }
                }
                workOrderSpecification.IsLeftHoursVisible = LeftHours != null;

                workOrderSpecification.LeftHours = LeftHours != null && LeftHours.HasValue ? LeftHours.Value.ToString() : "";

                //job description section
                workOrderSpecification.JobDescription = GetOfficeJobDescription(response);
                var separatedJobDescription = VesselJobDescriptionSeparated(workOrderSpecification.JobDescription);

                workOrderSpecification.JobDescriptionPart1 = separatedJobDescription.Item1;
                workOrderSpecification.JobDescriptionPart2 = separatedJobDescription.Item2;
                workOrderSpecification.JobDescriptionCheck = separatedJobDescription.Item3;

                //vessel guidelines section
                workOrderSpecification.PjbId = _provider.CreateProtector("PjbId").Protect(response.PjbId);

                // job rank and hour list
                workOrderSpecification.OrderRankList = GetWorkOrderRank(response.WorkOrderRanks);

                if (response.WorkOrderRanks != null && response.WorkOrderRanks.Any())
                {
                    workOrderSpecification.TotalManHours = response.WorkOrderRanks.Sum(x => x.ManHours);
                }
                else
                {
                    workOrderSpecification.TotalManHours = 0;
                }

                //Reschedule history work order log list
                //for due/overdue
                if (!string.IsNullOrWhiteSpace(response.PwoId))
                {
                    workOrderSpecification.PwoId = _provider.CreateProtector("PwoId").Protect(response.PwoId);
                }

                //for done
                if (!string.IsNullOrWhiteSpace(response.PwhId))
                {
                    workOrderSpecification.PwhId = _provider.CreateProtector("PwhId").Protect(response.PwhId);
                }


                var localDueDateIntervalTypeId = response.DueDateIntervalTypeId;

                workOrderSpecification.IsCounterBased = localDueDateIntervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.RunningHours)
                            || localDueDateIntervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Revolutions)
                            || localDueDateIntervalTypeId == EnumsHelper.GetKeyValue(JobIntervalType.Events);

                workOrderSpecification.JSARequired = response.JSARequired.GetValueOrDefault();
                workOrderSpecification.PermitRequired = response.JSAPermitRequired.GetValueOrDefault();
                workOrderSpecification.Critical = response.Critical.GetValueOrDefault();

            }

            return workOrderSpecification;
        }

        /// <summary>
        /// Posts the get unplanned wo specification.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<UnplannedWorkOrderDetailViewModel> GetUnplannedWOSpecification(PlannedMaintenanceDetailViewModel request)
        {
            PlannedMaintenanceRequestViewModel detailsVM = new PlannedMaintenanceRequestViewModel();
            string data = _provider.CreateProtector("PMSDetails").Unprotect(request.PlannedMaintenanceRequestDetailsURL);
            detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);
            UnplannedWorkOrderDetailViewModel result = new UnplannedWorkOrderDetailViewModel();
            UnplannedWorkOrderDetail response = null;
            if (!string.IsNullOrWhiteSpace(detailsVM.WorkOrderId))
            {
                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/UnplannedWOSpecification/" + detailsVM.WorkOrderId));
                response = await GetAsync<UnplannedWorkOrderDetail>(requestUrl);
            }
            if (response != null)
            {
                result.Approver = response.ApproverRankDescription;
                result.HodApproval = response.IsHODApprovalRequired ? "Yes" : "No";
                result.Responsibility = response.ResponsibleRankDescription;
                result.ResponsibleDepartment = response.ResponsibleDepartmentName;
                result.ShoreContractorInvolved = response.IsShoreStaffInvolved ? "Yes" : "No";

                result.WorkOrderHistoryId = response.WorkOrderHistoryId;
                result.ShowCurrentRWD = !string.IsNullOrWhiteSpace(response.WorkOrderHistoryId) && (response.WorkOrderStatus == JobStatus.ReOpenedWorkOrder || response.WorkOrderStatus == JobStatus.ReportedWorkOrder);

                if (response.JobDescription != null)
                {
                    result.Description = response.JobDescription.GuidelineText + "<br/>";
                    if (!string.IsNullOrWhiteSpace(response.JobDescription.Description) && !response.JobDescription.Description.StartsWith(Constants.HTMLStartString))
                    {
                        result.Description += GetJobDescriptionInHtmlFormat(response.JobDescription.Description);
                    }
                    else
                    {
                        result.Description += response.JobDescription.Description;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the office job description.
        /// </summary>
        /// <param name="WorkOrderDetail">The work order detail.</param>
        /// <returns></returns>
        private string GetOfficeJobDescription(ReportWorkOrder WorkOrderDetail)
        {
            string officeJobDescription = WorkOrderDetail.JobGuidelineText + "<br/>";
            if (!string.IsNullOrWhiteSpace(WorkOrderDetail.JobDescription) && !WorkOrderDetail.JobDescription.StartsWith(Constants.HTMLStartString))
            {

                officeJobDescription += GetJobDescriptionInHtmlFormat(WorkOrderDetail.JobDescription);
            }
            else
            {
                officeJobDescription += WorkOrderDetail.JobDescription;
            }

            return officeJobDescription;
        }

        /// <summary>
        /// Gets the job description in HTML format.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>
        /// String value.
        /// </returns>
        public static string GetJobDescriptionInHtmlFormat(string description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                description = description.Replace("\n", "<br/>");
                description = description.Replace("\r", "&nbsp;");
            }

            return description;
        }

        /// <summary>
        /// Posts the get vessel job description.
        /// </summary>
        /// <param name="PjbId">The PJB identifier.</param>
        /// <returns></returns>
        public async Task<string> PostGetVesselJobDescription(string PjbId)
        {
            if (!string.IsNullOrWhiteSpace(PjbId))
            {
                string decryptedPjbid = _provider.CreateProtector("PjbId").Unprotect(PjbId);
                string response = string.Empty;
                string urlRequest = "pjbId=" + decryptedPjbid;
                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/VesselJobDescription"), urlRequest);
                if (!string.IsNullOrWhiteSpace(decryptedPjbid))
                {
                    response = await PostAsync<string>(requestUrl, CreateHttpContent(decryptedPjbid));
                }

                return response;
            }
            return String.Empty;
        }

        /// <summary>
        /// Vessels the job description separated.
        /// </summary>
        /// <param name="jobDescription">The job description.</param>
        /// <returns></returns>
        public Tuple<string, string, bool> VesselJobDescriptionSeparated(string jobDescription)
        {
            string firstHalf = string.Empty;
            string secondHalf = string.Empty;

            if (!string.IsNullOrWhiteSpace(jobDescription))
            {
                int newLine = Regex.Matches(jobDescription, @"<br/>").Count;
                int indexCount = newLine > 8 ? 8 : 0;
                var firstHalfIndex = GetNthIndexOfString(jobDescription, indexCount);
                firstHalf = jobDescription.Substring(0, firstHalfIndex);
                if (indexCount == 0)
                {
                    var tupleEmptySecondHalf = Tuple.Create(firstHalf, secondHalf, false);
                    return tupleEmptySecondHalf;
                }
                else
                {
                    secondHalf = jobDescription.Substring(firstHalfIndex);
                    var tupleCompleteSecondHalf = Tuple.Create(firstHalf, secondHalf, true);
                    return tupleCompleteSecondHalf;
                }
            }
            var tuple = Tuple.Create(firstHalf, secondHalf, false);
            return tuple;
        }

        /// <summary>
        /// Gets the NTH index of string.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="indexCount">The index count.</param>
        /// <returns></returns>
        public int GetNthIndexOfString(string text, int indexCount)
        {
            int startIndex = 0;
            int currentIndex = 0;
            for (int counter = 0; counter < indexCount; counter++)
            {
                currentIndex = text.IndexOf("<br/>", startIndex + 1);
                startIndex = currentIndex;
            }
            return currentIndex;
        }

        /// <summary>
        /// Gets the work order rank.
        /// </summary>
        /// <param name="orderRankList">The order rank list.</param>
        /// <returns></returns>
        public List<WorkOrderRankViewModel> GetWorkOrderRank(List<WorkOrderRank> orderRankList)
        {
            List<WorkOrderRankViewModel> result = new List<WorkOrderRankViewModel>();
            if (orderRankList != null && orderRankList.Any())
            {
                foreach (var item in orderRankList)
                {
                    WorkOrderRankViewModel orderRank = new WorkOrderRankViewModel();
                    orderRank.RankDescription = item.RankDescription;
                    orderRank.RankShortCode = item.RankShortCode;
                    orderRank.ManHours = item.ManHours;
                    orderRank.TotalManHours = orderRankList.Sum(x => x.ManHours);
                    result.Add(orderRank);
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get work order required parts.
        /// </summary>
        /// <param name="scheduleTaskId">The schedule task identifier.</param>
        /// <returns></returns>
        public async Task<List<SearchPartResponseViewModel>> PostGetWorkOrderRequiredParts(string scheduleTaskId)
        {
            List<SearchPartResponseViewModel> sparePartList = new List<SearchPartResponseViewModel>();
            List<SearchPartResponse> response = null;
            string urlRequest = "scheduleTaskId=" + scheduleTaskId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WorkOrderRequiredParts"), urlRequest);

            if (!string.IsNullOrWhiteSpace(scheduleTaskId))
            {
                response = await PostAsync<List<SearchPartResponse>>(requestUrl, CreateHttpContent(scheduleTaskId));
            }

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    SearchPartResponseViewModel sparePartItem = new SearchPartResponseViewModel();
                    sparePartItem.PartName = item.PartName ?? "";
                    sparePartItem.MakerReferenceNumber = item.MakerReferenceNumber ?? "";
                    sparePartItem.PlateSheetNumber = item.PlateSheetNumber ?? "";
                    sparePartItem.DrawingPosition = item.DrawingPosition ?? "";
                    sparePartItem.PendingOrderCount = item.PendingOrderCount.GetValueOrDefault();
                    sparePartItem.QuantityROB = item.QuantityROB;
                    sparePartItem.IsRenewSpares = item.IsRenewSpares == true ? "Yes" : "No";
                    sparePartItem.QuantityRequired = item.QuantityRequired;
                    sparePartItem.IsQuantityRequiredGreaterThanROB = item.QuantityRequired > item.QuantityROB;
                    sparePartItem.IsMarkedForReorder = item.IsMarkedForReorder ? "Yes" : "No";
                    sparePartItem.ReorderQuantity = item.ReorderQuantity;
                    sparePartItem.Remarks = item.Remarks ?? "";
                    sparePartList.Add(sparePartItem);
                }
            }

            return sparePartList;
        }

        /// <summary>
        /// Gets the reschedule history logs.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<RescheduleWorkOrderDetailViewModel>> GetRescheduleHistoryLogs(PlannedMaintenanceDetailViewModel request)
        {
            List<RescheduleWorkOrderDetailViewModel> LogList = new List<RescheduleWorkOrderDetailViewModel>();
            List<RescheduleWorkOrderDetail> response = null;

            string decryptedPwoId = string.Empty;
            string decryptedPwhId = string.Empty;
            if (!string.IsNullOrWhiteSpace(request.PwoId))
            {
                //for due/overdue
                decryptedPwoId = _provider.CreateProtector("PwoId").Unprotect(request.PwoId);
            }

            if (!string.IsNullOrWhiteSpace(request.PwhId))
            {
                //for done
                decryptedPwhId = _provider.CreateProtector("PwhId").Unprotect(request.PwhId);
            }

            //to get stage
            PlannedMaintenanceRequestViewModel detailsVM = new PlannedMaintenanceRequestViewModel();
            string data = _provider.CreateProtector("PMSDetails").Unprotect(request.PlannedMaintenanceRequestDetailsURL);
            detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);

            if (!detailsVM.IsNavigatedFromDone && !detailsVM.IsSWO)
            {
                //when navigated from due/over due
                if (!string.IsNullOrWhiteSpace(decryptedPwoId))
                {
                    var input = new Dictionary<string, object>()
                    {
                        { "workOrderId", decryptedPwoId },
                        { "getActive", false }
                    };
                    Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WorkOrderRescheduleLog"));
                    response = await PostAsync<List<RescheduleWorkOrderDetail>>(requestUrl, CreateHttpContent(input));
                }
            }
            else
            {
                //when navigated form done
                if (!string.IsNullOrWhiteSpace(decryptedPwhId))
                {
                    string urlRequest = "workOrderHistoryId=" + decryptedPwhId;
                    Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/RescheduleHistory"), urlRequest);
                    response = await PostAsync<List<RescheduleWorkOrderDetail>>(requestUrl, CreateHttpContent(decryptedPwhId));
                }
            }

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    RescheduleWorkOrderDetailViewModel Log = new RescheduleWorkOrderDetailViewModel();
                    Log.RescheduleRequestType = item.RescheduleRequestType;
                    Log.OriginalDueDate = item.OriginalDueDate;
                    Log.RequestedDueDate = item.RequestedDueDate;
                    Log.NewDueDate = item.NewDueDate;
                    Log.WorkOrderReasonDescription = item.WorkOrderReasonDescription ?? "";
                    Log.RequestedBy = item.RequestedBy ?? "";
                    Log.RequesterRoleDescription = item.RequesterRoleDescription ?? "";
                    Log.ApprovedBy = item.ApprovedBy ?? "";
                    Log.ApproverRoleDescription = item.ApproverRoleDescription ?? "";
                    Log.OriginalInterval = item.OriginalInterval;
                    Log.RequestedInterval = item.RequestedInterval;
                    Log.RescheduledInterval = item.RescheduledInterval;
                    Log.RescheduleStatusDescription = item.RescheduleStatusDescription ?? "";
                    Log.Status = SetRescheduleStatusColor(item.RescheduleStatusId);
                    Log.WorkOrderRescheduleId = item.PorId;
                    Log.RescheduleRequestTypeId = item.RescheduleRequestTypeId;
                    LogList.Add(Log);
                }
            }

            return LogList;
        }

        /// <summary>
        /// Sets the color of the reschedule status.
        /// </summary>
        /// <param name="RescheduleStatusId">The reschedule status identifier.</param>
        /// <returns></returns>
        private string SetRescheduleStatusColor(string RescheduleStatusId)
        {
            string StatusColour = string.Empty;

            if (!string.IsNullOrWhiteSpace(RescheduleStatusId))
            {
                StatusColour = RescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Draft) ? "Normal"
                                       : RescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Revised) ? "Good"
                                       : RescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Approved) ? "Excellent"
                                       : RescheduleStatusId == EnumsHelper.GetKeyValue(WorkOrderRescheduleStatus.Rejected) ? "Critical" : "Normal";

            }
            else
            {
                StatusColour = "Normal";
            }
            return StatusColour;
        }

        /// <summary>
        /// Updates the work order status.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="refetchAfterSave">if set to <c>true</c> [refetch after save].</param>
        /// <returns></returns>
        public async Task<bool> UpdateWorkOrderStatus(WorkOrderStatusUpdateRequest request, bool refetchAfterSave)
        {
            var input = new Dictionary<string, object>()
                {
                    {"request", request },
                    {"refetchAfterSave",refetchAfterSave }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/UpdateWorkOrderStatus"));
            bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(input));
            return response;
        }

        #region Done

        /// <summary>
        /// Posts the get certficate work order history.
        /// </summary>
        /// <param name="WorkOrderHistoryId">The work order history identifier.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<CertificateReportWorkOrderViewModel> PostGetCertficateWorkOrderHistory(string WorkOrderHistoryId, string VesselId)
        {
            CertificateReportWorkOrderViewModel certificateReportViewModel = new CertificateReportWorkOrderViewModel();
            CertificateReportWorkOrder result = null;
            var value = new Dictionary<string, object>()
                {
                    { "vesselId", VesselId },
                    { "workOrderHistoryId", WorkOrderHistoryId}
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/CertficateWorkOrderHistory"));
            if (!string.IsNullOrWhiteSpace(WorkOrderHistoryId))
            {
                result = await PostAsync<CertificateReportWorkOrder>(requestUrl, CreateHttpContent(value));
            }

            if (result != null)
            {
                certificateReportViewModel.IsRunningHrsRangeWorkOrder = !string.IsNullOrWhiteSpace(result.JobIntervalTypeId)
                                && result.JobIntervalTypeId == EnumsHelper.GetKeyValue<ScheduleJobIntervalType>(ScheduleJobIntervalType.RunningHoursRange);
                certificateReportViewModel.IsCalendarRangeWorkOrder = !string.IsNullOrWhiteSpace(result.JobIntervalTypeId)
                                    && result.JobIntervalTypeId == EnumsHelper.GetKeyValue<ScheduleJobIntervalType>(ScheduleJobIntervalType.CalendarRange);
                certificateReportViewModel.IsInRangeWorkOrder = certificateReportViewModel.IsCalendarRangeWorkOrder || certificateReportViewModel.IsRunningHrsRangeWorkOrder;

                //header section
                certificateReportViewModel.WorkOrderName = result.WorkOrderName;
                certificateReportViewModel.JobType = result.JobType;
                certificateReportViewModel.WorkOrderStatusShortCode = result.WorkOrderStatusShortCode;
                certificateReportViewModel.Interval = result.Interval;
                certificateReportViewModel.IntervalType = result.IntervalType;
                certificateReportViewModel.FromRangeIntervalValue = result.FromRangeIntervalValue.HasValue ? result.FromRangeIntervalValue.Value.ToString() : "";
                certificateReportViewModel.DueDate = result.DueDate.HasValue ? result.DueDate.Value.ToString("dd MMM yyyy") : "";
                certificateReportViewModel.FromDueDate = result.FromDueDate.HasValue ? result.FromDueDate.Value.ToString("dd MMM yyyy") : "";

                //details section
                certificateReportViewModel.WorkDoneDate = result.WorkDoneDate.HasValue ? result.WorkDoneDate.Value.ToString("dd MMM yyyy") : "";
                certificateReportViewModel.OriginalDueDate = result.OriginalDueDate.HasValue ? result.OriginalDueDate.Value.ToString("dd MMM yyyy") : "";
                certificateReportViewModel.ResponsibleRank = result.ResponsibleRank ?? "";
                certificateReportViewModel.OfficeJobDescription = result.GuidelineTemplate + Environment.NewLine + result.JobDescription;

                certificateReportViewModel.OfficeJobDescription = GetJobDescriptionInHtmlFormat(certificateReportViewModel.OfficeJobDescription);

                var separatedJobDescription = VesselJobDescriptionSeparated(certificateReportViewModel.OfficeJobDescription);
                certificateReportViewModel.JobDescriptionPart1 = separatedJobDescription.Item1;
                certificateReportViewModel.JobDescriptionPart2 = separatedJobDescription.Item2;
                certificateReportViewModel.JobDescriptionCheck = separatedJobDescription.Item3;

                //Reschedule history work order log list - PwhId                
                //for done
                if (!string.IsNullOrWhiteSpace(result.WorkOrderHistoryId))
                {
                    certificateReportViewModel.PwhId = _provider.CreateProtector("PwhId").Protect(result.WorkOrderHistoryId);
                }

                certificateReportViewModel.IsCounterBased = false;
            }
            return certificateReportViewModel;
        }

        /// <summary>
        /// Posts the get work history detail.
        /// </summary>
        /// <param name="workOrderHistoryId">The work order history identifier.</param>
        /// <returns></returns>
        public async Task<ReportWorkOrderViewModel> PostGetWorkHistoryDetail(string workOrderHistoryId)
        {
            ReportWorkOrderViewModel WorkOrderDetails = new ReportWorkOrderViewModel();
            WorkOrderHistoryDetail result = null;
            if (!string.IsNullOrWhiteSpace(workOrderHistoryId))
            {
                string urlRequest = "workOrderHistoryId=" + workOrderHistoryId;
                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WorkHistoryDetail"), urlRequest);
                result = await PostAsync<WorkOrderHistoryDetail>(requestUrl, CreateHttpContent(workOrderHistoryId));
            }

            if (result != null && result.WorkOrderDetail != null)
            {
                WorkOrderDetails.ReportWorkDoneDate = result.WorkOrderDetail.WorkDoneDate != default(System.DateTime) ? result.WorkOrderDetail.WorkDoneDate.ToString("dd MMM yyyy") : "";
                WorkOrderDetails.RWDDueDate = result.WorkOrderDetail.DueDate != default(System.DateTime) ? result.WorkOrderDetail.DueDate.ToString("dd MMM yyyy") : "";
                WorkOrderDetails.ResponsibilityRank = result.WorkOrderDetail.ResponsibilityRank ?? "";

                WorkOrderDetails.JobDescription = result.WorkOrderDetail.JobGuidelineText + "<br/>";
                if (!string.IsNullOrWhiteSpace(result.WorkOrderDetail.JobDescription) && !result.WorkOrderDetail.JobDescription.StartsWith(Constants.HTMLStartString))
                {
                    WorkOrderDetails.JobDescription += GetJobDescriptionInHtmlFormat(result.WorkOrderDetail.JobDescription);
                }
                else
                {
                    //it is coming with classes so should it be put in iframe? -- Confirmation with Punit
                    WorkOrderDetails.JobDescription += result.WorkOrderDetail.JobDescription;
                }
            }

            return WorkOrderDetails;
        }

        /// <summary>
        /// Gets the work order history details.
        /// </summary>
        /// <param name="workOrderHistoryId">The work order history identifier.</param>
        /// <param name="scheduleTaskId">The schedule task identifier.</param>
        /// <returns></returns>
        public async Task<ReportWorkOrderViewModel> GetWorkOrderHistoryDetails(string workOrderHistoryId, string scheduleTaskId)
        {
            ReportWorkOrderViewModel WorkHeaderDetails = new ReportWorkOrderViewModel();
            WorkOrderHistoryDetail result = null;

            if (!string.IsNullOrWhiteSpace(workOrderHistoryId))
            {
                if (string.IsNullOrWhiteSpace(scheduleTaskId))
                {
                    string UnplannedWorkHistoryDetailUrlRequest = "workOrderHistoryId=" + workOrderHistoryId;
                    Uri UnplannedWorkHistoryDetailRequest = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/UnplannedWorkHistoryDetail"), UnplannedWorkHistoryDetailUrlRequest);
                    result = await PostAsync<WorkOrderHistoryDetail>(UnplannedWorkHistoryDetailRequest, CreateHttpContent(workOrderHistoryId));
                }
                else
                {
                    string WorkHistoryDetailUrlRequest = "workOrderHistoryId=" + workOrderHistoryId;
                    Uri WorkHistoryDetailRequest = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WorkHistoryDetail"), WorkHistoryDetailUrlRequest);
                    result = await PostAsync<WorkOrderHistoryDetail>(WorkHistoryDetailRequest, CreateHttpContent(workOrderHistoryId));
                }
            }

            if (result != null && result.WorkOrderDetail != null)
            {
                WorkHeaderDetails.ReportWorkDoneDate = result.WorkOrderDetail.WorkDoneDate != default(System.DateTime) ? result.WorkOrderDetail.WorkDoneDate.ToString("dd MMM yyyy") : "";
                WorkHeaderDetails.RWDDueDate = result.WorkOrderDetail.DueDate != default(System.DateTime) ? result.WorkOrderDetail.DueDate.ToString("dd MMM yyyy") : "";

                //visibility based on CbtId 
                //check nulll to visibility inverted is used ss
                //WorkHeaderDetails.IsCbtId = !string.IsNullOrWhiteSpace(result.WorkOrderDetail.CbtId) ? true : false;

                //null to visible inverted
                WorkHeaderDetails.IsCbtId = string.IsNullOrWhiteSpace(result.WorkOrderDetail.CbtId) ? true : false;
                WorkHeaderDetails.BeforeCondition = result.WorkOrderDetail.BeforeCondition;
                WorkHeaderDetails.AfterCondition = result.WorkOrderDetail.AfterCondition;

                //visibility for component reading - confirmation with Marine team is awaiting
                WorkHeaderDetails.ShowCounterRunningHours = result.WorkOrderDetail.CounterReading != null && result.WorkOrderDetail.CounterReading > 0;
                WorkHeaderDetails.CounterReading = (result.WorkOrderDetail.CounterReading.HasValue ? result.WorkOrderDetail.CounterReading.Value : 0) + " hours";

                WorkHeaderDetails.ShowCounterRevolutions = result.WorkOrderDetail.CounterRevolutionsReading != null && result.WorkOrderDetail.CounterRevolutionsReading > 0;
                WorkHeaderDetails.CounterRevolutionsReading = (result.WorkOrderDetail.CounterRevolutionsReading.HasValue ? result.WorkOrderDetail.CounterRevolutionsReading.Value : 0) + " revolutions";

                WorkHeaderDetails.ShowCounterEvents = result.WorkOrderDetail.CounterEventsReading != null && result.WorkOrderDetail.CounterEventsReading > 0;
                WorkHeaderDetails.CounterEventsReading = (result.WorkOrderDetail.CounterEventsReading.HasValue ? result.WorkOrderDetail.CounterEventsReading.Value : 0) + " events";

                //visibility
                WorkHeaderDetails.IsCommentForReason = !string.IsNullOrWhiteSpace(result.WorkOrderDetail.CommentForReason) ? true : false;
                WorkHeaderDetails.CommentForReason = result.WorkOrderDetail.CommentForReason;
                WorkHeaderDetails.Reason = result.WorkOrderDetail.Reason;

                //visibility based on CbtId                
                if (result.WorkOrderDetail.WorkOrderSymptoms != null && result.WorkOrderDetail.WorkOrderSymptoms.Any())
                {
                    WorkHeaderDetails.SymptomsObserved = string.Join(", ", result.WorkOrderDetail.WorkOrderSymptoms.Select(x => x.SymptomDescription));
                    WorkHeaderDetails.ShowOtherSymptom = result.WorkOrderDetail.WorkOrderSymptoms.Any(x => x.PwsId == EnumsHelper.GetKeyValue(WorkOrderSymptom.Other));
                    if (WorkHeaderDetails.ShowOtherSymptom)
                    {
                        WorkHeaderDetails.OtherSymptoms = result.WorkOrderDetail.WorkOrderSymptoms.Where(x => x.PwsId == EnumsHelper.GetKeyValue(WorkOrderSymptom.Other)).FirstOrDefault().SymptomComment;
                    }
                }

                WorkHeaderDetails.IsCounterBased = result.WorkOrderDetail.DueDateIntervalTypeId == EnumsHelper.GetKeyValue<JobIntervalType>(JobIntervalType.RunningHours)
                           || result.WorkOrderDetail.DueDateIntervalTypeId == EnumsHelper.GetKeyValue<JobIntervalType>(JobIntervalType.Revolutions)
                           || result.WorkOrderDetail.DueDateIntervalTypeId == EnumsHelper.GetKeyValue<JobIntervalType>(JobIntervalType.Events);

                WorkHeaderDetails.PwhId = _provider.CreateProtector("PwhId").Protect(result.WorkOrderDetail.PwhId);

                //job rank & hour
                if (result.WorkOrderDetail.WorkOrderRanks != null)
                {
                    WorkHeaderDetails.OrderRankList = GetWorkOrderRank(result.WorkOrderDetail.WorkOrderRanks);

                    WorkHeaderDetails.TotalManHours = result.WorkOrderDetail.WorkOrderRanks.Sum(x => x.ManHours);
                }
                else
                {
                    WorkHeaderDetails.TotalManHours = 0;
                }

                //visibility based on CbtId
                if (result.WorkOrderDetail.PartsUsed != null && string.IsNullOrEmpty(result.WorkOrderDetail.CbtId))
                {
                    WorkHeaderDetails.PartsUsed = SearchPartUsedList(result.WorkOrderDetail.PartsUsed);
                }

                WorkHeaderDetails.JobDescription = result.WorkOrderDetail.JobGuidelineText + "<br/>";
                if (!string.IsNullOrWhiteSpace(result.WorkOrderDetail.JobDescription) && !result.WorkOrderDetail.JobDescription.StartsWith(Constants.HTMLStartString))
                {
                    WorkHeaderDetails.JobDescription += GetJobDescriptionInHtmlFormat(result.WorkOrderDetail.JobDescription);
                }
                else
                {
                    WorkHeaderDetails.JobDescription += result.WorkOrderDetail.JobDescription;
                }

                WorkHeaderDetails.VesselJobDescription = result.WorkOrderDetail.VesselJobDescription;
                if (!string.IsNullOrWhiteSpace(result.WorkOrderDetail.VesselJobDescription) && !result.WorkOrderDetail.VesselJobDescription.StartsWith(Constants.HTMLStartString))
                {
                    WorkHeaderDetails.VesselJobDescription = GetJobDescriptionInHtmlFormat(result.WorkOrderDetail.VesselJobDescription);
                }

                WorkHeaderDetails.Remarks = result.WorkOrderDetail.Remark;
            }

            return WorkHeaderDetails;
        }

        /// <summary>
        /// Searches the part used list.
        /// </summary>
        /// <param name="PartsUsed">The parts used.</param>
        /// <returns></returns>
        public List<SearchPartResponseViewModel> SearchPartUsedList(List<SearchPartResponse> PartsUsed)
        {
            List<SearchPartResponseViewModel> partResponseVMList = new List<SearchPartResponseViewModel>();

            if (PartsUsed != null && PartsUsed.Any())
            {
                foreach (SearchPartResponse item in PartsUsed)
                {
                    SearchPartResponseViewModel part = new SearchPartResponseViewModel();
                    part.PartName = item.PartName ?? "";
                    part.MakerReferenceNumber = item.MakerReferenceNumber ?? "";
                    part.PlateSheetNumber = item.PlateSheetNumber ?? "";
                    part.DrawingPosition = item.DrawingPosition ?? string.Empty;
                    part.Location = item.Location ?? string.Empty;
                    part.Condition = item.Condition ?? string.Empty;
                    part.QuantityUsed = item.QuantityUsed;
                    partResponseVMList.Add(part);
                }
            }

            return partResponseVMList;
        }

        /// <summary>
        /// Posts the get component hierarchy for done round.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<ComponentHierarchyResponseViewModel>> PostGetComponentHierarchyForDoneRound(PlannedMaintenanceDetailViewModel request)
        {
            PlannedMaintenanceRequestViewModel detailsVM = new PlannedMaintenanceRequestViewModel();
            string data = _provider.CreateProtector("PMSDetails").Unprotect(request.PlannedMaintenanceRequestDetailsURL);
            detailsVM = Newtonsoft.Json.JsonConvert.DeserializeObject<PlannedMaintenanceRequestViewModel>(data);

            string descryptedSystemAreadId = string.Empty;
            if (!string.IsNullOrWhiteSpace(request.EncryptedSystemAreaId))
            {
                descryptedSystemAreadId = _provider.CreateProtector("SystemAreaId").Unprotect(request.EncryptedSystemAreaId);
            }

            List<ComponentHierarchyResponseViewModel> heirarchyResponse = new List<ComponentHierarchyResponseViewModel>();
            List<ComponentHierarchyResponse> response = null;

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(request.EncryptedVesselId);
            string vesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/ComponentHierarchy"));

            if (!string.IsNullOrWhiteSpace(vesselId) && !string.IsNullOrWhiteSpace(detailsVM.ComponentId))
            {
                var requestObject = new Dictionary<string, object>()
                {
                    { "vesselId", vesselId },
                    { "componentId", detailsVM.ComponentId},
                    { "systemAreaId", null}
                };
                response = await PostAsync<List<ComponentHierarchyResponse>>(requestUrl, CreateHttpContent(requestObject));
            }
            else
            {
                //call can be from round is selected from done - Round section - RoundsHistoryDialogViewModel - SystemAreaId is value passed from main details
                var requestObject = new Dictionary<string, object>()
                {
                    { "vesselId", vesselId },
                    { "componentId", null},
                    { "systemAreaId", descryptedSystemAreadId}
                };
                response = await PostAsync<List<ComponentHierarchyResponse>>(requestUrl, CreateHttpContent(requestObject));
            }

            if (response != null && response.Any())
            {
                var componentHeirarchy = response.OrderByDescending(x => x.Index).ToList();

                foreach (ComponentHierarchyResponse item in componentHeirarchy)
                {
                    ComponentHierarchyResponseViewModel component = new ComponentHierarchyResponseViewModel();
                    component.ComponentName = item.Name;
                    heirarchyResponse.Add(component);
                }
            }
            return heirarchyResponse;
        }

        #endregion

        #endregion

        #region Voyage Reporting

        /// <summary>
        /// Posts the get voyage activities paged.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<VoyageActivityReportViewModel>> PostGetVoyageActivitiesPaged(VoyageReportingRequestViewModel input)
        {
            List<VoyageActivityReportViewModel> voyageActivityList = new List<VoyageActivityReportViewModel>();

            string decryptedVesselString = _provider.CreateProtector("Vessel").Unprotect(input.EncryptedVesselDetail);
            string vesselId = decryptedVesselString.Split(Constants.Separator)[0];

            VoyageActivityReportRequest request = new VoyageActivityReportRequest();
            request.Item = new UserMenuItem();
            request.Item.Identifier = vesselId;
            request.Item.DisplayText = decryptedVesselString.Split(Constants.Separator)[1];
            request.Item.UserMenuItemType = input.MenuType;
            request.DateFrom = input.FromDate;
            request.DateTo = input.ToDate;
            request.VesselId = vesselId;

            var value = new Dictionary<string, object>()
                {
                    { "request", request }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/VoyageActivities"));
            List<VoyageActivityReport> response = await PostAsyncAutoPaged<VoyageActivityReport>(requestUrl, value, 50);
            VoyageReportingRequestViewModel voyageReportingRequest = null;
            VoyageReportingRequestViewModel voyageFromReportingRequest = null;
            VoyageReportingRequestViewModel voyageToReportingRequest = null;

            if (response != null)
            {
                response.ForEach(x =>
                {
                    voyageReportingRequest = new VoyageReportingRequestViewModel();
                    voyageReportingRequest.PositionListId = x.ActivityId;
                    voyageReportingRequest.VesselId = x.VesselId;
                    voyageReportingRequest.FromDate = input.FromDate;
                    voyageReportingRequest.ToDate = input.ToDate;
                    voyageReportingRequest.MenuType = input.MenuType;
                    voyageReportingRequest.VesselName = input.VesselName;
                    voyageReportingRequest.IsVesselLoadedFlag = x.IsVesselLoadedFlag;

                    voyageFromReportingRequest = new VoyageReportingRequestViewModel();
                    voyageFromReportingRequest.PositionListId = x.ActivityId;
                    voyageFromReportingRequest.VesselId = x.VesselId;
                    voyageFromReportingRequest.FromDate = input.FromDate;
                    voyageFromReportingRequest.ToDate = input.ToDate;
                    voyageFromReportingRequest.MenuType = input.MenuType;
                    voyageFromReportingRequest.VesselName = input.VesselName;
                    voyageFromReportingRequest.IsVesselLoadedFlag = x.IsVesselLoadedFlag;
                    voyageFromReportingRequest.PortId = x.FromPortId;

                    voyageToReportingRequest = new VoyageReportingRequestViewModel();
                    voyageToReportingRequest.PositionListId = x.ActivityId;
                    voyageToReportingRequest.VesselId = x.VesselId;
                    voyageToReportingRequest.FromDate = input.FromDate;
                    voyageToReportingRequest.ToDate = input.ToDate;
                    voyageToReportingRequest.MenuType = input.MenuType;
                    voyageToReportingRequest.VesselName = input.VesselName;
                    voyageToReportingRequest.IsVesselLoadedFlag = x.IsVesselLoadedFlag;
                    voyageToReportingRequest.PortId = x.ToPortId;

                    int agentCount = 0;
                    if (!string.IsNullOrEmpty(x.AgentName))
                    {
                        agentCount++;
                    }
                    if (!string.IsNullOrEmpty(x.Agent2Name))
                    {
                        agentCount++;
                    }
                    if (!string.IsNullOrEmpty(x.Agent3Name))
                    {
                        agentCount++;
                    }

                    PositionListDateStatus eospStatus = string.IsNullOrEmpty(x.EventCodes[0])
                                    ? PositionListDateStatus.EST : (PositionListDateStatus)Enum.Parse(typeof(PositionListDateStatus), EnumsHelper.GetEnumItemFromKeyValue(typeof(PositionListDateStatus), x.EventCodes[0]));

                    PositionListDateStatus berthStatus = string.IsNullOrEmpty(x.EventCodes[1])
                                    ? PositionListDateStatus.EST : (PositionListDateStatus)Enum.Parse(typeof(PositionListDateStatus), EnumsHelper.GetEnumItemFromKeyValue(typeof(PositionListDateStatus), x.EventCodes[1]));

                    PositionListDateStatus unBerthStatus = string.IsNullOrEmpty(x.EventCodes[2])
                                    ? PositionListDateStatus.EST : (PositionListDateStatus)Enum.Parse(typeof(PositionListDateStatus), EnumsHelper.GetEnumItemFromKeyValue(typeof(PositionListDateStatus), x.EventCodes[2]));

                    PositionListDateStatus faopStatus = string.IsNullOrEmpty(x.EventCodes[3])
                                    ? PositionListDateStatus.EST : (PositionListDateStatus)Enum.Parse(typeof(PositionListDateStatus), EnumsHelper.GetEnumItemFromKeyValue(typeof(PositionListDateStatus), x.EventCodes[3]));

                    voyageActivityList.Add(new VoyageActivityReportViewModel(x)
                    {
                        RequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequest)),
                        FromRequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageFromReportingRequest)),
                        ToRequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageToReportingRequest)),

                        EncryptedVesselDetail = input.EncryptedVesselDetail,
                        ActivityName = x.IsSeaPassageEvent ? x.IsVesselLoadedFlag ? Constants.SeaPassageLoaded : Constants.SeaPassageBallast : x.ActivityDescription ?? "",
                        FromPort = x.FromPortName + ", " + x.FromPortCountryCode,
                        ToPort = !string.IsNullOrWhiteSpace(x.ToPortName) ? !string.IsNullOrWhiteSpace(x.ToPortCountryCode) ? x.ToPortName + ", " + x.ToPortCountryCode : x.ToPortName : "",
                        HasFromPortAlert = x.PortAlertAdded ?? false,
                        HasToPortAlert = x.ToPortAlertAdded ?? false,
                        IsSeaPassageEvent = x.IsSeaPassageEvent,
                        HasPortAgent = x.HasPortAgent,
                        IsOffHire = x.IsOffHire,
                        IsDelay = x.IsDelay,
                        IsBadWeather = x.IsBadWeather,
                        CharterNumber = x.OfficeVoyageNumber ?? "",
                        VoyageNumber = x.ShipVoyageNumber ?? "",
                        IsVesselLoadedFlag = x.IsVesselLoadedFlag,
                        ActivityId = x.ActivityId,
                        LastReportEventDate = x.LastReportEventDate,

                        FromPortCountryName = x.FromPortCountryName,
                        FromPortIsKeyHubPort = x.FromPortIsKeyHubPort.GetValueOrDefault() ? "Yes" : "No",
                        FromPortUNLocode = x.FromPortUNLocode,
                        FromPortLat = GetLocationString(x.FromPortLatDegree, x.FromPortLatMin, x.FromPortLatIndicator),
                        FromPortLong = GetLocationString(x.FromPortLongDegree, x.FromPortLongMin, x.FromPortLongIndicator),
                        FromPortId = x.FromPortId,

                        ToPortCountryName = x.ToPortCountryName,
                        ToPortIsKeyHubPort = x.ToPortIsKeyHubPort.GetValueOrDefault() ? "Yes" : "No",
                        ToPortUNLocode = x.ToPortUNLocode,
                        ToPortLat = GetLocationString(x.ToPortLatDegree, x.ToPortLatMin, x.ToPortLatIndicator),
                        ToPortLong = GetLocationString(x.ToPortLongDegree, x.ToPortLongMin, x.ToPortLongIndicator),
                        ToPortId = x.ToPortId,

                        EospDate = x.EventDates[0] != null ? x.EventDates[0].Value.ToString(Constants.DateTime24HrFormat) : "-",
                        BerthedDate = x.EventDates[1] != null ? x.EventDates[1].Value.ToString(Constants.DateTime24HrFormat) : "-",
                        UnBerthedDate = x.EventDates[2] != null ? x.EventDates[2].Value.ToString(Constants.DateTime24HrFormat) : "-",
                        FaopDate = x.EventDates[3] != null ? x.EventDates[3].Value.ToString(Constants.DateTime24HrFormat) : "-",

                        EospStatus = GetDateHeader(eospStatus, PortEventType.EOSP),
                        BerthStatus = GetDateHeader(berthStatus, PortEventType.BERTHED),
                        UnBerthStatus = GetDateHeader(unBerthStatus, PortEventType.UNBERTHED),
                        FaopStatus = GetDateHeader(faopStatus, PortEventType.FAOP),

                        Agent1Name = x.AgentName,
                        Agent2Name = x.Agent2Name,
                        Agent3Name = x.Agent3Name,
                        AgentCount = agentCount,
                        Agent1Type = !string.IsNullOrWhiteSpace(x.Agent1Id) ? EnumsHelper.GetEnumNameFromKeyValue(typeof(VesselAgentType), x.Agent1Status) : string.Empty,
                        Agent2Type = !string.IsNullOrWhiteSpace(x.Agent2Id) ? EnumsHelper.GetEnumNameFromKeyValue(typeof(VesselAgentType), x.Agent2Status) : string.Empty,
                        Agent3Type = !string.IsNullOrWhiteSpace(x.Agent3Id) ? EnumsHelper.GetEnumNameFromKeyValue(typeof(VesselAgentType), x.Agent3Status) : string.Empty,
                    });
                });
            }
            return voyageActivityList;
        }

        /// <summary>
        /// Gets the location string.
        /// </summary>
        /// <param name="degree">The degree.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="indicator">The indicator.</param>
        /// <returns></returns>
        private string GetLocationString(decimal? degree, decimal? min, string indicator)
        {
            string location = string.Empty;
            if (degree.HasValue)
            {
                location += degree.Value.ToString("0.00").Replace(".00", String.Empty);
                location += "°, ";
            }
            if (min.HasValue)
            {
                location += min.Value.ToString("0.00");
                location += "' ";
                location += indicator;
            }
            return location;
        }

        /// <summary>
        /// Posts the get bad weather detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<BadWeatherViewModel> PostGetBadWeatherDetail(string input)
        {
            BadWeatherViewModel result = new BadWeatherViewModel();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            string queryString = "posId=" + voyageReportingRequestVM.PositionListId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/BadWeatherDetail"), queryString);
            VoyageEventBadWeatherDetailsWrapper response = await PostAsync<VoyageEventBadWeatherDetailsWrapper>(requestUrl, CreateHttpContent(voyageReportingRequestVM.PositionListId));

            if (response != null)
            {
                result.BadWeatherList = new List<BadWeatherDetailViewModel>();
                if (response.BadWeatherDetails != null && response.BadWeatherDetails.Any())
                {
                    response.BadWeatherDetails.ForEach(x =>
                    {
                        result.BadWeatherList.Add(
                            new BadWeatherDetailViewModel
                            {
                                EventDate = x.EventDate,
                                EventName = x.EventName ?? "",
                                MaxSwellLengthDscription = x.MaxSwellLengthDscription ?? "",
                                MaxWindForce = x.MaxWindForce ?? ""
                            });
                    });

                    result.CharterSwellLength = response.CharterSwellLength ?? "";
                    result.CharterWindForce = response.CharterWindForce ?? "";
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the get delays.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<DelayListViewModel>> PostGetDelays(string input)
        {
            List<DelayListViewModel> result = new List<DelayListViewModel>();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            string queryString = "positionListId=" + voyageReportingRequestVM.PositionListId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/Delays"), queryString);
            List<VoyageActivityDelay> response = await PostAsync<List<VoyageActivityDelay>>(requestUrl, CreateHttpContent(voyageReportingRequestVM.PositionListId));

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    x.DelayDuration = x.DateTo - x.DateFrom;
                    result.Add(
                        new DelayListViewModel
                        {
                            ActivityDescription = x.ActivityDescription ?? "",
                            DateFrom = x.DateFrom,
                            DateTo = x.DateTo,
                            DelayDuration = x.DelayDuration,
                            DelayDurationHours = string.Format(Constants.TimeFormat, (int)(x.DelayDuration.HasValue ? x.DelayDuration.Value.TotalHours : 0)),
                            DelayDurationMinutes = string.Format(Constants.TimeFormat, (x.DelayDuration.HasValue ? x.DelayDuration.Value.Minutes : 0)),
                        });
                });
            }

            return result;
        }

        /// <summary>
        /// Posts the get sea passage details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<CharterDetailViewModel> PostGetSeaPassageDetails(string input)
        {
            CharterDetailViewModel result = new CharterDetailViewModel();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            string queryString = "positionListId=" + voyageReportingRequestVM.PositionListId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/SeaPassageDetails"), queryString);
            SeaPassageCharterDetail response = await PostAsync<SeaPassageCharterDetail>(requestUrl, CreateHttpContent(voyageReportingRequestVM.PositionListId));

            if (response != null)
            {
                result.CharterName = response.ChtCompany;
                result.VoyageNumber = response.ChtVoycode;
                result.Trade = response.TradeType;

                string officeVoyage = response.ChtCompanyCode;
                if (!string.IsNullOrWhiteSpace(response.ChtVessel))
                { officeVoyage = officeVoyage + "/" + response.ChtVessel; }

                if (!string.IsNullOrWhiteSpace(response.ChtType))
                { officeVoyage = officeVoyage + "/" + response.ChtType; }

                if (!string.IsNullOrWhiteSpace(response.ChtNum))
                { officeVoyage = officeVoyage + "/" + response.ChtNum; }

                if (!string.IsNullOrWhiteSpace(response.ChtVoyage))
                { officeVoyage = officeVoyage + "/" + response.ChtVoyage; }

                result.CharterNumber = officeVoyage;

                result.CharterRequirementsList = new List<CharterRequirementsViewModel>() {
                    new CharterRequirementsViewModel()
                    {
                        FuelType ="SPEED (Kts)",
                        LoadedValue = response.EstimatedLoadedSpeed.HasValue ? response.EstimatedLoadedSpeed.Value : 0.0F,
                        BallastValue = response.EstimatedBallastSpeed.HasValue ? response.EstimatedBallastSpeed.Value : 0.0F,
                        ActualValue = response.ActualSpeed.HasValue ? response.ActualSpeed.Value : 0.0F
                    },
                    new CharterRequirementsViewModel()
                    {
                        FuelType ="FO (mt)",
                        LoadedValue = response.EstimatedLoadedFoPerDay.HasValue ? response.EstimatedLoadedFoPerDay.Value : 0.0F,
                        BallastValue = response.EstimatedBallastFoPerDay.HasValue ? response.EstimatedBallastFoPerDay .Value : 0.0F,
                        ActualValue = response.ActualFoPerDay.HasValue ? response.ActualFoPerDay.Value : 0.0F
                    },
                    new CharterRequirementsViewModel()
                    {
                        FuelType ="LSFO (mt)",
                        LoadedValue = response.EstimatedLoadedLsFoPerDay.HasValue ? response.EstimatedLoadedLsFoPerDay.Value : 0.0F,
                        BallastValue = response.EstimatedBallastLsFoPerDay.HasValue ? response.EstimatedBallastLsFoPerDay.Value : 0.0F,
                        ActualValue = response.ActualLsFoPerDay.HasValue ? response.ActualLsFoPerDay.Value : 0.0F
                    },
                    new CharterRequirementsViewModel()
                    {
                        FuelType ="DO (mt)",
                        LoadedValue = response.EstimatedLoadedDoPerDay.HasValue ? response.EstimatedLoadedDoPerDay.Value : 0.0F,
                        BallastValue = response.EstimatedBallastDoPerDay.HasValue ? response.EstimatedBallastDoPerDay.Value : 0.0F,
                        ActualValue = response.ActualDoPerDay.HasValue ? response.ActualDoPerDay.Value : 0.0F
                    },
                    new CharterRequirementsViewModel()
                    { FuelType="GO (mt)",
                        LoadedValue = response.EstimatedLoadedGoPerDay.HasValue ? response.EstimatedLoadedGoPerDay.Value : 0.0F,
                        BallastValue = response.EstimatedBallastGoPerDay.HasValue ? response.EstimatedBallastGoPerDay.Value : 0.0F,
                        ActualValue = response.ActualGoPerDay.HasValue ? response.ActualGoPerDay.Value : 0.0F
                    },
                    new CharterRequirementsViewModel()
                    {
                        FuelType ="LNG (mt)",
                        LoadedValue = response.EstimatedLoadedLngPerDay.HasValue ? response.EstimatedLoadedLngPerDay.Value : 0.0F,
                        BallastValue = response.EstimatedBallastLngPerDay.HasValue ? response.EstimatedBallastLngPerDay.Value : 0.0F,
                        ActualValue = response.ActualLngPerDay.HasValue ? response.ActualLngPerDay.Value : 0.0F
                    }
                };
            }

            return result;
        }

        /// <summary>
        /// Posts the get port call detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<PortCallDetailViewModel> PostGetPortCallDetail(string input)
        {
            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);
            PortCallDetailViewModel result = await PostGetPortCallDetailAsync(voyageReportingRequestVM.PositionListId);

            return result;
        }

        /// <summary>
        /// Posts the get port call detail async.
        /// </summary>
        /// <param name="positionListId"></param>
        /// <returns></returns>
        private async Task<PortCallDetailViewModel> PostGetPortCallDetailAsync(string positionListId)
        {
            PortCallDetailViewModel result = new PortCallDetailViewModel();
            string queryString = "positionListId=" + positionListId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/PortCallDetail"), queryString);
            PortActivityDetails response = await PostAsync<PortActivityDetails>(requestUrl, CreateHttpContent(positionListId));

            if (response != null)
            {
                result.CharterName = response.ChtCompany;
                result.VoyageNumber = response.ChtVoycode ?? "";
                result.Trade = response.TradeType;
                result.Port = response.Port;
                result.ChtCompanyCode = response.ChtCompanyCode;

                result.EospDateHeader = GetDateHeader(response.EospStatus, PortEventType.EOSP);
                result.FaopDateHeader = GetDateHeader(response.FaopStatus, PortEventType.FAOP);
                result.BerthDateHeader = GetDateHeader(response.BerthStatus, PortEventType.BERTHED);
                result.UnBerthDateHeader = GetDateHeader(response.UnBerthStatus, PortEventType.UNBERTHED);

                result.CargoOperationTime = FormatTimespanToDaysHrsMins(response.CargoOperationTime);
                result.OutofserviceTime = FormatTimespanToDaysHrsMins(response.OutofserviceTime);
                result.BerthTime = FormatTimespanToDaysHrsMins(response.BerthTime);
                result.TotalTime = FormatTimespanToDaysHrsMins(response.TotalTime);
                result.EOSP = response.EOSP != null ? response.EOSP.Value.ToString(Constants.DateTime24HrFormat) : "";
                result.Berthed = response.Berthed != null ? response.Berthed.Value.ToString(Constants.DateTime24HrFormat) : "";
                result.UnBerthed = response.UnBerthed != null ? response.UnBerthed.Value.ToString(Constants.DateTime24HrFormat) : "";
                result.Faop = response.Faop != null ? response.Faop.Value.ToString(Constants.DateTime24HrFormat) : "";

                result.EOSPDate = response.EOSP;
                result.BerthedDate = response.Berthed;
                result.UnBerthedDate = response.UnBerthed;
                result.FaopDate = response.Faop;

                string officeVoyage = response.ChtCompanyCode;
                if (!string.IsNullOrWhiteSpace(response.ChtVessel))
                { officeVoyage = officeVoyage + "/" + response.ChtVessel; }
                if (!string.IsNullOrWhiteSpace(response.ChtType))
                { officeVoyage = officeVoyage + "/" + response.ChtType; }
                if (!string.IsNullOrWhiteSpace(response.ChtNum))
                { officeVoyage = officeVoyage + "/" + response.ChtNum; }
                if (!string.IsNullOrWhiteSpace(response.ChtVoyage))
                { officeVoyage = officeVoyage + "/" + response.ChtVoyage; }

                result.CharterNumber = officeVoyage;
            }

            return result;
        }

        /// <summary>
        /// Gets the date header.
        /// </summary>
        /// <param name="dateStatus">The date status.</param>
        /// <param name="eventType">Type of the event.</param>
        /// <returns>
        /// header name
        /// </returns>
        private string GetDateHeader(PositionListDateStatus? dateStatus, PortEventType eventType)
        {
            switch (eventType)
            {
                case PortEventType.BERTHED:
                    switch (dateStatus)
                    {
                        case PositionListDateStatus.ACT:
                            return Constants.BTHD;
                        case PositionListDateStatus.EST:
                            return Constants.ETB;
                        default:
                            return Constants.ETB;
                    }
                case PortEventType.EOSP:
                    switch (dateStatus)
                    {
                        case PositionListDateStatus.ACT:
                            return Constants.EOSP;
                        case PositionListDateStatus.EST:
                            return Constants.ETA;
                        default:
                            return Constants.ETA;
                    }
                case PortEventType.FAOP:
                    switch (dateStatus)
                    {
                        case PositionListDateStatus.ACT:
                            return Constants.FAOP;
                        case PositionListDateStatus.EST:
                            return Constants.ETS;
                        default:
                            return Constants.ETS;
                    }
                case PortEventType.UNBERTHED:
                    switch (dateStatus)
                    {
                        case PositionListDateStatus.ACT:
                            return Constants.UNBTHD;
                        case PositionListDateStatus.EST:
                            return Constants.ETDBRTH;
                        default:
                            return Constants.ETDBRTH;
                    }
                default:
                    break;
            }
            return null;
        }

        /// <summary>
        /// Formats the timespan to days HRS mins.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private string FormatTimespanToDaysHrsMins(TimeSpan? data)
        {
            string result = "00 days 00 hrs 00 min";

            if (data != null)
            {
                TimeSpan value = data.Value;
                result = string.Format(Constants.TimeFormat, value.Days) + " days " + string.Format(Constants.TimeFormat, value.Hours) + " hrs " + string.Format(Constants.TimeFormat, value.Minutes) + " min";
            }

            return result;
        }

        /// <summary>
        /// Posts the off hire by position identifier.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<OffHireListViewModel>> PostOffHireByPosId(string input)
        {
            List<OffHireListViewModel> result = new List<OffHireListViewModel>();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            string queryString = "positionListId=" + voyageReportingRequestVM.PositionListId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/OffHireByPosId"), queryString);
            List<VoyageActivityOffHire> response = await PostAsync<List<VoyageActivityOffHire>>(requestUrl, CreateHttpContent(voyageReportingRequestVM.PositionListId));

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    x.DelayDuration = x.DateTo - x.DateFrom;
                    result.Add(
                        new OffHireListViewModel
                        {
                            Activity = x.ActivityDescription ?? "",
                            DateFrom = x.DateFrom,
                            DateTo = x.DateTo,
                            OffHireDuration = x.DelayDuration,
                            OffHireDurationHours = string.Format(Constants.TimeFormat, (int)(x.DelayDuration.HasValue ? x.DelayDuration.Value.TotalHours : 0)),
                            OffHireDurationMinutes = string.Format(Constants.TimeFormat, (x.DelayDuration.HasValue ? x.DelayDuration.Value.Minutes : 0)),
                        });
                });
            }
            return result;
        }

        /// <summary>
        /// Posts the get voyage activity detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<VoyageActivityReportViewModel> PostGetVoyageActivityDetail(string input)
        {
            VoyageActivityReportViewModel voyageActivity = new VoyageActivityReportViewModel();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            string queryString = "posId=" + voyageReportingRequestVM.PositionListId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/VoyageActivityDetail"), queryString);

            VoyageActivityReport response = await PostAsync<VoyageActivityReport>(requestUrl, CreateHttpContent(voyageReportingRequestVM.PositionListId));

            if (response != null)
            {
                voyageActivity.ActivityDescription = response.ActivityDescription;
                voyageActivity.HasPortAgent = response.HasPortAgent;
                voyageActivity.ActivityName = response.IsVesselLoadedFlag ? Constants.SeaPassageLoaded : Constants.SeaPassageBallast;
            }
            return voyageActivity;
        }

        /// <summary>
        /// Posts the get port call header.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<PortCallHeaderDetailViewModel> PostGetPortCallHeader(string input)
        {
            PortCallHeaderDetailViewModel result = new PortCallHeaderDetailViewModel();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            var value = new Dictionary<string, object>()
                {
                    { "posId", voyageReportingRequestVM.PositionListId }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/PortCallHeader"));
            PortCallHeaderDetail response = await PostAsync<PortCallHeaderDetail>(requestUrl, CreateHttpContent(value));

            if (response != null)
            {
                result.LastUpdatedDate = response.LastUpdatedDate != null ? response.LastUpdatedDate.Value.ToString(Constants.DateTime24HrFormat) : "";
            }
            return result;
        }

        /// <summary>
        /// Posts the get port agent detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<AgentDetailViewModel>> PostGetPortAgentDetail(string input)
        {
            List<AgentDetailViewModel> result = new List<AgentDetailViewModel>();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            string queryString = "posId=" + voyageReportingRequestVM.PositionListId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/PortAgentDetail"), queryString);
            PortCallAgentDetail response = await PostAsync<PortCallAgentDetail>(requestUrl, CreateHttpContent(voyageReportingRequestVM.PositionListId));

            if (response != null)
            {
                if (!string.IsNullOrWhiteSpace(response.Agent1Id))
                {
                    result.Add(new AgentDetailViewModel
                    {
                        EncryptedAgentId = _provider.CreateProtector("Company").Protect(response.Agent1Id),
                        AgentType = EnumsHelper.GetEnumNameFromKeyValue(typeof(VesselAgentType), response.Agent1Status)
                    });
                }
                if (!string.IsNullOrWhiteSpace(response.Agent2Id))
                {
                    result.Add(new AgentDetailViewModel
                    {
                        EncryptedAgentId = _provider.CreateProtector("Company").Protect(response.Agent2Id),
                        AgentType = EnumsHelper.GetEnumNameFromKeyValue(typeof(VesselAgentType), response.Agent2Status)
                    });
                }
                if (!string.IsNullOrWhiteSpace(response.Agent3Id))
                {
                    result.Add(new AgentDetailViewModel
                    {
                        EncryptedAgentId = _provider.CreateProtector("Company").Protect(response.Agent3Id),
                        AgentType = EnumsHelper.GetEnumNameFromKeyValue(typeof(VesselAgentType), response.Agent3Status)
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get port call location events.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<PortCallLocationEventDetailViewModel>> PostGetPortCallLocationEvents(string input)
        {
            List<PortCallLocationEventDetailViewModel> result = new List<PortCallLocationEventDetailViewModel>();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            string queryString = "positionListId=" + voyageReportingRequestVM.PositionListId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/PortCallLocationEvents"), queryString);
            List<PortCallLocationEventDetail> response = await PostAsync<List<PortCallLocationEventDetail>>(requestUrl, CreateHttpContent(voyageReportingRequestVM.PositionListId));
            DocumentDetailRequest documentDetailRequest = null;

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    documentDetailRequest = new DocumentDetailRequest();

                    documentDetailRequest.SourceId = x.EventId;
                    documentDetailRequest.SsmId = EnumsHelper.GetKeyValue(SubModule.PortEvent);

                    x.TimeElapsed = x.ToDate - x.FromDate;

                    result.Add(
                        new PortCallLocationEventDetailViewModel
                        {
                            DocumentRequestUrl = _provider.CreateProtector("DocumentURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(documentDetailRequest)),
                            EventName = x.EventName,
                            IsInComplete = ((x.PpfId == EnumsHelper.GetKeyValue(PortEventType.AnchorageCommencedOrCompleted) || x.PpfId == EnumsHelper.GetKeyValue(PortEventType.RiverPassageCommencedOrCompleted) || x.PpfId == EnumsHelper.GetKeyValue(PortEventType.CanalPassageCommencedOrCompleted)) && !x.ToDate.HasValue) || (x.PsfIsIncompleteEvent == true),
                            FromDate = x.FromDate,
                            ToDate = x.ToDate,
                            ElapsedTime = x.TimeElapsed,
                            TotalElapsedHours = string.Format(Constants.TimeFormat, (int)(x.TimeElapsed.HasValue ? x.TimeElapsed.Value.TotalHours : 0)),
                            TotalElapsedMinutes = string.Format(Constants.TimeFormat, (x.TimeElapsed.HasValue ? x.TimeElapsed.Value.Minutes : 0)),
                            IsLop = x.Lop.GetValueOrDefault(),
                            IsOffHire = x.OffHire.GetValueOrDefault(),
                            IsBadWeather = x.IsBadWeather,
                            IsDelay = x.IsDelay,
                            Distance = x.Distance,
                            TotalFo = x.TotalFo,
                            TotalLsfo = x.TotalLsfo,
                            TotalDo = x.TotalDo,
                            TotalGo = x.TotalGo,
                            TotalLng = x.TotalLng,
                            FwDomestic = x.FwDomestic,
                            FwTechnical = x.FwTechnical,
                            HasDocuments = x.HasDocuments,
                            Comments = x.Comments,
                            PpfId = x.PpfId,
                            PsfId = x.EventId,
                            PosId = x.PositionListId,
                            VesselId = x.VesselId,
                            ViewName = x.ViewName,
                            TotalLngCargo = x.TotalLngCargo
                        });
                });
            }

            return result;
        }


        /// <summary>
        /// Posts the get sea passage reports.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<SeaPassageActivityViewModel> PostGetSeaPassageReports(string input)
        {
            SeaPassageActivity response = new SeaPassageActivity();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            var value = new Dictionary<string, object>()
            {
                { "vesselId", voyageReportingRequestVM.VesselId },
                { "positionId", voyageReportingRequestVM.PositionListId }
            };
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/SeaPassageReports"));
            response = await PostAsync<SeaPassageActivity>(requestUrl, CreateHttpContent(value));

            SeaPassageActivityViewModel SeaPassageActivityList = new SeaPassageActivityViewModel();

            if (response != null && response.ActivityDetails != null && response.ActivityDetails.Any())
            {
                SeaPassageActivityList = SeaPassageActivityList.SetViewModelProperty(response);
                SeaPassageActivityList.ActivityDetails = new List<SeaPassageReportDetailsViewModel>();

                foreach (var item in response.ActivityDetails)
                {
                    //need to check this flag working properly or not
                    SeaPassageReportDetailsViewModel _seaPassageActivity = new SeaPassageReportDetailsViewModel(item, voyageReportingRequestVM.IsVesselLoadedFlag);

                    SeaPassageActivityList.ActivityDetails.Add(_seaPassageActivity);

                    SeaPassageActivityList.TotalLNG = SeaPassageActivityList.TotalLNG + item.LNG;
                    SeaPassageActivityList.TotalFreshWaterConsumptionDomestic = SeaPassageActivityList.TotalFreshWaterConsumptionDomestic + item.FreshWaterConsumptionDomestic;
                    SeaPassageActivityList.TotalFreshWaterConsumptionTechnial = SeaPassageActivityList.TotalFreshWaterConsumptionTechnial + item.FreshWaterConsumptionTechnial;
                    SeaPassageActivityList.TotalLubeOilConsumptionClo = SeaPassageActivityList.TotalLubeOilConsumptionClo + item.CloLubOilConsumption;
                    SeaPassageActivityList.TotalLubeOilConsumptionCrankCase = SeaPassageActivityList.TotalLubeOilConsumptionCrankCase + item.CrankLubOilConsumption;
                    SeaPassageActivityList.TotalLubeOilConsumptionAux = SeaPassageActivityList.TotalLubeOilConsumptionAux + item.AuxLubOilConsumption;
                    SeaPassageActivityList.TotalGeneralLubOilConsumption = SeaPassageActivityList.TotalGeneralLubOilConsumption + item.GeneralLubOilConsumption;
                }
                SeaPassageActivityList.IsVesselLoadedFlag = voyageReportingRequestVM.IsVesselLoadedFlag;
                //radial bar chart
                float? radialAverageSpeed = 0;
                if (response.TotalDistance != null && !string.IsNullOrWhiteSpace(response.TotalTime))
                {
                    int seperatorIndex = response.TotalTime.IndexOf(@":");
                    string hoursString = response.TotalTime.Substring(0, seperatorIndex);
                    string minutesString = response.TotalTime.Substring(seperatorIndex + 1, 2);
                    int hours = Convert.ToInt32(hoursString);
                    int minutes = Convert.ToInt32(minutesString);
                    float totalHours = hours + ((float)minutes / 60);

                    if (totalHours > 0)
                    {
                        radialAverageSpeed = response.TotalDistance / totalHours;
                    }
                }

                SeaPassageHeaderStatisticViewModel statisticsViewModel = new SeaPassageHeaderStatisticViewModel(SeaPassageActivityList.ActivityDetails.Last(), voyageReportingRequestVM.IsVesselLoadedFlag, radialAverageSpeed.GetValueOrDefault());
                SeaPassageActivityList.SpeedStatistics = statisticsViewModel;

                //bar chart
                TimeSpan totalTime = new TimeSpan();
                if (!string.IsNullOrWhiteSpace(response.TotalTime))
                {
                    List<string> totalTimeList = response.TotalTime.Split(':').ToList();
                    totalTime = new TimeSpan(totalTimeList != null && totalTimeList[0] != null ? Convert.ToInt32(totalTimeList[0]) : default(int), totalTimeList != null && totalTimeList[1] != null ? Convert.ToInt32(totalTimeList[1]) : default(int), 0);
                }

                SeaPassageActivityList.BarChartStats = LoadSeaPassageHeaderStatisticList(totalTime, SeaPassageActivityList.ActivityDetails.Last(), voyageReportingRequestVM.IsVesselLoadedFlag, radialAverageSpeed, SeaPassageActivityList);
            }

            return SeaPassageActivityList;
        }

        /// <summary>
        /// Loads the sea passage header statistic list.
        /// </summary>
        /// <param name="totalTime">The total time.</param>
        /// <param name="details">The details.</param>
        /// <param name="IsVesselLoadedFlag">if set to <c>true</c> [is vessel loaded flag].</param>
        /// <param name="AverageSpeed">The average speed.</param>
        /// <param name="Activity">The activity.</param>
        /// <returns></returns>
        private SeaPassageFuelConsumptionDetailsViewModel LoadSeaPassageHeaderStatisticList(TimeSpan totalTime, SeaPassageReportDetailsViewModel details, bool IsVesselLoadedFlag, float? AverageSpeed, SeaPassageActivityViewModel Activity)
        {
            SeaPassageFuelConsumptionDetailsViewModel BarChartStats = new SeaPassageFuelConsumptionDetailsViewModel();

            List<SeaPassageHeaderStatisticViewModel> CharterOrdersStatistics = new List<SeaPassageHeaderStatisticViewModel>();
            CharterOrdersStatistics.Add(new SeaPassageHeaderStatisticViewModel
            {
                Title = Constants.SpeedLabel,
                FirstCount = IsVesselLoadedFlag ? (decimal)details.SpeedCharterRequirementLoaded : (decimal)details.SpeedCharterRequirementBallast,
                SecondCount = (decimal)AverageSpeed.GetValueOrDefault(),
                IsGreater = AverageSpeed.HasValue ? (IsVesselLoadedFlag ? (decimal)AverageSpeed.Value < (decimal)details.SpeedCharterRequirementLoaded : (decimal)AverageSpeed.Value < (decimal)details.SpeedCharterRequirementBallast) : false
            });
            CharterOrdersStatistics.Add(new SeaPassageHeaderStatisticViewModel
            {
                Title = Constants.FOLabel,
                FirstCount = IsVesselLoadedFlag ? (decimal)details.FoCharterRequirementLoaded : (decimal)details.FoCharterRequirementBallast,
                // calculating daily fuel consumption (fuel consumption in 24 hrs) 
                // e.g. totalFuelConsump / totalTime (hrs) = 24HrsFuelConsump / 24 (hrs)
                // therefore  24HrsFuelConsump = (totalFuelConsump * 24 (hrs)) / totalTime (hrs)
                SecondCount = totalTime.TotalHours != 0 ? (decimal)((Activity.TotalFo.GetValueOrDefault() * 24) / totalTime.TotalHours) : 0.0M,
            });
            CharterOrdersStatistics.Add(new SeaPassageHeaderStatisticViewModel
            {
                Title = Constants.LSFOLabel,
                FirstCount = IsVesselLoadedFlag ? (decimal)details.LsfoCharterRequirementLoaded : (decimal)details.LsfoCharterRequirementBallast,
                SecondCount = totalTime.TotalHours != 0 ? (decimal)((Activity.TotalLsfo.GetValueOrDefault() * 24) / totalTime.TotalHours) : 0.0M,
            });
            CharterOrdersStatistics.Add(new SeaPassageHeaderStatisticViewModel
            {
                Title = Constants.DOLabel,
                FirstCount = IsVesselLoadedFlag ? (decimal)details.DoCharterRequirementLoaded : (decimal)details.DoCharterRequirementBallast,
                SecondCount = totalTime.TotalHours != 0 ? (decimal)((Activity.TotalDo.GetValueOrDefault() * 24) / totalTime.TotalHours) : 0.0M,
            });
            CharterOrdersStatistics.Add(new SeaPassageHeaderStatisticViewModel
            {
                Title = Constants.GOLabel,
                FirstCount = IsVesselLoadedFlag ? (decimal)details.GoCharterRequirementLoaded : (decimal)details.GoCharterRequirementBallast,
                SecondCount = totalTime.TotalHours != 0 ? (decimal)((Activity.TotalGo.GetValueOrDefault() * 24) / totalTime.TotalHours) : 0.0M,
            });
            CharterOrdersStatistics.Add(new SeaPassageHeaderStatisticViewModel
            {
                Title = Constants.LNGLabel,
                FirstCount = IsVesselLoadedFlag ? (decimal)details.LNGCharterRequirementLoaded.GetValueOrDefault() : (decimal)details.LNGCharterRequirementBallast.GetValueOrDefault(),
                SecondCount = totalTime.TotalHours != 0 ? (decimal)((Activity.TotalLNG.GetValueOrDefault() * 24) / totalTime.TotalHours) : 0.0M,
            });

            if (CharterOrdersStatistics != null && CharterOrdersStatistics.Any())
            {
                Dictionary<string, Tuple<decimal, decimal>> fuelDetails = GetTopTwoFuelConsumptionDetails(CharterOrdersStatistics.ToList());
                if (fuelDetails.Count >= 2)
                {
                    // Item1 contains charter consumption
                    List<FuelConsumptionViewModel> LoadedConsumptionDetails = new List<FuelConsumptionViewModel>()

                    {
                        new FuelConsumptionViewModel() { Type = fuelDetails.First().Key, Value = fuelDetails.First().Value.Item1 },
                        new FuelConsumptionViewModel() { Type = fuelDetails.Skip(1).First().Key, Value = fuelDetails.Skip(1).First().Value.Item1 }
                    };
                    // Item2 contains actual consumption
                    List<FuelConsumptionViewModel> ActualConsumptionDetails = new List<FuelConsumptionViewModel>()
                    {
                        new FuelConsumptionViewModel() { Type = fuelDetails.First().Key, Value = fuelDetails.First().Value.Item2 },
                        new FuelConsumptionViewModel() { Type = fuelDetails.Skip(1).First().Key, Value = fuelDetails.Skip(1).First().Value.Item2 }
                    };
                    var fuelConsumpList = new List<decimal>();
                    fuelConsumpList.AddRange(LoadedConsumptionDetails.Select(obj => obj.Value).ToList());
                    fuelConsumpList.AddRange(ActualConsumptionDetails.Select(obj => obj.Value).ToList());
                    var maxFuelConsump = (double)fuelConsumpList.Max();
                    if (maxFuelConsump == 0)
                    {
                        maxFuelConsump = double.NaN;
                    }
                    else
                    {
                        //maxFuelConsump = (Constant.MaxGraphPixelHeight * maxFuelConsump) / (Constant.MaxGraphPixelHeight - Constant.BarLabelPixelHeight);
                    }
                    //MaxFuelConsump = maxFuelConsump;
                    BarChartStats.FirstItemLabelName = fuelDetails.First().Key;
                    BarChartStats.FirstItemCharterValue = decimal.Round(fuelDetails.First().Value.Item1, 2);
                    BarChartStats.FirstItemActualValue = decimal.Round(fuelDetails.First().Value.Item2, 2);

                    BarChartStats.SecondItemLabelName = fuelDetails.Skip(1).First().Key;
                    BarChartStats.SecondItemCharterValue = decimal.Round(fuelDetails.Skip(1).First().Value.Item1, 2);
                    BarChartStats.SecondItemActualValue = decimal.Round(fuelDetails.Skip(1).First().Value.Item2, 2);
                    BarChartStats.CharterName = IsVesselLoadedFlag ? Constants.CharterLoaded : Constants.CharterBallast;
                }
            }

            return BarChartStats;
        }


        /// <summary>
        /// Gets the top two fuel consumption details.
        /// </summary>
        /// <param name="seaPassageStatistics">The sea passage statistics.</param>
        /// <returns></returns>
        private Dictionary<string, Tuple<decimal, decimal>> GetTopTwoFuelConsumptionDetails(List<SeaPassageHeaderStatisticViewModel> seaPassageStatistics)
        {
            Dictionary<string, Tuple<decimal, decimal>> mappedFuelConsumByFuelType = GetMappedFuelConsumActualAndCharterByFuelTypeInRequiredOrder(seaPassageStatistics);
            // when get only one fuel consumption
            if (mappedFuelConsumByFuelType.Count == 1)
            {
                // if not contain do then adding do with other fuel consumption
                if (!mappedFuelConsumByFuelType.Keys.Contains(Constants.DOLabel))
                {
                    SeaPassageHeaderStatisticViewModel statDo = seaPassageStatistics.FirstOrDefault(obj => obj.Title == Constants.DOLabel);
                    mappedFuelConsumByFuelType.Add(Constants.DOLabel, new Tuple<decimal, decimal>(statDo.FirstCount, statDo.SecondCount));
                }
                else // else not contain do then adding do with other fuel consumption
                {
                    foreach (var fuelType in GetFuelTypeByConsumptionOrder())
                    {
                        if (!mappedFuelConsumByFuelType.Keys.Contains(fuelType))
                        {
                            SeaPassageHeaderStatisticViewModel statFuel = seaPassageStatistics.FirstOrDefault(obj => obj.Title == fuelType);
                            mappedFuelConsumByFuelType.Add(fuelType, new Tuple<decimal, decimal>(statFuel.FirstCount, statFuel.SecondCount));
                            break;
                        }
                    }
                }
            }
            else if (mappedFuelConsumByFuelType.Count == 0) // when get non fuel consumption
            {
                SeaPassageHeaderStatisticViewModel statFo = seaPassageStatistics.FirstOrDefault(obj => obj.Title == Constants.FOLabel);
                if (statFo != null)
                {
                    mappedFuelConsumByFuelType.Add(Constants.FOLabel, new Tuple<decimal, decimal>(statFo.FirstCount, statFo.SecondCount));
                }

                SeaPassageHeaderStatisticViewModel statDo = seaPassageStatistics.FirstOrDefault(obj => obj.Title == Constants.DOLabel);
                if (statDo != null)
                {
                    mappedFuelConsumByFuelType.Add(Constants.DOLabel, new Tuple<decimal, decimal>(statDo.FirstCount, statDo.SecondCount));
                }
            }
            return mappedFuelConsumByFuelType;
        }

        /// <summary>
        /// Gets the fuel type by consumption order.
        /// </summary>
        /// <returns></returns>
        private List<string> GetFuelTypeByConsumptionOrder()
        {
            return new List<string>
            {
                Constants.FOLabel,
                Constants.GOLabel,
                Constants.LSFOLabel,
                Constants.DOLabel,
                Constants.LNGLabel
            };
        }

        /// <summary>
        /// Gets the mapped fuel consum actual and charter by fuel type in required order.
        /// </summary>
        /// <param name="seaPassageStatistics">The sea passage statistics.</param>
        /// <returns></returns>
        private Dictionary<string, Tuple<decimal, decimal>> GetMappedFuelConsumActualAndCharterByFuelTypeInRequiredOrder(List<SeaPassageHeaderStatisticViewModel> seaPassageStatistics)
        {
            Dictionary<string, Tuple<decimal, decimal>> mappedFuelConsumByFuelType = new Dictionary<string, Tuple<decimal, decimal>>();
            foreach (var fuelType in GetFuelTypeByConsumptionOrder())
            {
                if (seaPassageStatistics != null && seaPassageStatistics.Any(obj => obj.Title == fuelType))
                {
                    SeaPassageHeaderStatisticViewModel fuelStat = seaPassageStatistics.FirstOrDefault(obj => obj.Title == fuelType);
                    if (fuelStat.SecondCount != 0)
                    {
                        mappedFuelConsumByFuelType.Add(fuelType, new Tuple<decimal, decimal>(fuelStat.FirstCount, fuelStat.SecondCount));
                    }
                }
            }
            return mappedFuelConsumByFuelType;
        }

        /// <summary>
        /// Gets the sea passage header details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<SeaPassageHeaderDetailViewModel> GetSeaPassageHeaderDetails(string input)
        {
            SeaPassageHeaderDetailViewModel SeaPassageHeader = new SeaPassageHeaderDetailViewModel();
            SeaPassageHeaderDetail response = new SeaPassageHeaderDetail();
            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(input);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            var value = new Dictionary<string, object>()
            {
                { "positionListId", voyageReportingRequestVM.PositionListId },
                { "spaId", null }
            };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/SeaPassageHeader"));

            response = await PostAsync<SeaPassageHeaderDetail>(requestUrl, CreateHttpContent(value));

            if (response != null)
            {
                SeaPassageHeader.LastUpdatedEventDate = response.LastUpdatedEventDate.HasValue ? response.LastUpdatedEventDate.Value.ToString(Constants.DateTime24HrFormat) : string.Empty;
            }

            return SeaPassageHeader;
        }

        /// <summary>
        /// Posts the get voyage landing page detail.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<VoyageLandingPageDetailsViewModel> PostGetVoyageLandingPageDetail(string vesselId)
        {
            VoyageLandingPageDetailsViewModel response = null;
            List<VoyageLandingPageDetails> voyageLandingList = new List<VoyageLandingPageDetails>();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);

            voyageLandingList = await GetVoyageLandingList(voyageLandingList, decreptedString);

            if (voyageLandingList != null && voyageLandingList.Any())
            {
                VoyageLandingPageDetails voyage = voyageLandingList.FirstOrDefault();

                response = new VoyageLandingPageDetailsViewModel(voyage, _provider);
                VoyageReportingRequestViewModel voyageReportingRequest = GetVoyageReportingRequest(decreptedString, voyage);

                VoyageReportingRequestViewModel voyageFromReportingRequest = GetVoyageReportingRequest(decreptedString, voyage);
                voyageFromReportingRequest.PortId = voyage.FromPortId;

                VoyageReportingRequestViewModel voyageToReportingRequest = GetVoyageReportingRequest(decreptedString, voyage);
                voyageToReportingRequest.PortId = string.IsNullOrWhiteSpace(voyage.ToPortId) ? voyage.NextPortId : voyage.ToPortId;

                VoyageReportingRequestViewModel voyageFromAgentRequest = new VoyageReportingRequestViewModel();
                voyageFromAgentRequest.PositionListId = (voyage.IsAgentAvailable ?? false) ? voyage.POS_ID : voyage.PreviousActivityId;
                voyageFromAgentRequest.VesselId = voyage.VES_ID;

                VoyageReportingRequestViewModel voyageToAgentRequest = new VoyageReportingRequestViewModel();
                voyageToAgentRequest.PositionListId = voyage.NextActivityId;
                voyageToAgentRequest.VesselId = voyage.VES_ID;

                response.RequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequest));
                response.FromRequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageFromReportingRequest));
                response.ToRequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageToReportingRequest));

                response.FromAgentRequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageFromAgentRequest));
                response.ToAgentRequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageToAgentRequest));

                if (!string.IsNullOrEmpty(response.POS_ID))
                {
                    PortCallDetailViewModel result = await PostGetPortCallDetailAsync(response.POS_ID);
                    if (result != null)
                    {
                        response.CharterName = !string.IsNullOrWhiteSpace(result.CharterName) ? result.CharterName : "-";
                        response.CharterNumber = !string.IsNullOrWhiteSpace(result.CharterNumber) ? result.CharterNumber : "-";
                        response.VoyageNumber = !string.IsNullOrWhiteSpace(result.VoyageNumber) ? result.VoyageNumber : "-";
                    }
                }

                if (!response.IsSeaPassageEvent)
                {
                    PortHeaderDetailsViewModel portCallheader = await PostGetPortHeaderDetailsByPortId(response.FromRequestURL);
                    if (portCallheader != null)
                    {
                        response.PortFullName = !string.IsNullOrWhiteSpace(portCallheader.PortFullName) ? portCallheader.PortFullName : "-";
                        response.CountryName = !string.IsNullOrWhiteSpace(portCallheader.CountryName) ? portCallheader.CountryName : "-";
                        response.CountryCode = !string.IsNullOrWhiteSpace(portCallheader.CountryCode) ? portCallheader.CountryCode : "-";
                        response.FullLongitude = !string.IsNullOrWhiteSpace(portCallheader.FullLongitude) ? portCallheader.FullLongitude : "-";
                        response.FullLatitude = !string.IsNullOrWhiteSpace(portCallheader.FullLatitude) ? portCallheader.FullLatitude : "-";
                        response.Unlocode = !string.IsNullOrWhiteSpace(portCallheader.Unlocode) ? portCallheader.Unlocode : "-";
                        response.IsKeyHubPort = !string.IsNullOrWhiteSpace(portCallheader.IsKeyHubPort) ? portCallheader.IsKeyHubPort : "-";
                    }

                    PortCallHeaderDetailViewModel seaPassageHeader = await PostGetPortCallHeader(response.RequestURL);
                    if (seaPassageHeader != null)
                    {
                        response.LastUpdatedEventDate = !string.IsNullOrWhiteSpace(seaPassageHeader.LastUpdatedDate) ? seaPassageHeader.LastUpdatedDate : "-";
                    }

                }

            }

            return response;
        }

        /// <summary>
        /// Posts the get voyage detail.
        /// </summary>
        /// <param name="vesselIds">The vessel ids.</param>
        /// <returns></returns>
        public async Task<List<VoyageDetailsViewModel>> PostGetVoyageDetail(List<string> vesselIds)
        {
            List<string> vesselList = new List<string>();
            vesselList.AddRange(vesselIds.Select(x => CommonUtil.GetDecryptedVessel(_provider, x).Split(Constants.Separator)[0]));
            List<VoyageDetailsViewModel> response = new List<VoyageDetailsViewModel>();
            List<VoyageDetails> voyageList = new List<VoyageDetails>();

            DateTime currentDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var request = new Dictionary<string, object>()
            {
                { "vesselIds", vesselList },
                { "currentDate", currentDate }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/VoyageDetail"));
            voyageList = await PostAsync<List<VoyageDetails>>(requestUrl, CreateHttpContent(request));

            if (voyageList != null && voyageList.Any())
            {
                foreach (VoyageDetails item in voyageList)
                {
                    VoyageDetailsViewModel obj = new VoyageDetailsViewModel();
                    obj.DistanceTravelled = item.DistanceTravelled;
                    obj.TotalDistance = item.TotalDistance;
                    obj.RemainingValue = item.TotalDistance - item.DistanceTravelled;
                    obj.LastEventPosition = item.LantitudeDegree + "°, " + item.LantitudeMinute + "' " + item.LantitudeDirection + " " + item.LongitudeDegree + "°, " + item.LongitudeMinute + "' " + item.LongitudeDirection;
                    obj.VesselId = item.VesselId;
                    obj.EncryptedVesselId = vesselIds.Where(x => CommonUtil.GetDecryptedVessel(_provider, x).Split(Constants.Separator)[0] == item.VesselId).FirstOrDefault();
                    obj.IsSeaPassageEvent = !string.IsNullOrWhiteSpace(item.POS_ID) && item.PLA_ID == "SP";
                    response.Add(obj);
                }
            }

            return response;
        }

        /// <summary>
        /// Gets the voyage landing list.
        /// </summary>
        /// <param name="voyageLandingList">The voyage landing list.</param>
        /// <param name="decreptedString">The decrepted string.</param>
        /// <returns></returns>
        private async Task<List<VoyageLandingPageDetails>> GetVoyageLandingList(List<VoyageLandingPageDetails> voyageLandingList, string decreptedString)
        {
            var request = GetVoyageLandingRequest(decreptedString);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/VoyageLandingPageDetail"));
            voyageLandingList = await PostAsync<List<VoyageLandingPageDetails>>(requestUrl, CreateHttpContent(request));
            return voyageLandingList;
        }

        /// <summary>
        /// Gets the voyage reporting request.
        /// </summary>
        /// <param name="decreptedString">The decrepted string.</param>
        /// <param name="voyage">The voyage.</param>
        /// <returns></returns>
        private static VoyageReportingRequestViewModel GetVoyageReportingRequest(string decreptedString, VoyageLandingPageDetails voyage)
        {
            VoyageReportingRequestViewModel voyageReportingRequest = new VoyageReportingRequestViewModel();
            voyageReportingRequest.PositionListId = voyage.POS_ID;
            voyageReportingRequest.VesselId = decreptedString.Split(Constants.Separator)[0];
            voyageReportingRequest.MenuType = UserMenuItemType.Vessel;
            voyageReportingRequest.FromDate = voyage.FromDate.HasValue ? voyage.FromDate.Value.Date.AddMonths(-1) : DateTime.Now;
            voyageReportingRequest.ToDate = voyage.ToDate.HasValue ? voyage.ToDate.Value.Date.AddMonths(1) : voyage.FromDate.HasValue ? voyage.FromDate.Value.Date.AddMonths(1) : DateTime.Now;
            voyageReportingRequest.NextActivityId = voyage.NextActivityId;
            voyageReportingRequest.PreviousActivityId = voyage.PreviousActivityId;
            return voyageReportingRequest;
        }

        /// <summary>
        /// Gets the voyage landing request.
        /// </summary>
        /// <param name="decreptedString">The decrepted string.</param>
        /// <returns></returns>
        private Dictionary<string, object> GetVoyageLandingRequest(string decreptedString)
        {
            UserMenuItem menuItem = new UserMenuItem();
            menuItem.Identifier = decreptedString.Split(Constants.Separator)[0];
            menuItem.DisplayText = decreptedString.Split(Constants.Separator)[1];
            menuItem.UserMenuItemType = UserMenuItemType.Vessel;
            DateTime currentDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            var request = new Dictionary<string, object>()
            {
                { "itemType", menuItem },
                { "currentDate", currentDate }
            };
            return request;
        }

        /// <summary>
        /// Posts the get voyage landing voyage reporting request.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<VoyageReportingRequestViewModel> PostGetVoyageLandingVoyageReportingRequest(string vesselId)
        {
            List<VoyageLandingPageDetails> voyageLandingList = new List<VoyageLandingPageDetails>();
            VoyageReportingRequestViewModel voyageReportingRequest = null;
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
            voyageLandingList = await GetVoyageLandingList(voyageLandingList, decreptedString);

            if (voyageLandingList != null && voyageLandingList.Any())
            {
                VoyageLandingPageDetails voyage = voyageLandingList.FirstOrDefault();
                voyageReportingRequest = GetVoyageReportingRequest(decreptedString, voyage);
            }

            return voyageReportingRequest;
        }

        /// <summary>
        /// Posts the get breaks and bad weather detail.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <param name="IsBadWeatherCalled">if set to <c>true</c> [is bad weather called].</param>
        /// <returns></returns>
        public async Task<BreaksAndBadWeatherDetailViewModel> PostGetBreaksAndBadWeatherDetail(string inputRequest, bool IsBadWeatherCalled)
        {
            BreaksAndBadWeatherDetailViewModel BadWeather = new BreaksAndBadWeatherDetailViewModel();
            BreaksAndBadWeatherDetail response = new BreaksAndBadWeatherDetail();
            VoyageReportingModalRequestViewModel voyageReportingRequestVM = new VoyageReportingModalRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingModalURL").Unprotect(inputRequest);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingModalRequestViewModel>(data);
            var request = new Dictionary<string, object>();
            if (IsBadWeatherCalled)
            {
                request.Add("spaId", voyageReportingRequestVM.SpaId);
                request.Add("isBreakAlert", false);
                request.Add("isWeatherAlert", voyageReportingRequestVM.BadWeatherAlert);
            }
            else
            {
                request.Add("spaId", voyageReportingRequestVM.SpaId);
                request.Add("isBreakAlert", voyageReportingRequestVM.IsBreakInPassage);
                request.Add("isWeatherAlert", false);
            }

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/BreaksAndBadWeatherDetail"));

            response = await PostAsync<BreaksAndBadWeatherDetail>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                if (IsBadWeatherCalled)
                {
                    BadWeather.SpaDate = response.SpaDate.HasValue ? response.SpaDate.Value.ToString("dd MMM yyyy") : " ";
                    BadWeather.BreakAndBadWeatherList = new List<BreaksAndBadWeatherListViewModel>();
                    BreaksAndBadWeatherListViewModel swellLength = new BreaksAndBadWeatherListViewModel
                    {
                        BadWeatherDetailDescription = Constants.SwellLength,
                        CharterValue = response.CharterSeaPassageSwellLengthDescription ?? string.Empty,
                        MaxValue = response.MaxSeaPassageSwellLengthDescription ?? string.Empty
                    };
                    BreaksAndBadWeatherListViewModel WindForce = new BreaksAndBadWeatherListViewModel
                    {
                        BadWeatherDetailDescription = Constants.WindForce,
                        CharterValue = response.CharterSeaPassageWindForce ?? string.Empty,
                        MaxValue = response.MaxSeaPassageWindForce ?? string.Empty
                    };
                    BadWeather.BreakAndBadWeatherList.Add(swellLength);
                    BadWeather.BreakAndBadWeatherList.Add(WindForce);
                }
                else
                {
                    BadWeather.ListOfBreaks = new List<VoyageActivityDelayViewModel>();
                    if (response.ListOfBreaks != null && response.ListOfBreaks.Any())
                    {
                        foreach (var Break in response.ListOfBreaks)
                        {
                            VoyageActivityDelayViewModel Delay = new VoyageActivityDelayViewModel();
                            Delay.ActivityDescription = Break.ActivityDescription;
                            Delay.IsOffHire = Break.IsOffHire ? "Yes" : "No";
                            var localDelayDuration = Break.DateTo - Break.DateFrom;
                            Delay.DelayDuration = localDelayDuration.HasValue ? localDelayDuration.Value.ToString(@"hh\:mm") : "";
                            Delay.DateFrom = Break.DateFrom.HasValue ? Break.DateFrom.Value.ToString(Constants.DateTime24HrFormat) : "";
                            Delay.DateTo = Break.DateTo.HasValue ? Break.DateTo.Value.ToString(Constants.DateTime24HrFormat) : "";
                            Delay.OffHireType = Break.OffHireType ?? string.Empty;
                            Delay.Comments = Break.Comments ?? string.Empty;
                            BadWeather.ListOfBreaks.Add(Delay);
                        }
                    }
                }
            }

            return BadWeather;
        }

        /// <summary>
        /// Posts the get position list by vessel and position identifier.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<PositionListDetailsViewModel> PostGetPosListByVesselAndPosId(string inputRequest)
        {
            PositionListDetailsViewModel posList = new PositionListDetailsViewModel();
            PositionListDetails response = new PositionListDetails();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(inputRequest);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            var request = new Dictionary<string, object>()
            {
                { "activityId", voyageReportingRequestVM.PositionListId },
                { "vesselId", voyageReportingRequestVM.VesselId }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/PosListByVesselandPosId"));

            response = await PostAsync<PositionListDetails>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                posList.PosAgent1 = response.PosAgent1;
                posList.PosAgentStatus1 = response.PosAgent1Stat;
                posList.PosAgent2 = response.PosAgent2;
                posList.PosAgentStatus2 = response.PosAgent2Stat;
                posList.PosAgent3 = response.PosAgent3;
                posList.PosAgentStatus3 = response.PosAgent3Details;
            }

            return posList;

        }

        /// <summary>
        /// Posts the get port call delay.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<List<VoyageActivityDelayViewModel>> PostGetPortCallDelay(string inputRequest)
        {
            List<VoyageActivityDelayViewModel> Delays = new List<VoyageActivityDelayViewModel>();
            List<VoyageActivityDelay> response = new List<VoyageActivityDelay>();
            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();

            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(inputRequest);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            string queryString = "positionListId=" + voyageReportingRequestVM.PositionListId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/PortCallDelay"), queryString);

            response = await PostAsync<List<VoyageActivityDelay>>(requestUrl, CreateHttpContent((voyageReportingRequestVM.PositionListId)));

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    VoyageActivityDelayViewModel delayItem = new VoyageActivityDelayViewModel();
                    delayItem.ActivityDescription = item.ActivityDescription;
                    delayItem.IsOffHire = item.IsOffHire ? "Yes" : "No";
                    var localDelayDuration = item.DateTo - item.DateFrom;
                    delayItem.DelayDuration = localDelayDuration.HasValue ? localDelayDuration.Value.ToString(@"hh\:mm") : "";
                    delayItem.DateFrom = item.DateFrom.HasValue ? item.DateFrom.Value.ToString(Constants.DateTime24HrFormat) : "";
                    delayItem.DateTo = item.DateTo.HasValue ? item.DateTo.Value.ToString(Constants.DateTime24HrFormat) : "";
                    Delays.Add(delayItem);
                }
            }

            return Delays;
        }

        //Port service

        /// <summary>
        /// Posts the get port header details by port identifier.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<PortHeaderDetailsViewModel> PostGetPortHeaderDetailsByPortId(string inputRequest)
        {
            PortHeaderDetailsViewModel portHeader = new PortHeaderDetailsViewModel();
            PortHeaderDetails response = new PortHeaderDetails();
            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();

            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(inputRequest);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            string queryString = "portId=" + voyageReportingRequestVM.PortId;
            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/PortHeaderDetails"), queryString);
            response = await PostAsync<PortHeaderDetails>(requestUrl, CreateHttpContent((voyageReportingRequestVM.PortId)));

            if (response != null)
            {
                string fullName = string.Empty;

                if (!string.IsNullOrWhiteSpace(response.PortName))
                {
                    fullName = response.PortName;

                    if (!string.IsNullOrWhiteSpace(response.CountryCode))
                    {
                        fullName += ", ";
                    }
                }

                if (!string.IsNullOrWhiteSpace(response.CountryCode))
                {
                    fullName += response.CountryCode;
                }
                portHeader.PortFullName = fullName;

                portHeader.CountryName = response.CountryName ?? "";
                portHeader.CountryCode = response.CountryCode ?? "";

                string longitude = string.Empty;
                if (response.LongDegree.HasValue)
                {
                    longitude += response.LongDegree.Value.ToString("0.00").Replace(".00", String.Empty);
                    longitude += "°, ";
                }
                if (response.LongMin.HasValue)
                {
                    longitude += response.LongMin.Value.ToString("0.00");
                    longitude += "' ";
                    longitude += response.LongIndicator;
                }
                portHeader.FullLongitude = longitude;

                string latitude = string.Empty;
                if (response.LatDegree.HasValue)
                {
                    latitude += response.LatDegree.Value.ToString("0.00").Replace(".00", String.Empty);
                    latitude += "°, ";
                }
                if (response.LatMin.HasValue)
                {
                    latitude += response.LatMin.Value.ToString("0.00");
                    latitude += "' ";
                    latitude += response.LatIndicator;
                }
                portHeader.FullLatitude = latitude;

                portHeader.Unlocode = response.UNLocode ?? "";

                portHeader.IsKeyHubPort = response.IsKeyHubPort.GetValueOrDefault() ? "Yes" : "No";
            }

            return portHeader;
        }

        /// <summary>
        /// Posts the get acknowledged alerts.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<List<PortAlertDetailViewModel>> PostGetAcknowledgedAlerts(string inputRequest)
        {
            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(inputRequest);
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);

            AcknowledgeAlertRequest request = new AcknowledgeAlertRequest();
            request.IsVesselView = false;
            request.VesselId = voyageReportingRequestVM.VesselId;

            request.PortId = voyageReportingRequestVM.PortId;
            return await GetAcknowledgedAlertsResponse(request);
        }

        /// <summary>
        /// Gets the acknowledged alerts response.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<List<PortAlertDetailViewModel>> GetAcknowledgedAlertsResponse(AcknowledgeAlertRequest request)
        {
            List<PortAlertDetailViewModel> result = new List<PortAlertDetailViewModel>();
            List<PortAlertDetail> response = null;
            DocumentDetailRequest documentDetailRequest = null;
            if (!string.IsNullOrWhiteSpace(request.PortId) && !string.IsNullOrWhiteSpace(request.VesselId))
            {
                var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/AcknowledgedAlerts"));
                response = await PostAsync<List<PortAlertDetail>>(requestUrl, CreateHttpContent(request));
            }

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    documentDetailRequest = new DocumentDetailRequest();
                    documentDetailRequest.SourceId = x.PatId;
                    documentDetailRequest.SsmId = EnumsHelper.GetKeyValue(SubModule.PortAlert);
                    documentDetailRequest.DctId = EnumsHelper.GetKeyValue(DocumentCategoryType.GeneralDocumentation);

                    result.Add(
                        new PortAlertDetailViewModel()
                        {
                            DocumentRequestUrl = _provider.CreateProtector("DocumentURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(documentDetailRequest)),
                            Title = x.Title ?? "",
                            PrtId = x.PrtId,
                            IsAcknowledged = x.IsAcknowledged,
                            Description = x.Description,
                            AcknowledgeUserName = x.AcknowledgeUserName,
                            AcknowledgeUserRank = x.AcknowledgeUserRank,
                            AcknowledgeDate = x.AcknowledgeDate != null ? x.AcknowledgeDate.Value.ToString(Constants.DateFormat) : "",
                        });
                });
            }

            //result.RemoveAt(1);

            return result;
        }

        /// <summary>
        /// Posts the get port alerts.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="portId">The port identifier.</param>
        /// <returns></returns>
        public async Task<List<PortAlertDetailViewModel>> PostGetPortAlerts(string vesselId, string portId)
        {
            AcknowledgeAlertRequest request = new AcknowledgeAlertRequest();
            request.IsVesselView = false;
            var decryptedVesselId = CommonUtil.GetDecryptedVessel(_provider, vesselId);
            request.VesselId = string.IsNullOrWhiteSpace(decryptedVesselId) ? string.Empty : decryptedVesselId.Split(Constants.Separator)[0];
            request.PortId = portId;
            return await GetAcknowledgedAlertsResponse(request);
        }

        /// <summary>
        /// Gets the fuel efficiency details.
        /// </summary>
        /// <param name="requestVM">The request vm.</param>
        /// <returns></returns>
        public async Task<List<FuelEfficiencyDetailsResponseViewModel>> GetFuelEfficiencyDetails(FuelEfficiencyDetailsRequestViewModel requestVM)
        {
            FuelEfficiencyDetailsRequest request = new FuelEfficiencyDetailsRequest();
            if (requestVM != null)
            {
                request.FleetId = requestVM.FleetId;
                request.MenuType = requestVM.MenuType;

                string requestVesselId = GetVesselId(requestVM.EncryptedVesselId);
                request.VesselIds = !string.IsNullOrWhiteSpace(requestVesselId) ? new List<string>() { requestVesselId } : null;

                request.FromDate = DateTime.Now.Date.AddMonths(Constants.FuelEfficiencyFleetLevelNMonths);
                request.ToDate = DateTime.Now.Date;
            }

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/PWAFuelEfficiencyDetails"));
            List<FuelEfficiencyDetailsResponse> response = await PostAsync<List<FuelEfficiencyDetailsResponse>>(requestUrl, CreateHttpContent(request));

            List<FuelEfficiencyDetailsResponseViewModel> result = new List<FuelEfficiencyDetailsResponseViewModel>();

            VoyageReportingRequestViewModel fuelEfficiencyRequestVM = new VoyageReportingRequestViewModel();
            fuelEfficiencyRequestVM.FromDate = request.FromDate.GetValueOrDefault();
            fuelEfficiencyRequestVM.ToDate = request.ToDate.GetValueOrDefault();
            fuelEfficiencyRequestVM.IsFromFuelEfficiency = true;
            string fuelEfficiencyURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(fuelEfficiencyRequestVM));

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    result.Add(new FuelEfficiencyDetailsResponseViewModel()
                    {
                        EncryptedVesselId = GetEncryptedVessel(x.VesselId, x.VesselName, x.CoyId),
                        EncryptedFuelEfficiencyURL = fuelEfficiencyURL,
                        VesselName = x.VesselName,
                        FuelEfficiencyRatio = x.FuelEfficiencyRatio
                    });
                });
            }

            return result;
        }

        /// <summary>
        /// Posts the get sea passage breaks.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <param name="spaId">The spa identifier.</param>
        /// <returns></returns>
        public async Task<NoonReportDetailsViewModel> PostGetSeaPassageBreaks(string posId, string spaId)
        {
            NoonReportDetailsViewModel result = new NoonReportDetailsViewModel();
            result.SeaPassageBreaks = new List<SeaPassageBreakViewModel>();
            var value = new Dictionary<string, object>()
            {
                { "posId", posId },
                { "spaId", spaId }
            };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/NoonReportDetails"));
            NoonReportDetails response = await PostAsync<NoonReportDetails>(requestUrl, CreateHttpContent(value));
            if (response != null && response.SeaPassageBreaks != null && response.SeaPassageBreaks.Any())
            {
                foreach (var item in response.SeaPassageBreaks)
                {
                    result.SeaPassageBreaks.Add(new SeaPassageBreakViewModel
                    {
                        Comments = item.Comments ?? string.Empty,
                        From = item.From,
                        To = item.To,
                        Reason = item.ActivityTypeName ?? string.Empty,
                        OffHire = item.IsOutOfService == 1,
                        OffHireType = item.OffHireTypeName ?? string.Empty,
                        Type = item.BreakInPassageTypeName ?? string.Empty,
                        IsDelay = item.PlkIdBreakInPassageType == EnumsHelper.GetKeyValue(BreakInPassageType.DelayDeviation),
                        IsMedical = item.ActivityTypeNameId == Constants.PosActivityType_Medical_PLA_ID
                    });
                }
            }
            return result;
        }

        #endregion

        /// <summary>
        /// Posts the get vessel senior officer.
        /// </summary>
        /// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
        /// <returns></returns>
        public async Task<OnboardVesselOfficerDetailsViewModel> PostGetVesselSeniorOfficer(string encryptedVesselDetail)
        {
            List<OnboardSeniorOfficer> response = new List<OnboardSeniorOfficer>();
            OnboardVesselOfficerDetailsViewModel onBoardVesselOfficer = new OnboardVesselOfficerDetailsViewModel();

            string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
            string vesselId = decryptedString.Split(Constants.Separator)[0];

            string urlvessel = "vesselId=" + vesselId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vessel/VesselOfficer/"), urlvessel);

            if (!string.IsNullOrWhiteSpace(vesselId))
            {
                response = await PostAsync<List<OnboardSeniorOfficer>>(requestUrl, CreateHttpContent(vesselId));
            }

            if (response != null && response.Any())
            {

                OnboardSeniorOfficer master = response.FirstOrDefault(obj => obj.Rank == EnumsHelper.GetKeyValue(OnBoardCrewRank.Master));
                OnboardSeniorOfficer chief = response.FirstOrDefault(x => x.Rank == EnumsHelper.GetKeyValue(OnBoardCrewRank.ChiefEngineer));

                if (master != null)
                {
                    onBoardVesselOfficer.VesselMasterName = (master.ForeName ?? "") + " " + (master.SurName ?? "");
                }
                else
                {
                    onBoardVesselOfficer.VesselMasterName = string.Empty;
                }

                if (chief != null)
                {
                    onBoardVesselOfficer.VesselChiefEnggName = (chief.ForeName ?? "") + " " + (chief.SurName ?? "");
                }
                else
                {
                    onBoardVesselOfficer.VesselChiefEnggName = string.Empty;
                }
            }

            return onBoardVesselOfficer;
        }

        #region Hazocc
        /// <summary>
        /// Posts the get hazocc dashboard detail.
        /// </summary>
        /// <param name="hazoccDashboardRequest">The hazocc dashboard request.</param>
        /// <returns></returns>
        public async Task<HazoccDashboardDetailViewModel> PostGetHazoccDashboardDetail(HazoccDashboardRequestViewModel hazoccDashboardRequest)
        {
            HazoccDashboardDetailViewModel hazoccDetails = new HazoccDashboardDetailViewModel();
            HazOccDashboardDetail response = null;
            HazoccDashboardRequest request = new HazoccDashboardRequest();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(hazoccDashboardRequest.VesselId);

            UserMenuItem menuItem = new UserMenuItem();
            menuItem.DisplayText = decreptedString.Split(Constants.Separator)[1];
            menuItem.Identifier = decreptedString.Split(Constants.Separator)[0];
            menuItem.UserMenuItemType = UserMenuItemType.Vessel;
            request.Item = menuItem;

            request.StartDate = hazoccDashboardRequest.StartDate;
            request.EndDate = new DateTime(hazoccDashboardRequest.EndDate.Year, hazoccDashboardRequest.EndDate.Month, hazoccDashboardRequest.EndDate.Day, 23, 59, 59);
            request.VesselId = decreptedString.Split(Constants.Separator)[0];

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/Summary"));
            response = await PostAsync<HazOccDashboardDetail>(requestUrl, CreateHttpContent(request));

            if (response != null && response.HazOccDashboardList != null)
            {
                HazoccDashboardResponse hazoccDashboardResponse = response.HazOccDashboardList.FirstOrDefault();

                HazoccRequestViewModel hazoccRequestViewModel = new HazoccRequestViewModel();
                hazoccRequestViewModel.StartDate = hazoccDashboardRequest.StartDate;
                hazoccRequestViewModel.EndDate = hazoccDashboardRequest.EndDate;
                hazoccRequestViewModel.VesselId = decreptedString.Split(Constants.Separator)[0];
                hazoccRequestViewModel.IsSummaryClicked = true;

                hazoccDetails.AccidentClassifications = response.AccidentClassifications;
                hazoccDetails.OpenAccidentDetails = hazoccDashboardResponse.OpenAccidentDetails;
                hazoccDetails.OpenIncidentDetails = hazoccDashboardResponse.OpenIncidentDetails;

                //Open Items
                hazoccDetails.OpenItems = hazoccDashboardResponse.OpenItemsCount;
                hazoccDetails.OfficeRev = hazoccDashboardResponse.MyReviewsCount;
                hazoccDetails.OpenItemsUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.OpenItems);
                hazoccDetails.OfficeRevUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.OfficeRev);

                //Open Incident
                List<OpenIncidentDetail> openIncident = hazoccDashboardResponse.OpenIncidentDetails;
                hazoccDetails.IncidentVerySerious = GetOpenIncidentCount(openIncident, HazOccReportSeverity.CR);
                hazoccDetails.IncidentSerious = GetOpenIncidentCount(openIncident, HazOccReportSeverity.SR);
                hazoccDetails.IncidentModerate = GetOpenIncidentCount(openIncident, HazOccReportSeverity.MS);
                hazoccDetails.IncidentMinor = GetOpenIncidentCount(openIncident, HazOccReportSeverity.MN);

                hazoccDetails.IncidentVerySeriousUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.IncidentVerySerious);
                hazoccDetails.IncidentSeriousUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.IncidentSerious);
                hazoccDetails.IncidentModerateUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.IncidentModerate);
                hazoccDetails.IncidentMinorUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.IncidentMinor);

                //Crew Accidents
                List<OpenAccidentDetail> crewAccidents = hazoccDashboardResponse.OpenAccidentDetails;
                hazoccDetails.CrewAccidentFatal = GetOpenAccidentCount(crewAccidents, HazOccClassCodes.FT);
                hazoccDetails.CrewAccidentLWC = GetOpenAccidentCount(crewAccidents, HazOccClassCodes.LW);
                hazoccDetails.CrewAccidentRWC = GetOpenAccidentCount(crewAccidents, HazOccClassCodes.RC);
                hazoccDetails.CrewAccidentMTC = GetOpenAccidentCount(crewAccidents, HazOccClassCodes.MT);
                hazoccDetails.CrewAccidentFAC = GetOpenAccidentCount(crewAccidents, HazOccClassCodes.FA);

                hazoccDetails.CrewAccidentFatalUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.CrewAccidentFatal);
                hazoccDetails.CrewAccidentLWCUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.CrewAccidentLWC);
                hazoccDetails.CrewAccidentRWCUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.CrewAccidentRWC);
                hazoccDetails.CrewAccidentMTCUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.CrewAccidentMTC);
                hazoccDetails.CrewAccidentFACUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.CrewAccidentFAC);

                if (crewAccidents.FirstOrDefault(x => x.ClassificationShortCode.Trim().ToLower() == EnumsHelper.GetKeyValue(HazoccType.LTI).Trim().ToLower()) != null)
                {
                    hazoccDetails.StatisticsLTI = crewAccidents.FirstOrDefault(x => x.ClassificationShortCode.Trim().ToLower() == EnumsHelper.GetKeyValue(HazoccType.LTI).Trim().ToLower()).TotalCount;
                }
                if (crewAccidents.FirstOrDefault(x => x.ClassificationShortCode.Trim().ToLower() == EnumsHelper.GetKeyValue(HazoccType.TRC).Trim().ToLower()) != null)
                {
                    hazoccDetails.StatisticsTRC = crewAccidents.FirstOrDefault(x => x.ClassificationShortCode.Trim().ToLower() == EnumsHelper.GetKeyValue(HazoccType.TRC).Trim().ToLower()).TotalCount;
                }

                hazoccDetails.StatisticsMEXPHS = hazoccDashboardResponse.MexpHrs;

                hazoccDetails.StatisticsLTIUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.StatisticsLTI);
                hazoccDetails.StatisticsTRCUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.StatisticsTRC);

                //Passenger Accidents
                List<PassengerAccidentDetail> passengerAccidents = hazoccDashboardResponse.PassengerAccidentList;
                hazoccDetails.PassengerFatal = GetPassengerAccidentCount(passengerAccidents, HazOccClassCodes.FT);
                hazoccDetails.PassengerFAC = GetPassengerAccidentCount(passengerAccidents, HazOccClassCodes.FA);
                hazoccDetails.PassengerMTC = GetPassengerAccidentCount(passengerAccidents, HazOccClassCodes.MT);

                hazoccDetails.PassengerFatalUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.PassengerFatal);
                hazoccDetails.PassengerFACUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.PassengerFAC);
                hazoccDetails.PassengerMTCUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.PassengerMTC);

                //Near miss and Seafty observation
                hazoccDetails.NearMissSafeActs = hazoccDashboardResponse.SafeActConditionCount;
                hazoccDetails.NearMissCount = hazoccDashboardResponse.NearMissCount;
                hazoccDetails.NearMissUnsafeActs = hazoccDashboardResponse.UnsafeActCount;
                hazoccDetails.NearMissUnsafeCond = hazoccDashboardResponse.UnSafeCondtionCount;

                hazoccDetails.NearMissSafeActsUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.NearMissSafeActs);
                hazoccDetails.NearMissCountUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.NearMissCount);
                hazoccDetails.NearMissUnsafeActsUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.NearMissUnsafeActs);
                hazoccDetails.NearMissUnsafeCondUrl = SetHazoccUrl(hazoccRequestViewModel, HazoccDashboardType.NearMissUnsafeCond);

                if (response.HeaderDetail != null)
                {
                    HazOccDashboardHeaderDetail HeaderDetail = response.HeaderDetail;
                    hazoccDetails.ThirdPartyAccidents = HeaderDetail.TotalThirdPartyAccident;
                    hazoccDetails.TotalAccidents = HeaderDetail.TotalAccidents;
                    hazoccDetails.TotalIncidents = HeaderDetail.TotalIncidents;
                    hazoccDetails.TotalPassengerAccident = HeaderDetail.TotalPassengerAccident;
                    hazoccDetails.TotalNearMissObservations = HeaderDetail.SafeActConditionCount + HeaderDetail.UnsafeActCount + HeaderDetail.UnsafeConditionCount + HeaderDetail.NearMissCount;

                    hazoccDetails.TrcCount = HeaderDetail.TrcCount;
                    hazoccDetails.LtiCount = HeaderDetail.LtiCount;
                    hazoccDetails.MexpHrs = HeaderDetail.MexpHrs;
                    hazoccDetails.TotalCount = HeaderDetail.TotalAccidents + HeaderDetail.TotalIncidents + HeaderDetail.TotalPassengerAccident + hazoccDetails.TotalNearMissObservations;

                    hazoccDetails.TotalFatalities = HeaderDetail.FatalCount + HeaderDetail.PassengerFatalCount;
                    hazoccDetails.TotalVerySerious = HeaderDetail.VerySeriousIncidentCount;
                }

            }

            return hazoccDetails;
        }

        /// <summary>
        /// Sets the hazocc URL.
        /// </summary>
        /// <param name="hazoccRequestViewModel">The hazocc request view model.</param>
        /// <param name="hazoccDashboardType">Type of the hazocc dashboard.</param>
        /// <returns></returns>
        private string SetHazoccUrl(HazoccRequestViewModel hazoccRequestViewModel, HazoccDashboardType hazoccDashboardType)
        {
            hazoccRequestViewModel.HazoccDashboardType = hazoccDashboardType;
            string hazoccUrl = _provider.CreateProtector("Hazocc").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(hazoccRequestViewModel));
            return hazoccUrl;
        }

        /// <summary>
        /// Gets the open incident count.
        /// </summary>
        /// <param name="openIncident">The open incident.</param>
        /// <param name="hazOccReportSeverity">The haz occ report severity.</param>
        /// <returns></returns>
        private int GetOpenIncidentCount(List<OpenIncidentDetail> openIncident, HazOccReportSeverity hazOccReportSeverity)
        {
            if (openIncident != null && openIncident.Any() && openIncident.FirstOrDefault(x => x.SeverityId == EnumsHelper.GetKeyValue(hazOccReportSeverity)) != null)
            {
                return openIncident.FirstOrDefault(x => x.SeverityId == EnumsHelper.GetKeyValue(hazOccReportSeverity)).TotalCount;
            }
            return 0;
        }

        /// <summary>
        /// Gets the open accident count.
        /// </summary>
        /// <param name="openAccident">The open accident.</param>
        /// <param name="hazOccClassCodes">The haz occ class codes.</param>
        /// <returns></returns>
        private int GetOpenAccidentCount(List<OpenAccidentDetail> openAccident, HazOccClassCodes hazOccClassCodes)
        {
            if (openAccident != null && openAccident.Any() && openAccident.FirstOrDefault(x => x.ImcId == EnumsHelper.GetKeyValue(hazOccClassCodes)) != null)
            {
                return openAccident.FirstOrDefault(x => x.ImcId == EnumsHelper.GetKeyValue(hazOccClassCodes)).TotalCount;
            }
            return 0;
        }

        /// <summary>
        /// Gets the passenger accident count.
        /// </summary>
        /// <param name="passengerAccident">The passenger accident.</param>
        /// <param name="hazOccClassCodes">The haz occ class codes.</param>
        /// <returns></returns>
        private int GetPassengerAccidentCount(List<PassengerAccidentDetail> passengerAccident, HazOccClassCodes hazOccClassCodes)
        {
            if (passengerAccident != null && passengerAccident.Any() && passengerAccident.FirstOrDefault(x => x.ImcId == EnumsHelper.GetKeyValue(hazOccClassCodes)) != null)
            {
                return passengerAccident.FirstOrDefault(x => x.ImcId == EnumsHelper.GetKeyValue(hazOccClassCodes)).TotalCount;
            }
            return 0;
        }

        /// <summary>
        /// Posts the get haz occ summary detail.
        /// </summary>
        /// <param name="hazoccDashboardRequest">The hazocc dashboard request.</param>
        /// <returns></returns>
        public async Task<HazOccSummaryResponseViewModel> PostGetHazOccSummaryDetail(HazOccSummaryRequestViewModel hazoccDashboardRequest)
        {
            HazOccSummaryResponseViewModel result = new HazOccSummaryResponseViewModel();
            HazOccSummaryRequest request = new HazOccSummaryRequest();
            HazOccSummaryResponse response = new HazOccSummaryResponse();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(hazoccDashboardRequest.VesselId);
            request.VesselId = decreptedString.Split(Constants.Separator)[0];
            DateTime today = DateTime.Now.Date;

            request.AccidentStartDate = today.AddMonths(Constants.HazOccAccidentNMonths);
            request.AccidentEndDate = today;
            request.IncidentStartDate = today.AddMonths(Constants.HazOccIncidentNMonths);
            request.IncidentEndDate = today;
            request.AccidentPriorityLimit = Constants.HazOccAccidentPriority;
            request.IncidentPriorityLimit = Constants.HazOccIncidentPriority;
            request.UAUCPriorityGroupLimit = Constants.HazOccUnsafeConditionPriority;
            request.StartDate = hazoccDashboardRequest.StartDate;
            request.EndDate = hazoccDashboardRequest.EndDate;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/PWASummary"));
            response = await PostAsync<HazOccSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                result.UnsafeRate = response.UnsafeRate.HasValue && response.UnsafeRate != null ? response.UnsafeRate.Value.ToString() : "-";
                result.SeriousAccidentCount = response.SeriousAccidentCount.GetValueOrDefault();
                result.SeriousIncidentCount = response.SeriousIncidentCount.GetValueOrDefault();
                result.LTIFreeDaysCount = response.LTIFreeDaysCount.GetValueOrDefault();

                result.UnsafePriority = response.UnsafeRate.HasValue && response.UnsafeRate != null ? response.UnsafePriority : 0;
                result.SeriousAccidentPriority = response.SeriousAccidentPriority;
                result.SeriousIncidentPriority = response.SeriousIncidentPriority;
                result.LTIFreeDaysPriority = response.LTIFreeDaysPriority;

                //list summary counts
                result.CrewAccidentsCount = response.CrewAccidents.GetValueOrDefault();
                result.PassengerAccidentsCount = response.PassengerAccidents.GetValueOrDefault();
                result.ThirdPartyAccidentsCount = response.ThirdPartyAccidents.GetValueOrDefault();
                result.NearMissObservationCount = response.NearMissObservation.GetValueOrDefault();
                result.IncidentCount = response.Incident.GetValueOrDefault();
                result.FatalityCount = response.Fatality.GetValueOrDefault();
                result.VerySeriousCount = response.VerySerious.GetValueOrDefault();
                result.LTICount = response.LTI.GetValueOrDefault();
                result.TRCCount = response.TRC.GetValueOrDefault();
                result.MExpHrsCrw = response.MExpHrsCrw.GetValueOrDefault();
                result.MExpHrsPax = response.MExpHrsPax.GetValueOrDefault();
                result.IllnessCount = response.Illness.GetValueOrDefault();
                result.TotalCount = result.CrewAccidentsCount + result.PassengerAccidentsCount + result.ThirdPartyAccidentsCount + result.NearMissObservationCount + result.IncidentCount + result.IllnessCount;

            }
            HazOccListViewModel viewModel = new HazOccListViewModel
            {
                EncryptedVesselId = hazoccDashboardRequest.VesselId,
                StartDate = DateTime.Now.Date.AddMonths(-12).AddDays(1),
                EndDate = today.AddDays(1).AddSeconds(-1),
                GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.Total),
                StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.Total),
                StageDescription = EnumsHelper.GetKeyValue(HazOccListStageFilter.Total),
                ActiveMobileTabClass = Constants.Tab1
            };

            result.HazOccListRequestUrl = _provider.CreateProtector("HazOccList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(viewModel));

            HazOccListViewModel seriousAccident = new HazOccListViewModel();
            seriousAccident.EncryptedVesselId = hazoccDashboardRequest.VesselId;
            seriousAccident.StartDate = today.AddMonths(Constants.HazOccAccidentNMonths);
            seriousAccident.EndDate = today.AddDays(1).AddSeconds(-1);
            seriousAccident.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.SeriousAccidents);
            seriousAccident.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.SeriousAccidents);
            seriousAccident.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.SeriousAccidents);
            seriousAccident.ActiveMobileTabClass = Constants.Tab2;
            result.SeriousAccidentURL = GetHazOccRequestURL(seriousAccident);

            HazOccListViewModel seriousIncident = new HazOccListViewModel();
            seriousIncident.EncryptedVesselId = hazoccDashboardRequest.VesselId;
            seriousIncident.StartDate = today.AddMonths(Constants.HazOccIncidentNMonths);
            seriousIncident.EndDate = today.AddDays(1).AddSeconds(-1);
            seriousIncident.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.SeriousIncidents);
            seriousIncident.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.SeriousIncidents);
            seriousIncident.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.SeriousIncidents);
            seriousIncident.ActiveMobileTabClass = Constants.Tab2;
            result.SeriousIncidentURL = GetHazOccRequestURL(seriousIncident);

            HazOccListViewModel uaucRate = new HazOccListViewModel();
            uaucRate.EncryptedVesselId = hazoccDashboardRequest.VesselId;
            uaucRate.StartDate = today.AddMonths(Constants.UAUCRateNMonths);
            uaucRate.EndDate = today.AddDays(1).AddSeconds(-1);
            uaucRate.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.UAUCRate);
            uaucRate.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.UAUCRate);
            uaucRate.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.UAUCRate);
            uaucRate.ActiveMobileTabClass = Constants.Tab2;
            result.UnsafeActAndUnsafeConditionURL = GetHazOccRequestURL(uaucRate);

            HazOccListViewModel hazocRequestVm = new HazOccListViewModel();
            hazocRequestVm.EncryptedVesselId = hazoccDashboardRequest.VesselId;
            hazocRequestVm.StartDate = request.StartDate.GetValueOrDefault();
            hazocRequestVm.EndDate = request.EndDate.GetValueOrDefault();

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.CrewAccidents);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.CrewAccidents);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.CrewAccidents);
            hazocRequestVm.ActiveMobileTabClass = Constants.Tab2;
            result.TotalCrewAccidentsUrl = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.Fatality);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.Fatality);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.Fatality);
            result.TotalFatalitiesURL = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.Incidents);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.Incidents);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.Incidents);
            result.TotalIncidentsUrl = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.NearMissSafetyObserve);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.NearMissSafetyObserve);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.NearMissSafetyObserve);
            result.TotalNearMissObservationsUrl = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.PassengerAccidents);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.PassengerAccidents);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.PassengerAccidents);
            result.TotalPassengerAccidentUrl = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.Total);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.Total);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.Total);
            result.TotalCountUrl = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.VerySerious);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.VerySerious);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.VerySerious);
            result.TotalVerySeriousURL = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.LTI);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.LTI);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.LTI);
            result.LtiUrl = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.TRC);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.TRC);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.TRC);
            result.TrcUrl = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.ThirdPartyAccident);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.ThirdPartyAccident);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.ThirdPartyAccident);
            result.ThirdPartyAccidentsUrl = GetHazOccRequestURL(hazocRequestVm);

            hazocRequestVm.GridSubTitle = EnumsHelper.GetDescription(HazOccListStageFilter.Illness);
            hazocRequestVm.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.Illness);
            hazocRequestVm.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.Illness);
            result.IllnessUrl = GetHazOccRequestURL(hazocRequestVm);

            return result;
        }

        /// <summary>
        /// Gets the haz occ request URL.
        /// </summary>
        /// <param name="hazoccViewModel">The hazocc view model.</param>
        /// <returns></returns>
        private string GetHazOccRequestURL(HazOccListViewModel hazoccViewModel)
        {
            string hazOccURL = _provider.CreateProtector("HazOccList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(hazoccViewModel));
            return hazOccURL;
        }

        /// <summary>
        /// The Get hazOcc Linked Insurance Claims.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<LinkedInsuranceClaimResponseViewModel>> GetHazOccLinkedInsuranceClaims(LinkedInsuranceClaimRequest request)
        {

            List<LinkedInsuranceClaimResponseViewModel> claimsList = new List<LinkedInsuranceClaimResponseViewModel>();
            LinkedInsuranceClaimRequest inputRequest = new LinkedInsuranceClaimRequest();

            inputRequest.HazOccId = request.HazOccId;
            inputRequest.VesselId = request.VesselId;


            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/LinkedInsuranceClaims"));
            List<LinkedInsuranceClaimResponse> response = new List<LinkedInsuranceClaimResponse>();
            response = await PostAsync<List<LinkedInsuranceClaimResponse>>(requestUrl, CreateHttpContent(inputRequest));

            if (response != null && response.Any())
            {
                foreach (LinkedInsuranceClaimResponse item in response)
                {
                    LinkedInsuranceClaimResponseViewModel claimVM = new LinkedInsuranceClaimResponseViewModel();
                    claimVM.ClaimId = item.ClaimId ?? "";
                    claimVM.ClaimNumber = item.ClaimNumber ?? "";
                    claimVM.Name = item.Name ?? "";
                    claimVM.ClaimType = item.ClaimType ?? "-";
                    claimVM.OpenDate = (item.OpenDate.HasValue) ? item.OpenDate.Value.ToString(Constants.DateFormat) : "-";
                    claimVM.ClosedDate = (item.ClosedDate.HasValue) ? item.ClosedDate.Value.ToString(Constants.DateFormat) : "-";
                    claimVM.ReportDate = (item.ClaimDate.HasValue) ? item.ClaimDate.Value.ToString(Constants.DateFormat) : "-";
                    claimVM.SystemArea = item.SystemArea ?? "-";
                    claimVM.Cost = item.Cost.HasValue ? item.Cost.ToString() : "-";
                    claimsList.Add(claimVM);
                }
            }
            return claimsList;
        }

        #endregion

        #region Defect Methods

        #region Defect List Methods

        //TODO: Need to remove this method
        //To get the count of stages in defect module
        /// <summary>
        /// Posts the get defect dashboard detail.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<DefectDashboardResponseViewModel> PostGetDefectDashboardDetail(DefectListViewModel inputRequest)
        {
            DefectDashboardResponseViewModel response = new DefectDashboardResponseViewModel();
            DefectDashboardRequest request = new DefectDashboardRequest();
            List<DefectDashboardResponse> defectDashboardResponses = null;

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(inputRequest.EncryptedVesselId);

            UserMenuItem menuItem = new UserMenuItem();
            menuItem.DisplayText = decreptedString.Split(Constants.Separator)[1];
            menuItem.Identifier = decreptedString.Split(Constants.Separator)[0];
            menuItem.UserMenuItemType = UserMenuItemType.Vessel;
            request.Item = menuItem;
            //TODO: Need to remove this method and call new dashboard
            request.StartDate = DateTime.Now;// inputRequest.FromDate;
            request.EndDate = DateTime.Now; // inputRequest.ToDate;
            request.CreatedInLastNMonths = Constants.CreatedInLastNMonths;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/Summary"));
            defectDashboardResponses = await PostAsync<List<DefectDashboardResponse>>(requestUrl, CreateHttpContent(request));
            if (defectDashboardResponses != null && defectDashboardResponses.Any())
            {
                var defectResponse = defectDashboardResponses.FirstOrDefault();
                response.AllDefectCount = defectResponse.AllDefectCount.GetValueOrDefault();
                response.DueCount = defectResponse.DueCount.GetValueOrDefault();
                response.OverdueCount = defectResponse.OverdueCount.GetValueOrDefault();
                response.OpenDefectCount = defectResponse.OpenDefectCount.GetValueOrDefault();
                response.ClosedDefectCount = defectResponse.ClosedDefectCount.GetValueOrDefault();
                response.OffHireRequiredCount = defectResponse.OffHireRequiredCount.GetValueOrDefault();
                response.OrderCount = defectResponse.OrderCount.GetValueOrDefault();

                response.AllNavigation = GetDefectManagerURL(inputRequest, DefectManagerStages.OpenDefect);
                response.DueNavigation = GetDefectManagerURL(inputRequest, DefectManagerStages.OpenDefect);
                response.OverdueNavigation = GetDefectManagerURL(inputRequest, DefectManagerStages.Overdue);
                response.OpenDefectNavigation = GetDefectManagerURL(inputRequest, DefectManagerStages.OpenDefect);
                response.ClosedDefectNavigation = GetDefectManagerURL(inputRequest, DefectManagerStages.ClosedDefect);
                response.OffHireNavigation = GetDefectManagerURL(inputRequest, DefectManagerStages.OffHire);
                response.OrderNavigation = GetDefectManagerURL(inputRequest, DefectManagerStages.OpenDefect);
            }
            response.Month = DateTime.Now.Date.ToString("MMM");
            return response;
        }


        /// <summary>
        /// Posts the get defect work basket.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<DefectWorkBasketResponseViewModel>>> PostGetDefectWorkBasket(DataTablePageRequest<string> pageRequest, DefectListViewModel filters)
        {
            //Gets the data for Due, OverDue, offhire required
            //Gets the daa for all when status is all selected ie., passed as null
            DataTablePageResponse<List<DefectWorkBasketResponseViewModel>> result = new DataTablePageResponse<List<DefectWorkBasketResponseViewModel>>();
            result.Data = new List<DefectWorkBasketResponseViewModel>();
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);

            //setting reuest object based on Shipsure
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(filters.EncryptedVesselId);
            DefectWorkBasketRequest request = new DefectWorkBasketRequest();

            if (filters.StageName == EnumsHelper.GetKeyValue(DefectStages.OverDue))
            {
                request.VesselId = decreptedString.Split(Constants.Separator)[0];
                request.FromDate = filters.FromDate;
                request.ToDate = filters.ToDate;
                request.IsDue = false;
                request.IsOverdue = true;
                request.IsCritical = false;
                request.AddedInDamageForm = false;
                request.ComponentId = null;
                request.SystemAreaId = null;
                request.Priority = null;
                List<string> _status = new List<string>();
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed));
                request.Status = _status;
                request.Type = null;
                request.Category = null;
                request.GuaranteeClaimRequired = false;
                request.IsOffHire = false;
                request.DefectSystemAreaId = null;
                request.TopSystemAreaId = null;
            }
            else if (filters.StageName == EnumsHelper.GetKeyValue(DefectStages.Due))
            {
                request.VesselId = decreptedString.Split(Constants.Separator)[0];
                request.FromDate = filters.FromDate;
                request.ToDate = filters.ToDate;
                request.IsDue = true;
                request.IsOverdue = false;
                request.IsCritical = false;
                request.AddedInDamageForm = false;
                request.ComponentId = null;
                request.SystemAreaId = null;
                request.Priority = null;
                request.Type = null;
                request.Category = null;
                request.GuaranteeClaimRequired = false;
                request.IsOffHire = false;
                request.DefectSystemAreaId = null;
                request.TopSystemAreaId = null;
                List<string> _status = new List<string>();
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted));
                request.Status = _status;
            }
            else if (filters.StageName == EnumsHelper.GetKeyValue(DefectStages.OffHireRequired))
            {
                request.VesselId = decreptedString.Split(Constants.Separator)[0];
                request.FromDate = filters.FromDate;
                request.ToDate = filters.ToDate;
                request.IsDue = true;
                request.IsOverdue = true;
                request.IsOffHire = true;
                request.IsCritical = false;
                request.AddedInDamageForm = false;
                request.ComponentId = null;
                request.SystemAreaId = null;
                request.Priority = null;
                request.Type = null;
                request.Category = null;
                request.TopSystemAreaId = null;
                request.GuaranteeClaimRequired = false;
                request.DefectSystemAreaId = null;
                List<string> _status = new List<string>();
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed));
                request.Status = _status;
            }
            else if (filters.StageName == EnumsHelper.GetKeyValue(DefectStages.All))
            {
                request.VesselId = decreptedString.Split(Constants.Separator)[0];
                request.FromDate = null;
                request.ToDate = null;
                request.IsDue = false;
                request.IsOverdue = false;
                request.IsOffHire = false;
                request.IsCritical = false;
                request.AddedInDamageForm = false;
                request.ComponentId = null;
                request.SystemAreaId = null;
                request.Priority = null;
                request.Type = null;
                request.Category = null;
                request.TopSystemAreaId = null;
                request.GuaranteeClaimRequired = false;
                request.DefectSystemAreaId = null;

                request.Status = null;
            }
            var value = new Dictionary<string, object>()
            {
                { "pageRequest", pagedRequest },
                { "request", request }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/DefectWorkBasket"));
            PagedResponse<List<DefectWorkBasketResponse>> response = null;
            response = await PostAsync<PagedResponse<List<DefectWorkBasketResponse>>>(requestUrl, CreateHttpContent(value));

            if (response.Result != null)
            {
                foreach (DefectWorkBasketResponse item in response.Result)
                {
                    //naviagtion to details
                    DefectWorkBasketResponseViewModel workBasket = new DefectWorkBasketResponseViewModel();
                    workBasket = ConvertDefectViewModel(item);
                    result.Data.Add(workBasket);
                }
            }

            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;

            return result;
        }

        /// <summary>
        /// Posts the get defect included in damage form.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<List<DefectWorkBasketResponseViewModel>> PostGetDefectIncludedInDamageForm(DefectListViewModel inputRequest)
        {
            //will fetch data for open tech defect & close tech defect

            List<DefectWorkBasketResponseViewModel> result = new List<DefectWorkBasketResponseViewModel>();
            List<DefectWorkBasketResponse> response = new List<DefectWorkBasketResponse>();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(inputRequest.EncryptedVesselId);
            string VesselId = decreptedString.Split(Constants.Separator)[0];
            List<string> _status = new List<string>();
            if (inputRequest.StageName == EnumsHelper.GetKeyValue(DefectStages.OpenTechDefect))
            {
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule));
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted));
            }
            else if (inputRequest.StageName == EnumsHelper.GetKeyValue(DefectStages.ClosedTechDefect))
            {
                _status.Add(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Close));
            }

            var request = new Dictionary<string, object>()
            {
                { "vesselId", VesselId },
                { "inLastDays", Constants.InLastDays},
                { "defectWorkOrderStatuses",_status}
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/DefectIncludedInDamageForm"));
            response = await PostAsync<List<DefectWorkBasketResponse>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    //naviagtion to details

                    DefectWorkBasketResponseViewModel workBasket = new DefectWorkBasketResponseViewModel();
                    workBasket = ConvertDefectViewModel(item);
                    result.Add(workBasket);
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the get defect with outstanding order.
        /// </summary>
        /// <param name="EncryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<DefectWorkBasketResponseViewModel>> PostGetDefectWithOutstandingOrder(string EncryptedVesselId)
        {
            List<DefectWorkBasketResponseViewModel> result = new List<DefectWorkBasketResponseViewModel>();
            List<DefectWorkBasketResponse> response = new List<DefectWorkBasketResponse>();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(EncryptedVesselId);
            string VesselId = decreptedString.Split(Constants.Separator)[0];

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/DefectWithOutstandingOrder"));
            response = await PostAsync<List<DefectWorkBasketResponse>>(requestUrl, CreateHttpContent(VesselId));

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    //naviagtion to details
                    DefectWorkBasketResponseViewModel workBasket = new DefectWorkBasketResponseViewModel();
                    workBasket = ConvertDefectViewModel(item);
                    result.Add(workBasket);
                }
            }
            return result;
        }

        /// <summary>
        /// Converts the defect view model.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private DefectWorkBasketResponseViewModel ConvertDefectViewModel(DefectWorkBasketResponse input)
        {
            DefectWorkBasketResponseViewModel workBasket = new DefectWorkBasketResponseViewModel();
            if (input != null)
            {
                workBasket.DefectWorkOrderId = input.DwoId;
                workBasket.DocumentCount = input.DocumentCount.GetValueOrDefault();
                workBasket.DefectNo = input.DefectNumber ?? "";
                workBasket.Title = input.DefectName ?? "";

                workBasket.GuaranteeClaimNumber = input.GuaranteeClaimRequired == true ? input.GuaranteeClaimCode : null;
                workBasket.GuaranteeClaimCode = !string.IsNullOrWhiteSpace(workBasket.GuaranteeClaimNumber);

                workBasket.OffHire = input.OffHireDesc ?? "";

                workBasket.DefectDamageFormNumber = input.IncludeInDamageForm == true ? input.DamageFormNumber : null;
                workBasket.TechDefect = !string.IsNullOrWhiteSpace(workBasket.DefectDamageFormNumber);

                workBasket.Category = input.Category ?? "";
                workBasket.Status = input.Status ?? "";
                workBasket.StatusDescription = input.StatusDescription ?? "";
                workBasket.SystemArea = input.SystemArea ?? "";
                workBasket.SubSystemArea = input.SubSystemArea;
                workBasket.EstimatedCompleteDate = input.EstimatedCompleteDate;
                workBasket.IsCurrentDueDateVisible = input.RescheduleCount == null ? true : false;

                workBasket.VesselName = input.VesselName;

                if (input.OriginalDueDate != null && input.EstimatedCompleteDate != null && input.OriginalDueDate == input.EstimatedCompleteDate)
                {
                    input.OriginalDueDate = null;
                    workBasket.IsOverDueVisible = input.EstimatedCompleteDate < DateTime.Now.Date && !string.IsNullOrWhiteSpace(input.StatusId)
                        && (input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.DefectWorkOrder))
                                                || input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.Reschedule))
                                                || input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.ClaimAccepted))
                                                || input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.Completed)));
                }
                else
                {
                    if (input.OriginalDueDate != null)
                    {
                        workBasket.AdvODDays = Convert.ToInt32((input.EstimatedCompleteDate - input.OriginalDueDate).Value.TotalDays);
                    }
                    workBasket.IsOverDueVisible = input.EstimatedCompleteDate < DateTime.Now.Date && !string.IsNullOrWhiteSpace(input.StatusId)
                        && (input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.DefectWorkOrder))
                                                || input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.Reschedule))
                                                || input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.ClaimAccepted))
                                                || input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.Completed)));
                }
                workBasket.DueDateBeforeResc = input.OriginalDueDate;
                workBasket.ActualCompleteDate = input.ActualCompleteDate;
                workBasket.Impact = input.Impact ?? "";
                workBasket.SiteTypeDescription = input.SiteTypeDescription ?? "";
                workBasket.Priority = input.Priority ?? "";
                workBasket.RequisitionCount = input.RequisitionCount.GetValueOrDefault(); //need to chheck whar should be default value here
                workBasket.AdditionalJobs = 0;
                workBasket.ProjectCode = input.ProjectCode ?? "";
                workBasket.AccountCode = input.AccountCode ?? "";
                workBasket.YardGuaranteeClaimNumber = input.YardGuaranteeClaimNumber ?? "";
                workBasket.IsRescheduleCount = input.RescheduleCount.GetValueOrDefault() > 0;
                workBasket.IsCritical = input.IsCritical.GetValueOrDefault();
                workBasket.IsJSARequired = input.IsJSARequired;
                workBasket.IsMOCRequired = input.IsMOCRequired;
                workBasket.IsRobLessThanReq = input.IsRobLessThanReq;

                DefectDetailsViewModel defectDetails = new DefectDetailsViewModel();
                defectDetails.DefectWorkOrderId = input.DwoId;
                defectDetails.DueDate = input.EstimatedCompleteDate.HasValue ? input.EstimatedCompleteDate.Value.ToString(Constants.DateFormat) : "";
                if (!string.IsNullOrWhiteSpace(input.StatusId) && (input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.Close)) || input.StatusId.Equals(EnumsHelper.GetKeyValue<DefectWorkOrderStatus>(DefectWorkOrderStatus.Completed))))
                {
                    defectDetails.IsCompletedOrClosed = true;
                }
                if (!string.IsNullOrWhiteSpace(workBasket.GuaranteeClaimNumber))
                {
                    defectDetails.IsGuaranteeClaimCode = true;
                }
                workBasket.DefectDetailURL = _provider.CreateProtector("DefectDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(defectDetails));

                workBasket.EncryptedVesselId = CommonUtil.GetEncryptedVessel(_provider, input.VesselId, input.VesselName, input.CoyId);
            }
            return workBasket;
        }


        /// <summary>
        /// Gets the defect manager URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="selectedStage">The selected stage.</param>
        /// <returns></returns>
        private string GetDefectManagerURL(DefectListViewModel request, DefectManagerStages selectedStage)
        {
            request.FromDate = null;
            request.ToDate = null;
            request.StageName = EnumsHelper.GetKeyValue(selectedStage);
            string defectURL = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(request));
            return defectURL;
        }

        #endregion

        #region New Defect List Methods

        /// <summary>
        /// Gets the defect critical list.
        /// </summary>
        /// <returns></returns>
        public List<DefectCriticalStatus> GetDefectCriticalList()
        {
            List<DefectCriticalStatus> CriticaclList = new List<DefectCriticalStatus>();
            foreach (DefectCriticalStatus val in Enum.GetValues(typeof(DefectCriticalStatus)))
            {
                CriticaclList.Add(val);
            }

            return CriticaclList;
        }

        /// <summary>
        /// Gets the defect due status.
        /// </summary>
        /// <returns></returns>
        public List<DefectDueStatus> GetDefectDueStatus()
        {
            List<DefectDueStatus> DueStatusList = new List<DefectDueStatus>();
            foreach (DefectDueStatus val in Enum.GetValues(typeof(DefectDueStatus)))
            {
                DueStatusList.Add(val);
            }

            return DueStatusList;
        }

        /// <summary>
        /// Gets the defect list.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public async Task<List<DefectWorkBasketResponseViewModel>> GetDefectList(DefectListViewModel filters)
        {
            List<DefectWorkBasketResponseViewModel> result = new List<DefectWorkBasketResponseViewModel>();

            DefectWorkBasketRequest request = new DefectWorkBasketRequest();
            if (!string.IsNullOrWhiteSpace(filters.EncryptedVesselId))
            {
                string decreptedString = CommonUtil.GetDecryptedVessel(_provider, filters.EncryptedVesselId);
                request.VesselId = decreptedString.Split(Constants.Separator)[0];
            }
            request.FleetId = filters.FleetId;
            request.MenuType = filters.MenuType;

            if (filters.IsSearchClicked)
            {
                request.FromDate = filters.FromDate;
                request.ToDate = filters.ToDate;
                if (filters.SelectedSystemArea != null && filters.SelectedSystemArea.Any())
                {
                    request.DefectSystemAreaId = filters.SelectedSystemArea;
                }

                if (filters.SelectedStatus != null && filters.SelectedStatus.Any())
                {
                    request.Status = filters.SelectedStatus;
                }
                else
                {
                    request.Status = new List<string>();
                }
                if (filters.SelectedPlannedFor != null && filters.SelectedPlannedFor.Any())
                {
                    request.Type = filters.SelectedPlannedFor;
                }
                else
                {
                    request.Type = new List<string>();
                }
                if (!string.IsNullOrWhiteSpace(filters.DefectTitle))
                {
                    request.Title = filters.DefectTitle;
                }
                if (filters.SelectedCriticalStatus == EnumsHelper.GetKeyValue(DefectCriticalStatus.OnlyCritical))
                {
                    request.IsCritical = true;
                }
                else if (filters.SelectedCriticalStatus == EnumsHelper.GetKeyValue(DefectCriticalStatus.All))
                {
                    request.IsCritical = false;
                }

                if (filters.SelectedDueStatus == EnumsHelper.GetKeyValue(DefectDueStatus.All))
                {
                    request.IsDue = true;
                    request.IsOverdue = true;
                }
                else if (filters.SelectedDueStatus == EnumsHelper.GetKeyValue(DefectDueStatus.Due))
                {
                    request.IsDue = true;
                }
                else if (filters.SelectedDueStatus == EnumsHelper.GetKeyValue(DefectDueStatus.Overdue))
                {
                    request.IsOverdue = true;
                }
            }
            else
            {
                request.FromDate = null;
                request.ToDate = null;
                request.IsDue = true;
                request.IsOverdue = true;
                if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.ClosedDefect))
                {
                    request.Status = new List<string>() { "GLAS00000031" };
                    request.ToDate = DateTime.Now;
                    request.FromDate = request.ToDate.Value.Date.AddMonths(-12);
                }
                else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.OpenDefect))
                {
                    //GLAS00000046 GLAS00000029 GLAS00000030 GLAS00000033 
                    //Claim Accepted, Defect Work Order, Completed, Reschedule
                    request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
                }
                else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Overdue))
                {
                    request.IsDue = false;
                    request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
                }
                else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.OffHire))
                {
                    request.IsOffHire = true;
                    request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
                }
                else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Layover))
                {
                    request.Type = new List<string>() { "GLAS00000009" };
                    request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
                }
                else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Drydock))
                {
                    request.Type = new List<string>() { "GLAS00000010" };
                    request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
                }
                else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.TechnicalDefect))
                {
                    request.AddedInDamageForm = true;
                    request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
                }
                else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.GuaranteeClaim))
                {
                    request.GuaranteeClaimRequired = true;
                    request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.ClaimAccepted), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.DefectWorkOrder), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed), EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Reschedule) };
                }
                else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.AwaitingSpares))
                {
                    request.AddedInDamageForm = false;
                    request.Category = null;
                    request.ComponentId = null;
                    request.DefectSystemAreaId = null;
                    request.FromDate = null;
                    request.GuaranteeClaimRequired = false;
                    request.IsCritical = false;
                    request.IsDue = false;
                    request.IsOffHire = false;
                    request.IsOverdue = false;
                    request.Priority = null;
                    request.Status = null;
                    request.SystemAreaId = null;
                    request.Title = null;
                    request.ToDate = null;
                    request.TopSystemAreaId = null;
                    request.Type = null;

                    //only awaitingspares is required
                    request.IsAwaitingSpares = true;
                }
                else if (filters.StageName == EnumsHelper.GetKeyValue(DefectManagerStages.Completed))
                {
                    request.Status = new List<string>() { EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed) };
                }
            }

            var value = new Dictionary<string, object>()
            {
                { "request", request }
            };

            List<DefectWorkBasketResponse> response = null;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/PWADefectWorkBasket"));
            response = await PostAsyncAutoPaged<DefectWorkBasketResponse>(requestUrl, value, 70);


            if (response != null && response.Any())
            {
                foreach (DefectWorkBasketResponse item in response)
                {
                    DefectWorkBasketResponseViewModel workBasket = new DefectWorkBasketResponseViewModel();
                    workBasket = ConvertDefectViewModel(item);
                    result.Add(workBasket);
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the get defect work order status list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DefectWorkOrderAttributeViewModel>> PostGetDefectWorkOrderStatusList()
        {
            List<DefectWorkOrderAttributeViewModel> result = new List<DefectWorkOrderAttributeViewModel>();
            List<DefectWorkOrderAttribute> response = new List<DefectWorkOrderAttribute>();

            List<DefectAttribute> lookupCodes = new List<DefectAttribute>()
            {
                DefectAttribute.WorkOrderPriority,
                DefectAttribute.WorkOrderStatus,
                DefectAttribute.SiteType,
                DefectAttribute.WorkOrderCategory
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/DefectWorkOrderAttributes"));
            response = await PostAsync<List<DefectWorkOrderAttribute>>(requestUrl, CreateHttpContent(lookupCodes));

            if (response != null && response.Any())
            {
                List<DefectWorkOrderAttribute> status = response.Where(x => x.LookupCode.Equals(EnumsHelper.GetDescription(DefectAttribute.WorkOrderStatus).ToString())).ToList();
                if (status != null && status.Any())
                {
                    foreach (DefectWorkOrderAttribute item in status)
                    {
                        DefectWorkOrderAttributeViewModel statusItem = new DefectWorkOrderAttributeViewModel();
                        statusItem.AttributeName = item.AttributeName ?? "";
                        statusItem.DalId = item.DalId ?? "";
                        result.Add(statusItem);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get defect work order planned for list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<DefectWorkOrderAttributeViewModel>> PostGetDefectWorkOrderPlannedForList()
        {
            List<DefectWorkOrderAttributeViewModel> result = new List<DefectWorkOrderAttributeViewModel>();
            List<DefectWorkOrderAttribute> response = new List<DefectWorkOrderAttribute>();

            List<DefectAttribute> lookupCodes = new List<DefectAttribute>()
            {
                DefectAttribute.WorkOrderPriority,
                DefectAttribute.WorkOrderStatus,
                DefectAttribute.SiteType,
                DefectAttribute.WorkOrderCategory
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/DefectWorkOrderAttributes"));
            response = await PostAsync<List<DefectWorkOrderAttribute>>(requestUrl, CreateHttpContent(lookupCodes));

            if (response != null && response.Any())
            {
                List<DefectWorkOrderAttribute> typeList = response.Where(x => x.LookupCode.Equals(EnumsHelper.GetDescription(DefectAttribute.SiteType).ToString())).ToList();
                if (typeList != null && typeList.Any())
                {
                    foreach (DefectWorkOrderAttribute item in typeList)
                    {
                        DefectWorkOrderAttributeViewModel plannedFor = new DefectWorkOrderAttributeViewModel();
                        plannedFor.AttributeName = item.AttributeName ?? "";
                        plannedFor.DalId = item.DalId ?? "";
                        result.Add(plannedFor);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the system area.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lookup>> GetSystemArea()
        {
            List<Lookup> response = new List<Lookup>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/SystemArea"));
            response = await GetAsync<List<Lookup>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the defect dashboar summaryd detail.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<DefectSummaryResponseViewModel> GetDefectDashboarSummarydDetail(string vesselId)
        {
            DefectSummaryResponseViewModel result = new DefectSummaryResponseViewModel();
            DefectSummaryResponse response = new DefectSummaryResponse();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
            string localVesselId = decreptedString.Split(Constants.Separator)[0];

            DefectSummaryRequest request = new DefectSummaryRequest
            {
                VesselId = localVesselId,
                ClosedDate = DateTime.Today.AddMonths(-12),
                AwaitingSparesPriorityLimit = 0,
                OffHirePriorityLimit = 0,
                OverduePriorityLimit = 0
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/PWADefectSummary"));
            response = await PostAsync<DefectSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                result.CloseDefectCount = response.CloseDefectCount.GetValueOrDefault();
                result.DrydockCount = response.DrydockCount.GetValueOrDefault();
                result.GuaranteeClaimCount = response.GuaranteeClaimCount.GetValueOrDefault();
                result.LayoverCount = response.LayoverCount.GetValueOrDefault();
                result.OffHireCount = response.OffHireCount.GetValueOrDefault();
                result.OpenDefectCount = response.OpenDefectCount.GetValueOrDefault();
                result.OverdueCount = response.OverdueCount.GetValueOrDefault();
                result.TechnicalDefectCount = response.TechnicalDefectCount.GetValueOrDefault();
                result.AwaitingSparesCount = response.AwaitingSparesCount.GetValueOrDefault();
                result.CompletedDefectsCount = response.CompletedDefectsCount;

                //setting priority here
                result.OffHirePriority = response.OffHirePriority;
                result.OpenDefectPriority = response.OpenDefectPriority;
                result.OverduePriority = response.OverduePriority;
                result.AwaitingSparesPriority = response.AwaitingSparesPriority;

                //setting url here
                DefectListViewModel overdueRequest = new DefectListViewModel();
                overdueRequest.EncryptedVesselId = vesselId;
                overdueRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.Overdue);
                overdueRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.Overdue);
                overdueRequest.ActiveMobileTabClass = Constants.Tab2;
                result.OverdueDefectNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(overdueRequest));

                DefectListViewModel OffHireRequest = new DefectListViewModel();
                OffHireRequest.EncryptedVesselId = vesselId;
                OffHireRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.OffHire);
                OffHireRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.OffHire);
                OffHireRequest.ActiveMobileTabClass = Constants.Tab2;
                result.OffHireDefectNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(OffHireRequest));

                DefectListViewModel TechnicalDefectRequest = new DefectListViewModel();
                TechnicalDefectRequest.EncryptedVesselId = vesselId;
                TechnicalDefectRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.TechnicalDefect);
                TechnicalDefectRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.TechnicalDefect);
                TechnicalDefectRequest.ActiveMobileTabClass = Constants.Tab2;
                result.TechnicalDefectNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(TechnicalDefectRequest));

                DefectListViewModel GuaranteeClaimRequest = new DefectListViewModel();
                GuaranteeClaimRequest.EncryptedVesselId = vesselId;
                GuaranteeClaimRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.GuaranteeClaim);
                GuaranteeClaimRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.GuaranteeClaim);
                GuaranteeClaimRequest.ActiveMobileTabClass = Constants.Tab2;
                result.GuaranteeClaimDefectNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(GuaranteeClaimRequest));

                DefectListViewModel LayOverRequest = new DefectListViewModel();
                LayOverRequest.EncryptedVesselId = vesselId;
                LayOverRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.Layover);
                LayOverRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.Layover);
                LayOverRequest.ActiveMobileTabClass = Constants.Tab2;
                result.LayOverDefectNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(LayOverRequest));

                DefectListViewModel DrydockRequest = new DefectListViewModel();
                DrydockRequest.EncryptedVesselId = vesselId;
                DrydockRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.Drydock);
                DrydockRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.Drydock);
                DrydockRequest.ActiveMobileTabClass = Constants.Tab2;
                result.DrydockDefectNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(DrydockRequest));

                DefectListViewModel ClosedDefectRequest = new DefectListViewModel();
                ClosedDefectRequest.EncryptedVesselId = vesselId;
                ClosedDefectRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.ClosedDefect);
                ClosedDefectRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.ClosedDefect);
                ClosedDefectRequest.ActiveMobileTabClass = Constants.Tab2;
                result.ClosedDefectNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(ClosedDefectRequest));

                DefectListViewModel OpenDefectRequest = new DefectListViewModel();
                OpenDefectRequest.EncryptedVesselId = vesselId;
                OpenDefectRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.OpenDefect);
                OpenDefectRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.OpenDefect);
                OpenDefectRequest.ActiveMobileTabClass = Constants.Tab1;
                result.OpenDefectNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(OpenDefectRequest));

                DefectListViewModel AwaitingSparesRequest = new DefectListViewModel();
                AwaitingSparesRequest.EncryptedVesselId = vesselId;
                AwaitingSparesRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.AwaitingSpares);
                AwaitingSparesRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.AwaitingSpares);
                AwaitingSparesRequest.ActiveMobileTabClass = Constants.Tab2;
                result.AwaitingSparesNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(AwaitingSparesRequest));

                DefectListViewModel CompleteDefectsRequest = new DefectListViewModel();
                CompleteDefectsRequest.EncryptedVesselId = vesselId;
                CompleteDefectsRequest.GridSubTitle = EnumsHelper.GetDescription(DefectManagerStages.Completed);
                CompleteDefectsRequest.StageName = EnumsHelper.GetKeyValue(DefectManagerStages.Completed);
                CompleteDefectsRequest.ActiveMobileTabClass = Constants.Tab2;
                result.CompletedDefectsNavigation = _provider.CreateProtector("DefectList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(CompleteDefectsRequest));
            }
            return result;
        }

        #endregion

        #region Defect Details

        /// <summary>
        /// Gets the defect work order header detail.
        /// </summary>
        /// <param name="defectWorkOrderId">The defect work order identifier.</param>
        /// <returns></returns>
        public async Task<DefectWorkOrderViewModel> GetDefectWorkOrderHeaderDetail(string defectWorkOrderId)
        {
            DefectWorkOrderViewModel result = null;
            DefectWorkOrder response = null;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/WOHeaderDetail/" + defectWorkOrderId));

            if (!string.IsNullOrWhiteSpace(defectWorkOrderId))
            {
                response = await GetAsync<DefectWorkOrder>(requestUrl);
            }

            if (response != null)
            {
                result = new DefectWorkOrderViewModel
                {
                    DefectName = response.DefectName,
                    DefectNumber = response.DefectNumber,
                    IsCompletedOrClosed = !string.IsNullOrWhiteSpace(response.StatusId) && (response.StatusId.Equals(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Close)) || response.StatusId.Equals(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed))),
                    IsGuaranteeClaimCode = response.GuaranteeClaimRequired && !string.IsNullOrWhiteSpace(response.GuaranteeClaimNumber),
                    IsStatusCompleted = !string.IsNullOrWhiteSpace(response.StatusId) && (response.StatusId.Equals(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed)))
                };
            }

            return result;
        }

        /// <summary>
        /// Posts the get defect work order for edit.
        /// </summary>
        /// <param name="EncryptedDWOId">The encrypted dwo identifier.</param>
        /// <returns></returns>
        public async Task<DefectWorkOrderViewModel> PostGetDefectWorkOrderForEdit(string EncryptedDWOId)
        {
            DefectWorkOrderViewModel result = new DefectWorkOrderViewModel();
            DefectWorkOrder response = null;
            string defectWorkOrderId = _provider.CreateProtector("DefectDwoId").Unprotect(EncryptedDWOId);
            string urlDefectWorkOrderId = "dwoId=" + defectWorkOrderId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/WorkOrderDetail/"), urlDefectWorkOrderId);

            if (!string.IsNullOrWhiteSpace(defectWorkOrderId))
            {
                response = await PostAsync<DefectWorkOrder>(requestUrl, CreateHttpContent(defectWorkOrderId));
            }

            if (response != null)
            {
                result = SetDefectWorkOrderViewModel(response);
            }

            return result;
        }

        /// <summary>
        /// Sets the defect work order view model.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private DefectWorkOrderViewModel SetDefectWorkOrderViewModel(DefectWorkOrder entity)
        {
            DefectWorkOrderViewModel viewModel = new DefectWorkOrderViewModel();
            viewModel.DefectName = entity.DefectName;
            viewModel.DefectNumber = entity.DefectNumber;
            viewModel.DueDate = entity.OriginalDueDate.HasValue ? entity.OriginalDueDate.Value.ToString(Constants.DateFormat) : "";
            viewModel.DefectDescription = entity.DefectDescription ?? "";
            viewModel.RepairSpecification = entity.RepairSpecification ?? "";
            viewModel.Category = entity.Category ?? "";
            viewModel.DefectSystemArea = entity.DefectSystemArea ?? "";
            viewModel.DefectSubSystemArea = entity.DefectSubSystemArea ?? "";
            viewModel.Priority = entity.Priority;
            viewModel.SiteType = entity.SiteType;
            viewModel.ProposedStartDate = entity.ProposedStartDate.HasValue ? entity.ProposedStartDate.Value.ToString(Constants.DateFormat) : "";
            viewModel.EstimatedCompletionDate = entity.EstimatedCompletionDate.HasValue ? entity.EstimatedCompletionDate.Value.ToString(Constants.DateFormat) : "";
            viewModel.FoundDate = entity.FoundDate.HasValue ? entity.FoundDate.Value.ToString(Constants.DateFormat) : "";
            viewModel.DefectStatusDescription = entity.DefectStatusDescription ?? "";

            //Defect Attribute Section in PF
            viewModel.IsCritical = entity.IsCritical.GetValueOrDefault();
            viewModel.IncludeInDamageForm = entity.IncludeInDamageForm.GetValueOrDefault();
            viewModel.IsJSARequired = entity.IsJSARequired;
            viewModel.IsMOCRequired = entity.IsMOCRequired;
            if (entity != null && entity.AccessoryWork != null && entity.AccessoryWork.Any(work => work.DalId == Constants.AccessoryVessel || work.DalId == Constants.AccessoryUserDefined))
            {
                viewModel.OwnerSpecificId = entity.AccessoryWork.FirstOrDefault(work => work.DalId == Constants.AccessoryVessel || work.DalId == Constants.AccessoryUserDefined).DalName;
                viewModel.SpecificAttributeValue = entity.AccessoryWork.FirstOrDefault(work => work.DalId == Constants.AccessoryVessel || work.DalId == Constants.AccessoryUserDefined).SpecificValue;
            }
            else
            {
                viewModel.OwnerSpecificId = "-";
                viewModel.SpecificAttributeValue = "-";
            }
            //Impact section in PF
            viewModel.IsOffHire = entity.IsOffHire.GetValueOrDefault();

            if (!entity.EstimatedPeriod.HasValue && !entity.OffHireHours.HasValue && !entity.OffHireMins.HasValue)
            {
                viewModel.EstimatedPeriod = "-";
            }
            else
            {
                string estimatedPeriod = "";
                if (entity.EstimatedPeriod.HasValue)
                {
                    estimatedPeriod = estimatedPeriod + (entity.EstimatedPeriod.HasValue ? entity.EstimatedPeriod.GetValueOrDefault().ToString() + Constants.EstimatedPeriodDays : "");
                }
                if (entity.OffHireHours.HasValue)
                {
                    estimatedPeriod = estimatedPeriod + "  " + entity.OffHireHours.GetValueOrDefault().ToString() + Constants.EstimatedPeriodHours;
                }
                if (entity.OffHireMins.HasValue)
                {
                    estimatedPeriod = estimatedPeriod + "  " + entity.OffHireMins.GetValueOrDefault().ToString() + Constants.EstimatedPeriodMinutes;
                }
                viewModel.EstimatedPeriod = estimatedPeriod;

            }

            viewModel.OffHirePeriodId = entity.OffHirePeriodId;
            viewModel.Impact = entity.Impact;
            viewModel.IsImpact = !string.IsNullOrWhiteSpace(entity.Impact);
            viewModel.IsRegulatoryAuthority = entity.IsRegulatoryAuthority;
            viewModel.DispensationInPlace = entity.DispensationInPlace;
            viewModel.IsGasFree = entity.IsGasFree;
            viewModel.RankDescription = entity.RankDescription;


            //Action list
            viewModel.ActionList = SetActionList(entity.ActionTaken);

            viewModel.IsCompletedOrClosed = !string.IsNullOrWhiteSpace(entity.StatusId) && (entity.StatusId.Equals(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Close)) || entity.StatusId.Equals(EnumsHelper.GetKeyValue(DefectWorkOrderStatus.Completed)));

            return viewModel;
        }

        /// <summary>
        /// Sets the action list.
        /// </summary>
        /// <param name="defectAction">The defect action.</param>
        /// <returns></returns>
        private List<DefectActionTakenViewModel> SetActionList(List<DefectActionTaken> defectAction)
        {
            List<DefectActionTakenViewModel> ActionVMList = new List<DefectActionTakenViewModel>();
            if (defectAction != null && defectAction.Any())
            {
                foreach (DefectActionTaken item in defectAction)
                {
                    DefectActionTakenViewModel viewModel = new DefectActionTakenViewModel();
                    viewModel.Action = item.Action;
                    viewModel.Date = item.Date;
                    viewModel.ReportedBy = item.ReportedBy;
                    viewModel.ReportedById = item.ReportedById;
                    ActionVMList.Add(viewModel);
                }
            }
            return ActionVMList;
        }

        /// <summary>
        /// Posts the get defect report wo summary.
        /// </summary>
        /// <param name="EncryptedDWOId">The encrypted dwo identifier.</param>
        /// <returns></returns>
        public async Task<DefectReportWorkOrderSummaryViewModel> PostGetDefectReportWOSummary(string EncryptedDWOId)
        {
            DefectReportWorkOrderSummaryViewModel result = new DefectReportWorkOrderSummaryViewModel();
            DefectReportWorkOrderSummary response = null;
            string defectWorkOrderId = _provider.CreateProtector("DefectDwoId").Unprotect(EncryptedDWOId);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/ReportWOSummary/" + defectWorkOrderId));
            response = await GetAsync<DefectReportWorkOrderSummary>(requestUrl);
            if (response != null)
            {
                result.SpareParts = SetSparePartList(response.PartsUsed);
                Tuple<List<DefectReportWorkOrderRankViewModel>, bool> ShoreStaff = SetShoreStaffList(response);
                result.ShoreStaff = ShoreStaff.Item1;
                result.IsShoreStaff = ShoreStaff.Item2;

                Tuple<List<DefectReportWorkOrderRankViewModel>, bool> ShipStaff = SetShipStaffList(response);
                result.ShipStaff = ShipStaff.Item1;
                result.IsShipStaff = ShipStaff.Item2;

                result.Remark = response.Remark;
            }

            return result;
        }

        /// <summary>
        /// Sets the spare part list.
        /// </summary>
        /// <param name="spareParts">The spare parts.</param>
        /// <returns></returns>
        private List<DefectReportWorkOrderPartsUsedViewModel> SetSparePartList(List<DefectReportWorkOrderPartsUsed> spareParts)
        {
            List<DefectReportWorkOrderPartsUsedViewModel> sparePartList = new List<DefectReportWorkOrderPartsUsedViewModel>();
            if (spareParts != null && spareParts.Any())
            {
                foreach (DefectReportWorkOrderPartsUsed item in spareParts)
                {
                    DefectReportWorkOrderPartsUsedViewModel sparePartVM = new DefectReportWorkOrderPartsUsedViewModel();
                    sparePartVM.PartName = item.PartName ?? "";
                    sparePartVM.MakerReferenceNumber = item.MakerReferenceNumber ?? "";
                    sparePartVM.PlateSheetNumber = item.PlateSheetNumber ?? "";
                    sparePartVM.DrawingPositionNumber = item.DrawingPositionNumber ?? "";
                    sparePartVM.Location = item.Location ?? "";
                    sparePartVM.Condition = item.Condition ?? "";
                    sparePartVM.QuantityUsed = item.QuantityUsed;
                    sparePartList.Add(sparePartVM);
                }
            }
            return sparePartList;
        }

        /// <summary>
        /// Sets the shore staff list.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private Tuple<List<DefectReportWorkOrderRankViewModel>, bool> SetShoreStaffList(DefectReportWorkOrderSummary entity)
        {
            List<DefectReportWorkOrderRankViewModel> ShoreStaffList = new List<DefectReportWorkOrderRankViewModel>();
            bool IsShoreStaff = false;
            if (entity.StaffRank != null && entity.StaffRank.Any(x => string.IsNullOrWhiteSpace(x.RnkId)))
            {
                foreach (DefectReportWorkOrderRank item in entity.StaffRank.Where(x => string.IsNullOrWhiteSpace(x.RnkId)))
                {
                    DefectReportWorkOrderRankViewModel viewModel = new DefectReportWorkOrderRankViewModel();
                    viewModel.ShoreStaffRank = item.ShoreStaffRank;
                    viewModel.Hours = item.Hours;
                    ShoreStaffList.Add(viewModel);
                }
                IsShoreStaff = true;
            }

            Tuple<List<DefectReportWorkOrderRankViewModel>, bool> shoreStaff = new Tuple<List<DefectReportWorkOrderRankViewModel>, bool>(ShoreStaffList, IsShoreStaff);
            return shoreStaff;
        }

        /// <summary>
        /// Sets the ship staff list.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        private Tuple<List<DefectReportWorkOrderRankViewModel>, bool> SetShipStaffList(DefectReportWorkOrderSummary entity)
        {
            List<DefectReportWorkOrderRankViewModel> ShipStaffList = new List<DefectReportWorkOrderRankViewModel>();
            bool IsShipStaff = false;
            if (entity.StaffRank != null && entity.StaffRank.Any(x => !string.IsNullOrWhiteSpace(x.RnkId)))
            {
                foreach (DefectReportWorkOrderRank item in entity.StaffRank.Where(x => !string.IsNullOrWhiteSpace(x.RnkId)))
                {
                    DefectReportWorkOrderRankViewModel viewModel = new DefectReportWorkOrderRankViewModel();
                    viewModel.ShipStaffRank = item.ShipStaffRank;
                    viewModel.Hours = item.Hours;
                    ShipStaffList.Add(viewModel);
                }
                IsShipStaff = true;
            }

            Tuple<List<DefectReportWorkOrderRankViewModel>, bool> shipStaff = new Tuple<List<DefectReportWorkOrderRankViewModel>, bool>(ShipStaffList, IsShipStaff);
            return shipStaff;
        }

        /// <summary>
        /// Gets the defect wo reschedule log.
        /// </summary>
        /// <param name="EncryptedDWOId">The encrypted dwo identifier.</param>
        /// <returns></returns>
        public async Task<List<RescheduleDefectWorkOrderViewModel>> GetDefectWORescheduleLog(string EncryptedDWOId)
        {
            List<RescheduleDefectWorkOrderViewModel> result = new List<RescheduleDefectWorkOrderViewModel>();
            List<RescheduleDefectWorkOrder> response = null;

            string defectWorkOrderId = _provider.CreateProtector("DefectDwoId").Unprotect(EncryptedDWOId);
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/WORescheduleLog/" + defectWorkOrderId));
            response = await GetAsync<List<RescheduleDefectWorkOrder>>(requestUrl);

            if (response != null && response.Any())
            {
                foreach (RescheduleDefectWorkOrder item in response)
                {
                    RescheduleDefectWorkOrderViewModel viewModel = new RescheduleDefectWorkOrderViewModel();
                    viewModel.PreviousDueDate = item.PreviousDueDate;
                    viewModel.RequestedDueDate = item.RequestedDueDate;
                    viewModel.NewDueDate = item.NewDueDate;
                    viewModel.WorkOrderReasonDescription = item.WorkOrderReasonDescription ?? "";
                    viewModel.RequestedBy = item.RequestedBy ?? "";
                    viewModel.RequesterRole = item.RequesterRole ?? "";
                    viewModel.ApprovedBy = item.ApprovedBy ?? "";
                    viewModel.ApproverRole = item.ApproverRole ?? "";
                    viewModel.StatusShortCode = item.StatusShortCode;
                    viewModel.RescheduleWorkOrderStatus = item.RescheduleWorkOrderStatus ?? "";
                    viewModel.IsStatusShortCode = !string.IsNullOrWhiteSpace(item.StatusShortCode);
                    viewModel.StatusColour = GetStatusColor(item.RescheduleWorkOrderStatus);
                    result.Add(viewModel);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the color of the status.
        /// </summary>
        /// <param name="rescheduleWorkOrderStatus">The reschedule work order status.</param>
        /// <returns>
        /// Single KPI object.
        /// </returns>
        private string GetStatusColor(string rescheduleWorkOrderStatus)
        {
            if (rescheduleWorkOrderStatus.Equals(EnumsHelper.GetDescription<DefectWorkOrderRescheduleStatus>(DefectWorkOrderRescheduleStatus.Approved)))
            {
                return "Normal";
            }
            else
                if (rescheduleWorkOrderStatus.Equals(EnumsHelper.GetDescription<DefectWorkOrderRescheduleStatus>(DefectWorkOrderRescheduleStatus.Pending)))
            {
                return "PreWarning";
            }
            else
                    if (rescheduleWorkOrderStatus.Equals(EnumsHelper.GetDescription<DefectWorkOrderRescheduleStatus>(DefectWorkOrderRescheduleStatus.Rejected)))
            {
                return "Critical";
            }
            return "Normal";
        }

        /// <summary>
        /// Gets the mapped requisition.
        /// </summary>
        /// <param name="EncryptedDWOId">The encrypted dwo identifier.</param>
        /// <param name="EncryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<DefectRequisitionViewModel>> GetMappedRequisition(string EncryptedDWOId, string EncryptedVesselId)
        {
            List<DefectRequisitionViewModel> result = new List<DefectRequisitionViewModel>();
            List<DefectRequisition> response = null;
            string defectWorkOrderId = _provider.CreateProtector("DefectDwoId").Unprotect(EncryptedDWOId);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/MappedRequisition/" + defectWorkOrderId));
            response = await GetAsync<List<DefectRequisition>>(requestUrl);

            if (response != null && response.Any())
            {
                foreach (DefectRequisition item in response)
                {
                    DefectRequisitionViewModel viewModel = new DefectRequisitionViewModel();
                    string decreptedString = _provider.CreateProtector("Vessel").Unprotect(EncryptedVesselId);
                    PurchaseOrderRequestViewModel purchaseOrderUrl = new PurchaseOrderRequestViewModel();
                    purchaseOrderUrl.VesselId = decreptedString.Split(Constants.Separator)[0];
                    purchaseOrderUrl.VesselName = decreptedString.Split(Constants.Separator)[1];
                    purchaseOrderUrl.ToDate = null;
                    purchaseOrderUrl.FromDate = null;
                    purchaseOrderUrl.AccountCompanyId = item.CoyId;
                    purchaseOrderUrl.OrderNumber = item.OrderNumber;
                    purchaseOrderUrl.POStage = EnumsHelper.GetKeyValue(PoStagesFilter.InProcess);

                    viewModel.EncryptedVesselId = CommonUtil.GetEncryptedVessel(_provider, decreptedString.Split(Constants.Separator)[0], decreptedString.Split(Constants.Separator)[1], item.CoyId);
                    viewModel.PurchaseOrderUrl = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(purchaseOrderUrl));
                    viewModel.CoyId = item.CoyId;
                    viewModel.OrderNumber = item.OrderNumber;
                    viewModel.Priority = EnumsHelper.GetEnumNameFromKeyValue(typeof(PurchaseOrderPriority), item.Priority);
                    viewModel.StatusDescription = EnumsHelper.GetEnumNameFromKeyValue(typeof(PurchaseOrderStatus), item.Status);
                    viewModel.StatusShortCode = item.Status ?? "";
                    viewModel.IsStatusVisible = !string.IsNullOrWhiteSpace(item.Status);
                    if (item.Status != null)
                    {
                        if (item.Priority == EnumsHelper.GetKeyValue(PurchaseOrderPriority.Urgent))
                        {
                            viewModel.OrderStatusColor = "Critical";
                        }
                        else
                        {
                            viewModel.OrderStatusColor = "Good";
                        }
                    }
                    else
                    {
                        viewModel.OrderStatusColor = "PreWarning";
                    }
                    viewModel.OrderName = item.OrderName;
                    viewModel.OrderType = item.OrderType;
                    viewModel.RequestedDate = item.RequestedDate;
                    viewModel.OrderDate = item.OrderDate;
                    viewModel.ExpectedDeliveryDate = item.ExpectedDeliveryDate;
                    viewModel.ReceivedDate = item.ReceivedDate;
                    result.Add(viewModel);
                }
            }
            return result;
        }

        /// <summary>
        /// Closes the defect action.
        /// </summary>
        /// <param name="dwoIds">The dwo ids.</param>
        /// <param name="refetchAfterSave">if set to <c>true</c> [refetch after save].</param>
        /// <returns></returns>
        public async Task<bool> CloseDefectAction(List<string> dwoIds, bool refetchAfterSave)
        {
            var input = new Dictionary<string, object>()
                {
                    {"dwoIds", dwoIds },
                    {"refetchAfterSave",refetchAfterSave }
                };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "DefectManager/CloseDefectWorkOrder"));
            bool response = await PostAsync<bool>(requestUrl, CreateHttpContent(input));
            return response;
        }

        #endregion

        #endregion

        #region PMS Dashboard
        /// <summary>
        /// Posts the get hazocc dashboard detail.
        /// </summary>
        /// <param name="pmsDashboardRequestViewModel">The PMS dashboard request view model.</param>
        /// <returns></returns>
        public async Task<PMSDashboardDetailViewModel> PostGetPMSDashboardDetail(PMSDashboardRequestViewModel pmsDashboardRequestViewModel)
        {
            PMSDashboardDetailViewModel pmsDetails = new PMSDashboardDetailViewModel();
            PlannedMaintenanceSummary response = null;
            VesselDasboardRequest request = new VesselDasboardRequest();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(pmsDashboardRequestViewModel.VesselId);

            request.FromDate = pmsDashboardRequestViewModel.FromDate;
            request.ToDate = pmsDashboardRequestViewModel.ToDate;
            request.VesselId = decreptedString.Split(Constants.Separator)[0];

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/Summary"));
            response = await PostAsync<PlannedMaintenanceSummary>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                PMSRequestViewModel pmsRequestViewModel = new PMSRequestViewModel();
                pmsRequestViewModel.FromDate = pmsDashboardRequestViewModel.FromDate;
                int intMonth = pmsDashboardRequestViewModel.ToDate.Month;
                int intYear = pmsDashboardRequestViewModel.ToDate.Year;
                int intlastDateofMonth = DateTime.DaysInMonth(intYear, intMonth);
                pmsRequestViewModel.ToDate = new DateTime(intYear, intMonth, intlastDateofMonth, 23, 59, 59);

                pmsRequestViewModel.VesselId = decreptedString.Split(Constants.Separator)[0];
                pmsRequestViewModel.IsSummaryClicked = true;

                //Critical
                pmsDetails.CriticalDoneCount = response.CriticalWODoneCount;
                pmsDetails.CriticalDueCount = response.CriticalWODueCount;
                pmsDetails.CriticalOverDueCount = response.CriticalWOOverdueCount;
                pmsDetails.CriticalOverDuePriorCount = response.CriticalWOPriorOverdueCount;

                pmsDetails.CriticalDoneUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.CriticalDone);
                pmsDetails.CriticalDueUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.CriticalDue);
                pmsDetails.CriticalOverDueUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.CriticalOverDue);
                pmsDetails.CriticalOverDuePriorUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.CriticalOverDuePrior);

                pmsDetails.NonCriticalDoneCount = response.NonCriticalWODoneCount;
                pmsDetails.NonCriticalDueCount = response.NonCriticalWODueCount;
                pmsDetails.NonCriticalOverDueCount = response.NonCriticalWOOverdueCount;
                pmsDetails.NonCriticalOverDuePriorCount = response.NonCriticalWOPriorOverdueCount;

                pmsDetails.NonCriticalDoneUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.NonCriticalDone);
                pmsDetails.NonCriticalDueUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.NonCriticalDue);
                pmsDetails.NonCriticalOverDueUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.NonCriticalOverDue);
                pmsDetails.NonCriticalOverDuePriorUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.NonCriticalOverDuePrior);

                pmsDetails.ShipsWOPlannedCount = response.ShipsWOPlannedCount;
                pmsDetails.ShipsWODoneCount = response.ShipsWODoneCount;
                pmsDetails.RescWOApprovedCount = response.CalendarBasedWORescheduleCount;
                pmsDetails.SparesBelowTechMinCount = response.SpareBelowTechMinCount;
                pmsDetails.SparesBelowOprMinCount = response.SpareBelowOprMinCount;

                pmsDetails.ShipsWOPlannedUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.ShipsWOPlanned);
                pmsDetails.ShipsWODoneUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.ShipsWODone);
                pmsDetails.RescWOApprovedUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.RescWOApproved);
                pmsDetails.SparesBelowTechMinUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.SparesBelowTechMin);
                pmsDetails.SparesBelowOprMinUrl = SetPMSUrl(pmsRequestViewModel, PMSDashboardType.SparesBelowOprMin);
            }

            return pmsDetails;
        }

        /// <summary>
        /// PMSs the dashboard summary.
        /// </summary>
        /// <param name="summaryRequest">The summary request.</param>
        /// <returns></returns>
        public async Task<PMSDashboardSummaryViewModel> PMSDashboardSummary(PMSDashboardRequestViewModel summaryRequest)
        {
            PMSDashboardSummaryViewModel result = new PMSDashboardSummaryViewModel();

            PMSDashboardSummaryResponse response = new PMSDashboardSummaryResponse();
            PMSDashboardSummaryRequest request = new PMSDashboardSummaryRequest();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(summaryRequest.VesselId);
            DateTime now = DateTime.Now;
            request.StartDate = new DateTime(now.Year, now.Month, 1);
            request.EndDate = request.StartDate.AddMonths(1).AddDays(-1);
            request.VesselId = decreptedString.Split(Constants.Separator)[0];
            request.CriticalOverduePriorityLimit = Constants.CriticalOverduePriorityLimitPMS;
            request.ReportedInLastNDays = Constants.ReportedInLastNDays;
            request.OverduePriorityLimit = Constants.OverduePriorityLimitPMS;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/PWAPMSDashboardSummary"));
            response = await PostAsync<PMSDashboardSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                //Count
                result.Due = response.WorkOrderDueCount;
                result.CriticalDue = response.CriticalComponentWODueCount;
                result.Overdue = response.WOPriorMonthOverdueCount;
                result.CriticalOverdue = response.CriticalComponentWOPriorMonthOverdueCount;
                result.Critical = response.CriticalWO;
                result.PlannedFor = response.PlannedForWO;
                result.ReqReschedule = response.ReqRescheduleWO;
                result.CompletedWO = response.WOOfficeApprovalCount;

                //KPI priority
                result.DuePriority = response.DuePriority;
                result.CriticalDuePriority = response.CriticalDuePriority;
                result.OverduePriority = response.PriorMonthOverduePriority;
                result.CriticalOverduePriority = response.PriorMonthCriticalOverduePriority;

                //Navigation 
                request.GridSubTitle = Constants.PMSDue;
                result.DueURL = SetPMSNavigationURL(request, PMSDashboardStage.Due, Constants.Tab2);
                request.GridSubTitle = Constants.PMSCriticalDue;
                result.CriticalDueURL = SetPMSNavigationURL(request, PMSDashboardStage.CriticalDue, Constants.Tab2);
                request.GridSubTitle = Constants.PMSOverdue;
                result.OverdueURL = SetPMSNavigationURL(request, PMSDashboardStage.Overdue, Constants.Tab2);
                request.GridSubTitle = Constants.PMSCriticalOverdue;
                result.CriticalOverdueURL = SetPMSNavigationURL(request, PMSDashboardStage.CriticalOverdue, Constants.Tab2);
                request.GridSubTitle = Constants.PMSCritical;
                result.CriticalURL = SetPMSNavigationURL(request, PMSDashboardStage.Critical, Constants.Tab2);
                request.GridSubTitle = Constants.PMSPlannedFor;
                result.PlannedForURL = SetPMSNavigationURL(request, PMSDashboardStage.PlannedFor, Constants.Tab2);
                request.GridSubTitle = Constants.PMSReqReschedule;
                result.ReqRescheduleURL = SetPMSNavigationURL(request, PMSDashboardStage.ReqReschedule, Constants.Tab2);
                request.GridSubTitle = Constants.PMSCompleted;
                result.CompletedUrl = SetPMSNavigationURL(request, PMSDashboardStage.Completed, Constants.Tab2);
                request.GridSubTitle = Constants.PMSAll;
                result.AllRequestURL = SetPMSNavigationURL(request, PMSDashboardStage.All, Constants.Tab1);
            }

            return result;
        }

        /// <summary>
        /// Sets the PMS navigation URL.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <param name="stageName">Name of the stage.</param>
        /// <returns></returns>
        private string SetPMSNavigationURL(PMSDashboardSummaryRequest inputRequest, PMSDashboardStage stageName, string TabName)
        {
            PlannedMaintenanceListViewModel input = new PlannedMaintenanceListViewModel();
            input.FromDate = inputRequest.StartDate;
            input.ToDate = inputRequest.EndDate;
            input.StageName = EnumsHelper.GetDescription(stageName);
            input.GridSubTitle = inputRequest.GridSubTitle;
            input.ActiveMobileTabClass = TabName;
            string pmsURL = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(input));
            return pmsURL;
        }

        /// <summary>
        /// Sets the PMS URL.
        /// </summary>
        /// <param name="pmsRequestViewModel">The PMS request view model.</param>
        /// <param name="pmsDashboardType">Type of the PMS dashboard.</param>
        /// <returns></returns>
        private string SetPMSUrl(PMSRequestViewModel pmsRequestViewModel, PMSDashboardType pmsDashboardType)
        {
            pmsRequestViewModel.PMSDashboardType = pmsDashboardType;
            string hazoccUrl = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(pmsRequestViewModel));
            return hazoccUrl;
        }

        #endregion

        #region PMS Approval

        /// <summary>
        /// Gets the PMS Pending Approval list.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public async Task<List<ApprovalPmsResponseViewModel>> GetPmsPendingApprovalList(ApprovalPmsRequestViewModel filters)
        {
            List<ApprovalPmsResponseViewModel> result = new List<ApprovalPmsResponseViewModel>();

            PmsPendingApprovalRequest request = new PmsPendingApprovalRequest();
            if (!string.IsNullOrWhiteSpace(filters.EncryptedVesselId))
            {
                string decreptedString = CommonUtil.GetDecryptedVessel(_provider, filters.EncryptedVesselId);
                request.VesselId = decreptedString.Split(Constants.Separator)[0];
            }
            request.FleetId = filters.FleetId;
            request.MenuType = filters.MenuType;
            request.WorkOrderStatusIds = filters.WorkOrderStatusIds;
            var value = new Dictionary<string, object>()
            {
                { "request", request }
            };


            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/GetPMSApprovalsPaged"));
            List<PmsPendingApprovalResponse> response = await PostAsyncAutoPaged<PmsPendingApprovalResponse>(requestUrl, value, 70);

            if (response != null && response.Any())
            {
                foreach (PmsPendingApprovalResponse item in response)
                {
                    PlannedMaintenanceRequestViewModel plannedMaintenanceRequestUrl = new PlannedMaintenanceRequestViewModel();
                    plannedMaintenanceRequestUrl.ComponentId = item.ComponentId;
                    plannedMaintenanceRequestUrl.WorkOrderId = item.WorkOrderId;
                    plannedMaintenanceRequestUrl.ScheduleTaskId = item.ScheduleTaskId;
                    plannedMaintenanceRequestUrl.IsNavigatedFromDone = false;
                    plannedMaintenanceRequestUrl.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);

                    ApprovalPmsResponseViewModel workOrderResponse = new ApprovalPmsResponseViewModel();
                    workOrderResponse.VesselId = item.VesselId;
                    workOrderResponse.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);
                    workOrderResponse.VesselName = item.VesselName;
                    workOrderResponse.DueDate = item.DueDate;
                    workOrderResponse.ComponentName = item.ComponentName;
                    workOrderResponse.JobName = item.JobName;
                    workOrderResponse.StatusCode = item.StatusCode;
                    workOrderResponse.JobTypeShortCode = item.JobTypeShortCode;
                    workOrderResponse.JobTypeDescription = item.JobTypeDescription;
                    workOrderResponse.ResponsibleRankShortCode = item.ResponsibleRankShortCode;
                    workOrderResponse.Interval = (item.Frequency ?? 0) + " " + item.FrequencyTypeShortCode;

                    //navigation
                    if (!string.IsNullOrWhiteSpace(item.ScheduleTaskId))
                    {
                        //work order WO

                    }
                    else if (!string.IsNullOrWhiteSpace(item.DwoId) && !string.IsNullOrWhiteSpace(item.WorkOrderIndicationTypeId) && item.WorkOrderIndicationTypeId == EnumsHelper.GetKeyValue(WorkOrderIndicationType.Defect))
                    {
                        //defect DWO
                        workOrderResponse.IsDefectWorkOrder = true;
                        workOrderResponse.DwoId = item.DwoId;

                        DefectDetailsViewModel defectDetails = new DefectDetailsViewModel();
                        defectDetails.DefectWorkOrderId = item.DwoId;
                        defectDetails.DueDate = item.DueDate.HasValue ? item.DueDate.Value.ToString(Constants.DateFormat) : "";
                        defectDetails.IsGuaranteeClaimCode = false;

                        workOrderResponse.DefectDetailsUrl = _provider.CreateProtector("DefectDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(defectDetails));
                    }
                    else
                    {
                        //unplanned work order SWO
                        plannedMaintenanceRequestUrl.IsSWO = true;
                    }
                    string plannedMaintenanceDetailsRequest = _provider.CreateProtector("PMSDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(plannedMaintenanceRequestUrl));
                    workOrderResponse.PlannedMaintenanceDetailsRequestURL = plannedMaintenanceDetailsRequest;
                    result.Add(workOrderResponse);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the PMS Pending Reschedule Request list.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        public async Task<List<ApprovalPmsRescheduleResponseViewModel>> GetPmsRescheduleRequestApprovallList(ApprovalPmsRescheduleRequestViewModel filters)
        {
            List<ApprovalPmsRescheduleResponseViewModel> result = new List<ApprovalPmsRescheduleResponseViewModel>();

            ApprovalPmsRescheduleRequest request = new ApprovalPmsRescheduleRequest();
            if (!string.IsNullOrWhiteSpace(filters.EncryptedVesselId))
            {
                string decreptedString = CommonUtil.GetDecryptedVessel(_provider, filters.EncryptedVesselId);
                request.VesselId = decreptedString.Split(Constants.Separator)[0];
            }
            request.FleetId = filters.FleetId;
            request.MenuType = filters.MenuType;
            var value = new Dictionary<string, object>()
            {
                { "request", request }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/GetAwaitingApprovalRescheduleList"));
            List<ApprovalPmsRescheduleResponse> response = await PostAsyncAutoPaged<ApprovalPmsRescheduleResponse>(requestUrl, value, 70);

            if (response != null && response.Any())
            {
                foreach (ApprovalPmsRescheduleResponse item in response)
                {
                    PlannedMaintenanceRequestViewModel plannedMaintenanceRequestUrl = new PlannedMaintenanceRequestViewModel();
                    plannedMaintenanceRequestUrl.ComponentId = item.PtrId;
                    plannedMaintenanceRequestUrl.WorkOrderId = item.PwoId;
                    plannedMaintenanceRequestUrl.ScheduleTaskId = item.PstId;
                    plannedMaintenanceRequestUrl.IsNavigatedFromDone = false;
                    plannedMaintenanceRequestUrl.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);

                    ApprovalPmsRescheduleResponseViewModel workOrderResponse = new ApprovalPmsRescheduleResponseViewModel();
                    workOrderResponse.RescheduleType = item.RescheduleType;
                    workOrderResponse.JobType = item.JobTypeShortCode;
                    workOrderResponse.RequestNumber = item.RequestNumber ?? "";
                    workOrderResponse.PreviousDueDate = item.PreviousDueDate;
                    workOrderResponse.CurrentDueDate = item.CurrentDueDate;
                    workOrderResponse.ComponentName = item.ComponentName;
                    workOrderResponse.JobName = item.JobName;
                    workOrderResponse.Status = SetRescheduleStatusColor(item.RescheduleStatusId);
                    workOrderResponse.StatusShortCode = item.StatusShortCode;
                    workOrderResponse.RescheduleStatusName = item.RescheduleStatusName;
                    workOrderResponse.OriginalInterval = item.OriginalInterval.ToString() ?? "";
                    workOrderResponse.RequestedInterval = item.RequestedInterval.ToString() ?? "";
                    if (workOrderResponse.OriginalInterval != "")
                        workOrderResponse.OriginalInterval = workOrderResponse.OriginalInterval + " " + item.IntervalTypeShortCode;
                    if (workOrderResponse.RequestedInterval != "")
                        workOrderResponse.RequestedInterval = workOrderResponse.RequestedInterval + " " + item.IntervalTypeShortCode;
                    workOrderResponse.ResponsibleRank = item.ResponsibleRank;
                    workOrderResponse.IntervalTypeShortCode = item.IntervalTypeShortCode;
                    workOrderResponse.VesselId = item.VesselId;
                    workOrderResponse.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);
                    workOrderResponse.VesselName = item.VesselName;
                    workOrderResponse.Comment = item.Comment;

                    //Checking current month Overdue.
                    if (item.PreviousDueDate != null && item.PreviousDueDate.Value.Date >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1) && item.PreviousDueDate.Value.Date < DateTime.Now.Date)
                    {
                        workOrderResponse.IsOverDueVisible = true;
                    }
                    else if (item.PreviousDueDate != null && item.PreviousDueDate.Value.Date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)) //Checking Overdue prior from current month  
                    {
                        workOrderResponse.IsOverduePeriodVisible = true;
                    }
                    else
                    {
                        workOrderResponse.IsDue = true;
                    }

                    //unplanned work order SWO
                    plannedMaintenanceRequestUrl.IsSWO = false;

                    string plannedMaintenanceDetailsRequest = _provider.CreateProtector("PMSDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(plannedMaintenanceRequestUrl));
                    workOrderResponse.PlannedMaintenanceDetailsRequestURL = plannedMaintenanceDetailsRequest;
                    result.Add(workOrderResponse);
                }
            }

            return result;
        }


        #endregion
        /// <summary>
        /// Gets the certificate issue log.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<CertificateIssueDetail>> GetCertificateIssueLog(Dictionary<string, object> input)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Certificate/IssueLog"));
            var response = await PostAsync<List<CertificateIssueDetail>>(requestUrl, CreateHttpContent(input));
            return response;
        }

        /// <summary>
        /// Gets the PMS certificates.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<WorkBasketAllDetailViewModel>> GetPMSCertificates(string vesselId)
        {
            var input = new Dictionary<string, object>()
                {
                    { "vesselId", vesselId }
					//{"pageRequest",100 }
				};

            List<WorkBasketAllDetailViewModel> workBasketList = new List<WorkBasketAllDetailViewModel>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/PMSCertificates"));
            var response = await PostAsyncAutoPaged<WorkBasketDetailResponse>(requestUrl, input, 100);

            if (response != null && response.Any())
            {
                foreach (WorkBasketDetailResponse item in response)
                {
                    WorkBasketAllDetailViewModel workBasket = new WorkBasketAllDetailViewModel();
                    workBasket.IsCritical = item.IsCritical.HasValue ? item.IsCritical.Value : false;
                    workBasket.JobType = item.Type;
                    workBasket.ComponentName = item.ComponentName;
                    workBasket.Status = item.Status;
                    workBasket.Resp = item.ResponsibleRankShortCode ?? "";
                    workBasket.LeftHours = item.LeftHours;

                    workBasket.JobName = item.JobName;
                    workBasket.Interval = (item.Frequency ?? 0) + " " + item.FrequencyTypeShortCode;
                    workBasket.DueDate = item.DueDate;
                    workBasket.LastCompletedDate = item.LastCompletedDate;

                    workBasketList.Add(workBasket);
                }
            }
            return workBasketList;
        }

        /// <summary>
        /// Gets the requisition mapped orders.
        /// </summary>
        /// <param name="EncryptedVesselId">The encrypted vessel identifier.</param>
        /// <param name="vesselcertificateLogId">The vesselcertificate log identifier.</param>
        /// <returns></returns>
        public async Task<List<RequisitionOrdersViewModel>> GetRequisitionMappedOrders(string EncryptedVesselId, string vesselcertificateLogId)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(EncryptedVesselId);
            string VesselId = decreptedString.Split(Constants.Separator)[0];

            var input = new Dictionary<string, object>()
                {
                    {"vesselId", VesselId },
                    {"vesselcertificateLogId",vesselcertificateLogId }
                };

            List<RequisitionOrdersViewModel> requisitionOrderList = new List<RequisitionOrdersViewModel>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Certificate/LinkedOrders"));
            var response = await PostAsync<List<VesselOrderSearchResponse>>(requestUrl, CreateHttpContent(input));

            if (response != null && response.Any())
            {
                foreach (VesselOrderSearchResponse item in response)
                {
                    RequisitionOrdersViewModel requisitionOrder = new RequisitionOrdersViewModel();
                    PurchaseOrderRequestViewModel purchaseOrderUrl = new PurchaseOrderRequestViewModel();
                    purchaseOrderUrl.VesselId = decreptedString.Split(Constants.Separator)[0];
                    purchaseOrderUrl.VesselName = decreptedString.Split(Constants.Separator)[1];
                    purchaseOrderUrl.ToDate = null;
                    purchaseOrderUrl.FromDate = null;
                    purchaseOrderUrl.AccountCompanyId = item.CoyId;
                    purchaseOrderUrl.OrderNumber = item.OrderNumber;
                    purchaseOrderUrl.POStage = EnumsHelper.GetKeyValue(PoStagesFilter.InProcess);

                    requisitionOrder.OrderNumber = item.OrderNumber;
                    requisitionOrder.OrderName = item.OrderName;
                    requisitionOrder.CertificateStatus = item.CertificateStatus;
                    requisitionOrder.PurchaseOrderStatus = item.PurchaseOrderStatus;
                    requisitionOrder.RequestedDate = item.RequestedDate;
                    requisitionOrder.OrderDate = item.OrderDate;
                    requisitionOrder.ExpectedDeliveryPort = item.ExpectedDeliveryPort;
                    requisitionOrder.ExpectDeliveryDate = item.ExpectDeliveryDate;
                    requisitionOrder.OrderReceivedDate = item.OrderReceivedDate;
                    requisitionOrder.CoyId = item.CoyId;
                    requisitionOrder.PurchaseOrderPriority = item.PurchaseOrderPriority;
                    requisitionOrder.PurchaseOrderUrl = _provider.CreateProtector("PurchaseOrder").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(purchaseOrderUrl));
                    requisitionOrder.StatusDescription = EnumsHelper.GetEnumNameFromKeyValue(typeof(PurchaseOrderStatus), item.PurchaseOrderStatus);

                    if (item.PurchaseOrderStatus != null)
                    {
                        if (item.PurchaseOrderPriority != null && item.PurchaseOrderPriority == EnumsHelper.GetKeyValue(PurchaseOrderPriority.Urgent))
                        {
                            requisitionOrder.OrderStatusColor = Constants.OrderStatusColorCritical;
                        }
                        requisitionOrder.OrderStatusColor = Constants.OrderStatusColorGood;
                    }
                    else
                    {
                        requisitionOrder.OrderStatusColor = Constants.OrderStatusColorPreWarning;
                    }

                    requisitionOrder.IsStatusVisible = !string.IsNullOrWhiteSpace(item.PurchaseOrderStatus);
                    requisitionOrderList.Add(requisitionOrder);
                }
            }
            return requisitionOrderList;
        }

        /// <summary>
        /// Gets the work basket status tree list.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lookup>> GetWorkBasketStatusTreeList()
        {
            List<Lookup> response = new List<Lookup>(); //"PlannedMaintenance/WorkOrderStatus"
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WorkOrderStatus"));
            response = await GetAsync<List<Lookup>>(requestUrl);
            return response;
        }


        #region Dashboard - Methods

        /// <summary>
        /// Posts the get commercial summary.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns></returns>
        public async Task<CommercialSummaryResponseViewModel> GetCommercialSummary(string input)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input);
            string vesselId = decreptedString.Split(Constants.Separator)[0];

            CommercialSummaryResponseViewModel commercialSummary = new CommercialSummaryResponseViewModel();
            CommercialSummaryResponse response = new CommercialSummaryResponse();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/CommercialSummary/" + vesselId));
            response = await GetAsync<CommercialSummaryResponse>(requestUrl);

            if (response != null)
            {
                commercialSummary.UnplannedOffHireHrs = response.UnplannedOffHireHrs.Substring(0, response.UnplannedOffHireHrs.Length - 3);
                commercialSummary.FuelUnderPerformance = response.FuelUnderPerformance == null ? Constants.NotApplicable : string.Format(Constants.OneDecimal_NumberFormat, response.FuelUnderPerformance) + "%";
                commercialSummary.SpeedUnderPerformance = response.SpeedUnderPerformance == null ? Constants.NotApplicable : string.Format(Constants.OneDecimal_NumberFormat, response.SpeedUnderPerformance) + "%";

                //priorities
                commercialSummary.UnplannedOffHireHrsPriority = response.UnplannedOffHireHrsPriority;
                commercialSummary.FuelUnderPerformancePriority = response.FuelUnderPerformancePriority;
                commercialSummary.SpeedUnderPerformancePriority = response.SpeedUnderPerformancePriority;


            }
            return commercialSummary;
        }

        /// <summary>
        /// Posts the get inspection manager summary
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns></returns>
        public async Task<InspectionManagerSummaryResponseViewModel> GetInspectionManagerSummary(InspectionManagerDashboardRequestViewModel input)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input.EncryptedVesselId);
            string vesselId = decreptedString.Split(Constants.Separator)[0];
            string vesselName = decreptedString.Split(Constants.Separator)[1];

            InspectionManagerSummaryRequest summaryRequest = new InspectionManagerSummaryRequest();
            InspectionManagerSummaryResponseViewModel inspectionManagerSummary = new InspectionManagerSummaryResponseViewModel();
            InspectionManagerSummaryResponse response = new InspectionManagerSummaryResponse();

            summaryRequest.VesselId = vesselId;
            summaryRequest.PSCDetentionFromDate = input.PSCDetentionFromDate;
            summaryRequest.PSCDetentionToDate = input.PSCDetentionToDate;

            summaryRequest.DeficienciesPerOMVFromDate = input.FromDate;
            summaryRequest.DeficienciesPerOMVToDate = input.ToDate;

            summaryRequest.DeficienciesPerPSCFromDate = input.DeficienciesPerPSCFromDate;
            summaryRequest.DeficienciesPerPSCToDate = input.DeficienciesPerPSCToDate;

            summaryRequest.PscDeficiencyFromDate = input.PscDeficiencyFromDate;
            summaryRequest.PscDeficiencyToDate = input.PscDeficiencyToDate;

            summaryRequest.OmvRejectionFromDate = input.OmvRejectionFromDate;
            summaryRequest.OmvRejectionToDate = input.OmvRejectionToDate;

            summaryRequest.DeficienciesPerPscPriorityHighLimit = input.DeficienciesPerPscPriorityHighLimit;
            summaryRequest.DeficienciesPerPscPriorityMidLimit = input.DeficienciesPerPscPriorityMidLimit;
            summaryRequest.DeficienciesPerPscPriorityLowLimit = input.DeficienciesPerPscPriorityLowLimit;
            summaryRequest.DeficienciesPerOmvPriorityHighLimit = input.DeficienciesPerOmvPriorityHighLimit;
            summaryRequest.DeficienciesPerOmvPriorityMidLimit = input.DeficienciesPerOmvPriorityMidLimit;
            summaryRequest.DeficienciesPerOmvPriorityLowLimit = input.DeficienciesPerOmvPriorityLowLimit;
            summaryRequest.OverdueFindingsPriorityLimit = input.OverdueFindingsPriorityLimit;
            summaryRequest.OverdueInspectionsPriorityLimit = input.OverdueInspectionsPriorityLimit;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/PWASummary"));
            response = await PostAsync<InspectionManagerSummaryResponse>(requestUrl, CreateHttpContent(summaryRequest));

            if (response != null)
            {
                inspectionManagerSummary.VesselId = input.EncryptedVesselId;
                inspectionManagerSummary.VesselName = vesselName;

                //count
                inspectionManagerSummary.DeficienciesPerOMVRate = response.DeficienciesPerOMVRate == null ? "0" : string.Format(Constants.TwoDecimal_NumberFormat, response.DeficienciesPerOMVRate);
                inspectionManagerSummary.DeficienciesPerPSCRate = string.Format(Constants.TwoDecimal_NumberFormat, response.DeficienciesPerPSCRate);
                inspectionManagerSummary.OverdueFindingsCount = response.OverdueFindingsCount;
                inspectionManagerSummary.OverdueInspectionCount = response.OverdueInspectionCount;
                inspectionManagerSummary.PSCDetentionCount = response.PSCDetentionCount;
                inspectionManagerSummary.PSCDeficenCount = response.PscDeficiencyCount;
                inspectionManagerSummary.OMVRejCount = response.OmvRejectionCount == null ? "0" : response.OmvRejectionCount.ToString();

                //Priority
                inspectionManagerSummary.DeficienciesPerOMVPriority = response.DeficienciesPerOMVPriority;
                inspectionManagerSummary.DeficienciesPerPSCPriority = response.DeficienciesPerPSCPriority;
                inspectionManagerSummary.OverdueFindingsPriority = response.OverdueFindingsPriority;
                inspectionManagerSummary.OverdueInspectionPriority = response.OverdueInspectionPriority;
                inspectionManagerSummary.PSCDetentionPriority = response.PSCDetentionPriority;
                inspectionManagerSummary.PSCDeficenPriority = response.PscDeficiencyPriority;
                inspectionManagerSummary.OMVRejPriority = response.OmvRejectionPriority;

                //URL
                InspectionRequestViewModel inspectionURLRequest = new InspectionRequestViewModel();
                inspectionURLRequest.FromDate = input.FromDate;
                inspectionURLRequest.ToDate = input.ToDate;
                inspectionURLRequest.VesselId = vesselId;
                inspectionURLRequest.IsSummaryClicked = true;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.FindingsOverdueType);
                inspectionManagerSummary.OverdueFindingsUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.FindingsOverdueType);
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionOverdueType);
                inspectionManagerSummary.OverdueInspectionUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionOverdueType);

                inspectionURLRequest.FromDate = input.PSCDetentionFromDate;
                inspectionURLRequest.ToDate = input.PSCDetentionToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionPSCType);
                inspectionManagerSummary.PSCDetentionUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionPSCType);

                inspectionURLRequest.FromDate = input.PscDeficiencyFromDate;
                inspectionURLRequest.ToDate = input.PscDeficiencyToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.PSCDeficiencyType);
                inspectionManagerSummary.PSCDeficiencyUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.PSCDeficiencyType);

                inspectionURLRequest.FromDate = input.FromDate;
                inspectionURLRequest.ToDate = input.ToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.AllInspection);
                inspectionManagerSummary.AllInspectionUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.AllInspection);

                inspectionURLRequest.FromDate = input.OmvRejectionFromDate;
                inspectionURLRequest.ToDate = input.OmvRejectionToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.OMVRejectionType);
                inspectionManagerSummary.OMVRejectionUrl = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.OMVRejectionType);

                inspectionURLRequest.FromDate = input.FromDate;
                inspectionURLRequest.ToDate = input.ToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.OMVFindingsType);
                inspectionManagerSummary.DeficienciesPerOmvURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.OMVFindingsType);

                inspectionURLRequest.FromDate = input.DeficienciesPerPSCFromDate;
                inspectionURLRequest.ToDate = input.DeficienciesPerPSCToDate;
                inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.InspectionPscDeficiencyType);
                inspectionManagerSummary.DeficienciesPerPscURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.InspectionPscDeficiencyType);
            }
            return inspectionManagerSummary;
        }

        /// <summary>
        /// Gets the environment summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<EnvironmentSummaryResponseViewModel> GetEnvironmentSummary(EnvironmentSummaryRequestViewModel input)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input.VesselId);
            string vesselId = decreptedString.Split(Constants.Separator)[0];

            EnvironmentSummaryRequest summaryRequest = new EnvironmentSummaryRequest();
            EnvironmentSummaryResponseViewModel environmentSummary = new EnvironmentSummaryResponseViewModel();
            EnvironmentSummaryResponse response = new EnvironmentSummaryResponse();

            summaryRequest.VesselId = vesselId;
            summaryRequest.StartDate = input.StartDate;
            summaryRequest.EndDate = input.EndDate;
            summaryRequest.OilSpillStartDate = input.OilSpillStartDate;
            summaryRequest.OilSpillEndDate = input.OilSpillEndDate;
            summaryRequest.BilgeStartDate = input.BilgeStartDate;
            summaryRequest.BilgeEndDate = input.BilgeEndDate;
            summaryRequest.AEUtilisationStartDate = input.AEUtilisationStartDate;
            summaryRequest.AEUtilisationEndDate = input.AEUtilisationEndDate;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Environment/EnvironmentSummary"));
            response = await PostAsync<EnvironmentSummaryResponse>(requestUrl, CreateHttpContent(summaryRequest));

            if (response != null)
            {
                environmentSummary.EEOI = Math.Round(response.EEOI, 2).ToString();
                environmentSummary.OilBilgeRetention = Math.Round(response.OilBilgeRetention, 2).ToString();
                environmentSummary.AEUtilisation = Math.Round(response.AEUtilisation, 2).ToString();
                environmentSummary.AccidentalOilSpillsCount = response.AccidentalOilSpillsCount;
            }

            return environmentSummary;
        }

        /// <summary>
        /// Gets the inspection fleet summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<InspectionFleetSummaryResponseViewModel> GetInspectionFleetSummary(InspectionFleetSummaryRequestViewModel input)
        {
            InspectionFleetSummaryRequest request = new InspectionFleetSummaryRequest();
            InspectionFleetSummaryResponse response = new InspectionFleetSummaryResponse();
            InspectionFleetSummaryResponseViewModel fleetSummary = new InspectionFleetSummaryResponseViewModel();

            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;
            string decreptedString = CommonUtil.GetDecryptedVessel(_provider, input.VesselId);
            request.VesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;

            request.PSCDetentionFromDate = input.PSCDetentionFromDate;
            request.PSCDetentionToDate = input.PSCDetentionToDate;
            request.PSCDetentionPriorityLimit = input.PSCDetentionPriorityLimit;

            request.PSCDeficiencyFromDate = input.PSCDeficiencyFromDate;
            request.PSCDeficiencyToDate = input.PSCDeficiencyToDate;
            request.PSCDeficiencyPriorityLimit = input.PSCDeficiencyPriorityLimit;

            request.OMVFindingsFromdate = input.OMVFindingsFromdate;
            request.OMVFindingsToDate = input.OMVFindingsToDate;
            request.OMVFindingsPriorityLowLimit = input.OMVFindingsPriorityLowLimit;
            request.OMVFindingsPriorityHighLimit = input.OMVFindingsPriorityHighLimit;

            request.OverdueInspectionsPriorityLimit = input.OverdueInspectionsPriorityLimit;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/PWAInspectionDashboardFleetSummary"));
            response = await PostAsync<InspectionFleetSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                fleetSummary.PSCDetentionCount = response.PSCDetentionCount;
                fleetSummary.PSCDetentionPriority = response.PSCDetentionPriority;
                fleetSummary.PSCDetentionInfo = response.PSCDetentionInfo;

                fleetSummary.PSCDeficiencyCount = response.PSCDeficiencyCount;
                fleetSummary.PscDeficiencyRate = string.Format(Constants.TwoDecimal_NumberFormat, response.PscDeficiencyRate);
                fleetSummary.PscDeficiencyInspectionCount = response.PscDeficiencyInspectionCount;
                fleetSummary.PSCDeficiencyPriority = response.PSCDeficiencyPriority;
                fleetSummary.PscDeficiencyInfo = response.PscDeficiencyInfo;

                fleetSummary.OMVFindingsRate = string.Format(Constants.TwoDecimal_NumberFormat, response.OMVFindingsRate);
                fleetSummary.OMVFindingsPriority = response.OMVFindingsPriority;
                fleetSummary.OMVFindingsCount = response.OMVFindingsCount;
                fleetSummary.OMVInspectionsCount = response.OMVInspectionsCount;
                fleetSummary.OMVFindingsInfo = response.OMVFindingsInfo;

                fleetSummary.OverdueInspectionCount = response.OverdueInspectionCount;
                fleetSummary.OverdueInspectionPriority = response.OverdueInspectionPriority;
                fleetSummary.OverdueInspectionInfo = response.OverdueInspectionInfo;
            }
            return fleetSummary;
        }

        /// <summary>
        /// Gets the haz occ fleet summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<HazOccFleetSummaryResponseViewModel> GetHazOccFleetSummary(HazOccFleetSummaryRequestViewModel input)
        {
            HazOccFleetSummaryRequest request = new HazOccFleetSummaryRequest();
            HazOccFleetSummaryResponse response = new HazOccFleetSummaryResponse();
            HazOccFleetSummaryResponseViewModel fleetSummary = new HazOccFleetSummaryResponseViewModel();

            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;
            string decreptedString = CommonUtil.GetDecryptedVessel(_provider, input.VesselId);
            request.VesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;

            request.IncidentStartDate = input.IncidentStartDate;
            request.IncidentEndDate = input.IncidentEndDate;
            request.SeriousIncidentsPriority = input.SeriousIncidentsPriority;

            request.LtiFromDate = input.LtiFromDate;
            request.LtiToDate = input.LtiToDate;
            request.LtifPriority = input.LtifPriority;

            request.OilSpillFromDate = input.OilSpillFromDate;
            request.OilSpillToDate = input.OilSpillToDate;
            request.OilSpillPriorityLimit = input.OilSpillPriorityLimit;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/PWAHazOccDashboardFleetSummary"));
            response = await PostAsync<HazOccFleetSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                fleetSummary.SeriousIncidentsCount = response.SeriousIncidentsCount;
                fleetSummary.SeriousIncidentsPriority = response.SeriousIncidentsPriority;
                fleetSummary.SeriousIncidentsInfo = response.SeriousIncidentsInfo;

                fleetSummary.LtifCount = response.LtifCount;
                fleetSummary.LtifPriority = response.LtifPriority;
                fleetSummary.LtifInfo = response.LtifInfo;

                fleetSummary.OilSpillCount = response.OilSpillCount;
                fleetSummary.OilSpillPriority = response.OilSpillPriority;
                fleetSummary.OilSpillInfo = response.OilSpillInfo;
            }
            return fleetSummary;
        }

        /// <summary>
        /// Gets the commercial fleet summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<CommercialFleetSummaryResponseViewModel> GetCommercialFleetSummary(CommercialFleetSummaryRequestViewModel input)
        {
            CommercialFleetSummaryRequest request = new CommercialFleetSummaryRequest();
            CommercialFleetSummaryResponse response = new CommercialFleetSummaryResponse();
            CommercialFleetSummaryResponseViewModel fleetSummary = new CommercialFleetSummaryResponseViewModel();

            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;
            string decreptedString = CommonUtil.GetDecryptedVessel(_provider, input.VesselId);
            request.VesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;

            request.OffHireStartDate = input.OffHireStartDate;
            request.OffHireEndDate = input.OffHireEndDate;
            request.OffHirePriority = input.OffHirePriority;

            request.FuelEfficiencyFromDate = input.FuelEfficiencyFromDate;
            request.FuelEfficiencyToDate = input.FuelEfficiencyToDate;
            request.FuelEfficiencyPriorityHighLimit = input.FuelEfficiencyPriorityHighLimit;
            request.FuelEfficiencyPriorityLowLimit = input.FuelEfficiencyPriorityLowLimit;


            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Commercial/PWACommercialDashboardFleetSummary"));
            response = await PostAsync<CommercialFleetSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                fleetSummary.OffHireData = response.OffHireData;
                fleetSummary.OffHirePriority = response.OffHirePriority;
                fleetSummary.OffHireInfo = response.OffHireInfo;

                fleetSummary.FuelEfficiencyPercentage = response.FuelEfficiencyPercentage == null
                                                        ? Constants.NotApplicable
                                                        : string.Format(Constants.TwoDecimal_NumberFormat, response.FuelEfficiencyPercentage) + "%";
                fleetSummary.FuelEfficiencyPriority = response.FuelEfficiencyPriority;
                fleetSummary.FuelEfficiencyPercentageValue = response.FuelEfficiencyPercentage.GetValueOrDefault();
                fleetSummary.FuelEfficiencyInfo = response.FuelEfficiencyInfo;
            }

            return fleetSummary;
        }

        /// <summary>
        /// Gets the rightship fleet summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<RightshipFleetSummaryResponseViewModel> GetRightshipFleetSummary(RightshipFleetSummaryRequestViewModel input)
        {
            RightshipFleetSummaryRequest request = new RightshipFleetSummaryRequest();
            RightshipFleetSummaryResponse response = new RightshipFleetSummaryResponse();
            RightshipFleetSummaryResponseViewModel fleetSummary = new RightshipFleetSummaryResponseViewModel();

            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;
            string decreptedString = CommonUtil.GetDecryptedVessel(_provider, input.VesselId);
            request.VesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;

            request.RightShipPriority = input.RightShipPriority;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vessel/PWARightshipDashboardFleetSummary"));
            response = await PostAsync<RightshipFleetSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                fleetSummary.RightShipRate = string.Format(Constants.TwoDecimal_NumberFormat, response.RightShipRate);
                fleetSummary.RightShipPriority = response.RightShipPriority;
                fleetSummary.RightShipInfo = response.RightShipInfo;
            }
            return fleetSummary;
        }

        /// <summary>
        /// Gets the PMS fleet summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<PMSFleetSummaryResponseViewModel> GetPMSFleetSummary(PMSFleetSummaryRequestViewModel input)
        {
            PMSFleetSummaryRequest request = new PMSFleetSummaryRequest();
            PMSFleetSummaryResponse response = new PMSFleetSummaryResponse();
            PMSFleetSummaryResponseViewModel fleetSummary = new PMSFleetSummaryResponseViewModel();

            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;
            string decreptedString = CommonUtil.GetDecryptedVessel(_provider, input.VesselId);
            request.VesselId = !string.IsNullOrWhiteSpace(decreptedString) ? decreptedString.Split(Constants.Separator)[0] : string.Empty;

            request.CriticalPmspriority = input.CriticalPmspriority;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/PWAPMSDashboardFleetSummary"));
            response = await PostAsync<PMSFleetSummaryResponse>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                fleetSummary.CriticalPMSCount = response.CriticalPMSCount;
                fleetSummary.CriticalPMSPriority = response.CriticalPMSPriority;
                fleetSummary.CriticalPMSInfo = response.CriticalPMSInfo;
            }
            return fleetSummary;
        }

        #endregion

        /// <summary>
        /// Get the get vessel responsible ranks.
        /// </summary>
        /// <param name="input">input.</param>
        /// <returns></returns>
        public async Task<List<ResponsibleRankDetailResponseViewModel>> GetVesselResponsibleRanks(string input)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(input);
            string vesselId = decreptedString.Split(Constants.Separator)[0];

            List<ResponsibleRankDetailResponseViewModel> responsibleRankList = new List<ResponsibleRankDetailResponseViewModel>();
            List<ResponsibleRankDetail> response = new List<ResponsibleRankDetail>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/VesselResponsibleRanks/" + vesselId));
            response = await GetAsync<List<ResponsibleRankDetail>>(requestUrl);

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    ResponsibleRankDetailResponseViewModel responsibleRank = new ResponsibleRankDetailResponseViewModel();

                    responsibleRank.CrewRankId = item.CrewRankId;
                    responsibleRank.CrewRankShortCode = item.CrewRankShortCode;
                    responsibleRank.CrewRankDescription = item.CrewRankDescription;
                    responsibleRank.DepartmentId = item.DepartmentId;
                    responsibleRank.DepartmentName = item.DepartmentName;
                    responsibleRank.DepartmentShortCode = item.DepartmentShortCode;
                    responsibleRankList.Add(responsibleRank);
                }
            }
            return responsibleRankList;
        }

        /// <summary>
        /// Posts the get maintenance attributes
        /// </summary>
        /// <param name="lookupCodes">The lookup codes.</param>
        /// <returns></returns>
        public async Task<List<MaintenanceAttributeLookupViewModel>> GetMaintenanceAttributes(List<string> lookupCodes)
        {
            List<MaintenanceAttributeLookupViewModel> maintenanceAttrList = new List<MaintenanceAttributeLookupViewModel>();
            List<MaintenanceAttributeLookup> response = new List<MaintenanceAttributeLookup>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/MaintenanceAttributes"));
            response = await PostAsync<List<MaintenanceAttributeLookup>>(requestUrl, CreateHttpContent(lookupCodes));

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    MaintenanceAttributeLookupViewModel maintenanceAttr = new MaintenanceAttributeLookupViewModel();

                    maintenanceAttr.AttributeId = item.AttributeId;
                    maintenanceAttr.AttributeName = item.AttributeName;
                    maintenanceAttr.AttributeDescription = item.AttributeDescription;
                    maintenanceAttr.PathButtonImage = item.PathButtonImage;
                    maintenanceAttr.LookupCode = item.LookupCode;
                    maintenanceAttr.SortOrder = item.SortOrder;

                    maintenanceAttrList.Add(maintenanceAttr);
                }

                maintenanceAttrList.Add(new MaintenanceAttributeLookupViewModel()
                {
                    AttributeId = Constants.None,
                    AttributeName = Constants.WorkOrder,
                    AttributeDescription = Constants.WorkOrder
                });
            }
            return maintenanceAttrList;
        }

        /// <summary>
        /// Gets the type of the job.
        /// </summary>
        /// <param name="maintenanceType">Type of the maintenance.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> GetJobType(MaintenanceType? maintenanceType)
        {
            string queryString = maintenanceType.HasValue ? "maintenanceType=" + EnumsHelper.GetKeyValue(maintenanceType.Value) : null;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/GetJobType/"), queryString);
            List<Lookup> response = await GetAsync<List<Lookup>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the other filters.
        /// </summary>
        /// <returns></returns>
        public List<Lookup> GetPMSOtherFilters()
        {
            List<Lookup> result = new List<Lookup>();
            var list = Enum.GetValues(typeof(PMSOtherFilter)).Cast<PMSOtherFilter>().ToList();
            foreach (var item in list)
            {
                Lookup filter = new Lookup
                {
                    Identifier = EnumsHelper.GetKeyValue(item),
                    Description = EnumsHelper.GetEnumNameFromKeyValue(typeof(PMSOtherFilter), EnumsHelper.GetKeyValue(item))
                };
                result.Add(filter);
            }


            return result;
        }

        /// <summary>
        /// Gets the PMS vessel tree.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<ComponentCategoryTreeResponse>> GetPMSVesselTree(ComponentCategoryTreeRequest request)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/GetPMSVesselSysAreaComponentTree"));
            List<ComponentCategoryTreeResponse> response = await PostAsync<List<ComponentCategoryTreeResponse>>(requestUrl, CreateHttpContent(request));
            return response;
        }

        #region HazOcc

        /// <summary>
        /// Seriouses the incident details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<SeriousIncidentsViewModel>> SeriousIncidentDetails(SeriousIncidentsRequest request)
        {
            List<SeriousIncidentsViewModel> result = new List<SeriousIncidentsViewModel>();
            request.VesselId = GetVesselId(request.VesselId);
            request.IncidentEndDate = DateTime.Now.Date;
            request.IncidentStartDate = DateTime.Now.Date.AddMonths(Constants.SeriousIncidentFleetLevelNMonths);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/PWASeriousIncidents"));
            var response = await PostAsync<List<SeriousIncidentsResponse>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (SeriousIncidentsResponse item in response)
                {
                    HazOccDetailsViewModel hazOccDetails = new HazOccDetailsViewModel();
                    hazOccDetails.IncidentId = item.ImrId;
                    hazOccDetails.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);
                    hazOccDetails.ToDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                    hazOccDetails.FromDate = DateTime.Now.Date.AddMonths(Constants.SeriousIncidentFleetLevelNMonths);
                    string hazOccDetailsRequest = _provider.CreateProtector("HazOccDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(hazOccDetails));

                    SeriousIncidentsViewModel incident = new SeriousIncidentsViewModel();
                    incident.VesselName = item.VesselName;
                    incident.ShipRefNo = item.ShipRefNo;
                    incident.OccurranceDate = item.OccuranceDate;
                    incident.Category = item.Category;
                    incident.Classification = item.Classification;
                    incident.Status = item.Status;
                    incident.HazOccDetailsUrlData = hazOccDetailsRequest;
                    incident.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);
                    result.Add(incident);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the off hire kpi fleet summary details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<OffHireResponseViewModel>> GetOffHireKPIFleetSummaryDetails(OffHireRequest request)
        {
            List<OffHireResponseViewModel> result = new List<OffHireResponseViewModel>();
            request.VesselId = GetVesselId(request.VesselId);
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Commercial/PWAOffHire"));
            var response = await PostAsync<List<OffHireResponse>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (OffHireResponse item in response)
                {
                    //Navigation URL
                    VoyageReportingRequestViewModel seaPassageRequest = new VoyageReportingRequestViewModel();
                    VoyageReportingRequestViewModel portCallRequest = new VoyageReportingRequestViewModel();
                    if (item.IsSeaPassageEvent)
                    {
                        seaPassageRequest.PositionListId = item.PositionId;
                        seaPassageRequest.VesselId = item.VesselId;
                        seaPassageRequest.VesselName = item.VesselName;
                        seaPassageRequest.FromDate = DateTime.Now.AddMonths(-3);
                        seaPassageRequest.ToDate = DateTime.Now.Date;
                        seaPassageRequest.MenuType = UserMenuItemType.Vessel;
                        seaPassageRequest.IsVesselLoadedFlag = false;
                    }
                    else
                    {
                        portCallRequest.PositionListId = item.PositionId;
                        portCallRequest.VesselId = item.VesselId;
                        portCallRequest.VesselName = item.VesselName;
                        portCallRequest.FromDate = DateTime.Now.AddMonths(-3);
                        portCallRequest.ToDate = DateTime.Now.Date;
                        portCallRequest.MenuType = UserMenuItemType.Vessel;
                    }

                    OffHireResponseViewModel offHire = new OffHireResponseViewModel();
                    offHire.Comment = item.Comment;
                    offHire.DateFrom = item.DateFrom;
                    offHire.DateTo = item.DateTo;
                    offHire.DelayDuration = item.DelayDuration;
                    offHire.OffHireType = item.OffHireType;
                    offHire.PositionId = item.PositionId;
                    offHire.Reason = item.Reason;
                    offHire.VesselId = item.VesselId;
                    offHire.VesselName = item.VesselName;
                    offHire.IsSeaPassageEvent = item.IsSeaPassageEvent;
                    string SeaPassageURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(seaPassageRequest));
                    offHire.SeaPassageURL = SeaPassageURL;
                    string portCallURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(portCallRequest));
                    offHire.PortCallURL = portCallURL;
                    offHire.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);
                    result.Add(offHire);
                }
            }

            return result;
        }
        #endregion

        /// <summary>
        /// Maintenances the history summary.
        /// </summary>
        /// <param name="summaryRequest">The summary request.</param>
        /// <returns></returns>
        public async Task<ClosedWorkOrderHistorySummaryResponceViewModel> MaintenanceHistorySummary(ClosedWorkOrderHistorySummaryRequestViewModel summaryRequest)
        {
            ClosedWorkOrderHistorySummaryResponceViewModel result = new ClosedWorkOrderHistorySummaryResponceViewModel();

            ClosedWorkOrderHistorySummaryResponce response;
            ClosedWorkOrderHistorySummaryRequest request = new ClosedWorkOrderHistorySummaryRequest();

            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(summaryRequest.VesselId);
            request.VesselId = decreptedString.Split(Constants.Separator)[0];
            request.FromDate = summaryRequest.FromDate;
            request.ToDate = summaryRequest.ToDate;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/ClosedWorkOrderHistorySummary"));
            response = await PostAsync<ClosedWorkOrderHistorySummaryResponce>(requestUrl, CreateHttpContent(request));

            if (response != null)
            {
                //Count
                result.OverhaulCount = response.OverhaulCount;
                result.RescheduledCount = response.RescheduledCount;

                //Navigation 
                summaryRequest.GridSubTitle = Constants.PMSOverhaul;
                result.OverhaulURL = SetMaintenanceHistoryNavigationURL(summaryRequest, ClosedWorkOrderHistoryStage.OverhaulCount);
                summaryRequest.GridSubTitle = Constants.PMSRescheduled;
                result.RescheduledURL = SetMaintenanceHistoryNavigationURL(summaryRequest, ClosedWorkOrderHistoryStage.RescheduledCount);
            }

            return result;
        }

        /// <summary>
        /// Sets the maintenance history navigation URL.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <param name="stageName">Name of the stage.</param>
        /// <returns></returns>
        private string SetMaintenanceHistoryNavigationURL(ClosedWorkOrderHistorySummaryRequestViewModel inputRequest, ClosedWorkOrderHistoryStage stageName)
        {
            PlannedMaintenanceListViewModel input = new PlannedMaintenanceListViewModel();
            input.FromDate = inputRequest.FromDate;
            input.ToDate = inputRequest.ToDate;
            input.StageName = EnumsHelper.GetDescription(stageName);
            input.SelectedWBJobTypeIds = EnumsHelper.GetKeyValue(JobClassType.Overhaul);
            input.SelectedWBRescheduledIds = EnumsHelper.GetKeyValue(RescheduleType.Reschedule);
            input.GridSubTitle = inputRequest.GridSubTitle;

            string url = _provider.CreateProtector("PMSList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(input));
            return url;
        }

        /// <summary>
        /// Gets the vessel identifier.
        /// </summary>
        /// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
        /// <returns></returns>
        private string GetVesselId(string encryptedVesselDetail)
        {
            if (!string.IsNullOrWhiteSpace(encryptedVesselDetail))
            {
                string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselDetail);
                return decryptedString.Split(Constants.Separator)[0];
            }
            return null;
        }

        /// <summary>
        /// Gets the sea passage summary asynchronous.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <returns></returns>
        public async Task<SeaPassageSummary> GetSeaPassageSummary(string posId)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "VoyageReporting/SeaPassageSummary"));
            SeaPassageSummary response = await PostAsync<SeaPassageSummary>(requestUrl, CreateHttpContent(posId));
            return response;
        }

        /// <summary>
        /// Gets the sea passage summary asynchronous.
        /// </summary>
        /// <param name="workOrderRescheduleId">The position identifier.</param>
        /// <param name="rescheduleRequestTypeId">The reschedule request type identifier.</param>
        /// <returns></returns>
        public async Task<RescheduleWorkOrderDetailViewModel> GetWorkOrderRescheduleDetail(string workOrderRescheduleId, string rescheduleRequestTypeId)
        {
            RescheduleWorkOrderDetailViewModel RescheduleHistoryList = new RescheduleWorkOrderDetailViewModel();

            string urlRequest = "workOrderRescheduleId=" + workOrderRescheduleId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/WorkOrderRescheduleDetail"), urlRequest);
            RescheduleWorkOrderDetail result = await PostAsync<RescheduleWorkOrderDetail>(requestUrl, CreateHttpContent(workOrderRescheduleId));

            if (result != null)
            {
                if (!string.IsNullOrWhiteSpace(result.RequestedBy))
                {
                    RescheduleHistoryList.RequestedBy = result.RequestedBy;
                    RescheduleHistoryList.RequestedOn = result.RequestedOn.ToString(Constants.DateFormat);
                    RescheduleHistoryList.RequesterRoleDescription = result.RequesterComment;
                    RescheduleHistoryList.IsRequester = true;
                }

                if (!string.IsNullOrWhiteSpace(result.ApprovedBy)
                    && (string.IsNullOrWhiteSpace(rescheduleRequestTypeId) || rescheduleRequestTypeId == EnumsHelper.GetKeyValue(RescheduleType.Reschedule)))
                {
                    RescheduleHistoryList.IsApprovedRowVisible = true;
                    RescheduleHistoryList.ApprovedBy = result.ApprovedBy;
                    RescheduleHistoryList.ApproveOn = result.ApprovedOn.HasValue ? result.ApprovedOn.Value.ToString(Constants.DateFormat) : String.Empty;
                    RescheduleHistoryList.ApproverRoleDescription = result.ApproverComment;
                    RescheduleHistoryList.IsRequester = false;
                }
            }

            return RescheduleHistoryList;
        }



        /// <summary>
        /// Posts the get haz occ list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<HazOccPreviewResponseViewModel>> PostGetHazOccList(HazOccPreviewRequestViewModel request)
        {
            HazOccPreviewRequest requestObject = new HazOccPreviewRequest();
            requestObject.VesselId = GetVesselId(request.VesselId);
            requestObject.StartDate = request.StartDate;
            requestObject.EndDate = request.EndDate;
            requestObject.IncidentType = new List<string>();
            requestObject.IncidentStatus = new List<string>();
            requestObject.IncidentSeverity = new List<string>();

            if (request.IsSearchedClick)
            {
                requestObject.IncidentType = request.IncidentType != null ? request.IncidentType.Where(x => x != null).ToList() : null;
                requestObject.IncidentStatus = request.IncidentStatus != null ? request.IncidentStatus.Where(x => x != null && x != "deleted").ToList() : null;
                requestObject.IncidentSeverity = request.IncidentSeverity != null ? request.IncidentSeverity.Where(x => x != null).ToList() : null;
                requestObject.IncidentDeleted = request.IncidentStatus != null ? request.IncidentStatus.Where(x => x == "deleted").Any() : false;
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.CrewAccidents))
            {
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.CA));
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.Incidents))
            {
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccReportCodes.Incident));
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.NearMissSafetyObserve))
            {
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccReportCodes.NearMiss));
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccReportCodes.Observation));
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.PassengerAccidents))
            {
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.PA));
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.Fatality))
            {
                requestObject.ClassificationIds = new List<string> { EnumsHelper.GetKeyValue(HazOccClassCodes.FT) };
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.CA));
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.PA));
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.TA));
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.VerySerious))
            {
                requestObject.IncidentSeverity = new List<string> { EnumsHelper.GetKeyValue(HazOccSeverityStatus.VerySerious) };
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccReportCodes.Incident));
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.SeriousAccidents))
            {
                requestObject.IncidentSeverity = new List<string>
                                                {
                                                    EnumsHelper.GetKeyValue(HazOccSeverityStatus.Serious),
                                                    EnumsHelper.GetKeyValue(HazOccSeverityStatus.VerySerious)
                                                };
                requestObject.IncidentType = new List<string>
                                        {
                                            EnumsHelper.GetKeyValue(HazOccReportCodes.Accident)
                                        };
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.SeriousIncidents))
            {
                requestObject.IncidentSeverity = new List<string>
                                                {
                                                    EnumsHelper.GetKeyValue(HazOccSeverityStatus.Serious),
                                                    EnumsHelper.GetKeyValue(HazOccSeverityStatus.VerySerious)
                                                };
                requestObject.IncidentType = new List<string>
                                        {
                                            EnumsHelper.GetKeyValue(HazOccReportCodes.Incident)
                                        };
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.UAUCRate))
            {
                requestObject.ClassificationIds = new List<string>() { EnumsHelper.GetKeyValue(HazOccClassCodes.UA), EnumsHelper.GetKeyValue(HazOccClassCodes.UC) };
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.Total))
            {
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.CA));
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccReportCodes.Incident));
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccReportCodes.NearMiss));
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccReportCodes.Observation));
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.PA));
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.TA));
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.ILL));
            }

            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.LTI))
            {
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.CA));
                requestObject.ClassificationIds = new List<string> { EnumsHelper.GetKeyValue(HazOccClassCodes.FT),
                    EnumsHelper.GetKeyValue(HazOccClassCodes.PT),
                    EnumsHelper.GetKeyValue(HazOccClassCodes.PP),
                    EnumsHelper.GetKeyValue(HazOccClassCodes.LW) };
            }

            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.TRC))
            {
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.CA));
                requestObject.ClassificationIds = new List<string> { EnumsHelper.GetKeyValue(HazOccClassCodes.FT),
                    EnumsHelper.GetKeyValue(HazOccClassCodes.MT),
                    EnumsHelper.GetKeyValue(HazOccClassCodes.RC),
                    EnumsHelper.GetKeyValue(HazOccClassCodes.PT),
                    EnumsHelper.GetKeyValue(HazOccClassCodes.PP),
                    EnumsHelper.GetKeyValue(HazOccClassCodes.LW) };
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.ThirdPartyAccident))
            {
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.TA));
            }
            else if (request.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.Illness))
            {
                requestObject.IncidentType.Add(EnumsHelper.GetKeyValue(HazOccTypeCodes.ILL));
            }

            var value = new Dictionary<string, object>()
                {
                    { "request", requestObject }
                };
            List<HazOccPreviewResponseViewModel> results = new List<HazOccPreviewResponseViewModel>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/HazOccPreviewPaged"));
            var response = await PostAsyncAutoPaged<HazOccPreviewResponse>(requestUrl, value, 500);
            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    HazOccDetailsViewModel hazOccDetails = new HazOccDetailsViewModel();
                    hazOccDetails.IncidentId = item.Identifier;
                    hazOccDetails.EncryptedVesselId = request.VesselId;
                    hazOccDetails.IsRedirectFromHazOccList = true;
                    hazOccDetails.FromDate = request.StartDate;
                    hazOccDetails.ToDate = request.EndDate;
                    string hazOccDetailsRequest = _provider.CreateProtector("HazOccDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(hazOccDetails));

                    results.Add(new HazOccPreviewResponseViewModel
                    {
                        Category = item.Category,
                        Class = item.Class,
                        IncidentDate = item.IncidentDate,
                        ShipReferenceNumber = item.ShipReferenceNumber,
                        Status = item.Status,
                        Type = item.Type,
                        Severity = GetSeverity(item.TypeId, item.CategoryId, item),
                        StatusKPI = GetStatusColor(item.IsDeleted, item.StatusId),
                        HazOccDetailsUrlData = hazOccDetailsRequest,
                        Identifier = item.Identifier,
                        HasParent = item.HasParent,
                        HasChildReports = item.HasChildReports,
                        MappedDefectCount = item.MappedDefectCount.GetValueOrDefault()
                    });
                }
            }
            return results.OrderByDescending(x => x.IncidentDate).ToList();
        }

        /// <summary>
        /// Post Get Selected HazOcc By Incident Id
        /// </summary>
        /// <param name="incidentId">The incident identifier.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<HazOccDetailsViewModel> PostGetSelectedHazOccByIncidentId(string incidentId, string vesselId)
        {
            HazOccDetailsViewModel hazOccDetails = new HazOccDetailsViewModel();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/HazOccDetailById"), "incidentId=" + incidentId);
            HazOccPreviewResponse hazOccPreview = null;
            hazOccPreview = await GetAsync<HazOccPreviewResponse>(requestUrl);

            if (hazOccPreview != null)
            {

                string encryptedIdentifier = _provider.CreateProtector("HazOccIdentifier").Protect(hazOccPreview.Identifier);
                hazOccDetails.EncryptedIdentifier = encryptedIdentifier;
                hazOccDetails.IncidentId = hazOccPreview.Identifier;
                hazOccDetails.EncryptedVesselId = vesselId;
                hazOccDetails.VesselName = CommonUtil.GetVesselDisplayName(_provider, vesselId);
                hazOccDetails.ShipReferenceNumber = hazOccPreview.ShipReferenceNumber;
                hazOccDetails.Category = hazOccPreview.Category;
                hazOccDetails.CategoryId = hazOccPreview.CategoryId;
                hazOccDetails.Class = hazOccPreview.Class;
                hazOccDetails.TypeId = hazOccPreview.TypeId;
                hazOccDetails.Type = hazOccPreview.Type;
                hazOccDetails.Status = hazOccPreview.Status;
                hazOccDetails.ActualSeverityId = hazOccPreview.ActualSeverityId;
                hazOccDetails.VesselType = hazOccPreview.VesselType;
                hazOccDetails.VesselId = hazOccPreview.VesselId;
                hazOccDetails.IsIncident = hazOccPreview.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.Incident);
                hazOccDetails.IsAccident = hazOccPreview.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.Accident);
                hazOccDetails.IsObservation = hazOccPreview.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.Observation);
                hazOccDetails.IsNearMiss = hazOccPreview.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.NearMiss);
                hazOccDetails.IsIllness = hazOccPreview.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.Illness);

                if (hazOccDetails.IsIllness)
                {
                    if (hazOccPreview.CategoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.CI))
                    {
                        hazOccDetails.IsCrewIllness = true;
                    }
                    else if (hazOccPreview.CategoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.PI))
                    {
                        hazOccDetails.IsPassengerIllness = true;
                    }
                    else if (hazOccPreview.CategoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.TI))
                    {
                        hazOccDetails.IsThirdPartyIllness = true;
                    }
                }

                hazOccDetails.StatusKPI = (int?)GetStatusColor(hazOccPreview.IsDeleted, hazOccPreview.StatusId);

                bool isOriginalReport = hazOccPreview.StatusId == EnumsHelper.GetKeyValue(HazOccReportStatus.DR);
                bool isReportComplete = hazOccPreview.StatusId == EnumsHelper.GetKeyValue(HazOccReportStatus.Cl);
                bool isSeverityMinor = hazOccPreview.ActualSeverityId == EnumsHelper.GetKeyValue(HazOccReportSeverity.MN);
                bool isReportInReview = hazOccPreview.StatusId == EnumsHelper.GetKeyValue(HazOccReportStatus.IR);

                hazOccDetails.IsAwaitingCompletionLabelVisible = (IsReportIncidentOrAccident(hazOccPreview) || IsReportIllness(hazOccPreview)) && isReportInReview && !isSeverityMinor;
                hazOccDetails.ShowReopenComment = !isReportComplete && !string.IsNullOrWhiteSpace(hazOccPreview.ReopenAuthorisedBy);
                hazOccDetails.ReopenComments = string.IsNullOrWhiteSpace(hazOccPreview.ReopenComments) ? Constants.DashForEmpty : hazOccPreview.ReopenComments;
                hazOccDetails.ReopenAuthorisedBy = string.IsNullOrWhiteSpace(hazOccPreview.ReopenAuthorisedBy) ? Constants.DashForEmpty : hazOccPreview.ReopenAuthorisedBy;
                hazOccDetails.IsSubmissionCommentVisible = isOriginalReport && (IsReportIncidentOrAccident(hazOccPreview) || IsReportNearMissOrObservation(hazOccPreview) || IsReportIllness(hazOccPreview));

                bool isSeverityLow = hazOccPreview.ActualSeverityId == EnumsHelper.GetKeyValue(HazOccReportSeverity.LW);
                bool isSafeActOrCondition = hazOccPreview.CategoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.SA);
                hazOccDetails.SubmissionComment = isSeverityLow || isSafeActOrCondition ? Constants.CanBeSetToCompleteWithoutOfficeReview : Constants.NeedsToBeSubmittedForReviewAndCompletionInOffice;
            }

            return hazOccDetails;
        }

        /// <summary>
        /// Gets the hazocc dashboard details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<HazoccDashboardDetailViewModel> GetHazoccDashboardDetails(HazOccPreviewRequestViewModel request)
        {
            return await PostGetHazoccDashboardDetail(new HazoccDashboardRequestViewModel
            {
                VesselId = request.VesselId,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            });
        }

        /// <summary>
        /// Gets the severity.
        /// </summary>
        /// <param name="ReportTypeID">The report type identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private string GetSeverity(string ReportTypeID, string categoryId, HazOccPreviewResponse item)
        {
            string ParentReportType;
            bool IsCrewAccident = false;
            bool IsPassengerAccident = false;
            bool IsThirdPartyAccident = false;
            bool IsCrewIllness = false;
            bool IsPassengerIllness = false;
            bool IsThirdPartyIllness = false;
            bool IsNearMiss = false;
            bool IsIncident = false;
            bool EquipmentFailure = false;
            bool IsObservation = false;
            bool IsSafeObs = false;
            bool IsUnsafeObs = false;

            if (ReportTypeID == EnumsHelper.GetKeyValue(HazOccReportCodes.Accident))
            {
                ParentReportType = EnumsHelper.GetDescription(HazOccReportCodes.Accident);
                if (categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.CA))
                {
                    IsCrewAccident = true;
                }
                else if (categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.PA))
                {
                    IsPassengerAccident = true;
                }
                else if (categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.TA))
                {
                    IsThirdPartyAccident = true;
                }
            }
            else if (ReportTypeID == EnumsHelper.GetKeyValue(HazOccReportCodes.Illness))
            {
                ParentReportType = EnumsHelper.GetDescription(HazOccReportCodes.Illness);
                if (categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.CI))
                {
                    IsCrewIllness = true;
                }
                else if (categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.PI))
                {
                    IsPassengerIllness = true;
                }
                else if (categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.TI))
                {
                    IsThirdPartyIllness = true;
                }
            }
            else if (ReportTypeID == EnumsHelper.GetKeyValue(HazOccReportCodes.NearMiss))
            {
                IsNearMiss = true;
                ParentReportType = EnumsHelper.GetDescription(HazOccReportCodes.NearMiss);
            }
            else if (ReportTypeID == EnumsHelper.GetKeyValue(HazOccReportCodes.Incident))
            {
                IsIncident = true;
                ParentReportType = EnumsHelper.GetDescription(HazOccReportCodes.Incident);
                EquipmentFailure = (categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.IE));
            }
            else if (ReportTypeID == EnumsHelper.GetKeyValue(HazOccReportCodes.Observation))
            {
                ParentReportType = EnumsHelper.GetDescription(HazOccTypeCodes.SO);
                IsObservation = true;
                IsSafeObs = (categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.SA));
                IsUnsafeObs = (categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.OB));
            }

            bool IsAccident = (IsCrewAccident || IsPassengerAccident || IsThirdPartyAccident);
            bool IsAccidentIncident = (IsIncident || IsAccident);
            bool IsObservationOrNearMs = (IsObservation || IsNearMiss);
            bool IsIllnessReport = (IsCrewIllness || IsPassengerIllness || IsThirdPartyIllness);

            if (IsAccidentIncident || IsIllnessReport)
            {
                return item.ActualSeverity ?? "NA";
            }
            else if (IsObservationOrNearMs)
            {
                return IsSafeObs ? "NA" : item.PotentialSeverity ?? "NA";
            }
            else
            {
                return "NA";
            }
        }

        /// <summary>
        /// Gets the color of the status.
        /// </summary>
        /// <param name="isDeleted">if set to <c>true</c> [is deleted].</param>
        /// <param name="statusId">The status identifier.</param>
        /// <returns></returns>
        private KPI? GetStatusColor(bool isDeleted, string statusId)
        {
            if (isDeleted)
            {
                return KPI.Warning;
            }
            else if (statusId == EnumsHelper.GetKeyValue<HazOccReportStatus>(HazOccReportStatus.Cl))
            {
                return KPI.Better;
            }
            else if (statusId == EnumsHelper.GetKeyValue<HazOccReportStatus>(HazOccReportStatus.IR))
            {
                return KPI.Good;

            }
            else if (statusId == EnumsHelper.GetKeyValue<HazOccReportStatus>(HazOccReportStatus.DR))
            {
                return KPI.Normal;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the hazocc status filter.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lookup>> GetHazoccStatusFilter()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IncidentStatusLookups/"));
            List<Lookup> response = await GetAsync<List<Lookup>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the hazocc type filter.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Lookup>> GetHazoccTypeFilter()
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IncidentTypeLookups/"));
            List<Lookup> response = await GetAsync<List<Lookup>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the hazocc severity filter.
        /// </summary>
        /// <param name="isObservation">The is observation.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> GetHazoccSeverityFilter(bool? isObservation = null)
        {
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IncidentSeverityLookups/" + isObservation));
            List<Lookup> response = await GetAsync<List<Lookup>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the vessel specific attributes.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        public async Task<List<VesselSpecificAttribute>> GetVesselSpecificAttributes(string encryptedVesselId, List<VesselSpecificAttributeType> attributes)
        {

            VesselSpecificAttributesRequest request = new VesselSpecificAttributesRequest
            {
                VesselId = GetVesselId(encryptedVesselId),
                Permission = attributes
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/VesselSpecificAttributes/"));
            List<VesselSpecificAttribute> response = await PostAsync<List<VesselSpecificAttribute>>(requestUrl, CreateHttpContent(request));

            return response;
        }




        /// <summary>
        /// Criticals the PMS details.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<CriticalPMSOverdueResponseViewModel>> CriticalPMSDetails(CriticalPMSOverdueRequest request)
        {
            List<CriticalPMSOverdueResponseViewModel> result = new List<CriticalPMSOverdueResponseViewModel>();
            request.VesselId = GetVesselId(request.VesselId);
            var value = new Dictionary<string, object>()
            {
                { "request", request }
            };

            List<CriticalPMSOverdueResponse> response = null;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/PWACriticalPMSDetails"));
            response = await PostAsyncAutoPaged<CriticalPMSOverdueResponse>(requestUrl, value, 200);

            if (response != null && response.Any())
            {
                foreach (CriticalPMSOverdueResponse item in response)
                {
                    //naviagtion to details
                    PlannedMaintenanceRequestViewModel plannedMaintenanceRequestUrl = new PlannedMaintenanceRequestViewModel();
                    plannedMaintenanceRequestUrl.FromDate = DateTime.Now.Date;
                    plannedMaintenanceRequestUrl.ToDate = DateTime.Now.Date;
                    plannedMaintenanceRequestUrl.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);
                    plannedMaintenanceRequestUrl.StageName = EnumsHelper.GetDescription(PMSDashboardStage.CriticalOverdue);
                    plannedMaintenanceRequestUrl.ComponentId = item.ComponentId;
                    plannedMaintenanceRequestUrl.WorkOrderId = item.PWOId;
                    plannedMaintenanceRequestUrl.ScheduleTaskId = item.ScheculeTaskId;
                    plannedMaintenanceRequestUrl.IsNavigatedFromDone = false;

                    string plannedMaintenanceDetailsRequest = _provider.CreateProtector("PMSDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(plannedMaintenanceRequestUrl));

                    CriticalPMSOverdueResponseViewModel criticalPMS = new CriticalPMSOverdueResponseViewModel();
                    criticalPMS.ComponentName = item.ComponentName ?? "";
                    criticalPMS.DueDate = item.DueDate;
                    criticalPMS.JobName = item.JobName ?? "";
                    criticalPMS.VesselName = item.VesselName ?? "";
                    criticalPMS.PlannedMaintenanceDetailsRequestURL = plannedMaintenanceDetailsRequest;
                    criticalPMS.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);
                    result.Add(criticalPMS);
                }
            }

            return result;
        }

        /// <summary>
        /// Overdues the inspection details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<OverdueInspectionResponseViewModel>> OverdueInspectionDetails(OverdueInspectionRequestViewModel input)
        {
            List<OverdueInspectionResponseViewModel> result = new List<OverdueInspectionResponseViewModel>();
            OverdueInspectionRequest request = new OverdueInspectionRequest();
            if (input != null)
            {
                request.FleetId = input.FleetId;
                request.MenuType = input.MenuType;
                request.VesselIds = GetVesselId(input.EncryptedVesselId);
            }

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/PWAOverdueInspectionDetails"));
            List<OverdueInspectionResponse> response = await PostAsync<List<OverdueInspectionResponse>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (OverdueInspectionResponse item in response)
                {
                    OverdueInspectionResponseViewModel overdueInspection = new OverdueInspectionResponseViewModel();
                    overdueInspection.EncryptedVesselId = GetEncryptedVessel(item.VesselId, item.VesselName, item.CoyId);
                    overdueInspection.VesselName = item.VesselName;
                    overdueInspection.InspectionType = item.InspectionTypeDesc;
                    overdueInspection.LastDoneDate = item.DoneDate;
                    overdueInspection.NextDueDate = item.NextDueDate;

                    InspectionRequestViewModel inspectionURLRequest = new InspectionRequestViewModel();
                    inspectionURLRequest.FromDate = DateTime.Now.Date.AddYears(Constants.OverdueInspectionDefaultYearPeriod);
                    inspectionURLRequest.ToDate = DateTime.Now.Date;
                    inspectionURLRequest.VesselId = item.VesselId;
                    inspectionURLRequest.IsSummaryClicked = true;
                    inspectionURLRequest.GridSubTitle = EnumsHelper.GetDescription(InspectionDashboardType.VesselInspectionReport);
                    overdueInspection.EncryptedInspectionURL = SetInspectionURL(inspectionURLRequest, InspectionDashboardType.VesselInspectionReport);

                    result.Add(overdueInspection);
                }
            }

            return result;
        }

        /// <summary>
        /// Oils the spill to water details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<OilSpillWaterResponseViewModel>> OilSpillToWaterDetails(OilSpillWaterRequestViewModel input)
        {
            List<OilSpillWaterResponseViewModel> result = new List<OilSpillWaterResponseViewModel>();

            OilSpillWaterRequest request = new OilSpillWaterRequest();
            if (input != null)
            {
                string decryptedVesselId = GetVesselId(input.VesselId);
                request.VesselIds = !string.IsNullOrWhiteSpace(decryptedVesselId) ? new List<string>() { decryptedVesselId } : null;
                request.OilSpillToDate = DateTime.Now.Date;
                request.OilSpillFromDate = DateTime.Now.Date.AddMonths(Constants.OilSpillsToWaterFleetLevelNMonths);
                request.FleetId = input.FleetId;
                request.MenuType = input.MenuType;
            }

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/PWAOilSpillWater"));
            List<OilSpillWaterResponse> response = await PostAsync<List<OilSpillWaterResponse>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (OilSpillWaterResponse item in response)
                {
                    HazOccDetailsViewModel hazOccDetails = new HazOccDetailsViewModel();
                    hazOccDetails.IncidentId = item.ImrId;
                    hazOccDetails.EncryptedVesselId = CommonUtil.GetEncryptedVessel(_provider, item.VesselId, item.VesselName, item.CoyId);
                    hazOccDetails.ToDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                    hazOccDetails.FromDate = DateTime.Now.Date.AddMonths(Constants.OilSpillsToWaterFleetLevelNMonths);
                    string hazOccDetailsRequest = CommonUtil.GetEncryptedURL<HazOccDetailsViewModel>(_provider, Constants.HazOccDetailsEncryptionText, hazOccDetails);

                    OilSpillWaterResponseViewModel oilSpillWaterResponseVM = new OilSpillWaterResponseViewModel();
                    oilSpillWaterResponseVM.VesselName = item.VesselName ?? "";
                    oilSpillWaterResponseVM.IncidentDate = item.IMRIncDate;
                    oilSpillWaterResponseVM.IncidentType = item.IncType ?? "";
                    oilSpillWaterResponseVM.Severity = item.Severity ?? "";
                    oilSpillWaterResponseVM.LocOfVessel = item.LocationOfVessel ?? "";
                    oilSpillWaterResponseVM.QuantitySpilled = item.ImrOilLtrsSea;
                    oilSpillWaterResponseVM.ShipRefNo = item.IMRShipRefNo ?? "";
                    oilSpillWaterResponseVM.EncryptedVesselId = CommonUtil.GetEncryptedVessel(_provider, item.VesselId, item.VesselName, item.CoyId);
                    oilSpillWaterResponseVM.HazOccRequestURL = hazOccDetailsRequest;
                    result.Add(oilSpillWaterResponseVM);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the right ship details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<RightShipResponseViewModel>> GetRightShipDetails(RightShipRequestViewModel input)
        {
            RightShipRequest request = new RightShipRequest();
            List<RightShipResponseViewModel> result = new List<RightShipResponseViewModel>();

            string requestVesselId = GetVesselId(input.VesselId);
            request.VesselIds = !string.IsNullOrWhiteSpace(requestVesselId) ? new List<string>() { requestVesselId } : null;
            request.FleetId = input.FleetId;
            request.MenuType = input.MenuType;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vessel/PWARightShipDetails"));
            List<RightShipResponce> response = await PostAsync<List<RightShipResponce>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (RightShipResponce item in response)
                {
                    RightShipResponseViewModel rightShip = new RightShipResponseViewModel();
                    rightShip.VesselName = item.VesselName;
                    rightShip.RightShipScore = item.RightShipScore;
                    rightShip.GHGRating = item.GHGRating;
                    result.Add(rightShip);
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get haz occ accident summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<AccidentSummaryViewModel> PostGetHazOccAccidentSummary(HazOccDetailRequestViewModel request)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(request.EncryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/AccidentSummary"), urlRequest);
            AccidentSummary response = await PostAsync<AccidentSummary>(requestUrl, CreateHttpContent(incidentId));
            AccidentSummaryViewModel result = new AccidentSummaryViewModel();

            if (response != null)
            {
                LookUp location = response.IsAshore ? await GetAshoreLocations(request.CategoryId, response.VesselLocationId) : await GetOnBoardLocations(request.CategoryId, response.VesselLocationId);
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.ReportDate = response.ReportDate.HasValue ? response.ReportDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.AccidentDateTime = response.AccidentDateTime.HasValue ? response.AccidentDateTime.GetValueOrDefault().ToString(Constants.DateTime24HrFormat) : Constants.DashForEmpty;
                result.UpdatedDate = response.UpdatedDate.HasValue ? response.UpdatedDate.GetValueOrDefault().ToString(Constants.DayDateFormat) : Constants.DashForEmpty;
                result.CreatedBy = response.CreatedBy;
                result.SafetyOfficer = response.SafetyOfficer;
                result.ParentReportId = response.ParentReportId;
                result.MasterName = response.MasterName;
                result.ClassificationId = response.ClassificationId;
                result.Classification = response.Classification;
                result.PotentialSeverityId = response.PotentialSeverityId;
                result.ActualSeverityId = response.ActualSeverityId;
                result.ActualSeverity = response.ActualSeverity;

                result.ShipLocationLabel = response.IsAshore ? Constants.LocationAshore : Constants.LocationOnboard;
                result.ShipLocation = await GetShipLocation(request.VesselType, response.ShipLocationId, response.IsAshore);
                result.VesselLocation = location.Description;
                result.Manoeuvring = await GetManoeuvringOptionsLookup(response.ManoeuvringId);
                result.PortId = response.PortId;
                result.PortName = !string.IsNullOrWhiteSpace(response.CountryId) && !string.IsNullOrWhiteSpace(response.PortName) ? response.CountryId + " - " + response.PortName : "-";
                result.CountryId = response.CountryId;
                result.DockName = response.DockName;
                result.LocationName = response.LocationName;
                result.LatitudeDegrees = CreateLatitude(response.LatitudeDegrees, response.LatitudeMin, response.LatitudeInd);
                result.LongDegrees = CreateLongitude(response.LongDegrees, response.LongMin, response.LongInd);
                result.Light = response.IsDayLight ? Constants.DayLight : Constants.Night;
                result.Where = response.IsAshore ? Constants.Ashore : Constants.OnBoard;
                result.Description = response.Description;
                result.InvestigateName = response.InvestigateName;
                result.InvestigateRankName = await GetCrewRanks(response.InvestigateRank, response.InvestigateRankName, response.VesselId);
                result.InvestigateDate = response.InvestigateDate.HasValue ? response.InvestigateDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.LogBookDate = response.LogBookDate.HasValue ? response.LogBookDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.IsAccidentWorkRelated = response.IsAccidentWorkRelated == true ? Constants.Yes : Constants.No;
                result.PaxNumber = response.PaxNumber;
                result.CruiseNo = response.CruiseNo;
                result.CruiseId = response.CruiseId;
                result.ClosedDate = response.ClosedDate.HasValue ? response.ClosedDate.GetValueOrDefault().ToString(Constants.DateFormat) : null;
                result.MsqComments = response.MsqComments;
                result.FleetManagerComments = response.FleetManagerComments;
                result.ReportTypeId = response.ReportTypeId;
                result.MsqCommentDate = response.MsqCommentDate.HasValue ? response.MsqCommentDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.FleetManagerCommentDate = response.FleetManagerCommentDate.HasValue ? response.FleetManagerCommentDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.IsPassengerAccident = request.CategoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.PA);
                result.OtherShipLocation = response.OtherShipLocation;

                result.ShowOtherShipLocation = result.ShipLocation != Constants.DashForEmpty
                            && (response.ShipLocationId == EnumsHelper.GetKeyValue(HazOccStatCodes.LO)
                            || response.ShipLocationId == EnumsHelper.GetKeyValue(HazOccStatCodes.SHOT));

                if (!result.ShowOtherShipLocation && !string.IsNullOrWhiteSpace(result.OtherShipLocation))
                {
                    result.OtherShipLocation = null;
                }

                result.IsClosureCommentVisible = response.ActualSeverityId == EnumsHelper.GetKeyValue(HazOccReportSeverity.MN);
                result.IsFleetManagerCommentsVisible = result.IsHSEQManagerCommentsVisible = response.ActualSeverityId != EnumsHelper.GetKeyValue(HazOccReportSeverity.MN);
                result.ClosureComments = !string.IsNullOrWhiteSpace(response.MsqComments) ? response.MsqComments : response.FleetManagerComments;

                HazOccSummaryLocationViewModel locSummary = ShowHideHazOccSummaryLocationFields(location.Identifier);
                result.ShowLocationName = locSummary.ShowLocationName;
                result.ShowLongLat = locSummary.ShowLongLat;
                result.ShowManeuvering = locSummary.ShowManeuvering;
                result.LocationLookupLabel = locSummary.LocationLookupLabel;
                result.ShowPort = locSummary.ShowPort;
            }
            return result;
        }

        /// <summary>
        /// Posts the get haz occ observation summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<ObservationSummaryViewModel> PostGetHazOccObservationSummary(HazOccDetailRequestViewModel request)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(request.EncryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/ObservationSummary"), urlRequest);
            ObservationSummary response = await PostAsync<ObservationSummary>(requestUrl, CreateHttpContent(incidentId));
            ObservationSummaryViewModel result = new ObservationSummaryViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.ReportDate = response.ReportDate.HasValue ? response.ReportDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.UpdatedDate = response.UpdatedDate.HasValue ? response.UpdatedDate.GetValueOrDefault().ToString(Constants.DayDateFormat) : Constants.DashForEmpty;
                result.CreatedBy = response.CreatedBy;
                result.ObservationDateTime = response.ObservationDateTime.HasValue ? response.ObservationDateTime.GetValueOrDefault().ToString(Constants.DateTime24HrFormat) : Constants.DashForEmpty;
                result.SafetyOfficer = response.SafetyOfficer;
                result.ParentReportId = response.ParentReportId;
                result.MasterName = response.MasterName;
                result.ShipRefNo = response.ShipRefNo;
                result.PotentialSeverityId = response.PotentialSeverityId;
                result.PotentialSeverity = response.PotentialSeverity;
                result.RankId = response.RankId;
                result.Rank = await GetCrewRanks(response.RankId, response.Rank, response.VesselId);
                result.ObservationRaisedBy = response.ObservationRaisedBy;
                result.ObservationRaisedByCrewIdTp = response.ObservationRaisedByCrewIdTp;
                result.ObservationRaisedByName = response.ObservationRaisedByName;
                result.Description = response.Description;
                result.ShipOperation = await GetOperations(response.ShipOperationId);
                result.PossibleConsequence = await GetConsequences(response.PossibleConsequenceId);
                result.ImmediateActionTaken = response.ImmediateActionTaken;
                result.ActId = response.ActId;
                result.Comments = response.Comments;
                result.HasWorkStopped = Convert.ToBoolean(response.HasWorkStopped) ? Constants.Yes : Constants.No; ;
                result.ClosedDate = response.ClosedDate.HasValue ? response.ClosedDate.GetValueOrDefault().ToString(Constants.DateFormat) : null;
                result.MsqComments = response.MsqComments;
                result.FleetManagerComments = response.FleetManagerComments;
                result.ReportTypeId = response.ReportTypeId;
                result.ParentReportType = EnumsHelper.GetDescription(HazOccTypeCodes.SO);
                result.IsSafeObs = (request.CategoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.SA));
                result.IsUnsafeObs = (request.CategoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.OB));
                result.Classification = response.Classification;
                result.ActsLabelValue = await GetActsDescription(response.ActId, response.ClassificationId);

                result.IsClosureCommentVisible = !string.IsNullOrWhiteSpace(response.MsqComments) || !string.IsNullOrWhiteSpace(response.FleetManagerComments);
                if (result.IsClosureCommentVisible)
                {
                    result.ClosureComments = !string.IsNullOrWhiteSpace(response.MsqComments) ? response.MsqComments : response.FleetManagerComments;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the haz occ incident summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<HazOccIncidentSummaryViewModel> PostGetHazOccIncidentSummary(HazOccDetailRequestViewModel request)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(request.EncryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IncidentSummary"), urlRequest);
            HazOccIncidentSummary response = await PostAsync<HazOccIncidentSummary>(requestUrl, CreateHttpContent(incidentId));
            HazOccIncidentSummaryViewModel result = new HazOccIncidentSummaryViewModel();

            if (response != null)
            {
                LookUp location = response.IsAshore ? await GetAshoreLocations(request.CategoryId, response.VesselLocationId) : await GetOnBoardLocations(request.CategoryId, response.VesselLocationId);
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.ReportDate = response.ReportDate.HasValue ? response.ReportDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.UpdatedDate = response.UpdatedDate.HasValue ? response.UpdatedDate.GetValueOrDefault().ToString(Constants.DayDateFormat) : Constants.DashForEmpty;
                result.CreatedBy = response.CreatedBy;
                result.IncidentDateTime = response.IncidentDateTime.HasValue ? response.IncidentDateTime.GetValueOrDefault().ToString(Constants.DateTime24HrFormat) : Constants.DashForEmpty;
                result.SafetyOfficer = string.IsNullOrWhiteSpace(response.SafetyOfficer) ? Constants.DashForEmpty : response.SafetyOfficer;
                result.ParentReportId = response.ParentReportId;
                result.ClassificationId = response.ClassificationId;
                result.Classification = response.Classification;
                result.ClassificationDescription = response.ClassificationDescription;
                result.MasterName = response.MasterName;
                result.ShipRefNo = response.ShipRefNo;
                result.PotentialSeverityId = response.PotentialSeverityId;
                result.ActualSeverityId = response.ActualSeverityId;
                result.ActualSeverity = response.ActualSeverity;
                result.ShipLocationLabel = response.IsAshore ? Constants.LocationAshore : Constants.LocationOnboard;
                result.ShipLocation = await GetShipLocation(request.VesselType, response.ShipLocationId, response.IsAshore);
                result.OtherShipLocation = !string.IsNullOrWhiteSpace(response.OtherShipLocation) ? response.OtherShipLocation : Constants.DashForEmpty;
                result.VesselLocation = !string.IsNullOrWhiteSpace(location.Description) ? location.Description : Constants.DashForEmpty;
                result.Manoeuvring = await GetManoeuvringOptionsLookup(response.ManoeuvringId);
                result.PortId = response.PortId;
                result.PortName = response.CountryId != null && response.PortName != null ? response.CountryId + " - " + response.PortName : Constants.DashForEmpty;
                result.CountryId = response.CountryId;
                result.DockName = !string.IsNullOrWhiteSpace(response.DockName) ? response.DockName : Constants.DashForEmpty;
                result.LocationName = response.LocationName ?? Constants.DashForEmpty;
                result.LatitudeDegrees = CreateLongitude(response.LatitudeDegrees, response.LatitudeMin, response.LatitudeInd);
                result.LongDegrees = CreateLongitude(response.LongDegrees, response.LongMin, response.LongInd);
                result.Light = response.IsDayLight == true ? Constants.DayLight : Constants.Night;
                result.Where = response.IsAshore == true ? Constants.Ashore : Constants.OnBoard;
                result.Description = response.Description;
                result.EquipmentType = await GetEquipmentTypeLookup(response.EquipmentType);
                result.ClosedDate = response.ClosedDate.HasValue ? response.ClosedDate.GetValueOrDefault().ToString(Constants.DateFormat) : null;
                result.MsqComments = response.MsqComments;
                result.FleetManagerComments = response.FleetManagerComments;
                result.ReportTypeId = response.ReportTypeId;
                result.MsqCommentDate = response.MsqCommentDate.HasValue ? response.MsqCommentDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.FleetManagerCommentDate = response.FleetManagerCommentDate.HasValue ? response.FleetManagerCommentDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;

                result.ShowCollisionStationary = (response.ReportTypeId == EnumsHelper.GetKeyValue(HazOccTypeCodes.IC));
                result.CollisionStationary = response.CollisionStationary ? Constants.Yes : Constants.No;

                if (response.IsEquipmentFailure == null && Convert.ToBoolean(response.ImdEquipRequired))
                {
                    response.IsEquipmentFailure = true;
                }
                if (response.IsEquipmentFailure != null)
                {
                    result.IsEquipmentFailure = Convert.ToBoolean(response.IsEquipmentFailure) ? Constants.Yes : Constants.No;
                }

                result.IsClosureCommentVisible = response.ActualSeverityId == EnumsHelper.GetKeyValue(HazOccReportSeverity.MN);
                result.IsFleetManagerCommentsVisible = result.IsHSEQManagerCommentsVisible = response.ActualSeverityId != EnumsHelper.GetKeyValue(HazOccReportSeverity.MN);
                result.ClosureComments = !string.IsNullOrWhiteSpace(response.MsqComments) ? response.MsqComments : response.FleetManagerComments;
                result.ShowOtherShipLocation = result.ShipLocation != Constants.DashForEmpty
                            && (response.ShipLocationId == EnumsHelper.GetKeyValue(HazOccStatCodes.LO)
                            || response.ShipLocationId == EnumsHelper.GetKeyValue(HazOccStatCodes.SHOT));

                if (!result.ShowOtherShipLocation && !string.IsNullOrWhiteSpace(result.OtherShipLocation))
                {
                    result.OtherShipLocation = null;
                }

                HazOccSummaryLocationViewModel locSummary = ShowHideHazOccSummaryLocationFields(location.Identifier);
                result.ShowLocationName = locSummary.ShowLocationName;
                result.ShowLongLat = locSummary.ShowLongLat;
                result.ShowManeuvering = locSummary.ShowManeuvering;
                result.LocationLookupLabel = locSummary.LocationLookupLabel;
                result.ShowPort = locSummary.ShowPort;
            }
            return result;
        }

        /// <summary>
        /// Posts the get last twelve month summary.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<IncidentMonthSummaryViewModel>> PostGetLastTwelveMonthSummary(GetLastTwelveMonthSummaryRequest input)
        {
            List<IncidentMonthSummaryViewModel> result = new List<IncidentMonthSummaryViewModel>();
            if (input != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("vesselIds", input.VesselIds);
                dict.Add("reportTypeId", input.ReportTypeId);
                dict.Add("months", input.Months);

                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/LastTwelveMonthSummary"));
                List<IncidentMonthSummary> response = await PostAsync<List<IncidentMonthSummary>>(requestUrl, CreateHttpContent(dict));

                if (response != null && response.Any())
                {
                    foreach (IncidentMonthSummary item in response)
                    {
                        result.Add(new IncidentMonthSummaryViewModel
                        {
                            MaxValue = item.MaxValue,
                            MonthTotal = item.MonthTotal,
                            ReportTypeId = item.ReportTypeId,
                            VesselId = item.VesselId
                        });
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the haz occ defect details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<HazOccDefectDetailsViewModel>> PostGetHazOccDefectDetails(string encryptedIncidentId, string encryptedVesselId)
        {
            string hazOccId = null;
            if (!string.IsNullOrWhiteSpace(encryptedIncidentId))
            {
                hazOccId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            }
            string vesselId = GetVesselId(encryptedVesselId);
            List<HazOccDefectDetailsViewModel> result = new List<HazOccDefectDetailsViewModel>();

            var request = new Dictionary<string, object>()
            {
                { "hazOccId", hazOccId },
                { "vesId", vesselId }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/GetHazOccDefectDetails"));
            List<HazOccDefectDetails> response = await PostAsync<List<HazOccDefectDetails>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                foreach (HazOccDefectDetails item in response)
                {
                    HazOccDefectDetailsViewModel viewModel = new HazOccDefectDetailsViewModel();
                    viewModel.DefectNo = item.DefectNo;
                    viewModel.CurrentDueDate = item.CurrentDueDate;
                    viewModel.Datecompleted = item.Datecompleted;
                    viewModel.DefectTitle = item.DefectTitle;
                    viewModel.PlannedFor = item.PlannedFor;
                    viewModel.DefectStatus = item.DefectStatus;
                    viewModel.DefectId = item.DefectId;
                    viewModel.RequisitionCount = item.RequisitionCount;
                    viewModel.AccCode = item.AccCode;
                    viewModel.Priority = item.Priority;
                    result.Add(viewModel);
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ observation summary.
        /// </summary>
        /// <param name="ParentReportId">The parent report identifier.</param>
        /// <returns></returns>
        public async Task<string> PostGetHazOccParent(string ParentReportId)
        {
            string result = "-";
            if (!string.IsNullOrWhiteSpace(ParentReportId))
            {
                string urlRequest = "id=" + ParentReportId;
                Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IncidentPreviewById"), urlRequest);
                IncidentPreview response = await PostAsync<IncidentPreview>(requestUrl, CreateHttpContent(ParentReportId));

                if (response != null)
                {
                    result = response.ShipReferenceNumber + " [" + response.Type + "]";
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get haz occ accident event details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public async Task<AccidentEventDetailViewModel> PostGetHazOccAccidentEventDetails(string encryptedIncidentId, string categoryId)
        {
            List<HazOccStatCodes> hazoccLookupCodes = new List<HazOccStatCodes>()
            {
                HazOccStatCodes.DS,
                HazOccStatCodes.DP,
                HazOccStatCodes.IT,
                HazOccStatCodes.TA,
                HazOccStatCodes.BA,
                HazOccStatCodes.PE
            };

            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/AccidentEventDetails"), urlRequest);
            AccidentEventDetail response = await PostAsync<AccidentEventDetail>(requestUrl, CreateHttpContent(incidentId));
            AccidentEventDetailViewModel result = new AccidentEventDetailViewModel();
            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.ImmediateActionTaken = response.ImmediateActionTaken;
                result.AccidentDate = response.AccidentDate.HasValue ? response.AccidentDate.Value.ToString(Constants.DateTime24HrFormat) : String.Empty;
                result.ReportedDate = response.ReportedDate.HasValue ? response.ReportedDate.Value.ToString(Constants.DateTime24HrFormat) : String.Empty;
                result.ReportedTo = response.ReportedTo;
                result.ReportedToCrwIdTp = response.ReportedToCrwIdTp;
                result.ReportedToName = response.ReportedToName;
                result.AreaSupervisor = response.AreaSupervisor;
                result.AreaSupervisorCrwIdTp = response.AreaSupervisorCrwIdTp;
                result.AreaSupervisorName = response.AreaSupervisorName;
                result.AreaSupervisorRank = response.AreaSupervisorRank;
                result.AreaSupervisorRankName = response.AreaSupervisorRankName;
                result.Comments = response.Comments;
                result.WorkHours = response.WorkHours;
                result.RestHours = response.RestHours;
                result.DaysOnboard = response.DaysOnboard;
                result.HasSpectacle = response.HasSpectacle ? Constants.Yes : Constants.No;
                result.SpectaclesWorn = response.SpectaclesWorn ? Constants.Yes : Constants.No;
                result.PPEWorn = response.PPEWorn;
                result.Occupation = response.Occupation;
                result.TimeWithCompany = response.TimeWithCompany;
                result.DaysOnboardShip = response.DaysOnboardShip;

                if (response.CrewTimeWithCompany != null)
                {
                    string[] splitYear = response.CrewTimeWithCompany.ToString().Split('.');
                    int _timeWithCompanyYears = Convert.ToInt32(splitYear[0]);
                    int _timeWithCompanyMonths = Convert.ToInt32((response.CrewTimeWithCompany - _timeWithCompanyYears) * 12);

                    result.CrewTimeWithCompany = _timeWithCompanyYears + "yrs " + _timeWithCompanyMonths + "months";
                }

                if (response.CrewtimeInRank != null)
                {
                    string[] splitYear = response.CrewtimeInRank.ToString().Split('.');
                    int _timeInRankYears = Convert.ToInt32(splitYear[0]);
                    int _timeInRankMonths = Convert.ToInt32((response.CrewtimeInRank - _timeInRankYears) * 12);

                    result.CrewtimeInRank = _timeInRankYears + "yrs " + _timeInRankMonths + "months";
                }

                result.IsCrewAccident = categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.CA);
                result.IsThirdPartyAccident = categoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.TA);
                result.IsCrewOrThirtyPrtyAccident = result.IsCrewAccident || result.IsThirdPartyAccident;

                List<Lookup> hazOccStatLookup = await PostGetHazOccStatLookup(hazoccLookupCodes);

                if (hazOccStatLookup != null && hazOccStatLookup.Any())
                {
                    List<Lookup> AccidentDutyLookup = new List<Lookup>(hazOccStatLookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.DS) && x.Identifier == response.DutyStatusId));
                    List<Lookup> AccidentDeptLookup = new List<Lookup>(hazOccStatLookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.DP) && x.Identifier == response.PaxStatusId));
                    List<Lookup> InjuryTypeLookup = new List<Lookup>(hazOccStatLookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.IT) && x.Identifier == response.InjuryTypeId));
                    List<Lookup> AccidentTypeLookup = new List<Lookup>(hazOccStatLookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.TA) && x.Identifier == response.AccidentTypeId));
                    List<Lookup> BodyAreasLookup = new List<Lookup>(hazOccStatLookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.BA) && x.Identifier == response.BodyAreaAffectedId));
                    List<Lookup> PPEWornLookup = new List<Lookup>(hazOccStatLookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.PE) && x.Identifier == response.PPEWorn));

                    result.DutyStatus = AccidentDutyLookup != null && AccidentDutyLookup.Any() ? AccidentDutyLookup.FirstOrDefault().Description : Constants.DashForEmpty;
                    result.Department = AccidentDeptLookup != null && AccidentDeptLookup.Any() ? AccidentDeptLookup.FirstOrDefault().Description : Constants.DashForEmpty;
                    result.TypeInjury = InjuryTypeLookup != null && InjuryTypeLookup.Any() ? InjuryTypeLookup.FirstOrDefault().Description : Constants.DashForEmpty;
                    result.TypeAccident = AccidentTypeLookup != null && AccidentTypeLookup.Any() ? AccidentTypeLookup.FirstOrDefault().Description : Constants.DashForEmpty;
                    result.PPEWornDescription = PPEWornLookup != null && PPEWornLookup.Any() ? PPEWornLookup.FirstOrDefault().Description : Constants.DashForEmpty;
                    result.BodyAreasAffected = BodyAreasLookup != null && BodyAreasLookup.Any() ? BodyAreasLookup.FirstOrDefault().Description : Constants.DashForEmpty;
                }
                else
                {
                    result.DutyStatus = Constants.DashForEmpty;
                    result.Department = Constants.DashForEmpty;
                    result.TypeInjury = Constants.DashForEmpty;
                    result.TypeAccident = Constants.DashForEmpty;
                    result.PPEWornDescription = Constants.DashForEmpty;
                    result.BodyAreasAffected = Constants.DashForEmpty;
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get haz occ incident event details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<IncidentEventDetailViewModel> PostGetHazOccIncidentEventDetails(string encryptedIncidentId)
        {
            List<HazOccStatCodes> hazoccLookupCodes = new List<HazOccStatCodes>()
            {
                HazOccStatCodes.SO,
                HazOccStatCodes.PT
            };

            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IncidentEventDetails"), urlRequest);
            IncidentEventDetail response = await PostAsync<IncidentEventDetail>(requestUrl, CreateHttpContent(incidentId));
            IncidentEventDetailViewModel result = new IncidentEventDetailViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.DamageToVesselOrEquip = response.DamageToVesselOrEquip ? Constants.Yes : Constants.No;
                result.DamageToThirdParty = response.DamageToThirdParty ? Constants.Yes : Constants.No;
                result.DamageToThirdPartyDesc = response.DamageToThirdPartyDesc;
                result.IsDrugOrAlcoholFactor = response.IsDrugOrAlcoholFactor ? Constants.Yes : Constants.No;
                result.OilLtrsSea = response.OilLtrsSea;
                result.PollutionSeaId = response.PollutionSeaId;
                result.SeaDetails = response.SeaDetails;
                result.OilLtrsDeck = response.OilLtrsDeck;
                result.PollutionDeckId = response.PollutionDeckId;
                result.DeckDetails = response.DeckDetails;
                result.IsProtestNoteIssued = response.IsProtestNoteIssued ? Constants.Yes : Constants.No;
                result.ImmediateActionTaken = response.ImmediateActionTaken;
                result.DamageDescription = response.DamageDescription;

                List<Lookup> lookup = await PostGetHazOccStatLookup(hazoccLookupCodes);

                if (lookup != null && lookup.Any())
                {
                    List<Lookup> OperationsLookup = new List<Lookup>(lookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.SO)));
                    if (OperationsLookup != null && OperationsLookup.Any())
                    {
                        var SelectedOperation = OperationsLookup.FirstOrDefault(x => x.Identifier == response.ShipOperationId);
                        result.ShipOptions = SelectedOperation != null ? SelectedOperation.Description : Constants.DashForEmpty;
                    };

                    List<Lookup> IncidentPollutionCatLookup = new List<Lookup>(lookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.PT)));
                    if (IncidentPollutionCatLookup != null && IncidentPollutionCatLookup.Any())
                    {
                        var SelectedDeckPollutionCat = IncidentPollutionCatLookup.FirstOrDefault(x => x.Identifier == response.PollutionDeckId);
                        var SelectedSeaPollutionCat = IncidentPollutionCatLookup.FirstOrDefault(x => x.Identifier == response.PollutionSeaId);

                        result.PolutionCategoryDeck = SelectedDeckPollutionCat != null ? SelectedDeckPollutionCat.Description : Constants.DashForEmpty;
                        result.PolutionCategorySea = SelectedSeaPollutionCat != null ? SelectedSeaPollutionCat.Description : Constants.DashForEmpty;
                    }

                }

            }
            return result;
        }

        /// <summary>
        /// Posts the get haz occ ship finding.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<HazOccInitialFindingViewModel> PostGetHazOccShipFinding(string encryptedIncidentId, string encryptedVesselId)
        {
            string incidentId = null;
            if (!string.IsNullOrWhiteSpace(encryptedIncidentId))
            {
                incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            }
            string vesselId = GetVesselId(encryptedVesselId);

            HazOccInitialFindingViewModel result = new HazOccInitialFindingViewModel();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("vesselId", vesselId);
            dict.Add("incidentId", incidentId);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/ShipFinding"));
            HazOccInitialFinding response = await PostAsync<HazOccInitialFinding>(requestUrl, CreateHttpContent(dict));

            if (response != null)
            {
                result.Analysis = response.Analysis;
                result.Risk = response.Risk;
            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ investigation finding.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<HazOccInvestigationFindingViewModel> PostGetHazOccInvestigationFinding(string encryptedIncidentId, string encryptedVesselId)
        {
            string incidentId = null;
            if (!string.IsNullOrWhiteSpace(encryptedIncidentId))
            {
                incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            }
            string vesselId = GetVesselId(encryptedVesselId);

            HazOccInvestigationFindingViewModel result = new HazOccInvestigationFindingViewModel();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("vesselId", vesselId);
            dict.Add("incidentId", incidentId);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/InvestigationFinding"));
            HazOccInvestigationFinding response = await PostAsync<HazOccInvestigationFinding>(requestUrl, CreateHttpContent(dict));

            if (response != null)
            {
                result.Analysis = response.Analysis;
                result.Risk = response.Risk;
            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ direct causes.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<HazOccDirectCauseViewModel> PostGetHazOccDirectCauses(string encryptedIncidentId)
        {
            string incidentId = null;
            if (!string.IsNullOrWhiteSpace(encryptedIncidentId))
            {
                incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            }

            List<HazOccStatCodes> lookupCodes = new List<HazOccStatCodes>()
            {
                HazOccStatCodes.SSA,
                HazOccStatCodes.SSC
            };

            HazOccDirectCauseViewModel result = new HazOccDirectCauseViewModel();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("lookupCodes", lookupCodes);
            dict.Add("incidentId", incidentId);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/DirectCause"));
            HazOccDirectCause response = await PostAsync<HazOccDirectCause>(requestUrl, CreateHttpContent(dict));

            if (response != null)
            {
                result.DirectCauses = new List<HazOccQuestionsViewModel>();
                if (response.DirectCauses != null && response.DirectCauses.Any())
                {
                    foreach (var item in response.DirectCauses.Where(x => x.Value == 1).OrderBy(x => x.LookupCode))
                    {
                        HazOccQuestionsViewModel hazOccQuestionsViewModel = new HazOccQuestionsViewModel();
                        hazOccQuestionsViewModel.IadId = item.IadId;
                        hazOccQuestionsViewModel.LongDescription = item.LongDescription;
                        hazOccQuestionsViewModel.ParentIadId = item.ParentIadId;
                        hazOccQuestionsViewModel.SubStandardName = GetSubStandardName(item.LookupCode);
                        result.DirectCauses.Add(hazOccQuestionsViewModel);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ root causes.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<HazOccRootCauseViewModel> PostGetHazOccRootCauses(string encryptedIncidentId)
        {
            string incidentId = null;
            if (!string.IsNullOrWhiteSpace(encryptedIncidentId))
            {
                incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            }

            List<HazOccStatCodes> lookupCodes = new List<HazOccStatCodes>()
            {
                HazOccStatCodes.RCH,
                HazOccStatCodes.RCJ,
                HazOccStatCodes.RCM
            };

            HazOccRootCauseViewModel result = new HazOccRootCauseViewModel();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("lookupCodes", lookupCodes);
            dict.Add("incidentId", incidentId);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/RootCause"));
            HazOccRootCause response = await PostAsync<HazOccRootCause>(requestUrl, CreateHttpContent(dict));

            if (response != null)
            {
                result.RootCauses = new List<HazOccQuestionsViewModel>();

                if (response.RootCauses != null && response.RootCauses.Any())
                {
                    foreach (var item in response.RootCauses.Where(cause => cause.Value == 1).OrderBy(cause => cause.LookupCode))
                    {
                        HazOccQuestionsViewModel hazOccQuestionsViewModel = new HazOccQuestionsViewModel();
                        hazOccQuestionsViewModel.IadId = item.IadId;
                        hazOccQuestionsViewModel.LongDescription = item.LongDescription;
                        hazOccQuestionsViewModel.ParentIadId = item.ParentIadId;
                        hazOccQuestionsViewModel.SubStandardName = GetSubStandardName(item.LookupCode);
                        result.RootCauses.Add(hazOccQuestionsViewModel);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ causation.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<HazOccActionCausationViewModel> PostGetHazOccCausation(string encryptedIncidentId)
        {
            string incidentId = null;
            if (!string.IsNullOrWhiteSpace(encryptedIncidentId))
            {
                incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            }

            List<HazOccStatCodes> lookupCodes = new List<HazOccStatCodes>()
            {
                HazOccStatCodes.SSA,
                HazOccStatCodes.SSC,
                HazOccStatCodes.RCH,
                HazOccStatCodes.RCJ,
                HazOccStatCodes.RCM
            };

            HazOccActionCausationViewModel result = new HazOccActionCausationViewModel();
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("lookupCodes", lookupCodes);
            dict.Add("incidentId", incidentId);

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/SelectedHazOccCausation"));
            List<HazOccCausation> response = await PostAsync<List<HazOccCausation>>(requestUrl, CreateHttpContent(dict));

            if (response != null && response.Any())
            {
                List<HazOccCausation> DirectCauses = response.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.SSA)
                                                                    || x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.SSC)
                                                                    ).OrderBy(x => x.LookupCode).ToList();

                if (DirectCauses != null && DirectCauses.Any())
                {
                    result.DirectCauses = new List<HazOccCausationViewModel>();
                    foreach (var item in DirectCauses)
                    {
                        HazOccCausationViewModel hazOccQuestionsViewModel = new HazOccCausationViewModel();
                        hazOccQuestionsViewModel.IadId = item.IadId;
                        hazOccQuestionsViewModel.LongDescription = item.LongDescription;
                        hazOccQuestionsViewModel.Description = item.Description;
                        hazOccQuestionsViewModel.Name = GetSubStandardName(item.LookupCode);
                        result.DirectCauses.Add(hazOccQuestionsViewModel);
                    }
                }

                List<HazOccCausation> RootCauses = response.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.RCH)
                                                                    || x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.RCJ)
                                                                    || x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.RCM)
                                                                    ).OrderBy(x => x.LookupCode).ToList();
                if (RootCauses != null && RootCauses.Any())
                {
                    result.RootCauses = new List<HazOccCausationViewModel>();
                    foreach (var item in RootCauses)
                    {
                        HazOccCausationViewModel hazOccQuestionsViewModel = new HazOccCausationViewModel();
                        hazOccQuestionsViewModel.IadId = item.IadId;
                        hazOccQuestionsViewModel.LongDescription = item.LongDescription;
                        hazOccQuestionsViewModel.Description = item.Description;
                        hazOccQuestionsViewModel.Name = GetSubStandardName(item.LookupCode);
                        result.RootCauses.Add(hazOccQuestionsViewModel);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get incident actions all.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<List<IncidentActionViewModel>> PostGetIncidentActionsAll(string encryptedIncidentId)
        {
            string incidentId = null;
            if (!string.IsNullOrWhiteSpace(encryptedIncidentId))
            {
                incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            }
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IncidentActions"), urlRequest);
            List<IncidentAction> response = await PostAsync<List<IncidentAction>>(requestUrl, CreateHttpContent(incidentId));

            List<IncidentActionViewModel> result = new List<IncidentActionViewModel>();

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    IncidentActionViewModel incidentActionVM = new IncidentActionViewModel();
                    incidentActionVM.Status = GetActionCode(item.Status, item.ClosureDate);
                    incidentActionVM.StatusDesc = GetActionCodeDesc(item.Status, item.ClosureDate);
                    incidentActionVM.StatusColor = GetActionStatusColor(item);
                    incidentActionVM.CreatedDate = item.CreatedDate;
                    incidentActionVM.ActionToBeTaken = item.ActionToBeTaken;
                    incidentActionVM.ActionDate = item.ActionDate;
                    incidentActionVM.ClosureDate = item.ClosureDate;
                    incidentActionVM.ActionTaken = string.IsNullOrWhiteSpace(item.ActionTaken) ? string.Empty : item.ActionTaken;
                    incidentActionVM.Deadline = item.Deadline;
                    result.Add(incidentActionVM);
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get incident stat codes.
        /// </summary>
        /// <param name="devision">The devision.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> PostGetIncidentStatCodes(string devision)
        {
            List<Lookup> response = new List<Lookup>();
            string urlRequest = "devision=" + devision;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IncidentStatCodes"), urlRequest);
            response = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(devision));
            return response;
        }

        /// <summary>
        /// Posts the get incident classification filtered for abservation act label.
        /// </summary>
        /// <param name="reportType">Type of the report.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> PostGetIncidentClassificationFiltered(string reportType)
        {
            List<Lookup> response = new List<Lookup>();
            string urlRequest = "reportType=" + reportType;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IncidentClassificationFiltered"), urlRequest);
            response = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(reportType));
            return response;
        }

        /// <summary>
        /// Posts the get haz occ stat lookup.
        /// </summary>
        /// <param name="hazoccLookupCodes">The hazocc lookup codes.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> PostGetHazOccStatLookup(List<HazOccStatCodes> hazoccLookupCodes)
        {
            List<Lookup> response = new List<Lookup>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/HazOccStatLookup"));
            response = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(hazoccLookupCodes));
            return response;
        }

        /// <summary>
        /// Gets the manoeuvring options lookup.
        /// </summary>
        /// <param name="manoeuvringId">The manoeuvring identifier.</param>
        /// <returns></returns>
        private async Task<string> GetManoeuvringOptionsLookup(string manoeuvringId)
        {
            string result = string.Empty;
            var _manoeuvringOptions = await PostGetIncidentStatCodes(EnumsHelper.GetKeyValue(HazOccStatCodes.MN));

            if (_manoeuvringOptions != null && _manoeuvringOptions.Any(option => option.Identifier == manoeuvringId))
            {
                result = _manoeuvringOptions.FirstOrDefault(option => option.Identifier == manoeuvringId).Description;
            }

            return result;
        }

        /// <summary>
        /// Gets the equipment type lookup.
        /// </summary>
        /// <param name="EquipmentType">Type of the equipment.</param>
        /// <returns></returns>
        private async Task<string> GetEquipmentTypeLookup(string EquipmentType)
        {
            string result = Constants.DashForEmpty;
            var response = await PostGetIncidentStatCodes(EnumsHelper.GetKeyValue(HazOccStatCodes.EQP));

            if (response != null && response.Any(option => option.Identifier == EquipmentType))
            {
                result = response.FirstOrDefault(option => option.Identifier == EquipmentType).Description;
            }

            return result;
        }

        /// <summary>
        /// Gets the is passenger accident.
        /// </summary>
        /// <param name="CategoryId">The category identifier.</param>
        /// <returns></returns>
        private bool GetIsPassengerAccident(string CategoryId)
        {
            bool IsPassengerAccident = CategoryId == EnumsHelper.GetKeyValue(HazOccTypeCodes.PA);

            return IsPassengerAccident;
        }

        /// <summary>
        /// Gets the is passenger vessel.
        /// </summary>
        /// <param name="VtyDesc">The vty desc.</param>
        /// <returns></returns>
        private bool GetIsPassengerVessel(string VtyDesc)
        {
            bool IsPassengerVessel = (VtyDesc != null && VtyDesc.IndexOf(Constants.PassengerVesselType, 0, StringComparison.CurrentCultureIgnoreCase) >= 0);
            return IsPassengerVessel;
        }


        /// <summary>
        /// Gets the ashore locations.
        /// </summary>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="vesselLocationId">The vessel location identifier.</param>
        /// <returns></returns>
        private async Task<LookUp> GetAshoreLocations(string CategoryId, string vesselLocationId)
        {
            LookUp result = new LookUp();

            var _ashoreLocations = await PostGetIncidentStatCodes(EnumsHelper.GetKeyValue(GetIsPassengerAccident(CategoryId) ? HazOccStatCodes.PS : HazOccStatCodes.AL));
            if (_ashoreLocations != null && _ashoreLocations.Any(option => option.Identifier == vesselLocationId))
            {
                var response = _ashoreLocations.FirstOrDefault(option => option.Identifier == vesselLocationId);
                result.Identifier = response.Identifier;
                result.Description = response.Description;
            }
            return result;
        }

        /// <summary>
        /// Gets the on board locations.
        /// </summary>
        /// <param name="CategoryId">The category identifier.</param>
        /// <param name="vesselLocationId">The vessel location identifier.</param>
        /// <returns></returns>
        private async Task<LookUp> GetOnBoardLocations(string CategoryId, string vesselLocationId)
        {
            LookUp result = new LookUp();

            var _onBoardLocations = await PostGetIncidentStatCodes(EnumsHelper.GetKeyValue(GetIsPassengerAccident(CategoryId) ? HazOccStatCodes.PB : HazOccStatCodes.OL));
            if (_onBoardLocations != null && _onBoardLocations.Any(option => option.Identifier == vesselLocationId))
            {
                var response = _onBoardLocations.Where(option => option.Identifier == vesselLocationId).FirstOrDefault();
                result.Identifier = response.Identifier;
                result.Description = response.Description;
            }
            return result;
        }

        /// <summary>
        /// Gets the ship location.
        /// </summary>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <param name="shipLocationId">The ship location identifier.</param>
        /// <param name="IsAshore">if set to <c>true</c> [is ashore].</param>
        /// <returns></returns>
        private async Task<string> GetShipLocation(string vesselType, string shipLocationId, bool IsAshore)
        {
            var result = Constants.DashForEmpty;
            var shipLocationDetail = await PostGetIncidentStatCodes(EnumsHelper.GetKeyValue(GetIsPassengerVessel(vesselType) ? HazOccStatCodes.PSL : HazOccStatCodes.SL));
            if (!IsAshore && shipLocationDetail != null && shipLocationDetail.Any())
            {
                var shipsLocation = shipLocationDetail.Where(x => x.LookupCode == EnumsHelper.GetDescription(ChangeLocationEnum.Onboard)).ToList();

                if (shipsLocation != null && shipsLocation.Any(location => location.Identifier == shipLocationId))
                    result = shipsLocation.FirstOrDefault(location => location.Identifier == shipLocationId).Description;
            }
            else if (IsAshore && shipLocationDetail != null && shipLocationDetail.Any())
            {
                var shipsLocation = shipLocationDetail.Where(x => x.LookupCode == EnumsHelper.GetDescription(ChangeLocationEnum.Ashore)).ToList();

                if (shipsLocation != null && shipsLocation.Any(location => location.Identifier == shipLocationId))
                    result = shipsLocation.FirstOrDefault(location => location.Identifier == shipLocationId).Description;
            }
            return result;
        }

        /// <summary>
        /// Gets the operations.
        /// </summary>
        /// <param name="shipOperationId">The ship operation identifier.</param>
        /// <returns></returns>
        private async Task<string> GetOperations(string shipOperationId)
        {
            var result = string.Empty;
            var _operationsLookup = await PostGetIncidentStatCodes(EnumsHelper.GetKeyValue(HazOccStatCodes.NM));

            if (_operationsLookup != null && _operationsLookup.Any(operation => operation.Identifier == shipOperationId))
            {
                result = _operationsLookup.FirstOrDefault(operation => operation.Identifier == shipOperationId).Description;
            }
            return result;
        }

        /// <summary>
        /// Gets the consequences.
        /// </summary>
        /// <param name="possibleConsequenceId">The possible consequence identifier.</param>
        /// <returns></returns>
        private async Task<string> GetConsequences(string possibleConsequenceId)
        {
            string result = string.Empty;
            var _consequencesLookup = await PostGetIncidentStatCodes(EnumsHelper.GetKeyValue(HazOccStatCodes.PC));

            if (_consequencesLookup != null && _consequencesLookup.Any(consequence => consequence.Identifier == possibleConsequenceId))
            {
                result = _consequencesLookup.FirstOrDefault(consequence => consequence.Identifier == possibleConsequenceId).Description;
            }

            return result;
        }

        /// <summary>
        /// Gets the name of the sub standard.
        /// </summary>
        /// <param name="lookupCode">The lookup code.</param>
        /// <returns></returns>
        private string GetSubStandardName(string lookupCode)
        {
            if (lookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.RCH))
            {
                return Constants.HumanFactorsText;
            }
            else if (lookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.RCJ))
            {
                return Constants.JobFactorsText;
            }
            else if (lookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.RCM))
            {
                return Constants.ManagementFailureText;
            }
            else if (lookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.SSC))
            {
                return Constants.StandardCondtionText;
            }
            else if (lookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.SSA))
            {
                return Constants.StandardActText;
            }

            return null;
        }

        /// <summary>
        /// Shows the hide haz occ summary location fields.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        private HazOccSummaryLocationViewModel ShowHideHazOccSummaryLocationFields(string identifier)
        {
            HazOccSummaryLocationViewModel locationFields = new HazOccSummaryLocationViewModel();

            if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.AP) || identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.PAP))
            {
                locationFields.ShowPort = true;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.AY))
            {
                locationFields.LocationLookupLabel = Constants.YardNameReportLocationLabel;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.AO) || identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.PAO))
            {
                locationFields.LocationLookupLabel = Constants.SpecifyReportLocationLabel;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.OS) || identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.POS))
            {
                locationFields.ShowLongLat = true;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.OP) || identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.POP))
            {
                locationFields.ShowPort = true;
                locationFields.ShowManeuvering = true;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.OR) || identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.POR))
            {
                locationFields.ShowLongLat = true;
                locationFields.LocationLookupLabel = Constants.RivernameReportLocationLabel;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.OC) || identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.POC))
            {
                locationFields.LocationLookupLabel = Constants.CanalNameReportLocationLabel;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.OY))
            {
                locationFields.LocationLookupLabel = Constants.YardNameReportLocationLabel;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.OI))
            {
                locationFields.LocationLookupLabel = Constants.InstallationNameRefReportLocationLabel;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.OA) || identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.POA))
            {
                locationFields.ShowLongLat = true;
            }
            else if (identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.OO) || identifier == EnumsHelper.GetKeyValue(HazOccReportLocationAbr.POO))
            {
                locationFields.LocationLookupLabel = Constants.SpecifyReportLocationLabel;
            }

            return locationFields;
        }

        /// <summary>
        /// Creates the longitude.
        /// </summary>
        /// <param name="LongDegree">The long degree.</param>
        /// <param name="LongMin">The long minimum.</param>
        /// <param name="LongIndicator">The long indicator.</param>
        /// <returns></returns>
        private string CreateLongitude(short? LongDegree, short? LongMin, string LongIndicator)
        {
            string longitude = Constants.DashForEmpty;
            if (LongDegree.HasValue)
            {
                longitude += LongDegree.Value.ToString("0.00").Replace(".00", String.Empty);
                longitude += "°, ";
            }
            if (LongMin.HasValue)
            {
                longitude += LongMin.Value.ToString("0.00");
                longitude += "' ";
                longitude += LongIndicator;
            }
            return longitude;
        }

        /// <summary>
        /// Creates the latitude.
        /// </summary>
        /// <param name="LatDegree">The lat degree.</param>
        /// <param name="LatMin">The lat minimum.</param>
        /// <param name="LatIndicator">The lat indicator.</param>
        /// <returns></returns>
        private string CreateLatitude(short? LatDegree, short? LatMin, string LatIndicator)
        {
            string latitude = Constants.DashForEmpty;
            if (LatDegree.HasValue)
            {
                latitude += LatDegree.Value.ToString("0.00").Replace(".00", String.Empty);
                latitude += "°, ";
            }
            if (LatMin.HasValue)
            {
                latitude += LatMin.Value.ToString("0.00");
                latitude += "' ";
                latitude += LatIndicator;
            }
            return latitude;
        }

        /// <summary>
        /// Posts the get haz occ near miss summary.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<NearMissSummaryViewModel> PostGetHazOccNearMissSummary(string encryptedIncidentId, string vesselType)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/NearMissSummary"), urlRequest);
            NearMissSummary response = await PostAsync<NearMissSummary>(requestUrl, CreateHttpContent(incidentId));
            NearMissSummaryViewModel result = new NearMissSummaryViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.ReportDate = response.ReportDate == null ? Constants.DashForEmpty : response.ReportDate.GetValueOrDefault().ToString(Constants.DateFormat);
                result.NearMissDateTime = response.NearMissDateTime == null ? Constants.DashForEmpty : response.NearMissDateTime.GetValueOrDefault().ToString(Constants.DateTime24HrFormat);
                result.UpdatedDate = response.UpdatedDate == null ? Constants.DashForEmpty : response.UpdatedDate.GetValueOrDefault().ToString(Constants.DayDateFormat);
                result.CreatedBy = string.IsNullOrWhiteSpace(response.CreatedBy) ? Constants.DashForEmpty : response.CreatedBy;
                result.SafetyOfficer = string.IsNullOrWhiteSpace(response.SafetyOfficer) ? Constants.DashForEmpty : response.SafetyOfficer;
                result.MasterName = string.IsNullOrWhiteSpace(response.MasterName) ? Constants.DashForEmpty : response.MasterName;
                result.Classification = response.Classification ?? Constants.DashForEmpty;
                result.Description = response.Description ?? Constants.DashForEmpty;
                result.ObservationRaisedByName = response.ObservationRaisedByName ?? Constants.DashForEmpty;
                result.Rank = await GetCrewRanks(response.RankId, response.Rank, response.VesselId);
                result.PotentialSeverity = await GetPotentialSeverityLookup(response.PotentialSeverityId, true);
                result.ClosedDate = response.ClosedDate.HasValue ? response.ClosedDate.GetValueOrDefault().ToString(Constants.DateFormat) : null;
            }

            return result;
        }

        /// <summary>
        /// The post get hazocc near miss event details
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<NearMissEventDetailViewModel> PostGetHazOccNearMissEventDetails(string encryptedIncidentId)
        {
            List<HazOccStatCodes> hazoccLookupCodes = new List<HazOccStatCodes>()
            {
                HazOccStatCodes.NM,
                HazOccStatCodes.PC
            };

            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/NearMissEventDetails"), urlRequest);
            NearMissEventDetail response = await PostAsync<NearMissEventDetail>(requestUrl, CreateHttpContent(incidentId));
            NearMissEventDetailViewModel result = new NearMissEventDetailViewModel();
            if (response != null)
            {
                result.ImmediateActionTaken = response.ImmediateActionTaken;

                List<Lookup> hazOccStatLookup = await PostGetHazOccStatLookup(hazoccLookupCodes);

                if (hazOccStatLookup != null && hazOccStatLookup.Any())
                {
                    List<Lookup> OperationLookup = new List<Lookup>(hazOccStatLookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.NM) && x.Identifier == response.OperationId));
                    List<Lookup> PossibleConsequenceLookup = new List<Lookup>(hazOccStatLookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.PC) && x.Identifier == response.PossibleConsequenceId));

                    result.Operation = OperationLookup != null && OperationLookup.Any() ? OperationLookup.FirstOrDefault().Description : Constants.DashForEmpty;
                    result.PossibleConsequence = PossibleConsequenceLookup != null && PossibleConsequenceLookup.Any() ? PossibleConsequenceLookup.FirstOrDefault().Description : Constants.DashForEmpty;
                }
                else
                {
                    result.Operation = Constants.DashForEmpty;
                    result.PossibleConsequence = Constants.DashForEmpty;
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the potential severity lookup.
        /// </summary>
        /// <param name="potentialSeverityId">The potential severity identifier.</param>
        /// <param name="isObservation">The is observation.</param>
        /// <returns></returns>
        private async Task<string> GetPotentialSeverityLookup(string potentialSeverityId, bool? isObservation = null)
        {
            string PotentialSeverity = Constants.DashForEmpty;
            var PotentialSeverityLookup = await GetHazoccSeverityFilter(isObservation);
            if (PotentialSeverityLookup != null && PotentialSeverityLookup.Any(severity => severity.Identifier == potentialSeverityId))
            {
                var _selectedPotentialSeverity = PotentialSeverityLookup.FirstOrDefault(severity => severity.Identifier == potentialSeverityId);
                PotentialSeverity = _selectedPotentialSeverity.Description;
            }
            return PotentialSeverity;
        }

        /// <summary>
        /// Gets the action code.
        /// </summary>
        /// <param name="Status">The status.</param>
        /// <param name="ClosureDate">The closure date.</param>
        /// <returns></returns>
        private string GetActionCode(string Status, DateTime? ClosureDate)
        {
            if (ClosureDate != null)
            { return Constants.CL; }
            else if (Status == EnumsHelper.GetDescription(HazOccEvalCodes.CA))
            { return Constants.CA; }
            else if (Status == EnumsHelper.GetDescription(HazOccEvalCodes.SR))
            { return Constants.SR; }
            else { return Constants.CR; }
        }

        /// <summary>
        /// Gets the action code desc.
        /// </summary>
        /// <param name="Status">The status.</param>
        /// <param name="ClosureDate">The closure date.</param>
        /// <returns></returns>
        private string GetActionCodeDesc(string Status, DateTime? ClosureDate)
        {
            if (ClosureDate != null)
            { return Constants.Closed; }
            else if (Status == EnumsHelper.GetDescription(HazOccEvalCodes.CA))
            { return Constants.CorrectiveAction; }
            else if (Status == EnumsHelper.GetDescription(HazOccEvalCodes.SR))
            { return Constants.SendForReview; }
            else { return Constants.Correction; }
        }

        /// <summary>
        /// Gets the color of the action status.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private KPI GetActionStatusColor(IncidentAction item)
        {
            if (item.ClosureDate != null)
            {
                return KPI.Normal; //Closed
            }
            else if (item.Deadline != null && item.Deadline < DateTime.Now)
            {
                return KPI.Warning; //Due date up!
            }
            else if (item.Status == EnumsHelper.GetDescription(HazOccEvalCodes.SR))
            {
                return KPI.Good; //Send for review item
            }
            else
            {
                return KPI.PreWarning;
            }
        }

        /// <summary>
        /// Determines whether [is report incident or accident].
        /// </summary>
        /// <param name="IncidentParameter">The incident parameter.</param>
        /// <returns>
        ///   <c>true</c> if [is report incident or accident]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsReportIncidentOrAccident(HazOccPreviewResponse IncidentParameter)
        {
            return IncidentParameter != null && (IncidentParameter.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.Accident) || IncidentParameter.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.Incident));
        }

        /// <summary>
        /// Determines whether [is report illness].
        /// </summary>
        /// <param name="IncidentParameter">The incident parameter.</param>
        /// <returns>
        ///   <c>true</c> if [is report illness]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsReportIllness(HazOccPreviewResponse IncidentParameter)
        {
            return IncidentParameter != null
                && IncidentParameter.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.Illness);
        }

        /// <summary>
        /// Determines whether [is report near miss or observation].
        /// </summary>
        /// <param name="IncidentParameter">The incident parameter.</param>
        /// <returns>
        ///   <c>true</c> if [is report near miss or observation]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsReportNearMissOrObservation(HazOccPreviewResponse IncidentParameter)
        {
            return IncidentParameter != null && (IncidentParameter.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.NearMiss) || IncidentParameter.TypeId == EnumsHelper.GetKeyValue(HazOccReportCodes.Observation));
        }

        /// <summary>
        /// Posts the get hierarchy explorer mapping.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="sourceReferenceId">The source reference identifier.</param>
        /// <returns></returns>
        public async Task<List<HierarchyExplorerMappingDetailViewModel>> PostGetHierarchyExplorerMapping(string sourceId, string sourceReferenceId)
        {
            List<HierarchyExplorerMappingDetailViewModel> result = new List<HierarchyExplorerMappingDetailViewModel>();
            var input = new Dictionary<string, object>()
                    {
                        { "sourceId", sourceId },
                        { "sourceReferenceId", sourceReferenceId }
                    };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vessel/HierarchyExplorerMapping"));
            List<HierarchyExplorerMappingDetail> response = await PostAsync<List<HierarchyExplorerMappingDetail>>(requestUrl, CreateHttpContent(input));

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    result.Add(new HierarchyExplorerMappingDetailViewModel
                    {
                        SystemArea = x.SystemArea,
                        Maker = x.Maker,
                        ComponentName = x.ComponentName,
                        Designer = x.Designer,
                        Model = x.Model,
                        Position = x.Position
                    });
                });
            }

            return result;
        }

        /// <summary>
        /// Gets the crew ranks.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <param name="defaultRank">The default rank.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        private async Task<string> GetCrewRanks(string identifier, string defaultRank, string vesselId)
        {
            var CrewRanks = await GetVesselCrewRanks(vesselId);
            string _crewRank = defaultRank;

            if (CrewRanks != null && CrewRanks.Any(rank => rank.Identifier == identifier))
            {
                var _selectedCrewRank = CrewRanks.FirstOrDefault(rank => rank.Identifier == identifier);

                if (_selectedCrewRank != null)
                {
                    _crewRank = _selectedCrewRank.Description;
                }
            }

            return _crewRank;
        }

        /// <summary>
        /// Gets the vessel crew ranks.
        /// </summary>
        /// <param name="vesselId">The vesel identifier.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> GetVesselCrewRanks(string vesselId)
        {
            List<InspectionTypeDetailViewModel> responseVMList = new List<InspectionTypeDetailViewModel>();

            string urlRequest = "vesId=" + vesselId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vessel/VesselCrewRanks/"), urlRequest);
            List<Lookup> response = await PostAsync<List<Lookup>>(requestUrl, CreateHttpContent(vesselId));

            return response;
        }

        /// <summary>
        /// Posts the get haz occ illness report summary.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<IllnessReportSummaryViewModel> PostGetHazOccIllnessReportSummary(string encryptedIncidentId)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/IllnessReportSummary"), urlRequest);
            IllnessReportSummary response = await PostAsync<IllnessReportSummary>(requestUrl, CreateHttpContent(incidentId));
            IllnessReportSummaryViewModel result = new IllnessReportSummaryViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.ReportDate = response.ReportDate == null ? Constants.DashForEmpty : response.ReportDate.GetValueOrDefault().ToString(Constants.DateFormat);
                result.AccidentDateTime = response.AccidentDateTime == null ? Constants.DashForEmpty : response.AccidentDateTime.GetValueOrDefault().ToString(Constants.DateTime24HrFormat);
                result.UpdatedDate = response.UpdatedDate == null ? Constants.DashForEmpty : response.UpdatedDate.GetValueOrDefault().ToString(Constants.DayDateFormat);
                result.CreatedBy = string.IsNullOrWhiteSpace(response.CreatedBy) ? Constants.DashForEmpty : response.CreatedBy;
                result.SafetyOfficer = string.IsNullOrWhiteSpace(response.SafetyOfficer) ? Constants.DashForEmpty : response.SafetyOfficer;
                result.MasterName = string.IsNullOrWhiteSpace(response.MasterName) ? Constants.DashForEmpty : response.MasterName;
                result.Classification = response.Classification ?? Constants.DashForEmpty;
                result.Description = response.Description ?? Constants.DashForEmpty;
                result.ClosureComments = !string.IsNullOrWhiteSpace(response.MsqComments) ? response.MsqComments : response.FleetManagerComments;
                result.InvestigateName = response.InvestigateName;
                result.InvestigateDate = response.InvestigateDate.HasValue ? response.InvestigateDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.InvestigateRankName = await GetCrewRanks(response.InvestigateRank, response.InvestigateRankName, response.VesselId);
                result.ParentReportId = response.ParentReportId;
                result.ActualSeverity = await GetPotentialSeverityLookup(response.ActualSeverityId);
                result.ClosedDate = response.ClosedDate.HasValue ? response.ClosedDate.GetValueOrDefault().ToString(Constants.DateFormat) : null;
            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ illness passenger details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<HazOccPassengerAccidentDetailViewModel> PostGetHazOccPassengerDetails(string encryptedIncidentId)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/PassengerAccidentDetails"), urlRequest);
            HazOccPassengerAccidentDetail response = await PostAsync<HazOccPassengerAccidentDetail>(requestUrl, CreateHttpContent(incidentId));
            HazOccPassengerAccidentDetailViewModel result = new HazOccPassengerAccidentDetailViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.LastName = response.LastName;
                result.FirstName = response.FirstName;
                result.Nationality = response.Nationality;
                result.DateOfBirth = response.DateOfBirth.HasValue ? response.DateOfBirth.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.Address = response.Address;
                result.Gender = response.Gender == "F" ? "Female" : "Male";
                result.Occupation = response.Occupation;
                result.MaritalStatus = response.MaritalStatus;
                result.TelephoneNumber = response.TelephoneNumber;

                result.CabinNumber = response.CabinNumber;
                result.BookingNumber = response.BookingNumber;
                result.EmbarkedInPortName = response.EmbarkedInPortName;
                result.EmbarkedInCountryCode = response.EmbarkedInCountryCode;

                result.DisembarkedInPortName = response.DisembarkedInPortName;
                result.DisembarkedAtCountryCode = response.DisembarkedAtCountryCode;

                result.TravelAgent = response.TravelAgent;

                result.DisembarkedDate = response.DisembarkedDate.HasValue ? response.DisembarkedDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.EmbarkedDate = response.EmbarkedDate.HasValue ? response.EmbarkedDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;

                result.Relationship = response.Relationship;
                result.CompanionLastName = response.CompanionLastName;
                result.CompanionFirstName = response.CompanionFirstName;
                result.CompanionNationality = response.CompanionNationality;
                result.CompanionTelephoneNumber = response.CompanionTelephoneNumber;
                result.CompanionAddress = response.CompanionAddress;

            }
            return result;
        }

        /// <summary>
        /// Posts the get haz occ passenger doctors report.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<PassengerDoctorsReportViewModel> PostGetHazOccPassengerDoctorsReport(string encryptedIncidentId)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/PassengerDoctorsReport"), urlRequest);
            PassengerDoctorsReport response = await PostAsync<PassengerDoctorsReport>(requestUrl, CreateHttpContent(incidentId));
            PassengerDoctorsReportViewModel result = new PassengerDoctorsReportViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.IsFirstAidAdministered = response.IsFirstAidAdministered ? Constants.Yes : Constants.No;
                result.FirstAidAdministeredByForename = response.FirstAidAdministeredByForename;
                result.FirstAidAdministeredBySurname = response.FirstAidAdministeredBySurname;
                result.IsResuscitationEquipmentAvailable = response.IsResuscitationEquipmentAvailable ? Constants.Yes : Constants.No;
                result.EquipmentLocation = response.EquipmentLocation;
                result.HasConsumedDrugOrAlcohol = response.HasConsumedDrugOrAlcohol ? Constants.Yes : Constants.No;
                result.AlcoholConsumed = response.AlcoholConsumed;
                result.HasSpectacle = response.HasSpectacle ? Constants.Yes : Constants.No;
                result.SpectaclesWorn = response.SpectaclesWorn ? Constants.Yes : Constants.No;
                result.AtAuthorisedPlace = response.AtAuthorisedPlace ? Constants.Yes : Constants.No;
                result.ShoesDescription = response.ShoesDescription;
                result.DoctorsDiagnosis = response.DoctorsDiagnosis;
            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ passenger treatment details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<PassengerTreatmentDetailViewModel> PostGetHazOccPassengerTreatmentDetails(string encryptedIncidentId)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/PassengerTreatmentDetails"), urlRequest);
            PassengerTreatmentDetail response = await PostAsync<PassengerTreatmentDetail>(requestUrl, CreateHttpContent(incidentId));
            PassengerTreatmentDetailViewModel result = new PassengerTreatmentDetailViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.InjuryDetail = response.InjuryDetail;
                result.InjuryTreatment = response.InjuryTreatment;
                result.DisembarkedPortName = response.DisembarkedPortName;
                result.DisembarkedCountryCode = response.DisembarkedCountryCode;
                result.DisembarkedDate = response.DisembarkedDate.HasValue ? response.DisembarkedDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.SentToShoreDoctor = response.SentToShoreDoctor ? Constants.Yes : Constants.No;
                result.XRaysRecommended = response.XRaysRecommended ? Constants.Yes : Constants.No;
                result.TestResult = response.TestResult;
                result.DoctorNameAndAddress = response.DoctorNameAndAddress;
                result.Remarks = response.Remarks;
                result.Visits = new List<IncidentVisitViewModel>();

                if (response.Visits != null && response.Visits.Any())
                {
                    foreach (var item in response.Visits)
                    {
                        IncidentVisitViewModel visit = new IncidentVisitViewModel();
                        visit.VisitOn = item.VisitOn;
                        visit.VisitNo = item.VisitNo;
                        result.Visits.Add(visit);
                    }
                }

            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ crew illness detail.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<CrewIllnessDetailViewModel> PostGetHazOccCrewIllnessDetail(string encryptedIncidentId)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/CrewIllnessDetail"), urlRequest);
            CrewIllnessDetail response = await PostAsync<CrewIllnessDetail>(requestUrl, CreateHttpContent(incidentId));
            CrewIllnessDetailViewModel result = new CrewIllnessDetailViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.CrewNotFound = response.CrewNotFound ? Constants.Yes : Constants.No;
                result.CrewLastName = response.CrewLastName;
                result.CrewFirstName = response.CrewFirstName;
                result.RankDescription = response.RankDescription;
                result.Nationality = response.Nationality;
                result.DateOfBirth = response.DateOfBirth.HasValue ? response.DateOfBirth.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.Address = response.Address;
                result.Gender = response.Gender == "F" ? "Female" : "Male";
                result.MaritalStatus = response.MaritalStatus;
                result.PassportNumber = response.PassportNumber;
                result.BookNumber = response.BookNumber;
                result.PCN = response.PCN;
                result.ReportedDate = response.ReportedDate.HasValue ? response.ReportedDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;

                List<HazOccStatCodes> hazoccLookupCodes = new List<HazOccStatCodes>()
                {
                    HazOccStatCodes.MedIll
                };

                List<Lookup> hazOccStatLookup = await PostGetHazOccStatLookup(hazoccLookupCodes);

                if (hazOccStatLookup != null && hazOccStatLookup.Any())
                {
                    List<Lookup> lookup = new List<Lookup>(hazOccStatLookup.Where(x => x.LookupCode == EnumsHelper.GetKeyValue(HazOccStatCodes.MedIll) && x.Identifier == response.InjuryTypeId));
                    result.IllnessTypes = lookup != null && lookup.Any() ? lookup.FirstOrDefault().Description : Constants.DashForEmpty;
                }
            }
            return result;
        }

        /// <summary>
        /// Posts the get haz occ crew treatment detail.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<CrewTreatmentDetailViewModel> PostGetHazOccCrewTreatmentDetail(string encryptedIncidentId)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/CrewTreatmentDetail"), urlRequest);
            CrewTreatmentDetail response = await PostAsync<CrewTreatmentDetail>(requestUrl, CreateHttpContent(incidentId));
            CrewTreatmentDetailViewModel result = new CrewTreatmentDetailViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;
                result.InjuryDetail = response.InjuryDetail;
                result.InjuryTreatment = response.InjuryTreatment;
                result.DependentDetail = response.DependentDetail;
                result.Comments = response.Comments;

                result.OffSignedDate = response.OffSignedDate.HasValue ? response.OffSignedDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty; ;
                result.TreatmentDate = response.TreatmentDate.HasValue ? response.TreatmentDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty; ;

                if (!string.IsNullOrWhiteSpace(response.OffSignedPortId))
                {
                    result.OffSignedPortName = response.OffSignedPortName;
                    result.OffSignedCountryCode = response.OffSignedCountryCode;
                }

                if (!string.IsNullOrWhiteSpace(response.TreatmentPortId))
                {
                    result.TreatmentPortName = response.TreatmentPortName;
                    result.TreatmentCountryCode = response.TreatmentCountryCode;
                }

                result.TestResult = response.TestResult ? Constants.Yes : Constants.No;
                result.IsCrewTreatedAshore = response.IsCrewTreatedAshore ? Constants.Yes : Constants.No;
                result.IsCrewOffSigned = response.IsCrewOffSigned ? Constants.Yes : Constants.No;
                result.DrugAlcoholTested = response.DrugAlcoholTested ? Constants.Yes : Constants.No;
                result.HasCrewResumedWork = response.HasCrewResumedWork ? Constants.Yes : Constants.No;
                result.HoursComplaint = response.HoursComplaint ? Constants.Yes : Constants.No;
                result.FirstAidGivenOnBoard = response.FirstAidGivenOnBoard ? Constants.Yes : Constants.No;
                result.NumberOfDaysOff = response.NumberOfDaysOff;
                result.ResumedDate = response.ResumedDate.HasValue ? response.ResumedDate.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty; ;

            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ crew doctors report.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<CrewDoctorsReportViewModel> PostGetHazOccCrewDoctorsReport(string encryptedIncidentId)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/CrewDoctorsReport"), urlRequest);
            CrewDoctorsReport response = await PostAsync<CrewDoctorsReport>(requestUrl, CreateHttpContent(incidentId));
            CrewDoctorsReportViewModel result = new CrewDoctorsReportViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.VesselId = response.VesselId;

                result.DoctorName = response.DoctorName;
                result.RestrictionDays = response.RestrictionDays;
                result.DisabilityDays = response.DisabilityDays;
                result.HospitalisationRequired = response.HospitalisationRequired ? Constants.Yes : Constants.No;

                result.SymptomDescription = response.SymptomDescription;
                result.HospitalDetail = response.HospitalDetail;
                result.DoctorAddress = response.DoctorAddress;

                result.Visits = new List<DoctorVisitReportViewModel>();
                if (response.Visits != null && response.Visits.Any())
                {
                    foreach (var item in response.Visits)
                    {
                        DoctorVisitReportViewModel visit = new DoctorVisitReportViewModel();
                        visit.Limitation = item.Limitation;
                        visit.Type = item.Type;
                        result.Visits.Add(visit);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Posts the get haz occ third party accident detail.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<HazOccThirdPartyAccidentDetailViewModel> PostGetHazOccThirdPartyAccidentDetail(string encryptedIncidentId)
        {
            string incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string urlRequest = "incidentId=" + incidentId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "HazardousOccurrences/ThirdPartyAccidentDetail"), urlRequest);
            HazOccThirdPartyAccidentDetail response = await PostAsync<HazOccThirdPartyAccidentDetail>(requestUrl, CreateHttpContent(incidentId));
            HazOccThirdPartyAccidentDetailViewModel result = new HazOccThirdPartyAccidentDetailViewModel();

            if (response != null)
            {
                result.ImrId = response.ImrId;
                result.LastName = response.LastName;
                result.FirstName = response.FirstName;
                result.Nationality = response.Nationality;
                result.DateOfBirth = response.DateOfBirth.HasValue ? response.DateOfBirth.GetValueOrDefault().ToString(Constants.DateFormat) : Constants.DashForEmpty;
                result.Address = response.Address;
                result.PassportNumber = response.PassportNumber;
                result.BookNumber = response.BookNumber;
                result.InjuryDetail = response.InjuryDetail;
            }
            return result;
        }

        /// <summary>
        /// Gets the acts lookup.
        /// </summary>
        /// <param name="ActId">The act identifier.</param>
        /// <param name="ClassificationId">The classification identifier.</param>
        /// <returns></returns>
        private async Task<string> GetActsDescription(string ActId, string ClassificationId)
        {
            string result = Constants.DashForEmpty;
            List<Lookup> actsLookup = new List<Lookup>();

            if (ClassificationId == EnumsHelper.GetKeyValue(HazOccClassCodes.UA) || ClassificationId == EnumsHelper.GetKeyValue(HazOccClassCodes.SA))
            {
                actsLookup = await PostGetIncidentStatCodes(EnumsHelper.GetKeyValue(HazOccStatCodes.AT));
            }
            else if (ClassificationId == EnumsHelper.GetKeyValue(HazOccClassCodes.UC) || ClassificationId == EnumsHelper.GetKeyValue(HazOccClassCodes.SD))
            {
                actsLookup = await PostGetIncidentStatCodes(EnumsHelper.GetKeyValue(HazOccStatCodes.CT));
            }

            if (actsLookup != null && actsLookup.Any(act => act.Identifier == ActId))
            {
                result = actsLookup.FirstOrDefault(act => act.Identifier == ActId).Description;
            }

            return result;
        }

        /// <summary>
        /// Exports to excel PMS list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<WorkBasketDetailExportViewModel>> ExportToExcelPMSList(PlannedMaintenanceListViewModel request)
        {
            List<WorkBasketDetailResponse> response = new List<WorkBasketDetailResponse>();
            WorkBasketDetailRequest filter = SetPMSListRequestObject(request);
            var value = new Dictionary<string, object>()
                {
                    { "request", filter }
                };
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/PWAWorkBasketDetail"));
            response = await PostAsyncAutoPaged<WorkBasketDetailResponse>(requestUrl, value, 500);
            List<WorkBasketDetailExportViewModel> result = new List<WorkBasketDetailExportViewModel>();

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    result.Add(new WorkBasketDetailExportViewModel
                    {
                        Job = item.JobName,
                        DueDate = item.DueDate.HasValue ? item.DueDate.Value.ToString(Constants.DateFormat) : string.Empty,
                        Type = item.Type,
                        ComponentName = item.ComponentName,
                        Status = item.Status,
                        Interval = (item.Frequency ?? 0) + " " + item.FrequencyTypeShortCode,
                        Resp = item.ResponsibleRankShortCode ?? string.Empty,
                        LeftHours = item.LeftHours,
                        RequiredSpareCount = !IsReportedWorkOrder(item.WorkOrderStatusId) ? item.RequiredSpareCount.GetValueOrDefault() : 0,

                        //specifics column icon related flags
                        HasMappedJSA = (item.JsaRequired && !string.IsNullOrWhiteSpace(item.MappedJsaId)) ? Constants.Yes : Constants.No,
                        IsJSAToBeMapped = (item.JsaRequired && string.IsNullOrWhiteSpace(item.MappedJsaId)) ? Constants.Yes : Constants.No,
                        IsJSAPermitRequired = item.JsaPermitRequired ? Constants.Yes : Constants.No,
                        HasRoundsJobIcon = (!string.IsNullOrWhiteSpace(item.WorkOrderIndicationTypeId) && item.WorkOrderIndicationTypeId == EnumsHelper.GetKeyValue(WorkOrderIndicationType.Round)) ? Constants.Yes : Constants.No,
                        IsRobLessThanReq = item.IsRobLessThanReq.GetValueOrDefault() ? Constants.Yes : Constants.No,
                        IsCritical = item.IsCritical.GetValueOrDefault() ? Constants.Yes : Constants.No
                    });
                }
            }
            return result;
        }

        /// <summary>
        /// Exports to excel maintenance history list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<List<WorkHistoryExportViewModel>> ExportToExcelMaintenanceHistoryList(PlannedMaintenanceListViewModel request)
        {
            List<WorkHistoryExportViewModel> workBaseketHistoryList = new List<WorkHistoryExportViewModel>();
            List<WorkHistoryResponse> response = null;
            WorkHistoryRequest filter = SetMaintenanceHistoryListRequestObject(request);

            var value = new Dictionary<string, object>()
                {
                    { "request", filter }
                };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "PlannedMaintenance/ClosedWorkOrderHistory"));

            if (filter != null && !string.IsNullOrWhiteSpace(filter.VesselId))
            {
                response = await PostAsyncAutoPaged<WorkHistoryResponse>(requestUrl, value, 500);
            }

            if (response != null && response.Any())
            {
                foreach (WorkHistoryResponse item in response)
                {
                    WorkHistoryExportViewModel workBasketHistoryItem = new WorkHistoryExportViewModel();
                    workBasketHistoryItem.DoneDate = item.WOCompletedDate == null ? string.Empty : item.WOCompletedDate.GetValueOrDefault().ToString(Constants.DateFormat);
                    workBasketHistoryItem.ComponentName = item.ComponentName ?? string.Empty;
                    workBasketHistoryItem.JobName = item.JobName ?? string.Empty;
                    workBasketHistoryItem.Dept = item.DepartmentShortCode ?? string.Empty;
                    workBasketHistoryItem.Resp = item.ResponsibleRankShortCode ?? string.Empty;
                    workBasketHistoryItem.Type = item.JobTypeShortCode ?? string.Empty;
                    workBasketHistoryItem.Interval = item.Interval ?? string.Empty;
                    workBasketHistoryItem.IsCritical = item.IsCritical ? Constants.Yes : Constants.No;
                    workBaseketHistoryList.Add(workBasketHistoryItem);
                }
            }

            return workBaseketHistoryList;
        }

        /// <summary>
        /// Gets the inspection type with vessel type filter.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<List<InspectionTypeDetailViewModel>> GetInspectionTypeWithVesselTypeFilter(InspectionTypeDetailRequestViewModel input)
        {
            List<InspectionTypeDetailViewModel> responseVMList = new List<InspectionTypeDetailViewModel>();
            InspectionTypeDetailRequest request = new InspectionTypeDetailRequest();
            if (!String.IsNullOrEmpty(input.Ves_Id))
            {
                var decreptedString = CommonUtil.GetDecryptedVessel(_provider, input.Ves_Id);
                request.Ves_Id = decreptedString.Split(Constants.Separator)[0];

            }

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "InspectionManager/GetInspectionTypeFilterWithVesselType"));
            List<InspectionTypeDetail> response = await PostAsync<List<InspectionTypeDetail>>(requestUrl, CreateHttpContent(request));

            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    responseVMList.Add(new InspectionTypeDetailViewModel
                    {
                        InspectionHeaderType = x.InspectionHeaderType,
                        InspectionType = x.InspectionType,
                        InspectionTypeId = x.InspectionTypeId,
                        IsAuditType = x.IsAuditType,
                        IsInternal = x.IsInternal,
                        Type = x.Type
                    });
                });
            }

            return responseVMList;
        }

        /// <summary>
        /// Gets the certificate details.
        /// </summary>
        /// <param name="vesselCertificateId">The vessel certificate identifier.</param>
        /// <returns></returns>
        public async Task<CertificateDetailViewModel> GetCertificateDetails(string vesselCertificateId)
        {
            CertificateDetailViewModel result = null;
            CertificateDetailsResponse response = null;

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Certificate/GetCertificateDetails/" + vesselCertificateId));
            response = await GetAsync<CertificateDetailsResponse>(requestUrl);

            if (response != null)
            {
                result = new CertificateDetailViewModel
                {
                    CertificateName = (response.CertificateName + (!string.IsNullOrWhiteSpace(response.Annotation)
                                                                    ? (" - " + response.Annotation)
                                                                    : "")) ?? "-",
                    CertificateNumber = (response.CertificateCode + (response.CertificateExtendedNumber != null
                                                                    ? " " + response.CertificateExtendedNumber.ToString()
                                                                    : "")) ?? "-",
                    IssueDate = (response.IssueDate.HasValue
                                ? response.IssueDate.Value.ToString(Constants.DateFormat)
                                : "-") ?? "-",
                    IssuedBy = response.IssuedBy ?? "-",
                    Notes = response.Notes ?? "-",
                    VesselCertificateId = response.VesselCertificateId,
                    VesselId = response.VesselId,
                    VesselName = response.VesselName
                };
            }

            return result;
        }

        /// <summary>
        /// Gets the jsa graph and summary.
        /// </summary>
        /// <param name="requestViewModel">The request view model.</param>
        /// <returns></returns>
        public async Task<JobSafetyAnalysisDashboardViewModel> GetJSAGraphAndSummary(JobSafetyAnalysisDashboardRequestViewModel requestViewModel)
        {
            JobSafetyAnalysisDashboardRequest request = new JobSafetyAnalysisDashboardRequest { Item = requestViewModel.Item };

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/JSADashboardDetail"));
            List<JobSafetyAnalysisDashboardResponse> response = await PostAsync<List<JobSafetyAnalysisDashboardResponse>>(requestUrl, CreateHttpContent(request));
            JobSafetyAnalysisDashboardViewModel result = null;

            if (response != null && response.Any())
            {
                var item = response.FirstOrDefault();

                JSAListViewModel listViewModel = new JSAListViewModel { EncryptedVesselId = requestViewModel.EncryptedVesselId };
                listViewModel.ActiveMobileTabClass = Constants.Tab2;
                string overviewURL = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, listViewModel);

                listViewModel.StageName = EnumsHelper.GetKeyValue(JSAStage.Total);
                listViewModel.GridSubTitle = EnumsHelper.GetDescription(JSAStage.Total);
                listViewModel.ActiveMobileTabClass = Constants.Tab1;
                string totalUrl = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, listViewModel);

                listViewModel.StageName = EnumsHelper.GetKeyValue(JSAStage.Completed);
                listViewModel.GridSubTitle = EnumsHelper.GetDescription(JSAStage.Completed);
                listViewModel.ActiveMobileTabClass = Constants.Tab2;
                string completedUrl = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, listViewModel);

                listViewModel.StageName = EnumsHelper.GetKeyValue(JSAStage.Low);
                listViewModel.GridSubTitle = EnumsHelper.GetDescription(JSAStage.Low);
                string lowUrl = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, listViewModel);

                listViewModel.StageName = EnumsHelper.GetKeyValue(JSAStage.MidHigh);
                listViewModel.GridSubTitle = EnumsHelper.GetDescription(JSAStage.MidHigh);
                string midHighUrl = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, listViewModel);

                listViewModel.StageName = EnumsHelper.GetKeyValue(JSAStage.OverdueForClosure);
                listViewModel.GridSubTitle = EnumsHelper.GetDescription(JSAStage.OverdueForClosure);
                string overdueForClosureUrl = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, listViewModel);

                listViewModel.StageName = EnumsHelper.GetKeyValue(JSAStage.PendingOfficeApproval);
                listViewModel.GridSubTitle = EnumsHelper.GetDescription(JSAStage.PendingOfficeApproval);
                string pendingOfficeApprovalUrl = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, listViewModel);

                result = new JobSafetyAnalysisDashboardViewModel
                {
                    CompletedCount = item.CompletedCount,
                    ResidualRiskLowCount = item.ResidualRiskLowCount,
                    ResidualRiskMediumAndHighCount = item.ResidualRiskMediumAndHighCount,
                    TotalCount = item.ResidualRiskLowCount + item.ResidualRiskMediumAndHighCount,
                    OverdueForClosureCount = item.OverdueForClosureCount,
                    PendingOfficeApprovalCount = item.PendingOfficeApprovalCount,
                    OverviewURL = overviewURL,
                    LowUrl = lowUrl,
                    PendingOfficeApprovalUrl = pendingOfficeApprovalUrl,
                    CompletedUrl = completedUrl,
                    MidHighUrl = midHighUrl,
                    TotalUrl = totalUrl,
                    OverdueForClosureUrl = overdueForClosureUrl,
                    PendingOfficeApprovalPriority = item.PendingOfficeApprovalPriority,
                    OpenPriority = item.OpenPriority,
                    OverdueForClosurePriority = item.OverdueForClosurePriority,
                    ResidualRiskMediumAndHighPriority = item.ResidualRiskMediumAndHighPriority
                };
            }
            return result;
        }

        /// <summary>
        /// Gets the jsa list.
        /// </summary>
        /// <param name="requestVm">The request vm.</param>
        /// <returns></returns>
        public async Task<List<JsaJobDetailResponseViewModel>> GetJSAList(JSAListViewModel requestVm)
        {
            string decrypted = CommonUtil.GetDecryptedVessel(_provider, requestVm.EncryptedVesselId);

            JsaJobDetailRequest request = new JsaJobDetailRequest();
            request.VesselId = decrypted.Split(Constants.Separator)[0];
            request.StatusIdList = new List<string>();
            if (requestVm.IsSearchClicked)
            {
                request.StatusIdList = GetListOfStringCustom(requestVm.SelectedStatus);
                request.JobTypeIdList = GetListOfStringCustom(requestVm.SelectedSystemArea);
                List<string> riskFilters = GetListOfStringCustom(requestVm.SelectedRiskFilter);
                if (riskFilters != null && riskFilters.Any())
                {
                    if (riskFilters.Any(s => s.Equals(EnumsHelper.GetKeyValue(RiskAssessment.Total), StringComparison.OrdinalIgnoreCase)))
                    {
                        request.riskType = RiskAssessment.Total;
                    }
                    else if (riskFilters.Any(s => s.Equals(EnumsHelper.GetKeyValue(RiskAssessment.Average), StringComparison.OrdinalIgnoreCase)))
                    {
                        request.riskType = RiskAssessment.Average;
                    }
                    else
                    {
                        request.riskType = RiskAssessment.MediumOrHigher;
                    }
                }
                request.IsOverdueForClosure = requestVm.OverdueForClosure;
            }
            else
            {
                if (requestVm.StageName == EnumsHelper.GetKeyValue(JSAStage.Total))
                {
                    request.riskType = null;
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Planned));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.ApprovalPending));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.OfficeApprovalPending));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Approved));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Reopened));

                }
                else if (requestVm.StageName == EnumsHelper.GetKeyValue(JSAStage.Completed))
                {
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Completed));
                }
                else if (requestVm.StageName == EnumsHelper.GetKeyValue(JSAStage.Low))
                {
                    request.riskType = RiskAssessment.Average;
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Planned));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.ApprovalPending));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.OfficeApprovalPending));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Approved));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Reopened));
                }
                else if (requestVm.StageName == EnumsHelper.GetKeyValue(JSAStage.MidHigh))
                {
                    request.riskType = RiskAssessment.MediumOrHigher;
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Planned));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.ApprovalPending));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.OfficeApprovalPending));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Approved));
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Reopened));
                }
                else if (requestVm.StageName == EnumsHelper.GetKeyValue(JSAStage.OverdueForClosure))
                {
                    request.IsOverdueForClosure = true;
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.Approved));
                }
                else if (requestVm.StageName == EnumsHelper.GetKeyValue(JSAStage.PendingOfficeApproval))
                {
                    request.StatusIdList.Add(EnumsHelper.GetKeyValue(JSAStatus.OfficeApprovalPending));
                }
            }

            var value = new Dictionary<string, object>()
            {
                { "request", request }
            };

            List<JsaJobDetailResponse> response = new List<JsaJobDetailResponse>();
            List<JsaJobDetailResponseViewModel> result = new List<JsaJobDetailResponseViewModel>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/JsaJobListPaged"));
            response = await PostAsyncAutoPaged<JsaJobDetailResponse>(requestUrl, value, 500);
            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    JsaJobDetailResponseViewModel job = new JsaJobDetailResponseViewModel();
                    job.JSADetails = CommonUtil.GetEncryptedURL(_provider, Constants.JSADetails, new JSADetailsViewModel() { JobId = item.JobId });
                    job.JobId = item.JobId;
                    job.RefNo = item.UpdatedSite + " \\ " + item.RefNumber;
                    job.Title = item.JobName;
                    job.Status = item.Status;
                    job.MaxRisk = item.RiskFactorDescription;
                    job.SystemArea = item.JobType;
                    job.StartDate = item.EstimatedStartDate;
                    job.EndDate = item.EstimatedEndDate;
                    job.StatusKPI = GetStatusKPI(item.StatusId);
                    job.RiskKPI = GetRiskColor(item.RiskFactor);
                    result.Add(job);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the jsa list paged.
        /// </summary>
        /// <param name="pageRequest">The page request.</param>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public async Task<DataTablePageResponse<List<JsaJobDetailResponseViewModel>>> GetJSAListPaged(DataTablePageRequest<string> pageRequest, ApprovalJSARequestViewModel inputRequest)
        {
            PagedRequest pagedRequest = CommonUtil.TransformPagedRequest<string>(pageRequest);
            string vesselId = String.Empty;

            if (!string.IsNullOrWhiteSpace(inputRequest.EncryptedVesselId))
            {
                string decryptedVesselId = _provider.CreateProtector("Vessel").Unprotect(inputRequest.EncryptedVesselId);
                vesselId = decryptedVesselId.Split(Constants.Separator)[0];
            }

            JsaJobDetailRequest request = new JsaJobDetailRequest();
            request.FleetId = inputRequest.FleetId;
            request.MenuType = inputRequest.MenuType;
            request.VesselId = vesselId;

            var value = new Dictionary<string, object>()
            {
                { "request", request },
                {"pageRequest",pagedRequest }
            };

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaApprovalsListPaged"));
            PagedResponse<List<JsaJobDetailResponse>> response = await PostAsync<PagedResponse<List<JsaJobDetailResponse>>>(requestUrl, CreateHttpContent(value));

            DataTablePageResponse<List<JsaJobDetailResponseViewModel>> result = new DataTablePageResponse<List<JsaJobDetailResponseViewModel>>();

            result.Data = new List<JsaJobDetailResponseViewModel>();

            if (response != null && response.Result != null && response.Result.Any())
            {
                response.Result.ForEach(x =>
                {
                    result.Data.Add(new JsaJobDetailResponseViewModel
                    {
                        VesselName = x.VesselName,
                        JSADetails = CommonUtil.GetEncryptedURL(_provider, Constants.JSADetails, new JSADetailsViewModel() { JobId = x.JobId }),
                        JobId = x.JobId,
                        RefNo = x.UpdatedSite + " \\ " + x.RefNumber,
                        Title = x.JobName,
                        Status = x.Status,
                        MaxRisk = x.RiskFactorDescription,
                        SystemArea = x.JobType,
                        StartDate = x.EstimatedStartDate,
                        EndDate = x.EstimatedEndDate,
                        StatusKPI = GetStatusKPI(x.StatusId),
                        RiskKPI = GetRiskColor(x.RiskFactor)
                    });
                });
            }
            result.RecordsFiltered = response.TotalRecords;
            result.RecordsTotal = response.TotalRecords;

            return result;
        }

        /// <summary>
        /// Gets the list of string custom.
        /// </summary>
        /// <param name="lst">The LST.</param>
        /// <returns></returns>
        private List<string> GetListOfStringCustom(List<string> lst)
        {
            if (lst != null && lst.Any())
            {
                string item = lst.FirstOrDefault();
                if (item == null)
                {
                    return null;
                }
                else
                {
                    return new List<string>(item.Split(","));
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Sets the color of the risk.
        /// </summary>
        /// <param name="RiskFactor">The risk factor.</param>
        /// <returns></returns>
        private KPI GetRiskColor(int RiskFactor)
        {
            KPI RiskColor = KPI.Good;
            switch (RiskFactor)
            {
                case 1:
                case 2: RiskColor = KPI.Good; break;
                case 3:
                case 4: RiskColor = KPI.Warning; break;
                case 5:
                case 6: RiskColor = KPI.Critical; break;
                default: RiskColor = KPI.Good; break;
            }
            return RiskColor;
        }

        /// <summary>
        /// Gets the status kpi.
        /// </summary>
        /// <param name="StatusId">The status identifier.</param>
        /// <returns></returns>
        private KPI GetStatusKPI(string StatusId)
        {
            KPI JobStatusKPI = KPI.Good;

            if (StatusId == EnumsHelper.GetKeyValue<JSAStatus>(JSAStatus.Planned))
            {
                JobStatusKPI = KPI.Normal;
            }
            else if (StatusId == EnumsHelper.GetKeyValue<JSAStatus>(JSAStatus.Cancelled))
            {
                JobStatusKPI = KPI.PreWarning;
            }
            else if (StatusId == EnumsHelper.GetKeyValue<JSAStatus>(JSAStatus.Completed))
            {
                JobStatusKPI = KPI.Good;
            }
            else if (StatusId == EnumsHelper.GetKeyValue<JSAStatus>(JSAStatus.Rejected))
            {
                JobStatusKPI = KPI.Warning;
            }
            else if (StatusId == EnumsHelper.GetKeyValue<JSAStatus>(JSAStatus.ApprovalPending))
            {
                JobStatusKPI = KPI.Normal;
            }
            else if (StatusId == EnumsHelper.GetKeyValue<JSAStatus>(JSAStatus.OfficeApprovalPending))
            {
                JobStatusKPI = KPI.Normal;
            }

            else if (StatusId == EnumsHelper.GetKeyValue<JSAStatus>(JSAStatus.Reopened))
            {
                JobStatusKPI = KPI.Normal;
            }
            else if (StatusId == EnumsHelper.GetKeyValue<JSAStatus>(JSAStatus.Approved))
            {
                JobStatusKPI = KPI.Good;
            }
            return JobStatusKPI;
        }

        /// <summary>
        /// Gets the jsa statuses.
        /// </summary>
        /// <returns></returns>
        public List<JSAStatus> GetJSAStatuses()
        {
            return Enum.GetValues(typeof(JSAStatus)).Cast<JSAStatus>().Where(x => !x.Equals(JSAStatus.Deleted)).ToList();
        }


        /// <summary>
        /// Gets the system area.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<List<Lookup>> GetSystemArea(string vesselId)
        {
            string queryString = "vesselId=" + vesselId;
            List<Lookup> response = new List<Lookup>();
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/SystemArea"), queryString);
            response = await GetAsync<List<Lookup>>(requestUrl);
            return response;
        }

        /// <summary>
        /// Gets the risk filter.
        /// </summary>
        /// <returns></returns>
        public List<Lookup> GetRiskFilter()
        {
            List<Lookup> response = new List<Lookup>();
            foreach (RiskAssessment value in Enum.GetValues(typeof(RiskAssessment)).OfType<RiskAssessment>())
            {
                bool isValidFilter = true;
                RiskAssessment status = value;
                EnumValueDataAttribute data = status.GetEnumMetadata<RiskAssessment, EnumValueDataAttribute>();

                string currentStatus = "";
                switch (data.KeyValue)
                {
                    case "Total":
                        currentStatus = "All";
                        break;

                    case "Average":
                        currentStatus = "Low";
                        break;

                    case "MediumOrHigher":
                        currentStatus = "Medium & High";
                        break;

                    default:
                        isValidFilter = false;
                        break;

                }
                if (isValidFilter)
                {
                    response.Add(new Lookup { Description = currentStatus, Identifier = data.KeyValue });
                }
            }
            return response.OrderBy(x => x.Description).ToList();
        }

        #region JSA

        /// <summary>
        /// Gets the jsa job summary details.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public async Task<JSASummaryDetailsViewModel> GetJsaJobSummaryDetails(string jobId)
        {
            JSASummaryDetailsViewModel summaryDetails = new JSASummaryDetailsViewModel();
            JSAJobDetail jobDetail = new JSAJobDetail();
            string queryString = "jobId=" + jobId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaJobSummary"), queryString);
            jobDetail = await GetAsync<JSAJobDetail>(requestUrl);
            if (jobDetail != null)
            {
                summaryDetails.JsaName = jobDetail.JobName;
                summaryDetails.Responsibility = jobDetail.CrewForeName + ", " + jobDetail.CrewLastName;
                summaryDetails.SystemArea = jobDetail.JobTypeName ?? "";
                //if (jobDetail.AttributeList != null && jobDetail.AttributeList.Any(x => x.AttributeType == EnumsHelper.GetDescription<JSAAttributeLookupType>(JSAAttributeLookupType.CommunicationProtocol)))
                //{
                //    var communicationProtocol = jobDetail.AttributeList.FirstOrDefault(x => x.AttributeType == EnumsHelper.GetDescription<JSAAttributeLookupType>(JSAAttributeLookupType.CommunicationProtocol));
                //    if (communicationProtocol != null)
                //    {
                //        //CommunicationProtocolId = communicationProtocol.JslId;
                //        summaryDetails.EmergencyCommunicationProtocol = communicationProtocol.AttributeName;
                //        summaryDetails.CommunicationProtocolDescription = communicationProtocol.Other;
                //    }
                //}
                //summaryDetails.EmergencyCommunicationProtocol = jobDetail.CommunicationProtocol ?? "";
                //summaryDetails.CommunicationProtocolDescription
                summaryDetails.MeetingDate = jobDetail.MeetingDate.HasValue ? jobDetail.MeetingDate.Value.ToString(Constants.DateFormat) : "";
                summaryDetails.ProposedStartDate = jobDetail.StartDateTime.HasValue ? jobDetail.StartDateTime.Value.ToString(Constants.DateTime24HrFormat) : "";
                summaryDetails.EstimatedEndDate = jobDetail.EndDateTime.HasValue ? jobDetail.EndDateTime.Value.ToString(Constants.DateTime24HrFormat) : "";
                summaryDetails.CreatedBy = jobDetail.CreatedBy ?? "";
                summaryDetails.OfficeComments = jobDetail.OfficeComments ?? "";
                summaryDetails.Reasons = jobDetail.Reason ?? "";
                summaryDetails.Details = jobDetail.Details ?? "";
            }
            return summaryDetails;
        }

        /// <summary>
        /// Ges the jsa summaryt hazards details.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public async Task<List<JSAHazardDetailViewModel>> GeJSASummarytHazardsDetails(string jobId)
        {
            List<JSAHazardDetailViewModel> hazardDetail = new List<JSAHazardDetailViewModel>();
            string queryString = "jobId=" + jobId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaAdditionalHazardSummary"), queryString);
            List<JSAHazardDetail> hazard = null;
            hazard = await GetAsync<List<JSAHazardDetail>>(requestUrl);

            if (hazard != null && hazard.Any())
            {
                int hazardNumber = 0;
                foreach (JSAHazardDetail item in hazard)
                {
                    JSAHazardDetailViewModel hazardDetailsVM = new JSAHazardDetailViewModel();
                    hazardDetailsVM.JahId = item.JahId;
                    hazardDetailsVM.Description = item.Description;
                    hazardDetailsVM.HazardNumber = ++hazardNumber;
                    hazardDetailsVM.LikelihoodDescription = item.LikelihoodDescription;
                    if (!string.IsNullOrWhiteSpace(item.LikelihoodDescription))
                    {
                        hazardDetailsVM.LikelihoodColor = GetColorBrush(Convert.ToInt16(item.LikelihoodDescription.Substring(0, 1)));
                    }
                    else
                    {
                        hazardDetailsVM.LikelihoodColor = "Good";
                    }

                    if (!string.IsNullOrWhiteSpace(item.SeverityDescription))
                    {
                        hazardDetailsVM.SeverityColor = GetColorBrush(Convert.ToInt16(item.SeverityDescription.Substring(0, 1)));
                    }
                    else
                    {
                        hazardDetailsVM.SeverityColor = "Good";
                    }
                    hazardDetailsVM.SeverityDescription = item.SeverityDescription;

                    if (!string.IsNullOrWhiteSpace(item.RiskFactorDescription))
                    {
                        hazardDetailsVM.RiskColor = GetColorBrush(Convert.ToInt16(item.RiskFactorDescription.Substring(0, 1)));
                    }
                    else
                    {
                        hazardDetailsVM.RiskColor = "Good";
                    }

                    hazardDetailsVM.RiskFactorDescription = item.RiskFactorDescription;
                    hazardDetailsVM.WorkActivityDescription = Constants.AdditionalJobHazards;
                    hazardDetail.Add(hazardDetailsVM);
                }

            }
            return hazardDetail;
        }

        /// <summary>
        /// Gets the jsa risk assessment summary.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public async Task<List<JSARiskAssessmentDetailViewModel>> GetJsaRiskAssessmentSummary(string jobId)
        {
            List<JSARiskAssessmentDetailViewModel> workActivity = new List<JSARiskAssessmentDetailViewModel>();
            List<JSARiskAssessmentDetail> response = null;
            string queryString = "jobId=" + jobId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaRiskAssessmentSummary"), queryString);
            response = await GetAsync<List<JSARiskAssessmentDetail>>(requestUrl);

            if (response != null && response.Any())
            {
                foreach (JSARiskAssessmentDetail jSARisk in response)
                {

                    if (jSARisk.HazardList != null && jSARisk.HazardList.Any())
                    {
                        int MaxCount = 0;
                        string MaxCountDescription = "";
                        double Sum = 0;
                        int hazardNumber = 0;
                        string riskFactorLabel = jSARisk.Originator + "+" + jSARisk.RefNumber;

                        foreach (JSAHazardDetail hazard in jSARisk.HazardList)
                        {
                            if (hazard.HazardActive)
                            {
                                JSARiskAssessmentDetailViewModel hazardDetailsVM = new JSARiskAssessmentDetailViewModel();
                                hazardDetailsVM.JahId = hazard.JahId;
                                int? avgSum = jSARisk.HazardList.Sum(g => Int32.Parse(g.RiskFactorDescription.Substring(0, 1)));
                                hazardDetailsVM.AvgCount = (Sum / jSARisk.HazardList.Count).ToString();
                                if (hazardDetailsVM.AvgCount.Length > 4)
                                {
                                    hazardDetailsVM.AvgCount = hazardDetailsVM.AvgCount.Substring(0, 3);
                                }

                                Sum += Convert.ToInt32(hazard.RiskFactorDescription.Substring(0, 1));
                                if (MaxCount < Convert.ToInt32(hazard.RiskFactorDescription.Substring(0, 1)))
                                {
                                    MaxCount = Convert.ToInt32(hazard.RiskFactorDescription.Substring(0, 1));
                                    MaxCountDescription = hazard.RiskFactorDescription;
                                    hazardDetailsVM.ParentRiskColor = GetColorBrush(MaxCount);
                                    hazardDetailsVM.ParentriskFactorDescription = MaxCountDescription;
                                }

                                hazardDetailsVM.Description = hazard.Description;
                                hazardDetailsVM.HazardNumber = ++hazardNumber;
                                hazardDetailsVM.LikelihoodDescription = hazard.LikelihoodDescription;
                                if (!string.IsNullOrWhiteSpace(hazard.LikelihoodDescription))
                                {
                                    hazardDetailsVM.LikelihoodColor = GetColorBrush(Convert.ToInt16(hazard.LikelihoodDescription.Substring(0, 1)));
                                }
                                else
                                {
                                    hazardDetailsVM.LikelihoodColor = "Good";
                                }

                                if (!string.IsNullOrWhiteSpace(hazard.SeverityDescription))
                                {
                                    hazardDetailsVM.SeverityColor = GetColorBrush(Convert.ToInt16(hazard.SeverityDescription.Substring(0, 1)));
                                }
                                else
                                {
                                    hazardDetailsVM.SeverityColor = "Good";
                                }
                                hazardDetailsVM.SeverityDescription = hazard.SeverityDescription;

                                if (!string.IsNullOrWhiteSpace(hazard.RiskFactorDescription))
                                {
                                    hazardDetailsVM.RiskColor = GetColorBrush(Convert.ToInt16(hazard.RiskFactorDescription.Substring(0, 1)));
                                }
                                else
                                {
                                    hazardDetailsVM.RiskColor = "Good";
                                }
                                hazardDetailsVM.RiskFactorDescription = hazard.RiskFactorDescription;

                                if (!string.IsNullOrWhiteSpace(hazard.InitialRiskFactorDescription))
                                {
                                    hazardDetailsVM.InitialRiskColor = GetColorBrush(Convert.ToInt32(hazard.InitialRiskFactorDescription.Substring(0, 1)));
                                }

                                if (!string.IsNullOrWhiteSpace(hazard.InitialRiskFactorDescription) && !string.IsNullOrWhiteSpace(hazard.RiskFactorDescription) && !string.IsNullOrWhiteSpace(hazard.RghId))
                                {
                                    hazardDetailsVM.IsInitialRiskVisible = Convert.ToInt32(hazard.InitialRiskFactorDescription.Substring(0, 1)) != Convert.ToInt32(hazard.RiskFactorDescription.Substring(0, 1));
                                }
                                else
                                {
                                    hazardDetailsVM.IsInitialRiskVisible = false;
                                }

                                hazardDetailsVM.InitialRiskFactorDescription = "Initial Risk " + hazard.InitialRiskFactorDescription;

                                //hazardDetailsVM.WorkActivityDescription = jSARisk.WorkActivityDescription;
                                hazardDetailsVM.WorkActivityDescription = riskFactorLabel + "+" + jSARisk.WorkActivityDescription;
                                workActivity.Add(hazardDetailsVM);
                            }
                        }
                    }
                }
            }

            return workActivity;
        }

        /// <summary>
        /// Gets the color brush.
        /// </summary>
        /// <param name="count">The count.</param>
        /// <returns>
        /// Single KPI
        /// </returns>
        private string GetColorBrush(int count)
        {
            switch (count)
            {
                case 1:
                case 2: return "Good";
                case 3:
                case 4: return "Warning";
                case 5:
                case 6: return "Critical";
                default: return "Good";
            }
        }

        /// <summary>
        /// Gets the jsa crew summary.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public async Task<List<JSACrewDetailViewModel>> GetJsaCrewSummary(string jobId)
        {
            List<JSACrewDetailViewModel> crewList = new List<JSACrewDetailViewModel>();
            string queryString = "jobId=" + jobId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaCrewSummary"), queryString);
            List<JSACrewDetail> response = await GetAsync<List<JSACrewDetail>>(requestUrl);

            if (response != null && response.Any())
            {
                foreach (JSACrewDetail crew in response.Where(x => x.IsDeleted == false))
                {
                    JSACrewDetailViewModel crewVM = new JSACrewDetailViewModel();
                    crewVM.CrewId = crew.CrewId ?? "";
                    crewVM.CRW_ID_TP = crew.CRW_ID_TP ?? "";
                    crewVM.CrewFullName = (!string.IsNullOrWhiteSpace(crew.LastName) ? crew.LastName : string.Empty) + (!string.IsNullOrWhiteSpace(crew.FirstName) ? ", " + crew.FirstName : string.Empty);
                    crewVM.Rank = crew.Rank ?? "";
                    crewVM.Notes = crew.Notes ?? "";
                    crewVM.IsNotesVisible = !string.IsNullOrWhiteSpace(crew.Notes);
                    crewVM.Department = crew.Department ?? "";
                    crewVM.HasMeetingAttended = crew.HasMeetingAttended;
                    crewVM.HasWorkInstructionUnderstood = crew.HasWorkInstructionUnderstood;
                    crewVM.HasSatisfiedWithPrecaution = crew.HasSatisfiedWithPrecaution;
                    crewVM.OtherCrewName = crew.OtherCrewName ?? "";
                    crewVM.OtherCompany = crew.OtherCompany ?? "";
                    crewVM.OtherIdentityNo = crew.OtherIdentityNo ?? "";
                    crewVM.OtherPosition = crew.OtherPosition ?? "";
                    crewList.Add(crewVM);
                }
            }

            return crewList;
        }

        /// <summary>
        /// Gets the jsa attribute summary.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public async Task<List<JSAAttributeDetailViewModel>> GetJsaAttributeSummary(string jobId)
        {
            List<JSAAttributeDetailViewModel> jsaAttributeCollection = new List<JSAAttributeDetailViewModel>();

            string queryString = "jobId=" + jobId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaAttributeSummary"), queryString);
            List<JSAAttributeDetail> response = await GetAsync<List<JSAAttributeDetail>>(requestUrl);
            if (response != null && response.Any())
            {
                //List<JSAAttributeDetail> filteredResponse = response.Where(x => x.AttributeType == EnumsHelper.GetDescription<JSAAttributeLookupType>(JSAAttributeLookupType.PermitType) && x.IsDeleted == false).ToList();
                foreach (JSAAttributeDetail item in response)
                {
                    JSAAttributeDetailViewModel jsaAttribute = new JSAAttributeDetailViewModel();
                    jsaAttribute.AttributeName = item.AttributeName;
                    jsaAttribute.Other = item.Other;
                    jsaAttribute.PermitNumber = item.PermitNumber;
                    jsaAttribute.PermitRequestDateTime = item.PermitRequestDateTime;
                    jsaAttribute.ValidityToDateTime = item.ValidityToDateTime;
                    jsaAttribute.ValidityFromDateTime = item.ValidityFromDateTime;
                    jsaAttribute.AttributeType = item.AttributeType;
                    jsaAttribute.IsDeleted = item.IsDeleted;
                    jsaAttribute.SortOrder = item.SortOrder;
                    jsaAttribute.JslId = item.JslId;

                    jsaAttributeCollection.Add(jsaAttribute);
                }
            }

            return jsaAttributeCollection;
        }


        /// <summary>
        /// Gets the jsa task break down summary.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public async Task<List<JSATaskBreakdownDetailViewModel>> GetJsaTaskBreakDownSummary(string jobId)
        {
            List<JSATaskBreakdownDetailViewModel> taskBreakdownList = new List<JSATaskBreakdownDetailViewModel>();

            string queryString = "jobId=" + jobId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaTaskBreakDownSummary"), queryString);
            List<JSATaskBreakdownDetail> response = await GetAsync<List<JSATaskBreakdownDetail>>(requestUrl);

            if (response != null && response.Any())
            {
                List<JSATaskBreakdownDetail> filteredResponse = response.Where(x => x.IsDeleted == false).OrderBy(x => x.EstCompletionDateTime).ToList();
                int _seqNum = 0;
                foreach (JSATaskBreakdownDetail task in filteredResponse)
                {
                    _seqNum++;
                    JSATaskBreakdownDetailViewModel taskBreakdown = new JSATaskBreakdownDetailViewModel();
                    taskBreakdown.EstCompletionDateTime = task.EstCompletionDateTime;
                    taskBreakdown.Rank = task.Rank;
                    taskBreakdown.ResponsiblePerson = task.CrewForeName + " " + task.CrewLastName;
                    taskBreakdown.SeqNo = _seqNum;
                    taskBreakdown.StepDescription = task.StepDescription;
                    taskBreakdown.TaskDescription = task.TaskDescription;
                    taskBreakdownList.Add(taskBreakdown);
                }
            }

            return taskBreakdownList;
        }

        /// <summary>
        /// Gets the jsa details header summary.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public async Task<JsaJobDetailResponseViewModel> GetJSADetailsHeaderSummary(string jobId)
        {
            JsaJobDetailResponseViewModel HeaderSummary = new JsaJobDetailResponseViewModel();

            string queryString = "jobId=" + jobId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaJobById"), queryString);
            JsaJobDetailResponse response = await GetAsync<JsaJobDetailResponse>(requestUrl);

            if (response != null)
            {
                HeaderSummary.RefNo = response.UpdatedSite + " \\ " + response.RefNumber;
                HeaderSummary.Title = response.JobName;
                HeaderSummary.Status = response.Status;
                HeaderSummary.StatusKPI = GetStatusKPI(response.StatusId);
                HeaderSummary.MaxRisk = response.RiskFactorDescription;
                HeaderSummary.RiskKPI = GetRiskColor(response.RiskFactor);
                HeaderSummary.SimultaneousJobVisible = response.IsSimultaneousJobVisible;
            }
            return HeaderSummary;
        }

        /// <summary>
        /// TO Get Jsa Hazard Additional Details
        /// </summary>
        /// <param name="jahId"></param>
        /// <returns></returns>
        public async Task<JSAHazardDetailsViewModel> GetJSAHazardAdditionalDetails(string jahId)
        {
            JSAHazardDetailsViewModel hazardDetailVM = new JSAHazardDetailsViewModel();
            JSAHazardDetail response = null;
            string queryString = "jahId=" + jahId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJSAHazardAdditionalDetail"), queryString);
            response = await GetAsync<JSAHazardDetail>(requestUrl);

            if (response != null)
            {
                hazardDetailVM.JahId = response.JahId;
                hazardDetailVM.Description = response.Description;
                hazardDetailVM.Consequences = string.IsNullOrEmpty(response.Consequences) ? "" : response.Consequences;
                hazardDetailVM.ControlMeasures = string.IsNullOrEmpty(response.ControlMeasures) ? "" : response.ControlMeasures;
                hazardDetailVM.ReportedDate = response.ReportedDate.HasValue ? response.ReportedDate.Value.ToString(Constants.DateFormat) : "";
                hazardDetailVM.FurtherControlMeasure = "";
                hazardDetailVM.FurtherControlMeasureCount = 0;
                foreach (JSAFurtherControlMeasure furtherControlMeasure in response.FurtherControlMeasure)
                {
                    hazardDetailVM.FurtherControlMeasure += furtherControlMeasure.FurtherRisk + "\n";
                    hazardDetailVM.FurtherControlMeasureCount++;
                }
                hazardDetailVM.LikelihoodDescription = response.LikelihoodDescription;
                hazardDetailVM.LikelihoodDefinition = response.LikelihoodDefinition;
                if (!string.IsNullOrWhiteSpace(response.LikelihoodDescription))
                {
                    hazardDetailVM.LikelihoodColor = GetColorBrush(Convert.ToInt16(response.LikelihoodDescription.Substring(0, 1)));
                }
                else
                {
                    hazardDetailVM.LikelihoodColor = "Good";
                }

                if (!string.IsNullOrWhiteSpace(response.SeverityDescription))
                {
                    hazardDetailVM.SeverityColor = GetColorBrush(Convert.ToInt16(response.SeverityDescription.Substring(0, 1)));
                }
                else
                {
                    hazardDetailVM.SeverityColor = "Good";
                }
                hazardDetailVM.SeverityDescription = response.SeverityDescription;
                hazardDetailVM.SeverityDefinition = response.SeverityDefinition;

                if (!string.IsNullOrWhiteSpace(response.RiskFactorDescription))
                {
                    hazardDetailVM.RiskColor = GetColorBrush(Convert.ToInt16(response.RiskFactorDescription.Substring(0, 1)));
                }
                else
                {
                    hazardDetailVM.RiskColor = "Good";
                }
                hazardDetailVM.RiskFactorDescription = response.RiskFactorDescription;
                hazardDetailVM.RiskFactorDefinition = response.RiskFactorDefinition;

            }
            return hazardDetailVM;
        }

        /// <summary>
        /// Gets the jsa component summary
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns></returns>
        public async Task<List<JSAComponentDetailViewModel>> GetJsaComponentSummary(string jobId)
        {
            List<JSAComponentDetailViewModel> componentList = new List<JSAComponentDetailViewModel>();
            string queryString = "jobId=" + jobId;
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaComponentSummary"), queryString);
            List<JSAComponentDetail> response = await GetAsync<List<JSAComponentDetail>>(requestUrl);

            if (response != null && response.Any())
            {
                foreach (JSAComponentDetail component in response.Where(x => x.IsDeleted == false))
                {
                    JSAComponentDetailViewModel componentVM = new JSAComponentDetailViewModel();
                    componentVM.ComponentName = component.ComponentName ?? "";
                    componentVM.JobName = component.JobName ?? "";
                    componentVM.Position = component.Position ?? "";
                    componentVM.Maker = component.Maker ?? "";
                    componentVM.Model = component.Model ?? "";
                    componentVM.ClassCode = component.ClassCode ?? "";
                    componentVM.ReportWorkDoneDate = component.ReportWorkDoneDate;
                    componentVM.WorkOrderStatusDescription = component.WorkOrderStatusDescription ?? "";
                    componentList.Add(componentVM);
                }
            }
            return componentList;
        }

        /// <summary>
        /// Gets the Jsa Simultaneous Jobs
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<List<JsaJobDetailResponseViewModel>> GetJsaSimultaneousJobs(JSASimultaneousJobRequestViewModel request)
        {
            List<JsaJobDetailResponseViewModel> jobList = new List<JsaJobDetailResponseViewModel>();
            JSASimultaneousJobRequest inputRequest = new JSASimultaneousJobRequest();

            inputRequest.StartDate = Convert.ToDateTime(request.StartDateFromUI);
            inputRequest.EndDate = Convert.ToDateTime(request.EndDateFromUI);
            inputRequest.JobId = request.JobId;
            inputRequest.VesselId = request.VesselId;

            var requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJsaSimultaneousJobs"));
            List<JsaJobDetailResponse> response = await PostAsync<List<JsaJobDetailResponse>>(requestUrl, CreateHttpContent(inputRequest));

            if (response != null && response.Any())
            {
                foreach (JsaJobDetailResponse job in response)
                {
                    JsaJobDetailResponseViewModel jobVM = new JsaJobDetailResponseViewModel();
                    jobVM.jsaNo = job.UpdatedSite + " / " + job.RefNumber;
                    jobVM.JobName = job.JobName;
                    jobVM.Responsibility = job.LastName + ", " + job.ForeName;
                    jobVM.SystemArea = job.JobType ?? "";
                    jobVM.JobDetail = job.JobDetail;
                    jobVM.JobId = job.JobId;
                    jobVM.StartDateUI = job.EstimatedStartDate.ToString(Constants.DateFormat);
                    jobVM.EndDateUI = job.EstimatedEndDate.ToString(Constants.DateFormat);
                    jobVM.Status = job.Status;
                    jobList.Add(jobVM);
                }
            }
            return jobList;
        }

        public async Task<string> GetJSAMeetingGuidelines()
        {
            string MeetingGuidelines = "";
            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "JSA/GetJSAAttributeLookup"));
            List<JSAAttributeLookup> response = await GetAsync<List<JSAAttributeLookup>>(requestUrl);
            if (response != null && response.Any())
            {
                if (response.Any(x => x.JslId == Constants.MeetingGuidelinesKey))
                {
                    MeetingGuidelines = response.FirstOrDefault(x => x.JslId == Constants.MeetingGuidelinesKey).ParameterDetail;
                }
            }
            return MeetingGuidelines;
        }

        #endregion

        #region Vessel

        /// <summary>
        /// Gets the logs and possible work flows.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<LogsAndPossibleWorkFlowsResponseViewModel> GetLogsAndFirstPossibleWorkflow(LogsAndPossibleWorkFlowsRequest request)
        {
            LogsAndPossibleWorkFlowsResponse response = null;
            LogsAndPossibleWorkFlowsResponseViewModel result = new LogsAndPossibleWorkFlowsResponseViewModel();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vessel/GetLogsAndPossibleWorkFlows"));

            if (request != null && !string.IsNullOrWhiteSpace(request.ModuleIdentifier) && !string.IsNullOrWhiteSpace(request.RecordId))
            {
                response = await PostAsync<LogsAndPossibleWorkFlowsResponse>(requestUrl, CreateHttpContent(request));
            }

            if (response != null)
            {
                if (response.PreviousLogsDetails != null && response.PreviousLogsDetails.Any())
                {
                    result.PreviousLogsDetails = new List<WorkflowDetailViewModel>();
                    response.PreviousLogsDetails.ForEach(x =>
                    {
                        result.PreviousLogsDetails.Add(new WorkflowDetailViewModel
                        {
                            ActivityName = x.LogStatus,
                            PerformedByRoleName = x.PerformedByRoleName,
                            PerformedByName = x.PerformedByName,
                            PerfomedUTCDate = x.PerfomedUTCDate,
                            IsApplicableToVessel = x.RoleApplicableToVessel,
                            IsDone = true
                        });
                    });
                }
                if (response.PossibleWorkflowDetails != null && response.PossibleWorkflowDetails.Any())
                {
                    IGrouping<string, PossibleWorkflowDetails> topWorkflow = response.PossibleWorkflowDetails.GroupBy(x => x.WorkFlowId).ToList().FirstOrDefault();
                    result.WorkflowGroupCount = response.PossibleWorkflowDetails.GroupBy(x => x.WorkFlowId).Count();
                    List<IGrouping<int?, PossibleWorkflowDetails>> groupedList = topWorkflow.GroupBy(x => x.ActivityPriority).ToList();
                    result.PossibleWorkflowDetails = new List<WorkflowDetailViewModel>();

                    foreach (var item in groupedList)
                    {
                        PossibleWorkflowDetails obj = item.FirstOrDefault();

                        result.PossibleWorkflowDetails.Add(new WorkflowDetailViewModel
                        {
                            ActivityName = obj.StatusDescription,
                            IsApplicableToVessel = obj.RoleApplicableToVessel,
                            PerformedByRoleName = String.Join(", ", item.Select(x => x.RoleName).ToList()),
                            IsDone = false
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the logs and possible workflow.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<JsaPossibleScenarioViewModel> GetLogsAndPossibleWorkflow(LogsAndPossibleWorkFlowsRequest request)
        {
            LogsAndPossibleWorkFlowsResponse response = null;
            JsaPossibleScenarioViewModel result = new JsaPossibleScenarioViewModel();
            result.WorkflowList = new List<WorkflowActivityDetailViewModel>();
            result.ActivityList = new List<string>();

            Uri requestUrl = CreateRequestUri(_client.BaseAddress, string.Format(System.Globalization.CultureInfo.InvariantCulture, "Vessel/GetLogsAndPossibleWorkFlows"));

            if (request != null && !string.IsNullOrWhiteSpace(request.ModuleIdentifier) && !string.IsNullOrWhiteSpace(request.RecordId))
            {
                response = await PostAsync<LogsAndPossibleWorkFlowsResponse>(requestUrl, CreateHttpContent(request));
            }

            if (response != null && response.PossibleWorkflowDetails != null && response.PossibleWorkflowDetails.Any())
            {
                List<IGrouping<string, PossibleWorkflowDetails>> workflows = response.PossibleWorkflowDetails.GroupBy(x => x.WorkFlowId).ToList();
                workflows = workflows.Skip(1).ToList();
                WorkflowActivityDetailViewModel workflowActivityDetailViewModel;
                foreach (var item in workflows)
                {
                    workflowActivityDetailViewModel = new WorkflowActivityDetailViewModel();
                    workflowActivityDetailViewModel.WorkflowName = item.FirstOrDefault().WorkFlowName;
                    List<IGrouping<int?, PossibleWorkflowDetails>> workflowgroupedList = item.GroupBy(x => x.ActivityPriority).ToList();

                    List<WorkflowDetailViewModel> workflowDetailViewModels = new List<WorkflowDetailViewModel>();
                    foreach (var workflowGroup in workflowgroupedList)
                    {
                        List<IGrouping<string, PossibleWorkflowDetails>> statusGroupedList = workflowGroup.GroupBy(x => x.StatusDescription).ToList();
                        foreach (var obj in statusGroupedList)
                        {
                            var firstItem = obj.FirstOrDefault();
                            workflowDetailViewModels.Add(new WorkflowDetailViewModel
                            {
                                ActivityName = obj.Key,
                                PerformedByRoleName = String.Join(", ", item.Where(x => x.StatusDescription.Equals(obj.Key)).Select(x => x.RoleName).ToList()),
                                SortOrder = firstItem.SortOrder.GetValueOrDefault(),
                            });
                        }
                    }
                    workflowActivityDetailViewModel.PossibleWorkflowDetails = workflowDetailViewModels;

                    result.WorkflowList.Add(workflowActivityDetailViewModel);
                }

                result.ActivityList = response.PossibleWorkflowDetails.OrderBy(x => x.SortOrder.GetValueOrDefault()).Select(x => x.StatusDescription).Distinct().ToList();
            }

            return result;
        }

        #endregion
    }
}
