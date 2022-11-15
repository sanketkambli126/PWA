using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PWAFeaturesRnd.Common;
using PWAFeaturesRnd.Common.Enums;
using PWAFeaturesRnd.Controllers.Base;
using PWAFeaturesRnd.Helper;
using PWAFeaturesRnd.Models.Common;
using PWAFeaturesRnd.Models.Report.Dashboard;
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.PurchaseOrder;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.Models.Report.VoyageReporting;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.Dashboard;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.Shared;
using PWAFeaturesRnd.ViewModels.VoyageReporting;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// Voyage Reporting Controller
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    public class VoyageReportingController : AuthenticatedController
    {
        /// <summary>
        /// The client
        /// </summary>
        private readonly MarineClient _marineClient;

        /// <summary>
        /// The marine WCF client
        /// </summary>
        private readonly MarineWCFClient _marineWCFClient;

        /// <summary>
        /// The DocumentClient
        /// </summary>
        private DocumentClient _documentClient;

        /// <summary>
        /// The shared client
        /// </summary>
        private readonly SharedClient _sharedClient;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// The notification client
        /// </summary>
        private readonly NotificationClient _notificationClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="VoyageReportingController" /> class.
        /// </summary>
        /// <param name="marineClient">The marine client.</param>
        /// <param name="sharedClient">The shared client.</param>
        /// <param name="provider">The provider.</param>
        public VoyageReportingController(MarineClient marineClient, SharedClient sharedClient, IDataProtectionProvider provider, NotificationClient notificationClient, MarineWCFClient marineWCFClient, DocumentClient documentClient)
        {
            _marineClient = marineClient;
            _sharedClient = sharedClient;
            _provider = provider;
            _notificationClient = notificationClient;
            _marineWCFClient = marineWCFClient;
            _documentClient = documentClient;
        }

        #region Views

        /// <summary>
        /// Vessels the position list.
        /// </summary>
        /// <param name="VoyageReportingRequestUrl">The voyage reporting request URL.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="isFromDashboard">if set to <c>true</c> [is from dashboard].</param>
        /// <param name="isVesselChanged">if set to <c>true</c> [is vessel changed].</param>
        /// <returns></returns>
        public async Task<IActionResult> VesselActivityListAsync(string VoyageReportingRequestUrl, string VesselId, bool isFromDashboard = false, bool isVesselChanged = false)
        {
            _marineClient.AccessToken = GetAccessToken();

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();

            if (!String.IsNullOrWhiteSpace(VoyageReportingRequestUrl))
            {
                string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(VoyageReportingRequestUrl);
                voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);
            }

            voyageReportingRequestVM.EncryptedVesselDetail = VesselId;

            string decryptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);

            if (isFromDashboard || isVesselChanged)
            {
                VoyageReportingRequestViewModel voyageReporting = await _marineClient.PostGetVoyageLandingVoyageReportingRequest(VesselId);
                if (!voyageReportingRequestVM.IsFromFuelEfficiency)
                {
                    voyageReportingRequestVM.FromDate = voyageReporting.FromDate;
                    voyageReportingRequestVM.ToDate = voyageReporting.ToDate;
                }
                voyageReportingRequestVM.PositionListId = voyageReporting.PositionListId;
                voyageReportingRequestVM.MenuType = voyageReporting.MenuType;
                voyageReportingRequestVM.NextActivityId = voyageReporting.NextActivityId;
                voyageReportingRequestVM.PreviousActivityId = voyageReporting.PreviousActivityId;
            }


            var voyageURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequestVM));
            SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.VoyageReportingListPageKey), null, voyageURL);
            RemoveSessionFilter(_provider, EnumsHelper.GetKeyValue(NavigationPageKey.VoyageReportingListPageKey), null, decryptedString.Split(Constants.Separator)[0]);
            var voyageReportVM = _provider.CreateProtector("VoyageReportingURL").Unprotect(GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.VoyageReportingListPageKey)));
            voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(voyageReportVM);
            voyageReportingRequestVM.EncryptedVesselDetail = VesselId;
            voyageReportingRequestVM.VesselName = decryptedString.Split(Constants.Separator)[1];



            List<VoyageActivityReportViewModel> activities = await _marineClient.PostGetVoyageActivitiesPaged(voyageReportingRequestVM);

            List<String> seaPassageActivityIds = activities.Where(x => x.IsSeaPassageEvent == true).Select(x => x.ActivityId).ToList();
            List<String> portCallActivityIds = activities.Where(x => x.IsSeaPassageEvent == false).Select(x => x.ActivityId).ToList();

            await SetChatAndNotesCountAsync(activities, seaPassageActivityIds, Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.SeaPassage)));
            await SetChatAndNotesCountAsync(activities, portCallActivityIds, Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.PortCallLocationEvent)));

            voyageReportingRequestVM.CurrentActivity = activities.Where(x => x.ActivityId == voyageReportingRequestVM.PositionListId).FirstOrDefault();
            if (voyageReportingRequestVM.CurrentActivity == null)
            {
                voyageReportingRequestVM.CurrentActivity = activities.FirstOrDefault();
            }

            //Agent details for current activity only
            if (voyageReportingRequestVM.CurrentActivity != null)
            {
                VoyageReportingRequestViewModel voyageFromAgentRequest = new VoyageReportingRequestViewModel();
                voyageFromAgentRequest.PositionListId = voyageReportingRequestVM.PreviousActivityId;
                voyageFromAgentRequest.VesselId = voyageReportingRequestVM.VesselId;

                VoyageReportingRequestViewModel voyageToAgentRequest = new VoyageReportingRequestViewModel();
                voyageToAgentRequest.PositionListId = voyageReportingRequestVM.NextActivityId;
                voyageToAgentRequest.VesselId = voyageReportingRequestVM.VesselId;

                voyageReportingRequestVM.CurrentActivity.FromAgentRequestURL = string.IsNullOrWhiteSpace(voyageReportingRequestVM.PreviousActivityId) ? null : _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageFromAgentRequest));
                voyageReportingRequestVM.CurrentActivity.ToAgentRequestURL = string.IsNullOrWhiteSpace(voyageReportingRequestVM.NextActivityId) ? null : _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageToAgentRequest));
            }

            int currentIndex = activities.IndexOf(voyageReportingRequestVM.CurrentActivity);

            voyageReportingRequestVM.PreviousActivities = new List<VoyageActivityReportViewModel>();
            voyageReportingRequestVM.ScheduleActivities = new List<VoyageActivityReportViewModel>();
            for (int i = 0; i < activities.Count; i++)
            {
                if (i < currentIndex)
                {
                    voyageReportingRequestVM.ScheduleActivities.Add(activities[i]);
                }
                else if (i > currentIndex)
                {
                    voyageReportingRequestVM.PreviousActivities.Add(activities[i]);
                }
            }

            return View(voyageReportingRequestVM);
        }

        /// <summary>
        /// Sets the chat and notes count asynchronous.
        /// </summary>
        /// <param name="activities">The activities.</param>
        /// <param name="activityIds">The activity ids.</param>
        /// <param name="categoryId">The category identifier.</param>
        private async Task SetChatAndNotesCountAsync(List<VoyageActivityReportViewModel> activities, List<String> activityIds, int categoryId)
        {
            if (activities != null && activities.Any())
            {
                RecordDiscussionRequestViewModel request1 = new RecordDiscussionRequestViewModel();
                request1.CategoryId = categoryId;
                request1.ReferenceIds = activityIds;

                _notificationClient.AccessToken = GetAccessToken();
                List<RecordDiscussionResponse> DiscussionAndNotesCountList = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(request1);

                foreach (var item in DiscussionAndNotesCountList.Where(x => x.ChannelCount > 0 || x.NotesCount > 0))
                {
                    foreach (var activity in activities.Where(x => x.ActivityId == item.ReferenceIdentifier))
                    {
                        NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
                        {
                            CategoryId = categoryId,
                            ReferenceIdentifier = item.ReferenceIdentifier
                        };

                        activity.ChannelCount = item.ChannelCount;
                        activity.NotesCount = item.NotesCount;
                        activity.MessageDetailsJSON = Uri.EscapeDataString(JsonConvert.SerializeObject(newMessageDetails));
                    }
                }
            }
        }

        /// <summary>
        /// Ports the call location event.
        /// </summary>
        /// <param name="VoyageReportingRequestUrl">The voyage reporting request URL.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IActionResult PortCallLocationEvent(string VoyageReportingRequestUrl, string VesselId, bool IsVesselChanged, string context)
        {
            if (IsVesselChanged)
            {
                return RedirectToAction("VesselActivityList", new { VoyageReportingRequestUrl, VesselId });
            }

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();

            if (!string.IsNullOrWhiteSpace(VoyageReportingRequestUrl))
            {
                string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(VoyageReportingRequestUrl);
                voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);
                voyageReportingRequestVM.ListURL = VoyageReportingRequestUrl;
            }

            string decryptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
            string currentVesselId = !String.IsNullOrWhiteSpace(decryptedString) ? decryptedString.Split(Constants.Separator)[0] : string.Empty;
            string currentVesselName = !String.IsNullOrWhiteSpace(decryptedString) ? decryptedString.Split(Constants.Separator)[1] : string.Empty;

            if (!string.IsNullOrWhiteSpace(context))
            {
                ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
                string positionListId = contextParameter.PositionListId;
                voyageReportingRequestVM = new VoyageReportingRequestViewModel() { PositionListId = positionListId };
                voyageReportingRequestVM.VesselId = currentVesselId;
                voyageReportingRequestVM.ListURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequestVM));
            }

            _marineClient.AccessToken = GetAccessToken();
            Task<VoyageActivityReportViewModel> taskResponse = _marineClient.PostGetVoyageActivityDetail(voyageReportingRequestVM.ListURL);
            VoyageActivityReportViewModel response = taskResponse.Result ?? new VoyageActivityReportViewModel();

            voyageReportingRequestVM.VesselName = currentVesselName;
            voyageReportingRequestVM.EncryptedVesselDetail = VesselId;

            string[] contextParams = { voyageReportingRequestVM.PositionListId };
            string[] messageParams = { response.ActivityDescription };

            voyageReportingRequestVM.IsFromViewRecord = IsFromViewRecordVal(context);
            voyageReportingRequestVM.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.PortCallLocationEvent, voyageReportingRequestVM.VesselId, CommonUtil.GetVesselNameFromDisplayName(voyageReportingRequestVM.VesselName), contextParams, messageParams, voyageReportingRequestVM.PositionListId);
            voyageReportingRequestVM.ActiveMobileTabClass = SetTab(EnumsHelper.GetKeyValue(NavigationPageKey.PortCallEventPageKey), voyageReportingRequestVM.ActiveMobileTabClass, Constants.Tab1);
            SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.PortCallEventPageKey), EnumsHelper.GetKeyValue(NavigationPageKey.VoyageReportingListPageKey), VoyageReportingRequestUrl);
            return View(voyageReportingRequestVM);
        }

        /// <summary>
        /// Seas the passage event.
        /// </summary>
        /// <param name="VoyageReportingRequestUrl">The voyage reporting request URL.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public IActionResult SeaPassageEvent(string VoyageReportingRequestUrl, string VesselId, bool IsVesselChanged, string context)
        {
            if (IsVesselChanged)
            {
                return RedirectToAction("VesselActivityList", new { VoyageReportingRequestUrl, VesselId });
            }

            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();

            if (!string.IsNullOrWhiteSpace(VoyageReportingRequestUrl))
            {
                string data = _provider.CreateProtector("VoyageReportingURL").Unprotect(VoyageReportingRequestUrl);
                voyageReportingRequestVM = Newtonsoft.Json.JsonConvert.DeserializeObject<VoyageReportingRequestViewModel>(data);
                voyageReportingRequestVM.ListURL = VoyageReportingRequestUrl;
            }

            string decryptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
            string currentVesselId = !String.IsNullOrWhiteSpace(decryptedString) ? decryptedString.Split(Constants.Separator)[0] : string.Empty;
            string currentVesselName = !String.IsNullOrWhiteSpace(decryptedString) ? decryptedString.Split(Constants.Separator)[1] : string.Empty;

            if (!string.IsNullOrWhiteSpace(context))
            {
                ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
                string positionListId = contextParameter.PositionListId;
                voyageReportingRequestVM = new VoyageReportingRequestViewModel() { PositionListId = positionListId };
                voyageReportingRequestVM.VesselId = currentVesselId;
                voyageReportingRequestVM.ListURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequestVM));
            }

            _marineClient.AccessToken = GetAccessToken();
            Task<VoyageActivityReportViewModel> taskResponse = _marineClient.PostGetVoyageActivityDetail(voyageReportingRequestVM.ListURL);
            VoyageActivityReportViewModel response = taskResponse.Result ?? new VoyageActivityReportViewModel();

            voyageReportingRequestVM.VesselName = currentVesselName;
            voyageReportingRequestVM.FleetTrackerURL = CommonUtil.GetFleetTrackerURL(_provider, null, new DashboardParameter() { VesselId = VesselId });
            voyageReportingRequestVM.EncryptedVesselDetail = VesselId;

            string[] contextParams = { voyageReportingRequestVM.PositionListId };
            string[] messageParams = { response.ActivityName };

            voyageReportingRequestVM.IsFromViewRecord = IsFromViewRecordVal(context);
            voyageReportingRequestVM.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.SeaPassage, voyageReportingRequestVM.VesselId, CommonUtil.GetVesselNameFromDisplayName(voyageReportingRequestVM.VesselName), contextParams, messageParams, voyageReportingRequestVM.PositionListId);
            SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.SeaPassageEventPageKey), EnumsHelper.GetKeyValue(NavigationPageKey.VoyageReportingListPageKey), VoyageReportingRequestUrl);
            voyageReportingRequestVM.ActiveMobileTabClass = SetTab(EnumsHelper.GetKeyValue(NavigationPageKey.SeaPassageEventPageKey), voyageReportingRequestVM.ActiveMobileTabClass, Constants.Tab1);
            return View(voyageReportingRequestVM);
        }

        #endregion

        /// <summary>
        /// Sets the page parameter.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public IActionResult SetPageParameter(VoyageReportingRequestViewModel input)
        {
            VoyageReportingRequestViewModel parameter = new VoyageReportingRequestViewModel
            {
                FromDate = input.FromDate,
                ToDate = input.ToDate,
                MenuType = input.MenuType,
                PositionListId = input.PositionListId,
                EncryptedVesselDetail = input.EncryptedVesselDetail
            };

            string voyageReportingUrl = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(input));
            SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.VoyageReportingListPageKey), voyageReportingUrl, input.EncryptedVesselDetail);
            return Json(Url.Action("VesselActivityList", new { VoyageReportingRequestUrl = voyageReportingUrl, VesselId = input.EncryptedVesselDetail }));
        }

        /// <summary>
        /// Gets the voyage activities list.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVoyageActivitiesList(VoyageReportingRequestViewModel input)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<VoyageActivityReportViewModel> response = await _marineClient.PostGetVoyageActivitiesPaged(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the bad weather detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetBadWeatherDetail(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            BadWeatherViewModel response = await _marineClient.PostGetBadWeatherDetail(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the delays.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDelays(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<DelayListViewModel> response = await _marineClient.PostGetDelays(input);
            return new JsonResult(new { data = response });
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
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the off hire by position identifier.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetOffHireByPosId(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<OffHireListViewModel> response = await _marineClient.PostOffHireByPosId(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the port call location document details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetDocumentDetails(string input)
        {
            _sharedClient.AccessToken = GetAccessToken();
            List<DocumentDetail> response = await _sharedClient.PostGetDocumentDetails(input);
            List<DocumentDetailViewModel> result = new List<DocumentDetailViewModel>();
            if (response != null && response.Any())
            {
                response.ForEach(x =>
                {
                    result.Add(new DocumentDetailViewModel()
                    {
                        EttId = x.EttId,
                        CreatedOn = x.CreatedOn,
                        Type = x.CategoryName,
                        Description = x.Description,
                        Title = x.Title,
                        CanRequestDocument = x.CanRequestDocument,
                        IsWebAddressEditable = x.DocumentType != null && x.DocumentType == Convert.ToInt32(EnumsHelper.GetKeyValue(DocumentType.WebAddress)),
                        WebAddress = x.WebAddress,
                        CloudFileName = x.CloudFileName,
                        DocumentCategory = DocumentCategory.PortEvent
                    });
                });
            }
            return new JsonResult(new { data = result });
        }
        public async Task<IActionResult> DownloadDocument(string input)
        {
            _documentClient.AccessToken = GetAccessToken();
            CloudDocumentDownloadRequest request = Newtonsoft.Json.JsonConvert.DeserializeObject<CloudDocumentDownloadRequest>(input);

            request.DocumentFileType = EnumsHelper.GetValues<DocumentFileType>().Where(x => EnumsHelper.GetKeyValue(x) == Path.GetExtension(request.FileName)).FirstOrDefault();
            var result = await _documentClient.DownloadDocument(request);
            byte[] byteData = result != null ? CommonUtil.ConvertStreamToByte(result) : null;
            string byteString = byteData != null ? Convert.ToBase64String(byteData) : null;
            return new JsonResult(new { filename = request.FileName, bytes = byteString, fileType = EnumsHelper.GetDescription(request.DocumentFileType) });
        }
        #region Port Call/Location Event

        /// <summary>
        /// Vessels the header details.
        /// </summary>
        /// <param name="encryptedVesselDetail">The encrypted vessel detail.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVesselPreview(string encryptedVesselDetail)
        {
            _marineClient.AccessToken = GetAccessToken();
            VesselPreviewViewModel previewViewModel = await _marineClient.PostGetVesselPreview(encryptedVesselDetail);
            return new JsonResult(previewViewModel);
        }

        /// <summary>
        /// Gets the voyage activity detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVoyageActivityDetail(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            VoyageActivityReportViewModel response = await _marineClient.PostGetVoyageActivityDetail(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the port call header.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortCallHeader(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            PortCallHeaderDetailViewModel response = await _marineClient.PostGetPortCallHeader(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the port agent detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortAgentList(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<AgentDetailViewModel> response = await _marineClient.PostGetPortAgentDetail(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the port agent detail.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortAgentDetail(string input)
        {
            _sharedClient.AccessToken = GetAccessToken();
            CompanyDetails response = await _sharedClient.PostGetCompanyDetail(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the port call location events.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortCallLocationEvents(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<PortCallLocationEventDetailViewModel> response = await _marineClient.PostGetPortCallLocationEvents(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// the get port event and rob details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortEventAndRobDetails(PortEventRobSummaryRequestViewModel input)
        {
            _marineWCFClient.AccessToken = GetAccessToken();
            PortEventDetailsViewModel response = await _marineWCFClient.GetPortEventAndRobDetails(input);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// The get port eosp event and rob details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortEospEventAndRobDetails(PortEventRobSummaryRequestViewModel input)
        {
            _marineWCFClient.AccessToken = GetAccessToken();
            PortEventDetailsViewModel response = await _marineWCFClient.GetPortEospEventAndRobDetails(input);
            return new JsonResult(response);
        }
        #endregion

        #region Sea Passage Event

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

        /// <summary>
        /// Seas the passage header.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> SeaPassageHeader(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            SeaPassageHeaderDetailViewModel response = await _marineClient.GetSeaPassageHeaderDetails(input);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the current voyage bad weather details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCurrentVoyageBadWeatherDetails(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            BreaksAndBadWeatherDetailViewModel response = await _marineClient.PostGetBreaksAndBadWeatherDetail(input, true);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the current voyage off hire details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetCurrentVoyageOffHireDetails(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            BreaksAndBadWeatherDetailViewModel response = await _marineClient.PostGetBreaksAndBadWeatherDetail(input, false);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the port delay alert.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortDelayAlert(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<VoyageActivityDelayViewModel> response = await _marineClient.PostGetPortCallDelay(input);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the port service details.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortServiceDetails(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            PortHeaderDetailsViewModel response = await _marineClient.PostGetPortHeaderDetailsByPortId(input);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the port alert.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortAlert(string input)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<PortAlertDetailViewModel> response = await _marineClient.PostGetAcknowledgedAlerts(input);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the vessel activities port alert.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <param name="portId">The port identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetVesselActivitiesPortAlert(string vesselId, string portId)
        {
            _marineClient.AccessToken = GetAccessToken();
            List<PortAlertDetailViewModel> response = await _marineClient.PostGetPortAlerts(vesselId, portId);
            return new JsonResult(response);
        }

        /// <summary>
        /// Gets the sea passage summary asynchronous.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSeaPassageGraphDetails(string posId, string vesselId)
        {
            _marineClient.AccessToken = GetAccessToken();
            SeaPassageSummary response = await _marineClient.GetSeaPassageSummary(posId);
            VoyageReportingRequestViewModel voyageReportingRequestVM = new VoyageReportingRequestViewModel();
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(vesselId);
            voyageReportingRequestVM.VesselId = decreptedString.Split(Constants.Separator)[0];
            voyageReportingRequestVM.PositionListId = posId;
            string input = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(voyageReportingRequestVM));

            SeaPassageActivityViewModel responseActivity = await _marineClient.PostGetSeaPassageReports(input);

            bool? isEosp = responseActivity != null && responseActivity.ActivityDetails != null && responseActivity.ActivityDetails.Any() ?
                              responseActivity.ActivityDetails.Last().PositionListActivityType == EnumsHelper.GetKeyValue(SeaPassageActivityType.EOSP) :
                              default(bool?);

            SeaPassageSummaryViewModel result = new SeaPassageSummaryViewModel
            {
                FromCountryCode = response.FromCountryCode,
                FromDate = response.FromDate,
                FromPortName = response.FromPortName,
                ToCountryCode = response.ToCountryCode,
                ToDate = response.ToDate,
                ToPortName = response.ToPortName,
                TotalDistance = isEosp.GetValueOrDefault() == false ? response.TotalDistance : (decimal)responseActivity.TotalDistance.GetValueOrDefault(),
                VesselProfileType = response.VesselProfileType,
                SailedDistance = (decimal)responseActivity.TotalDistance.GetValueOrDefault(),
                LastEventPosition = (responseActivity.ActivityDetails != null && responseActivity.ActivityDetails.Any()) ? responseActivity.ActivityDetails.Last().Activity : "",
                FromEventType = Constants.FAOP,
                ToEventType = Constants.EOSP,
                IsSeaPassageEvent = true,
                RequestURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(new VoyageReportingRequestViewModel { PositionListId = posId })),
                FromPortURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(new VoyageReportingRequestViewModel { PositionListId = posId, PortId = response.FromPortId, VesselId = voyageReportingRequestVM.VesselId })),
                ToPortURL = _provider.CreateProtector("VoyageReportingURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(new VoyageReportingRequestViewModel { PositionListId = posId, PortId = response.ToPortId, VesselId = voyageReportingRequestVM.VesselId })),
                HasFromPortAlert = response.HasFromPortAlert.GetValueOrDefault(),
                HasToPortAlert = response.HasToPortAlert.GetValueOrDefault()

            };

            result.BadWeatherDetails = response.WeatherDetail != null && response.WeatherDetail.Any() ?
                                        response.WeatherDetail.Where(obj => obj.IsBreakInPassage || obj.BadWeatherAlert).Select(obj => new VoyageActivityBadWeatherDetailViewModel(obj, null, _provider)).ToList() : new List<VoyageActivityBadWeatherDetailViewModel>();

            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the sea passage breaks.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <param name="spaId">The spa identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSeaPassageBreaks(string posId, string spaId)
        {
            _marineClient.AccessToken = GetAccessToken();
            NoonReportDetailsViewModel response = await _marineClient.PostGetSeaPassageBreaks(posId, spaId);
            return new JsonResult(new { data = response.SeaPassageBreaks });
        }

        #endregion

        /// <summary>
        /// Gets the dashboard full map URL.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult GetDashboardFullMapUrl(string vesselId)
        {
            DashboardMapViewModel mapViewModel = new DashboardMapViewModel();
            mapViewModel.FleetTrackerURL = CommonUtil.GetFleetTrackerURL(_provider, null, new DashboardParameter() { VesselId = vesselId });
            string mapurl = CommonUtil.GetEncryptedURL(_provider, Constants.FullMapDetails, mapViewModel);
            return new JsonResult(mapurl);
        }

        /// <summary>
        /// Gets the sea passage noon report.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <param name="spaId">The spa identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSeaPassageNoonReport(SeaPassageReportRequestViewModel requestViewModel)
        {
            _marineWCFClient.AccessToken = GetAccessToken();
            NoonReportDetailsViewModel response = await _marineWCFClient.PostGetSeaPassageNoonDetailsReport(requestViewModel);
            //response.vesselPreview = await _marineClient.PostGetVesselPreview(vesselId);

            return PartialView("_SeaPassageNoonReport", response);
        }

        /// <summary>
        /// Gets the sea passage faop details report.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <param name="spaId">The spa identifier.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSeaPassageFAOPDetailsReport(string posId, string spaId, string vesselId)
        {
            _marineWCFClient.AccessToken = GetAccessToken();
            FaopDetailViewModel response = await _marineWCFClient.PostGetSeaPassageFAOPDetailsReport(posId, spaId, vesselId);

            return PartialView("_SeaPassageFAOP", response);
        }

        /// <summary>
        /// Gets the change in destination report.
        /// </summary>
        /// <param name="posId">The position identifier.</param>
        /// <param name="spaId">The spa identifier.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetChangeInDestinationReport(string posId, string spaId, string vesselId)
        {
            _marineWCFClient.AccessToken = GetAccessToken();
            ChangeInDestinationViewModel response = await _marineWCFClient.PostGetChangeInDestination(posId, spaId, vesselId);

            return PartialView("_SeaPassageDestinationChanged", response);
        }

        /// <summary>
        /// The get port Manoeuring event details
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetPortEventManoeuvringDetails(PortEventRobSummaryRequestViewModel input)
        {
            _marineWCFClient.AccessToken = GetAccessToken();
            PortEventDetailsViewModel response = await _marineWCFClient.GetPortEventManoeuvringDetails(input);
            return new JsonResult(response);
        }
    }
}