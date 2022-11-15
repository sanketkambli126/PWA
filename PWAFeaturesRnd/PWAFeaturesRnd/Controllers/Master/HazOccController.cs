using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Lookup;
using PWAFeaturesRnd.Models.Report.HazardousOccurrences;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.HazardousOccurrences;
using PWAFeaturesRnd.ViewModels.HazOcc;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.VesselManagement;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// HazOccController
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class HazOccController : AuthenticatedController
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly MarineClient _marineClient;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// The notification client
        /// </summary>
		private NotificationClient _notificationClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HazOccController" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="notificationClient">The notification client.</param>
        public HazOccController(MarineClient client, IDataProtectionProvider provider, NotificationClient notificationClient)
        {
            _marineClient = client;
            _provider = provider;
            _notificationClient = notificationClient;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }



        /// <summary>
        /// Lists the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult List(string request, string vesselId)
        {
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);

            HazOccListViewModel HazOccModel = CommonUtil.GetDecryptedRequest<HazOccListViewModel>(_provider, "HazOccList", request);
            HazOccModel = GetHazOccFilterDetails(HazOccModel);
            string EncryptedRequest = CommonUtil.GetEncryptedURL(_provider, "HazOccList", HazOccModel);

            SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey), null, EncryptedRequest);
            RemoveSessionFilter(_provider, EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey), null, decreptedString.Split(Constants.Separator)[0]);

            string data = _provider.CreateProtector("HazOccList").Unprotect(GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey)));
            HazOccListViewModel viewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<HazOccListViewModel>(data);
            HazOccListViewModel clearButtonUrl = new HazOccListViewModel
            {
                EncryptedVesselId = vesselId,
                StartDate = DateTime.Now.Date.AddMonths(-12).AddDays(1),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59),
                StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.Total),
                StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.Total)
            };
            string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey);
            viewModel.EncryptedVesselId = vesselId;
            viewModel.ClearButtonUrl = _provider.CreateProtector("HazOccList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(clearButtonUrl));
            viewModel.VesselName = decreptedString.Split(Constants.Separator)[1];
            viewModel.ActiveMobileTabClass = SetTab(pageKey, viewModel.ActiveMobileTabClass, Constants.Tab2);
            return View(viewModel);
        }

        /// <summary>
        /// Detailses the specified haz occ details.
        /// </summary>
        /// <param name="HazOccDetails">The haz occ details.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string HazOccDetails, string VesselId, bool IsVesselChanged, string context)
        {
            _marineClient.AccessToken = GetAccessToken();
            
            if (IsVesselChanged)
            {
                HazOccListViewModel hazOccListViewModel = new HazOccListViewModel();
                hazOccListViewModel.StartDate = DateTime.Now.Date.AddMonths(-12).AddDays(1);
                hazOccListViewModel.EndDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                hazOccListViewModel.StageName = EnumsHelper.GetKeyValue(HazOccListStageFilter.Total);
                hazOccListViewModel.StageDescription = EnumsHelper.GetDescription(HazOccListStageFilter.Total);
                hazOccListViewModel.EncryptedVesselId = VesselId;
                string hazOccListURL = _provider.CreateProtector("HazOccList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(hazOccListViewModel));

                return RedirectToAction("List", new { request = hazOccListURL, VesselId = VesselId });
            }

            string incidentId = null;
            
            if(!string.IsNullOrWhiteSpace(HazOccDetails))
            {
                string data = _provider.CreateProtector("HazOccDetails").Unprotect(HazOccDetails);
                HazOccDetailsViewModel viewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<HazOccDetailsViewModel>(data);
                incidentId = viewModel.IncidentId;
                if(string.IsNullOrWhiteSpace(VesselId))
                {
                    VesselId = viewModel.EncryptedVesselId;
                }
            }
            if(!string.IsNullOrWhiteSpace(context))
            {
                ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
                incidentId = contextParameter.IncidentId;
            }           
            HazOccDetailsViewModel hazOccDetails = await _marineClient.PostGetSelectedHazOccByIncidentId(incidentId, VesselId);
            string[] contextParams = { incidentId };
            string[] messageParams = { hazOccDetails.ShipReferenceNumber, hazOccDetails.Type };

            hazOccDetails.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.HazOcc, hazOccDetails.VesselId, hazOccDetails.VesselName, contextParams, messageParams, hazOccDetails.IncidentId);

            string hazOccDetailsEncrypt = _provider.CreateProtector("HazOccDetails").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(hazOccDetails));
            string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.HazOccDetailsPageKey);
            SetSessionDetail(pageKey, EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey), hazOccDetailsEncrypt);
           
            hazOccDetails.ActiveMobileTabClass = SetTab(pageKey, hazOccDetails.ActiveMobileTabClass, Constants.DropdownTab1);
            hazOccDetails.IsFromViewRecord = IsFromViewRecordVal(context);
            return View(hazOccDetails);
        }

        /// <summary>
        /// Gets the haz occ list.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// HazOccPreviewResponse
        /// </returns>
        public async Task<IActionResult> GetHazOccs(HazOccListViewModel request)
        {
            HazOccPreviewRequestViewModel parameter = new HazOccPreviewRequestViewModel();
            parameter.VesselId = request.EncryptedVesselId;
            parameter.StageName = request.StageName;
            parameter.StartDate = request.StartDate;
            parameter.EndDate = request.EndDate;
            parameter.IncidentStatus = GetListOfString(request.SelectedIncidentStatus);
            parameter.IncidentType = GetListOfString(request.SelectedIncidentTypes);
            parameter.IncidentSeverity = GetListOfString(request.SelectedIncidentSeveritys);
            parameter.IsSearchedClick = request.IsSearchedClick;
            _marineClient.AccessToken = GetAccessToken();
            List<HazOccPreviewResponseViewModel> response = await _marineClient.PostGetHazOccList(parameter);

            if (response != null && response.Any())
            {
                RecordDiscussionRequestViewModel request1 = new RecordDiscussionRequestViewModel();
                request1.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.HazOcc));
                request1.ReferenceIds = response.Select(x => x.Identifier).ToList();

                _notificationClient.AccessToken = GetAccessToken();
                List<RecordDiscussionResponse> DiscussionAndNotesCountList = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(request1);

                foreach (var item in DiscussionAndNotesCountList.Where(x => x.ChannelCount > 0 || x.NotesCount > 0))
                {
                    foreach (var hazocc in response.Where(x => x.Identifier == item.ReferenceIdentifier))
                    {
                        NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
                        {
                            CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.HazOcc)),
                            ReferenceIdentifier = item.ReferenceIdentifier
                        };

                        hazocc.ChannelCount = item.ChannelCount;
                        hazocc.NotesCount = item.NotesCount;
                        hazocc.MessageDetailsJSON = JsonConvert.SerializeObject(newMessageDetails);
                    }
                }
            }
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the list of string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [NonAction]
        private List<string> GetListOfString(string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Split(',').ToList();
            }
            else
            {
                return new List<string>();
            }

        }

        /// <summary>
        /// Maintains the filter parameters.
        /// </summary>
        /// <returns></returns>
        public IActionResult MaintainFilterParameters()
        {
            string HazOccListFilter = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey));
            string vesselId = GetSessionVesselFilter(EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey));

            if (!string.IsNullOrWhiteSpace(HazOccListFilter) && !string.IsNullOrWhiteSpace(vesselId))
            {
                string decryptedViewModel = _provider.CreateProtector("HazOccList").Unprotect(HazOccListFilter);
                HazOccListViewModel viewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<HazOccListViewModel>(decryptedViewModel);
                return new JsonResult(new { data = viewModel, isTempDataExist = true });
            }
            else
            {
                return new JsonResult(new { data = string.Empty, isTempDataExist = false });
            }
        }

        /// <summary>
        /// Sets the page parameter.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public IActionResult SetPageParameter(HazOccListViewModel inputRequest)
        {
            string hazOccURL = _provider.CreateProtector("HazOccList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(inputRequest));
            SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey), hazOccURL, inputRequest.EncryptedVesselId);

            return new JsonResult(new { data = inputRequest });
        }

        /// <summary>
        /// Sets the summary filter in temporary data.
        /// </summary>
        /// <param name="hazOccUrl">The haz occ URL.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult SetSummaryFilterInTempData(string hazOccUrl, string vesselId)
        {
            string data = _provider.CreateProtector("HazOccList").Unprotect(hazOccUrl);
            HazOccListViewModel viewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<HazOccListViewModel>(data);
            viewModel = GetHazOccFilterDetails(viewModel);
            viewModel.EncryptedVesselId = vesselId;
            if (viewModel.ActiveMobileTabClass == null)
            { 
                viewModel.ActiveMobileTabClass = SetTab(EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey), viewModel.ActiveMobileTabClass, Constants.Tab2);
            }
            string hazOccURL = _provider.CreateProtector("HazOccList").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(viewModel));
            SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.HazOccListPageKey), hazOccURL, vesselId);
            return new JsonResult(new { data = viewModel });
        }

        /// <summary>
        /// Gets the status filter.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetStatusFilter()
        {
            _marineClient.AccessToken = GetAccessToken();
            List<Lookup> response = await _marineClient.GetHazoccStatusFilter();
            List<TreeViewModel<Lookup>> treeList = new List<TreeViewModel<Lookup>>();
            List<TreeViewModel<Lookup>> childItems = new List<TreeViewModel<Lookup>>();

            TreeViewModel<Lookup> DeletedOption = new TreeViewModel<Lookup>
            {
                Title = "Deleted",
                Expanded = false,
                Key = "deleted",
                Checkbox = true,
                Lazy = false,
                Tooltip = "Deleted",
                Children = new List<TreeViewModel<Lookup>>(),
            };
            treeList.Add(DeletedOption);

            TreeViewModel<Lookup> AllOption = new TreeViewModel<Lookup>
            {
                Title = Constants.All,
                Expanded = true,
                Key = "",
                Checkbox = true,
                Lazy = false,
                Tooltip = Constants.All,
                Children = new List<TreeViewModel<Lookup>>(),
            };

            if (response != null && response.Any())
            {
                AllOption.Children.AddRange(response.Select(y => new TreeViewModel<Lookup>
                {
                    Key = y.Identifier,
                    Title = y.Description,
                    Tooltip = y.LongDescription ?? y.Description,
                    Expanded = false,
                    Checkbox = true,
                    Lazy = false,
                    Children = null
                }));
            }
            treeList.Add(AllOption);

            return new JsonResult(treeList);
        }

        /// <summary>
        /// Gets the type filter.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetTypeFilter(string encryptedVesselId)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<Lookup> response = await _marineClient.GetHazoccTypeFilter();
            //pending to implement
            List<VesselSpecificAttributeType> permissionList = new List<VesselSpecificAttributeType>
            {
                VesselSpecificAttributeType.VesselhasAccesstoMedicalManager
            };
            List<VesselSpecificAttribute> attributeList = await _marineClient.GetVesselSpecificAttributes(encryptedVesselId, permissionList);

            bool _hasAccessToMedicalManager = false;
            if (attributeList != null && attributeList.Any(attribute => attribute.VlkIdLookupCode == EnumsHelper.GetKeyValue(VesselSpecificAttributeType.VesselhasAccesstoMedicalManager)))
            {
                VesselSpecificAttribute medicalManager = attributeList.FirstOrDefault(attribute => attribute.VlkIdLookupCode == EnumsHelper.GetKeyValue(VesselSpecificAttributeType.VesselhasAccesstoMedicalManager));
                _hasAccessToMedicalManager = medicalManager != null && medicalManager.AttributeDetail != null ? medicalManager.AttributeDetail.AttributeBoolValue : false;
            }
            else
            {
                _hasAccessToMedicalManager = false;
            }

            List<TreeViewModel<Lookup>> treeList = new List<TreeViewModel<Lookup>>();
            List<TreeViewModel<Lookup>> childItems = new List<TreeViewModel<Lookup>>();

            TreeViewModel<Lookup> AllOption = new TreeViewModel<Lookup>
            {
                Title = Constants.All,
                Expanded = true,
                Key = "",
                Checkbox = true,
                Lazy = false,
                Tooltip = Constants.All,
                Children = new List<TreeViewModel<Lookup>>(),
            };

            if (response != null && response.Any())
            {
                foreach (var item in response)
                {
                    if (item.Identifier == EnumsHelper.GetKeyValue(HazOccTypeCodes.ILL) && !_hasAccessToMedicalManager)
                    {
                        continue;
                    }
                    else
                    {
                        AllOption.Children.Add(new TreeViewModel<Lookup>
                        {
                            Key = item.Identifier,
                            Title = item.Description,
                            Tooltip = item.LongDescription ?? item.Description,
                            Expanded = false,
                            Checkbox = true,
                            Lazy = false,
                            Children = null
                        });
                    }

                }
            }
            treeList.Add(AllOption);

            return new JsonResult(treeList);
        }

        /// <summary>
        /// Gets the severity filter.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetSeverityFilter()
        {
            _marineClient.AccessToken = GetAccessToken();
            List<Lookup> response = await _marineClient.GetHazoccSeverityFilter();
            List<TreeViewModel<Lookup>> treeList = new List<TreeViewModel<Lookup>>();
            List<TreeViewModel<Lookup>> childItems = new List<TreeViewModel<Lookup>>();

            TreeViewModel<Lookup> AllOption = new TreeViewModel<Lookup>
            {
                Title = Constants.All,
                Expanded = true,
                Key = "",
                Checkbox = true,
                Lazy = false,
                Tooltip = Constants.All,
                Children = new List<TreeViewModel<Lookup>>(),
            };

            if (response != null && response.Any())
            {
                AllOption.Children.AddRange(response.Select(y => new TreeViewModel<Lookup>
                {
                    Key = y.Identifier,
                    Title = y.Description,
                    Tooltip = y.LongDescription ?? y.Description,
                    Expanded = false,
                    Checkbox = true,
                    Lazy = false,
                    Children = null
                }));
            }
            treeList.Add(AllOption);

            return new JsonResult(treeList);
        }

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
        /// Gets the near miss LTM data.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetNearMissLtmData(string encryptedVesselId)
        {
            _marineClient.AccessToken = GetAccessToken();
            GetLastTwelveMonthSummaryRequest request = GetLastTwelveMonthSummaryRequestObject(encryptedVesselId, EnumsHelper.GetKeyValue(HazOccTypeCodes.NM));
            List<IncidentMonthSummaryViewModel> result = await _marineClient.PostGetLastTwelveMonthSummary(request);
            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the unsafe act and condition LTM data.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetUnsafeActAndConditionLtmData(string encryptedVesselId)
        {
            _marineClient.AccessToken = GetAccessToken();
            GetLastTwelveMonthSummaryRequest request = GetLastTwelveMonthSummaryRequestObject(encryptedVesselId, EnumsHelper.GetKeyValue(HazOccTypeCodes.OB));
            List<IncidentMonthSummaryViewModel> result = await _marineClient.PostGetLastTwelveMonthSummary(request);
            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the safe act and condition LTM data.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSafeActAndConditionLtmData(string encryptedVesselId)
        {
            _marineClient.AccessToken = GetAccessToken();
            GetLastTwelveMonthSummaryRequest request = GetLastTwelveMonthSummaryRequestObject(encryptedVesselId, EnumsHelper.GetKeyValue(HazOccTypeCodes.SA));
            List<IncidentMonthSummaryViewModel> result = await _marineClient.PostGetLastTwelveMonthSummary(request);
            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the last twelve month summary request object.
        /// </summary>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <param name="reportTypeId">The report type identifier.</param>
        /// <returns></returns>
        [NonAction]
        private GetLastTwelveMonthSummaryRequest GetLastTwelveMonthSummaryRequestObject(string encryptedVesselId, string reportTypeId)
        {
            GetLastTwelveMonthSummaryRequest request = null;
            if (!string.IsNullOrWhiteSpace(encryptedVesselId))
            {
                string decreptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVesselId);
                request = new GetLastTwelveMonthSummaryRequest
                {
                    VesselIds = new List<string> { decreptedString.Split(Constants.Separator)[0] },
                    Months = Constants.HazoccGraphMonthPeriod,
                    ReportTypeId = reportTypeId
                };
            }

            return request;
        }

        /// <summary>
        /// Hazs the occ summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> HazOccSummary(HazOccSummaryRequestViewModel request)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazOccSummaryResponseViewModel summaryVM = await _marineClient.PostGetHazOccSummaryDetail(request);
            return new JsonResult(summaryVM);
        }

        #region HazOcc Details       
        /// <summary>
        /// Gets the haz occ accident summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccAccidentSummary(HazOccDetailRequestViewModel request)
        {
            _marineClient.AccessToken = GetAccessToken();
            AccidentSummaryViewModel response = await _marineClient.PostGetHazOccAccidentSummary(request);
            response.ParentReport = await _marineClient.PostGetHazOccParent(response.ParentReportId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ incident summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccIncidentSummary(HazOccDetailRequestViewModel request)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazOccIncidentSummaryViewModel response = await _marineClient.PostGetHazOccIncidentSummary(request);
            response.ParentReport = await _marineClient.PostGetHazOccParent(response.ParentReportId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the near miss summary.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetNearMissSummary(string encryptedIncidentId, string vesselType)
        {
            _marineClient.AccessToken = GetAccessToken();
            NearMissSummaryViewModel response = await _marineClient.PostGetHazOccNearMissSummary(encryptedIncidentId, vesselType);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ observation summary.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccObservationSummary(HazOccDetailRequestViewModel request)
        {
            _marineClient.AccessToken = GetAccessToken();
            ObservationSummaryViewModel response = await _marineClient.PostGetHazOccObservationSummary(request);
            response.ParentReport = await _marineClient.PostGetHazOccParent(response.ParentReportId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ defect details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="encryptedVeselId">The encrypted vesel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccDefectDetails(string encryptedIncidentId, string encryptedVeselId)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<HazOccDefectDetailsViewModel> response = await _marineClient.PostGetHazOccDefectDetails(encryptedIncidentId, encryptedVeselId);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the haz occ accident event details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="categoryId">The category identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccAccidentEventDetails(string encryptedIncidentId, string categoryId)
        {
            _marineClient.AccessToken = GetAccessToken();
            AccidentEventDetailViewModel response = await _marineClient.PostGetHazOccAccidentEventDetails(encryptedIncidentId, categoryId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ near miss event details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccNearMissEventDetails(string encryptedIncidentId)
        {
            _marineClient.AccessToken = GetAccessToken();
            NearMissEventDetailViewModel response = await _marineClient.PostGetHazOccNearMissEventDetails(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ incident event details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccIncidentEventDetails(string encryptedIncidentId)
        {
            _marineClient.AccessToken = GetAccessToken();
            IncidentEventDetailViewModel response = await _marineClient.PostGetHazOccIncidentEventDetails(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ ship finding.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccShipFinding(string encryptedIncidentId, string encryptedVesselId)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazOccInitialFindingViewModel response = await _marineClient.PostGetHazOccShipFinding(encryptedIncidentId, encryptedVesselId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ investigation finding.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="encryptedVesselId">The encrypted vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccInvestigationFinding(string encryptedIncidentId, string encryptedVesselId)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazOccInvestigationFindingViewModel response = await _marineClient.PostGetHazOccInvestigationFinding(encryptedIncidentId, encryptedVesselId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ direct causes.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccDirectCauses(string encryptedIncidentId)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazOccDirectCauseViewModel response = await _marineClient.PostGetHazOccDirectCauses(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ root causes.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccRootCauses(string encryptedIncidentId)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazOccRootCauseViewModel response = await _marineClient.PostGetHazOccRootCauses(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ causation.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccCausation(string encryptedIncidentId)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazOccActionCausationViewModel response = await _marineClient.PostGetHazOccCausation(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the incident actions all.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetIncidentActionsAll(string encryptedIncidentId)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<IncidentActionViewModel> response = await _marineClient.PostGetIncidentActionsAll(encryptedIncidentId);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the hierarchy explorer mapping.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHierarchyExplorerMapping(string encryptedIncidentId)
        {
            _marineClient.AccessToken = GetAccessToken();
            string incidentId = null;
            if (!string.IsNullOrWhiteSpace(encryptedIncidentId))
            {
                incidentId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            }
            string sourceReferenceId = EnumsHelper.GetKeyValue(HierarchyExplorerMappingSource.HazOcs);

            List<HierarchyExplorerMappingDetailViewModel> response = await _marineClient.PostGetHierarchyExplorerMapping(incidentId, sourceReferenceId);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the haz occ illness summary.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccIllnessSummary(string encryptedIncidentId, string vesselType)
        {
            _marineClient.AccessToken = GetAccessToken();
            IllnessReportSummaryViewModel response = await _marineClient.PostGetHazOccIllnessReportSummary(encryptedIncidentId);
            response.ParentReport = await _marineClient.PostGetHazOccParent(response.ParentReportId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ passenger details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccPassengerDetails(string encryptedIncidentId, string vesselType)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazOccPassengerAccidentDetailViewModel response = await _marineClient.PostGetHazOccPassengerDetails(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ passenger treatment details.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccPassengerTreatmentDetails(string encryptedIncidentId, string vesselType)
        {
            _marineClient.AccessToken = GetAccessToken();
            PassengerTreatmentDetailViewModel response = await _marineClient.PostGetHazOccPassengerTreatmentDetails(encryptedIncidentId);
            return new JsonResult(new { data = response.Visits, response });
        }

        /// <summary>
        /// Gets the haz occ passenger doctors report.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccPassengerDoctorsReport(string encryptedIncidentId, string vesselType)
        {
            _marineClient.AccessToken = GetAccessToken();
            PassengerDoctorsReportViewModel response = await _marineClient.PostGetHazOccPassengerDoctorsReport(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ crew illness detail.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccCrewIllnessDetail(string encryptedIncidentId, string vesselType)
        {
            _marineClient.AccessToken = GetAccessToken();
            CrewIllnessDetailViewModel response = await _marineClient.PostGetHazOccCrewIllnessDetail(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ crew treatment detail.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccCrewTreatmentDetail(string encryptedIncidentId, string vesselType)
        {
            _marineClient.AccessToken = GetAccessToken();
            CrewTreatmentDetailViewModel response = await _marineClient.PostGetHazOccCrewTreatmentDetail(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ crew doctors report.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccCrewDoctorsReport(string encryptedIncidentId, string vesselType)
        {
            _marineClient.AccessToken = GetAccessToken();
            CrewDoctorsReportViewModel response = await _marineClient.PostGetHazOccCrewDoctorsReport(encryptedIncidentId);
            return new JsonResult(new { data = response.Visits, response });
        }

        /// <summary>
        /// Gets the haz occ third party accident detail.
        /// </summary>
        /// <param name="encryptedIncidentId">The encrypted incident identifier.</param>
        /// <param name="vesselType">Type of the vessel.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetHazOccThirdPartyAccidentDetail(string encryptedIncidentId, string vesselType)
        {
            _marineClient.AccessToken = GetAccessToken();
            HazOccThirdPartyAccidentDetailViewModel response = await _marineClient.PostGetHazOccThirdPartyAccidentDetail(encryptedIncidentId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the haz occ filter details.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        [NonAction]
        private HazOccListViewModel GetHazOccFilterDetails(HazOccListViewModel parameter)
        {
            HazOccPreviewRequestViewModel request = new HazOccPreviewRequestViewModel();
            if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.CrewAccidents))
            {
                request.IncidentType = new List<string>() { EnumsHelper.GetKeyValue(HazOccTypeCodes.CA) };
            }
            else if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.Incidents))
            {
                request.IncidentType = new List<string>() { EnumsHelper.GetKeyValue(HazOccReportCodes.Incident) };
            }
            else if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.NearMissSafetyObserve))
            {
                request.IncidentType = new List<string>() { EnumsHelper.GetKeyValue(HazOccReportCodes.NearMiss),
                    EnumsHelper.GetKeyValue(HazOccReportCodes.Observation)
                };
            }
            else if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.PassengerAccidents))
            {
                request.IncidentType = new List<string>() { EnumsHelper.GetKeyValue(HazOccTypeCodes.PA) };
            }
            else if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.VerySerious))
            {
                request.IncidentSeverity = new List<string> { EnumsHelper.GetKeyValue(HazOccSeverityStatus.VerySerious) };
                request.IncidentType = new List<string> { EnumsHelper.GetKeyValue(HazOccReportCodes.Incident) };
            }
            else if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.SeriousAccidents))
            {
                request.IncidentSeverity = new List<string>
                                                {
                                                    EnumsHelper.GetKeyValue(HazOccSeverityStatus.Serious),
                                                    EnumsHelper.GetKeyValue(HazOccSeverityStatus.VerySerious)
                                                };
                request.IncidentType = new List<string>
                                        {
                                            EnumsHelper.GetKeyValue(HazOccReportCodes.Accident)
                                        };
            }
            else if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.SeriousIncidents))
            {
                request.IncidentSeverity = new List<string>
                                                {
                                                    EnumsHelper.GetKeyValue(HazOccSeverityStatus.Serious),
                                                    EnumsHelper.GetKeyValue(HazOccSeverityStatus.VerySerious)
                                                };
                request.IncidentType = new List<string>
                                        {
                                            EnumsHelper.GetKeyValue(HazOccReportCodes.Incident)
                                        };
            }
            else if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.Total))
            {
                request.IncidentType = new List<string> {
                                        EnumsHelper.GetKeyValue(HazOccTypeCodes.CA),
                                        EnumsHelper.GetKeyValue(HazOccReportCodes.Incident),
                                        EnumsHelper.GetKeyValue(HazOccReportCodes.NearMiss),
                                        EnumsHelper.GetKeyValue(HazOccReportCodes.Observation),
                                        EnumsHelper.GetKeyValue(HazOccTypeCodes.PA),
                                        EnumsHelper.GetKeyValue(HazOccTypeCodes.TA),
                                        EnumsHelper.GetKeyValue(HazOccTypeCodes.ILL) };
            }
            else if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.ThirdPartyAccident))
            {
                request.IncidentType = new List<string> { EnumsHelper.GetKeyValue(HazOccTypeCodes.TA) };
            }
            else if (parameter.StageName == EnumsHelper.GetKeyValue(HazOccListStageFilter.Illness))
            {
                request.IncidentType = new List<string> { EnumsHelper.GetKeyValue(HazOccTypeCodes.ILL) };
            }

            if (request.IncidentType != null && request.IncidentType.Any())
            {
                parameter.SelectedIncidentTypes = string.Join(',', request.IncidentType);
            }

            if (request.IncidentStatus != null && request.IncidentStatus.Any())
            {
                parameter.SelectedIncidentStatus = string.Join(',', request.IncidentStatus);
            }

            if (request.IncidentSeverity != null && request.IncidentSeverity.Any())
            {
                parameter.SelectedIncidentSeveritys = string.Join(',', request.IncidentSeverity);
            }

            return parameter;
        }

        public async Task<IActionResult> GetHazOccLinkedInsuranceClaims(string encryptedIncidentId, string encryptedVeselId)
        {
            _marineClient.AccessToken = GetAccessToken();
            LinkedInsuranceClaimRequest request = new LinkedInsuranceClaimRequest();
            request.HazOccId = _provider.CreateProtector("HazOccIdentifier").Unprotect(encryptedIncidentId);
            string decryptedString = _provider.CreateProtector("Vessel").Unprotect(encryptedVeselId);
            request.VesselId = decryptedString.Split(Constants.Separator)[0];
            List<LinkedInsuranceClaimResponseViewModel> linkedInsuranceClaimResponses = await _marineClient.GetHazOccLinkedInsuranceClaims(request);
            return new JsonResult(new { data = linkedInsuranceClaimResponses });
        }

        #endregion
    }
}