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
using PWAFeaturesRnd.Models.Report.Notification;
using PWAFeaturesRnd.Models.Report.Shared;
using PWAFeaturesRnd.Models.Report.Vessel;
using PWAFeaturesRnd.ViewModels.Common;
using PWAFeaturesRnd.ViewModels.JSA;
using PWAFeaturesRnd.ViewModels.Notification;
using PWAFeaturesRnd.ViewModels.Shared;
using PWAFeaturesRnd.ViewModels.Vessel;

namespace PWAFeaturesRnd.Controllers.Master
{
    /// <summary>
    /// Job Safety Analysis
    /// </summary>
    /// <seealso cref="PWAFeaturesRnd.Controllers.Base.AuthenticatedController" />
    public class JSAController : AuthenticatedController
    {

        /// <summary>
        /// The client
        /// </summary>
        private readonly MarineClient _client;

        /// <summary>
        /// The provider
        /// </summary>
        private IDataProtectionProvider _provider;

        /// <summary>
        /// The SharedClient
        /// </summary>
        private SharedClient _sharedClient;

        /// <summary>
        /// The notification client
        /// </summary>
        private readonly NotificationClient _notificationClient;

        /// <summary>
        /// The notification client
        /// </summary>
        private readonly MarineWCFClient _marineWCFClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="JSAController" /> class.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="sharedClient">The shared client.</param>
        /// <param name="notificationClient">The notification client.</param>
        public JSAController(MarineClient client, IDataProtectionProvider provider, SharedClient sharedClient, NotificationClient notificationClient, MarineWCFClient marineWCFClient)
        {
            _client = client;
            _provider = provider;
            _sharedClient = sharedClient;
            _notificationClient = notificationClient;
            _marineWCFClient = marineWCFClient;
        }


        /// <summary>
        /// Lists the specified jsa request.
        /// </summary>
        /// <param name="JsaRequest">The jsa request.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult List(string JsaRequest, string VesselId)
        {
            JSAListViewModel jSAListViewModel = CommonUtil.GetDecryptedRequest<JSAListViewModel>(_provider, Constants.JSAList, JsaRequest);
            jSAListViewModel = GetJSAFilterDetails(jSAListViewModel);
            string EncryptedRequest = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, jSAListViewModel);
            string decreptedString = _provider.CreateProtector("Vessel").Unprotect(VesselId);
            string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey);
            //back button
            SetSessionDetail(EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey), null, EncryptedRequest);
            RemoveSessionFilter(_provider, EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey), null, decreptedString.Split(Constants.Separator)[0]);

            JSAListViewModel viewModel = SetJSAListViewModel(GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey)));
            viewModel.VesselName = decreptedString.Split(Constants.Separator)[1];
            viewModel.EncryptedVesselId = VesselId;
            viewModel.ActiveMobileTabClass = SetTab(pageKey, viewModel.ActiveMobileTabClass, Constants.Tab1);
            return View(viewModel);
        }

        /// <summary>
        /// Sets the jsa ListView model.
        /// </summary>
        /// <param name="jsaListURL">The jsa list URL.</param>
        /// <returns></returns>
        [NonAction]
        private JSAListViewModel SetJSAListViewModel(string jsaListURL)
        {
            JSAListViewModel jsaVm = new JSAListViewModel();
            string data = _provider.CreateProtector(Constants.JSAList).Unprotect(jsaListURL);
            jsaVm = Newtonsoft.Json.JsonConvert.DeserializeObject<JSAListViewModel>(data);
            return jsaVm;
        }

        /// <summary>
        /// Detailses this instance.
        /// </summary>
        /// <param name="JSADetails">The jsa details.</param>
        /// <param name="VesselId">The vessel identifier.</param>
        /// <param name="IsVesselChanged">if set to <c>true</c> [is vessel changed].</param>
        /// <param name="Source">The source.</param>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string JSADetails, string VesselId, bool IsVesselChanged, string Source, string context)
        {
            JSADetailsViewModel detailsViewModel = new JSADetailsViewModel();
            if (IsVesselChanged)
            {
                JSAListViewModel jsaList = new JSAListViewModel();
                jsaList.EncryptedVesselId = VesselId;
                jsaList.StageName = EnumsHelper.GetKeyValue(JSAStage.Total);
                string jsaListURL = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, jsaList);
                return RedirectToAction("List", new { JsaRequest = jsaListURL, VesselId = VesselId });
            }

            string decryptedString = CommonUtil.GetDecryptedVessel(_provider, VesselId);
            string currentVesselId = string.Empty;
            string currentVesselName = string.Empty;
            string currentVesselDisplayName = string.Empty;

            if (!string.IsNullOrWhiteSpace(decryptedString))
            {
                currentVesselId = decryptedString.Split(Constants.Separator)[0];
                currentVesselDisplayName = decryptedString.Split(Constants.Separator)[1];
                currentVesselName = CommonUtil.GetVesselNameFromDisplayName(currentVesselDisplayName);
            }

            if (!String.IsNullOrWhiteSpace(JSADetails))
            {
                detailsViewModel = CommonUtil.GetDecryptedRequest<JSADetailsViewModel>(_provider, Constants.JSADetails, JSADetails);
                detailsViewModel.VesselId = currentVesselId;
                detailsViewModel.EncryptedJobId = CommonUtil.GetEncryptedURL(_provider, Constants.JsaJobId, detailsViewModel.JobId);
            }
            else if (!String.IsNullOrWhiteSpace(context))
            {
                ContextParameter contextParameter = CommonUtil.GetDecryptedRequest<ContextParameter>(_provider, Constants.NotificationRecordDetailsEncKey, context);
                detailsViewModel.VesselId = contextParameter.VesselId;
                detailsViewModel.JobId = contextParameter.JobId;
                detailsViewModel.EncryptedJobId = CommonUtil.GetEncryptedURL(_provider, Constants.JsaJobId, detailsViewModel.JobId);
            }

            detailsViewModel.EncryptedVesselId = VesselId;
            detailsViewModel.VesselName = currentVesselDisplayName;

            _client.AccessToken = GetAccessToken();
            Task<JsaJobDetailResponseViewModel> taskHeaderDetails = _client.GetJSADetailsHeaderSummary(detailsViewModel.JobId);
            JsaJobDetailResponseViewModel headerDetails = taskHeaderDetails.Result ?? new JsaJobDetailResponseViewModel();

            string[] contextParams = { detailsViewModel.JobId };
            string[] messageParams = { headerDetails.RefNo, headerDetails.Title };
            string currentStatus = headerDetails.Status;

            detailsViewModel.MessageDetailsJSON = GetRecordLevelFeaturesJsonString(_notificationClient, MessageCategoryEnum.JSA, currentVesselId, currentVesselName, contextParams, messageParams, detailsViewModel.JobId);
            detailsViewModel.IsFromViewRecord = IsFromViewRecordVal(context);

            _marineWCFClient.AccessToken = GetAccessToken();

            List<string> controlRightsList = await _marineWCFClient.GetJSAControlRights(detailsViewModel.JobId);

            if (controlRightsList != null)
            {
                detailsViewModel.IsApproved = CanApproveJob(currentStatus, controlRightsList);
                detailsViewModel.IsRejected = CanRejectJobSafetyAnalysis(currentStatus, controlRightsList);
                detailsViewModel.IsReopened = CanReopenJobSafetyAnalysis(currentStatus, controlRightsList);
            }

            detailsViewModel.MaxRisk = headerDetails.MaxRisk;

            string pageKey = EnumsHelper.GetKeyValue(NavigationPageKey.JSADetailsPageKey);
            SetSessionDetail(pageKey, EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey), JSADetails);
            detailsViewModel.ActiveMobileTabClass = SetTab(pageKey, detailsViewModel.ActiveMobileTabClass, Constants.DropdownTab1);
            return View(detailsViewModel);
        }


        /// <summary>
        /// Gets the jsa summary and graph.
        /// </summary>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJSASummaryAndGraph(string vesselId)
        {
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

            _client.AccessToken = GetAccessToken();
            JobSafetyAnalysisDashboardViewModel result = await _client.GetJSAGraphAndSummary(request);
            return new JsonResult(result);
        }

        /// <summary>
        /// Gets the jsa list.
        /// </summary>
        /// <param name="requestVm">The request vm.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJSAList(JSAListViewModel requestVm)
        {
            _client.AccessToken = GetAccessToken();
            List<JsaJobDetailResponseViewModel> result = await _client.GetJSAList(requestVm);

            if (result != null && result.Any())
            {
                RecordDiscussionRequestViewModel recordRequest = new RecordDiscussionRequestViewModel();
                recordRequest.CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.JSA));
                recordRequest.ReferenceIds = result.Select(x => x.JobId).ToList();

                _notificationClient.AccessToken = GetAccessToken();
                List<RecordDiscussionResponse> recordResponse = await _notificationClient.GetListLevelRecordDiscussionCountByReferenceId(recordRequest);

                IEnumerable<RecordDiscussionResponse> filteredRecordResponse = recordResponse.Where(x => x.ChannelCount > 0 || x.NotesCount > 0);

                foreach (var item in filteredRecordResponse)
                {
                    JsaJobDetailResponseViewModel obj = result.FirstOrDefault(x => x.JobId == item.ReferenceIdentifier);
                    if (obj != null)
                    {
                        NewMessageParametersViewModel newMessageDetails = new NewMessageParametersViewModel
                        {
                            CategoryId = Convert.ToInt32(EnumsHelper.GetKeyValue(MessageCategoryEnum.JSA)),
                            ReferenceIdentifier = item.ReferenceIdentifier
                        };

                        obj.ChannelCount = item.ChannelCount;
                        obj.NotesCount = item.NotesCount;
                        obj.MessageDetailsJSON = JsonConvert.SerializeObject(newMessageDetails);
                    }
                }
            }

            return new JsonResult(new { data = result });
        }

        /// <summary>
        /// Sets the summary filter in temporary data.
        /// </summary>
        /// <param name="JSAUrl">The jsa URL.</param>
        /// <param name="vesselId">The vessel identifier.</param>
        /// <returns></returns>
        public IActionResult SetSummaryFilterInTempData(string JSAUrl, string vesselId)
        {
            JSAListViewModel viewModel = CommonUtil.GetDecryptedRequest<JSAListViewModel>(_provider, Constants.JSAList, JSAUrl);

            viewModel = GetJSAFilterDetails(viewModel);
            viewModel.EncryptedVesselId = vesselId;

            string jsaURL = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, viewModel);
            SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey), jsaURL, vesselId);
            return new JsonResult(new { data = viewModel });
        }

        /// <summary>
        /// Gets the jsa filter details.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns></returns>
        [NonAction]
        private JSAListViewModel GetJSAFilterDetails(JSAListViewModel viewModel)
        {
            viewModel.SelectedStatus = new List<string>();
            viewModel.SelectedRiskFilter = new List<string>();
            viewModel.SelectedSystemArea = new List<string>();
            if (viewModel.StageName == EnumsHelper.GetKeyValue(JSAStage.Total))
            {
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Planned));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.ApprovalPending));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.OfficeApprovalPending));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Approved));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Reopened));


                viewModel.SelectedRiskFilter.Add(EnumsHelper.GetKeyValue(RiskAssessment.Total));
                viewModel.SelectedRiskFilter.Add(EnumsHelper.GetKeyValue(RiskAssessment.Average));
                viewModel.SelectedRiskFilter.Add(EnumsHelper.GetKeyValue(RiskAssessment.MediumOrHigher));
            }
            else if (viewModel.StageName == EnumsHelper.GetKeyValue(JSAStage.Completed))
            {
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Completed));
            }
            else if (viewModel.StageName == EnumsHelper.GetKeyValue(JSAStage.Low))
            {
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Planned));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.ApprovalPending));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.OfficeApprovalPending));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Approved));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Reopened));

                viewModel.SelectedRiskFilter.Add(EnumsHelper.GetKeyValue(RiskAssessment.Average));
            }
            else if (viewModel.StageName == EnumsHelper.GetKeyValue(JSAStage.MidHigh))
            {
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Planned));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.ApprovalPending));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.OfficeApprovalPending));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Approved));
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Reopened));

                viewModel.SelectedRiskFilter.Add(EnumsHelper.GetKeyValue(RiskAssessment.MediumOrHigher));
            }
            else if (viewModel.StageName == EnumsHelper.GetKeyValue(JSAStage.OverdueForClosure))
            {
                viewModel.OverdueForClosure = true;
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.Approved));

            }
            else if (viewModel.StageName == EnumsHelper.GetKeyValue(JSAStage.PendingOfficeApproval))
            {
                viewModel.SelectedStatus.Add(EnumsHelper.GetKeyValue(JSAStatus.OfficeApprovalPending));

            }

            if (viewModel.SelectedStatus != null && viewModel.SelectedStatus.Count > 0)
            {
                viewModel.SelectedStatusIds = string.Join(',', viewModel.SelectedStatus);
            }

            if (viewModel.SelectedRiskFilter != null && viewModel.SelectedRiskFilter.Count > 0)
            {
                viewModel.SelectedRiskFilterIds = string.Join(',', viewModel.SelectedRiskFilter);
            }

            if (viewModel.SelectedSystemArea != null && viewModel.SelectedSystemArea.Count > 0)
            {
                viewModel.SelectedSystemAreaIds = string.Join(',', viewModel.SelectedSystemArea);
            }

            return viewModel;
        }

        /// <summary>
        /// Maintains the filter parameters.
        /// </summary>
        /// <returns></returns>
        public IActionResult MaintainFilterParameters()
        {
            string jsaFilter = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey));
            string vesselId = GetSessionVesselFilter(EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey));

            if (!string.IsNullOrWhiteSpace(jsaFilter) && !string.IsNullOrWhiteSpace(vesselId))
            {
                string decryptedViewModel = _provider.CreateProtector(Constants.JSAList).Unprotect(jsaFilter);
                JSAListViewModel viewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<JSAListViewModel>(decryptedViewModel);
                return new JsonResult(new { data = viewModel, isTempDataExist = true });
            }
            else
            {
                return new JsonResult(new { data = string.Empty, isTempDataExist = false });
            }
        }

        /// <summary>
        /// Gets the jsa status.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetJSAStatus()
        {
            List<JSAStatus> statuses = _client.GetJSAStatuses();
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

            if (statuses != null && statuses.Any())
            {
                AllOption.Children.AddRange(statuses.Select(y => new TreeViewModel<Lookup>
                {
                    Key = EnumsHelper.GetKeyValue(y),
                    Title = EnumsHelper.GetDescription(y),
                    Tooltip = EnumsHelper.GetDescription(y),
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
        /// Gets the system area.
        /// </summary>
        /// <param name="encVesselId">The enc vessel identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetSystemArea(string encVesselId)
        {
            string decrypted = CommonUtil.GetDecryptedVessel(_provider, encVesselId);
            string vesselId = decrypted.Split(Constants.Separator)[0];

            _client.AccessToken = GetAccessToken();

            List<Lookup> items = await _client.GetSystemArea(vesselId);
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

            if (items != null && items.Any())
            {
                AllOption.Children.AddRange(items.Select(y => new TreeViewModel<Lookup>
                {
                    Key = y.Identifier,
                    Title = y.Description,
                    Tooltip = y.Description,
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
        /// Sets the page parameter.
        /// </summary>
        /// <param name="inputRequest">The input request.</param>
        /// <returns></returns>
        public IActionResult SetPageParameter(JSAListViewModel inputRequest)
        {
            JSAListViewModel viewModel = new JSAListViewModel();
            viewModel.IsSearchClicked = inputRequest.IsSearchClicked;
            viewModel.EncryptedVesselId = inputRequest.EncryptedVesselId;
            viewModel.SelectedStatus = GetListOfString(inputRequest.SelectedStatus);
            viewModel.SelectedSystemArea = GetListOfString(inputRequest.SelectedSystemArea);
            viewModel.SelectedRiskFilter = GetListOfString(inputRequest.SelectedRiskFilter);
            viewModel.SelectedStatusIds = GetCommaSeparatedString(inputRequest.SelectedStatus);
            viewModel.SelectedSystemAreaIds = GetCommaSeparatedString(inputRequest.SelectedSystemArea);
            viewModel.SelectedRiskFilterIds = GetCommaSeparatedString(inputRequest.SelectedRiskFilter);
            viewModel.StageName = inputRequest.StageName;
            viewModel.OverdueForClosure = inputRequest.OverdueForClosure;
            string jsaURL = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, viewModel);
            SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey), jsaURL, inputRequest.EncryptedVesselId);

            return new JsonResult(new { data = viewModel });
        }

        /// <summary>
        /// Gets the list of string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [NonAction]
        private List<string> GetListOfString(List<string> input)
        {
            if (input != null && input.Any() && !string.IsNullOrWhiteSpace(input.FirstOrDefault()))
            {
                return input[0].Split(',').ToList();
            }
            else
            {
                return new List<string>();
            }

        }

        /// <summary>
        /// Returns the comma separated string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        [NonAction]
        private string GetCommaSeparatedString(List<string> input)
        {
            return input != null && input.Any() ? string.Join(",", input) : "";
        }

        /// <summary>
        /// Gets the risk filter.
        /// </summary>
        /// <returns></returns>
        public IActionResult GetRiskFilter()
        {

            List<Lookup> items = _client.GetRiskFilter();
            List<TreeViewModel<Lookup>> treeList = new List<TreeViewModel<Lookup>>();
            List<TreeViewModel<Lookup>> childItems = new List<TreeViewModel<Lookup>>();

            Lookup parent = items.Find(x => x.Description == "All");

            TreeViewModel<Lookup> AllOption = new TreeViewModel<Lookup>
            {
                Title = parent.Description,
                Expanded = true,
                Key = parent.Identifier,
                Checkbox = true,
                Lazy = false,
                Tooltip = Constants.All,
                Children = new List<TreeViewModel<Lookup>>(),
            };

            if (items != null && items.Any())
            {
                AllOption.Children.AddRange(items.Where(x => x.Description != "All").Select(y => new TreeViewModel<Lookup>
                {
                    Key = y.Identifier,
                    Title = y.Description,
                    Tooltip = y.Description,
                    Expanded = false,
                    Checkbox = true,
                    Lazy = false,
                    Children = null
                }));
            }
            treeList.Add(AllOption);

            return new JsonResult(treeList);
        }


        #region Details Action

        /// <summary>
        /// Gets the jsa attachments.
        /// </summary>
        /// <param name="encryptedJobId">The encrypted job identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJSAAttachments(string encryptedJobId)
        {
            _sharedClient.AccessToken = GetAccessToken();
            DocumentDetailRequest documentDetailRequest = new DocumentDetailRequest();
            string decryptedJobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, encryptedJobId);
            documentDetailRequest.SourceId = decryptedJobId;
            documentDetailRequest.SsmId = EnumsHelper.GetKeyValue(SubModule.JobSafetyAnalysis);
            //documentDetailRequest.DctId = EnumsHelper.GetKeyValue<DocumentCategory>(DocumentCategory.JobSafetyAnalysis);
            string input = _provider.CreateProtector("DocumentURL").Protect(Newtonsoft.Json.JsonConvert.SerializeObject(documentDetailRequest));
            List<DocumentDetail> response = await _sharedClient.PostGetDocumentDetails(input);
            List<DocumentDetailViewModel> result = new List<DocumentDetailViewModel>();
            if (response != null && response.Any())
            {
                foreach (DocumentDetail item in response)
                {
                    DocumentDetailViewModel viewModel = new DocumentDetailViewModel();
                    viewModel.CreatedOn = item.CreatedOn;
                    viewModel.Type = item.CategoryName;
                    viewModel.Description = !string.IsNullOrEmpty(item.Description) ? item.Description : "";
                    viewModel.Title = item.Title;
                    viewModel.CanRequestDocument = item.CanRequestDocument;
                    viewModel.IsWebAddressEditable = item.DocumentType != null && item.DocumentType == Convert.ToInt32(EnumsHelper.GetKeyValue(DocumentType.WebAddress));
                    viewModel.WebAddress = CommonUtil.GetExecutableWebAddress(item.WebAddress);
                    viewModel.EttId = item.EttId;
                    viewModel.CloudFileName = item.CloudFileName;
                    if (item.SsmId == EnumsHelper.GetKeyValue(SubModule.JobSafetyAnalysis))
                    {
                        viewModel.DocumentCategory = DocumentCategory.JobSafetyAnalysis;
                    }
                    else if (item.SsmId == EnumsHelper.GetKeyValue(SubModule.JobSafetyAnalysis))
                    {
                        viewModel.DocumentCategory = DocumentCategory.JobSafetyAnalysis;
                    }

                    result.Add(viewModel);
                }
            }
            return new JsonResult(new { data = result });
        }

        /// <summary>
        /// Gets the jsa summary details.
        /// </summary>
        /// <param name="encryptedJobId">The encrypted job identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJSASummaryDetails(string encryptedJobId)
        {
            _client.AccessToken = GetAccessToken();
            JSASummaryDetailsViewModel summaryDetails = new JSASummaryDetailsViewModel();
            string decryptedJobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, encryptedJobId);
            summaryDetails = await _client.GetJsaJobSummaryDetails(decryptedJobId);
            return new JsonResult(summaryDetails);
        }

        /// <summary>
        /// Ges the jsa summaryt hazards details.
        /// </summary>
        /// <param name="encryptedJobId">The encrypted job identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GeJSASummarytHazardsDetails(string encryptedJobId)
        {
            _client.AccessToken = GetAccessToken();
            string decryptedJobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, encryptedJobId);
            List<JSARiskAssessmentDetailViewModel> riskAssessmentDetails = await _client.GetJsaRiskAssessmentSummary(decryptedJobId);
            List<JSAHazardDetailViewModel> additionalHazard = await _client.GeJSASummarytHazardsDetails(decryptedJobId);
            if (additionalHazard != null && additionalHazard.Any())
            {
                foreach (var item in additionalHazard)
                {
                    JSARiskAssessmentDetailViewModel hazardDetailsVM = new JSARiskAssessmentDetailViewModel();
                    hazardDetailsVM.JahId = item.JahId;
                    hazardDetailsVM.Description = item.Description;
                    hazardDetailsVM.HazardNumber = item.HazardNumber;
                    hazardDetailsVM.LikelihoodDescription = item.LikelihoodDescription;
                    hazardDetailsVM.LikelihoodColor = item.LikelihoodColor;
                    hazardDetailsVM.SeverityColor = item.SeverityColor;
                    hazardDetailsVM.SeverityDescription = item.SeverityDescription;
                    hazardDetailsVM.RiskColor = item.RiskColor;
                    hazardDetailsVM.RiskFactorDescription = item.RiskFactorDescription;
                    hazardDetailsVM.WorkActivityDescription = item.WorkActivityDescription;
                    riskAssessmentDetails.Add(hazardDetailsVM);
                }
            }
            return new JsonResult(new { data = riskAssessmentDetails });
        }

        /// <summary>
        /// Gets the jsa crew summary.
        /// </summary>
        /// <param name="encryptedJobId">The encrypted job identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJsaCrewSummary(string encryptedJobId)
        {
            _client.AccessToken = GetAccessToken();
            string decryptedJobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, encryptedJobId);
            List<JSACrewDetailViewModel> crewList = await _client.GetJsaCrewSummary(decryptedJobId);
            List<JSACrewDetailViewModel> crewMembersList = new List<JSACrewDetailViewModel>();
            List<JSACrewDetailViewModel> otherMembersAttendingList = new List<JSACrewDetailViewModel>();
            foreach (JSACrewDetailViewModel JSACrew in crewList)
            {
                if (!string.IsNullOrWhiteSpace(JSACrew.CrewId) || !string.IsNullOrWhiteSpace(JSACrew.CRW_ID_TP))
                {
                    crewMembersList.Add(JSACrew);
                }
                else
                {
                    otherMembersAttendingList.Add(JSACrew);
                }
            }
            return new JsonResult(new { crewMembers = crewMembersList, otherMembers = otherMembersAttendingList });
        }

        /// <summary>
        /// Gets the jsa attribute summary.
        /// </summary>
        /// <param name="encryptedJobId">The encrypted job identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJsaAttributeSummary(string encryptedJobId)
        {
            _client.AccessToken = GetAccessToken();
            string decryptedJobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, encryptedJobId);
            List<JSAAttributeDetailViewModel> response = await _client.GetJsaAttributeSummary(decryptedJobId);
            List<JSAAttributeDetailViewModel> OfficeApprovalCollection = new List<JSAAttributeDetailViewModel>();
            List<JSAAttributeDetailViewModel> SafetyPrecautionList = new List<JSAAttributeDetailViewModel>();
            string CommunicationProtocol = "";
            string CommunicationProtocolDescription = "";
            string OtherSafetyPrecaution = "";
            bool IsOtherSafetyAvailable = false;
            int SafetyPrecautionTotalCount = 0;
            if (response != null && response.Any())
            {
                OfficeApprovalCollection = response.Where(x => x.AttributeType == EnumsHelper.GetDescription<JSAAttributeLookupType>(JSAAttributeLookupType.PermitType) && x.IsDeleted == false).ToList();
                SafetyPrecautionList = response.OrderBy(y => y.SortOrder).Where(x => x.AttributeType == EnumsHelper.GetDescription<JSAAttributeLookupType>(JSAAttributeLookupType.SafetyPrecaution) && x.IsDeleted == false).ToList();
                SafetyPrecautionTotalCount = SafetyPrecautionList != null && SafetyPrecautionList.Any() ? SafetyPrecautionList.Count : 0;
                if (response.Where(x => x.PermitNumber == null && x.JslId == null && x.IsDeleted == false).Any())
                {
                    OtherSafetyPrecaution = response.Where(x => x.PermitNumber == null && x.JslId == null && x.IsDeleted == false).FirstOrDefault().Other;
                    IsOtherSafetyAvailable = true;
                    SafetyPrecautionTotalCount++;
                }
                else
                {
                    IsOtherSafetyAvailable = false;
                }

            }

            if (response != null && response.Any(x => x.AttributeType == EnumsHelper.GetDescription<JSAAttributeLookupType>(JSAAttributeLookupType.CommunicationProtocol)))
            {
                var communicationProtocol = response.FirstOrDefault(x => x.AttributeType == EnumsHelper.GetDescription<JSAAttributeLookupType>(JSAAttributeLookupType.CommunicationProtocol));

                if (communicationProtocol != null)
                {
                    CommunicationProtocol = communicationProtocol.AttributeName;
                    CommunicationProtocolDescription = communicationProtocol.Other;
                }
            }

            return new JsonResult(new { OfficeApprovalCollection = OfficeApprovalCollection, SafetyPrecautionList = SafetyPrecautionList, CommunicationProtocol = CommunicationProtocol, CommunicationProtocolDescription = CommunicationProtocolDescription, IsOtherSafetyAvailable = IsOtherSafetyAvailable, OtherSafetyPrecaution = OtherSafetyPrecaution, SafetyPrecautionTotalCount = SafetyPrecautionTotalCount });
        }

        /// <summary>
        /// Gets the jsa task break down summary.
        /// </summary>
        /// <param name="encryptedJobId">The encrypted job identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJsaTaskBreakDownSummary(string encryptedJobId)
        {
            _client.AccessToken = GetAccessToken();
            string decryptedJobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, encryptedJobId);
            List<JSATaskBreakdownDetailViewModel> taskBreakdownList = await _client.GetJsaTaskBreakDownSummary(decryptedJobId);
            return new JsonResult(new { data = taskBreakdownList });
        }

        /// <summary>
        /// Gets the jsa details header summary.
        /// </summary>
        /// <param name="encryptedJobId">The encrypted job identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJSADetailsHeaderSummary(string encryptedJobId)
        {
            _client.AccessToken = GetAccessToken();
            string decryptedJobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, encryptedJobId);
            JsaJobDetailResponseViewModel HeaderSummary = await _client.GetJSADetailsHeaderSummary(decryptedJobId);
            return new JsonResult(HeaderSummary);
        }

        /// <summary>
        /// Ges the jsa hazards additional details.
        /// </summary>
        /// <param name="hazardId">The hazard identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJSAHazardAdditionalDetails(string hazardId)
        {
            _client.AccessToken = GetAccessToken();
            JSAHazardDetailsViewModel hazardDetails = await _client.GetJSAHazardAdditionalDetails(hazardId);
            return new JsonResult(new { data = hazardDetails });
        }

        /// <summary>
        /// Gets the jsa component summary.
        /// </summary>
        /// <param name="encryptedJobId">The encrypted job identifier.</param>
        /// <returns></returns>
        public async Task<IActionResult> GetJsaComponentSummary(string encryptedJobId)
        {
            _client.AccessToken = GetAccessToken();
            string decryptedJobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, encryptedJobId);
            List<JSAComponentDetailViewModel> componentList = await _client.GetJsaComponentSummary(decryptedJobId);
            return new JsonResult(new { data = componentList });
        }

        /// <summary>
        /// Gets the Jsa Simultaneous Jobs
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetJsaSimultaneousJobs(JSASimultaneousJobRequestViewModel request)
        {
            _client.AccessToken = GetAccessToken();
            request.JobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, request.JobId);
            var DecryptedVesselId = CommonUtil.GetDecryptedVessel(_provider, request.VesselId);
            request.VesselId = DecryptedVesselId.Split(Constants.Separator)[0];
            List<JsaJobDetailResponseViewModel> response = await _client.GetJsaSimultaneousJobs(request);
            return new JsonResult(new { data = response });
        }

        /// <summary>
        /// Gets the JSA MEeting Guidelines
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetJSAMeetingGuidelines()
        {
            _client.AccessToken = GetAccessToken();
            string response = await _client.GetJSAMeetingGuidelines();
            return new JsonResult(new { MeetingGuidelines = response });
        }

        /// <summary>
        /// Determines whether this instance [can reject job safety analysis] the specified current status.
        /// </summary>
        /// <param name="currentStatus">The current status.</param>
        /// <param name="_availableActionRights">The available action rights.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can reject job safety analysis] the specified current status; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        private bool CanRejectJobSafetyAnalysis(string currentStatus, List<string> _availableActionRights)
        {
            return (currentStatus == EnumsHelper.GetDescription<JSAStatus>(JSAStatus.ApprovalPending)
                    || currentStatus == EnumsHelper.GetDescription<JSAStatus>(JSAStatus.OfficeApprovalPending))
                    && _availableActionRights != null && _availableActionRights.Any(x => x == EnumsHelper.GetKeyValue(JSAStatus.Rejected)
                    || x == EnumsHelper.GetKeyValue(JSAStatus.Approved));
        }

        /// <summary>
        /// Determines whether this instance [can reopen job safety analysis] the specified current status.
        /// </summary>
        /// <param name="currentStatus">The current status.</param>
        /// <param name="_availableActionRights">The available action rights.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can reopen job safety analysis] the specified current status; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        private bool CanReopenJobSafetyAnalysis(string currentStatus, List<string> _availableActionRights)
        {
            return (currentStatus == EnumsHelper.GetDescription<JSAStatus>(JSAStatus.ApprovalPending)
                    || currentStatus == EnumsHelper.GetDescription<JSAStatus>(JSAStatus.OfficeApprovalPending))
                    && _availableActionRights != null && _availableActionRights.Any(x => x == EnumsHelper.GetKeyValue(JSAStatus.Reopened)
                    || x == EnumsHelper.GetKeyValue(JSAStatus.Approved));
        }

        /// <summary>
        /// Determines whether this instance [can approve job] the specified current status.
        /// </summary>
        /// <param name="currentStatus">The current status.</param>
        /// <param name="_availableActionRights">The available action rights.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can approve job] the specified current status; otherwise, <c>false</c>.
        /// </returns>
        [NonAction]
        private bool CanApproveJob(string currentStatus, List<string> _availableActionRights)
        {
            return _availableActionRights != null && _availableActionRights.Any(x => x == EnumsHelper.GetKeyValue(JSAStatus.Approved));
        }

        /// <summary>
        /// Changes the jsa status.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="remark">The remark.</param>
        /// <param name="jsaStatus">The jsa status.</param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeJSAStatus(string jobId, string remark, string jsaStatus)
        {
            string decypytedjobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, jobId);
            JSAStatus changedStatus;
            Enum.TryParse<JSAStatus>(jsaStatus, out changedStatus);

            _marineWCFClient.AccessToken = GetAccessToken();
            bool isJSAStatusChanged = await _marineWCFClient.ChangeJSAStatus(decypytedjobId, remark, changedStatus);

            return new JsonResult(new { isJSAStatusChanged = isJSAStatusChanged });
        }

		/// <summary>
		/// Gets the jsa top workflow.
		/// </summary>
		/// <param name="jobId">The job identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetJsaTopWorkflow(string jobId)
        {
            _client.AccessToken = GetAccessToken();
            LogsAndPossibleWorkFlowsRequest request = new LogsAndPossibleWorkFlowsRequest();
            request.ModuleIdentifier = EnumsHelper.GetKeyValue(SecModule.JobSafetyAnalysis);
            request.RecordId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, jobId);
            LogsAndPossibleWorkFlowsResponseViewModel response = await _client.GetLogsAndFirstPossibleWorkflow(request);
            return new JsonResult(response);
        }

        /// <summary>
		/// Gets the all jsa workflows.
		/// </summary>
		/// <param name="jobId">The job identifier.</param>
		/// <returns></returns>
		public async Task<IActionResult> GetAllJsaWorkflows(string jobId)
        {
            _client.AccessToken = GetAccessToken();
            LogsAndPossibleWorkFlowsRequest request = new LogsAndPossibleWorkFlowsRequest();
            request.ModuleIdentifier = EnumsHelper.GetKeyValue(SecModule.JobSafetyAnalysis);
            request.RecordId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, jobId);
            JsaPossibleScenarioViewModel response = await _client.GetLogsAndPossibleWorkflow(request);
            
            return new JsonResult(new { data = response.WorkflowList, activityList = response.ActivityList });
        }
        #endregion

        /// <summary>
        /// Gets the jsa approval succes URL.
        /// </summary>
        /// <param name="pageKey">The page key.</param>
        /// <returns></returns>
        public JsonResult GetJSAApprovalSuccesUrl(string pageKey)
        {
            string sourceUrl = GetSourceURLString(pageKey);
            var session = CommonUtil.GetSessionObject<Dictionary<string, object>>(HttpContext.Session, EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey));
            if (session != null)
            {
                JSAListViewModel jsaListVM = SetJSAListViewModel(GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey)));
                var SessionData = GetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey));
                string data = _provider.CreateProtector(Constants.JSAList).Unprotect(SessionData);
                jsaListVM = JsonConvert.DeserializeObject<JSAListViewModel>(data);

                jsaListVM.StageName = EnumsHelper.GetKeyValue(JSAStage.PendingOfficeApproval);
                jsaListVM.GridSubTitle = EnumsHelper.GetDescription(JSAStage.PendingOfficeApproval);
                jsaListVM.IsSearchClicked = false;
                jsaListVM.SelectedStatus = new List<string> { EnumsHelper.GetKeyValue(JSAStatus.OfficeApprovalPending) };
                jsaListVM = GetJSAFilterDetails(jsaListVM);
                string jsaURL = CommonUtil.GetEncryptedURL(_provider, Constants.JSAList, jsaListVM);
                SetSessionFilter(EnumsHelper.GetKeyValue(NavigationPageKey.JSAListPageKey), jsaURL, jsaListVM.EncryptedVesselId);
            }
            return new JsonResult(sourceUrl);
        }

        /// <summary>
        /// Changes the jsa office comments.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <param name="officecomments">The comment.</param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeJSAOfficeComments(string jobId,string officecomments)
        {
            string decypytedjobId = CommonUtil.GetDecryptedRequest<string>(_provider, Constants.JsaJobId, jobId);

            _marineWCFClient.AccessToken = GetAccessToken();
            bool isCommentsAdded = await _marineWCFClient.ChangeJSAOfficeComments(decypytedjobId, officecomments);

            return new JsonResult(new { isCommentsAdded = isCommentsAdded });
        }
    }
}
